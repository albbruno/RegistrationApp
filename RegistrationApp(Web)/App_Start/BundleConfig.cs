using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace RegistrationApp_Web_
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        { 
            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/*.css"));

            bundles.Add(new ScriptBundle("~/Scripts/jquery").Include("~/Scripts/jquery-{version}.js").Include("~/Scripts/jquery.unobtrusive-ajax.js"));
        }

    }
}