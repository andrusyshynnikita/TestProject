﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Foundation;
using SQLite;
using TestProject.Core.Interface;
using UIKit;

namespace TestProjectForms.iOS.Services
{
    class DatabaseConnectionService : IDatabaseConnectionService
    {
        public DatabaseConnectionService()
        {
            var database = GetDatebaseConnection();
        }

        public SQLiteConnection GetDatebaseConnection()
        {
            var dbName = "TaskyDB.db3";
            var path = Path.Combine(System.Environment.
              GetFolderPath(System.Environment.
              SpecialFolder.Personal), dbName);
            return new SQLiteConnection(path);
        }
    }
}