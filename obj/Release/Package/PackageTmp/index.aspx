<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="FotoRealTime.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
  
    <head runat="server">
  
        <title></title>
        <%--<script src="Scripts/jquery-1.6.4.min.js"></script>--%>
        <script src="Scripts/jquery-3.0.0.min.js"></script>
        <script src="Scripts/jquery.signalR-2.2.3.min.js"></script>

        <script src="/signalR/hubs"></script>
        <script src="Scripts/jquery-ui-1.12.1.min.js"></script>
        <link href="Content/themes/base/jquery-ui.min.css" rel="stylesheet" />



    
            <meta name="viewport" content="width=device-width, initial-scale=1" />
            <link rel="stylesheet" href="Content/bootstrap.min.css" />

            <script src="Scripts/bootstrap.min.js"></script>

          <script type="text/javascript">

              $(function () {
                
                  //element.setCustomValidity("Vendosni nje Shofer");
                  //Krijon Proxy
                  var job = $.connection.myHubCel;

                  // Deklaron nje funksion
                  job.client.displayStatus = function () {
                      getData();
                      getSh();
                  };

                  // Lidhet me hubin
                  $.connection.hub.start();
                
                  getData();
                  getSh();
                 
                  //var prova = $('#TextBox1').val();
                  $(".kerko").click(function () {
                      getData1();
                  });
              function getData() {
                  var $tbl = $('#tbl');
                  var $tbl1 = $('#tbl1');
                  var $tbl2 = $('#tbl2');
                  var $tblD = $('#tblDetaje');
                  
                //  tbl1.style = "font-family:Georgia, Garamond, Serif;color:white"
            
                  $.ajax({
                      url: 'index.aspx/GetData',
                      contentType: "application/json; charset=utf-8",                 
                      dataType: "json",
                      type: "POST",
                      success: function (data) {
                         debugger;
                          if (data.d.length > 0) {
                              var newdata = data.d;
                         
                              $tbl.empty();
                              $tbl.append(' <tr bgcolor="#428bca" style="color : #f9f9f9"><th>ID</th><th>Data</th><th>Derguesi</th><th>Marresi</th><th></th></tr>');
                   

                              $tbl1.empty();
                              $tbl2.empty();
                              $tbl2.append(' <tr><th>ID</th><th>Klienti</th><th>Adresa</th><th>Statusi</th><th>Shoferi<th>Cmimi</th></tr>');

                              $tbl1.append(' <tr bgcolor="#428bca" style="color : #f9f9f9"><th>ID</th><th>Derguesi</th><th>Adresa</th><th>Shoferi</th><th>Anullo</th></tr>');
                              var rows = [];
                              var rowCount = 1
                           
                              for (var i = 0; i < newdata.length; i++) {
                        
                                  if (newdata[i].DriverIdPickUp == null && newdata[i].pickUp == false) {
                                      var row = '<tr> <td class = "klik" style ="visibility: hidden">' + newdata[i].idOrder + '</td><td  class = "klik">' + newdata[i].EmroDerguesi + '</td><td  class = "klik">' + newdata[i].EmriMarresi + '</td><td  class = "klik">' + newdata[i].adresaMarresi + '</td><td  class = "klik">' + newdata[i].Telefon + '</td><td  class = "klik">' + newdata[i].ZonaEmertimi + '</td><td  class = "klik">' + newdata[i].Pesha + ' ' + 'KG' + '</td><td> <button type="submit" class="andi">Dergo</button></td></tr>';
                                
                                      //rows.push('<tr><td>' + newdata[i].id + '</td><td>' + newdata[i].client + '</td><td>' + newdata[i].adresa + '</td><td>' + newdata[i].statusi + '</td><td>' + newdata[i].driver + '</td><td>' + newdata[i].cmimi + '</td></tr>');

                                    
                                      debugger;
                                    
                                    
                                      //if (newdata[i].KthyerMag == true) {
                                      //    // $tbl.append(row).css({'background' : '#d9534f', 'color' : '#f9f9f9'});
                                      //    //$(tbl.row).css('background', '#d9534f');
                                      //    //$('#tbl tr:eq(' + i + ')').css('background', '#d9534f');
                                      //    //$('#tbl tr:eq(' + i + ')').css('color', '#f9f9f9');
                                      //    row = '<tr bgcolor="#d9534f" style="color : #f9f9f9"><td>' + newdata[i].idOrder + '</td><td>' + newdata[i].EmroDerguesi + '</td><td>' + newdata[i].EmriMarresi + '</td><td>' + newdata[i].adresaMarresi + '</td><td>' + newdata[i].Telefon + '</td><td>' + newdata[i].ZonaEmertimi + '</td><td>' + newdata[i].Pesha + ' ' + 'KG' + '</td><td> <button type="submit" class="andi">Dergo</button></td></tr>';

                                      //}


                                      if ($('#tbl tr').length % 2 == 1) {
                                          row = '<tr class = "klik" bgcolor="#f2f2f2"><td>' + newdata[i].idOrder + '</td><td>' + newdata[i].ora + '</td><td>' + newdata[i].EmroDerguesi + '</td><td>' + newdata[i].adresaMarresi + '<td> <button style = "background-color: #428bca;color: #f9f9f9" type="submit" class="andi">Dergo</button></td>' + '</td><td style=display:none;>' + newdata[i].EmriMarresi + '</td><td style=display:none;>' + newdata[i].Telefon + '</td><td style=display:none;>' + newdata[i].ZonaEmertimi + '</td><td style=display:none;>' + newdata[i].Pesha + ' ' + 'KG' + '</td><td style=display:none;>' + newdata[i].Cmimi + '</td><td style=display:none;>' + newdata[i].Vlera + '</td>' + '</td><td style=display:none;>' + newdata[i].Barcode + '</td>' + '</td><td style=display:none;>' + newdata[i].Shenime + '</td><td style=display:none;>' + newdata[i].Paguan + '</td></tr>';
                                         
                                      }
                                      else
                                      {
                                          row = '<tr class = "klik"><td>' + newdata[i].idOrder + '</td><td>' + newdata[i].ora + '</td><td>' + newdata[i].EmroDerguesi + '</td><td>' + newdata[i].adresaMarresi + '<td> <button style = "background-color: #428bca;color: #f9f9f9" type="submit" class="andi">Dergo</button></td>' + '</td><td style=display:none;>' + newdata[i].EmriMarresi + '</td><td style=display:none;>' + newdata[i].Telefon + '</td><td style=display:none;>' + newdata[i].ZonaEmertimi + '</td><td style=display:none;>' + newdata[i].Pesha + ' ' + 'KG' + '</td><td style=display:none;>' + newdata[i].Cmimi + '</td><td style=display:none;>' + newdata[i].Vlera + '</td>' + '</td><td style=display:none;>' + newdata[i].Barcode + '</td>' + '</td><td style=display:none;>' + newdata[i].Shenime + '</td><td style=display:none;>' + newdata[i].Paguan + '</td></tr>';


                                      }
                                      $tbl.append(row);
                                      continue;
                                  }

                                  if (newdata[i].DriverIdPickUp != null && newdata[i].pickUp == false) {
                                      var row = '<tr> <td>' + newdata[i].idOrder + '</td><td>' + newdata[i].EmroDerguesi + '</td ><td style=display:none;>' + newdata[i].EmriMarresi + '</td><td>' + newdata[i].adresaMarresi + '</td><td style=display:none;>' + newdata[i].Telefon + '</td><td style=display:none;>' + newdata[i].ZonaEmertimi + '</td><td style=display:none;>' + newdata[i].Pesha + ' ' + 'KG' + '</td><td>' + newdata[i].EmriShoferi + '  ' + newdata[i].MbiemriShoferi + '</td><td style=display:none;>' + newdata[i].DriverIdPickUp + '</td><td> <button type="submit" class="andi11">Anullo</button></td></tr>';
                                      //rows.push('<tr><td>' + newdata[i].id + '</td><td>' + newdata[i].client + '</td><td>' + newdata[i].adresa + '</td><td>' + newdata[i].statusi + '</td><td>' + newdata[i].driver + '</td><td>' + newdata[i].cmimi + '</td></tr>');
                               
                                      //$tbl1.append(row);
                                      //rowCount = rowCount + 1;
                                      if (newdata[i].PareKlienti == true)
                                      {
                                        
                                          row = '<tr bgcolor="#d9534f" style="color : #f9f9f9"><td>' + newdata[i].idOrder + '</td><td>' + newdata[i].EmroDerguesi + '</td><td style=display:none;>' + newdata[i].EmriMarresi + '</td><td>' + newdata[i].adresaMarresi + '</td><td style=display:none;>' + newdata[i].Telefon + '</td><td style=display:none;>' + newdata[i].ZonaEmertimi + '</td><td style=display:none;>' + newdata[i].Pesha + ' ' + 'KG' + '</td><td>' + newdata[i].EmriShoferi + '  ' + newdata[i].MbiemriShoferi + '</td><td> </td></tr>';

                                      }
                                      else
                                      {

                                          row = '<tr bgcolor="#5cb85c" style="color : #f9f9f9"><td>' + newdata[i].idOrder + '</td><td>' + newdata[i].EmroDerguesi + '</td><td style=display:none;>' + newdata[i].EmriMarresi + '</td><td >' + newdata[i].adresaMarresi + '</td><td style=display:none;>' + newdata[i].Telefon + '</td><td style=display:none;>' + newdata[i].ZonaEmertimi + '</td><td style=display:none;>' + newdata[i].Pesha + ' ' + 'KG' + '</td><td>' + newdata[i].EmriShoferi + '  ' + newdata[i].MbiemriShoferi + '</td><td> <button style = "background-color: #428bca;color: #f9f9f9" type="submit" class="andi11">Anullo</button></td></tr>';

                                      }

                                      $tbl1.append(row);

                                      continue;

                                  }
                           
                              };
                              $(".andi").click(function () {
                                  if (document.getElementById("DropDownList1").value != "-1") {

                                   
                                          var $row = $(this).closest("tr");    // Find the row
                                          var $text = $row.find('td:eq(0)').text(); // Find the text
                                         
                                          var $driver = $('#DropDownList1').val();

                                          document.getElementById("DropDownList1").selectedIndex = "-1";                                      
                                      
                                      //alert($.connection.hub.state);

                                     
                                      $.ajax({
                                          url: 'index.aspx/Update',
                                          data: "{ id: " + $text + ",driver: " + $driver + "}",
                                          contentType: "application/json; charset=utf-8",
                                          dataType: "json",
                                          type: "POST"

                                      });
                                  }
                                  else
                                      alert("Zgjidhni shoferin !");

                              });

                              $(".andi11").click(function () {
                                  debugger;
                                      var $row = $(this).closest("tr");    // Find the row
                                      var $text = $row.find('td:eq(0)').text(); // Find the text
                                      //var $driver = $('#DropDownList1').val();
                                      document.getElementById("DropDownList1").selectedIndex = "-1";
                                      //document.getElementById("usr").value = "-1";




                                      $.ajax({
                                          url: 'index.aspx/Update11',
                                          data: "{ id: " + $text + "}",
                                          contentType: "application/json; charset=utf-8",
                                          dataType: "json",
                                          type: "POST"

                                      });

                                 
                              });


                              $("tr.klik").click(function () {
                                  var tableData = $(this).children("td").map(function () {
                                      return $(this).text();
                                  }).get();
                               
                                  $(this).addClass('highlight').siblings().removeClass('highlight');
                                  debugger;


                                  $tblD.empty();
                                  $tblD.append('<tr><td style = "color:#428bca;  font-weight :bold">ID : </td><td style = "color:#d9534f;  font-weight :bold">' + $.trim(tableData[0]) + '</td></tr>');
                                  $tblD.append('<tr><td style = "color:#428bca;  font-weight :bold">Data : </td><td style = "color:#d9534f;  font-weight :bold">' + $.trim(tableData[1]) + '</td></tr>');
                                  $tblD.append('<tr><td style = "color:#428bca;  font-weight :bold">Klienti : </td><td style = "color:#d9534f;  font-weight :bold">' + $.trim(tableData[2]) + '</td></tr>');
                                  $tblD.append('<tr><td style = "color:#428bca;  font-weight :bold">Adresa : </td><td style = "color:#d9534f;  font-weight :bold">' + $.trim(tableData[3]) + '</td></tr>');
                                  $tblD.append('<tr><td style = "color:#428bca;  font-weight :bold">Marresi : </td><td style = "color:#d9534f;  font-weight :bold">' + $.trim(tableData[5]) + '</td></tr>');
                                  $tblD.append('<tr><td style = "color:#428bca;  font-weight :bold">Telefon : </td><td style = "color:#d9534f;  font-weight :bold">' + $.trim(tableData[6]) + '</td></tr>');
                                  $tblD.append('<tr><td style = "color:#428bca;  font-weight :bold">Zona : </td><td style = "color:#d9534f;  font-weight :bold">' + $.trim(tableData[7]) + '</td></tr>');
                                  $tblD.append('<tr><td style = "color:#428bca;  font-weight :bold">Pesha : </td><td style = "color:#d9534f;  font-weight :bold">' + $.trim(tableData[8]) + '</td></tr>');
                                  $tblD.append('<tr><td style = "color:#428bca;  font-weight :bold">Cmimi : </td><td style = "color:#d9534f;  font-weight :bold">' + $.trim(tableData[9]) + ' LEK</td></tr>');
                                  $tblD.append('<tr><td style = "color:#428bca;  font-weight :bold">Paguan : </td><td style = "color:#d9534f;  font-weight :bold">' + $.trim(tableData[13]) + '</td></tr>');

                                  $tblD.append('<tr><td style = "color:#428bca;  font-weight :bold">Vlera : </td><td style = "color:#d9534f;  font-weight :bold">' + $.trim(tableData[10]) + ' LEK</td></tr>');
                                  $tblD.append('<tr><td style = "color:#428bca;  font-weight :bold">Barcode : </td><td style = "color:#d9534f;  font-weight :bold">' + $.trim(tableData[11]) + '</td></tr>');
                                  $tblD.append('<tr><td style = "color:#428bca;  font-weight :bold">Shenime : </td><td style = "color:#d9534f;  font-weight :bold">' + $.trim(tableData[12]) + '</td></tr>');

                              });

                           
                          }
                      }
                  });

                  function ToJavaScriptDate(value) {
                      var pattern = /Date\(([^)]+)\)/;
                      var results = pattern.exec(value);
                      var dt = new Date(parseFloat(results[1]));
                      return dt.getDate() + "/" + (dt.getMonth() + 1) + "/" + dt.getFullYear();
                  }
              }


              function getSh()
              {
                  debugger;
                 var $tbl3 = $('#tbl3');
                 
                  //$tbl3.append(' <tr><th>Shoferet</th><th>P</th><th>D</th></tr>');

                  $.ajax({
                      url: 'index.aspx/getShoferet',                     
                      contentType: "application/json; charset=utf-8",
                      dataType: "json",
                      type: "POST",
                      success: function (data)
                      {
                          debugger;
                          if (data.d.length > 0)
                          {
                              $tbl3.empty();
                              var newdata1 = data.d;
                              // to do krijimi i tabeles dhe vendosja e te dhenave te shoferave
                              for (var i = 0; i < newdata1.length; i++)
                              {
                                  var row = '<tr style = "color:#428bca;  font-weight :bold"> <td>' + newdata1[i].emri + '</td><td style = "color:#d9534f;  font-weight :bold">' + newdata1[i].pickup + '</td><td style = "color:#5cb85c;  font-weight :bold">' + newdata1[i].dorezuar + '</td><td></tr>';
                                  $tbl3.append(row);

                              }
                          }
                      }
                  });
              }

              });
        </script>

    <script>

        function GetSelectedItem(el) {
            var e = document.getElementById(el);
            //var strSel = "The Value is: " + e.options[e.selectedIndex].value + " and text is: " + e.options[e.selectedIndex].text;
            document.getElementById('usr').value = e.options[e.selectedIndex].value;
        
        }
    </script>


        <script>
            $(document).ready(function () {
                $("#TextBox1").datepicker({
                    monthNames: ['Janar', 'Shkurt', 'Mars', 'Prill', 'Maj', 'Qershor',
                    'Korrik', 'Gusht', 'Shtator', 'Tetor', 'Nentor', 'Dhjetor'],
                    monthNamesShort: ['Jan', 'Shk', 'Mar', 'Pri', 'Maj', 'Qer',
                            'Kor', 'Gus', 'Sht', 'Tet', 'Nen', 'Dhj'],
                    dayNames: ['Djele', 'Hene', 'Marte', 'Mekure', 'Enjte', 'Premte', 'Shtune'],
                    dayNamesShort: ['So', 'Mo', 'Di', 'Mi', 'Do', 'Fr', 'Sa'],
                    dayNamesMin: ['Dj', 'He', 'Ma', 'Me', 'En', 'Pr', 'Sh'],
                    weekHeader: 'Java',
                    dateFormat: 'mm.dd.yy'
                });
            });
        </script>






        <%--************************************************************--%>    
    </head>



<body onload="onload()">
    


    <form id="form1" runat="server">
        <meta charset="utf-8" />

       


        <style type="text/css">
.highlight
{

     background-color: #d9534f;
     color : #f9f9f9;
}

</style>







        
         
        <div class="container-fluid">
            <div class=" row">
                <div class="col-md-2">
                    <br />
                     <div class="table-responsive">
                    <table  style ="margin-left : 3px" class="table" id="tbl3"></table> 
                         </div>
                 </div>
               
                
                
                
                
                <div class="col-md-6">
                 <div class=" row">
                            <div class="col-md-12">
                                
                                <div class="table-responsive" >
                                    <table  style ="margin-left : 3px" class="table" id="tbl1"></table>
                                </div>
                            </div>
                 </div>
                        <div class=" row">
                        <div class="col-md-12">
                            <h3 class="text-center text-danger" >
                               
                                Dergesat e Klienteve</h3>
                                          <div> <asp:DropDownList ID="DropDownList1" runat="server" onClick="GetSelectedItem('DropDownList1');" Font-Bold="True" Font-Size="Medium"></asp:DropDownList></div>

                            <div class="table-responsive">
                                <table style ="margin-left : 3px" class="table" id="tbl"></table>
                            </div>
                        </div>



                       
                    </div>
                </div>
                
                <div class="col-md-4">
            <br />
                      <div class="table-responsive">
                                <table style ="margin-left : 30px;position:sticky; font-size:large" id="tblDetaje"></table>
                            </div>
                  
                      
                     
                 

                </div>

            </div>
             <asp:TextBox ID="usr" runat="server" Visible="False"></asp:TextBox>
        </div>

    </form>
    </body>
    </html>

