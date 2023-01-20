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