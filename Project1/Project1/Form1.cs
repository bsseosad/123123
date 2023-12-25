using AForge.Video;
using AForge.Video.DirectShow;
using System;
using System.Drawing;
using System.IO;
using System.Security.AccessControl;
using System.Windows.Forms;

namespace Project1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
        }
        FilterInfoCollection filterInfo;
        VideoCaptureDevice device;

        private void Form1_Load(object sender, EventArgs e)
        {
            filterInfo = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo filterInfo in filterInfo)
                comboBox1.Items.Add(filterInfo.Name);
            comboBox1.SelectedIndex = 0;
            device = new VideoCaptureDevice();

        }
        private void NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            pictureBox1.Image = (Bitmap)eventArgs.Frame.Clone();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (device.IsRunning == true)
            {
                device.Stop();
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            device = new VideoCaptureDevice(filterInfo[comboBox1.SelectedIndex].MonikerString);
            device.NewFrame += NewFrame;
            device.Start();
        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            device.Stop();
            if (device.IsRunning == false)
            {
                SaveImageToFile("C:\\Users\\byeon\\OneDrive\\바탕 화면\\Heo");
            }
            device.Start();
        }
        private void SaveImageToFile(string folderPath)
        {

            string filePath = Path.Combine(folderPath,"sample_image.png");
            Bitmap bitmap = new Bitmap(pictureBox1.Image);
           
            try
            {
                bitmap.Save(filePath, System.Drawing.Imaging.ImageFormat.Png);
                MessageBox.Show("이미지가 성공적으로 저장되었습니다.", "성공", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("저장할 권한이 없습니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"이미지 저장 중 오류 발생: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}
