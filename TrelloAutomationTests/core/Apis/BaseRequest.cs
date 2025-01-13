using Microsoft.Playwright;

public class BaseRequest
{
     protected readonly IPage _page;
     protected Dictionary<string, string> _endpoints;
     protected ApiManager _apiManager;
     protected dynamic _dataApi;
     protected string _baseUri;
     protected string _token;
     protected string _key;

     public BaseRequest(IPage page)
     {
          _page = page;
          _endpoints = new Dictionary<string, string>();
          _apiManager = new ApiManager(page);
     }

     public async Task LoadConfig()
     {
          // Carga los datos de la API desde el archivo JSON
          _dataApi = await Credentials.GetDataUser("api");

          // Accede de forma segura a los valores del JSON cargado
          _baseUri = _dataApi.GetProperty("baseuri").GetString(); 
          _token = _dataApi.GetProperty("token").GetString();     
          _key = _dataApi.GetProperty("apiKey").GetString();      
     }


     protected async Task<string> GenerateRandomName(int maxLength = 25)
     {
          var prefix = "BoardScript-";
          var timestamp = DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString();
          var randomName = prefix + timestamp;
          return randomName[..Math.Min(randomName.Length, maxLength)];
     }
}
