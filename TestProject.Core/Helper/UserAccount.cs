using Plugin.Settings;
using System;
using System.Collections.Generic;
using System.Text;
using TestProject.Core.Models;

namespace TestProject.Core.Helper
{
    public class UserAccount
    {
        public static string GetUserId()
        {
            var user_Id = CrossSettings.Current.GetValueOrDefault("Twitter", string.Empty).ToString();

            return user_Id;
        }

        public static void SetUserId(TwitterUser twitterUser)
        {
            CrossSettings.Current.AddOrUpdateValue("Twitter", twitterUser.id_str);
        }
    }
}
