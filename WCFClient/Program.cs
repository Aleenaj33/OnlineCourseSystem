using System;
using System.ServiceModel;
using WCFService;

namespace WCFClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var binding = new BasicHttpBinding();
            var endpoint = new EndpointAddress("http://localhost:8000/WCFService");

            var factory = new ChannelFactory<IService>(binding, endpoint);
            var client = factory.CreateChannel();

            Console.WriteLine(client.RegisterStudent("John Doe", "john@example.com"));

            Console.WriteLine("\nAvailable courses:");
            foreach (var course in client.GetAvailableCourses())
            {
                Console.WriteLine(course);
            }

            bool enrolled = client.EnrollStudent(1, 2);
            Console.WriteLine(enrolled ? "\nEnrollment successful!" : "\nEnrollment failed!");

            Console.WriteLine("\nStudent 1 enrolled courses:");
            foreach (var course in client.GetStudentCourses(1))
            {
                Console.WriteLine(course);
            }

            Console.WriteLine("\nPress Enter to exit.");
            Console.ReadLine();
        }
    }
}
