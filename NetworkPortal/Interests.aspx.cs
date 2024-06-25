using NetworkPortal.RegistrationReference;
using System;
using System.Web.UI.WebControls;
using System.Linq;

namespace NetworkPortal
{
    public partial class Interests : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Interests 
                chkInterests.Items.Add("Religion");
                chkInterests.Items.Add("Technology");
                chkInterests.Items.Add("Sports");
                chkInterests.Items.Add("Music");
                chkInterests.Items.Add("Art");
                chkInterests.Items.Add("Travel");
                
            }
        }

        protected void btnSaveInterests_Click(object sender, EventArgs e)
        {
            try
            {
                // Get selected interests
                string interests = string.Join(",", chkInterests.Items.Cast<ListItem>().Where(i => i.Selected).Select(i => i.Value));

                string email = Session["UserEmail"].ToString();
                RegistrationServiceSoapClient apiClient = new RegistrationServiceSoapClient();
                string result = apiClient.SaveInterests(email, interests);
                lblMessage.Text = result;
            }
            catch (Exception ex)
            {
                lblMessage.Text = "An error occurred: " + ex.Message;
            }
        }
    }
}
