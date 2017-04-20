using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dataSource;

namespace DynamicIP
{

    public class DynamicIPBLL
    {
        public static Dictionary<string, dataViewModel> vpnEntrys = new Dictionary<string, dataViewModel>();
        private static dataSource.dvModelBLL mBll = new dvModelBLL();
        public DynamicIPBLL() { }
        public string add(dataViewModel reg)
        {
            reg.createDate = DateTime.Now;
            reg.KEY = createKey(reg.Name, reg.vpnID, reg.vpnPW, reg.vpnMac);
            reg.Name = string.IsNullOrEmpty(reg.Name) ? " " : reg.Name;
            reg.vpnID = string.IsNullOrEmpty(reg.vpnID) ? " " : reg.vpnID;
            vpnEntrys.Add(reg.KEY, reg);
            mBll.Create(reg);
            return reg.KEY;
        }
        public string update(dataViewModel reg)
        {
            vpnEntrys[reg.KEY] = reg;
            mBll.Update(reg);
            return reg.KEY;

        }

        /// <summary>
        /// 注册新IP地址,返回值:成功>0,失败<=0
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="IP"></param>
        /// <returns></returns>
        public string registerIP(string Key, string IP)
        {
            string r = "";
            if (Exist(key: Key))
            {
                var entryUpdate = vpnEntrys.ContainsKey(Key) ? vpnEntrys[Key] : null;
                if (entryUpdate.vpnIP == IP) return "IP地址未变更!";
                entryUpdate.vpnIP = IP;
                entryUpdate.modifyDate = DateTime.Now;
                mBll.Update(entryUpdate);
                r = "成功!";
            }
            else r = "key " + Key + " 不存在!";
            return r;
        }
        
        public int delete(string key)
        {
            int r = -1;
            if (vpnEntrys.ContainsKey(key))
            {
                r = vpnEntrys.Remove(key) ? 1 : -1;
                mBll.Delete(key);
            }
            else r = -1;
            return r;
        }
        public IEnumerable<dataViewModel> filter(string name = null, string key = null)
        {

            if (vpnEntrys == null) { vpnEntrys = new Dictionary<string, dataViewModel>(); getLists(); }
            if (vpnEntrys.Count <= 0) { getLists(); }

            var vpns1 = vpnEntrys.Values.ToList().FindAll(fa => fa.KEY == key);
            var vpns2 = vpnEntrys.Values.ToList().FindAll(fa => fa.Name == name);
            var vpns = vpns1.Union(vpns2);
            return vpns;
        }
        public bool Exist(string name = null, string key = null)
        {
            bool r = false;
            r = filter(name, key).Count() > 0;
            return r;
        }
        public string createKey(dataViewModel dvModel) {
            return createKey(dvModel.Name, dvModel.vpnID, dvModel.vpnPW, dvModel.vpnMac);
        }
        public string createKey(string name, string id, string pwd, string mac)
        {
            string strKey = string.Empty;
            if (string.IsNullOrEmpty(name)
                && string.IsNullOrEmpty(id)
                && string.IsNullOrEmpty(pwd)
                && string.IsNullOrEmpty(mac)) return null;
            StringBuilder r = new StringBuilder();
            r.Append(id);
            r.Append(name);
            r.Append(pwd);
            r.Append(mac);
            strKey = MD5_Crypto.MD5(r.ToString());//加密
            return strKey;
        }


        public dataViewModel signal(string key)
        {
            return vpnEntrys[key];
        }
        public List<dataViewModel> getLists()
        {
            List<dataViewModel> dvModels = mBll.getList(null);
            //List<dataViewModel> dvModels = new List<dataViewModel>();
            IEnumerable <dataViewModel> except = dvModels.Except(vpnEntrys.Values.ToList(),new ModelComparer());
            if (except.Count() > 0) {
                foreach (dataViewModel dvm in except) {
                    vpnEntrys.Add(dvm.KEY, dvm);
                }
            }
            return vpnEntrys.Values.ToList() ;
        }
    }
    
    public class ModelComparer : IEqualityComparer<dataViewModel>
    {
        public bool Equals(dataViewModel x, dataViewModel y)
        {
            return x.KEY == y.KEY;
        }

        public int GetHashCode(dataViewModel model)
        {
            //Check whether the object is null
            if (Object.ReferenceEquals(model, null)) return 0;

            //Get hash code for the Name field if it is not null.
            int hashModelName = model.Name == null ? 0 : model.Name.GetHashCode();

            //Get hash code for the Code field.
            int hashModelCode = model.KEY == null ? 0 : model.KEY.GetHashCode();

            //Calculate the hash code for the product.
            return hashModelName ^ hashModelCode;            
        }
    }
}
