using NetworkPortal.RegistrationReference;
using System;

namespace NetworkPortal
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                // Get user input from form controls
                string firstName = txtFirstName.Text.Trim();
                string lastName = txtLastName.Text.Trim();
                string email = txtEmail.Text.Trim();
                DateTime dob = DateTime.Parse(txtDOB.Text.Trim()); 
                string password = txtPassword.Text;
                string confirmPassword = txtConfirmPassword.Text;

                // Create an instance of the API service client
                RegistrationServiceSoapClient apiClient = new RegistrationServiceSoapClient();

               
                string result = apiClient.RegisterUser(firstName, lastName, email, dob, password, confirmPassword);

              
                lblMessage.Text = result;
            }
            catch (Exception ex)
            {
                lblMessage.Text = "An error occurred: " + ex.Message;
                
            }
        }
    }
}
