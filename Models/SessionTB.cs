using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VisitingBook.Models
{
    public class SessionTB
    {
        public Guid SessionID { get; set; }
        public string SessionKey { get; set; }
        public string SessionValue { get; set; }
    }
}