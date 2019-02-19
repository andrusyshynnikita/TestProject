using Newtonsoft.Json;
using System;
using System.Linq;
using TestProject.Core.Helper;
using TestProject.Core.Interface;
using TestProject.Core.Models;
using Xamarin.Auth;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace TestProject.Core.services
{
    public class LoginService : ILoginService
    {
        private OAuth1Authenticator _auth;
        private TwitterUser _twitterUser;
        private Account _currentUserAccount;
        public Action OnLoggedInHandler
        {
            get; set;
        }

        public void LoginTwitter()
        {
            _auth = new OAuth1Authenticator(
                               Constants.TWITTER_KEY,
                               Constants.TWITTE_SECRET,
                               new Uri(Constants.TWITTE_REQ_TOKEN),
                               new Uri(Constants.TWITTER_AUTH),
                               new Uri(Constants.TWITTER_ACCESS_TOKEN),
                               new Uri(Constants.TWITTE_CALLBACKURL));

            _auth.AllowCancel = true;
            _auth.Completed += twitter_auth_Completed;
        }

        async private void twitter_auth_Completed(object sender, AuthenticatorCompletedEventArgs eventArgs)
        {
            
            if (eventArgs.IsAuthenticated)
            {
                Account loggedInAccount = eventArgs.Account;
                // AccountStore.Create().Save(loggedInAccount, "Twitter");
                var request = new OAuth1Request("GET",
                    new Uri("https://api.twitter.com/1.1/account/verify_credentials.json"),
                    null,
                    eventArgs.Account);
                var response = await request.GetResponseAsync();
                
                var json = response.GetResponseText();

                _twitterUser = JsonConvert.DeserializeObject<TwitterUser>(json);
                CrossSettings.Current.AddOrUpdateValue("Twitter", _twitterUser.id_str);
                OnLoggedInHandler();
            }

        }

        public void Logout()
        {
            var data = CrossSettings.Current.Contains("Twitter");

            if (data == true)
            {
                CrossSettings.Current.Clear();
            }
        }

        public OAuth1Authenticator Authenticator()
        {
            return _auth;
        }
    }
}
