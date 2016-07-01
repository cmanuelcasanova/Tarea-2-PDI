using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms.DataVisualization.Charting;

namespace PDI_Tarea1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        struct Pixel
        {
            public int red;
            public int green;
            public int blue;
        }


        String Ruta, temp;
        Color pixeles;
        Bitmap image, image2;
        PictureBox imgPictureBox, imgPictureBox2;
        int Ini, Ancho, Alto, Cab, Prof, Relleno,num;
        double Angulo, Prom, porc;
        int Valor = 0;
        byte[] bytes;
        Pixel[,] Matriz;
        Pixel[,] Matriz2;

        int[] Reds = new int[256];
        int[] Greens = new int[256];
        int[] Blues = new int[256];



        string tobinary(int x)
        {
            temp = Convert.ToString(x, 2);
            if (temp.Length < 8)
            {
                while (temp.Length < 8)
                {
                    temp = "0" + temp;
                }
            }
            return temp;
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            OpenFileDialog fdlg = new OpenFileDialog();
            fdlg.Title = "seleccionar Imagen!";
            fdlg.InitialDirectory = @"c:\";
            fdlg.Filter = "Imagen BMP (*.bmp)|*.bmp";
            fdlg.FilterIndex = 2;
            fdlg.RestoreDirectory = true;
            if (fdlg.ShowDialog() == DialogResult.OK)
            {
                this.Ruta = fdlg.FileName;


            }





            if (Ruta != null)
            {



                this.panel1.Controls.Clear();

                this.bytes = File.ReadAllBytes(this.Ruta);
                //textBox1.Text = bytes[2].ToString();
                //textBox1.Text = bytes.Length.ToString();
                this.Ini = Convert.ToInt32(tobinary(this.bytes[13]) + tobinary(this.bytes[12]) + tobinary(this.bytes[11]) + tobinary(this.bytes[10]), 2);
                this.Cab = Convert.ToInt32(tobinary(this.bytes[17]) + tobinary(this.bytes[16]) + tobinary(this.bytes[15]) + tobinary(this.bytes[14]), 2);
                this.Ancho = Convert.ToInt32(tobinary(this.bytes[21]) + tobinary(this.bytes[20]) + tobinary(this.bytes[19]) + tobinary(this.bytes[18]), 2);
                this.Alto = Convert.ToInt32(tobinary(this.bytes[25]) + tobinary(this.bytes[24]) + tobinary(this.bytes[23]) + tobinary(this.bytes[22]), 2);
                this.Prof = Convert.ToInt32(tobinary(this.bytes[29]) + tobinary(this.bytes[28]), 2);



                Console.Write("Profundidad " + this.Prof);
                Console.Write("\n");

                int inicio = this.Ini;



                this.Matriz = new Pixel[this.Ancho, this.Alto];
               


                for (int i = 0; i < 4; i++)
                {
                    if (((this.Ancho * 3) + i) % 4 == 0)
                    {
                        this.Relleno = i; break;
                    }
                }



                for (int y = this.Alto - 1; y >= 0; y--)
                {
                    for (int x = 0; x < this.Ancho; x++)
                    {

                        this.Matriz[x, y].red = this.bytes[inicio + 2];
                        this.Matriz[x, y].green = this.bytes[inicio + 1];
                        this.Matriz[x, y].blue = this.bytes[inicio];
                        inicio = inicio + 3;

                    }
                    inicio = inicio + this.Relleno;
                }

                

                this.image = new Bitmap(this.Ancho, this.Alto);
                this.imgPictureBox = new PictureBox();
                imgPictureBox.SizeMode = PictureBoxSizeMode.AutoSize;

                for (int y = 0; y < this.Alto; y++)
                    for (int x = 0; x < this.Ancho; x++)
                    {
                        pixeles = Color.FromArgb(this.Matriz[x, y].red, this.Matriz[x, y].green, this.Matriz[x, y].blue);

                        image.SetPixel(x, y, pixeles);
                    }
                imgPictureBox.Image = image;

                this.panel1.AutoScroll = true;
                this.panel1.Controls.Add(imgPictureBox);
            }
            else
            {
                MessageBox.Show("Debe Seleccionar una Imagen BMP", "Error Title", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }





        private void button2_Click(object sender, EventArgs e)
        {



            for (int y = 0; y < this.Alto; y++)
            {
                for (int x = 0; x < this.Ancho; x++)
                {

                    if (255 - Matriz[x, y].red < 0) { Matriz[x, y].red = (-1) * (255 - Matriz[x, y].red); } else { Matriz[x, y].red = 255 - Matriz[x, y].red; };
                    if (255 - Matriz[x, y].green < 0) { Matriz[x, y].green = (-1) * (255 - Matriz[x, y].green); } else { Matriz[x, y].green = 255 - Matriz[x, y].green; };
                    if (255 - Matriz[x, y].blue < 0) { Matriz[x, y].blue = (-1) * (255 - Matriz[x, y].blue); } else { Matriz[x, y].blue = 255 - Matriz[x, y].blue; };
                }

            }


            for (int y = 0; y < this.Alto; y++)
                for (int x = 0; x < this.Ancho; x++)
                {
                    pixeles = Color.FromArgb(this.Matriz[x, y].red, this.Matriz[x, y].green, this.Matriz[x, y].blue);

                    image.SetPixel(x, y, pixeles);
                }
            imgPictureBox.Image = image;

            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(imgPictureBox);








        }

        private void button3_Click(object sender, EventArgs e)
        {

            Pixel aux;

            for (int y = 0; y < this.Alto/2; y++)
            {
                for (int x = 0; x < this.Ancho; x++)
                {
                    
                    aux.red = Matriz[x, y].red;
                    aux.green = Matriz[x, y].green;
                    aux.blue = Matriz[x, y].blue;

                    this.Matriz[x,y].red = Matriz[x, this.Alto - y-1].red;
                    this.Matriz[x,y].green = Matriz[x, this.Alto - y-1].green;
                    this.Matriz[x,y].blue = Matriz[x, this.Alto - y-1].blue;


                    this.Matriz[x, this.Alto - y-1].red = aux.red;
                    this.Matriz[x, this.Alto - y-1].green = aux.green;
                    this.Matriz[x, this.Alto - y-1].blue = aux.blue;

                }

            }





            for (int y = 0; y < this.Alto; y++)
                for (int x = 0; x < this.Ancho; x++)
                {
                    pixeles = Color.FromArgb(this.Matriz[x, y].red, this.Matriz[x, y].green, this.Matriz[x, y].blue);

                    image.SetPixel(x, y, pixeles);
                }
            imgPictureBox.Image = image;

            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(imgPictureBox);


        }



        private void button4_Click(object sender, EventArgs e)
        {


            Pixel aux;

            for (int y = 0; y < this.Alto; y++)
            {
                for (int x = 0; x < this.Ancho/2; x++)
                {

                    aux.red = Matriz[x, y].red;
                    aux.green = Matriz[x, y].green;
                    aux.blue = Matriz[x, y].blue;

                    this.Matriz[x, y].red = Matriz[this.Ancho - x - 1, y].red;
                    this.Matriz[x, y].green = Matriz[this.Ancho - x - 1,y].green;
                    this.Matriz[x, y].blue = Matriz[this.Ancho - x - 1, y].blue;


                    this.Matriz[this.Ancho - x - 1, y ].red = aux.red;
                    this.Matriz[this.Ancho - x - 1, y ].green = aux.green;
                    this.Matriz[this.Ancho - x - 1, y ].blue = aux.blue;

                }

            }



            for (int y = 0; y < this.Alto; y++)
                for (int x = 0; x < this.Ancho; x++)
                {
                    pixeles = Color.FromArgb(this.Matriz[x, y].red, this.Matriz[x, y].green, this.Matriz[x, y].blue);

                    image.SetPixel(x, y, pixeles);
                }
            imgPictureBox.Image = image;

            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(imgPictureBox);



        }

        private void button5_Click(object sender, EventArgs e)
        {



            this.Matriz2 = new Pixel[this.Alto, this.Ancho];



           for (int y = 0; y < this.Alto; y++)
            {
                for (int x = 0; x < this.Ancho; x++)
                {

                
                    this.Matriz2[this.Alto-1-y,x].red = Matriz[x, y].red;
                    this.Matriz2[this.Alto-1-y,x].green = Matriz[x, y].green;
                    this.Matriz2[this.Alto-1-y,x].blue = Matriz[x, y].blue;



                }

            }

           this.image = new Bitmap(this.Alto, this.Ancho);
            
            for (int y = 0; y < this.Ancho; y++)
                for (int x = 0; x < this.Alto; x++)
                {
                    pixeles = Color.FromArgb(this.Matriz2[x, y].red, this.Matriz2[x, y].green, this.Matriz2[x, y].blue);

                    image.SetPixel(x, y, pixeles);
                }
            imgPictureBox.Image = image;

            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(imgPictureBox);


            this.num = this.Ancho;
            this.Ancho = this.Alto;
            this.Alto = this.num;

            this.Matriz = this.Matriz2;




        }

        private void button6_Click_1(object sender, EventArgs e)
        {

            this.Matriz2 = new Pixel[this.Alto, this.Ancho];

           for (int y = 0; y < this.Alto; y++)
            {
                for (int x = 0; x < this.Ancho; x++)
                {


                    this.Matriz2[y,this.Ancho - 1 - x].red = Matriz[x, y].red;
                    this.Matriz2[y,this.Ancho - 1 - x].green = Matriz[x, y].green;
                    this.Matriz2[y,this.Ancho - 1 - x].blue = Matriz[x, y].blue;



                }

            }

            this.image = new Bitmap(this.Alto, this.Ancho);

            for (int y = 0; y < this.Ancho; y++)
                for (int x = 0; x < this.Alto; x++)
                {
                    pixeles = Color.FromArgb(this.Matriz2[x, y].red, this.Matriz2[x, y].green, this.Matriz2[x, y].blue);

                    image.SetPixel(x, y, pixeles);
                }
            imgPictureBox.Image = image;

            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(imgPictureBox);


            this.num = this.Ancho;
            this.Ancho = this.Alto;
            this.Alto = this.num;

            this.Matriz = this.Matriz2;





        }

    }
}
