<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Frm_Pruebas.aspx.vb" Inherits="Metrologia.Frm_Pruebas2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
    <script type="text/javascript">
      google.charts.load('current', {'packages':['bar']});
      google.charts.setOnLoadCallback(drawStuff);

      function drawStuff() {
        var data = new google.visualization.arrayToDataTable([
          ['Galaxy', 'Distance', 'Carga'],
          ['POS 1', 1, 1],
          ['POS 2', 1, 1],
          ['PSO 3', 1,1],
          ['POS 4', 1, 0.88],
          ['POS 5', 1, 1]
        ]);

        var options = {
          width: 800,
          chart: {
            title: 'Nearby galaxies',
            subtitle: 'distance on the left, brightness on the right'
          },
          bars: 'horizontal', // Required for Material Bar Charts.
          series: {
            0: { axis: 'distance' }, // Bind series 0 to an axis named 'distance'.
            1: { axis: 'brightness' } // Bind series 1 to an axis named 'brightness'.
          },
          axes: {
            x: {
              distance: {label: 'parsecs'}, // Bottom x-axis.
              brightness: {side: 'top', label: 'apparent magnitude'} // Top x-axis.
            }
          }
        };

      var chart = new google.charts.Bar(document.getElementById('dual_x_div'));
      chart.draw(data, options);
    };
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
               <div id="dual_x_div" style="width: 900px; height: 500px;"></div>
        </div>
    </form>
</body>
</html>
