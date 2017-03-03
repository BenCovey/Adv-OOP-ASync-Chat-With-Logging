using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using LogLib;
using Logging;

namespace ChatLib
{
    /// <summary>
    /// Client child to ClientServerParent
    /// </summary>
    public class Client : ClientServerParent
    {
        /// <summary>
        /// All globally accessed variables at the top, including delegate,
        /// Data variable, server ip, Port data, the holder for TCP, and streaming.
        /// </summary>
        private readonly ILoggingService logger;
        public MessageRecievingEventHandler MessageHandler;
        public string Data = "";
        string Line;
        static string Server = "127.0.0.1";
        static Int32 Port = 13000;
        TcpClient TCPClient;
        NetworkStream Stream;
        public bool Recieving = true;
        private TextLogger textLogger;


        /// <summary>
        /// TextLogger 
        /// </summary>
        /// <param name="textLogger"></param>
        public Client(ILoggingService logger)
        {
            this.logger = logger;
        }

        /// <summary>
        /// Connection check function
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public bool Connection()
        {
            try
            {
                if (Stream.CanRead == false)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception err)
            {
                return false;
            }
        }
        /// <summary>
        /// Close method, responsible for closing the connection 
        /// </summary>
        public override void Close()
        {
            try
            {
                //Close Connection
                TCPClient.Close();
                Stream.Close();
            }//End Try
            catch (Exception err) { }
        }//end Close();

        /// <summary>
        /// Connect method responsible for connecting the two clients together.
        /// </summary>
        public override void Connect()
        {
            try
            {
                //Create TcpClient, open stream
                TCPClient = new TcpClient(Server, Port);
                Stream = TCPClient.GetStream();
            }//End try
            catch(SocketException err)
            {
                Console.Write(err);
            }
        }//end Connect()

        /// <summary>
        /// Send new message to other client/Server
        /// </summary>
        /// <param name="Data"></param>
        public override void Send(string Data)
        {
            try
            {
                if (Stream.CanWrite)
                {
                    //Create response in bytes
                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(Data);
                    //Send response
                    //Create line with timestamp, and add message
                    Line = DateTime.Now.ToString("yyyy-MM-dd-HH:mm:ss") + " Client: " + Data + "\n";
                    //Write to log
                    logger.Log(Line);
                    Stream.Write(msg, 0, msg.Length);
                }//end if
            }catch(Exception err)
            {

            }
        }//end method

        /// <summary>
        /// Listen method for listening for messages
        /// </summary>
        public override void Listen()
        {
            while (Recieving)
            {
                string RecievedMessage = "";
                try
                {
                    //Byte Array for response Data
                    Byte[] Data = new Byte[256];

                    if (Stream.DataAvailable)
                    {
                        Int32 Bytes = Stream.Read(Data, 0, Data.Length);
                        RecievedMessage = System.Text.Encoding.ASCII.GetString(Data, 0, Bytes);
                        Line = DateTime.Now.ToString("yyyy-MM-dd-HH:mm:ss") + " Server: " + RecievedMessage + "\n";
                        //Write to log
                        logger.Log(Line);
                        MessageHandler(this, new MessageRecievingEventArgs(RecievedMessage));
                        //MessageHandler(RecievedMessage);
                    }//end if
                                    
                }//end Try
                catch(Exception err)
                {
                    //For testing only! Remove later
                    Console.Write(err);
                }//end catch
            }//end while
        }
    }//end class

}//end main
