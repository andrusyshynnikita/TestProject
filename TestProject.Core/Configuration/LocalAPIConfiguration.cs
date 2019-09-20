using System;
using System.Collections.Generic;
using System.Text;
using TestProject.Core.Configuration.Interfaces;

namespace TestProject.Core.Configuration
{
    public class LocalAPIConfiguration : IAPIConfiguration
    {
        public string CloudHostUrl => $"http://10.10.2.144:3000";
    }
}
