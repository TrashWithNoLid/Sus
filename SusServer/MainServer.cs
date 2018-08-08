using System;
using CoreSus.Nodes;
namespace SusServer
{
    public class MainServer
    {
        public MainServer()
        {
            server = new Server();

            Console.Write("Creating server socket...");
            server.CreateSocket( //Creates the socket for the server
                server.GetLocalAddress(""), //Gets the address of the server
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
            string latestCommand = ""; //Stores the newest command
            while (latestCommand != "quit") //Runs until the user quits
            {
                Console.Write(">");
                latestCommand = Console.ReadLine(); //Gets the user input for commands

                //Begin parsing commands
                if(latestCommand == "quit")
                {
                    user.Send(0b00000001);
                }
                else if(latestCommand == "shutdown")
                {
                    user.Send(0b00000010);
                }
                else if(latestCommand == "disconnect")
                {
                    latestCommand = "quit";
                    user.Send(0b00000011);
                }
            }
        }

        private Server server;
        private Client user;
    }
}
