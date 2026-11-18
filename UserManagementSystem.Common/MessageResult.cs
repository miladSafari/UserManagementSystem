using System;
using System.Collections.Generic;
using System.Text;
using UserManagementSystem.Common.Enums;

namespace UserManagementSystem.Domain.Common
{
    public class MessageResult<T>
    {
        public string Message { get; set; }
        public StatusResult Status { get; set; }
        public T Result { get; set; }
    }
}
