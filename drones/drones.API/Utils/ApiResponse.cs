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

        public void AddOkResponse200(object result)
        {
            Result = result;
            StatusCode = HttpStatusCode.OK;
            IsOK = true;
            Errors = new List<string>();
        }

        public void AddCrateResponse204(object result)
        {
            Result = result;
            StatusCode = HttpStatusCode.Created;
            IsOK = true;
            Errors = new List<string>();
        }

        public void AddBadResponse400(string exception)
        {
            StatusCode = HttpStatusCode.BadRequest;
            IsOK = false;
            Errors.Add(exception);
        }

        public void AddNotFoundResponse404(string mensaje)
        {
            StatusCode = HttpStatusCode.NotFound;
            IsOK = false;
            Errors.Add(mensaje);
        }
    }
}
