using Microsoft.Playwright.NUnit;
using Microsoft.Playwright;
using NUnit.Framework;
using Microsoft.AspNetCore.Mvc.RazorPages;

[Parallelizable(ParallelScope.Self)]
public class TrelloUiTests : PageTest
{
     private IPage _page;

     [SetUp]
     public async Task Setup()
     {
          
          _page = await BrowserConfig.InitializeBrowser();
     }

     [TearDown]
     public async Task TearDown()
     {
          if (_page != null)
          {
               await _page.Context.CloseAsync();  
               await _page.CloseAsync();          
          }
     }

     //  CREAR TABLERO POR UI Y VERIFICAR
     [Test]
     public async Task IsNewBoardDisplayed()
     {
          var trelloPage = new TrelloPage(_page);
          var loginPage = await trelloPage.GoToLogin();
          var boardPage = await loginPage.LoginUserByDefault();
          var titleBoard = await boardPage.CreateNewBoard();
          var isTitleDisplayed = await boardPage.IsTitleBoardSideBarVisible(titleBoard);
          Assert.That(isTitleDisplayed, Is.True);
     }

     //  CREAR TABLERO POR API Y VERIFICA
     [Test]
     public async Task IsNewBoardByApiAndVerifyByUi()
     {
          
          var boardRequest = new BoardRequest(_page);
          var titleBoard = await boardRequest.CreateBoard();
          var trelloPage = new TrelloPage(_page);
          var loginPage = await trelloPage.GoToLogin();
          var boardPage = await loginPage.LoginUserByDefault();
          var isTitleDisplayed = await boardPage.IsTitleBoardDashboardVisible(titleBoard);
         // Assert.That(isTitleDisplayed, Is.True);
     }
}
