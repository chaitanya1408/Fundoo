using Experimental.System.Messaging;
using System;

namespace MSMQReceiver
{
    public delegate void MessageReceivedEventhandler(object sender, MessageEventArgs args);

    public class class1
    {
        public static void Main()
        {
            const string QueuePath = @".\Private$\EmailQueue";
            MSMQListener msmqListener = new MSMQListener(QueuePath);
            msmqListener.FormatterType = new Type[] { typeof(string) };
            msmqListener.MessageReceived += new MessageReceivedEventhandler(ListenMessageReceived);
            msmqListener.Start();
        }

        public static void ListenMessageReceived(object sender, MessageEventArgs messageeventargs)
        {
            Console.WriteLine("email received");
        }
    }
    public class MSMQListener
    {
        public bool listen;
        public Type[] types;
        public MessageQueue queue;
        public MSMQListener(string queuePath)
        {
            this.queue = new MessageQueue(queuePath);
        }
        public event MessageReceivedEventhandler MessageReceived;

        public Type[] FormatterType
        {
            get { return this.types; }
            set { this.types = value; }
        }
        public void Start()
        {
            this.listen = true;
            if (this.types != null && this.types.Length == 0)
            {
                this.queue.Formatter = new XmlMessageFormatter(this.types);
            }
            this.queue.ReceiveCompleted += new ReceiveCompletedEventHandler(this.OnReceiveCompleted);
            this.StartListening();
            Console.ReadKey();
        }
        public void Stop()
        {
            this.listen = false;
            this.queue.ReceiveCompleted -= new ReceiveCompletedEventHandler(this.OnReceiveCompleted);
        }
        private void StartListening()
        {
            if(this.listen)
            {
                return;
            }
            if (this.queue.Transactional)
            {
                this.queue.BeginPeek();
            }
            else
            {
                this.queue.BeginReceive();
            }
        }
        private void FireReceiveEvent(object body)
        {
            if (this.MessageReceived != null)
            {
                this.MessageReceived(this,new MessageEventArgs(body));
            }
        }
        private void OnReceiveCompleted(object sender,ReceiveCompletedEventArgs e)
        {
            Message message = this.queue.EndReceive(e.AsyncResult);
            Console.WriteLine("Message:"+message.Body+message.Label);
            EmailReceiver emailReceiver = new EmailReceiver();
            emailReceiver.Email(message.Body.ToString(), message.Label.ToString());
            this.StartListening();
            this.FireReceiveEvent(message.Body);
        }
    }
    public class MessageEventArgs : EventArgs
    {
        private object messageBody;
        public object MessageBody
        {
            get { return this.messageBody; }
        }
        public MessageEventArgs(object body)
        {
            this.messageBody = body;
        }
    }

}
