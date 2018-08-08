namespace SusClient
{
    public class Program
    {
        static void Main(string[] args)
        {
            MainClient mainClient = new MainClient();
            mainClient.ConnectToServer();
            mainClient.BeginCommandProcessing();
        }
    }
}
