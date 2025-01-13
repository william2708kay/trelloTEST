using Microsoft.Playwright;

public class BoardPage : BasePage
{
     public string PageUrl { get; private set; }

     public BoardPage(IPage page) : base(page)
     {
          _locators["Create new board"] = "div.board-tile p span";
          _locators["Board title"] = "input[data-testid='create-board-title-input']";
          _locators["Create button"] = "button[data-testid='create-board-submit-button']";
          _locators["Board Title dashboard"] = "//li//*[@class='board-tile-details-name']/div[text()='<replaceme>']";
          _locators["Board Title SideBar"] = "//ul[@data-testid='collapsible-list-items']//a[text()='<replaceme>']";
     }

     public async Task<string> CreateNewBoard(string newTitle = null)
     {
          var title = newTitle ?? await GenerateRandomName();
          await _uiManager.Click(_locators["Create new board"]);
          await _uiManager.Fill(_locators["Board title"], title);
          await _uiManager.Click(_locators["Create button"]);
          await _uiManager.WaitDefault();
          return title;
     }

     public async Task<bool> IsTitleBoardSideBarVisible(string titleBoard)
     {

          var locatorDynamic = await ReplaceValueInLocator("Board Title SideBar", titleBoard);


          return await _uiManager.IsElementVisible(locatorDynamic);
     }

     public async Task<bool> IsTitleBoardDashboardVisible(string titleBoard)
     {
          var locatorDynamic = await ReplaceValueInLocator("Board Title dashboard", titleBoard);
          return await _uiManager.IsElementVisible(locatorDynamic);
     }

}