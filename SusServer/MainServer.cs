using System;
using CoreSus.Nodes;
using System.Collections.Generic;

namespace SusServer
{
    public class MainServer
    {
        public MainServer()
        {
            server = new Server();

            Console.Write("Creating server socket...");
            server.CreateSocket( //Creates the socket for the server
                server.ConvertToAddress(""), //Gets the address of the server
                11345 //The port the server is listening on
            );
            Console.Write("Done!\n");

            Console.Write("Binding server information to socket...");
            server.Bind(); //Binds the server information to the socket
            Console.Write("Done\n");
        }
        public void FindClient()
        {
            Console.Write("Finding connection...");
            user = server.FindConnection(); //Finds a connection and accepts it
            Console.Write("Done!\n");
        }
        public void BeginInput()
        {
            string latestCommand = String.Empty; //Stores the newest command

            while (latestCommand != "quit") //Runs until the user quits
            {
                Console.Write(">");
                latestCommand = Console.ReadLine(); //Gets the user input for commands


                string command = String.Empty; //Stores the first segment of the command
                string dataBuffer = String.Empty;

                var arguments = new List<string> { }; //Stores the arguments of the command
                bool isArg = false; //Determines if the data is a command or an argument

                foreach (char c in latestCommand)
                {
                    if (c == ' ')
                    {
                        if (!isArg) //Checks if the data is an argument or command
                        {
                            command = dataBuffer; //Sets the command to the current data
                            isArg = true; //Lets the loop know that the command has been found

                        }
                        else
                        {
                            arguments.Add(dataBuffer); //Adds the current data to the arguments
                        }
                        dataBuffer = String.Empty; //Resets the current data
                    }
                    else
                    {
                        dataBuffer += c; //Adds the current character to the data buffer
                    }
                }

                if(dataBuffer != String.Empty) //Checks if any data was left over
                {
                    if(command == String.Empty) //Checks if the left over data was a command or argument
                    {
                        command = dataBuffer; //Sets the command to the data
                    }
                    else
                    {
                        arguments.Add(dataBuffer);  //Adds the data into the arguments
                    }
                }

                Console.WriteLine("Command: {0}, Argument Size: {1}", command, arguments.Count);

                //Begin parsing commands
                if(command == "shutdown")
                {
                    if(arguments.Count == 0)
                    {
                        user.Send(0b00000010);
                    }
                    else
                    {
                        Console.WriteLine("Received '{0}' arguments, Expected: '{1}'!", arguments.Count, '0');
                    }
                }
                else if(command == "quit")
                {
                    if(arguments.Count == 0)
                    {
                        user.Send(0b00000001);
                    }
                    else if (arguments.Count == 1)
                    {
                        if (arguments[0] == "all")
                        {
                            user.Send(0b00000001);
                            break;
                        }
                        else if (arguments[0] == "this")
                        {
                            user.Send(0b00000011);
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Unknown parameter: \"{0}\"", arguments[0]);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Unknown command!");
                }
            }
        }

        private Server server;
        private Client user;
    }
}
