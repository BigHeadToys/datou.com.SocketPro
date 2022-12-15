using datou.com.SocketPro.entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
    /// <summary>
    /// 方便转换和判断
    /// </summary>
    public enum MessageType
    {
        ASCII,
        UTF8,
        Hex,
        File,
        JSON
    }
    public partial class FrmTCPServer : Form
    {
        public FrmTCPServer()
        {
            InitializeComponent();
            this.Load += FrmTCPServer_Load;
        }

        private void FrmTCPServer_Load(object sender, EventArgs e)
        {
            this.lst_Rcv.Columns[1].Width 
                = this.lst_Rcv.ClientSize.Width - this.lst_Rcv.Columns[0].Width;
        }

        //申明一个Socket对象
        private Socket socketServer;

        //创建字典集合，键是ClientIP,值是socketClient类型
        private Dictionary<string, Socket> CurrentClientlist = new Dictionary<string, Socket>();


        /// <summary>
        /// 开启服务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_StartService_Click(object sender, EventArgs e)
        {
            //第一步：调用Socket()函数创建一个用于通信的套接字
            socketServer = new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);

            //第二步:给已创建的套接字绑定一个端口号，这一般通过设置网络套接字接口地址和调用Bind()函数来实现
            IPEndPoint ipe = new IPEndPoint(IPAddress.Parse(this.txt_IP.Text),int.Parse(this.txt_Port.Text));
            
            try
            {
                socketServer.Bind(ipe);
            }
            catch (Exception ex)
            {
                //写入日志
                AddLog(2,"服务器开启失败，原因：" + ex.Message);
                return;
            }
            //第三步：调用Listen()函数使套接字成为一个监听套接字
            socketServer.Listen(10);//10是指缓冲池的大小

            //第四步：创建一个监听的线程
            Task.Run(new Action(() => {
                CheckListening();
            }));
            AddLog(0,"服务器开启成功");

            this.btn_StartService.Enabled = false;
        }

        #region 监听线程
        /// <summary>
        /// 检查监听的线程方法体
        /// </summary>
        private void CheckListening()
        {
            while (true)
            {
                //第四步：调用Accept()函数来接受客户端连接，这时就可以跟客户端通信了
                Socket socketClient = socketServer.Accept();//阻塞式
                string client = socketClient.RemoteEndPoint.ToString();//获取IP地址
                AddLog(0,client+"上线了");
                CurrentClientlist.Add(client,socketClient);
                UpdateOnline(client,true);
                Task.Run(new Action(() =>
                {
                    ReciveMessage(socketClient);
                }));
            }
        }
        #endregion

        #region 16进制字符串处理
        private string HexGetString(byte[] buffer, int start, int length)
        {
            string Result = string.Empty;
            if (buffer != null && buffer.Length > start + length)
            {
                //截取字节数组
                byte[] res = new byte[length];

                Array.Copy(buffer, start, res, 0, length);

                string Hex = Encoding.Default.GetString(res, 0, res.Length);

                if (Hex.Contains(" "))
                {
                    string[] str = Regex.Split(Hex, "\\s", RegexOptions.IgnoreCase);

                    foreach (var item in str)
                    {
                        Result += "0x" + item + "";
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

        #region 多线程接收数据
        /// <summary>
        /// 接受客户端数据的方法
        /// </summary>
        /// <param name="socketClient"></param>
        private void ReciveMessage(Socket socketClient)
        {
            while (true)
            {
                //创建一个缓冲区
                byte[] buffer  = new byte[1024*1024*2];//2MB
                int length = -1;
                string client = socketClient.RemoteEndPoint.ToString();
                //第五步：处理客户端请求
                try
                {
                    length = socketClient.Receive(buffer);//阻塞式的
                }
                catch (Exception)
                {
                    UpdateOnline(client, false);
                    AddLog(0, client + "下线了");
                    CurrentClientlist.Remove(client);
                    break;//停止线程
                }
                string msg = string.Empty;
                if (length > 0)
                {
                    //处理
                    MessageType type = (MessageType)buffer[0];
                    switch (type)
                    {
                        case MessageType.ASCII:
                            msg = Encoding.ASCII.GetString(buffer, 1, length - 1);
                            AddLog(0,client=":"+msg);
                            break;
                        case MessageType.UTF8:
                            msg = Encoding.UTF8.GetString(buffer, 1, length - 1);
                            AddLog(0, client = ":" + msg);
                            break;
                        case MessageType.Hex:
                            msg = HexGetString(buffer, 1, length - 1);
                            AddLog(0, client = ":" + msg);
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
                            Invoke(new Action(() => {
                                string res = Encoding.Default.GetString(buffer, 1, length);
                                List<Student> stuList = JSONHelper.JsonToEntity<List<Student>>(res);
                                new FrmShowJson(stuList).Show();
                                AddLog(0, "接收JSON数据" + res);
                            }));
                            break;
                        default:
                            break;
                    }
                    //string msg = Encoding.Default.GetString(buffer, 0, length);
                    AddLog(0, "来自" + client + ":" + msg);

                }
                else 
                {
                    UpdateOnline(client, false);
                    AddLog(0, client + "下线了");
                    break;//停止线程
                }
                
            }
        }
        #endregion

        #region 接受信息的方法

        //当前时间属性
        private string CurrentTime
        {
            get { 
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
                //invoke方法的初衷是为了解决在某个非某个控件创建的线程中刷新该控件可能会引发异常的问题
                Invoke(new Action(() =>
                {
                    ListViewItem lst = new ListViewItem(" " + CurrentTime, index);
                    lst.SubItems.Add(info);
                    lst_Rcv.Items.Insert(lst_Rcv.Items.Count, lst);//最后插入的放在最上面
                }));
            }
        }
        #endregion


        #region  在线列表更新
        private void UpdateOnline(string client,bool operate)
        {
            if (!this.lst_Online.InvokeRequired)//如果不是跨线程访问
            {
                if (operate)
                {
                    this.lst_Online.Items.Add(client);
                }
                else
                {
                    foreach (string item in this.lst_Online.Items)
                    {
                        if (item == client)
                        {
                            this.lst_Online.Items.Remove(item);
                            break;
                        }
                    }
                }
            }
            else 
            {
                //Delegate的Invoke其实就是从线程池中调用委托方法执行，Invoke是同步的方法，会卡住调用它的UI线程
                Invoke(new Action(() =>
                {
                    if (operate)
                    {
                        this.lst_Online.Items.Add(client);
                    }
                    else {
                        foreach (string item in this.lst_Online.Items)
                        {
                            if (item == client)
                            {
                                this.lst_Online.Items.Remove(item);
                                break;
                            }
                        }
                    }
                }));
            }
        }
        #endregion



        private void btn_SendMsg_Click(object sender, EventArgs e)
        {
            if (this.lst_Online.SelectedItem != null)
            {
                AddLog(0,"发送内容：" + this.Txt_Send.Text.Trim());
                foreach (var item in this.lst_Online.SelectedItems)
                {
                    string client = item.ToString();
                    CurrentClientlist[client].Send(Encoding.Default.GetBytes(this.Txt_Send.Text.Trim()));
                }  
            }
            else 
            {
                MessageBox.Show("请选择你要发送的客户端对象！","发送消息");
            }
            
        }


        private void btn_Client_Click(object sender, EventArgs e)
        {
            new FrmTCPClient().Show();
        }

        private void btn_SendJSON_Click(object sender, EventArgs e)
        {

        }

        private void btn_SendASCII_Click(object sender, EventArgs e)
        {
            if (this.lst_Online.SelectedItems.Count > 0)
            {
                AddLog(0, "发送ASCII内容：" + this.Txt_Send.Text.Trim());
                byte[] send = Encoding.ASCII.GetBytes(this.Txt_Send.Text.Trim());

                //创建最终发送的数组
                byte[] sendMsg = new byte[send.Length + 1];

                //整体拷贝数组
                Array.Copy(send, 0, sendMsg, 1, send.Length);

                //给首字节赋值
                sendMsg[0] = (byte)MessageType.ASCII;

                foreach (var item in this.lst_Online.SelectedItems)
                {
                    string client = item.ToString();
                    CurrentClientlist[client].Send(sendMsg);
                }
                this.Txt_Send.Clear();
            }
            else
            {
                MessageBox.Show("请选择你要发送的客户端对象！","发送消息");
            }
        }

        private void btn_UTF8_Click(object sender, EventArgs e)
        {
            if (this.lst_Online.SelectedItems.Count > 0)
            {
                AddLog(0, "发送内容：" + this.Txt_Send.Text.Trim());

                byte[] send = Encoding.UTF8.GetBytes(this.Txt_Send.Text.Trim());

                //创建最终发送的数组
                byte[] sendMsg = new byte[send.Length + 1];

                //整体拷贝数组
                Array.Copy(send, 0, sendMsg, 1, send.Length);

                //给首字节赋值

                sendMsg[0] = (byte)MessageType.UTF8;

                foreach (var item in this.lst_Online.SelectedItems)
                {
                    //获取Socket对象
                    string client = item.ToString();

                    CurrentClientlist[client]?.Send(sendMsg);
                }

                this.Txt_Send.Clear();
            }
            else
            {
                MessageBox.Show("请选择你要发送的客户端对象！", "发送消息");
            }
        }

        private void btn_SendHex_Click(object sender, EventArgs e)
        {
            if (this.lst_Online.SelectedItems.Count > 0)
            {
                AddLog(0, "发送内容：" + this.Txt_Send.Text.Trim());
                byte[] send = Encoding.Default.GetBytes(this.Txt_Send.Text.Trim());
                //创建最终发送的数组
                byte[] sendMsg = new byte[send.Length + 1];
                //整体拷贝数组
                Array.Copy(send, 0, sendMsg, 1, send.Length);
                //给首字节赋值
                sendMsg[0] = (byte)MessageType.Hex;
                foreach (var item in this.lst_Online.SelectedItems)
                {
                    //获取Socket对象
                    string client = item.ToString();
                    CurrentClientlist[client]?.Send(sendMsg);
                }
                this.Txt_Send.Clear();
            }
            else
            {
                MessageBox.Show("请选择你要发送的客户端对象！", "发送消息");
            }
        }

        private void btn_SendFile_Click(object sender, EventArgs e)
        {

        }
    }
}
