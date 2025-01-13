using Microsoft.Playwright;

public class BoardRequest : BaseRequest
{
     public BoardRequest(IPage page) : base(page)
     {
          _endpoints["CREATE_BOARD"] = "/1/boards";
          _endpoints["GET_BOARDS"] = "/1/members/me/boards"; // Endpoint para obtener tableros
     }

     public async Task<string> CreateBoard(string newTitle = null)
     {
          await LoadConfig();
          var title = newTitle ?? await GenerateRandomName();
          var endpointValues = $"?name={title}&key={_key}&token={_token}";

          try
          {
               var response = await _apiManager.Post<BoardResponse>(
                   $"{_baseUri}{_endpoints["CREATE_BOARD"]}{endpointValues}");
               return response.Name;
          }
          catch (Exception ex)
          {
               throw new Exception($"Failed to create board: {ex.Message}");
          }
     }

     public async Task<List<BoardResponse>> GetBoards()
     {
          await LoadConfig();
          try
          {
               var response = await _apiManager.Get<List<BoardResponse>>(
                   $"{_baseUri}{_endpoints["GET_BOARDS"]}?key={_key}&token={_token}");
               return response;
          }
          catch (Exception ex)
          {
               throw new Exception($"Failed to get boards: {ex.Message}");
          }
     }
}


public class BoardResponse
{
     public string Id { get; set; }
     public string Name { get; set; }
     public string Url { get; set; }
}
