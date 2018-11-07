using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Module.Email
{
    public class EmailOptions
    {
        public bool EnableSsl { get; set; }
        public int Port { get; set; }
        public string Host { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string From { get; set; }
        public string DisplayName { get; set; }
        // e.g. "~/EmailTemplate/Layout.cshtml"
        public string LayoutPath { get; set; }
    }
}
