using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AganithMembership;
using AganithGlobalFrameWork;
using System.Data;
using HacosCRM.Repository;
using System.Text;
using System.Web.Security;

namespace HacosCRM.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/

        public ActionResult Index(UserModel model)
        {
            return View(model);
        }


        [HttpPost]
        public ActionResult Login(UserModel model)
        {


            string strQuery = "select * from users where username='" + SQLHelper.getSqlVal(model.username) + "' and password = '" + SQLHelper.getSqlVal(model.password) + "'";

            DataView oView = SQLHelper.getTblView(strQuery);



            if (oView.Count > 0)
            {

                model = Extensions.ToObject<UserModel>(oView);
                HacosCRM.Core.Membership.setUserToken(model);


                string strQuery1 = "update users set last_login_date= getdate() where uid='" + HacosCRM.Core.Membership.currentUser().uid.ToString() + "'";


                string strOut = "";
                DBServices.UpdateSQL(strQuery1, out strOut);




                return Content("<script>window.location.href='/dashboard';</script>", "text/html");
            }
            else
            {
                return Content("<div class='alert alert-danger'><strong>Please enter valid credentials</strong>", "text/html");
            }








        }

        [HttpPost]
        public ActionResult Logout()
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectPermanent("/account");
        }


        [HttpPost]
        public ActionResult ForgotPassword(UserModel model)
        {


            string strError = "";

            if (model.email == null)
            {
                strError = "<li>  Please enter Email  </li>";
            }
            else if (model.email.Trim().Length < 1)
            {
                strError = "<li> Please enter Email  </li>";
            }


            if (strError.Equals(""))
            {
                // check username, if username is valid send email with existing password.
                // if user not provide valid username, throw error

                DataView oView = SQLHelper.getTblView("select * from users where email ='" + SQLHelper.getSqlVal(model.email) + "'");
                if (oView.Count > 0)
                {
                    // valid user  send email with new password



                    StringBuilder sb = new StringBuilder();
                    sb.Append("<html><body><table style='font-family:arial;font-size:11px;' cellpadding='0' cellspacing='6' width='600'><tr><td>");
                    sb.Append("<strong>Hi, " + oView[0].Row["username"].ToString() + "</strong><br><br>");
                    sb.Append("Your User name and Password are provided below.<br><br>");
                    sb.Append("User name: " + oView[0].Row["username"].ToString());
                    sb.Append("<br>Password: " + oView[0].Row["password"].ToString());
                    sb.Append("<br><br>Thanks! <br>- The Joyson Team</td></tr></table></body></html>");



                    string strSenderName = System.Configuration.ConfigurationManager.AppSettings.Get("Email_SenderName");

                    // CommonLib.SendEmail(strSenderName, "admin@joyson.com.sg", model.Email, "Reset Password", sb.ToString());

                }
                else
                {
                    strError = "<li>You have entered invalid user email address.</li>";
                }



            }




            if (strError.Equals(""))
            {
                return Content("<div class='alert alert-success'>Reset password link has been sent to your email address.</div>", "text/html");
            }
            else
            {

                return Content("<div class='alert alert-danger'><strong>Error!</strong> " + strError + "</div>", "text/html");
            }
        }
    }
}
