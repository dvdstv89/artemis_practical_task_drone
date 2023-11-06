namespace drones.API.Utils
{
    public class MessageText
    {
        public const string DRONE_CARGO_WEIGHT_EXCEDED = "The maximum load weight of the drone {0}gr was exceeded. Load weight to transport of {1}gr";
        public const string DRONE_CHANGE_STATE_TO_LOADING_WITH_BATTERY_LOW = "The drone SN:{0} has the battery status low {1}%";
        public const string DRONE_LOADED = "The drone was loaded with the medication satisfactorily.";
        public const string DRONE_LOG_NOT_FOUND = "No battery capacity records found for drone with this serial number {0}";
        public const string DRONE_NOT_FOUND = "No drone found with the serial number {0}";
        public const string DRONE_NOT_FOUND_EMPTY_DB = "No drone founds in the database";
        public const string DRONE_NOT_FOUND_AVAILABLES_FOR_LOADING = "No drone found availablea for loading";
        public const string DRONE_SERIAL_NUMBER_DUPLICATED = "Exist a drone registered with the same serial number {0}";
        public const string DRONE_SERIAL_NUMBER_EMPTY = "The serial number of the drone cannot be empty";
        public const string DRONE_STATE_NO_READY_TO_FLY_BATTERY_LOW = "The drone SN:{0} is not available because the battery status is low {1}%";
        public const string DRONE_STATE_NO_READY_TO_FLY_BUSY = "The drone SN:{0} is not ready to available because is busy. Status {1}";

        public const string MEDICATION_CODE_FORMAT_VALIDATION = "Code must only contain upper case letters, numbers, or '_'.";
        public const string MEDICATION_NAME_FORMAT_VALIDATION = "Name must only contain letters, numbers, '_', or '-'.";
        public const string MEDICATIONS_EMPTY = "No medications provided for loading";
        public const string MEDICATION_NOT_FOUND = "No medication found with the Code {0}";
        public const string MEDICATION_LOADED_NOT_FOUND = "No medication loades found in the drone with serial number {0}";
        public const string MEDICATION_WEIGHT_MIN_VALUE_VALIDATION = "Weight must be greater than 0.";

        public const string ENDPOINT_NAME_REGISTER_DRONE = "Registering a new drone";
        public const string ENDPOINT_NAME_LOAD_MEDICATION = "Loading a drone with medication items";
        public const string ENDPOINT_NAME_CKECK_LOAD_MEDICATION = "Checking loaded medication items for a given drone";
        public const string ENDPOINT_NAME_CKECK_AVAILABLES_DRONES = "Checking available drones for loading";
        public const string ENDPOINT_NAME_CKECK_BATTERY_LEVEL_DRONE = "Check drone battery level for a given drone";
        public const string ENDPOINT_NAME_Change_STATE_DRONE = "Change drone state for a given drone";
        public const string ENDPOINT_NAME_Change_BATTERY_LEVEL_DRONE = "Change drone battery capacity for a given drone";
        public const string ENDPOINT_NAME_SHOW_LOGS = "Check drone battery capacity logs for a given drone";       

        public const string HANDLE_API_RESPONSE_OK = "Request processed successfully from endpoint => {0}";
        public const string HANDLE_API_RESPONSE_CREATED = "Resource created successfully from endpoint => {0}";
        public const string HANDLE_API_RESPONSE_NOT_FOUND = "Resource not found from endpoint => {0}";
        public const string HANDLE_API_RESPONSE_BAD_RESPONSE = "Bad request received from endpoint =>  {0}";

        public const string PERIODIC_TASK_EXCEPTION = " Failed to execute PeriodicHostedService with exception message {0}";
    }
}