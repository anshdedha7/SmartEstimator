<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="MLPOCUtility.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Smart Estimation</title>
    <link href="Content/Main.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div id="TitleLabel">
        <asp:Label ID="Label1" runat="server" Text="Smart Estimator"></asp:Label>
    </div>

        <div class="container">
                <asp:Label ID="Label7" runat="server" Text="Make"></asp:Label>
                <asp:DropDownList ID="DropDownList5" runat="server">
                    <asp:ListItem>Toyota</asp:ListItem>
                </asp:DropDownList>
                <asp:Label ID="Label8" runat="server" Text="Model"></asp:Label>
                <asp:DropDownList ID="DropDownList6" runat="server">
                    <asp:ListItem>Innova</asp:ListItem>
                    <asp:ListItem>Fortuner</asp:ListItem>
                </asp:DropDownList>
                <asp:Label ID="Label2" runat="server" Text="Condition"></asp:Label>
                    <asp:DropDownList ID="DropDownList1" runat="server" >
                        <asp:ListItem>Select Value</asp:ListItem>
                        <asp:ListItem Value="1">EXCELLENT</asp:ListItem>
                        <asp:ListItem Value="2">GOOD</asp:ListItem>
                        <asp:ListItem Value="3">POOR</asp:ListItem>
                        <asp:ListItem Value="4">VERY POOR</asp:ListItem>
                        <asp:ListItem Value="5">TOTAL LOSS</asp:ListItem>
                    </asp:DropDownList>
  
                 <asp:Label ID="Label3" runat="server" Text="Fluid Leaks"></asp:Label>
                    <asp:DropDownList ID="DropDownList2" runat="server" >
                         <asp:ListItem>Select Value</asp:ListItem>
                         <asp:ListItem>YES</asp:ListItem>
                         <asp:ListItem>NO</asp:ListItem>
                    </asp:DropDownList>

                <asp:Label ID="Label4" runat="server" Text="Drivability" ></asp:Label>
                    <asp:DropDownList ID="DropDownList3" runat="server"  >
                        <asp:ListItem>Select Value</asp:ListItem>
                        <asp:ListItem>Drivable</asp:ListItem>
                        <asp:ListItem>Not drivable</asp:ListItem>
                        <asp:ListItem>Not drivable but driven in</asp:ListItem>
                    </asp:DropDownList>

                <asp:Label ID="Label5" runat="server" Text="Point of impact"></asp:Label>
                    <asp:DropDownList ID="DropDownList4" runat="server"  >
                        <asp:ListItem>Select Value</asp:ListItem>
                        <asp:ListItem>FRONT</asp:ListItem>
                        <asp:ListItem>BACK</asp:ListItem>
                        <asp:ListItem>SIDE</asp:ListItem>
                        <asp:ListItem>DIAGNOL</asp:ListItem>
                        <asp:ListItem>TOP</asp:ListItem>
                    </asp:DropDownList>
      
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Submit" />
        <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Reset" />
           
        <div id="ListboxContent">    
            <asp:Label ID="Label6" runat="server" Text="Note:Parts are arranged in order of there decreasing probability of being damaged." Visible="False"></asp:Label>  
             <asp:ListBox ID="ListBox1" runat="server" Visible="False" AutoPostBack="True" OnSelectedIndexChanged="ListBox1_SelectedIndexChanged" ></asp:ListBox>
            <asp:ListBox ID="ListBox2" runat="server" OnSelectedIndexChanged="ListBox1_SelectedIndexChanged" ViewStateMode="Disabled" Visible="False"></asp:ListBox>
         </div>    
        </div>
    </form>
</body>
</html>
