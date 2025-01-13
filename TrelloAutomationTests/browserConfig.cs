using Microsoft.Playwright;

public class BrowserConfig
{
     public static async Task<IPage> InitializeBrowser()
     {
       
          var playwright = await Playwright.CreateAsync();
          var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
          {
               Headless = false,
               SlowMo = 100
          });

          var context = await browser.NewContextAsync(new BrowserNewContextOptions
          {
               ViewportSize = new ViewportSize { Width = 1920, Height = 1080 },
               BaseURL = "https://trello.com"
          });

          var page = await context.NewPageAsync();
          return page;
     }
}
