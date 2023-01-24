<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="WebApiTest2.wwwroot.index" %>

<!DOCTYPE html>

<script runat="server"> 


</script>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>DemoTestPage</title>
    <style>        
        #downstage {
                height: 20px; /* Высота слоя */
                background: #FEDFC0;                 
                bottom: 0;
        }
        .Block1 {
            left: 10vw;
        }
        .table1 {                       
            left: 10vw;
            overflow: scroll;
            height: 85vh;
            width: 25vw;
        }
        .ListBox1Dv {
            position: absolute;
            top:10vh;
            left:42vw;
        }
        #ListBox1 {
            width: 22vw;
            height: 30vh;
        }
        .ListBoxButtons {
            position: absolute;
            top:41vh;
            left:51vw;
        }
        .table2 {
            position: absolute;
            top: 10vh;
            left: 26vw;
            overflow: scroll;
            height: 35vh;
            width: 15vw;
        }
        .EXPORTJSON {
            position: absolute;
            top: 46vh;
            left: 27vw;
            
        }
        #DropDownList1 {
            position: inherit;
            top: 4vh;
            left: 0vw;
        }
        #FileNameSort {
            position: inherit;
            top: 8vh;
            left: 0vw;
        }
        #HelpText {
            position: inherit;
            top: 12vh;
            left: 0vw;
        }
        #HelpTextTime{
            position: inherit;
            top: 16vh;
            left: 0vw;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <h3 class="h3text" align="center">
            
            Table Example, constructed programmatically</h3>
        <div class="Block1" >
            <div class="table1" align="left"> 
                <asp:Table id="Table1" 
                     GridLines="Both" 
                     HorizontalAlign="Center" 
                     Font-Names="Verdana" 
                     Font-Size="8pt" 
                     CellPadding="15" 
                     CellSpacing="0" 
                     Runat="server"/>  
            </div>
            <div class="table2" alight="left">
                <asp:Table id="Table2" 
                     GridLines="Both" 
                     HorizontalAlign="Center" 
                     Font-Names="Verdana" 
                     Font-Size="8pt" 
                     CellPadding="15" 
                     CellSpacing="0" 
                     Runat="server"/>
                
            </div>
            <div class="EXPORTJSON" alight="left">
                <asp:Button ID="ExportJsonButton" runat="server" Text="Export Json" OnClick="ExportJsonButton_Click" />
                <asp:Label ID="xportinfo" runat="server" Text="file_name"></asp:Label>
                <asp:DropDownList ID="DropDownList1" runat="server">
                    <asp:ListItem Value="0">По имени файла</asp:ListItem>
                    <asp:ListItem Value="1">По времени запуска первой операции </asp:ListItem>
                    <asp:ListItem Value="2">По среднему показателю </asp:ListItem>
                    <asp:ListItem Value="3">По среднему времени </asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="FileNameSort" runat="server"></asp:TextBox>
                <asp:Label ID="HelpText" runat="server" Text="Имя файла/левая и правая граница"></asp:Label>
                <asp:Label ID="HelpTextTime" runat="server" Text="TimeFormat: 2021-01-31;2024-01-01"></asp:Label>
            </div>
            <div class="ListBox1Dv">                
                <asp:ListBox ID="ListBox1"                     
                    runat="server"></asp:ListBox>                      
            </div>             
            <div class ="ListBoxButtons">
                <asp:Button ID="ButtonListboxAdd" runat="server" Text="Add" OnClick="ButtonListboxAdd_Click" />
                <asp:Button ID="ButtonListboxDel" runat="server" Text="Del" OnClick="ButtonListboxDel_Click"/>
            </div>
        </div>

        <p align="center">
            <asp:FileUpload ID="FileUpload1" runat="server" />
            <asp:Button ID="UploadButton" runat="server" Text="Upload" OnClick="UploadDataButton_Click" Width="57px" />
            <asp:Label ID="LabelTest" runat="server" Text="Label"></asp:Label>
        </p>
        <div id = "downstage">
            <p align="center" style="color: #000000; font-family: 'Times New Roman', Times, serif; font-size: 14px; text-decoration: blink;">
                designed by letby3</p>
        </div>        
        
        
        
    </form>
</body>
</html>