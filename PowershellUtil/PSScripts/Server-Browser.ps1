Param(
        [parameter(Mandatory=$false)] $ServerName = "localhost"
)

Add-Type -AssemblyName System.Web

#$psutil_html_header = ""
$psutil_html_header = "<style>"
$psutil_html_header = $psutil_html_header + "BODY{font-face:Helvetica; font-size: 16px;}"
$psutil_html_header = $psutil_html_header + "TABLE{border-width: 2px;border-style: solid;border-color: black;border-collapse: collapse;}"
$psutil_html_header = $psutil_html_header + "tr:nth-child(even){ background:#e9e9ff; }"
$psutil_html_header = $psutil_html_header + "TH, TD{padding: 10px;border-style: solid;border-color: black}"
$psutil_html_header = $psutil_html_header + "</style>"

echo "<p>ServerName: $serverName</p>"

$a = @()
gwmi -computername $ServerName -class win32_logicaldisk | foreach {

    $letter = [string]$_.DeviceID.Replace(':','')

    $item = [PSCustomObject] @{
	    Drive = "<a href=`"#`" target=`"popup`" onclick=`"window.open('RetrieveContent.aspx?provider=DRIVE&path=\\\\$ServerName\\$letter`$','popup','width=600,height=600'); return false;`">$letter</a>"
	    FreeSpace = [string]([math]::round($_.FreeSpace/1GB, 2)) + " GB"
	    Size = [string]([math]::round($_.size/1GB, 2)) + " GB" 
    }

    $a += $item
}

$html = $a | ConvertTo-Html -fragment
[System.Web.HttpUtility]::HtmlDecode($html)| out-file -filepath "C:\\Code\\DotNET\\2017\\CSharp\\PowershellUtil\\PowershellUtil\\PSScripts\\Helpers\\test.html"
[System.Web.HttpUtility]::HtmlDecode($html) 
#.Replace('</html>','').Replace('<body','').Replace('</body>','')
