using Microsoft.Playwright;

public class BasePage
{
     protected readonly IPage _page;
     protected readonly UiManager _uiManager;
     protected Dictionary<string, string> _locators;

     public BasePage(IPage page)
     {
          _page = page;
          _uiManager = new UiManager(page);
          _locators = new Dictionary<string, string>();
     }

     public async Task NavigateToSite(string siteUrl)
     {
          
          if (!siteUrl.StartsWith("http") && !siteUrl.StartsWith("/"))
          {
               siteUrl = "/" + siteUrl;
          }
          await _page.GotoAsync(siteUrl);
          await _uiManager.WaitDefault();
     }

     protected async Task<string> GenerateRandomName(int maxLength = 25)
     {
          var prefix = "Board Script - ";
          var timestamp = DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString();
          var randomName = prefix + timestamp;
          return randomName[..Math.Min(randomName.Length, maxLength)];
     }

     protected async Task<string> ReplaceValueInLocator(string key, string replacement)
     {
          if (_locators.ContainsKey(key))
          {
               var currentValue = _locators[key];
               var newValue = currentValue.Replace("<replaceme>", replacement);
               _locators[key] = newValue;
          }
          return _locators[key];
     }
}