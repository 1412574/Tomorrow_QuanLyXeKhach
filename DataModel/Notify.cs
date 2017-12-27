using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class Notify
    {
        public string message { get; set; }
        public string notificationType { get; set; }
        public Notify(string _message, string _notificationType)
        {
            message = _message;
            notificationType = _notificationType;
        }
    }
}
