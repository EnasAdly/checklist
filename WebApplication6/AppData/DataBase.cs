using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace WebApplication6.AppData
{
    public class DataBase
    {
         static SqlConnection Database_Conection = new SqlConnection(WebConfigurationManager.ConnectionStrings["CheckLists"].ConnectionString);

        public static void insert(
            string UserName,
            string Password,
            string Getdate
            )
        {
            Database_Conection.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "LoginIn";
            cmd.Parameters.Add("UserName", UserName);
            cmd.Parameters.Add("Password", Password);
            cmd.Parameters.Add("Getdate", Getdate);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = Database_Conection;
            cmd.ExecuteNonQuery();
            Database_Conection.Close();
        }
        public static void selectfilter
            (string ApplicationName)
        {

            Database_Conection.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "ChecFilter";
            cmd.Parameters.Add("ApplicationName ", ApplicationName);          
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = Database_Conection;
            cmd.ExecuteNonQuery();
            Database_Conection.Close();
        }
        public static void insertNewApp(
            string ApplicationName,
            string ServerPort
            )
        {
            Database_Conection.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "AddNewApp";
            cmd.Parameters.Add("ApplicationName", ApplicationName);
            cmd.Parameters.Add("ServerPort", ServerPort);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = Database_Conection;
            cmd.ExecuteNonQuery();
            Database_Conection.Close();
        }
        public static void insertNewConfig(
            string ApplicationName,
            string ServerPort,
            string status,
            string statusDesc
            )
        {
            Database_Conection.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "InsertAppDesc";
            cmd.Parameters.Add("ApplicationName", ApplicationName);
            cmd.Parameters.Add("ServerPort", ServerPort);
            cmd.Parameters.Add("statusname", status);
            cmd.Parameters.Add("StatusDesc", statusDesc);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = Database_Conection;
            cmd.ExecuteNonQuery();
            Database_Conection.Close();
        }
        public static void insertNewAppDEsc(
            string ApplicationName,
            string ServerPort,
            string status,
            string statusDesc,
            string GroupName
            )
        {
            Database_Conection.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "AddNewApptoAppDesc";
            cmd.Parameters.Add("ApplicationName", ApplicationName);
            cmd.Parameters.Add("ServerPort", ServerPort);
            cmd.Parameters.Add("statusname", status);
            cmd.Parameters.Add("StatusDesc", statusDesc); ;
            cmd.Parameters.Add("GroupName", GroupName); ;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = Database_Conection;
            cmd.ExecuteNonQuery();
            Database_Conection.Close();
        }
        public static void DeleteAppDEsc(
            string ApplicationName
            
            )
        {
            Database_Conection.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "DeleteApplication ";
            cmd.Parameters.Add("ApplicationName", ApplicationName);          
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = Database_Conection;
            cmd.ExecuteNonQuery();
            Database_Conection.Close();
        }
        public static string Select(string UserName)
        {
            Database_Conection.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "CheckLogin";
            //cmd.Parameters.Add("AccountNumber",AccountNumber);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = Database_Conection;
            cmd.Parameters.Add("UserName", UserName);
            SqlParameter retval = new SqlParameter("@result", SqlDbType.NVarChar, 60);  
            // param.Direction = ParameterDirection.Input;
            retval.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(retval);
            cmd.ExecuteNonQuery();

            //param.Value = AccountNumber;
            //cmd.Parameters.Add(param);
            //cmd.Parameters.Add(retval
            string retunvalue = (string)cmd.Parameters["@result"].Value;
            Database_Conection.Close();
            return retunvalue;
        }
        public static string SelectActiveDirectory(string UserName)
        {
            Database_Conection.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "ActiveDirectory";
            //cmd.Parameters.Add("AccountNumber",AccountNumber);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = Database_Conection;
            cmd.Parameters.Add("GroupName", UserName);
            SqlParameter retval = new SqlParameter("@result", SqlDbType.NVarChar, 60);
            // param.Direction = ParameterDirection.Input;
            retval.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(retval);
            cmd.ExecuteNonQuery();

            
            string retunvalue = (string)cmd.Parameters["@result"].Value;
            Database_Conection.Close();
            return retunvalue;
        }

        public static string SelectCheckApp(string ApplicationName)
        {
            Database_Conection.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "CheckOnApp ";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = Database_Conection;
            cmd.Parameters.Add("ApplicationName", ApplicationName);
            SqlParameter retval = new SqlParameter("@result", SqlDbType.NVarChar, 60);
           
            retval.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(retval);
            cmd.ExecuteNonQuery();

            
            string retunvalue = (string)cmd.Parameters["@result"].Value;
            Database_Conection.Close();
            return retunvalue;
        }

    }
}