<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="FotoRealTime.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <style>
          #bb {
              /*background-image: url("~/css/background1.jpg");*/
             background-color : whitesmoke;
            
          }  
    </style>   
            <div   style="text-align: center";  >
           <div  style="width: 300px; margin-left: auto; margin-right:auto;">
            <br /><br /><br /><br /><br /><br />
             <h2  style="color:#5895B6;" >ADEX DISPECHER</h2>
                <asp:Panel ID="Panel2" runat="server" BorderStyle="Double" BackColor="#6D6E71"> <br />
               <asp:Label ID="Label1" runat="server" Text="PERDORUESI" Font-Size="Large" Font-Bold="False" ForeColor="White"></asp:Label>   <br />
                 <asp:TextBox ID="TextBox1" runat="server" Font-Size="Medium"></asp:TextBox>
               
                 <br />
                   <asp:Label ID="Label4" runat="server" Text="FJALEKALIMI" Font-Size="Large" Font-Bold="False" ForeColor="White"></asp:Label>
                 <br />
                 <asp:TextBox ID="TextBox2" runat="server" Font-Size="Medium" TextMode="Password"></asp:TextBox> <br /><br />
                 <asp:Button ID="Button2" runat="server" OnClick="Button1_Click" Text="HYR" Width="127px" Font-Bold="True" Font-Size="Medium" BackColor="#5895B6" />
                   <br /><br />
               </asp:Panel>
             
               
    </div>
                </div>
    </form>
</body>
</html>
