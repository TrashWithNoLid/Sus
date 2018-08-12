using System;
using System.IO;
using System.Security;

namespace CoreSus
{
    namespace IP
    {
        public class IPManager
        {
            public IPManager(string dir, string file)
            {
                try
                {
                    fileLocation = dir + '/' + file;
                    if(!Directory.Exists(dir))
                    {
                        Directory.CreateDirectory(dir); //Creates the directory if it does not exist
                    }

                    if(!File.Exists(fileLocation)) //Checks if the file exists
                    {
                        WriteIP("127.0.0.1");
                    }
                }
                catch (UnauthorizedAccessException uae)
                {
                    Console.WriteLine("Unauthorized Access Exeption: {0}", uae.StackTrace);
                }
                catch (ArgumentException ae)
                {
                    Console.WriteLine("Argument Exception: {0}", ae.StackTrace);
                }
                catch (IOException ioe)
                {
                    Console.WriteLine("IO Exception: {0}", ioe.StackTrace);
                }
                catch (SecurityException secE)
                {
                    Console.WriteLine("Security Exception: {0}", secE.StackTrace);
                }
                catch (NotSupportedException nse)
                {
                    Console.WriteLine("Not Supported Exception: {0}", nse.StackTrace);
                }
            }
            public string GetIP()
            {
                try
                {
                    return File.ReadAllText(fileLocation); //Get the ip
                }
                catch (OutOfMemoryException ome)
                {
                    Console.WriteLine("Out Of Memory Exception: {0}", ome.StackTrace);
                }
                catch (IOException ioe)
                {
                    Console.WriteLine("IO Exception: {0}", ioe.StackTrace);
                }
                return null;
            }
            public void WriteIP(string newAddr)
            {
                File.WriteAllText(fileLocation, newAddr); //Writes the ip to the file
            }
            public string GetFileLocation()
            {
                return fileLocation;
            }

            private string fileLocation;
        }
    }
}
