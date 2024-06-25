using NetworkPortal.RegistrationReference;
using System;
using System.Linq;
using System.Web.UI.WebControls;

namespace NetworkPortal
{
    public partial class Messages : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadUsers();
                LoadMessages();
            }
        }

        protected void LoadUsers()
        {
            RegistrationServiceSoapClient apiClient = new RegistrationServiceSoapClient();
            var users = apiClient.GetAllUsers();
            ddlUsers.DataSource = users;
            ddlUsers.DataTextField = "Email";
            ddlUsers.DataValueField = "Email";
            ddlUsers.DataBind();
        }

        protected void LoadMessages()
        {
            string email = Session["UserEmail"].ToString();
            RegistrationServiceSoapClient apiClient = new RegistrationServiceSoapClient();
            var messages = apiClient.GetMessages(email);
            gvMessages.DataSource = messages;
            gvMessages.DataBind();
        }

        protected void btnSendMessage_Click(object sender, EventArgs e)
        {
            try
            {
                string fromEmail = Session["UserEmail"].ToString();
                string toEmail = ddlUsers.SelectedValue;
                string message = txtMessage.Text.Trim();

                RegistrationServiceSoapClient apiClient = new RegistrationServiceSoapClient();
                string result = apiClient.SendMessage(fromEmail, toEmail, message);
                lblMessage.Text = result;

                // Reload messages
                LoadMessages();
            }
            catch (Exception ex)
            {
                lblMessage.Text = "An error occurred: " + ex.Message;
            }
        }
    }
}
