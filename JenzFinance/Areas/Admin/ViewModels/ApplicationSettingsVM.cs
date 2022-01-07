using System;
using System.Collections.Generic;
using System.Linq;

namespace JenzFinance.Areas.Admin.ViewModels
{
    public class ApplicationSettingsVM
    {
        public int ID { get; set; }
        public string AppName { get; set; }
        public byte[] Logo { get; set; }
        public byte[] Favicon { get; set; }

    }
}