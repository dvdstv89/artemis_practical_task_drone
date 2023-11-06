using System.Net;

namespace drones.API.Utils
{
    public class ApiResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool IsValid { get; set; }
        public List<string> Errors { get; set; }
        public object Result { get; set; }
        public ApiResponse()
        {
            Errors = new List<string>();
        }

        public void AddOkResponse200(object result)
        {
            Result = result;
            StatusCode = HttpStatusCode.OK;
            IsValid = true;
            Errors = new List<string>();
        }

        public void AddCrateResponse204(object result)
        {
            Result = result;
            StatusCode = HttpStatusCode.Created;
            IsValid = true;
            Errors = new List<string>();
        }

        public void AddBadResponse400(string exception)
        {
            StatusCode = HttpStatusCode.BadRequest;
            IsValid = false;
            Errors.Add(exception);
            Result = null;
        }

        public void AddNotFoundResponse404(string mensaje)
        {
            StatusCode = HttpStatusCode.NotFound;
            IsValid = false;
            Errors.Add(mensaje);
            Result = null;
        }
    }
}
