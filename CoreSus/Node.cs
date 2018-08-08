using System;
using System.Security;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace CoreSus
{
    namespace Nodes
    {
        namespace Foundation
        {
            public class Node
            {
                public Node(){}
                public Node(ref Socket socket)
                {
                    this.socket = socket;
                }
                ~Node()
                {
                    try
                    {
                        socket.Shutdown(SocketShutdown.Both); //Cleans up after the socket
                        socket.Close();
                    }
                    catch (SocketException se)
                    {
                        Console.WriteLine("Socket Exception: {0}", se.StackTrace);
                    }
                    catch (ObjectDisposedException ode)
                    {
                        Console.WriteLine("Object Disposed Exception: {0}", ode.StackTrace);
                    }
                }
                public IPAddress GetLocalAddress(string addr)
                {
                    try
                    {
                        return Dns.GetHostAddresses(addr)[0]; //Gets the local ip address
                    }
                    catch (ArgumentNullException ane)
                    {
                        Console.WriteLine("Argument Null Exception: {0}", ane.StackTrace);
                    }
                    catch (ArgumentOutOfRangeException are)
                    {
                        Console.WriteLine("Argument Out Of Range Exception: {0}", are.StackTrace);
                    }
                    catch (SocketException se)
                    {
                        Console.WriteLine("Socket Exception: {0}", se.StackTrace);
                    }
                    catch (ArgumentException ae)
                    {
                        Console.WriteLine("Argument Exception: {0}", ae.StackTrace);
                    }
                    return null;
                }

                public void CreateSocket(IPAddress addr, int port)
                {
                    endPoint = new IPEndPoint(addr, port); //Creates an endpoint for the socket
                    socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp); //Creates the socket
                }

                public void Send(byte data)
                {
                    if (socket != null) //Ensures the socket has been created
                    {
                        try
                        {
                            var array = new byte[1]; //Stores the one byte of data in an array
                            array[0] = data;
                            socket.Send(array); //Transmits the one byte of data
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
                    }
                    else
                    {
                        Console.WriteLine("Error: The Socket has not been created!");
                    }
                }

                public byte[] Receive(int bufferSize)
                {
                    if (socket != null) //Ensures socket is not null
                    {
                        try
                        {
                            byte[] buffer = new byte[bufferSize];
                            socket.Receive(buffer);

                            return buffer;
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
                    return null;
                }

                public void Disconnect()
                {
                    try
                    {
                        socket.Disconnect(true);
                    }
                    catch (PlatformNotSupportedException pse)
                    {
                        Console.WriteLine("Platform Not Supported Exception: {0}", pse.StackTrace);
                    }
                    catch (ObjectDisposedException ode)
                    {
                        Console.WriteLine("Object Disposed Exception: {0}", ode.StackTrace);
                    }
                    catch (SocketException se)
                    {
                        Console.WriteLine("Socket Exception: {0}", se.StackTrace);
                    }
                }

                protected IPEndPoint endPoint; //Stores the address and port of the other node
                protected Socket socket; //The socket used to communicate
            }
        }
    }
}
