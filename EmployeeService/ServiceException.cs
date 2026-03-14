using System.Net;
using System.ServiceModel.Web;

namespace EmployeeService
{
    public static class ServiceException
    {
        public static WebFaultException<string> NotFound(string message)
        {
            return new WebFaultException<string>(message, HttpStatusCode.NotFound);
        }

        public static WebFaultException<string> BadRequest(string message)
        {
            return new WebFaultException<string>(message, HttpStatusCode.BadRequest);
        }
    }
}