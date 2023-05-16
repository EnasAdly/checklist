using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using MimeKit;
using Image = iTextSharp.text.Image;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.Configuration;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;

namespace WebApplication6
{
    public partial class DailyCheck : System.Web.UI.Page
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
                //this.BindGrid2();
                string[] name = Session["UserName"].ToString().Split(' ');
                useri.Text = string.Join(" ", name[0], name[name.Length - 1]).Trim();

            }

        }
        [Obsolete]
        public static string Teamfilter(string UserName)
        {
            SqlConnection Database_Conection = new SqlConnection(WebConfigurationManager.ConnectionStrings["CheckLists"].ConnectionString);

            Database_Conection.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "ActivDirectFilter";
            //cmd.Parameters.Add("AccountNumber",AccountNumber);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = Database_Conection;
            cmd.Parameters.Add("GroupName", UserName);
            SqlParameter retval = new SqlParameter("@result", SqlDbType.NVarChar, 60);
            retval.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(retval);
            cmd.ExecuteNonQuery();
            string retunvalue = (string)cmd.Parameters["@result"].Value;
            Database_Conection.Close();
            return retunvalue;
        }
        protected void Generatebtn(object sender, EventArgs e)
        {
            disablebtns();
            SendEMail();
            saveUpdate();
        }
        protected void saveUpdate()
        {
            string selectedValues = chkHobbies.SelectedValue;
            using (SqlConnection con = new SqlConnection())
            {
                SqlConnection Database_Conection = new SqlConnection(WebConfigurationManager.ConnectionStrings["CheckLists"].ConnectionString);

                using (SqlCommand cmd = new SqlCommand())
                {
                    Database_Conection.Open();
                    cmd.CommandText = "InsertUpdate";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = Database_Conection;
                    cmd.Parameters.AddWithValue("userId", int.Parse(Session["UserID"].ToString()));
                    cmd.Parameters.AddWithValue("appId", selectedValues);
                    cmd.Connection = Database_Conection;
                    cmd.ExecuteNonQuery();
                    Database_Conection.Close();

                }
            }
        }
        protected async void SendEMail()
        {
            string FromEmail = "";
            string ToEmail = "";
            //string FromEmail = ConfigurationManager.AppSettings["FromEmail"];
            //string ToEmail;
            string Port = ConfigurationManager.AppSettings["Port"];
            string SMTP = ConfigurationManager.AppSettings["SMTP"];
            string UserNameSMTP = "";
            string PasswordSMTP = "";
            //string UserNameSMTP = ConfigurationManager.AppSettings["UserNameSMTP"];
            //string PasswordSMTP = ConfigurationManager.AppSettings["PasswordSMTP"];
            MailMessage message = new MailMessage();
            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
            //ToEmail = (Session["group"].ToString().Equals("IT Core Team")) ? ConfigurationManager.AppSettings["ToCoreEmail"] : ConfigurationManager.AppSettings["ToNonCoreEmail"];

            message.From = new MailAddress(FromEmail);
            message.To.Add(ToEmail);
            //message.CC.Add(ConfigurationManager.AppSettings["1ccEmail"]);
            //message.CC.Add(ConfigurationManager.AppSettings["2ccEmail"]);

            message.IsBodyHtml = true;
            var builder = new BodyBuilder();
            message.Subject = "Daily Report for " + chkHobbies.SelectedValue;
            var pdfContent = await GetPdfContentAsync(Session["group"].ToString());
            message.Attachments.Add(new Attachment(new MemoryStream(pdfContent), "DailyCheckReport" + DateTime.Today.ToString("dd/MM/yyyy") + ".pdf", "application/pdf"));
            message.Body = $"Kindly find the Daily Report for <span style=\"font-size: medium;\"><strong>" + chkHobbies.SelectedValue + "</strong> </span> Application on date:<span style=\"font-size: medium;\" <strong>" + DateTime.Today.ToString("dd/MM/yyyy") + "</strong></span> in the attached file:\n\n";
            smtp.Port = Convert.ToInt32(Port);
            smtp.Host = SMTP;
            smtp.EnableSsl = false;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(UserNameSMTP, PasswordSMTP);
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Send(message);
        }
        private async Task<byte[]> GetPdfContentAsync(string group)
        {
            using (var stream = new MemoryStream())
            {
                using (var document = new iTextSharp.text.Document())
                {
                    PdfWriter.GetInstance(document, stream);
                    document.Open();
                    // Add title
                    var title = new Paragraph("DailyCheckReport");
                    title.Font.Size = 18f;
                    title.Alignment = Element.ALIGN_CENTER;
                    document.Add(title);

                    // Add logo
                    var image = iTextSharp.text.Image.GetInstance("D:/asp.net course/WebApplication6/WebApplication6/Images/logo2.jpg");
                    image.SetAbsolutePosition(36f, 790f);       // Set the position of the image in the PDF document 
                    image.ScaleToFit(100f, 100f);   // Resize the image to fit within a specified width and height
                    document.Add(image);

                    // Add paragraph
                    var paragraph = new Paragraph();
                    Font normalFont = FontFactory.GetFont(FontFactory.HELVETICA, 12f);
                    // Create a font for the bold text 
                    Font boldFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14f);
                    // Add the non-bold text to the paragraph
                    paragraph.Add(new Chunk("This is a Daily Check Report for ", normalFont));
                    // Add the bold text (chkHobbies.SelectedValue) to the paragraph 
                    Chunk boldChunk = new Chunk(chkHobbies.SelectedValue, boldFont);
                    paragraph.Add(boldChunk);
                    // Add the non-bold text to the paragraph 
                    paragraph.Add(new Chunk(" Application on date: ", normalFont));
                    // Add the bold text (DateTime.Now.ToString("dd/MM/yyyy")) to the paragraph
                    boldChunk = new Chunk(DateTime.Today.ToString("dd/MM/yyyy"), boldFont);
                    paragraph.Add(boldChunk);
                    paragraph.Alignment = Element.ALIGN_JUSTIFIED;
                    paragraph.SpacingBefore = 10f;
                    paragraph.SpacingAfter = 10f;
                    document.Add(paragraph);

                    // Add table

                    var table = new PdfPTable(6);
                    table.WidthPercentage = 100f;
                    table.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.SpacingBefore = 10f;
                    table.SpacingAfter = 10f;

                    var cell1 = new PdfPCell(new Phrase("Application Name"));
                    cell1.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell1.BackgroundColor = BaseColor.LIGHT_GRAY;

                    var cell2 = new PdfPCell(new Phrase("Server Port"));
                    cell2.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell2.BackgroundColor = BaseColor.LIGHT_GRAY;

                    var cell3 = new PdfPCell(new Phrase("CheckPoint"));
                    cell3.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell3.BackgroundColor = BaseColor.LIGHT_GRAY;


                    var cell5 = new PdfPCell(new Phrase("Failed/Success"));
                    cell5.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell5.BackgroundColor = BaseColor.LIGHT_GRAY;

                    var cell6 = new PdfPCell(new Phrase("Application Status"));
                    cell6.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell6.BackgroundColor = BaseColor.LIGHT_GRAY;

                    var cell7 = new PdfPCell(new Phrase("Comment"));
                    cell7.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell7.BackgroundColor = BaseColor.LIGHT_GRAY;

                    table.AddCell(cell1);
                    table.AddCell(cell2);
                    table.AddCell(cell3);
                    table.AddCell(cell5);
                    table.AddCell(cell6);
                    table.AddCell(cell7);

                    using (SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["CheckLists"].ConnectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand())
                        {
                            string selectedValues = chkHobbies.SelectedValue;
                            con.Open();
                            cmd.CommandText = "GetList";
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@selectedValues", selectedValues);
                            cmd.Connection = con;
                            using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                            {
                                using (DataTable dt = new DataTable())
                                {
                                    sda.Fill(dt);
                                    foreach (DataRow row in dt.Rows)
                                    {
                                        table.AddCell(row["ApplicationName"].ToString());
                                        table.AddCell(row["Serverport"].ToString());
                                        table.AddCell(row["statusname"].ToString());
                                        table.AddCell(row["StatusDesc"].ToString());
                                        table.AddCell(row["success"].ToString());
                                        table.AddCell(row["Comment"].ToString());
                                    }
                                }
                            }

                            con.Close();
                        }


                        //    using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["CheckLists"].ConnectionString))
                        //{
                        //    SqlCommand cmd = new SqlCommand();
                        //    cmd.Connection = conn;
                        //    string selectedValues = chkHobbies.SelectedValue;
                        //    cmd.CommandText = $"SELECT * FROM Description WHERE ApplicationName = '{selectedValues}'";

                        //    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        //    {
                        //        using (DataTable dt = new DataTable())
                        //        {
                        //            sda.Fill(dt);
                        //            foreach (DataRow row in dt.Rows)
                        //            {
                        //                table.AddCell(row["ApplicationName"].ToString());
                        //                table.AddCell(row["Serverport"].ToString());
                        //                table.AddCell(row["statusname"].ToString());
                        //                table.AddCell(row["StatusDesc"].ToString());
                        //                table.AddCell(row["success"].ToString());
                        //                table.AddCell(row["Comment"].ToString());
                        //            }
                        //        }

                        //    }
                        //    conn.Close();
                        document.Add(table);
                    }
                    document.Close(); // Get the bytes of the PDF
                    byte[] pdfBytes = stream.ToArray();
                    return await Task.FromResult(pdfBytes);
                }
            }
        }
        private void PopulateHobbies()
        {
            string name = Session["group"].ToString();

            //if (Teamfilter(name).Equals("2") && Session["UserName"].ToString() != "Inas Adly Abdelaziz Mahmoud")
            //{
            //    Response.Write("<script>alert('invalid')</script>");
            //}
            //else
            //{

            SqlConnection Database_Conection = new SqlConnection(WebConfigurationManager.ConnectionStrings["CheckLists"].ConnectionString);
            Database_Conection.Open();
            SqlCommand cmd = new SqlCommand();
            //    cmd.CommandText = "SelectApps";
            //    cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = Database_Conection;
            //SqlDataReader sdr = cmd.ExecuteReader();
            //        while (sdr.Read())
            //        {
            //            var item = new System.Web.UI.WebControls.ListItem();

            //            item.Text = sdr["ApplicationName"].ToString();
            //            item.Value = sdr["ApplicationId"].ToString();
            //            chkHobbies.Items.Add(item);
            //        }
            cmd.CommandText = "filterteam";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("GroupName", Session["group"].ToString());
            //cmd.Connection = Database_Conection;
            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                var item = new System.Web.UI.WebControls.ListItem();
                item.Text = sdr["ApplicationName"].ToString();
                chkHobbies.Items.Add(item);
            }
            Database_Conection.Close();
            chkHobbies.SelectedIndex = 0;
        }
        protected void CheckBox_Checked_Unchecked(object sender, EventArgs e)
        {
            this.BindGrid1();
        }
        protected void checkexist()
        {
            string selectedValues = chkHobbies.SelectedValue;
            using (SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["CheckLists"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("checkexist",con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@selectedValues", selectedValues);
                    cmd.Parameters.AddWithValue("@userId", int.Parse(Session["UserID"].ToString()));
                    cmd.Parameters.AddWithValue("@Date", DateTime.Now.ToString("yyyy-MM-dd"));
                    SqlParameter returnParameter = new SqlParameter("@ReturnValue", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(returnParameter);
                    cmd.ExecuteNonQuery();
                    int val = (int)cmd.Parameters["@ReturnValue"].Value;
                    if (val == 0)
                        disablebtns();
                    else
                    {
                        Completed.Visible = false;
                        btnExport.Enabled = true;
                    }
                    con.Close();
                }
            }
        }
        protected void BindGrid1()
        {
            string selectedValues = chkHobbies.SelectedValue;
            using (SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["CheckLists"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    con.Open();
                    cmd.CommandText = "GetList";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@selectedValues", selectedValues);
                    cmd.Connection = con;
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            GridView1.DataSource = dt;
                            GridView1.DataBind();
                        }
                    }
                    con.Close();
                    checkexist();
                }
            }
            
            //using (SqlConnection connection = new SqlConnection(connectionString))
            //{
            //    SqlCommand command = new SqlCommand();
            //    command.Connection = connection;
            //    string selectedValues = chkHobbies.SelectedValue;
            //    command.CommandText = $"SELECT * FROM Description WHERE ApplicationName = '{selectedValues}'";
            //    SqlDataAdapter adapter = new SqlDataAdapter(command);
            //    DataTable table = new DataTable();
            //    adapter.Fill(table);
            //    GridView1.DataSource = table;
            //    GridView1.DataBind();
            //}
        }
        protected void disablebtns()
        {
            Completed.Visible = true;
            btnExport.Enabled = false;
            foreach (GridViewRow row in GridView1.Rows)
            {
                Button editButton = (Button)row.FindControl("btn_Edit");
                RadioButton radioButtonf = (RadioButton)row.FindControl("Radiofale");
                RadioButton radioButtons = (RadioButton)row.FindControl("Radiosucc");
                editButton.Enabled = false;
                radioButtonf.Enabled = false;
                radioButtons.Enabled = false;
            }
        }
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            this.BindGrid1();
        }
        protected void GridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;

            this.BindGrid1();
        }
        protected void GridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            this.BindGrid1();
        }
        protected void GridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

            string type = string.Empty;
            var date = DateTime.Now;
            GridViewRow row = GridView1.Rows[e.RowIndex];
            RadioButton rb1= (RadioButton)row.FindControl("Radiofale");
            RadioButton rb2 = (RadioButton)row.FindControl("Radiosucc");
            string ApplicationID = (row.FindControl("AppID") as Label).Text;
            type = (rb1.Checked) ? rb1.Text : rb2.Text;

            string Comment = (row.FindControl("CommentBox") as TextBox).Text; ;


            using (SqlConnection con = new SqlConnection())
            {
                SqlConnection Database_Conection = new SqlConnection(WebConfigurationManager.ConnectionStrings["CheckLists"].ConnectionString);

                using (SqlCommand cmd = new SqlCommand())
                {
                    Database_Conection.Open();
                    cmd.CommandText = "AddComment";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = Database_Conection;
                    cmd.Parameters.AddWithValue("ApplicationID", ApplicationID);
                    cmd.Parameters.AddWithValue("Comment", Comment);
                    cmd.Parameters.AddWithValue("success", type);
                    cmd.Connection = Database_Conection;
                    cmd.ExecuteNonQuery();
                    Database_Conection.Close();

                }
            }
            GridView1.EditIndex = -1;

            this.BindGrid1();

        }
        protected void logout_Click(object sender, EventArgs e)
        {
            Session.Remove("group");
            Session.Remove("UserName");
            Response.Redirect("LoginPage.aspx");
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            //required to avoid the runtime error "  
            //Control 'GridView1' of type 'GridView' must be placed inside a form tag with runat=server."  
        }
        protected void submit_Click(object sender, EventArgs e)
        {
            string type = string.Empty;
            foreach (GridViewRow row in GridView1.Rows)
            {
                string ApplicationID = (row.FindControl("AppID") as Label).Text;
                RadioButton rb1 = (RadioButton)row.FindControl("Radiofale");
                RadioButton rb2 = (RadioButton)row.FindControl("Radiosucc");

                if (rb1.Checked)
                {
                    type = rb1.Text;
                }
                else
                {
                    type = rb2.Text;
                }
                SqlConnection Database_Conection = new SqlConnection(WebConfigurationManager.ConnectionStrings["CheckLists"].ConnectionString);
                SqlCommand cmd = new SqlCommand();
                Database_Conection.Open();
                cmd.CommandText = "RadioBt";
                cmd.Parameters.AddWithValue("ApplicationId", ApplicationID);
                cmd.Parameters.AddWithValue("success", type);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = Database_Conection;
                cmd.ExecuteNonQuery();
                Database_Conection.Close();
            }
            this.BindGrid1();


        }

    }

}
