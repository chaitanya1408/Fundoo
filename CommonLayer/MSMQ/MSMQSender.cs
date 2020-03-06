using Experimental.System.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.MSMQ
{
    public class MSMQSender
    {
        public void SendToQueue(string email, string token)
        {

            MessageQueue messageQueue=null;

            const string QueuePath = @".\Private$\EmailQueue";

            if (!MessageQueue.Exists(QueuePath))
            {
                messageQueue = MessageQueue.Create(QueuePath);
            }
            else
            {
                messageQueue = new MessageQueue(QueuePath);
            }

            try
            {
                messageQueue.Send(email, token);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
            finally
            {
                messageQueue.Close();
            }

            Console.WriteLine("Email Sent");
        }
    }
}
