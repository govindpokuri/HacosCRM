using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HacosCRM.Repository;
using System.Data;

namespace HacosCRM.Core
{
    public class BaseDO
    {

        public IList<SelectListItem> StatusListItem
        {
            get
            {
                return new List<SelectListItem> { new SelectListItem { Text = "Active", Value = "L" }, new SelectListItem { Text = "Inactive", Value = "D" } };
            }
        }

        public IList<SelectListItem> RoleListItem
        {
            get
            {
                return new List<SelectListItem> { new SelectListItem { Text = "Sales Rep", Value = "0" }, new SelectListItem { Text = "Administrator", Value = "1" } };
            }
        }


        public IList<SelectListItem> LeadSourceListItem
        {

            get
            {

                string[] str = new string[] { "-- Select -- ", "Advertisement", "Cold Call", "Employee Referral", "External Referral", "Online", "Partner", "Public Releations", "Sales Mail Alias", "Seminar Partner", "Seminar-Internal", "Trade Show", "Web", "Chat" };

                List<SelectListItem> list = new List<SelectListItem>();
                for (int i = 0; i < str.Length; i++)
                {
                    list.Add(new SelectListItem { Text = str[i], Value = i.ToString() });
                }

                return list;
            }

        }


        public IList<SelectListItem> LeadStatusListItem
        {

            get
            {

                string[] str = new string[] { "-- Select -- ", "Attempted to Contact", "Contact in Future", "Contracted", "Junk Lead", "Lost Lead", "Not Contacted", "Pre Qualified", "Others" };

                List<SelectListItem> list = new List<SelectListItem>();
                for (int i = 0; i < str.Length; i++)
                {
                    list.Add(new SelectListItem { Text = str[i], Value = i.ToString() });
                }

                return list;
            }

        }



        public string getLeadStatus(int id)
        {
            string[] str = new string[] { "-- Select -- ", "Attempted to Contact", "Contact in Future", "Contracted", "Junk Lead", "Lost Lead", "Not Contacted", "Pre Qualified", "Others" };
            string strOut = "";
            List<SelectListItem> list = new List<SelectListItem>();
            for (int i = 0; i < str.Length; i++)
            {
                if (i == id)
                {
                    strOut = str[i];
                    break;
                }
            }

            return strOut;
        }

        public string getLeadSource(int id)
        {
            string[] str = new string[] { "-- Select -- ", "Advertisement", "Cold Call", "Employee Referral", "External Referral", "Online", "Partner", "Public Releations", "Sales Mail Alias", "Seminar Partner", "Seminar-Internal", "Trade Show", "Web", "Chat" };
            string strOut = "";
            List<SelectListItem> list = new List<SelectListItem>();
            for (int i = 0; i < str.Length; i++)
            {
                if (i == id)
                {
                    strOut = str[i];
                    break;
                }
            }

            return strOut;
        }


        public IList<SelectListItem> getUsers()
        {

            string strQuery = "";
            if (Membership.isAdmin())
            {
                strQuery = "select uid, first_name + ' ' + last_name name from users where role=0 order by first_name asc";
            }
            else
            {
                strQuery = "select uid, first_name + ' ' + last_name name from users where role=0 and uid='" + HacosCRM.Core.Membership.currentUser().uid.ToString() + "'";
            }

            DataView oView = DBServices.GetTblView(strQuery);


            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem { Text = " Select Sales Rep ", Value = "0" });
            for (int i = 0; i < oView.Count; i++)
            {
                list.Add(new SelectListItem { Text = oView[i].Row["name"].ToString(), Value = oView[i].Row["uid"].ToString() });
            }

            return list;
        }



    }
}
