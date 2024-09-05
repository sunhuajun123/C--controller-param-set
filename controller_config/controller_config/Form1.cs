using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

namespace controller_config
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            textBox1.Text = "0";
            textBox2.Text = "0";
            textBox3.Text = "0";
            textBox4.Text = "5";
            textBox5.Text = "0";
            textBox6.Text = "50";
            textBox7.Text = "0";
            textBox8.Text = "5";
            textBox9.Text = "1";
            textBox10.Text = "3";
            textBox13.Text = "2";
            textBox12.Text = "6";
            textBox11.Text = "4";
            timer1.Enabled = false;
            serialPort1.DataReceived += new SerialDataReceivedEventHandler(DataReceiveHandler);
        }

        private void DataReceiveHandler(object sender,SerialDataReceivedEventArgs e)
        {
            int data_len = serialPort1.BytesToRead;
            byte[] data_buffer = new byte[100];
            byte buffer_index = 0,check_sum=0;
            serialPort1.Read(data_buffer,0,data_len);

            try
            {
                Invoke((new Action(() => {
                    if (data_len >= 9)
                    {
                        /*寻找帧头*/
                        foreach (byte data_a6 in data_buffer)
                        {
                            if (data_a6 == 0xa6)
                            {
                                break;
                            }
                            buffer_index++;
                            if (buffer_index >= 5)
                            {
                                return;
                            }
                        }
                        for (byte i = buffer_index; i < (data_len - buffer_index - 2); i++)
                        {
                            check_sum += data_buffer[i];
                        }
                        if (check_sum == data_buffer[data_len - buffer_index - 2] && data_buffer[buffer_index + 1] == 0x15)
                        {
                            if (data_buffer[buffer_index + 3] == 3)//write
                            {
                                if (data_buffer[buffer_index + 4] == 0x41 && data_buffer[buffer_index + 5] == 0x43 && data_buffer[buffer_index + 6] == 0x4b)
                                {
                                    textBox14.Text = "写数据成功";
                                }
                            }
                            else if (data_buffer[buffer_index + 3] == 0x0c)//read
                            {
                                textBox1.Text = Convert.ToString(data_buffer[buffer_index + 4]);
                                textBox2.Text = Convert.ToString(data_buffer[buffer_index + 5]);
                                textBox3.Text = Convert.ToString(data_buffer[buffer_index + 6]);
                                textBox4.Text = Convert.ToString(data_buffer[buffer_index + 13]);
                                textBox5.Text = Convert.ToString(data_buffer[buffer_index + 14]);
                                textBox6.Text = Convert.ToString(data_buffer[buffer_index + 15]);
                                textBox7.Text = Convert.ToString(data_buffer[buffer_index + 8] & 0x0f);
                                textBox8.Text = Convert.ToString(data_buffer[buffer_index + 9] >> 4 & 0x0f);
                                textBox9.Text = Convert.ToString(data_buffer[buffer_index + 10] & 0x0f);
                                textBox10.Text = Convert.ToString(data_buffer[buffer_index + 10] >> 4 & 0x0f);
                                textBox13.Text = Convert.ToString(data_buffer[buffer_index + 11] & 0x0f);
                                textBox12.Text = Convert.ToString(data_buffer[buffer_index + 11] >> 4 & 0x0f);
                                textBox11.Text = Convert.ToString(data_buffer[buffer_index + 12] & 0x0f);
                                /*Sport 功能*/
                                byte type_cov = data_buffer[buffer_index + 7];
                                type_cov &= 0x01;
                                if (type_cov == 0x01)
                                {
                                    radioButton6.Checked = true;
                                }
                                else
                                {
                                    radioButton5.Checked = true;
                                }
                                type_cov = data_buffer[buffer_index + 7];
                                type_cov &= 0x02;
                                if (type_cov == 0x02)
                                {
                                    radioButton8.Checked = true;
                                }
                                else
                                {
                                    radioButton7.Checked = true;
                                }
                                type_cov = data_buffer[buffer_index + 7];
                                type_cov &= 0x04;
                                if (type_cov == 0x04)
                                {
                                    radioButton10.Checked = true;
                                }
                                else
                                {
                                    radioButton9.Checked = true;
                                }
                                type_cov = data_buffer[buffer_index + 7];
                                type_cov &= 0x08;
                                if (type_cov == 0x08)
                                {
                                    radioButton12.Checked = true;
                                }
                                else
                                {
                                    radioButton11.Checked = true;
                                }
                                type_cov = data_buffer[buffer_index + 7];
                                type_cov &= 0x10;
                                if (type_cov == 0x10)
                                {
                                    radioButton14.Checked = true;
                                }
                                else
                                {
                                    radioButton13.Checked = true;
                                }
                                type_cov = data_buffer[buffer_index + 7];
                                type_cov &= 0x20;
                                if (type_cov == 0x20)
                                {
                                    radioButton16.Checked = true;
                                }
                                else
                                {
                                    radioButton15.Checked = true;
                                }
                                type_cov = data_buffer[buffer_index + 7];
                                type_cov &= 0x40;
                                if (type_cov == 0x40)
                                {
                                    radioButton17.Checked = true;
                                }
                                else
                                {
                                    radioButton18.Checked = true;
                                }
                                type_cov = data_buffer[buffer_index + 7];
                                type_cov &= 0x80;
                                if (type_cov == 0x80)
                                {
                                    radioButton20.Checked = true;
                                }
                                else
                                {
                                    radioButton19.Checked = true;
                                }
                                /*转向灯*/
                                type_cov = data_buffer[buffer_index + 8];
                                type_cov &= 0x10;
                                if (type_cov == 0x10)
                                {
                                    radioButton22.Checked = true;
                                }
                                else
                                {
                                    radioButton21.Checked = true;
                                }
                                /*速度单位及电机转向*/
                                type_cov = data_buffer[buffer_index + 9];
                                type_cov &= 0x01;
                                if (type_cov == 0x01)
                                {
                                    radioButton3.Checked = true;
                                }
                                else
                                {
                                    radioButton4.Checked = true;
                                }
                                type_cov = data_buffer[buffer_index + 9];
                                type_cov &= 0x02;
                                if (type_cov == 0x02)
                                {
                                    radioButton1.Checked = true;
                                }
                                else
                                {
                                    radioButton2.Checked = true;
                                }
                                textBox14.Text = "读数据成功";
                            }
                            else
                            {
                                textBox14.Text = "无效操作";
                            }
                        }
                    }
                })));
            }
            catch(Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen == false)//串口处于关闭状态时
            {
                comboBox1.Items.Clear();//清除serialbox_serialnum框内容
                foreach (string str in SerialPort.GetPortNames())
                {
                    if (comboBox1.Items.Count < 11)//限制串口数量最多到com10
                    {
                        comboBox1.Items.Add(str);
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen == false)
            {
                try
                {
                    serialPort1.PortName = comboBox1.Text;
                    serialPort1.BaudRate = Convert.ToInt32(comboBox2.Text);
                    serialPort1.DataBits = 8;
                    serialPort1.Parity = Parity.None;
                    serialPort1.StopBits = StopBits.One;
                    serialPort1.Open();
                    if (button2.Text == "打开串口")
                    {
                        button2.Text = "关闭串口";
                    }
                }
                catch
                {
                    MessageBox.Show("IOT COM open failed!", "warning");
                }
            }
            else if (serialPort1.IsOpen == true)
            {
                try
                {
                    serialPort1.Close();
                    if (button2.Text == "关闭串口")
                    {
                        button2.Text = "打开串口";
                    }
                }
                catch
                {
                    System.Media.SystemSounds.Asterisk.Play();
                    MessageBox.Show("IOT COM close failed!", "warning");
                }
            }
        }

        byte[] Get_MotorParam_Command_Buffer = new byte[6] { 0xA6,0x51,0xFF,0x00,0xF6,0x6A};
        byte[] Set_MotorParam_Command_Buffer = new byte[100];
        private void Protocol_Get_MotorParam()
        {
            serialPort1.Write(Get_MotorParam_Command_Buffer,0,6);
        }
        
        private void Protocol_Set_MotorParam()
        {
            byte protocol_basic_mode = 0, protocol_taillight_mode = 0, data_buf=0, check_sum=0;
            byte protocol_motor_set1 = 0, protocol_motor_set2 = 0, protocol_motor_set3 = 0, protocol_motor_set4 = 0;
            /*basic mode value remap*/
            if (radioButton6.Checked == true)
            {
                protocol_basic_mode |= (1<<0);
            }
            else
            {
                protocol_basic_mode &= 0xfe;
            }
            if (radioButton8.Checked == true)
            {
                protocol_basic_mode |= (1 << 1);
            }
            else
            {
                protocol_basic_mode &= 0xfd;
            }
            if (radioButton10.Checked == true)
            {
                protocol_basic_mode |= (1 << 2);
            }
            else
            {
                protocol_basic_mode &= 0xfb;
            }
            if (radioButton12.Checked == true)
            {
                protocol_basic_mode |= (1 << 3);
            }
            else
            {
                protocol_basic_mode &= 0xf7;
            }
            if (radioButton14.Checked == true)
            {
                protocol_basic_mode |= (1 << 4);
            }
            else
            {
                protocol_basic_mode &= 0xef;
            }
            if (radioButton16.Checked == true)
            {
                protocol_basic_mode |= (1 << 5);
            }
            else
            {
                protocol_basic_mode &= 0xdf;
            }
            if (radioButton17.Checked == true)
            {
                protocol_basic_mode |= (1 << 6);
            }
            else
            {
                protocol_basic_mode &= 0xbf;
            }
            if (radioButton20.Checked == true)
            {
                protocol_basic_mode |= (1 << 7);
            }
            else
            {
                protocol_basic_mode &= 0x7f;
            }

            data_buf = Convert.ToByte(textBox7.Text);
            if (radioButton22.Checked == true)
            {
                protocol_taillight_mode = (1 << 4);
                protocol_taillight_mode += data_buf;
            }
            else
            {
                protocol_taillight_mode = data_buf;
            }
            /*速度单位、电机转向，电机HALL0参数*/
            if (radioButton3.Checked == true)//速度单位
            {
                protocol_motor_set1 |= 1;
            }
            else
            {
                protocol_motor_set1 &= 0xfe;
            }
            if (radioButton1.Checked == true)//电机转动方向
            {
                protocol_motor_set1 |= 0x02;
            }
            else
            {
                protocol_motor_set1 &= 0xfd;
            }
            data_buf = Convert.ToByte(textBox8.Text);
            data_buf <<= 4;
            protocol_motor_set1 |= data_buf;
            /*电机HALL1-2参数*/
            data_buf = Convert.ToByte(textBox9.Text);
            protocol_motor_set2 |= data_buf;
            data_buf = Convert.ToByte(textBox10.Text);
            data_buf <<= 4;
            protocol_motor_set2 |= data_buf;
            /*电机HALL3-4参数*/
            data_buf = Convert.ToByte(textBox13.Text);
            protocol_motor_set3 |= data_buf;
            data_buf = Convert.ToByte(textBox12.Text);
            data_buf <<= 4;
            protocol_motor_set3 |= data_buf;
            /*电机HALL5参数*/
            protocol_motor_set4 = Convert.ToByte(textBox11.Text);

            Set_MotorParam_Command_Buffer[0] = 0xA6;
            Set_MotorParam_Command_Buffer[1] = 0x51;
            Set_MotorParam_Command_Buffer[2] = 0xFF;
            Set_MotorParam_Command_Buffer[3] = 0x0C;            
            Set_MotorParam_Command_Buffer[4] = Convert.ToByte(textBox1.Text);
            Set_MotorParam_Command_Buffer[5] = Convert.ToByte(textBox2.Text);
            Set_MotorParam_Command_Buffer[6] = Convert.ToByte(textBox3.Text);
            Set_MotorParam_Command_Buffer[7] = protocol_basic_mode;
            Set_MotorParam_Command_Buffer[8] = protocol_taillight_mode;
            Set_MotorParam_Command_Buffer[9] = protocol_motor_set1;
            Set_MotorParam_Command_Buffer[10] = protocol_motor_set2;
            Set_MotorParam_Command_Buffer[11] = protocol_motor_set3;
            Set_MotorParam_Command_Buffer[12] = protocol_motor_set4;
            Set_MotorParam_Command_Buffer[13] = Convert.ToByte(textBox4.Text);
            Set_MotorParam_Command_Buffer[14] = Convert.ToByte(textBox5.Text);
            Set_MotorParam_Command_Buffer[15] = Convert.ToByte(textBox6.Text);
            for (byte buffer_index = 0; buffer_index < 16; buffer_index++)
            {
                check_sum += Set_MotorParam_Command_Buffer[buffer_index];
            }
            Set_MotorParam_Command_Buffer[16] = check_sum;
            Set_MotorParam_Command_Buffer[17] = 0x6A;
            serialPort1.Write(Set_MotorParam_Command_Buffer, 0, 18);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
                      
        }
        protected override void WndProc(ref Message m)//USB插拔处理
        {
            try
            {
                if (m.Msg == 0x0219)
                {
                    if (m.WParam.ToInt32() == 0x8004) // usb串口拔出
                    {
                        try
                        {
                            if (button2.Text == "关闭")
                            {
                                serialPort1.Close();
                                button2.Text = "打开";
                            }
                        }
                        catch
                        {
                            if (serialPort1.IsOpen == true)
                            {
                                serialPort1.Close();
                            }
                            MessageBox.Show("检测到USB工具插拔", "error");
                        }
                    }
                }
                base.WndProc(ref m);
            }
            catch
            {
                MessageBox.Show("检测到USB工具插拔", "error");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen == false)
            {
                MessageBox.Show("请先打开串口", "警告！！！");
            }
            else 
            {
                Protocol_Get_MotorParam();
            }
        }


        private void button4_Click_1(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen == false)
            {
                MessageBox.Show("请先打开串口", "警告！！！");
            }
            else
            {
                Protocol_Set_MotorParam();
            }
        }
    }
}
