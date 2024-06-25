using System;
using System.Collections.Generic;
using System.Web.Services;
using RegisterLibrary;
using System.Linq;

namespace NetworkAPI
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]

    public class RegistrationService : WebService
    {
        private static string connectionString = "Server=MARIAMTUKASINGU;Database=RegistrationDB;Integrated Security=True;";
        private QueueProcessor queueProcessor = new QueueProcessor();
        private DatabaseHelper registrationDB = new DatabaseHelper(connectionString);

        [WebMethod]
        public string RegisterUser(string firstName, string lastName, string email, DateTime dob, string password, string confirmPassword)
        {
            if (password != confirmPassword)
            {
                return "Passwords do not match.";
            }

            if (registrationDB.IsEmailRegistered(email) || queueProcessor.IsEmailInQueue(email))
            {
                return "User already registered or registration is processing.";
            }

            RegistrationDetails registrationDetails = new RegistrationDetails
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                DOB = dob,
                Password = password
            };

            queueProcessor.SendMessage(registrationDetails);

            return "Your registration is being processed.";
        }

        [WebMethod]
        public string GetStatus(string email)
        {
            if (queueProcessor.IsEmailInQueue(email))
            {
                return "Registration is pending.";
            }

            if (registrationDB.IsEmailRegistered(email))
            {
                return "Registration successful.";
            }

            return "No registration Found. Try registering again maybe!!!";
        }

        [WebMethod]
        public string Login(string email, string password)
        {
            
            if (queueProcessor.IsEmailInQueue(email))
            {
                return "Registration is pending, you cannot log in.";
            }

            // Check if the email is registered in the database
            if (registrationDB.IsEmailRegistered(email))
            {
                // Validate the user's password
                if (registrationDB.ValidateUser(email, password))
                {
                    return "Login successful.";
                }
                else
                {
                    return "Invalid email or password.";
                }
            }

           
            return "User not found.";
        }
        [WebMethod]
        public List<string> GetAllUsers()
        {
            return registrationDB.GetAllUsers();
        }

        [WebMethod]
        public string SendFriendRequest(string fromEmail, string toEmail)
        {
            return registrationDB.SendFriendRequest(fromEmail, toEmail);
        }

        [WebMethod]
        public List<string> GetFriends(string email)
        {
            return registrationDB.GetFriends(email);
        }

        [WebMethod]
        public string SaveInterests(string email, string interests)
        {
            return registrationDB.SaveInterests(email, interests);
        }

        [WebMethod]
        public List<string> GetMessages(string email)
        {
            return registrationDB.GetMessages(email);
        }

        [WebMethod]
        public string SendMessage(string fromEmail, string toEmail, string message)
        {
            return registrationDB.SendMessage(fromEmail, toEmail, message);
        }

        [WebMethod]
        public List<string> GetAllNetworks()
        {
            return registrationDB.GetAllNetworks();
        }

        [WebMethod]
        public List<string> GetUserNetworks(string email)
        {
            return registrationDB.GetUserNetworks(email);
        }

        [WebMethod]
        public string JoinNetwork(string email, int networkId)
        {
            return registrationDB.JoinNetwork(email, networkId);
        }



    }
}
