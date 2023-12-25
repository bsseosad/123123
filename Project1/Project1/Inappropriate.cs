using AForge.Video;
using AForge.Video.DirectShow;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project1
{
    public partial class Inappropriate : Form
    {
        private int currentImageIndex = 1;
        public Inappropriate()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
        }
        FilterInfoCollection filterInfo;
        VideoCaptureDevice device;


        private void Inappropriate_Load(object sender, EventArgs e)
        {

    
            timer1.Interval = 1500; // 2초마다 이미지 변경 (원하는 시간으로 변경 가능)
            timer1.Tick += Timer1_Tick;
            filterInfo = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo filterInfo in filterInfo)
                comboBox1.Items.Add(filterInfo.Name);
            comboBox1.SelectedIndex = 0;
            device = new VideoCaptureDevice();
           
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            currentImageIndex++;
            if (currentImageIndex > imageList1.Images.Count)
            {
                currentImageIndex = 1; // 이미지 인덱스를 1로 초기화하여 순환
            }
            LoadImage();
        }
        private void LoadImage()
        {
            pictureBox2.Image = imageList1.Images[currentImageIndex-1];
        }

        private void NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            pictureBox1.Image = (Bitmap)eventArgs.Frame.Clone();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            device.Stop();
            if (device.IsRunning == false)
            {
               
                this.imageList1.Images.Add((Bitmap)pictureBox1.Image.Clone());
            }
            device.Start();
            timer1.Enabled = true;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            device = new VideoCaptureDevice(filterInfo[comboBox1.SelectedIndex].MonikerString);
            device.NewFrame += NewFrame;
            device.Start();
        }

        private void Inappropriate_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (device.IsRunning == true)
            {
                device.Stop();
            }
        }
    }
}
