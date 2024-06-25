using NetworkPortal.RegistrationReference;
using System;

namespace NetworkPortal
{
    public partial class Status : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnCheckStatus_Click(object sender, EventArgs e)
        {
            try
            {                
                string email = txtEmail.Text.Trim();

                
                RegistrationServiceSoapClient apiClient = new RegistrationServiceSoapClient();

                
                string result = apiClient.GetStatus(email);

                //result
                lblStatus.Text = result;
            }
            catch (Exception ex)
            {
                lblStatus.Text = "An error occurred: " + ex.Message;
                
            }
        }
    }
}
