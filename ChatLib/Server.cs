using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatLib
{
    /// <summary>
    /// Server : ClientServerParents
    /// </summary>
    public class Server : ClientServerParent
    {
        /// <summary>
        /// Globals for TCPListener, Byte Array for recieving message
        /// Data String, TCPClient, Stream and Data, IP, and port Data
        /// </summary>
        TcpListener TCPServer = null;
        Byte[] Bytes = new Byte[256];
        String Data = null;
        Int32 Port = 13000;
        IPAddress LocalAddress = IPAddress.Parse("127.0.0.1");
        TcpClient TCPClient = null;
        NetworkStream Stream;
        String Message;

        /// <summary>
        /// Close Method, close all connections
        /// </summary>
        public override void Close()
        {
            //Close Connections, Stop Server
            TCPClient.Close();
            TCPServer.Stop();
            Stream.Close();
        }//end Close

        /// <summary>
        /// Connect Method, to connect Client to the server
        /// </summary>
        public override void Connect()
        {
            try
            {
                //Set up Server
                TCPServer = new TcpListener(LocalAddress, Port);
                //Start Server
                TCPServer.Start();
                //BLOCKING CALL
                TCPClient = TCPServer.AcceptTcpClient();
                //Create Stream object
                Stream = TCPClient.GetStream();
            }//end try
            catch(SocketException err)
            {
                //For Testing Only! Remove later
                Console.Write(err);
            }//end Catch
        }//end Connect

        /// <summary>
        ///  Send is for sending data in a byte array to the client
        /// </summary>
        /// <param name="Data"></param>
        public override void Send(string Data)
        {
            if (Stream.CanWrite)
            {
                //Create response in bytes
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(Data);
                //Send response
                Stream.Write(msg, 0, msg.Length);
            }//end if
        }//end Send()

        /// <summary>
        /// Listen method for listening for incoming messages
        /// </summary>
        public override void Listen()
        {
            if (Stream.DataAvailable)
            {
                Int32 Data = Stream.Read(Bytes, 0, Bytes.Length);
                Message = System.Text.Encoding.ASCII.GetString(Bytes, 0, Data);
            }//end if
        }//end Listen
    }//End Server
}//End Main
