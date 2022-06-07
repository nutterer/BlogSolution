using BlogSolution.Models.Bussiness.Connection;
using database.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogSolution.Models.Bussiness.Base
{
    public class BaseBC
    {
        public db_BlogEntities qDB;
        public bool isResult = true;

        #region Contructor
        public BaseBC() 
        {
            ConnectionBC bc = new ConnectionBC();
            qDB = bc.GetConnection();
             if (qDB != null)
                qDB.Database.CommandTimeout = 180;
        }

        public BaseBC(db_BlogEntities context)
        {
            qDB = context;
            if (qDB != null)
                qDB.Database.CommandTimeout = 180;
        }
        #endregion

        #region SaveDefault
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">Type (ตาราง) เช่น Member</typeparam>
        /// <param name="sqlSelect"></param>
        /// <param name="sqlWhere"></param> 
        /// <param name="IsOrderBy">If No need Order by</param>
        /// <returns></returns>
        protected T saveDefault<T>(T model)
        {
            try
            {
                if (qDB != null)
                {
                    Type t = typeof(T);
                    var table = qDB.Set(t);
                    table.Add(model);
                    qDB.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                model = default(T);
            }
            return model;
        }
        #endregion

        #region SQLCommand
        public bool sqlCommandExcute(string sql)
        {
            try
            {
                if (qDB != null)
                {
                    if (!string.IsNullOrEmpty(sql))
                    {
                        qDB.Database.ExecuteSqlCommand(sql);
                    }
                    isResult = true;
                }
            }
            catch (Exception ex)
            {
                isResult = false;
            }
            return isResult;
        }

        #endregion
    }
}