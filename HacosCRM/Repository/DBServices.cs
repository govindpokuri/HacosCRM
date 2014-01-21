using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace HacosCRM.Repository
{
    public static class DBServices
    {

        private static String strConn = ConfigurationManager.AppSettings.Get("ConnectionString");

        public static DataView GetTblView(string sqlStr)
        {

            SqlConnection MyConnection = default(SqlConnection);
            DataSet DS = null;
            SqlDataAdapter MyCommand = default(SqlDataAdapter);
            DataView Source = new System.Data.DataView();




            MyConnection = new SqlConnection(strConn);

            try
            {

                MyCommand = new SqlDataAdapter(sqlStr, MyConnection);

                DS = new DataSet();
                MyCommand.Fill(DS);
                Source = DS.Tables[0].DefaultView;

            }
            catch (Exception e)
            {

            }
            finally
            {
                MyConnection.Close();
            }

            return Source;

        }


        public static object UpdateSQL(string strQuery, out string strMsg)
        {
            Int32 i = 0;
            SqlConnection oConn = new SqlConnection(strConn);
            strMsg = "";

            try
            {
                oConn.Open();
                SqlCommand cmd = new SqlCommand(strQuery, oConn);
                i = cmd.ExecuteNonQuery();
            }
            catch (Exception er)
            {
                //Console.WriteLine(strQuery);
                i = -1;
                strMsg = er.ToString() + ";" + strQuery;
            }
            finally
            {
                oConn.Close();
            }

            return i.ToString();
        }

        public static object InsertSQL(string strQuery, out string strMsg, out int lastInsertID)
        {

            lastInsertID = 0;
            Int32 i = 0;
            SqlConnection oConn = new SqlConnection(strConn);
            strMsg = "";

            try
            {
                oConn.Open();
                SqlCommand cmd = new SqlCommand(strQuery, oConn);
                lastInsertID = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception er)
            {
                //Console.WriteLine(strQuery);
                i = -1;
                lastInsertID = -1;
                strMsg = er.ToString() + ";" + strQuery;
            }
            finally
            {
                oConn.Close();
            }


            return i.ToString();

        }
    }
}
