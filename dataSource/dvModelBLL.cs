using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentData;
using System.IO;

namespace dataSource
{
    public class dvModelBLL : dbCRUD<dataViewModel>
    {
        IDbContext dvContext;
        public dvModelBLL()
        {
            ///使用Sqlite 数据文件在软件目录\DB\localConfig.db
            ///表名dvList
            initDBFile();
        }
        private void initDBFile() {
            string dbPath = getDBPath();
            string dbDir = Path.GetDirectoryName(dbPath);
            string dbConn = "Data Source = " + dbPath;
            if (!Directory.Exists(dbDir))
                Directory.CreateDirectory(dbDir);
            dvContext = new DbContext().ConnectionString(dbConn, new SqliteProvider());
            if (!isExistTable("dvList")) {
                initDBTable();
            }
        }
        private void initDBTable() {
            string cmd = string.Empty;//select KEY,Name,vpnID,vpnPW,vpnIP,vpnEncryptionType,createDate,modifyDate,enable,vpnMac
            cmd = "CREATE TABLE dvList( KEY TEXT PRIMARY KEY ,Name TEXT,vpnID TEXT,vpnPW TEXT,vpnIP TEXT,vpnEncryptionType TEXT,createDate TEXT,modifyDate TEXT,enable INTEGER,vpnMac TEXT )";
            dvContext.Sql(cmd).Execute();
        }
        private bool isExistTable(string tableName)
        {
            bool r = false;
            string cmd = "select [name] from sqlite_master where [type]='table' and [name]='" + tableName + "'";
            string tn = dvContext.Sql(cmd).QuerySingle<string>();
            r = !string.IsNullOrEmpty(tn);
            return r;
        }
        public string getDBPath()
        {
            string r = string.Empty;// System.Configuration.ConfigurationManager.ConnectionStrings["dbConn"].ToString();
            //r = Environment.CurrentDirectory + "/Data/localConfig.db";
            r = AppDomain.CurrentDomain.BaseDirectory + "/Data/localConfig.db";
            return r;
        }
        public override long Create(dataViewModel t)
        {
            int r = -1;
            r = dvContext.Insert("dvList", t)
                .Column("KEY", t.KEY)
                .Column("Name", t.Name)
                .Column("vpnID", t.vpnID)
                .Column("vpnPW", t.vpnPW)
                .Column("vpnIP", t.vpnIP)
                .Column("vpnEncryptionType", t.vpnEncryptionType)
                .Column("createDate", t.createDate)
                .Column("modifyDate", t.modifyDate)
                .Column("enable", t.enable.HasValue ? (t.enable.Value ? 1 : 0) : 0)
                .Column("vpnMac", t.vpnMac)
                .Execute();//.ExecuteReturnLastId<int>("id");
            return r;
        }

        public override int Delete(string code)
        {
            int r = -1;
            if (!string.IsNullOrEmpty(code))
                r = dvContext.Delete("dvList").Where("KEY",code).Execute();
            return r;
        }
        public override int Delete(long id) { throw new NotImplementedException(); }
        public override List<dataViewModel> getList(dataViewModel t)
        {
            string cmd = string.Empty;
            List<dataViewModel> dvList = new List<dataViewModel>();
            cmd = " select KEY,Name,vpnID,vpnPW,vpnIP,vpnEncryptionType,createDate,modifyDate,enable,vpnMac from dvList where 1 = 1 " + WhereStr(t);
            dvList = dvContext.Sql(cmd).QueryMany<dataViewModel>(Mapper);
            return dvList;
        }



        public override dataViewModel Retrieve(string code)
        {
            string cmd = string.Empty;
            dataViewModel dvm = null;
            if (string.IsNullOrEmpty(code))
            {
                cmd = " select KEY,Name,vpnID,vpnPW,vpnIP,vpnEncryptionType,createDate,modifyDate,enable,vpnMac from dvList where 1 = 1  and key = " + code;
                dvm = dvContext.Sql(cmd).QuerySingle<dataViewModel>(Mapper);
            }
            return dvm;
        }

        public override dataViewModel Retrieve(int id)
        {
            throw new NotImplementedException();
        }

        public override int Update(dataViewModel t)
        {
            int r = -1;
            r = dvContext.Update("dvList", t)
                .Column("KEY", t.KEY)
                .Column("Name", t.Name)
                .Column("vpnID", t.vpnID)
                .Column("vpnPW", t.vpnPW)
                .Column("vpnIP", t.vpnIP)
                .Column("vpnEncryptionType", t.vpnEncryptionType)
                .Column("createDate", t.createDate)
                .Column("modifyDate", t.modifyDate)
                .Column("enable", t.enable.HasValue?(t.enable.Value?1:0):0)
                .Column("vpnMac", t.vpnMac)
                .Where("KEY", t.KEY)
                .Execute();
            return r;
        }

        public override string WhereStr(dataViewModel t)
        {
            string r = string.Empty;
            if (t != null)
            {
                if (string.IsNullOrEmpty(t.KEY))
                    r += (" and KEY = " + t.KEY);
                if (string.IsNullOrEmpty(t.Name))
                    r += (" and Name like '%" + t.Name + "%'");
                if (string.IsNullOrEmpty(t.vpnID))
                    r += (" and vpnID like '%" + t.vpnID + "%'");
                if (string.IsNullOrEmpty(t.vpnMac))
                    r += (" and vpnMac like '%" + t.vpnMac + "%'");
            }
            return r;
        }

        public override void Mapper(dataViewModel m, IDataReader row)
        {
            m.KEY = row.GetString("KEY");
            m.Name = row.GetString("Name");
            m.vpnID = row.GetString("vpnID");
            m.vpnPW = row.GetString("vpnPW");
            m.vpnIP = row.GetString("vpnIP");
            m.vpnEncryptionType = row.GetString("vpnEncryptionType");
            var cDate = row.GetString("createDate");
            var mDate = row.GetString("modifyDate");
            var e = row.GetInt64("enable");
            m.createDate = string.IsNullOrEmpty(cDate) ? new DateTime() : DateTime.Parse(cDate);
            m.modifyDate = string.IsNullOrEmpty(mDate) ? new DateTime() : DateTime.Parse(mDate);
            m.enable = (e > 0);
                
            m.vpnMac = row.GetString("vpnMac");
        }
    }
}
