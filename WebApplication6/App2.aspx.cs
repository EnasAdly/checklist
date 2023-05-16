using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using Document = iTextSharp.text.Document;
using iTextSharp.tool.xml;
using WebApplication6.AppData;
using AngleSharp.Html.Parser;
using System.Net.Mail;
using iTextSharp.text.html.simpleparser;
using System.Net;
using System.Text;
using Syncfusion.Pdf.Graphics;
using System.Drawing;

namespace WebApplication6
{
    public partial class App21 : System.Web.UI.Page
    {

        protected void Page_Init(object Sender, EventArgs e)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
            Response.Cache.SetNoStore();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.Page.SetFocus(this);

                try
                {
                    H_GID.Value = Session["group"].ToString();
                }
                catch
                {
                    Response.Redirect("LoginPage.aspx");

                }

                if (H_GID.ToString() == "")
                {
                    Response.Redirect("LoginPage.aspx");

                }
                this.PopulateHobbies();
                this.BindGrid1();
                GridView1.Visible = false;
              user.Text = Session["UserName"].ToString();

            }


        }
        //public static string Teamfilter(string UserName)
        //{
        //    SqlConnection Database_Conection = new SqlConnection(WebConfigurationManager.ConnectionStrings["CheckLists"].ConnectionString);

        //    Database_Conection.Open();
        //    SqlCommand cmd = new SqlCommand();
        //    cmd.CommandText = "ActivDirectFilter";
        //    //cmd.Parameters.Add("AccountNumber",AccountNumber);

        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Connection = Database_Conection;
        //    cmd.Parameters.Add("GroupName", UserName);
        //    SqlParameter retval = new SqlParameter("@result", SqlDbType.NVarChar, 60);
        //    retval.Direction = ParameterDirection.Output;
        //    cmd.Parameters.Add(retval);
        //    cmd.ExecuteNonQuery();
        //    string retunvalue = (string)cmd.Parameters["@result"].Value;
        //    Database_Conection.Close();
        //    return retunvalue;
        //}

        //private void PopulateHobbies()
        //{
        //    string name = Session["group"].ToString();

        //    if (Teamfilter(name).Equals("2"))
        //    {
        //        Response.Write("<script>alert('invalid')</script>");
        //    }
        //    else
        //    {
        //        SqlConnection Database_Conection = new SqlConnection(WebConfigurationManager.ConnectionStrings["CheckLists"].ConnectionString);

        //        Database_Conection.Open();
        //        SqlCommand cmd = new SqlCommand();
        //        cmd.CommandText = "filterteam";
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("GroupName", name);
        //        cmd.Connection = Database_Conection;
        //        using (SqlDataReader sdr = cmd.ExecuteReader())
        //        {
        //            while (sdr.Read())
        //            {
        //                var item = new System.Web.UI.WebControls.ListItem();
        //                item.Text = sdr["ApplicationName"].ToString();
        //                chkHobbies.Items.Add(item);
        //            }
        //        }

        //        Database_Conection.Close();

        //    }



        //    using (SqlConnection conn = new SqlConnection())
        //    {
        //        SqlConnection Database_Conection = new SqlConnection(WebConfigurationManager.ConnectionStrings["CheckLists"].ConnectionString);

        //        using (SqlCommand cmd = new SqlCommand())
        //        {
        //            Database_Conection.Open();

        //            cmd.CommandText = "SelectApps";
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Connection = Database_Conection;

        //            using (SqlDataReader sdr = cmd.ExecuteReader())
        //            {
        //                while (sdr.Read())
        //                {

        //                    // CheckBox checkBox = new CheckBox();
        //                    var item = new System.Web.UI.WebControls.ListItem();
        //                    //checkBox.Text = sdr["ApplicationName"].ToString();

        //                    item.Text = sdr["ApplicationName"].ToString();
        //                    //LabelStayInTouch.Text=item.Text.ToString();
        //                    item.Value = sdr["ApplicationId"].ToString();
        //                    //item.Selected = Convert.ToBoolean(sdr["IsSelected"]);
        //                    chkHobbies.Items.Add(item);
        //                    //PlaceHolder1.Controls.Add(checkBox);
        //                }
        //            }
        //        }

        //        conn.Close();
        //    }

        //}


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

                            var item = new System.Web.UI.WebControls.ListItem();

                            item.Text = sdr["ApplicationName"].ToString();
                            item.Value = sdr["ApplicationId"].ToString();
                            chkHobbies.Items.Add(item);
                        }
                    }
                }

                conn.Close();
            }
        }


        protected void CheckBox_Checked_Unchecked(object sender, EventArgs e)
        {

            this.BindGrid1();
           
            foreach (System.Web.UI.WebControls.ListItem item in chkHobbies.Items)
            {
                if (item.Selected)
                {
                    GridView1.Visible = true;
                }
            }
        

        }
        void griviewfalse()


        {
            foreach (GridViewRow row in GridView1.Rows)


                (row.FindControl("AppBox") as TextBox).Visible = false;

        }
        void griviewtrue()
        {
            foreach (GridViewRow row in GridView1.Rows)
            {

                (row.FindControl("AppBox") as TextBox).Visible = true;
            }
        }
        protected void BindGrid1()
        {
            //string id= Request.QueryString["value"].ToString();

            string connectionString = WebConfigurationManager.ConnectionStrings["CheckLists"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                List<string> selectedValues = new List<string>();

                foreach (System.Web.UI.WebControls.ListItem item in chkHobbies.Items)
                {
                    if (item.Selected)
                    {
                        selectedValues.Add(item.Text);
                    }
                }
                if (selectedValues.Count == 0)
                {
                    GridView1.Visible = false;
                    command.CommandText = $"SELECT * FROM Description";

                    //command.CommandText = $"SELECT * FROM Description WHERE GroupName =@GroupName";
                    //command.Parameters.AddWithValue("@GroupName",id);
                }
                else
                {
                    string parameterValue = string.Join(",", selectedValues.Select(x => "'" + x + "'"));
                    command.CommandText = $"SELECT * FROM Description WHERE ApplicationName IN ({parameterValue})";
                }

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable table = new DataTable();
                adapter.Fill(table);
                GridView1.DataSource = table;
                GridView1.DataBind();
               
            }
        }

        private void BindGrid()
        {
            SqlConnection Database_Conection = new SqlConnection(WebConfigurationManager.ConnectionStrings["CheckLists"].ConnectionString);


            string query = "SELECT ApplicationId, ApplicationName,Serverport,statusname,StatusDesc,Failed,success,GetDate FROM Description";

            string condition = string.Empty;
            foreach (System.Web.UI.WebControls.ListItem item in chkHobbies.Items)
            {
                condition += item.Selected ? string.Format("'{0}',", item.Value) : "";
            }
            if (!string.IsNullOrEmpty(condition))
            {
                condition = string.Format(" where ApplicationName in ({0})", condition.Substring(0, condition.Length - 1));
            }
            SqlCommand cmd = new SqlCommand(query + condition);
            using (SqlConnection conn = new SqlConnection())
            {


                Database_Conection.Open();

                //cmd.CommandText = "SelecDesc";
                //cmd.CommandType = CommandType.StoredProcedure;

                //cmd.Connection = Database_Conection;
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    cmd.Connection = Database_Conection;
                    sda.SelectCommand = cmd;
                    using (DataSet ds = new DataSet())
                    {
                        sda.Fill(ds);
                        GridView1.DataSource = ds;
                        GridView1.DataBind();
                    }
                }

                conn.Close();
            }
        }




        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            this.BindGrid1();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            this.BindGrid1();
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            this.BindGrid1();
        }


        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

            Label AppID = GridView1.Rows[e.RowIndex].FindControl("AppID") as Label;           
            TextBox appname= GridView1.Rows[e.RowIndex].FindControl("AppBox") as TextBox;
            TextBox server= GridView1.Rows[e.RowIndex].FindControl("serverBox") as TextBox;
            TextBox status = GridView1.Rows[e.RowIndex].FindControl("statusBox") as TextBox;
            TextBox statusDesc = GridView1.Rows[e.RowIndex].FindControl("DescBox") as TextBox;

            using (SqlConnection con = new SqlConnection())
            {
                SqlConnection Database_Conection = new SqlConnection(WebConfigurationManager.ConnectionStrings["CheckLists"].ConnectionString);

                using (SqlCommand cmd = new SqlCommand())
                {
                    Database_Conection.Open();
                    cmd.CommandText = "UpdateAppName";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = Database_Conection;
                    cmd.Parameters.AddWithValue("ApplicationID", AppID.Text);
                    cmd.Parameters.AddWithValue("ApplicationName", appname.Text);
                    cmd.Parameters.AddWithValue("Serverport", server.Text);
                    cmd.Parameters.AddWithValue("statusname", status.Text);
                    cmd.Parameters.AddWithValue("StatusDesc", statusDesc.Text);
                    cmd.Connection = Database_Conection;
                    cmd.ExecuteNonQuery();
                    Database_Conection.Close();

                }
            }
            GridView1.EditIndex = -1;
            this.BindGrid1();
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddNewApplication.aspx");
        }

        protected void DeleteID_Click(object sender, EventArgs e)
        {

            
            var item = new System.Web.UI.WebControls.ListItem();
            for(int i=0; i<chkHobbies.Items.Count; i++)
            {
                if (chkHobbies.Items[i].Selected == true)
                {
                    item.Text=chkHobbies.Items[i].Text;
                    DataBase.DeleteAppDEsc(item.Text);
                    chkHobbies.Items.RemoveAt(i);
                }
            }
            this.BindGrid1();
        }

        protected void logout_Click(object sender, EventArgs e)
        {
            Session.Remove("group");
            Response.Redirect("LoginPage.aspx");
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Label AppID = GridView1.Rows[e.RowIndex].FindControl("AppID") as Label;
           

            using (SqlConnection con = new SqlConnection())
            {
                SqlConnection Database_Conection = new SqlConnection(WebConfigurationManager.ConnectionStrings["CheckLists"].ConnectionString);

                using (SqlCommand cmd = new SqlCommand())
                {
                    Database_Conection.Open();
                    cmd.CommandText = "onRowDelete ";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = Database_Conection;
                    cmd.Parameters.AddWithValue("ApplicationID", AppID.Text);
                   
                    cmd.Connection = Database_Conection;
                    cmd.ExecuteNonQuery();
                    Database_Conection.Close();

                }
            }
          
            this.BindGrid1();
        }
    }
}