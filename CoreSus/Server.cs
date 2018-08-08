using System;
using System.Security;
using System.Net.Sockets;
using CoreSus.Nodes.Foundation;

namespace CoreSus
{
    namespace Nodes
    {
        public class Server : Node
        {
            public Server(){}
            public Server(Socket socket) : base(ref socket) {}

            public void Bind() //Binds the socket to the ip and port
            {
                if (socket != null) //Ensures the socket has been created
                {
                    try
                    {
                        socket.Bind(endPoint);
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
                }
                else
                {
                    Console.WriteLine("Error: The Socket has not been created!");
                }
            }
            public Client FindConnection()
            {
                if(socket != null) //Ensures the socket has been created
                {
                    try
                    {
                        socket.Listen(1); //Waits for a connection

                        cSocket = socket.Accept(); //Gets the info of the newly connected client
                        client = new Client(cSocket); //Create a client object
                        return client; //Return the object
                    }
                    catch (SocketException se)
                    {
                        Console.WriteLine("Socket Exception: {0}", se.StackTrace);
                    }
                    catch (ObjectDisposedException ode)
                    {
                        Console.WriteLine("Object Disposed Exception: {0}", ode.StackTrace);
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
                return null; //Return null if the creation failed
            }

            private Socket cSocket;
            private Client client;
        }
    }
}
