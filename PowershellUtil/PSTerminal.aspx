<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PSTerminal.aspx.cs" Inherits="PowershellUtil.PSTerminal" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>PSUtil Terminal</title>

    <script src="https://code.jquery.com/jquery-3.3.1.min.js"></script>
    <script src="resources/js/jquery.terminal.js"></script>    
    <link rel="stylesheet" href="resources/js/jquery.terminal.min.css" />

</head>
<body>
    <template id="greetings">[[;gray;]
    ____  _____  __  __ __   _   __    ______                        _                  __
   / __ \/ ___/ / / / // /  (_) / /   /_  __/___   _____ ____ ___   (_) ____   ____ _  / /
  / /_/ /\__ \ / / / // __// / / /     / /  / _ \ / ___// __ `__ \ / / / __ \ / __ `/ / / 
 / ____/ __/ // /_/ // /_ / / / /     / /  /  __// /   / / / / / // / / / / // /_/ / / /  
/_/    /____/ \____/ \__//_/ /_/     /_/   \___//_/   /_/ /_/ /_//_/ /_/ /_/ \__,_/ /_/
PSUtil Terminal version {TERMINAL_VERSION} Copyright (c) 2011 - present Darron Nesbitt All rights reserved.]

Type [[;red;]exit] to close the terminal.
    </template>
    </font>

    <form id="frmMain" runat="server">
        <div>
        </div>
    </form>
    <script>
        $('body').terminal("PSUtilTerminalService.aspx", {
            greetings: greetings.innerHTML.replace("{TERMINAL_VERSION}", "[[;white;]<%=TerminalVersion%>]"),
            prompt: "[[;white;]PSUtil> ]"
            });

    </script>
</body>
</html>
