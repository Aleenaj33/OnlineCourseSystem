using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace WCFService
{
    [ServiceContract]
    public interface IService
    {
        [OperationContract]
        string RegisterStudent(string name, string email);

        [OperationContract]
        List<string> GetAvailableCourses();

        [OperationContract]
        bool EnrollStudent(int studentId, int courseId);

        [OperationContract]
        List<string> GetStudentCourses(int studentId);
    }
}
