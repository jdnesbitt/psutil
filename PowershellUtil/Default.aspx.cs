using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

using System.Management.Automation;
using System.Collections;

using System.IO;

namespace PowershellUtil
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                PopulateScripts();
            }
        }
        
        private void PopulateScripts()
        {
            string scriptPath = Server.MapPath("./PSScripts");
            cmbScripts.Items.Clear();
            cmbScripts.Items.Add("");

            // get file list
            string[] files = Directory.GetFiles(scriptPath, "*.ps1");

            foreach(string fileName in files)
            {
                string name = Path.GetFileName(fileName);

                cmbScripts.Items.Add(name);
            }
        }

        protected void btnExecute_Click(object sender, EventArgs e)
        {
            string scriptPath = Server.MapPath("./PSScripts");
            string htmlResult = "";

            string psScriptFile = scriptPath + "\\" + cmbScripts.Text;

            string psScript = "";
            
            // if script exists, load contents
            if (System.IO.File.Exists(psScriptFile))
                psScript = System.IO.File.ReadAllText(psScriptFile);

            // get form arguments if submitted

            var shell = PowerShell.Create();
            
            try
            {
                shell.Commands.AddScript(psScript);

                // check if form values exist
                if (Request.Form != null)
                {
                    // iterate over form values 
                    foreach (string key in Request.Form.AllKeys)
                    {
                        // only use form value if it starts with PSUTIL_
                        if (key.StartsWith("PSUTIL_"))
                        {
                            string val = Request.Form[key];
                            string paramName = key.Substring("PSUTIL_".Length);

                            // add ps param
                            shell.Commands.AddParameter(paramName, val);
                        }
                    }
                }

                //System.Collections.ObjectModel.Collection<PSObject>
                var results = shell.Invoke();

                if (results.Count > 0)
                {
                    // We use a string builder ton create our result text
                    var builder = new StringBuilder();

                    foreach (var psObject in results)
                    {
                        // Convert the Base Object to a string and append it to the string builder.
                        // Add \r\n for line breaks
                        builder.Append(psObject.BaseObject.ToString() + "\r\n");
                    }

                    // Encode the string in HTML (prevent security issue with 'dangerous' caracters like < >
                    htmlResult = Server.HtmlEncode(builder.ToString());
                }
                else
                    htmlResult = "<h3>No results</h3>";
            }
            catch(Exception ex)
            {
                htmlResult = ex.ToString();
            }

            //Response.Write(htmlResult);
            divMainContent.InnerHtml = HttpUtility.HtmlDecode(htmlResult);

        }

        protected void cmbScripts_SelectedIndexChanged(object sender, EventArgs e)
        {
            string scriptPath = Server.MapPath("./PSScripts");
            string scriptFileNameNoExt = System.IO.Path.GetFileNameWithoutExtension(cmbScripts.Text);
            string htmlFile = scriptPath + "\\" + scriptFileNameNoExt + ".html";

            if (File.Exists(htmlFile))
            {
                string html = System.IO.File.ReadAllText(htmlFile);
                divHtmlForm.InnerHtml = html;
            }
            else
                divHtmlForm.InnerHtml = "";
        }

    }
}