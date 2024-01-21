using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using AForge;
using AForge.Video;
using AForge.Video.DirectShow;
using ZXing;
using ZXing.Aztec;

namespace BSCS3_Prelim
{
    public partial class Attendance : Form
    {  
        private FilterInfoCollection CaptureDevice;
        private VideoCaptureDevice FinalFrame;

         

        public Attendance()
        {
            InitializeComponent();
        } 

        private void Attendance_Load(object sender, EventArgs e)
        {
        timer1.Interval = 1000; // Set the timer interval (in milliseconds)

            CaptureDevice = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo Device in CaptureDevice)
                cmbSelectCam.Items.Add(Device.Name);

            cmbSelectCam.SelectedIndex = 0;
            FinalFrame = new VideoCaptureDevice();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            FinalFrame = new VideoCaptureDevice(CaptureDevice[cmbSelectCam.SelectedIndex].MonikerString);
            FinalFrame.NewFrame += new NewFrameEventHandler(FinalFrame_NewFrame);
            FinalFrame.Start();
            timer1.Start();
        }

        private void FinalFrame_NewFrame(object sender, NewFrameEventArgs EventArgs)
        {
            picBoxFrame.Image = (Bitmap)EventArgs.Frame.Clone();
        }

        private void Attendance_FormClosing(object sender, EventArgs e)
        {
            if (FinalFrame != null && FinalFrame.IsRunning)
                FinalFrame.Stop();
            timer1.Stop();
        }
        private void timer1_tick(object sender, EventArgs e)
        {
            if (FinalFrame != null && FinalFrame.IsRunning)
            {
                BarcodeReader reader = new BarcodeReader();
                Result result = reader.Decode((Bitmap)picBoxFrame.Image);

                try
                {
                    if (result != null)
                    {
                        string decoded = result.Text.Trim();
                        if (!string.IsNullOrEmpty(decoded))
                        {
                            textBox1.Text = decoded;
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle exception, e.g., display an error message
                    MessageBox.Show("Error decoding QR code: " + ex.Message);
                }
            }
        }

        private void btnEnd_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }
    }
}

  