using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace superchangefortest
{
    public partial class Form1 : Form
    {

        System.Drawing.Image [] imgs=new Image[20];


        int flag = 1;
        int flag2 = 11;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string str2 = System.IO.Directory.GetCurrentDirectory();


            string zzz = str2+ "\\timgchanged";

            for(int i = 1; i < 16; i++)
            {
                string zzz2 = zzz + i + ".jpg";

                imgs[i]= System.Drawing.Image.FromFile(zzz2);//双引号里是图片的路径
            }

            
            timer1.Start();



            //System.Threading.Thread.Sleep(1000);//

            //pictureBox1.Image = img2;
            //pictureBox2.Image = img;

            //System.Threading.Thread.Sleep(1000);//


            /*
            int i = 0;
            while (true)
            {
                if (i == 0)
                {
                    pictureBox1.Image = img;
                    pictureBox2.Image = img2;
                    i = 1;
                }
                else
                {

                    pictureBox1.Image = img2;
                    pictureBox2.Image = img;

                    i = 0;
                }


            }
            */

        }
        public struct Pixel
        {
            public byte B;
            public byte G;
            public byte R;
        }
        private void button2_Click(object sender, EventArgs e)
        {


            Bitmap b = new Bitmap("C:\\Users\\zhangmingchen\\Desktop\\timg1.jpg");

            Pixel[,] changed_data = new Pixel[b.Width, b.Height];

            BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite,
            PixelFormat.Format24bppRgb);
            int stride = bmData.Stride;   // 扫描的宽度 

            unsafe
            {
                byte* p = (byte*)bmData.Scan0.ToPointer(); // 获取图像首地址 
                byte* p2 = null;
                int nOffset = stride - b.Width * 3;  // 实际宽度与系统宽度的距离 

                for (int y = 0; y < b.Height; ++y)
                {
                    for (int x = 0; x < b.Width; ++x)
                    {

                        changed_data[x, y].R= p[0];
                        changed_data[x, y].G = p[1];
                        changed_data[x, y].B = p[2];

                        p += 3;  // 跳过3个字节处理下个像素点 
                    }
                    p += nOffset; // 加上间隔 
                }
            }
            b.UnlockBits(bmData); // 解锁 



            Bitmap bitmap_temp3 = zzz2(changed_data);
            bitmap_temp3.Save("C:\\Users\\zhangmingchen\\Desktop\\timgchanged1.jpg");


        }
        Bitmap zzz2(Pixel[,] cuted_data)
        {
            Bitmap b = new Bitmap(cuted_data.GetLength(0), cuted_data.GetLength(1)/3);

            BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, (b.Height)), ImageLockMode.ReadWrite,
            PixelFormat.Format24bppRgb);
            int stride = bmData.Stride;   // 扫描的宽度 

            unsafe
            {
                byte* p = (byte*)bmData.Scan0.ToPointer(); // 获取图像首地址 
                byte* p2 = null;
                int nOffset = stride - b.Width * 3;  // 实际宽度与系统宽度的距离 

                for (int y = 0; y < (b.Height); ++y)
                {
                    for (int x = 0; x < b.Width; ++x)
                    {
                        p[2] = (byte)rgbchange(cuted_data,x,y);
                        p[1]= (byte)rgbchange(cuted_data, x, b.Height+y);
                        p[3]= (byte)rgbchange(cuted_data, x, b.Height*2+y);

                        p += 3;  // 跳过3个字节处理下个像素点 
                    }
                    p += nOffset; // 加上间隔 
                }
            }
            b.UnlockBits(bmData); // 解锁 

            return b;
        }
        int rgbchange(Pixel[,] cuted_data,int x,int y)
        {
            int final = (cuted_data[x, y].R + cuted_data[x, y].G + cuted_data[x, y].B) / 3;

            if (final>100)
            {
                final = 255;
            }
            else
            {
                final = 0;
            }
            return final;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Location = new Point(0,-10);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Location = new Point(124, 638);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Bitmap b = new Bitmap("C:\\Users\\zhangmingchen\\Desktop\\timg2.jpg");

            Pixel[,] changed_data = new Pixel[b.Width, b.Height];

            BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite,
            PixelFormat.Format24bppRgb);
            int stride = bmData.Stride;   // 扫描的宽度 

            unsafe
            {
                byte* p = (byte*)bmData.Scan0.ToPointer(); // 获取图像首地址 
                byte* p2 = null;
                int nOffset = stride - b.Width * 3;  // 实际宽度与系统宽度的距离 

                for (int y = 0; y < b.Height; ++y)
                {
                    for (int x = 0; x < b.Width; ++x)
                    {

                        changed_data[x, y].R = p[0];
                        changed_data[x, y].G = p[1];
                        changed_data[x, y].B = p[2];

                        p += 3;  // 跳过3个字节处理下个像素点 
                    }
                    p += nOffset; // 加上间隔 
                }
            }
            b.UnlockBits(bmData); // 解锁 



            Bitmap bitmap_temp3 = zzz2(changed_data);
            bitmap_temp3.Save("C:\\Users\\zhangmingchen\\Desktop\\timgchanged2.jpg");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
            for(int i = 1; i < 10; i++)
            {

                if (flag == i)
                {
                    pictureBox1.Image = imgs[i];
                    flag++;
                    if (flag == 10)
                    {
                        flag = 1;
                    }
                    break;
                }


            }
            for (int i = 11; i < 16; i++)
            {

                if (flag2 == i)
                {
                    pictureBox2.Image = imgs[i];
                    flag2++;
                    if (flag2 == 16)
                    {
                        flag2 = 11;
                    }
                    break;
                }

            }


        }

        private void button5_Click(object sender, EventArgs e)
        {


            int sz = Convert.ToInt16(textBox1.Text);

            Point bufpoint = this.Location;
            bufpoint.Y -= sz;

            this.Location = bufpoint;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int sz = Convert.ToInt16(textBox1.Text);

            Point bufpoint = this.Location;
            bufpoint.Y += sz;

            this.Location = bufpoint;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            int sz = Convert.ToInt16(textBox1.Text);
            Point bufpoint = this.Location;
            bufpoint.X -= sz;

            this.Location = bufpoint;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            int sz = Convert.ToInt16(textBox1.Text);
            Point bufpoint = this.Location;
            bufpoint.X += sz;

            this.Location = bufpoint;
        }

        private void button9_Click(object sender, EventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {

        }
    }
}
