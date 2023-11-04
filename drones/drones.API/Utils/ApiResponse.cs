using System.Net;

namespace drones.API.Utils
{
    public class ApiResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool IsOK { get; set; }
        public List<string> Errors { get; set; }
        public object Result { get; set; }
        public ApiResponse()
        {
            Errors = new List<string>();
        }
    }
}
