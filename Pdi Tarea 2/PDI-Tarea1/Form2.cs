using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace PDI_Tarea1
{
    public partial class Form2 : Form
    {
        public Form2(int[] Reds, int[] Greens, int[] Blues)
        {
            InitializeComponent();




            Series series1 = this.chart1.Series.Add("Canal Rojo");
            Series series2 = this.chart2.Series.Add("Canal Verde");
            Series series3 = this.chart3.Series.Add("Canal Azul");
            this.chart1.Palette = ChartColorPalette.None;
            this.chart1.PaletteCustomColors = new Color[] { Color.Red };
            this.chart2.Palette = ChartColorPalette.None;
            this.chart2.PaletteCustomColors = new Color[] { Color.Green };
            this.chart3.Palette = ChartColorPalette.None;
            this.chart3.PaletteCustomColors = new Color[] { Color.Blue };

            for (int i = 0; i < 256; i++)
            {
                series1.Points.Add(Reds[i]);
                series2.Points.Add(Greens[i]);
                series3.Points.Add(Blues[i]);


            }



            
        }

        public Form1 frm1;

        public void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
