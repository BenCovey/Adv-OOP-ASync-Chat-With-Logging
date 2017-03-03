using System;
namespace ChatLib
{
    public class MessageRecievingEventArgs : EventArgs
    {
        public MessageRecievingEventArgs(string message)
        {
            RealMessage = message;
        }
        public string RealMessage{ get; }
    }
}
