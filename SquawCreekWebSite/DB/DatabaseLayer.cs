using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace SquawCreekWebSite.DB
{
    public class DatabaseLayer
    {
        public MySqlConnection dbConnection { get; set; }

        public DatabaseLayer()
        {
            Init();
        }

        private void Init()
        {
            string connectionString = "server=localhost;database=squawcreek;uid=root;pwd=Milly10_;";
            dbConnection = new MySqlConnection(connectionString);
        }

        protected bool OpenDBConnection()
        {
            try
            {
                dbConnection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                return false;
            }
        }

        protected bool CloseDBConnection()
        {
            try
            {
                dbConnection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                return false;
            }
        }   
    }
}