using Microsoft.Playwright;
using System.Text.Json;

public class ApiManager
{
     private readonly IPage _page;
     private readonly Dictionary<string, string> _headers;

     public ApiManager(IPage page)
     {
          _page = page;
          _headers = new Dictionary<string, string>
        {
            { "Content-Type", "application/json" }
        };
     }

     public async Task<T> Get<T>(string endpoint)
     {
          return await MakeRequest<T>("GET", endpoint);
     }

     public async Task<T> Post<T>(string endpoint, object data = null)
     {
          return await MakeRequest<T>("POST", endpoint, data);
     }

     public async Task<T> Delete<T>(string endpoint)
     {
          return await MakeRequest<T>("DELETE", endpoint);
     }

     private async Task<T> MakeRequest<T>(string method, string endpoint, object data = null)
     {
          var requestOptions = new
          {
               method = method,
               url = endpoint,
               headers = _headers,
               body = data != null ? JsonSerializer.Serialize(data) : null
          };

          var response = await _page.EvaluateAsync<JsonElement>(@"async (request) => {
            const fetchResponse = await fetch(request.url, {
                method: request.method,
                headers: request.headers,
                body: request.body
            });
            
            const responseText = await fetchResponse.text();
            return {
                status: fetchResponse.status,
                body: responseText
            };
        }", requestOptions);

          var status = response.GetProperty("status").GetInt32();
          var body = response.GetProperty("body").GetString();

          if (status == 200)
          {
               return JsonSerializer.Deserialize<T>(body);
          }

          throw new Exception($"Failed to make {method} request to {endpoint}. Status: {status}");
     }

     public async Task Post(string url)
     {
          await _page.APIRequest.PostAsync(url);
     }
}