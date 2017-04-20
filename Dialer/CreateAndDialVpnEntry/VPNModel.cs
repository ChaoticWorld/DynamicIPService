using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace Dialer
{
    [Serializable]
    public partial class vpn_Entry
    {
        public string KEY { get; set; }
        public string Name { get; set; }
        public string vpnID { get; set; }
        public string vpnPW { get; set; }
        public string vpnIP { get; set; }
        public string vpnEncryptionType { get; set; }
        public System.DateTime createDate { get; set; }
        public Nullable<System.DateTime> modifyDate { get; set; }
        public Nullable<bool> enable { get; set; }
        //public int autoid { get; set; }
        public string vpnMac { get; set; }
        public NetworkCredential nc { get; set; }
    }

}
