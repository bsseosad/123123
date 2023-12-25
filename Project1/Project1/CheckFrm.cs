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
    public partial class CheckFrm : Form
    {

        private int currentImageIndex = 1; // 현재 표시 중인 이미지 인덱스
        private int totalImages = 3; // 전체 이미지 개수 (예제에서는 3개로 가정)
        public CheckFrm()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
        }

        private void CheckFrm_Load(object sender, EventArgs e)
        {
            timer1.Interval = 2000; // 2초마다 이미지 변경 (원하는 시간으로 변경 가능)
            timer1.Tick += Timer1_Tick;
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            currentImageIndex++;
            if (currentImageIndex > totalImages)
            {
                currentImageIndex = 1; // 이미지 인덱스를 1로 초기화하여 순환
            }
            LoadImage();
        }
        private void LoadImage()
        {
            string resourceName = $"Image{currentImageIndex}";
            pictureBox1.Image = (System.Drawing.Image)Properties.Resources.ResourceManager.GetObject(resourceName);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            // 시작 또는 정지 버튼 클릭 시 타이머 동작 여부 전환
            if (timer1.Enabled)
            {
                timer1.Stop();
            }
            else
            {
                timer1.Start();
            }
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void PictureEditBtn_Click(object sender, EventArgs e)
        {
           Form1 form = new Form1();
            form.ShowDialog();
        }

        private void InappropriateBtn_Click(object sender, EventArgs e)
        {
            Inappropriate inappro = new Inappropriate();
            inappro.ShowDialog();   
        }
    }
}
