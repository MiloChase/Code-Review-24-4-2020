/***************************************************************************
*Disclaimer: NASA USLI Oregon State University ECE Capstone Team-9 2019-2020
*Author: Milo Chase & Jia Yi Li 
*Description: Graphic User Interface for the Payload Subteam.
*Date: 11/20/2019
*Version: 1.0
***************************************************************************/
//include required modules
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.IO.Ports;
using System.IO;
using AForge;
using AForge.Video;
using AForge.Video.DirectShow;

//GUI window
namespace GUI_Payload
{
    public partial class Form1 : Form
    {
        //creates private variables within the class
        private SerialPort _serialPort;
        //private DateTime dt;
        private string port_data;
        private string[]  strList;
        private string  myAcc, myLong, myLat;
        private FilterInfoCollection CaptureDevices;
        private VideoCaptureDevice videoSource;

        public Form1()
        {
            InitializeComponent();
            getAvailablePorts();
        }
        void getAvailablePorts()     //gets available ports from computer and displays them in the combo box to select
        {
            String[] ports = SerialPort.GetPortNames();
            comboBox2.Items.AddRange(ports);
        }


        private void GUI_Payload(object sender, EventArgs e)
        {
           //access diferrent type of USB web cams
            CaptureDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            //looks for all available camera devices found and add as an option
            foreach (FilterInfo Device in CaptureDevices)
            {
                comboBox1.Items.Add(Device.Name);
            }
          //  comboBox1.SelectedIndex = 0;                                       //bug, can't run when bo cameras are found
           //initialize video source variable to capture video data form selected device
            videoSource = new VideoCaptureDevice();
        }

        private void startbutton_Click(object sender, EventArgs e)
        {
            //create video source with selected camera
            videoSource = new VideoCaptureDevice(CaptureDevices[comboBox1.SelectedIndex].MonikerString);
            //get new available frame from video source and start connecting
            videoSource.NewFrame += new NewFrameEventHandler(VideoSource_NewFrame);
            videoSource.Start();
        }

        private void VideoSource_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            //gets new frames and retrieves copies show in picture box1
            pictureBox1.Image = (Bitmap)eventArgs.Frame.Clone();
        }

        private void Resetall_click(object sender, EventArgs e)
        {
            //stop receiving frames from video source and set null to picture boxes
            videoSource.Stop();
            pictureBox1.Image = null;
            pictureBox1.Invalidate();
            pictureBox2.Image = null;
            pictureBox2.Invalidate();

        }
        private void Pause_click(object sender, EventArgs e)
        {
            //stop receiving frames
            videoSource.Stop();

        }
        private void Capture_click(object sender, EventArgs e)
        {
            //get the instant image of picturebox1 in picture box2
            pictureBox2.Image = (Bitmap)pictureBox1.Image.Clone();
        }
        private void exit_click(object sender, EventArgs e)
        {
            if(videoSource.IsRunning == true)
            {
                videoSource.Stop();
            }
            Application.Exit(null);
        }

        //private void pictureBox1_Click(object sender, EventArgs e)
        //{

        //}

        //private void label1_Click(object sender, EventArgs e)
        //{

        //}

        void serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //read line from serial port and call displaydata to show data
            port_data = _serialPort.ReadLine();
            this.Invoke(new EventHandler(displaydata));
        }
        private void displaydata(object sender, EventArgs e)
        {
            //dt = DateTime.Now;
            textBox2.Text= port_data;
        }
        private void displayAcc(object sender, EventArgs e)
        {

            textBox1.Text = myAcc;
        }
        private void displayTime(object sender, EventArgs e)
        {
            DateTime localDate = DateTime.Now;
            textBox3.Text = localDate.ToString();
        }
        private void displayLong(object sender, EventArgs e)
        {
            
            textBox4.Text = myLong;
        }
        private void displayLat(object sender, EventArgs e)
        {
   
            textBox5.Text = myLat;
        }
        private void displayMap(object sender, EventArgs e)
        {
          
            //map section!
            try
            {

                String myLat = "44°34'02.3\"N";// = strList[2];  //fix this when we know the formating 
                String myLong = "123°16'30.7\"W";// = strList[3];  //these are set to osu right now

                StringBuilder queryaddress = new StringBuilder();
                queryaddress.Append("http://maps.google.com/maps?q=");
                if (myLat != String.Empty)
                {
                    queryaddress.Append(myLat + ",+");
                }
                if (myLong != String.Empty)
                {
                    queryaddress.Append(myLong + ",+");
                }
                webBrowser1.Navigate(queryaddress.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void connect_button_Click(object sender, EventArgs e)
        {
            //initialize serial port class
            _serialPort = new SerialPort();
            _serialPort.PortName = "COM5";
            _serialPort.BaudRate = 9600;
            _serialPort.DataReceived += serialPort_DataReceived;
            try
            {
                _serialPort.Open();
                //textBox1.Text = "\n";
            }
            catch (Exception ex1)
            {
                MessageBox.Show(ex1.Message, "Error in Connecting.");
            }

        }
        private void disconnect_button_Click(object sender, EventArgs e)
        {
            try
            {
                _serialPort.Close();

            }
            catch (Exception ex2)
            {
                MessageBox.Show(ex2.Message, "Error in Disconnecting.");
            }
        }

        private void save_button_Click(object sender, EventArgs e)
        {
            try
            {
                string filepath = @"C:\Users\Jayee Li\Desktop\OSU\Capstone\IMU";
                string filename = "IMUdata.txt";
                System.IO.File.WriteAllText(filepath + filename, textBox1.Text);
                MessageBox.Show("Data has been saved to " + filepath);
            }
            catch(Exception ex3)
            {
                MessageBox.Show(ex3.Message, "Error in saving file.");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox2.Text == "" || comboBox3.Text == "")
                {
                    textBox2.Text = "Please select port # and baud rate.";
                }
                else
                {
                    serialPort1.PortName = comboBox2.Text;
                    serialPort1.BaudRate = Convert.ToInt32(comboBox3.Text);
                    serialPort1.Open();
                    button10.Enabled = true;
                   // button1.Enabled = true;
                    button6.Enabled = false;
                    serialPort1.DataReceived += new SerialDataReceivedEventHandler(MySerialPort_DataReceived);

                }
            }
            catch (UnauthorizedAccessException)
            {
                textBox2.Text = "Unauthorized Access";
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        void MySerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                port_data = serialPort1.ReadExisting();
                this.Invoke(new EventHandler(displaydata));
                String tempStr = textBox2.Text;
                String[] spearator = {","};
                strList = tempStr.Split(spearator, StringSplitOptions.RemoveEmptyEntries);
                //mySpeed = strList[0];
                myLat = strList[3];
                myLong = strList[4];
                myAcc = strList[1];
                this.Invoke(new EventHandler(displayLat));
                this.Invoke(new EventHandler(displayLong));
                this.Invoke(new EventHandler(displayAcc));
                this.Invoke(new EventHandler(displayTime));
               // this.Invoke(new EventHandler(displayMap));
              
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex);
            }
        }
        private void button10_Click(object sender, EventArgs e)
        {
            serialPort1.Close();
            button10.Enabled = false;
          //  button1.Enabled = false;
            button6.Enabled = true;
        }
    }
}
