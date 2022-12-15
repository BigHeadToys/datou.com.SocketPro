using datou.com.SocketPro.entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace datou.com.SocketPro
{
    public partial class FrmTCPClient : Form
    {
        public FrmTCPClient()
        {
            InitializeComponent();
            this.Load += FrmTCPServer_Load;
        }

        /*
         * 第一步：调用Socket()函数创建一个用于通信的套接字;
         * 第二步：通过设置套接字地址结构，说明客户端与之通信的服务器的IP地址和端口号;
         * 第三步：调用connect()函数来建立服务器连接;
         * 第四步：调用读写函数发送或者接收数据;
         * 第五步：终止连接
         */
        private void FrmTCPServer_Load(object sender, EventArgs e)
        {
            this.lst_Rcv.Columns[1].Width 
                = this.lst_Rcv.ClientSize.Width - this.lst_Rcv.Columns[0].Width;
        }

        //申明一个Socket对象
        private Socket socketClient;

        //创建字典集合，键是ClientIP,值是socketClient类型
        private Dictionary<string, Socket> CurrentClientlist = new Dictionary<string, Socket>();

        private void btn_Connect_Click(object sender, EventArgs e)
        {
            AddLog(0,"与服务器连接中");
            //第一步：调用Socket()函数创建一个用于通信的套接字;
            socketClient = new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);

            //第二步：通过设置套接字地址和结构，说明客户端与之通信的服务器的IP地址和端口号;
            IPEndPoint ipe = new IPEndPoint(IPAddress.Parse(this.txt_IP.Text.Trim()), int.Parse(this.txt_Port.Text.Trim()));

            //第三步：调用Connect()函数来建立与服务器的连接
            try
            {
                socketClient.Connect(ipe);
            }
            catch (Exception ex)
            {
                AddLog(2, "连接服务器失败：" + ex.Message);
                return;
            }

            //创建一个监听多线程
            Task.Run(new Action(() => {
                CheckReciveMsg();
            }));
            AddLog(0, "成功连接至服务器");

            this.btn_Connect.Enabled = false;
        }

        /// <summary>
        /// 检查有没有连接的信息
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private void CheckReciveMsg()
        {
            while (true)
            {
                //创建一个缓冲区
                byte[] buffer = new byte[1024 * 1024 * 2];//2MB
                int length = -1;

                //第四步：调用读写函数发送或者接收数据
                try
                {
                    length = socketClient.Receive(buffer);//阻塞式的
                }
                catch (Exception ex)
                {
                    AddLog(2, "服务器断开连接" + ex.Message);
                    break;//停止线程
                }

                if (length > 0)
                {
                    string msg = string.Empty;

                    MessageType type = (MessageType)buffer[0];

                    switch (type)
                    {
                        case MessageType.ASCII:

                            msg = Encoding.ASCII.GetString(buffer, 1, length - 1);

                            AddLog(0, "服务器：" + msg);

                            break;
                        case MessageType.UTF8:

                            msg = Encoding.UTF8.GetString(buffer, 1, length - 1);

                            AddLog(0, "服务器：" + msg);

                            break;
                        case MessageType.Hex:

                            msg = HexGetString(buffer, 1, length - 1);

                            AddLog(0, "服务器：" + msg);

                            break;
                        case MessageType.File:

                            Invoke(new Action(() =>
                            {
                                SaveFileDialog sfd = new SaveFileDialog();

                                sfd.Filter = "txt files(*.txt)|*.txt|xls files(*.xls)|*.xls|xlsx files(*.xlsx)|*.xlsx|All files(*.*)|*.*";

                                if (sfd.ShowDialog() == DialogResult.OK)
                                {
                                    string fileSavePath = sfd.FileName;

                                    using (FileStream fs = new FileStream(fileSavePath, FileMode.Create))
                                    {
                                        fs.Write(buffer, 1, length - 1);
                                    }

                                    AddLog(0, "文件成功保存至" + fileSavePath);
                                }
                            }));

                            break;
                        case MessageType.JSON:

                            Invoke(new Action(() =>
                            {
                                string res = Encoding.Default.GetString(buffer, 1, length);

                                List<Student> StuList = JSONHelper.JsonToEntity<List<Student>>(res);

                                new FrmShowJson(StuList).Show();

                                AddLog(0, "接收JSON数据：" + res);

                            }));

                            break;
                        default:
                            break;
                    }



                }
                else
                {
                    break;
                }
            }
        }


        #region 16进制字符串处理

        private string HexGetString(byte[] buffer, int start, int length)
        {
            string Result = string.Empty;

            if (buffer != null && buffer.Length >= start + length)
            {
                //截取字节数组

                byte[] res = new byte[length];

                Array.Copy(buffer, start, res, 0, length);

                string Hex = Encoding.Default.GetString(res, 0, res.Length);

                // 01   03 0 40 0A
                if (Hex.Contains(" "))
                {
                    string[] str = Regex.Split(Hex, "\\s+", RegexOptions.IgnoreCase);

                    foreach (var item in str)
                    {
                        Result += "0x" + item + " ";
                    }
                }
                else
                {
                    Result += "0x" + Hex;
                }

            }
            else
            {
                Result = "Error";
            }
            return Result;
        }


        #endregion


        #region 接受信息的方法

        //当前时间属性
        private string CurrentTime
        {
            get
            {
                return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            }
        }

        private void AddLog(int index, string info)
        {
            if (!this.lst_Rcv.InvokeRequired)//如果不是跨线程访问
            {
                ListViewItem lst = new ListViewItem(" " + CurrentTime, index);
                lst.SubItems.Add(info);
                lst_Rcv.Items.Insert(lst_Rcv.Items.Count, lst);//最后插入的放在最上面
            }
            else
            {
                Invoke(new Action(() =>
                {
                    ListViewItem lst = new ListViewItem(" " + CurrentTime, index);
                    lst.SubItems.Add(info);
                    lst_Rcv.Items.Insert(lst_Rcv.Items.Count, lst);//最后插入的放在最上面
                }));
            }
        }
        #endregion

        

        private void FrmTCPClient_FormClosing(object sender, FormClosingEventArgs e)
        {
            socketClient?.Close();
        }
        private void btn_DisConnect_Click(object sender, EventArgs e)
        {
            socketClient?.Close();
        }

        private void btn_SendAll_Click(object sender, EventArgs e)
        {

        }

        #region 发送JSON
        //用json发送数据耗费的流量小
        private void btn_SendJSON_Click(object sender, EventArgs e)
        {
            //创建集合，模拟从其地方获得的数据
            List<Student> stuList = new List<Student>()
            {
                new Student(){ StudentID=10001,StudentName="小明",ClassName="软件一班"},
                new Student(){ StudentID=10001,StudentName="小明",ClassName="软件二班"},
                new Student(){ StudentID=10001,StudentName="小明",ClassName="软件三班"},
                new Student(){ StudentID=10001,StudentName="小明",ClassName="软件四班"},
                new Student(){ StudentID=10001,StudentName="小明",ClassName="软件五班"},
                new Student(){ StudentID=10001,StudentName="小明",ClassName="软件六班"},
                new Student(){ StudentID=10001,StudentName="小明",ClassName="软件七班"}
            };
            string str = JSONHelper.EntityToJson(stuList);

            byte[] send = Encoding.Default.GetBytes(str);

            byte[] sendMsg = new byte[send.Length + 1];

            Array.Copy(send,0,sendMsg,1,send.Length);

            sendMsg[0] = (byte)MessageType.JSON;

            socketClient?.Send(sendMsg);

            AddLog(0,"发送JSON数据：" + str);

        }
        #endregion



        private void btn_SendASCII_Click(object sender, EventArgs e)
        {
            AddLog(0, "发送ASCII内容：" + this.Txt_Send.Text.Trim());
            byte[] send = Encoding.ASCII.GetBytes(this.Txt_Send.Text.Trim());

            //创建最终发送的数组
            byte[] sendMsg = new byte[send.Length + 1];

            //整体拷贝数组
            Array.Copy(send, 0, sendMsg, 1, send.Length);

            //给首字节赋值
            sendMsg[0] = (byte)MessageType.ASCII;

            socketClient?.Send(sendMsg);

            this.Txt_Send.Clear();
        }

        private void btn_SendUTF8_Click(object sender, EventArgs e)
        {
            AddLog(0, "发送UTF8内容：" + this.Txt_Send.Text.Trim());
            byte[] send = Encoding.UTF8.GetBytes(this.Txt_Send.Text.Trim());

            //创建最终发送的数组
            byte[] sendMsg = new byte[send.Length + 1];

            //整体拷贝数组
            Array.Copy(send, 0, sendMsg, 1, send.Length);

            //给首字节赋值
            sendMsg[0] = (byte)MessageType.UTF8;

            socketClient?.Send(sendMsg);

            this.Txt_Send.Clear();
        }

        private void btn_SendHex_Click(object sender, EventArgs e)
        {
            AddLog(0, "发送Hex内容：" + this.Txt_Send.Text.Trim());
            byte[] send = Encoding.Default.GetBytes(this.Txt_Send.Text.Trim());

            //创建最终发送的数组
            byte[] sendMsg = new byte[send.Length + 1];

            //整体拷贝数组
            Array.Copy(send, 0, sendMsg, 1, send.Length);

            //给首字节赋值
            sendMsg[0] = (byte)MessageType.Hex;

            socketClient?.Send(sendMsg);

            this.Txt_Send.Clear();
        }

        #region 选择文件
        private void btn_SelectFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            //设置默认的路径
            ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                this.txt_File.Text = ofd.FileName;
                AddLog(0,"选择文件："+ this.txt_File.Text);
            }
        }
        #endregion

        #region 发送文件
        private void btn_SendFile_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txt_File.Text))
            {
                MessageBox.Show("请先选择你要发送的文件路径", "发送文件");
                return;
            }
            else 
            {
                //发送两次
                using (FileStream fs = new FileStream(this.txt_File.Text, FileMode.Open))
                {
                    //第一次发送：发送文件名称，文件格式
                    //获取文件名称
                    string fileName = Path.GetFileName(this.txt_File.Text);
                    //获取后缀名
                    string fileExtension = Path.GetExtension(this.txt_File.Text);

                    string strMsg = "发送文件：" + fileName + "." + fileExtension;

                    byte[] send1 = Encoding.UTF8.GetBytes(strMsg);

                    byte[] send1Msg = new byte[send1.Length+1];

                    Array.Copy(send1,0,send1Msg,1,send1.Length);
                    //文件格式
                    send1Msg[0] = (byte)MessageType.UTF8;

                    socketClient?.Send(send1Msg);
                    
                    //第二次发送
                    //缓冲区
                    byte[] send2 = new byte[1024 * 1024 * 10];
                    //有效长度
                    int length = fs.Read(send2,0,send2.Length);

                    byte[] send2Msg = new byte[length + 1];

                    Array.Copy(send2,0,send2Msg,1,length);

                    send2Msg[0] = (byte)MessageType.File;

                    socketClient?.Send(send2Msg);

                    AddLog(0, strMsg);

                    this.txt_File.Clear();

                }
            }
        }
        #endregion
    }
}
