

using System;
using System.IO;
using System.Linq;
using TestProject.Core.Models;

namespace TestProject.Core.Helper
{
    public class Constants
    {
        //** For Twitter **//
        public static string TWITTER_KEY = "3xQZI7K71BrOJ7DFVkL1CPTXp";
        public static string TWITTE_SECRET = "96oyLeoB5B9YFcJuPw46WYcDGLTR8u31lfH0hOcrhZCETgWaZB";
        public static string TWITTE_REQ_TOKEN = "https://api.twitter.com/oauth/request_token";
        public static string TWITTER_AUTH = "https://api.twitter.com/oauth/authorize";
        public static string TWITTER_ACCESS_TOKEN = "https://api.twitter.com/oauth/access_token";
        public static string TWITTE_CALLBACKURL = "https://www.google.com/"; //"https://mobile.twitter.com";
        public static string TWITTER_REQUESTURL = "https://api.twitter.com/1.1/account/verify_credentials.json";

        //** For AudioService **//
        public static string INITIAL_AUDIO_FILE_PATH = Path.Combine(System.Environment.
               GetFolderPath(System.Environment.
               SpecialFolder.Personal), "0" + TwitterUserId.Id_User) + ".3gpp";

        public static string AUDIO_FILE_PATH(string nameFile)
        {
           string filename = nameFile.Split('\\').LastOrDefault().Split('/').LastOrDefault();
            string audio_File_Path = Path.Combine(System.Environment.
                   GetFolderPath(System.Environment.
                   SpecialFolder.Personal), nameFile);

            return audio_File_Path;

        }

        
    }
}

