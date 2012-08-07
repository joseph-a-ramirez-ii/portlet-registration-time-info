using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CUS.ICS.RegistrationTimeInfoPortlet
{
    public class RegistrationTimeInfoPortlet : Jenzabar.Portal.Framework.Web.UI.PortletBase
    {
        protected override Jenzabar.Portal.Framework.Web.UI.PortletViewBase GetCurrentScreen()
        {
            return (this.LoadPortletView("ICS/RegistrationTimeInfoPortlet/Default_View.ascx"));
        }
    }
}
