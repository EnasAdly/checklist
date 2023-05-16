using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication6.AppData;

namespace WebApplication6
{
    public partial class AddNewApplication : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.PopulateHobbies();
            PlaceHolder1.Visible = false;
            NewConfigH.Visible = false;
            //AddNewID.Visible = false;
            AddNewAppH.Visible = false;
            this.Team();
            
           
        }


        private void PopulateHobbies()
        {
            using (SqlConnection conn = new SqlConnection())
            {
                SqlConnection Database_Conection = new SqlConnection(WebConfigurationManager.ConnectionStrings["CheckLists"].ConnectionString);

                using (SqlCommand cmd = new SqlCommand())
                {
                    Database_Conection.Open();

                    cmd.CommandText = "SelectApps";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = Database_Conection;

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {

                            // CheckBox checkBox = new CheckBox();
                            var item = new System.Web.UI.WebControls.ListItem();
                            var item2= new System.Web.UI.WebControls.ListItem();
                          
                            item.Text = sdr["ApplicationName"].ToString();
                            item2.Text = sdr["server port"].ToString();
                           
                            DropApplication.Items.Add(item);
                            DropServer.Items.Add(item2);
                        }
                    }
                }

                conn.Close();
            }
        }
        private void Team()
        {
            using (SqlConnection conn = new SqlConnection())
            {
                SqlConnection Database_Conection = new SqlConnection(WebConfigurationManager.ConnectionStrings["CheckLists"].ConnectionString);

                using (SqlCommand cmd = new SqlCommand())
                {
                    Database_Conection.Open();

                    cmd.CommandText = "SelectTeam";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = Database_Conection;

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {

                            // CheckBox checkBox = new CheckBox();
                            var item = new System.Web.UI.WebControls.ListItem();

                            item.Text = sdr["GroupName"].ToString();
                            

                            DropTeam.Items.Add(item);
                        }
                    }
                }

                conn.Close();
            }
        }

        protected void CreatNew_Config(object sender, EventArgs e)
        {
            PlaceHolder1.Visible = true;
            NewConfigH.Visible = true;
            AddNewAppH.Visible = false;
            AddNewID.Visible = false;
            
        }

        protected void AddNewApp_Click(object sender, EventArgs e)
        {
            AddNewID.Visible = true;
            AddNewAppH.Visible = true;
            NewConfigH.Visible = false;
            PlaceHolder1.Visible = false;
            
           
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            if (DataBase.SelectCheckApp(AppName.Text).Equals("2"))
            {

                DataBase.insertNewAppDEsc(
                    AppName.Text,
                    ServerPort.Text,
                    CheckPointID.Text,
                    ValidationID.Text,
                    DropTeam.Text
                    );
                Response.Write("<script>alert('Done')</script>");
            }
            else
            {
                Response.Write("<script>alert('Already exist')</script>");
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            DataBase.insertNewConfig(
                DropApplication.Text,
                DropServer.Text,
                statusBoc.Text,
                DescriptionId.Text
                );
            Response.Write("<script>alert('Done')</script>");
        }

        //protected void Apps_Click(object sender, EventArgs e)
        //{
        //                Response.Redirect("App2.aspx?value=" + DropTeam.Text);

        //    Response.Redirect("App2.aspx?value=" + DropTeam.Text);
        //}
    }
}