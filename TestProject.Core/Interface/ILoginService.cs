using System;
using System.Collections.Generic;
using System.Text;
using TestProject.Core.Models;
using Xamarin.Auth;

namespace TestProject.Core.Interface
{
    public interface ILoginService
    {

        void LoginTwitter();
        Action OnLoggedInHandler { get; set; }
        OAuth1Authenticator Authenticator();
        void Logout();
    }
}
