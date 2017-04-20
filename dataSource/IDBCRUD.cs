using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentData;

namespace dataSource
{
    public interface IdbCRUD<T>
    {
        //CREATE,RETRIEVE,UPDATE,DELETE:增加,读取,更新,删除
        /// <summary>
        /// 增加记录
        /// </summary>
        /// <param name="t">增加记录实例</param>
        /// <returns>新记录的ID</returns>
        long Create(T t);
        T Retrieve(int id);
        T Retrieve(string code);
        int Update(T t);
        int Delete(long id);
        int Delete(string code);
        List<T> getList(T t);
    }
    public abstract class dbCRUD<T> : IdbCRUD<T>
    {
        public abstract string WhereStr(T t);

        public abstract void Mapper(T m, IDataReader row);
        public abstract long Create(T t);

        public abstract int Delete(long id);
        public abstract int Delete(string code);

        public abstract List<T> getList(T t);

        public abstract T Retrieve(string code);

        public abstract T Retrieve(int id);

        public abstract int Update(T t);
    }

}
