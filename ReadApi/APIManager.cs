using System;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;

#pragma warning disable CS8603
#pragma warning disable CS8625
namespace ReadApi
{
    public class Medicines
    {
        public Medicines(int ID, string Name, string Storage, int Count)
        {
            this.ID = ID;
            this.Name = Name;
            this.Storage = Storage;
            this.Count = Count;
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public string Storage { get; set; }
        public int Count { get; set; }
    }

    enum HttpRequestType
    {
        GET,
        POST,
        PUT,
        DELETE
    }

    internal class APIManager
    {
        private static readonly HttpClient client = new();
        private static readonly string API_URL = "http://192.168.0.105:5016/api/Medicines/";

        public APIManager()
        {
            client.BaseAddress = new Uri(API_URL);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        private async Task<bool> canConnectToAPI()
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("checkStatus");
                return true;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        private async Task<object> executeRequest<T>(HttpRequestType requestType, string request, Medicines medicine = null)
        {
            if (!await canConnectToAPI())
                return null;

            HttpResponseMessage response = requestType switch
            {
                HttpRequestType.GET => await client.GetAsync(request),
                HttpRequestType.POST => await client.PostAsJsonAsync(request, medicine),
                HttpRequestType.PUT => await client.PutAsJsonAsync(request, medicine),
                HttpRequestType.DELETE => await client.DeleteAsync(request),
                _ => throw new NotImplementedException(),
            };

            if (requestType != HttpRequestType.GET)
            {
                response.EnsureSuccessStatusCode();
                return response.StatusCode;
            }

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<T>();
            }

            return null;
        }

        public async Task<Medicines> getMedicine(int index)
        {
            object result = await executeRequest<Medicines>(HttpRequestType.GET, $"getMedicine?index={index}");
            return result != null ? (Medicines)result : null;
        }

        public async Task<List<Medicines>> getMedicines()
        {
            object result = await executeRequest<List<Medicines>>(HttpRequestType.GET, "getMedicines");
            return result != null ? (List<Medicines>)result : null;
        }

        public async Task<HttpStatusCode> createMedicine(Medicines medicine)
        {
            object result = await executeRequest<HttpStatusCode>(HttpRequestType.POST, "addMedicine", medicine);
            return result != null ? (HttpStatusCode)result : HttpStatusCode.InternalServerError;
        }

        public async Task<HttpStatusCode> deleteMedicine(int index)
        {
            object result = await executeRequest<HttpStatusCode>(HttpRequestType.DELETE, $"deleteMedicine?index={index}");
            return result != null ? (HttpStatusCode)result : HttpStatusCode.InternalServerError;
        }

        public async Task<HttpStatusCode> updateMedicine(int index, Medicines medicine)
        {
            object result = await executeRequest<HttpStatusCode>(HttpRequestType.PUT, $"updateMedicine?index={index}", medicine);
            return result != null ? (HttpStatusCode)result : HttpStatusCode.InternalServerError;
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