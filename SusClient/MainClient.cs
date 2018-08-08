using System;
using System.Diagnostics;
using CoreSus.Nodes;

namespace SusClient
{
    public class MainClient
    {
        public MainClient()
        {
            user = new Client();

            Console.Write("Creating client socket...");
            user.CreateSocket( //Creates the socket
                  user.GetLocalAddress("192.168.1.2"), //Gets the servers address
                  11345 //Is the port of the conbnection
            );
            Console.Write("Done!\n");
        }
        public void ConnectToServer()
        {
            Console.Write("Finding server...");
            while (!user.Connect())
            {
                System.Threading.Thread.Sleep(10000);
            }
            Console.Write("Done!\n");
        }

        public void BeginCommandProcessing()
        {
            Console.WriteLine("Waiting for command: ");
            byte[] latestCommand = new byte[1];
            latestCommand[0] = 0b00000000;

            while(latestCommand[0] != 0b00000001) //Quits taking commands once 'quit' command has been executed
            {
                if(latestCommand[0] == 0b00000010) //Executes the 'shutdown' command
                {
                    Console.WriteLine("'shutdown' command has been issued!");
                    ShutdownCommand(); //Runs the shutdown routine
                }
                else if(latestCommand[0] == 0b00000011)
                {
                    Console.WriteLine("'disconnect' command has been issued!");
                    DisconnectCommand();
                    break;
                }

                latestCommand = user.Receive(1); //Gets the latest command
                Console.WriteLine("'{0}' has been received!", latestCommand[0]);
            }
            Console.WriteLine("Command loop has exited!");
            if(latestCommand[0] == 0b00000011)
            {
                ConnectToServer();
            }
        }

        private void ShutdownCommand()
        {
            Process.Start("shutdown", "/p /f"); //Creates a new cmd that executes the shutdown command
        }

        private void DisconnectCommand()
        {
            user.Disconnect();
        }

        private Client user; //The main client
    }
}
