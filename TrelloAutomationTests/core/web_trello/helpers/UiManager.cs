using Microsoft.Playwright;

public class UiManager
{
     private readonly IPage _page;
     private const int DefaultTimeout = 30000;

     public UiManager(IPage page)
     {
          _page = page;
     }

     public async ValueTask Click(string selector, int timeout = DefaultTimeout) 
     {
          try
          {
               await _page.WaitForSelectorAsync(selector, new()
               {
                    State = WaitForSelectorState.Visible,
                    Timeout = timeout
               });
               await _page.ClickAsync(selector);
          }
          catch (TimeoutException)
          {
               await _page.EvaluateAsync($"document.querySelector('{selector}').click()");
          }
     }

     public async ValueTask Fill(string selector, string value, int timeout = DefaultTimeout) 
     {
          await _page.WaitForSelectorAsync(selector, new()
          {
               State = WaitForSelectorState.Visible,
               Timeout = timeout
          });
          await _page.FillAsync(selector, value);
     }

     public async ValueTask WaitDefault() 
     {
          await _page.WaitForLoadStateAsync(LoadState.NetworkIdle);
          await _page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
     }

     public async ValueTask<bool> IsElementVisible(string selector) 
     {
          try
          {
               await _page.WaitForSelectorAsync(selector, new()
               {
                    State = WaitForSelectorState.Visible,
                    Timeout = 5000
               });
               return true;
          }
          catch
          {
               return false;
          }
     }
}