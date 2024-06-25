using System;
using System.Messaging;

namespace RegisterLibrary
{
    public class QueueProcessor
    {
        private const string QueuePath = @".\private$\RegistrationQueue";

        public QueueProcessor()
        {
            if (!MessageQueue.Exists(QueuePath))
            {
                MessageQueue.Create(QueuePath);
            }
        }

        public void SendMessage(RegistrationDetails registrationDetails)
        {
            using (MessageQueue messageQueue = new MessageQueue(QueuePath))
            {
                messageQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(RegistrationDetails) });
                messageQueue.Send(registrationDetails);
            }
        }

        public RegistrationDetails ReceiveMessage()
        {
            using (MessageQueue messageQueue = new MessageQueue(QueuePath))
            {
                messageQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(RegistrationDetails) });
                Message message = messageQueue.Receive();
                return (RegistrationDetails)message.Body;
            }
        }

        public bool IsEmailInQueue(string email)
        {
            using (MessageQueue messageQueue = new MessageQueue(QueuePath))
            {
                messageQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(RegistrationDetails) });
                foreach (Message message in messageQueue.GetAllMessages())
                {
                    RegistrationDetails registrationDetails = (RegistrationDetails)message.Body; 
                    if (registrationDetails.Email == email)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

       
    }
}