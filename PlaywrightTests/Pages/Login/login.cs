using Microsoft.Playwright;
using PlaywrightTests.Helpers;
using System.Threading.Tasks;

namespace PlaywrightTests.Pages.Login
{
    public class LoginPage(IPage page)
    {
        private readonly IPage _page = page;
        private readonly string _userNameField = "//input[@placeholder='Username']";
        private readonly string _passwordField = "//input[@placeholder='Password']";
        private readonly string _loginButton = "//button[@type='submit']";


        public async Task EnterUserName(string username)
        {
            await Helper.Fill(_page, _userNameField, username);
        }

        public async Task EnterPassword(string password)
        {
            await Helper.Fill(_page, _passwordField, password);
        }

        public async Task ClickLoginButton()
        {
            await Helper.Click(_page, _loginButton);
        }

        public async Task Login(string email, string password)
        {
            await EnterUserName(email);
            await EnterPassword(password);
            await ClickLoginButton();
        }
    }
}
