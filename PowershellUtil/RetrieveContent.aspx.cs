using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

using System.Management.Automation;
using System.Collections;

namespace PowershellUtil
{
    public partial class RetrieveContent : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            bool postback = IsPostBack;

            string server = Request["server"] != null ? Request["server"] : "";
            string provider = Request["provider"] != null ? Request["provider"] : "";
            string name = Request["name"] != null ? Request["name"] : "";
            string path = Request["path"] != null ? Request["path"] : "";

            if (provider.ToUpper().Equals("DRIVE"))
                BrowseDrive(path, postback);

            if (provider.ToUpper().Equals("REGISTRY"))
                BrowseRegistry(server, name, provider, path);

            if (provider.ToUpper().Equals("CERT"))
                BrowseCertificates(server, name, provider, path);

            if (provider.ToUpper().Equals("ENV"))
                BrowseEnvVars(server, name, provider, path);

            Response.Write("<h3>Invalid command</h3>");

        }

        protected void BrowseDrive(string path, bool isPostBack)
        {
            if(isPostBack)
            {
                if (path.IndexOf('$') != path.Length)
                    path = path.Replace("$", "$\\");
            }

            string scriptCode = "$a = @()\n " +
                                "Get-ChildItem " + path + " | select Mode,LastWriteTime,Length,Name,FullName | foreach { \n" +
                                //"$uncPath = '" + path + "\\\' + $_.Name\n" +
                                "$uncPath = $_.FullName\n" +
                                "$name = $_.Name\n";

            if (!isPostBack)
            {
                scriptCode += "$item = [PSCustomObject] @{ \n" +
                                    "            Type = $_.Mode\n " +
                                    "            Modified = $_.LastWriteTime\n " +
                                    "            Size = $_.Length\n " +
                                    "            Name = \"<a href=`\"$uncPath`\" target=`\"popup`\" onclick=`\"window.open('RetrieveContent.aspx?provider=DRIVE&path=$uncPath','popup','width=600,height=600'); return false;`\">$name</a>\"\n " +
                                    "}\n " +
                                    "$a += $item\n " +
                                    "}\n " +
                                    "$a | ConvertTo-Html";
            }
            else
            {
                scriptCode += "$item = [PSCustomObject] @{ \n" +
                                    "            Type = $_.Mode\n " +
                                    "            Modified = $_.LastWriteTime\n " +
                                    "            Size = $_.Length\n " +
                                    "            Name = \"<a href='$uncPath' target='popup' onclick=`\"window.open('RetrieveContent.aspx?provider=DRIVE&path=$uncPath','popup','width=600,height=600'); return false;`\">$name</a>\"\n " +
                                    "}\n " +
                                    "$a += $item\n " +
                                    "}\n " +
                                    "$a | ConvertTo-Html";
            }

            ExecutePowershell(scriptCode);
        }

        protected void BrowseRegistry(string server, string name, string provider, string path)
        {

        }

        protected void BrowseCertificates(string server, string name, string provider, string path)
        {

        }

        protected void BrowseEnvVars(string server, string name, string provider, string path)
        {

        }

        protected void ExecutePowershell(string scriptCode)
        {
            string htmlResult = "";
            var shell = PowerShell.Create();

            try
            {
                shell.Commands.AddScript(scriptCode);

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
                    //htmlResult = Server.HtmlEncode(builder.ToString());
                    htmlResult = builder.ToString();
                    htmlResult = HttpUtility.HtmlDecode(htmlResult);
                }
                else
                    htmlResult = "<h3>No results</h3>";
            }
            catch (Exception ex)
            {
                htmlResult = ex.ToString();
            }

            Response.Write(htmlResult);
        }
    }
}