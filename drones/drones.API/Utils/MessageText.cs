namespace drones.API.Utils
{
    public class MessageText
    {
        public const string DRONE_CARGO_WEIGHT_EXCEDED = "The maximum load weight of the drone {0}gr was exceeded. Load weight to transport of {1}gr";
        public const string DRONE_LOADED = "The drone was loaded with the medication satisfactorily.";
        public const string DRONE_NO_FOUND = "No drone found with the serial number {0}";
        public const string DRONE_SERIAL_NUMBER_DUPLICATED = "Exist a drone registered with that serial number {0}";
        public const string DRONE_SERIAL_NUMBER_EMPTY = "The serial number of the drone cannot be empty";
        public const string DRONE_STATE_NO_READY_TO_FLY_BATTERY_LOW = "The drone SN:{0} is not ready to fly because the battery status is low {1}%";
        public const string DRONE_STATE_NO_READY_TO_FLY_BUSY = "The drone SN:{0} is not ready to fly because is busy. Status {1}";

        public const string MEDICATION_CODE_FORMAT_VALIDATION = "Code must only contain upper case letters, numbers, or '_'.";
        public const string MEDICATIONS_EMPTY = "No medications provided for loading";
        public const string MEDICATION_NO_FOUND = "No medication found with the Code {0}";

        public const string ENDPOINT_NAME_LOAD_MEDICATION = "Load medications Into a specific drone";
        public const string ENDPOINT_NAME_REGISTER_DRONE = "Registering a new drone";

        public const string HANDLE_API_RESPONSE_OK = "Request processed successfully from endpoint => {0}";
        public const string HANDLE_API_RESPONSE_CREATED = "Resource created successfully from endpoint => {0}";
        public const string HANDLE_API_RESPONSE_NO_FOUND = "Resource not found from endpoint => {0}";
        public const string HANDLE_API_RESPONSE_BAD_RESPONSE = "Bad request received from endpoint =>  {0}";
    }
}
