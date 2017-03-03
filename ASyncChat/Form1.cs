using System;
using System.Threading;
using ChatLib;
using ChatLogLib;
using System.Windows.Forms;
//using Logging;

namespace ASyncChat
{
    public partial class Form1 : Form
    {
        Thread ListenerThread;
        Client client;
        bool Connected = false;

        /// <summary>
        /// Form 1 Second Constructor
        /// </summary>
        public Form1(Client client)
        {
            this.client = client;
            InitializeComponent();
            //Disable Send Button
            btnSend.Enabled = false;
            //Create File for log with time stamp
            client.MessageHandler += new MessageRecievingEventHandler(newData);
        }

        public Form1()
        {
            
            InitializeComponent();
            //Disable Send Button
            btnSend.Enabled = false;
            //Create File for log with time stamp
            client.MessageHandler += new MessageRecievingEventHandler(newData);

        }

        /// <summary>
        /// New Data, the method to recieve data from the Custom Event Args
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="Data"></param>
        private void newData(Object Sender, MessageRecievingEventArgs Data)
        {
            if (txtConversation.InvokeRequired)
            {
                txtConversation.Invoke(new MethodInvoker(delegate ()
                {
                    //Append Text to box
                    txtConversation.Text += "\r\n" + "Server: " + Data.RealMessage;
                }));
            }//end if
            else
            {
                //Append Text to box
                txtConversation.Text += "\n" + "Server: " + Data;

            }//end else
        }//end newData

        private void exit_click(object sender, EventArgs e)
        {
            client.Send("Connection Closed");
            client.Send("quit");
            client.Close();
            txtConversation.Text += "\r\nConnection Closed\r\n";
            client.Recieving = false;
            ListenerThread.Join();           
            Application.Exit();
        }//end exit_click

        private void connectToolStripMenuItem_Click(object sender, EventArgs e)
        {

            //Client Connect
            client.Connect();
            bool isAvail = client.Connection();
            if (isAvail == true)
            {
                
                //Enable Send Button
                btnSend.Enabled = true;
                //Enable Bool
                Connected = true;
                //Create Listener Thread, and start it
                ListenerThread = new Thread(client.Listen);
                ListenerThread.Start();
                txtConversation.Text += "Connected to the Server. \r\n";
            }
            else
            {
                txtConversation.Text += "No Connection Available \r\n";
            }

        }//end Connect

        private void disconnectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Disconnect Client
            client.Send("Connection Closed");
            client.Send("quit");
            client.Close();
            txtConversation.Text += "\r\nConnection Closed\r\n";
            Connected = false;
            btnSend.Enabled = false;
        }//end Disconnect

        private void btnSend_Click(object sender, EventArgs e)
        {
            //Get User Data From Text Box
            String UserData = txtMessage.Text;
            //Add to Text for Conversation
            txtConversation.Text += "\r\n" + "Client: " + UserData;
            //Send Message
            client.Send(UserData);
            //Clear the Textbox
            txtMessage.Clear();

        }//end Send_Click

        

        private void txtMessage_TextChanged(object sender, EventArgs e)
        {
            if (txtMessage.TextLength == 0 || Connected == false)
            {
                btnSend.Enabled = false;
            }//End if
            else
            {
                btnSend.Enabled = true;
            }//End Else
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            client.Send("Connection Closed");
            client.Send("quit");
            client.Close();
            txtConversation.Text += "\r\nConnection Closed";
            client.Recieving = false;
            try
            {
                ListenerThread.Join();
            }
            catch (Exception err) { }
            Application.Exit();
        }

        private void exitGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            client.Send("Connection Closed");
            client.Send("quit");
            client.Close();
            txtConversation.Text += "\r\nConnection Closed";
            client.Recieving = false;
            ListenerThread.Join();
            Application.Exit();
        }
    }//end Form
}//End Main