using MVC.ShiftLogger.Models;
using Newtonsoft.Json;
using RestSharp;
using System.Globalization;
using System.Web;

namespace MVC.ShiftLogger.Services
{
    public class ShiftService
    {
        private static string ApiUrl = "https://localhost:7230/";
        public void AddShift(Shift shift)
        {
            string format = "dd/MM/yyyy HH:mm";

            string FirstName = shift.FirstName;

            string LastName = shift.LastName;

            DateTime StartTime = shift.StartTime;

            DateTime EndTime = shift.EndTime;

            string parsedToISO8601StartTime = StartTime.ToString("o");
            string parsedToISO8601EndTime = EndTime.ToString("o");

            var client = new RestClient(ApiUrl);
            var request = new RestRequest("api/ShiftApi", Method.Post);

            request.AddJsonBody(new
            {
                firstName = FirstName,
                lastName = LastName,
                startTime = parsedToISO8601StartTime,
                endTime = parsedToISO8601EndTime
            });

            client.ExecuteAsync(request);
        }

        public List<Shift> ReadShifts()
        {
            var client = new RestClient(ApiUrl);
            var request = new RestRequest("api/ShiftApi");
            var response = client.ExecuteAsync(request);

            List<Shift> shifts = new();

            if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var rawResponse = response.Result.Content;

                var deserializedResponse = JsonConvert.DeserializeObject<List<Shift>>(rawResponse);

                shifts = deserializedResponse;

                return shifts;
            }

            return shifts;
        }

        public Shift ReadShift(int shiftId)
        {
            List<Shift> shiftsToUpdate = ReadShifts();
            Shift chosenShift = new();

            var client = new RestClient(ApiUrl);
            var request = new RestRequest($"api/ShiftApi/{HttpUtility.UrlEncode(shiftId.ToString())}");
            var response = client.ExecuteAsync(request);

            if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var rawResponse = response.Result.Content;

                var deserializedResponse = JsonConvert.DeserializeObject<Shift>(rawResponse);

                chosenShift = deserializedResponse;

                return chosenShift;
            }

            return chosenShift;
        }

        public void UpdateShift(Shift shift)
        {
            var client = new RestClient(ApiUrl);
            var request = new RestRequest($"api/ShiftApi/{HttpUtility.UrlEncode(shift.Id.ToString())}");

            string newFirstName = shift.FirstName;
            string newLastName = shift.LastName;

            string newStartTimeString = shift.StartTime.ToString();
            DateTime parsedToDateTimeStartTime = DateTime.ParseExact(newStartTimeString, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None);

            string newEndTimeString = shift.StartTime.ToString();
            DateTime parsedToDateTimeEndTime = DateTime.ParseExact(newEndTimeString, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None);

            TimeSpan WorkedTime = parsedToDateTimeEndTime - parsedToDateTimeStartTime;

            string parsedToISO8601StartTime = parsedToDateTimeStartTime.ToString("o");
            string parsedToISO8601EndTime = parsedToDateTimeEndTime.ToString("o");

            request.AddBody(new
            {
                id = shift.Id,
                firstName = shift.FirstName,
                lastName = shift.LastName,
                startTime = parsedToISO8601StartTime,
                endTime = parsedToISO8601EndTime,
                workedTime = WorkedTime
            });

            client.ExecutePutAsync(request);
        }

        public void DeleteShift(int shiftId)
        {
            List<Shift> shifts = ReadShifts();

            var client = new RestClient(ApiUrl);
            var request = new RestRequest($"api/ShiftApi/{HttpUtility.UrlEncode(shiftId.ToString())}", Method.Delete);
            client.ExecuteAsync(request);
        }
    }
}
