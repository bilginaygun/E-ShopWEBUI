using E_ShopWEBUI.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ShopWEBUI.Helper.SessionHelper
{
    public class SessionManager
    {

        public static LoginDTO? LoggedCustomer
        {
            get => AppHttpContext.Current.Session.GetObject<LoginDTO>("LoginDTO");
            set => AppHttpContext.Current.Session.SetObject("LoginDTO", value);
        }


    }
}
