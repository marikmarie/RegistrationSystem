using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace RegisterLibrary
{
    public class DatabaseHelper
    {
        private string connectionString;

        public DatabaseHelper(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public bool IsEmailRegistered(string email)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT COUNT(*) FROM Registrations WHERE Email = @Email";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Email", email);
                        int count = (int)command.ExecuteScalar();
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                throw new Exception("Error checking email registration status", ex);
            }
        }

        public void RegisterUser(RegistrationDetails registrationDetails)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO Registrations (FirstName, LastName, Email, DOB, Password) VALUES (@FirstName, @LastName, @Email, @DOB, @Password)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@FirstName", registrationDetails.FirstName);
                        command.Parameters.AddWithValue("@LastName", registrationDetails.LastName);
                        command.Parameters.AddWithValue("@Email", registrationDetails.Email);
                        command.Parameters.AddWithValue("@DOB", registrationDetails.DOB);
                        command.Parameters.AddWithValue("@Password", HashPassword(registrationDetails.Password));
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                // Log SQL exception
                string errorMessage = $"SQL Error registering user: {sqlEx.Message}, Code: {sqlEx.Number}, State: {sqlEx.State}, Line: {sqlEx.LineNumber}";
                throw new Exception(errorMessage, sqlEx);
            }
            catch (Exception ex)
            {
                
                throw new Exception("Error registering user", ex);
            }
        }

        public bool ValidateUser(string email, string password)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT Password FROM Registrations WHERE Email = @Email";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Email", email);
                        string storedPasswordHash = (string)command.ExecuteScalar();
                        return VerifyHashedPassword(storedPasswordHash, password);
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                throw new Exception("Error validating user", ex);
            }
        }
        public List<string> GetAllUsers()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT Email FROM Registrations";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        List<string> users = new List<string>();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                users.Add(reader.GetString(0));
                            }
                        }
                        return users;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving users", ex);
            }
        }

        public string SendFriendRequest(string fromEmail, string toEmail)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO FriendRequests (FromEmail, ToEmail) VALUES (@FromEmail, @ToEmail)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@FromEmail", fromEmail);
                        command.Parameters.AddWithValue("@ToEmail", toEmail);
                        command.ExecuteNonQuery();
                    }
                }
                return "Friend request sent successfully.";
            }
            catch (Exception ex)
            {
                return "Error sending friend request: " + ex.Message;
            }
        }

        public List<string> GetFriends(string email)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT ToEmail FROM FriendRequests WHERE FromEmail = @Email";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Email", email);
                        List<string> friends = new List<string>();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                friends.Add(reader.GetString(0));
                            }
                        }
                        return friends;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving friends", ex);
            }
        }

        public string SaveInterests(string email, string interests)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "UPDATE Registrations SET Interests = @Interests WHERE Email = @Email";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@Interests", interests);
                        command.ExecuteNonQuery();
                    }
                }
                return "Interests saved successfully.";
            }
            catch (Exception ex)
            {
                return "Error saving interests: " + ex.Message;
            }
        }

        public List<string> GetMessages(string email)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT Message FROM Messages WHERE ToEmail = @Email";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Email", email);
                        List<string> messages = new List<string>();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                messages.Add(reader.GetString(0));
                            }
                        }
                        return messages;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving messages", ex);
            }
        }

        public string SendMessage(string fromEmail, string toEmail, string message)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO Messages (FromEmail, ToEmail, Message) VALUES (@FromEmail, @ToEmail, @Message)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@FromEmail", fromEmail);
                        command.Parameters.AddWithValue("@ToEmail", toEmail);
                        command.Parameters.AddWithValue("@Message", message);
                        command.ExecuteNonQuery();
                    }
                }
                return "Message sent successfully.";
            }
            catch (Exception ex)
            {
                return "Error sending message: " + ex.Message;
            }
        }

        public List<string> GetAllNetworks()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT Id, Name FROM Networks";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        List<string> networks = new List<string>();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                networks.Add(reader.GetInt32(0) + " - " + reader.GetString(1));
                            }
                        }
                        return networks;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving networks", ex);
            }
        }

        public List<string> GetUserNetworks(string email)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT n.Id, n.Name FROM UserNetworks un JOIN Networks n ON un.NetworkId = n.Id WHERE un.UserEmail = @Email";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Email", email);
                        List<string> networks = new List<string>();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                networks.Add(reader.GetInt32(0) + " - " + reader.GetString(1));
                            }
                        }
                        return networks;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving user networks", ex);
            }
        }

        public string JoinNetwork(string email, int networkId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO UserNetworks (UserEmail, NetworkId) VALUES (@UserEmail, @NetworkId)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserEmail", email);
                        command.Parameters.AddWithValue("@NetworkId", networkId);
                        command.ExecuteNonQuery();
                    }
                }
                return "Joined network successfully.";
            }
            catch (Exception ex)
            {
                return "Error joining network: " + ex.Message;
            }
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private bool VerifyHashedPassword(string hashedPassword, string password)
        {
            string hashedInputPassword = HashPassword(password);
            return hashedPassword == hashedInputPassword;
        }

    }
}
