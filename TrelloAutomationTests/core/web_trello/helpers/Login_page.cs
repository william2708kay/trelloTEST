using Microsoft.Playwright;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

public class LoginPage : BasePage
{
     public LoginPage(IPage page) : base(page)
     {
          _locators["Enter email"] = "#username";
          _locators["Enter password"] = "#password";
          _locators["Continue"] = "#login-submit";
          _locators["Log in"] = "#login-submit";
     }

     public async Task<BoardPage> LoginUserByDefault()
     {
          var dataUser = await GetDataUser();

          await _uiManager.Fill(_locators["Enter email"], dataUser["username"]);
          await _uiManager.Click(_locators["Continue"]);
          await _uiManager.WaitDefault();
          await _uiManager.Fill(_locators["Enter password"], dataUser["password"]);
          await _uiManager.Click(_locators["Log in"]);
          await _uiManager.WaitDefault();

          return new BoardPage(_page);
     }

     private async Task<Dictionary<string, string>> GetDataUser()
     {
          var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Core", "ManageData", "credentials.json");
          string jsonString;

          try
          {
               jsonString = await File.ReadAllTextAsync(path);
          }
          catch (FileNotFoundException)
          {
               Console.WriteLine("El archivo credentials.json no se encontró. Verifique la ruta.");
               return null;
          }

          var data = new Dictionary<string, string>();

          try
          {
               JsonDocument doc = JsonDocument.Parse(jsonString);
               JsonElement root = doc.RootElement;

               if (root.TryGetProperty("defaultUser", out JsonElement defaultUser))
               {
                    if (defaultUser.TryGetProperty("username", out JsonElement username))
                    {
                         data["username"] = username.GetString();
                    }

                    if (defaultUser.TryGetProperty("password", out JsonElement password))
                    {
                         data["password"] = password.GetString();
                    }
               }
               else
               {
                    Console.WriteLine("No se encontró la propiedad 'defaultUser' en el JSON.");
               }

               return data;
          }
          catch (JsonException e)
          {
               Console.WriteLine($"Error al analizar el JSON: {e.Message}");
               return null;
          }
     }
}
