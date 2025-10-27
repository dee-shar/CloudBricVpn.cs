using System.Net.Http;
using System.Text.Json;
using System.Net.Http.Json;
using System.Net.Http.Headers;

namespace CloudBricVpnApi
{
    public class CloudBricVpn
    {
        private string accessToken;
        private readonly HttpClient httpClient;
        private readonly string uuid = Guid.NewGuid().ToString();
        private readonly string apiUrl = "https://vpn-controller.cloudbric.com/api/v2/";
        public CloudBricVpn()
        {
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Dart/3.8 (dart:io)");
            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }
     
        public async Task<string> Register(string email)
        {
            var data = JsonContent.Create(new {email = email});
            var response = await httpClient.PostAsync($"{apiUrl}/sign/up", data);
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> CompleteRegistration(int verificationCode, string registrationToken)
        {
            var data = JsonContent.Create(new { token = registrationToken, code = verificationCode });
            var response = await httpClient.PostAsync($"{apiUrl}/auth/code", data);
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> SetAccountPassword(string password, string verificationToken)
        {
            var data = JsonContent.Create(new { token = verificationToken, password = password });
            var response = await httpClient.PostAsync($"{apiUrl}/user/password", data);
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> Login(string email, string password)
        {
            var data = JsonContent.Create(new
            {
                email = email,
                password = password,
                device_id = Guid.NewGuid().ToString(),
                public_key = "/xBYYIEI3mDQ3FRchamBFylkr+qfIz4PSNlvQez4/jE=",
                os_type = "AOS",
                os_version = "9",
                app_version = "2.0.5"
            });
            var response = await httpClient.PostAsync($"{apiUrl}/sign/in", data);
            var content = await response.Content.ReadAsStringAsync();
            var document = JsonDocument.Parse(content);
            if (document.RootElement.TryGetProperty("data", out var dataElement) && dataElement.TryGetProperty("access_token", out var tokenElement))
            {
                accessToken = tokenElement.GetString();
                httpClient.DefaultRequestHeaders.Add("Authorization", accessToken);
            }
            return content;
        }
        
        public async Task<string> GetServers()
        {
            var response = await httpClient.GetAsync($"{apiUrl}/vpn-server");
            return await response.Content.ReadAsStringAsync();
        }
    }
}
