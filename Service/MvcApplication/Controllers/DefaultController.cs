using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DynamicIP;
using dataSource;

namespace MvcApplication.Controllers
{
    public class DefaultController : Controller
    {

        static DynamicIPBLL Bll = new DynamicIPBLL();
        //
        // GET: /Default/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult managerTable(string id)
        {
            var model = Bll.getLists();
            return View(model);
        }
        public JsonResult flushClientException(string id)
        {

            JsonResult rJson = new JsonResult();
            rJson.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            rJson.MaxJsonLength = int.MaxValue;

            rJson.Data = null;

            var ls = Bll.getLists();
            if (ls != null && ls.Count >= 1)
            {
                switch (id)
                {
                    case "register":
                        rJson.Data = from i in ls select i.KEY;
                        break;
                    case "dialer":
                        rJson.Data = from i in ls select i.KEY + "-" + i.vpnIP;
                        break;
                    default:
                        rJson.Data = id;
                        break;
                }
            }
            else
            {
                Bll = new DynamicIPBLL();
                if (Bll.getLists().Count > 0) flushClientException(id);
                else rJson.Data = "数据空，请联系管理员，检查服务数据表。";
            }
            return rJson;
        }

        [HttpPost]
        public JsonResult getUpdatedRow(string id) {//prev Param:int id
            JsonResult r = new JsonResult();
            r.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            r.MaxJsonLength = int.MaxValue;

            dataViewModel vpn = Bll.signal(id);

            r.Data = vpn;
            return r;
        }
        [HttpPost]
        public ActionResult Update(FormCollection f)
        {//{ autoid: id, name: name, vpnid: vpnid, vpnpw: vpnpw, mac: mac, ip: ip, EncryptionType: EncryptionType }
            dataViewModel reg = new dataViewModel();
            if (f.AllKeys.Contains("name") && !string.IsNullOrEmpty(f["name"]))
                reg.Name = f["name"].ToString();
            if (f.AllKeys.Contains("vpnid") && !string.IsNullOrEmpty(f["vpnid"]))
                reg.vpnID = f["vpnid"].ToString();
            if (f.AllKeys.Contains("vpnpwd") && !string.IsNullOrEmpty(f["vpnpwd"]))
                reg.vpnPW = f["vpnpwd"].ToString();
            if (f.AllKeys.Contains("mac") && !string.IsNullOrEmpty(f["mac"]))
                reg.vpnMac = f["mac"].ToString();
            if (f.AllKeys.Contains("ip") && !string.IsNullOrEmpty(f["ip"]))
                reg.vpnIP = f["ip"].ToString();
            if (f.AllKeys.Contains("vpnEncryptionType") && !string.IsNullOrEmpty(f["vpnEncryptionType"]))
                reg.vpnEncryptionType = f["vpnEncryptionType"].ToString();
            if (f.AllKeys.Contains("KEY") && !string.IsNullOrEmpty(f["KEY"]))
                reg.KEY = f["KEY"].ToString();
            //if (f.AllKeys.Contains("autoid") && !string.IsNullOrEmpty(f["autoid"]))
            //    reg.autoid = int.Parse(f["autoid"].ToString());

            string savestate = string.Empty;
            if (string.IsNullOrEmpty(reg.KEY))
                savestate = Bll.add(reg);
            else
                savestate = Bll.update(reg);

            JsonResult rJson = new JsonResult();
            rJson.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            rJson.MaxJsonLength = 64;
            rJson.Data = savestate;
            return rJson;
        }


        [HttpPost]
        public ActionResult Delete(string id)//prev Param:int id
        {
            int savestate = -1;
            savestate = Bll.delete(id);

            JsonResult rJson = new JsonResult();
            rJson.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            rJson.MaxJsonLength = 64;
            rJson.Data = savestate;
            return rJson;
        }
        [HttpPost]
        public ActionResult Import(string id, string DIPModelsString)
        {
            JsonResult rJson = new JsonResult();
            rJson.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            rJson.MaxJsonLength = 64;
            /* Bug : DIPModels做为参数，传入后List.Count是对的，但其中元素为空
            List<dataViewModel> DIPModels = new List<dataViewModel>(); ;
            if (DIPModels != null && DIPModels.Count > 0)
            {
                foreach (dataViewModel dm in DIPModels)
                {
                    if (Bll.Exist(dm.Name, dm.KEY))
                        Bll.update(dm);
                    else
                        Bll.add(dm);                            
                }
                rJson.Data = "已导入数据，请核对！";
                return rJson;
            }
            else
            {
                rJson.Data = "无导入数据！";
                
            }
            */
            //分析传入的DIPModelsString
            var strList = DIPModelsString.Split('\n');
            dataViewModel dip = new dataViewModel();
            if (string.IsNullOrEmpty(DIPModelsString))
            {
                rJson.Data = "无导入数据！";
                return rJson;
            }
            foreach (string strItem in strList)
            {
                dip = new dataViewModel();
                var elems = strItem.Split(',');
                if (elems.Length < 3) continue;
                dip.Name = elems[0];
                dip.vpnID = elems[1];
                dip.vpnPW = elems[2];
                dip.KEY = Bll.createKey(dip);

                if (Bll.Exist(dip.Name, dip.KEY))
                    Bll.update(dip);
                else
                    Bll.add(dip);

            }
            rJson.Data = "已导入数据，请核对！";
            return rJson;
        }

        [HttpPost]
        public ActionResult RegisterIP(FormCollection f)
        {
            string Key = string.Empty;
            string IP = string.Empty;
            string savestate = "";
            string _ip = Request.UserHostAddress;
            if (f.AllKeys.Contains("Key") && !string.IsNullOrEmpty(f["Key"]))
                Key = f["Key"].ToString();
            if (f.AllKeys.Contains("ip") && !string.IsNullOrEmpty(f["ip"]))
                IP = f["ip"].ToString();
            else IP = _ip;
            savestate = Bll.registerIP(Key, IP);

            JsonResult rJson = new JsonResult();
            rJson.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            rJson.MaxJsonLength = 64;
            rJson.ContentEncoding = System.Text.Encoding.GetEncoding("gb2312");
            rJson.Data = savestate;
            return rJson;
        }

        [HttpPost]
        public JsonResult getIP(FormCollection f)
        {
            JsonResult rJson = new JsonResult();
            rJson.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            rJson.MaxJsonLength = int.MaxValue;
            string keys = f.AllKeys.Contains("Keys") ? f["Keys"] : "";
            keys += ",";
            dataViewModel vpn;
            List<dataViewModel> r = new List<dataViewModel>();
            string[] Keys = keys.Split(',');
            foreach (string k in Keys)
            {
                if ( (!string.IsNullOrEmpty(k)) && Bll.Exist(key: k))
                {
                    vpn = Bll.signal(k);
                    r.Add(vpn);
                }
            }

            rJson.Data = r;
            return rJson;
        }
        [HttpPost]
        public JsonResult authen(string authenCode) {

            JsonResult rJson = new JsonResult();
            rJson.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            rJson.MaxJsonLength = int.MaxValue;

            rJson.Data = ("authenCode" == authenCode);

            return rJson;
        }

    }
}
