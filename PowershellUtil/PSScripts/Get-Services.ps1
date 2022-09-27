Param(
        [parameter(Mandatory=$false)] $ServerName = "localhost"
)

$psutil_html_header = "<style>"
$psutil_html_header = $psutil_html_header + "BODY{font-face:Helvetica; font-size: 16px;}"
$psutil_html_header = $psutil_html_header + "TABLE{border-width: 2px;border-style: solid;border-color: black;border-collapse: collapse;}"
$psutil_html_header = $psutil_html_header + "tr:nth-child(even){ background:#e9e9ff; }"
$psutil_html_header = $psutil_html_header + "TH, TD{padding: 10px;border-style: solid;border-color: black}"
$psutil_html_header = $psutil_html_header + "</style>"

echo "<p>ServerName: $serverName</p>"
get-service -computername $ServerName| select displayname,status,starttype | ConvertTo-HTML -head $psutil_html_header 
