using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dataSource
{
    public class dataViewModel
    {
        public dataViewModel() { }
        public string KEY { get; set; }
        public string Name { get; set; }
        public string vpnID { get; set; }
        public string vpnPW { get; set; }
        public string vpnIP { get; set; }
        public string vpnEncryptionType { get; set; }
        public System.DateTime createDate { get; set; }
        public Nullable<System.DateTime> modifyDate { get; set; }
        public Nullable<bool> enable { get; set; }        
        public string vpnMac { get; set; }
        //select KEY,Name,vpnID,vpnPW,vpnIP,vpnEncryptionType,createDate,modifyDate,enable,vpnMac
    }

}
