﻿using JenzFinance.Areas.Admin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace JenzFinance.Areas.Admin.Interfaces
{
    interface IApplicationSettingsService
    {
        // Application settings
        ApplicationSettingsVM GetApplicationSettings();
        bool UpdateApplicationSettings(ApplicationSettingsVM Vmodel, HttpPostedFileBase Logo, HttpPostedFileBase Favicon);

    }
}
