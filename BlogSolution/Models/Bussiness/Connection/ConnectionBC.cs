using BlogSolution.Models.ModelApp.Connection;
using database.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.Linq;
using System.Web;


namespace BlogSolution.Models.Bussiness.Connection
{
    public class objDatabaseContainer : db_BlogEntities
    {
        public objDatabaseContainer(string connectString = null) { }
    }
    public class ConnectionBC
    {
        private string dbContainer = "db_BlogEntities";

        #region get cookie connection
        public objDatabaseContainer GetConnection()
        {
            objDatabaseContainer qDB = new objDatabaseContainer();
            try
            {
                ConnectionModel model = new ConnectionModel();

                model.Host = "DESKTOP-R73BTUJ";
                model.DataBase = "db_Blog";
                model.Username = "sa";
                model.Password = "1";

                qDB = this.GetConnectonContainer(model);
            }
            catch (Exception) { }
            return qDB;
        }
        #endregion

        #region get connection container
        public objDatabaseContainer GetConnectonContainer(ConnectionModel model)
        {
            objDatabaseContainer qDB = new objDatabaseContainer(this.generateConnectionString(model));
            try
            {
                if (!String.IsNullOrEmpty(model.Host) && !String.IsNullOrEmpty(model.DataBase) &&
                    !String.IsNullOrEmpty(model.Username) && !String.IsNullOrEmpty(model.Password))
                {
                    this.createObjectConnectionString(qDB, model);
                    if (qDB.Database.Connection.State != ConnectionState.Open)
                        qDB.Database.Connection.Open();

                    qDB.Database.Connection.Close();
                }
            }
            catch (Exception) { }
            return qDB;
        }
        #endregion

        #region generateConnectionString
        public string generateConnectionString(ConnectionModel model)
        {
            var conStr = "";
            conStr += "metadata=res://iBiz.DataBase/DataBaseModel.iBiz.csdl|res://iBiz.DataBase/DataBaseModel.iBiz.ssdl|res://iBiz.DataBase/DataBaseModel.iBiz.msl;provider=System.Data.SqlClient;provider connection string=\"data source = \"";
            conStr += model.Host;
            conStr += "initial catalog=";
            conStr += model.DataBase;
            conStr += ";persist security info=True;user id=";
            conStr += model.Username;
            conStr += ";password=";
            conStr += model.Password;
            conStr += ";MultipleActiveResultSets=True;App=EntityFramework";
            return conStr;
        }
        #endregion

        #region createObjectConnectionString
        public void createObjectConnectionString(objDatabaseContainer obj, ConnectionModel model)
        {
            try
            {
                var configNameEf = string.IsNullOrEmpty(this.dbContainer) ? obj.GetType().Name : this.dbContainer;
                var entityCnxStringBuilder = new EntityConnectionStringBuilder(System.Configuration.ConfigurationManager.ConnectionStrings[configNameEf].ConnectionString);
                var sqlCnxStringBuilder = new SqlConnectionStringBuilder(entityCnxStringBuilder.ProviderConnectionString);

                if (!string.IsNullOrEmpty(model.Host))
                    sqlCnxStringBuilder.InitialCatalog = model.DataBase;
                if (!string.IsNullOrEmpty(model.DataBase))
                    sqlCnxStringBuilder.DataSource = model.Host;
                if (!string.IsNullOrEmpty(model.Username))
                    sqlCnxStringBuilder.UserID = model.Username;
                if (!string.IsNullOrEmpty(model.Password))
                    sqlCnxStringBuilder.Password = model.Password;
                sqlCnxStringBuilder.IntegratedSecurity = false;
                sqlCnxStringBuilder.MultipleActiveResultSets = true;
                sqlCnxStringBuilder.PersistSecurityInfo = true;
                obj.Database.Connection.ConnectionString = sqlCnxStringBuilder.ConnectionString;
            }
            catch (Exception) { }
        }
        #endregion
    }
}