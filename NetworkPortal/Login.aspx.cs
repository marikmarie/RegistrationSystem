using NetworkPortal.RegistrationReference;
using System;

namespace NetworkPortal
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                
                string email = txtEmail.Text.Trim();
                string password = txtPassword.Text;

                
                RegistrationServiceSoapClient apiClient = new RegistrationServiceSoapClient();

               
                string result = apiClient.Login(email, password);

                lblMessage.Text = result;

                if (result == "Login successful.")
                {
                        Response.Redirect("Interests.aspx");
                   
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "An error occurred: " + ex.Message;
                // Optionally, log the exception for troubleshooting
            }

        }
    }
}
