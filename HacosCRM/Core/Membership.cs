using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AganithMembership;
using HacosCRM.Repository;
using System.Data;

namespace HacosCRM.Core
{
    public static class Membership
    {
        public static string AdminRole = "1";
        public static string UserRole = "0";

        public static string AdminRoles = "Admin,View,Insert,Edit,Delete";
        public static string UserRoles = "View";
        public static string SalesRepRole = "Insert";

        public static bool isAuthorized()
        {
            if (HttpContext.Current.Session["userid"] != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public static string GetUserRights()
        {

            if (isAuthorized())
            {
                return isAdmin() ? AdminRoles : UserRoles;
            }
            else
            {
                return "";
            }
        }

        public static UserModel currentUser()
        {
            if (isAuthorized())
            {
                return (UserModel)HttpContext.Current.Session["currentUser"];
            }
            else
            {
                return new UserModel();
            }

        }

        public static void setUserToken(UserModel model)
        {
            HttpContext.Current.Session["userid"] = model.uid;
            HttpContext.Current.Session["role"] = model.role.ToString();
            HttpContext.Current.Session["email"] = model.email;
            HttpContext.Current.Session["first_name"] = model.first_name;
            HttpContext.Current.Session["currentUser"] = model;

        }

        public static string getCRMUsers(int id)
        {

            string strQuery = "";
            string strOut = "";
            strQuery = "select  first_name + ' ' + last_name name from users where uid=" + id;


            DataView oView = DBServices.GetTblView(strQuery);


            strOut = oView[0].Row["name"].ToString();
            return strOut;
        }

        public static bool getCRMid()
        {

            string strQuery = "";
            strQuery = "select  * from leads where uid=" + HacosCRM.Core.Membership.currentUser().uid;


            DataView oView = DBServices.GetTblView(strQuery);


            if (oView.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string getRole()
        {
            return HttpContext.Current.Session["role"] != null ? HttpContext.Current.Session["role"].ToString() : "";
        }


        public static bool isAdmin()
        {
            return getRole().Equals(AdminRole) ? true : false;
        }


        public static string getEmail()
        {
            return HttpContext.Current.Session["email"] != null ? HttpContext.Current.Session["email"].ToString() : "";

        }
    }
}
