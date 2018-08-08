using System;
using System.Security;
using System.Net.Sockets;
using CoreSus.Nodes.Foundation;
namespace CoreSus
{
    namespace Nodes
    {
        public class Client :  Node
        {
            public Client(){}
            public Client(Socket socket) : base(ref socket) {}

            public bool Connect() //Connects the client socket to the server
            {
                if (socket != null) //Ensures the socket has been created
                {
                    try
                    {
                        socket.Connect(endPoint); //Attempts to connect to the server
                        return true;
                    }
                    catch (ArgumentNullException ane)
                    {
                        Console.WriteLine("Argument Null Exception: {0}", ane.StackTrace);
                    }
                    catch (SocketException se)
                    {
                        Console.WriteLine("Socket Exception: {0}", se.StackTrace);
                    }
                    catch (ObjectDisposedException ode)
                    {
                        Console.WriteLine("Object Disposed Exception: {0}", ode.StackTrace);
                    }
                    catch (SecurityException secE)
                    {
                        Console.WriteLine("Security Exception: {0}", secE.StackTrace);
                    }
                    catch (InvalidOperationException ioe)
                    {
                        Console.WriteLine("Invalid Operation Exception: {0}", ioe.StackTrace);
                    }
                }
                else
                {
                    Console.WriteLine("Error: The Socket has not been created!");
                }
                return false;
            }
        }
    }
}
