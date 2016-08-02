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


        String Ruta,temp;
        Bitmap image,image2;
        PictureBox imgPictureBox,imgPictureBox2;
        int Ini, Ancho, Alto, Cab, Prof, Relleno;
        int Ang = 0;
        long tam;
        double Angulo,Prom,porc,siz;
        int Valor = 0;
        byte[] bytes;
        Pixel[,] Matriz;
        Pixel[,] Matriz2;
        Pixel[,] Matriz3;
        Pixel[,] Matriz4;

        int[] Reds = new int[256];
        int[] Greens = new int[256];
        int[] Blues = new int[256];

       

        string tobinary (int x){
              temp=Convert.ToString(x, 2);
              if (temp.Length < 8) {
                  while (temp.Length <8){
                      temp = "0" + temp;}  }
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





            if (Ruta!=null)
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
                this.tam = Convert.ToInt64(tobinary(this.bytes[37]) + tobinary(this.bytes[36]) + tobinary(this.bytes[35]) + tobinary(this.bytes[34]), 2);

                  int inicio=this.Ini;

             
             
                this.Matriz = new Pixel[this.Ancho,this.Alto];
                this.Matriz2 = new Pixel[this.Ancho, this.Alto];
                this.Matriz3 = new Pixel[this.Ancho, this.Alto];


                for (int i = 0; i < 4; i++) {
                    if (((this.Ancho*3)+i) % 4 == 0){
                        this.Relleno = i; break;
                    }
                }

                

                for (int y = this.Alto-1; y >=0; y--){
                    for (int x=0; x<this.Ancho; x++)
                    {

                        this.Matriz[x, y].red = this.bytes[inicio + 2];
                        this.Matriz[x, y].green = this.bytes[inicio + 1];
                        this.Matriz[x, y].blue = this.bytes[inicio];
                        inicio=inicio+3;
                     
                    }
                    inicio = inicio + this.Relleno;
                 }

                Color pixeles;

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




                this.Matriz2 = (Pixel[,])this.Matriz.Clone();



                this.image2 = new Bitmap(this.Ancho, this.Alto);
                this.imgPictureBox2 = new PictureBox();
                this.imgPictureBox2.SizeMode = PictureBoxSizeMode.AutoSize;

                for (int y = 0; y < this.Alto; y++)
                    for (int x = 0; x < this.Ancho; x++)
                    {
                        pixeles = Color.FromArgb(this.Matriz2[x, y].red, this.Matriz2[x, y].green, this.Matriz2[x, y].blue);

                        this.image2.SetPixel(x, y, pixeles);
                    }
                this.imgPictureBox2.Image = this.image2;

                this.panel2.Controls.Clear();
                this.panel2.AutoScroll = true;
                this.panel2.Controls.Add(this.imgPictureBox2);

                
                 

            }
            else
            {
                MessageBox.Show("Debe Seleccionar una Imagen BMP", "Error Title", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            this.siz= (Convert.ToDouble(this.tam)/1024)/1024;


            label9.Text = "Resolucion: " + this.Ancho + " x " + this.Alto + " / Prof: " + this.Prof + " / Tamaño: " + Math.Round(this.siz, 2)+" MB";

            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {


    
            for (int i = 0; i < 256; i++) {
                this.Reds[i] = 0; this.Greens[i] = 0; this.Blues[i] = 0;
            }

           

            for (int y = 0; y < this.Alto; y++)
            {
                for (int x = 0; x < this.Ancho; x++)
                {


                    this.Reds[this.Matriz2[x, y].red] = this.Reds[this.Matriz2[x, y].red] + 1;
                    this.Greens[this.Matriz2[x, y].green] = this.Greens[this.Matriz2[x, y].green] + 1;
                    this.Blues[this.Matriz2[x, y].blue] = this.Blues[this.Matriz2[x, y].blue] + 1;
                }

            }

            Form2 H = new Form2(this.Reds, this.Greens, this.Blues); 
            H.Show();


        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            

            try
            {

                this.Valor = this.Valor + 10;
             
                

                for (int y = 0; y < this.Alto; y++)
                {
                    for (int x = 0; x < this.Ancho; x++)
                    {

                        this.Matriz2[x, y].red = this.Matriz2[x, y].red + this.Valor; if (Matriz2[x, y].red > 255) { Matriz2[x, y].red = 255; } else { if (Matriz2[x, y].red <0) { Matriz2[x, y].red = 0; } };
                        this.Matriz2[x, y].green = this.Matriz2[x, y].green + this.Valor; if (Matriz2[x, y].green > 255) { Matriz2[x, y].green = 255; } else { if (Matriz2[x, y].green < 0) { Matriz2[x, y].green = 0; } };
                        this.Matriz2[x, y].blue = this.Matriz2[x, y].blue + this.Valor; if (Matriz2[x, y].blue > 255) { Matriz2[x, y].blue = 255; } else { if (Matriz2[x, y].blue < 0) { Matriz2[x, y].blue = 0; } };
                    }

                }

               
                Color pixeles;

                this.image2 = new Bitmap(this.Ancho, this.Alto);
                this.imgPictureBox2 = new PictureBox();
                this.imgPictureBox2.SizeMode = PictureBoxSizeMode.AutoSize;

                for (int y = 0; y < this.Alto; y++)
                    for (int x = 0; x < this.Ancho; x++)
                    {
                        pixeles = Color.FromArgb(this.Matriz2[x, y].red, this.Matriz2[x, y].green, this.Matriz2[x, y].blue);

                        this.image2.SetPixel(x, y, pixeles);
                    }
                this.imgPictureBox2.Image = this.image2;

                this.panel2.Controls.Clear();
                this.panel2.AutoScroll = true;
                this.panel2.Controls.Add(this.imgPictureBox2);


            }catch(Exception){

                MessageBox.Show("Debe Ingresar un Valor Valido", "Error Title", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            
            }







        }

        private void button4_Click(object sender, EventArgs e)
        {

            try
            {

              
                    this.Valor = this.Valor - 10;
              
                



                for (int y = 0; y < this.Alto; y++)
                {
                    for (int x = 0; x < this.Ancho; x++)
                    {

                        this.Matriz2[x, y].red = this.Matriz2[x, y].red + this.Valor; if (Matriz2[x, y].red > 255) { Matriz2[x, y].red = 255; } else { if (Matriz2[x, y].red < 0) { Matriz2[x, y].red = 0; } };
                        this.Matriz2[x, y].green = this.Matriz2[x, y].green + this.Valor; if (Matriz2[x, y].green > 255) { Matriz2[x, y].green = 255; } else { if (Matriz2[x, y].green < 0) { Matriz2[x, y].green = 0; } };
                        this.Matriz2[x, y].blue = this.Matriz2[x, y].blue + this.Valor; if (Matriz2[x, y].blue > 255) { Matriz2[x, y].blue = 255; } else { if (Matriz2[x, y].blue < 0) { Matriz2[x, y].blue = 0; } };
                   


                        }

                }

               

                Color pixeles;

                this.image2 = new Bitmap(this.Ancho, this.Alto);
                this.imgPictureBox2 = new PictureBox();
                this.imgPictureBox2.SizeMode = PictureBoxSizeMode.AutoSize;

                for (int y = 0; y < this.Alto; y++)
                    for (int x = 0; x < this.Ancho; x++)
                    {
                        pixeles = Color.FromArgb(this.Matriz2[x, y].red, this.Matriz2[x, y].green, this.Matriz2[x, y].blue);

                        this.image2.SetPixel(x, y, pixeles);
                    }
                this.imgPictureBox2.Image = this.image2;

                this.panel2.Controls.Clear();
                this.panel2.AutoScroll = true;
                this.panel2.Controls.Add(this.imgPictureBox2);



            }
            catch (Exception)
            {

                MessageBox.Show("Debe Ingresar un Valor Valido", "Error Title", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }






        }

       



        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {


            for (int y = 0; y < this.Alto; y++)
            {
                for (int x = 0; x < this.Ancho; x++)
                {

                    if (255-Matriz2[x, y].red  < 0) { Matriz2[x, y].red = (-1) * (255 - Matriz2[x, y].red); } else { Matriz2[x, y].red= 255 - Matriz2[x, y].red; };
                    if (255 - Matriz2[x, y].green < 0) { Matriz2[x, y].green = (-1) * (255 - Matriz2[x, y].green); } else { Matriz2[x, y].green = 255 - Matriz2[x, y].green; };
                    if (255 - Matriz2[x, y].blue < 0) { Matriz2[x, y].blue = (-1) * (255 - Matriz2[x, y].blue); } else { Matriz2[x, y].blue = 255 - Matriz2[x, y].blue; };
                }

            }

            //despues.Text = " Datos" + this.Matriz2[0, 0].red + " " + this.Matriz2[0, 0].green + " " + this.Matriz2[0, 0].blue;


            Color pixeles;

            this.image2 = new Bitmap(this.Ancho, this.Alto);
            this.imgPictureBox2 = new PictureBox();
            this.imgPictureBox2.SizeMode = PictureBoxSizeMode.AutoSize;

            for (int y = 0; y < this.Alto; y++)
                for (int x = 0; x < this.Ancho; x++)
                {
                    pixeles = Color.FromArgb(this.Matriz2[x, y].red, this.Matriz2[x, y].green, this.Matriz2[x, y].blue);

                    this.image2.SetPixel(x, y, pixeles);
                }
            this.imgPictureBox2.Image = this.image2;

            this.panel2.Controls.Clear();
            this.panel2.AutoScroll = true;
            this.panel2.Controls.Add(this.imgPictureBox2);





        }

        private void bindingNavigator1_RefreshItems(object sender, EventArgs e)
        {

        }

  


        private void trackBar1_Scroll(object sender, EventArgs e)
        {

            trackBar1.ValueChanged +=
            new System.EventHandler(TrackBar1_ValueChanged);
            this.Controls.Add(this.trackBar1);

        }


        private void TrackBar1_ValueChanged(object sender, System.EventArgs e)
        {
            
            //this.Angulo= Math.Tan((trackBar1.Value) * Math.PI / 180.0);
            this.Angulo = ((Convert.ToDouble(trackBar1.Value) / 10) + 1) * (Math.PI / 4);
            this.Angulo *= (1 / ((Math.PI) / 4));

            
            for (int y = 0; y < this.Alto; y++)
            {
                for (int x = 0; x < this.Ancho; x++)
                {


                    Matriz2[x, y].red = Convert.ToInt32((Matriz2[x, y].red - 128) * (Math.Tan(this.Angulo)) + 128); if (Matriz2[x, y].red > 255) { Matriz2[x, y].red = 255; } if (Matriz2[x, y].red < 0) { Matriz2[x, y].red = 0; }
                    Matriz2[x, y].green = Convert.ToInt32((Matriz2[x, y].green - 128) * (Math.Tan(this.Angulo)) + 128); if (Matriz2[x, y].green > 255) { Matriz2[x, y].green = 255; } if (Matriz2[x, y].green < 0) { Matriz2[x, y].green = 0; }
                    Matriz2[x, y].blue = Convert.ToInt32((Matriz2[x, y].blue - 128) * (Math.Tan(this.Angulo)) + 128); if (Matriz2[x, y].blue > 255) { Matriz2[x, y].blue = 255; } if (Matriz2[x, y].blue < 0) { Matriz2[x, y].blue = 0; }
                    }

            }
            Color pixeles;

            this.image2 = new Bitmap(this.Ancho, this.Alto);
            this.imgPictureBox2 = new PictureBox();
            this.imgPictureBox2.SizeMode = PictureBoxSizeMode.AutoSize;

            for (int y = 0; y < this.Alto; y++)
                for (int x = 0; x < this.Ancho; x++)
                {
                    pixeles = Color.FromArgb(this.Matriz2[x, y].red, this.Matriz2[x, y].green, this.Matriz2[x, y].blue);

                    this.image2.SetPixel(x, y, pixeles);
                }
            this.imgPictureBox2.Image = this.image2;

            this.panel2.Controls.Clear();
            this.panel2.AutoScroll = true;
            this.panel2.Controls.Add(this.imgPictureBox2);


        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            try
            {

                double min, max;
                min = Convert.ToDouble(textBox1.Text);
                max = Convert.ToDouble(textBox2.Text);



                for (int y = 0; y < this.Alto; y++)
                {
                    for (int x = 0; x < this.Ancho; x++)
                    {


                        if (Matriz[x, y].red >= max)
                        {
                            Matriz2[x, y].red = 255;
                        }
                        else
                        {
                            if (Matriz2[x, y].red <= min)
                            {
                                Matriz2[x, y].red = 0;
                            }
                            else
                            {

                                Matriz2[x, y].red = Convert.ToInt32((Matriz[x, y].red - min) / (max - min) * 255);
                            }
                        }



                        if (Matriz[x, y].green >= max)
                        {
                            Matriz2[x, y].green = 255;
                        }
                        else
                        {
                            if (Matriz2[x, y].green <= min)
                            {
                                Matriz2[x, y].green = 0;
                            }
                            else
                            {

                                Matriz2[x, y].green = Convert.ToInt32(((Matriz[x, y].green - min) / (max - min)) * 255);

                            }
                        }




                        if (Matriz[x, y].blue >= max)
                        {
                            Matriz2[x, y].blue = 255;
                        }
                        else
                        {
                            if (Matriz2[x, y].blue <= min)
                            {
                                Matriz2[x, y].blue = 0;
                            }
                            else
                            {

                                Matriz2[x, y].blue = Convert.ToInt32(((Matriz[x, y].blue - min) / (max - min)) * 255);

                            }
                        }




                    }

                }
                Color pixeles;

                this.image2 = new Bitmap(this.Ancho, this.Alto);
                this.imgPictureBox2 = new PictureBox();
                this.imgPictureBox2.SizeMode = PictureBoxSizeMode.AutoSize;

                for (int y = 0; y < this.Alto; y++)
                    for (int x = 0; x < this.Ancho; x++)
                    {
                        pixeles = Color.FromArgb(this.Matriz2[x, y].red, this.Matriz2[x, y].green, this.Matriz2[x, y].blue);

                        this.image2.SetPixel(x, y, pixeles);
                    }
                this.imgPictureBox2.Image = this.image2;

                this.panel2.Controls.Clear();
                this.panel2.AutoScroll = true;
                this.panel2.Controls.Add(this.imgPictureBox2);


            }
            catch (Exception)
            {

                MessageBox.Show("Debe Ingresar Valores entre 0-255", "Error Title", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }

        }

        private void button8_Click(object sender, EventArgs e)
        {


            for (int i = 0; i < 256; i++)
            {
                this.Reds[i] = 0; this.Greens[i] = 0; this.Blues[i] = 0;
            }



            for (int y = 0; y < this.Alto; y++)
            {
                for (int x = 0; x < this.Ancho; x++)
                {


                    this.Reds[this.Matriz2[x, y].red] = this.Reds[this.Matriz2[x, y].red] + 1;
                    this.Greens[this.Matriz2[x, y].green] = this.Greens[this.Matriz2[x, y].green] + 1;
                    this.Blues[this.Matriz2[x, y].blue] = this.Blues[this.Matriz2[x, y].blue] + 1;
                }

            }

            int[] dr = new int[256];
            int[] dg = new int[256];
            int[] db = new int[256];
            double[] pr = new double[256];
            double[] pg = new double[256];
            double[] pb = new double[256];
            double ar = 0;
            double ag = 0;
            double ab = 0;




            /*
                        int[] h = { 790, 1023, 850, 656, 329, 245, 122,81 };
                        int[] d = new int[8];
                        double[] p = new double[8];
                        double ac=0;

            */
            for (int i = 0; i < 256; i++)
            {

                pr[i] =  Convert.ToDouble(this.Reds[i]) / (this.Alto*this.Ancho);
                pg[i] = Convert.ToDouble(this.Greens[i]) / (this.Alto * this.Ancho);
                pb[i] = Convert.ToDouble(this.Blues[i]) / (this.Alto * this.Ancho);
            }



            for (int i = 0; i < 256; i++)
            {

                for (int j = 0; j <= i; j++)
                {
                    ar += pr[j];
                    ag += pg[j];
                    ab += pb[j];
                   
                }
                dr[i] =  Convert.ToInt32(ar*255);
                dg[i] = Convert.ToInt32(ag * 255);
                db[i] = Convert.ToInt32(ab * 255);
                ar = 0;
                ag = 0;
                ab = 0;

                //richTextBox1.Text += d[i] + "\n";  
            }




            for (int y = 0; y < this.Alto; y++)
            {
                for (int x = 0; x < this.Ancho; x++)
                {



                    Matriz2[x, y].red = dr[Matriz2[x, y].red];
                    Matriz2[x, y].green = dg[Matriz2[x, y].green];
                    Matriz2[x, y].blue = db[Matriz2[x, y].blue];

                }

            }

            //despues.Text = " Datos" + this.Matriz2[0, 0].red + " " + this.Matriz2[0, 0].green + " " + this.Matriz2[0, 0].blue;


            Color pixeles;

            this.image2 = new Bitmap(this.Ancho, this.Alto);
            this.imgPictureBox2 = new PictureBox();
            this.imgPictureBox2.SizeMode = PictureBoxSizeMode.AutoSize;

            for (int y = 0; y < this.Alto; y++)
                for (int x = 0; x < this.Ancho; x++)
                {
                    pixeles = Color.FromArgb(this.Matriz2[x, y].red, this.Matriz2[x, y].green, this.Matriz2[x, y].blue);

                    this.image2.SetPixel(x, y, pixeles);
                }
            this.imgPictureBox2.Image = this.image2;

            this.panel2.Controls.Clear();
            this.panel2.AutoScroll = true;
            this.panel2.Controls.Add(this.imgPictureBox2);







        }

      


 


     

    

     
       
        private void button9_Click(object sender, EventArgs e)
        {


            for (int y = 0; y < this.Alto; y++)
            {
                for (int x = 0; x < this.Ancho; x++)
                {

                    this.Matriz2[x, y].red =  Convert.ToInt32(Convert.ToDouble(this.Matriz2[x, y].red)*0.393 + Convert.ToDouble(this.Matriz2[x, y].green)*0.769 + Convert.ToDouble(this.Matriz2[x, y].blue)*0.189); if (Matriz2[x, y].red > 255) { Matriz2[x, y].red = 255; } else { if (Matriz2[x, y].red < 0) { Matriz2[x, y].red = 0; } };
                    this.Matriz2[x, y].green = Convert.ToInt32(Convert.ToDouble(this.Matriz2[x, y].red) * 0.349 + Convert.ToDouble(this.Matriz2[x, y].green) * 0.686 + Convert.ToDouble(this.Matriz2[x, y].blue) * 0.168); if (Matriz2[x, y].green > 255) { Matriz2[x, y].green = 255; } else { if (Matriz2[x, y].green < 0) { Matriz2[x, y].green = 0; } };
                    this.Matriz2[x, y].blue = Convert.ToInt32(Convert.ToDouble(this.Matriz2[x, y].red) * 0.272 + Convert.ToDouble(this.Matriz2[x, y].green) * 0.534 + Convert.ToDouble(this.Matriz2[x, y].blue) * 0.131); if (Matriz2[x, y].blue > 255) { Matriz2[x, y].blue = 255; } else { if (Matriz2[x, y].blue < 0) { Matriz2[x, y].blue = 0; } };
                }

            }


            Color pixeles;

            this.image2 = new Bitmap(this.Ancho, this.Alto);
            this.imgPictureBox2 = new PictureBox();
            this.imgPictureBox2.SizeMode = PictureBoxSizeMode.AutoSize;

            for (int y = 0; y < this.Alto; y++)
                for (int x = 0; x < this.Ancho; x++)
                {
                    pixeles = Color.FromArgb(this.Matriz2[x, y].red, this.Matriz2[x, y].green, this.Matriz2[x, y].blue);

                    this.image2.SetPixel(x, y, pixeles);
                }
            this.imgPictureBox2.Image = this.image2;

            this.panel2.Controls.Clear();
            this.panel2.AutoScroll = true;
            this.panel2.Controls.Add(this.imgPictureBox2);



        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {

            try
            {

                this.Valor = Convert.ToInt32(textBox4.Text);



                for (int y = 0; y < this.Alto; y++)
                {
                    for (int x = 0; x < this.Ancho; x++)
                    {

                        
                        this.Angulo=(Convert.ToDouble(this.Matriz2[x, y].red)+ Convert.ToDouble(this.Matriz2[x, y].green) + Convert.ToDouble(this.Matriz2[x, y].blue))/3;
                        if (Convert.ToInt32(this.Angulo) < this.Valor) { this.Angulo = 0; }else{this.Angulo = 255;};


                        this.Matriz2[x, y].red = Convert.ToInt32(this.Angulo);
                        this.Matriz2[x, y].green = Convert.ToInt32(this.Angulo);
                        this.Matriz2[x, y].blue = Convert.ToInt32(this.Angulo);
                    }

                }


                Color pixeles;

                this.image2 = new Bitmap(this.Ancho, this.Alto);
                this.imgPictureBox2 = new PictureBox();
                this.imgPictureBox2.SizeMode = PictureBoxSizeMode.AutoSize;

                for (int y = 0; y < this.Alto; y++)
                    for (int x = 0; x < this.Ancho; x++)
                    {
                        pixeles = Color.FromArgb(this.Matriz2[x, y].red, this.Matriz2[x, y].green, this.Matriz2[x, y].blue);

                        this.image2.SetPixel(x, y, pixeles);
                    }
                this.imgPictureBox2.Image = this.image2;

                this.panel2.Controls.Clear();
                this.panel2.AutoScroll = true;
                this.panel2.Controls.Add(this.imgPictureBox2);


            }
            catch (Exception)
            {

                MessageBox.Show("Debe Ingresar un Valor Valido", "Error Title", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }






        }

        private void button11_Click(object sender, EventArgs e)
        {
            try
            {

                this.Valor = Convert.ToInt32(textBox4.Text);



                for (int y = 0; y < this.Alto; y++)
                {
                    for (int x = 0; x < this.Ancho; x++)
                    {


                        if (this.Matriz2[x, y].red < this.Valor) { this.Matriz2[x, y].red = 0; } else { this.Matriz2[x, y].red = 255; };

                        this.Matriz2[x, y].green = 0;
                        this.Matriz2[x, y].blue = 0;

                    }
                }

                Color pixeles;

                this.image2 = new Bitmap(this.Ancho, this.Alto);
                this.imgPictureBox2 = new PictureBox();
                this.imgPictureBox2.SizeMode = PictureBoxSizeMode.AutoSize;

                for (int y = 0; y < this.Alto; y++)
                    for (int x = 0; x < this.Ancho; x++)
                    {
                        pixeles = Color.FromArgb(this.Matriz2[x, y].red, this.Matriz2[x, y].green, this.Matriz2[x, y].blue);

                        this.image2.SetPixel(x, y, pixeles);
                    }
                this.imgPictureBox2.Image = this.image2;

                this.panel2.Controls.Clear();
                this.panel2.AutoScroll = true;
                this.panel2.Controls.Add(this.imgPictureBox2);


            }
            catch (Exception)
            {

                MessageBox.Show("Debe Ingresar un Valor Valido", "Error Title", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }




        }

        private void button12_Click(object sender, EventArgs e)
        {

            try
            {

                this.Valor = Convert.ToInt32(textBox4.Text);



                for (int y = 0; y < this.Alto; y++)
                {
                    for (int x = 0; x < this.Ancho; x++)
                    {


                        if (this.Matriz2[x, y].green < this.Valor) { this.Matriz2[x, y].green = 0; } else { this.Matriz2[x, y].green = 255; };

                        this.Matriz2[x, y].red = 0;
                        this.Matriz2[x, y].blue = 0;

                    }
                }

                Color pixeles;

                this.image2 = new Bitmap(this.Ancho, this.Alto);
                this.imgPictureBox2 = new PictureBox();
                this.imgPictureBox2.SizeMode = PictureBoxSizeMode.AutoSize;

                for (int y = 0; y < this.Alto; y++)
                    for (int x = 0; x < this.Ancho; x++)
                    {
                        pixeles = Color.FromArgb(this.Matriz2[x, y].red, this.Matriz2[x, y].green, this.Matriz2[x, y].blue);

                        this.image2.SetPixel(x, y, pixeles);
                    }
                this.imgPictureBox2.Image = this.image2;

                this.panel2.Controls.Clear();
                this.panel2.AutoScroll = true;
                this.panel2.Controls.Add(this.imgPictureBox2);


            }
            catch (Exception)
            {

                MessageBox.Show("Debe Ingresar un Valor Valido", "Error Title", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }




        }

        private void button13_Click(object sender, EventArgs e)
        {
            try
            {

                this.Valor = Convert.ToInt32(textBox4.Text);



                for (int y = 0; y < this.Alto; y++)
                {
                    for (int x = 0; x < this.Ancho; x++)
                    {


                        if (this.Matriz2[x, y].blue < this.Valor) { this.Matriz2[x, y].blue = 0; } else { this.Matriz2[x, y].blue = 255; };

                        this.Matriz2[x, y].green = 0;
                        this.Matriz2[x, y].red = 0;

                    }
                }

                Color pixeles;

                this.image2 = new Bitmap(this.Ancho, this.Alto);
                this.imgPictureBox2 = new PictureBox();
                this.imgPictureBox2.SizeMode = PictureBoxSizeMode.AutoSize;

                for (int y = 0; y < this.Alto; y++)
                    for (int x = 0; x < this.Ancho; x++)
                    {
                        pixeles = Color.FromArgb(this.Matriz2[x, y].red, this.Matriz2[x, y].green, this.Matriz2[x, y].blue);

                        this.image2.SetPixel(x, y, pixeles);
                    }
                this.imgPictureBox2.Image = this.image2;

                this.panel2.Controls.Clear();
                this.panel2.AutoScroll = true;
                this.panel2.Controls.Add(this.imgPictureBox2);


            }
            catch (Exception)
            {

                MessageBox.Show("Debe Ingresar un Valor Valido", "Error Title", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }


        }

        private void button14_Click(object sender, EventArgs e)
        {

            Color pixeles;

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




            this.Matriz2 = (Pixel[,])this.Matriz.Clone();



            this.image2 = new Bitmap(this.Ancho, this.Alto);
            this.imgPictureBox2 = new PictureBox();
            this.imgPictureBox2.SizeMode = PictureBoxSizeMode.AutoSize;

            for (int y = 0; y < this.Alto; y++)
                for (int x = 0; x < this.Ancho; x++)
                {
                    pixeles = Color.FromArgb(this.Matriz2[x, y].red, this.Matriz2[x, y].green, this.Matriz2[x, y].blue);

                    this.image2.SetPixel(x, y, pixeles);
                }
            this.imgPictureBox2.Image = this.image2;

            this.panel2.Controls.Clear();
            this.panel2.AutoScroll = true;
            this.panel2.Controls.Add(this.imgPictureBox2);


        }

        private void button15_Click(object sender, EventArgs e)
        {

            SaveFileDialog fdlg = new SaveFileDialog();
            fdlg.Title = "Guardar Imagen!";
            fdlg.InitialDirectory = @"c:\";
            fdlg.Filter = "Imagen BMP (*.bmp)|*.bmp";
            fdlg.FilterIndex = 2;
            fdlg.RestoreDirectory = true;
            if (fdlg.ShowDialog() == DialogResult.OK)
            {
                this.Ruta = fdlg.FileName;

                this.image2.Save(this.Ruta, System.Drawing.Imaging.ImageFormat.Bmp);


            }



        }



        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void button16_Click(object sender, EventArgs e)
        {



            
            this.Ang += Convert.ToInt32(textBox5.Text);

           


            float mitx = this.Ancho / 2;
            float mity = this.Alto / 2;
            double tempx,tempy,Rx,Ry;
            int Ang=Convert.ToInt32(textBox5.Text);

            for (int y = 0; y < this.Alto; y++)
            {
                for (int x = 0; x < this.Ancho; x++)
                {
                    Matriz3[x, y].red = 0;
                    Matriz3[x, y].green = 0;
                    Matriz3[x, y].blue = 0;
                }

            }

      

            for (int y = 0; y < this.Alto; y++)
            {
                for (int x = 0; x < this.Ancho; x++)
                {

                    tempx = x - mitx;
                    tempy = y - mity;

                    Rx = (tempx * Math.Cos((Math.PI / 180) * this.Ang)) - (tempy * Math.Sin((Math.PI / 180) * this.Ang));
                    Ry = (tempx * Math.Sin((Math.PI / 180) * this.Ang)) + (tempy * Math.Cos((Math.PI / 180) * this.Ang));

                    tempx = Math.Round(Rx + mitx,0);
                    tempy = Math.Round(Ry + mity, 0);

                    if (0 <= tempx && tempx < this.Ancho && 0 <= tempy && tempy < this.Alto)
                    {
                        Matriz3[Convert.ToInt32(tempx), Convert.ToInt32(tempy)].red = Matriz2[x, y].red;
                        Matriz3[Convert.ToInt32(tempx), Convert.ToInt32(tempy)].green = Matriz2[x, y].green;
                        Matriz3[Convert.ToInt32(tempx), Convert.ToInt32(tempy)].blue = Matriz2[x, y].blue;
                    }
                }

            }



           for (int y = 1; y < this.Alto-1; y++)
            {
                for (int x = 1; x < this.Ancho-1; x++)
                {

                    if (Matriz3[x, y].red == 0 && Matriz3[x, y].green == 0 && Matriz3[x, y].blue == 0)
                    {

                        Matriz3[x, y].red = (Convert.ToInt32(Matriz3[x - 1, y - 1].red) + Convert.ToInt32(Matriz3[x, y - 1].red) + Convert.ToInt32(Matriz3[x + 1, y - 1].red) + Convert.ToInt32(Matriz3[x - 1, y].red) + Convert.ToInt32(Matriz3[x + 1, y].red) + Convert.ToInt32(Matriz3[x - 1, y + 1].red) + Convert.ToInt32(Matriz3[x, y + 1].red) + Convert.ToInt32(Matriz3[x + 1, y + 1].red)) / 8;
                        Matriz3[x, y].green = (Convert.ToInt32(Matriz3[x - 1, y - 1].green) + Convert.ToInt32(Matriz3[x, y - 1].green) + Convert.ToInt32(Matriz3[x + 1, y - 1].green) + Convert.ToInt32(Matriz3[x - 1, y].green) + Convert.ToInt32(Matriz3[x + 1, y].green) + Convert.ToInt32(Matriz3[x - 1, y + 1].green) + Convert.ToInt32(Matriz3[x, y + 1].green) + Convert.ToInt32(Matriz3[x + 1, y + 1].green)) / 8;
                        Matriz3[x, y].blue = (Convert.ToInt32(Matriz3[x - 1, y - 1].blue) + Convert.ToInt32(Matriz3[x, y - 1].blue) + Convert.ToInt32(Matriz3[x + 1, y - 1].blue) + Convert.ToInt32(Matriz3[x - 1, y].blue) + Convert.ToInt32(Matriz3[x + 1, y].blue) + Convert.ToInt32(Matriz3[x - 1, y + 1].blue) + Convert.ToInt32(Matriz3[x, y + 1].blue) + Convert.ToInt32(Matriz3[x + 1, y + 1].blue)) / 8;

                    }


                }

            }

          
       
            Color pixeles;

            this.image2 = new Bitmap(this.Ancho, this.Alto);
            this.imgPictureBox2 = new PictureBox();
            this.imgPictureBox2.SizeMode = PictureBoxSizeMode.AutoSize;

            for (int y = 0; y < this.Alto; y++)
                for (int x = 0; x < this.Ancho; x++)
                {
                    pixeles = Color.FromArgb(this.Matriz3[x, y].red, this.Matriz3[x, y].green, this.Matriz3[x, y].blue);

                    this.image2.SetPixel(x, y, pixeles);
                }
            this.imgPictureBox2.Image = this.image2;

            this.panel2.Controls.Clear();
            this.panel2.AutoScroll = true;
            this.panel2.Controls.Add(this.imgPictureBox2);
            


        }

        private void textBox1_TextChanged_2(object sender, EventArgs e)
        {

        }

        private void button17_Click(object sender, EventArgs e)
        {


            Pixel aux;

            for (int y = 0; y < this.Alto / 2; y++)
            {
                for (int x = 0; x < this.Ancho; x++)
                {

                    aux.red = Matriz2[x, y].red;
                    aux.green = Matriz2[x, y].green;
                    aux.blue = Matriz2[x, y].blue;

                    this.Matriz2[x, y].red = Matriz2[x, this.Alto - y - 1].red;
                    this.Matriz2[x, y].green = Matriz2[x, this.Alto - y - 1].green;
                    this.Matriz2[x, y].blue = Matriz2[x, this.Alto - y - 1].blue;


                    this.Matriz2[x, this.Alto - y - 1].red = aux.red;
                    this.Matriz2[x, this.Alto - y - 1].green = aux.green;
                    this.Matriz2[x, this.Alto - y - 1].blue = aux.blue;

                }

            }





            Color pixeles;

            this.image2 = new Bitmap(this.Ancho, this.Alto);
            this.imgPictureBox2 = new PictureBox();
            this.imgPictureBox2.SizeMode = PictureBoxSizeMode.AutoSize;

            for (int y = 0; y < this.Alto; y++)
                for (int x = 0; x < this.Ancho; x++)
                {
                    pixeles = Color.FromArgb(this.Matriz2[x, y].red, this.Matriz2[x, y].green, this.Matriz2[x, y].blue);

                    this.image2.SetPixel(x, y, pixeles);
                }
            this.imgPictureBox2.Image = this.image2;

            this.panel2.Controls.Clear();
            this.panel2.AutoScroll = true;
            this.panel2.Controls.Add(this.imgPictureBox2);

        }

        private void button18_Click(object sender, EventArgs e)
        {


            Pixel aux;

            for (int y = 0; y < this.Alto; y++)
            {
                for (int x = 0; x < this.Ancho / 2; x++)
                {

                    aux.red = Matriz2[x, y].red;
                    aux.green = Matriz2[x, y].green;
                    aux.blue = Matriz2[x, y].blue;

                    this.Matriz2[x, y].red = Matriz2[this.Ancho - x - 1, y].red;
                    this.Matriz2[x, y].green = Matriz2[this.Ancho - x - 1, y].green;
                    this.Matriz2[x, y].blue = Matriz2[this.Ancho - x - 1, y].blue;


                    this.Matriz2[this.Ancho - x - 1, y].red = aux.red;
                    this.Matriz2[this.Ancho - x - 1, y].green = aux.green;
                    this.Matriz2[this.Ancho - x - 1, y].blue = aux.blue;

                }

            }


            Color pixeles;

            this.image2 = new Bitmap(this.Ancho, this.Alto);
            this.imgPictureBox2 = new PictureBox();
            this.imgPictureBox2.SizeMode = PictureBoxSizeMode.AutoSize;

            for (int y = 0; y < this.Alto; y++)
                for (int x = 0; x < this.Ancho; x++)
                {
                    pixeles = Color.FromArgb(this.Matriz2[x, y].red, this.Matriz2[x, y].green, this.Matriz2[x, y].blue);

                    this.image2.SetPixel(x, y, pixeles);
                }
            this.imgPictureBox2.Image = this.image2;

            this.panel2.Controls.Clear();
            this.panel2.AutoScroll = true;
            this.panel2.Controls.Add(this.imgPictureBox2);







        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click_1(object sender, EventArgs e)
        {
            double esc = 1 + (Convert.ToDouble(textBox6.Text) / 100);
            Console.Write(Math.Round(this.Ancho*esc,0));

            int ancho4, alto4;

            ancho4=Convert.ToInt32(Math.Round(this.Ancho * esc, 0));
            alto4 = Convert.ToInt32(Math.Round(this.Alto * esc, 0));

            this.Matriz4 = new Pixel[ancho4,alto4];


            for (int y = 0; y < alto4; y++)
            {
                for (int x = 0; x < ancho4; x++)
                {
                    Matriz4[x, y].red = 0;
                    Matriz4[x, y].green = 0;
                    Matriz4[x, y].blue = 0;
                }

            }


            for (int y = 0; y < this.Alto; y++)
            {
                for (int x = 0; x < this.Ancho; x++)
                {
                    Matriz4[Convert.ToInt32(Math.Round(x * esc, 0)), Convert.ToInt32(Math.Round(y * esc, 0))].red = Matriz2[x,y].red;
                    Matriz4[Convert.ToInt32(Math.Round(x * esc, 0)), Convert.ToInt32(Math.Round(y * esc, 0))].green = Matriz2[x,y].green;
                    Matriz4[Convert.ToInt32(Math.Round(x * esc, 0)), Convert.ToInt32(Math.Round(y * esc, 0))].blue = Matriz2[x,y].blue;
                }

            }

           bool hue=false;

           for (int y = 0; y < alto4; y++)
                for (int x = 0; x < ancho4; x++)
                {
                    if (Matriz4[x, y].red == 0 && Matriz4[x, y].green == 0 && Matriz4[x, y].blue == 0)
                    {
                        
                       // Console.Write(x + "  " + 0 + "\n");

                        if (x==0){

                            if(Matriz4[x, y].red!=0){
                                hue=false;
                            }else{

                                hue=true;
                            }

                        }

                        if(hue==false){
                            Matriz4[x, y].red = Matriz4[x - 1, y].red;
                            Matriz4[x, y].green = Matriz4[x - 1, y].green;
                            Matriz4[x, y].blue = Matriz4[x - 1, y].blue;

                        }
                        else {

                            
                                Matriz4[x, y].red = Matriz4[x, y-1].red;
                                Matriz4[x, y].green = Matriz4[x, y-1].green;
                                Matriz4[x, y].blue = Matriz4[x, y-1].blue;
                        
                         }
                    
                        
                        


                       /*// Console.Write(x + "  " + y+"\n");

                        if (x - 1 >= 0)
                        {
                            if (Matriz4[x - 1, 0].red != -1)
                            {
                                Matriz4[x, 0].red = Matriz4[x - 1, 0].red;
                                Matriz4[x, 0].green = Matriz4[x - 1, 0].green;
                                Matriz4[x, 0].blue = Matriz4[x - 1, 0].blue;
                            }
                            /*else
                            {
                                if (y - 1 >= 0)
                                {
                                    Matriz4[x, y].red = Matriz4[x, y - 1].red;
                                    Matriz4[x, y].green = Matriz4[x, y - 1].green;
                                    Matriz4[x, y].blue = Matriz4[x, y - 1].blue;

                                }
                            }
                            
                        }

                        */


                   



                    }
                 

            }
            

            Color pixeles;

            this.image2 = new Bitmap(ancho4,alto4);
            this.imgPictureBox2 = new PictureBox();
            this.imgPictureBox2.SizeMode = PictureBoxSizeMode.AutoSize;

            for (int y = 0; y < alto4; y++)
                for (int x = 0; x < ancho4; x++)
                {
                    pixeles = Color.FromArgb(this.Matriz4[x, y].red, this.Matriz4[x, y].green, this.Matriz4[x, y].blue);

                    this.image2.SetPixel(x, y, pixeles);
                }
            this.imgPictureBox2.Image = this.image2;

            this.panel2.Controls.Clear();
            this.panel2.AutoScroll = true;
            this.panel2.Controls.Add(this.imgPictureBox2);
















        }

       
       

    }
}
