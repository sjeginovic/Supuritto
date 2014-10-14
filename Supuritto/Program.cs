using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Supuritto
{
    class Program
    {
        static void Main(string[] args)
        {

            string location = AppDomain.CurrentDomain.BaseDirectory;
            string name = Path.Combine(location, "input.txt");
            List<string> allHosts = new List<string> { };
            bool firstOne = true;
            int lastFoundLineNumber = 0;
            //FileStream fileStream = new FileStream(name, FileMode.Open);
            string[] fileContent = File.ReadAllLines(name);
            try
            {

                for (int i=0; i<fileContent.Length;i++)
                {

                    string testLine = fileContent[i];
                    if (testLine.Contains("Server Name:") && !firstOne)
                    {
                        StringBuilder sb = new StringBuilder();

                        for (int j=lastFoundLineNumber;j<=i-1;j++)
                        {
                            sb.Append(fileContent[j]);
                            sb.Append(Environment.NewLine);
                        }

                        int index = fileContent[lastFoundLineNumber].IndexOf(':');

                        string newFile = fileContent[lastFoundLineNumber].Substring(index + 1);
                        string newFileName = Path.Combine(location + @"\output\" + newFile + ".txt");
                        allHosts.Add(newFile);
                        using (StreamWriter outfile = new StreamWriter(newFileName))
                        {
                            outfile.Write(sb.ToString());
                        }
                        lastFoundLineNumber = i;
                    }
                    else
                    {
                        firstOne = false;
                    }

                }

                //search for Server Name
                //bool present = fileContent.IndexOf("Server Name: ") >= 0; 
                // read from file or write to file
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }

            finally
            {
                //fileStream.Close();
                StringBuilder sb = new StringBuilder();

                foreach (string host in allHosts)
                {
                    sb.Append(host);
                    sb.Append(Environment.NewLine);
                }

               
                string newFileName = Path.Combine(location + @"\output\AllHosts.txt");
                
                using (StreamWriter outfile = new StreamWriter(newFileName))
                {
                    outfile.Write(sb.ToString());
                }


            }

            Console.WriteLine("Pres Enter to exit");
            Console.ReadLine();

        }
    }
}
