using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication6.AppData;

namespace WebApplication6
{
    public partial class LoginPage : System.Web.UI.Page
    {
        protected void Page_Init(object Sender, EventArgs e)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
            Response.Cache.SetNoStore();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Cookies.Clear();
            Session.Remove("group");
        }
        //  SelectActiveDirectory
        string DomainConn = System.Configuration.ConfigurationManager.AppSettings["DomainConn"];
        public List<GroupPrincipal> GetGroups(string userName)
        {
            List<GroupPrincipal> result = new List<GroupPrincipal>();

            // establish domain context
            PrincipalContext yourDomain = new PrincipalContext(ContextType.Domain);

            // find your user
            UserPrincipal user = UserPrincipal.FindByIdentity(yourDomain, userName);

            // if found - grab its groups
            if (user != null)
            {
                PrincipalSearchResult<Principal> groups = user.GetAuthorizationGroups();

                // iterate over all groups
                foreach (Principal p in groups)
                {
                    // make sure to add only group principals
                    if (p is GroupPrincipal)
                    {
                        result.Add((GroupPrincipal)p);
                    }
                }
            }

            return result;
        }
        public bool AuthenticateUser(string domain, string username, string password)
        {
            string LDAPPATH = "";
            string domainAndUsername = domain + @"\" + username;
            DirectoryEntry entry = new DirectoryEntry(LDAPPATH, domainAndUsername, password);
            try
            {
                Object obj = entry.NativeObject;
                DirectorySearcher search = new DirectorySearcher(entry);
                search.Filter = "(SAMAccountName=" + username + ")";
                search.PropertiesToLoad.Add("cn");
                SearchResult result = search.FindOne();
                if (null == result)
                {
                    return false;
                }
                DirectoryEntry directoryEntry = result.GetDirectoryEntry();
                string displayName = directoryEntry.Properties["displayName"][0].ToString();
                string Email = directoryEntry.Properties["mail"][0].ToString();
                string userID = directoryEntry.Properties["employeeID"][0].ToString();
                //string empid = directoryEntry.Properties[""][0].ToString();
                LDAPPATH = result.Path;

                var groups = GetGroups(username);
                if (displayName == "Inas Adly Abdelaziz Mahmoud")
                {
                    Session["group"] = groups;
                    Session["UserName"] = displayName;
                    Response.Redirect("DailyCheck.aspx");
                }
                else
                {
                    foreach (var group in groups)
                    {
                        if (group.Name.Equals("IT Non Core APP") || group.Name.Equals("IT Core Team"))
                        {
                            Session["UserID"] = userID;
                            Session["group"] = group.Name;
                            Session["UserName"] = displayName;
                            Response.Redirect("DailyCheck.aspx");
                        }
                        else
                        {
                            PassWord.Text = "";
                            LB_errorLogin.Visible = true;
                            LB_errorLogin.Text = "Access Denied";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                PassWord.Text = "";
                LB_errorLogin.Visible = true;
                LB_errorLogin.Text = ex.Message;

            }

            return true;
        }

        protected void loginBt_Click(object sender, EventArgs e)
        {
            string S_user = username.Text;
            string S_Pass = PassWord.Text;
            bool auth = AuthenticateUser(DomainConn, S_user, S_Pass);
            //if (DataBase.Select(username.Text).Equals("2"))
            //{

            //    Response.Write("<script>alert('Account not found')</script>");
            //    //Response.Redirect("exist......................////////////////////");


            //}
            //else
            //{
            //    DataBase.insert(
            //                       username.Text,
            //                       PassWord.Text,
            //                       date.ToString()
            //                       );
            //Response.Write("<script>alert('Done')</script>");
            //Response.Redirect("App2.aspx?value=" + username.Text);

        //}
        }
    }
}