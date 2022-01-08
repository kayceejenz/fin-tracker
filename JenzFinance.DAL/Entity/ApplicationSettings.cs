using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JenzFinance.DAL.Entity
{
    public class ApplicationSettings
    {
        public int Id { get; set; }
        public string AppName { get; set; }
        public byte[] Logo { get; set; }
        public byte[] Favicon { get; set; }
        public bool AllowSetupRouteAccess { get; set; }
    }
}
