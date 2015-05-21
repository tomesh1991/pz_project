using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PzProj.Requests
{
    public class HostRequest
    {
        /// <summary>
        /// Nazwa hosta
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// Id Hosta, niezmienny sprzętowy identyfikator hosta
        /// </summary>
        public long unique_id { get; set; }
    }
}
