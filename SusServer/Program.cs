namespace SusServer
{
    class Program
    {
        static void Main(string[] args)
        {
            MainServer mainServer = new MainServer();
            mainServer.FindClient();
            mainServer.BeginInput();
        }
    }
}
