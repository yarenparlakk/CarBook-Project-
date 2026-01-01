using System;
using Microsoft.AspNetCore.SignalR;

namespace UdemyCarBook.WebApi.Hubs
{
    public class CarHub : Hub
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CarHub(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task SendCarCount()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessange = await client.GetAsync("https://localhost:7003/api/Statistics/GetCarCount");
            var value = await responseMessange.Content.ReadAsStringAsync();
            await Clients.All.SendAsync("ReceiveCarCount", value);
        }
    }
}
