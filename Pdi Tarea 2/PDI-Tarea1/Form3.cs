using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;




namespace PDI_Tarea1
{
    public partial class Form3 : Form
    {
        public Form3(PictureBox PB)
        {
            InitializeComponent();


            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(PB);


           /* Bitmap BM;
            PictureBox PB;

            Color pixeles;
            
            BM = new Bitmap(int An, int Al);
            PB = new PictureBox();
            PB.SizeMode = PictureBoxSizeMode.AutoSize;

            for (int y = 0; y < Al; y++)
                for (int x = 0; x < An; x++)
                {
                    pixeles = Color.FromArgb(this.Matriz[x, y].red, this.Matriz[x, y].green, this.Matriz[x, y].blue);

                    image.SetPixel(x, y, pixeles);
                }
            imgPictureBox.Image = image;

            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(imgPictureBox);
            */


        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }
    }
}
