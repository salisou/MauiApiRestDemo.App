using MauiApiRestDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MauiApiRestDemo.ViewModels
{
    public class MainViewModel
    {
        HttpClient client;
        JsonSerializerOptions _serializeOption;
        string baseUrl = "https://64a45849c3b509573b576bde.mockapi.io";
        private List<Users> Users;
        public MainViewModel()
        {
            client = new HttpClient();
            _serializeOption = new JsonSerializerOptions
            {
                WriteIndented = true,
            };
        }
        public ICommand GetAllUsersCommand => new Command(async () =>
        {
            // Get List of users
            var url = $"{baseUrl}/users";
            var response = await client.GetAsync(url);

            if(response.IsSuccessStatusCode)
            {
                //var content = await response.Content.ReadAsStringAsync();
                using(var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    var data = await JsonSerializer.DeserializeAsync<List<Users>>(responseStream, _serializeOption);
                    Users = data;
                };
            }
        });

        public ICommand GetSingleUserCommand => new Command(async() =>
        {
            var url = $"{baseUrl}/users/25";
            var response = await client.GetAsync(url);
        
        
        });


        public ICommand AddUserCommand => new Command(async() =>
        {
            var url = $"{baseUrl}/users";
            var user = new Users
            {
                createdAt = DateTime.Now,
                name = "Moussa",
                avatar = "https://fakeimg.pl/350x200/?text=Moussa"
            };

            string json = JsonSerializer.Serialize<Users>(user, _serializeOption);
            
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            
            var response = await client.PostAsync(url, content);
        });


        public ICommand UpdateUserCommand => new Command(async () =>
        {
            var user = Users.FirstOrDefault(x => x.id == "1");
            var url = $"{baseUrl}/users/1";
            user.name = "test";

            string json = JsonSerializer.Serialize<Users>(user, _serializeOption);

            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(url, content);
        });

        public ICommand DeleteUserCommand => new Command(async () => 
        {
            var url = $"{baseUrl}//users/10";
            var responce = await client.DeleteAsync(url);
        });

    }
}
