using System;
using System.ServiceModel;
using WCFService;

namespace WCFHost
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                using (ServiceHost host = new ServiceHost(typeof(CourseService)))
                {
                    host.Open();
                    Console.WriteLine("WCF Service is running...");
                    Console.WriteLine("Press Enter to terminate.");
                    Console.ReadLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                Console.ReadLine();
            }
        }
    }
}