using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PowershellUtil
{
    public partial class PSTerminal : System.Web.UI.Page
    {

        public static string TerminalVersion { get { return PSUtilConstants.C_TERM_VERSION; } }


        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                // setup terminal header
                
            }
        }
    }
}