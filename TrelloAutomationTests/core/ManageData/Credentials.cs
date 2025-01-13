using System.Text.Json;

public static class Credentials
{
     public static async Task<dynamic> GetDataUser(string userInfo = "defaultUser")
     {
          var path = Path.Combine(Directory.GetCurrentDirectory(), "Core", "ManageData", "credentials.json");
          var jsonContent = await File.ReadAllTextAsync(path);
          var credentials = JsonSerializer.Deserialize<Dictionary<string, dynamic>>(jsonContent);
          return credentials[userInfo];
     }
}