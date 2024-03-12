using System.Net.Http.Json;

#pragma warning disable CS8600
#pragma warning disable CS8603
namespace ReadApi
{
    public class PersonLocation
    {
        public int Id { get; set; }
        public string PersonCode { get; set; }
        public string PersonRole { get; set; }
        public int LastSecurityPointNumber { get; set; }
        public string LastSecurityPointDirection { get; set; }
        public DateTime LastSecurityPointTime { get; set; }
    }

    internal class APIReader
    {
        private static readonly string url = "http://127.0.0.1:8082";
        private static readonly HttpClient client = new();

        public static async Task<PersonLocation> getPersonLocation(int id)
        {
            if (!await canConnectToAPI())
                return null;

            PersonLocation personLocation = null;
            HttpResponseMessage response = await client.GetAsync(url + $"/PersonLocation/{id}");
            if (response.IsSuccessStatusCode)
            {
                personLocation = await response.Content.ReadFromJsonAsync<PersonLocation>();
            }
            return personLocation;
        }

        public static async Task<List<PersonLocation>> getPersonsLocations()
        {
            if (!await canConnectToAPI())
                return null;

            List<PersonLocation> personLocations = null;
            HttpResponseMessage response = await client.GetAsync(url + "/PersonLocation");
            if (response.IsSuccessStatusCode)
            {
                personLocations = await response.Content.ReadFromJsonAsync<List<PersonLocation>>();
            }
            return personLocations;
        }

        private static async Task<bool> canConnectToAPI()
        {
            try
            {
                await client.GetAsync(url);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}


//output data from api http://127.0.0.1:8082/PersonLocation
/*
[
  {
    "id": 1,
    "PersonCode": "PC4560177",
    "PersonRole": "Клиент",
    "LastSecurityPointNumber": 24,
    "LastSecurityPointDirection": "in",
    "LastSecurityPointTime": "2024-04-23T18:25:43.511Z"
  },
  {
    "id": 2,
    "PersonCode": "PC4000247",
    "PersonRole": "Сотрудник",
    "LastSecurityPointNumber": 27,
    "LastSecurityPointDirection": "in",
    "LastSecurityPointTime": "2024-04-23T18:29:12.421Z"
  },
  {
    "id": 3,
    "PersonCode": "PC2460192",
    "PersonRole": "Сотрудник",
    "LastSecurityPointNumber": 27,
    "LastSecurityPointDirection": "in",
    "LastSecurityPointTime": "2024-04-23T18:31:33.247Z"
  },
  {
    "id": 4,
    "PersonCode": "PC2510429",
    "PersonRole": "Клиент",
    "LastSecurityPointNumber": 34,
    "LastSecurityPointDirection": "out",
    "LastSecurityPointTime": "2024-04-23T18:31:47.237Z"
  },
  {
    "id": 5,
    "PersonCode": "PC8750064",
    "PersonRole": "Сотрудник",
    "LastSecurityPointNumber": 11,
    "LastSecurityPointDirection": "in",
    "LastSecurityPointTime": "2024-04-23T18:31:41.412Z"
  },
  {
    "id": 6,
    "PersonCode": "PC3327408",
    "PersonRole": "Клиент",
    "LastSecurityPointNumber": 9,
    "LastSecurityPointDirection": "out",
    "LastSecurityPointTime": "2024-04-23T18:31:57.023Z"
  },
  {
    "id": 7,
    "PersonCode": "PC7854107",
    "PersonRole": "Сотрудник",
    "LastSecurityPointNumber": 27,
    "LastSecurityPointDirection": "in",
    "LastSecurityPointTime": "2024-04-23T18:32:05.212Z"
  },
  {
    "id": 8,
    "PersonCode": "PC6545275",
    "PersonRole": "Клиент",
    "LastSecurityPointNumber": 14,
    "LastSecurityPointDirection": "out",
    "LastSecurityPointTime": "2024-04-23T18:32:21.352Z"
  },
  {
    "id": 9,
    "PersonCode": "PC8545398",
    "PersonRole": "Клиент",
    "LastSecurityPointNumber": 7,
    "LastSecurityPointDirection": "out",
    "LastSecurityPointTime": "2024-04-23T18:35:11.501Z"
  }
]
*/

//output data from api http://127.0.0.1:8082/PersonLocation/1
/*
{
  "id": 1,
  "PersonCode": "PC4560177",
  "PersonRole": "Клиент",
  "LastSecurityPointNumber": 24,
  "LastSecurityPointDirection": "in",
  "LastSecurityPointTime": "2024-04-23T18:25:43.511Z"
}
*/
//Learn Microsoft - https://learn.microsoft.com/ru-ru/aspnet/web-api/overview/advanced/calling-a-web-api-from-a-net-client
