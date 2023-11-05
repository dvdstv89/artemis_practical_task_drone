using System.Net;

namespace drones.API.Utils
{
    public class MessageText
    {
        public const string DRONE_SERIAL_NUMBER_EMPTY = "The serial number of the drone cannot be empty";
        public const string DRONE_NO_FOUND = "No drone found with the serial number {0}";
        public const string DRONE_SERIAL_NUMBER_DUPLICATED = "Exist a drone registered with that serial number {0}";

        public const string ENDPOINT_NAME_REGISTER_DRONE = "Registering a new drone";

        public const string HANDLE_API_RESPONSE_OK = "Request processed successfully from endpoint => {0}";
        public const string HANDLE_API_RESPONSE_CREATED = "Resource created successfully from endpoint => {0}";
        public const string HANDLE_API_RESPONSE_NO_FOUND = "Resource not found from endpoint => {0}";
        public const string HANDLE_API_RESPONSE_BAD_RESPONSE = "Bad request received from endpoint =>  {0}";
    }
}
