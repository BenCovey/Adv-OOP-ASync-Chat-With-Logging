using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatLib
{
    /// <summary>
    /// Abstract class for client/server
    /// </summary>
    public abstract class ClientServerParent
    {
        /// <summary>
        /// Abstract Connections method
        /// </summary>
        //Connections method
        abstract public void Connect();
        /// <summary>
        /// Abstract Listening Method
        /// </summary>
        //Listening Method
        abstract public void Listen();
        /// <summary>
        /// Abstract Send Data Method
        /// </summary>
        //Send Data Method
        abstract public void Send(String Data);
        /// <summary>
        /// Abstract Close Connection Method
        /// </summary>
        //Close Connection Method
        abstract public void Close();
    }
}
