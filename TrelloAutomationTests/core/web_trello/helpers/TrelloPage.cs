using Microsoft.Playwright;

public class TrelloPage : BasePage
{
     public TrelloPage(IPage page) : base(page)
     {
          _locators["Log in"] = "a[href*='/login']"; 
     }

     public async ValueTask<LoginPage> GoToLogin() 
     {
          await NavigateToSite("https://trello.com");

          var elements = await _page.QuerySelectorAllAsync(_locators["Log in"]);
          Console.WriteLine($"Found {elements.Count} login elements");

          await _page.WaitForLoadStateAsync(LoadState.NetworkIdle);
          await _page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);

           
          string selector = _locators["Log in"];
          Console.WriteLine($"Attempting to click on selector: {selector}");

            
          string escapedSelector = selector.Replace("'", "\\'").Replace("\"", "\\\"");

          await _page.EvaluateAsync($"document.querySelector('{escapedSelector}').click()");

          return new LoginPage(_page);
     }
}