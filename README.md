# artemis_practical_task_drone

This project implements a REST API service in ASP.NET Core Web API, using .NET 7, to manage a set of drones responsible for delivering medicines. Drones can register, load medications, and perform various operations related to their status and cargo.

#Project Configuration

## Previous requirements
	Make sure you have the following installed: .NET 7 SDK

## Database Configuration

	The project uses an in-memory database, so no additional configuration is required. The necessary data is loaded automatically when you start the application.

###Database Structure
	The database has the following entities:

		Drone
		{
			SerialNumber(PK) : string (cadena de hasta 100 caracteres)
			Model : Enum (Lightweight = 1, Middleweight = 2, Cruiserweight = 3, Heavyweight = 4)
			WeightLimit : double (Range 1 : 500 gr)
			BatteryCapacity : double (Range 0:100)
			State : Enum (IDLE = 1, LOADING = 2, LOADED = 3, DELIVERING = 4, DELIVERED = 5, RETURNING = 6)
		}

		Medication
		{
			Code(PK) : string (allowed only letters, numbers, ‘-‘, ‘_’)
			Name : (allowed only upper case letters, underscore and numbers)
			Weight : double (Range 1 : 500 gr)	
			Image : byte[]
		}

		Many-to-many relationship between Drones and Medicines count an attribute in the relationship to know the amount of a medicine loaded in the drone
		DroneMedication 
		{	
			DroneSerialNumber : string
			MedicationCode : string
			Count: int
		}

		PeriodicTaskLog
		{
			Id : GUID
			SerialNumber : string
			BatteryCapacity : double
			Date : DateTime
		}

### Initial Data Set
	When you start the application, a sample data set is automatically loaded to ensure a functional environment for testing and demonstrations.
	This data set includes:
	
	#### Drones
	
		Drone { SerialNumber = "DRN001", Model = DroneModel.Lightweight, WeightLimit = 100, BatteryCapacity = 80, State = DroneState.IDLE }
        Drone { SerialNumber = "DRN002", Model = DroneModel.Lightweight, WeightLimit = 175, BatteryCapacity = 70, State = DroneState.LOADED }
        Drone { SerialNumber = "DRN003", Model = DroneModel.Middleweight, WeightLimit = 250, BatteryCapacity = 18, State = DroneState.RETURNING }
        Drone { SerialNumber = "DRN004", Model = DroneModel.Middleweight, WeightLimit = 300, BatteryCapacity = 23, State = DroneState.RETURNING }
        Drone { SerialNumber = "DRN005", Model = DroneModel.Cruiserweight, WeightLimit = 350, BatteryCapacity = 40, State = DroneState.IDLE }
        Drone { SerialNumber = "DRN006", Model = DroneModel.Cruiserweight, WeightLimit = 400, BatteryCapacity = 5, State = DroneState.IDLE }
        Drone { SerialNumber = "DRN007", Model = DroneModel.Cruiserweight, WeightLimit = 325, BatteryCapacity = 75, State = DroneState.IDLE }
        Drone { SerialNumber = "DRN008", Model = DroneModel.Heavyweight, WeightLimit = 500, BatteryCapacity = 80, State = DroneState.IDLE }
        Drone { SerialNumber = "DRN009", Model = DroneModel.Heavyweight, WeightLimit = 480, BatteryCapacity = 98, State = DroneState.IDLE }
        Drone { SerialNumber = "DRN010", Model = DroneModel.Heavyweight, WeightLimit = 500, BatteryCapacity = 100, State = DroneState.IDLE }
		
	#### Medication
	    Medication { Code = "MED001", Name = "MedicationA", Weight = 80, Image = new byte[] { 255, 255, 255, 255 } }
        Medication { Code = "MED002", Name = "MedicationB", Weight = 60, Image = new byte[] { 255, 255, 255, 255 } }
        Medication { Code = "MED003", Name = "MedicationC", Weight = 45, Image = new byte[] { 255, 255, 255, 255 } }
        Medication { Code = "MED004", Name = "MedicationD", Weight = 97, Image = new byte[] { 255, 255, 255, 255 } }
        Medication { Code = "MED005", Name = "MedicationE", Weight = 38, Image = new byte[] { 255, 255, 255, 255 } }
        Medication { Code = "MED006", Name = "MedicationF", Weight = 100, Image = new byte[] { 255, 255, 255, 255 } }
        Medication { Code = "MED007", Name = "MedicationG", Weight = 50, Image = new byte[] { 255, 255, 255, 255 } }
        Medication { Code = "MED008", Name = "MedicationH", Weight = 75, Image = new byte[] { 255, 255, 255, 255 } }
        Medication { Code = "MED009", Name = "MedicationI", Weight = 57, Image = new byte[] { 255, 255, 255, 255 } }
        Medication { Code = "MED010", Name = "MedicationJ", Weight = 40, Image = new byte[] { 255, 255, 255, 255 } }
		
	#### DroneMedication
	    DroneMedication { DroneSerialNumber = "DRN002", MedicationCode = "MED001", Count = 1 }
        DroneMedication { DroneSerialNumber = "DRN002", MedicationCode = "MED002", Count = 1 }
	
	
# Project Configuration

## Build the project
	dotnet build

## Run the project
	dotnet run

# Periodic Battery Log Task
The system includes a periodic task that records the battery status of all drones every 10 seconds and saves them in a table called PeriodicTaskLog.
	Drone: DRN001     | BatteryCapacity: 80  | Date: 5/11/2023 11:33:51 PM
    Drone: DRN002     | BatteryCapacity: 70  | Date: 5/11/2023 11:33:51 PM
    Drone: DRN003     | BatteryCapacity: 18  | Date: 5/11/2023 11:33:51 PM
    Drone: DRN004     | BatteryCapacity: 23  | Date: 5/11/2023 11:33:51 PM
    Drone: DRN005     | BatteryCapacity: 40  | Date: 5/11/2023 11:33:51 PM
    Drone: DRN006     | BatteryCapacity: 5   | Date: 5/11/2023 11:33:51 PM
    Drone: DRN007     | BatteryCapacity: 75  | Date: 5/11/2023 11:33:51 PM
    Drone: DRN008     | BatteryCapacity: 80  | Date: 5/11/2023 11:33:51 PM
    Drone: DRN009     | BatteryCapacity: 98  | Date: 5/11/2023 11:33:51 PM
    Drone: DRN010     | BatteryCapacity: 100 | Date: 5/11/2023 11:33:51 PM
    Drone: DRONE123   | BatteryCapacity: 100 | Date: 5/11/2023 11:33:51 PM

# API usage
The service provides endpoints to interact with the drones. Below are details:

POST /api/Drone Register a new drone.
POST /api/Drone/load-medications/{serialNumber}: Load medicines into a specific drone.
GET /api/Drone/chek-load-medications/{serialNumber}: Gets the medications loaded on a drone.
GET /api/Drone/chek-available-drone-for-loading: Gets the list of drones available for charging.
GET /api/Drone/chek-battery-capacity/{serialNumber}: Get the battery level of a specific drone.
POST /api/Drone/change-battery-level/{serialNumber}: Change the battery level of a specific drone.
POST /api/Drone/change-state/{serialNumber}: Change the state of a specific drone.
GET /api/Log/{serialNumber}: Get the battery status log of a specific drone recorded by the periodic task.

## Application Example
{
  "serialNumber": "DRONE123",
  "model": 1,
  "weightLimit": 500,
  "batteryCapacity": 100,
  "state": 1
}

## Example of a Created Response
{
  "statusCode": 201,
  "isValid": true,
  "errors": [],
  "result": {
    "serialNumber": "DRONE123",
    "model": 1,
    "weightLimit": 500,
    "batteryCapacity": 100,
    "state": 1
  }
}

## Example of Bad Response
{
  "statusCode": 400,
  "isValid": false,
  "errors": [
    "Exist a drone registered with the same serial number DRONE123"
  ],
  "result": null
}

## Example of Not Found Response
{
  "statusCode": 404,
  "isValid": false,
  "errors": [
    "No drone found with the serial number 1"
  ],
  "result": null
}

## Example of Log Battery State Response
{
  "statusCode": 200,
  "isValid": true,
  "errors": [],
  "result": [
    {
      "serialNumber": "DRONE123",
      "batteryCapacity": 100,
      "date": "2023-11-05T23:28:01.5045138-05:00"
    },
    {
      "serialNumber": "DRONE123",
      "batteryCapacity": 100,
      "date": "2023-11-05T23:28:11.5012361-05:00"
    },
    {
      "serialNumber": "DRONE123",
      "batteryCapacity": 100,
      "date": "2023-11-05T23:28:21.4923649-05:00"
    }
  ]
}

# Testing execution
	Make sure you have the following installed: .NET 7 SDK

52 test cases have been prepared to ensure the functionality and stability of the service.
These test cases cover various aspects of the system, including drone registration, medication loading, load verification on specific drones, and battery monitoring.

The test cases are located in the "test" directory of the project and can be run using the command:


## Navigate to the test project directory
	cd drones/drones.API.test

## Run the tests
	dotnet test

Passed!  - Failed:     0, Passed:    52, Skipped:     0, Total:    52, Duration: 761 ms - drones.API.test.dll (net7.0)