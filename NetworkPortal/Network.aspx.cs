using NetworkPortal.RegistrationReference;
using System;
using System.Linq;
using System.Web.UI.WebControls;

namespace NetworkPortal
{
    public partial class Networks : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadNetworks();
                LoadUserNetworks();
            }
        }

        protected void LoadNetworks()
        {
            RegistrationServiceSoapClient apiClient = new RegistrationServiceSoapClient();
            var networks = apiClient.GetAllNetworks();
            ddlNetworks.DataSource = networks;
            ddlNetworks.DataTextField = "Name";
            ddlNetworks.DataValueField = "Id";
            ddlNetworks.DataBind();
        }

        protected void LoadUserNetworks()
        {
            string email = Session["UserEmail"].ToString();
            RegistrationServiceSoapClient apiClient = new RegistrationServiceSoapClient();
            var userNetworks = apiClient.GetUserNetworks(email);
            gvNetworks.DataSource = userNetworks;
            gvNetworks.DataBind();
        }

        protected void btnJoinNetwork_Click(object sender, EventArgs e)
        {
            try
            {
                string email = Session["UserEmail"].ToString();
                int networkId = int.Parse(ddlNetworks.SelectedValue);

                RegistrationServiceSoapClient apiClient = new RegistrationServiceSoapClient();
                string result = apiClient.JoinNetwork(email, networkId);
                lblMessage.Text = result;

                // Reload user networks
                LoadUserNetworks();
            }
            catch (Exception ex)
            {
                lblMessage.Text = "An error occurred: " + ex.Message;
            }
        }
    }
}
