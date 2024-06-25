using NetworkPortal.RegistrationReference;
using System;
using System.Linq;
using System.Web.UI.WebControls;

namespace NetworkPortal
{
    public partial class Friends : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadUsers();
                LoadFriends();
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

        protected void LoadFriends()
        {
            string email = Session["UserEmail"].ToString();
            RegistrationServiceSoapClient apiClient = new RegistrationServiceSoapClient();
            var friends = apiClient.GetFriends(email);
            gvFriends.DataSource = friends;
            gvFriends.DataBind();
        }

        protected void btnSendRequest_Click(object sender, EventArgs e)
        {
            try
            {
                string fromEmail = Session["UserEmail"].ToString();
                string toEmail = ddlUsers.SelectedValue;

                RegistrationServiceSoapClient apiClient = new RegistrationServiceSoapClient();
                string result = apiClient.SendFriendRequest(fromEmail, toEmail);
                lblMessage.Text = result;

                // Reload friends
                LoadFriends();
            }
            catch (Exception ex)
            {
                lblMessage.Text = "An error occurred: " + ex.Message;
            }
        }
    }
}
