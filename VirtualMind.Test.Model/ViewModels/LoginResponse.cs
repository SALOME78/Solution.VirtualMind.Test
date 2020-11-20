using System;
using System.Collections.Generic;
using System.Text;

namespace VirtualMind.Test.Model.ViewModels
{
    public class LoginResponse
    {
        public int IDUser { get; set; }
        public string UserName { get; set; }
        public bool Status { get; set; }
        public int IDRole { get; set; }
    }
}
