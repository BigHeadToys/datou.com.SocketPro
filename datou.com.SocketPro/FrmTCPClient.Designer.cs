namespace datou.com.SocketPro
{
    partial class FrmTCPClient
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTCPClient));
            this.label1 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btn_SelectFile = new System.Windows.Forms.Button();
            this.txt_File = new System.Windows.Forms.TextBox();
            this.Txt_Send = new System.Windows.Forms.TextBox();
            this.lst_Rcv = new System.Windows.Forms.ListView();
            this.infoTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.info = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.btn_DisConnect = new System.Windows.Forms.Button();
            this.btn_SendJSON = new System.Windows.Forms.Button();
            this.btn_SendUTF8 = new System.Windows.Forms.Button();
            this.btn_SendFile = new System.Windows.Forms.Button();
            this.btn_SendASCII = new System.Windows.Forms.Button();
            this.btn_SendHex = new System.Windows.Forms.Button();
            this.Txt_UserName = new System.Windows.Forms.TextBox();
            this.btn_Connect = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_Port = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_IP = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("黑体", 22.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(747, 84);
            this.label1.TabIndex = 0;
            this.label1.Text = "TCP传输客户端";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(-2, 96);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btn_SelectFile);
            this.splitContainer1.Panel1.Controls.Add(this.txt_File);
            this.splitContainer1.Panel1.Controls.Add(this.Txt_Send);
            this.splitContainer1.Panel1.Controls.Add(this.lst_Rcv);
            this.splitContainer1.Panel1.Padding = new System.Windows.Forms.Padding(10);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.btn_DisConnect);
            this.splitContainer1.Panel2.Controls.Add(this.btn_SendJSON);
            this.splitContainer1.Panel2.Controls.Add(this.btn_SendUTF8);
            this.splitContainer1.Panel2.Controls.Add(this.btn_SendFile);
            this.splitContainer1.Panel2.Controls.Add(this.btn_SendASCII);
            this.splitContainer1.Panel2.Controls.Add(this.btn_SendHex);
            this.splitContainer1.Panel2.Controls.Add(this.Txt_UserName);
            this.splitContainer1.Panel2.Controls.Add(this.btn_Connect);
            this.splitContainer1.Panel2.Controls.Add(this.label4);
            this.splitContainer1.Panel2.Controls.Add(this.label3);
            this.splitContainer1.Panel2.Controls.Add(this.txt_Port);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Panel2.Controls.Add(this.txt_IP);
            this.splitContainer1.Size = new System.Drawing.Size(737, 414);
            this.splitContainer1.SplitterDistance = 499;
            this.splitContainer1.TabIndex = 2;
            // 
            // btn_SelectFile
            // 
            this.btn_SelectFile.Location = new System.Drawing.Point(383, 367);
            this.btn_SelectFile.Name = "btn_SelectFile";
            this.btn_SelectFile.Size = new System.Drawing.Size(105, 35);
            this.btn_SelectFile.TabIndex = 3;
            this.btn_SelectFile.Text = "选择文件";
            this.btn_SelectFile.UseVisualStyleBackColor = true;
            this.btn_SelectFile.Click += new System.EventHandler(this.btn_SelectFile_Click);
            // 
            // txt_File
            // 
            this.txt_File.Location = new System.Drawing.Point(10, 372);
            this.txt_File.Multiline = true;
            this.txt_File.Name = "txt_File";
            this.txt_File.Size = new System.Drawing.Size(359, 25);
            this.txt_File.TabIndex = 2;
            // 
            // Txt_Send
            // 
            this.Txt_Send.Location = new System.Drawing.Point(10, 223);
            this.Txt_Send.Multiline = true;
            this.Txt_Send.Name = "Txt_Send";
            this.Txt_Send.Size = new System.Drawing.Size(479, 126);
            this.Txt_Send.TabIndex = 1;
            // 
            // lst_Rcv
            // 
            this.lst_Rcv.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.infoTime,
            this.info});
            this.lst_Rcv.Dock = System.Windows.Forms.DockStyle.Top;
            this.lst_Rcv.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lst_Rcv.HideSelection = false;
            this.lst_Rcv.Location = new System.Drawing.Point(10, 10);
            this.lst_Rcv.Margin = new System.Windows.Forms.Padding(10);
            this.lst_Rcv.Name = "lst_Rcv";
            this.lst_Rcv.Size = new System.Drawing.Size(479, 200);
            this.lst_Rcv.SmallImageList = this.imageList1;
            this.lst_Rcv.TabIndex = 0;
            this.lst_Rcv.UseCompatibleStateImageBehavior = false;
            this.lst_Rcv.View = System.Windows.Forms.View.Details;
            // 
            // infoTime
            // 
            this.infoTime.Text = "日志时间";
            this.infoTime.Width = 180;
            // 
            // info
            // 
            this.info.Text = "日志信息";
            this.info.Width = 200;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "info.ico");
            this.imageList1.Images.SetKeyName(1, "warning.ico");
            this.imageList1.Images.SetKeyName(2, "error.ico");
            // 
            // btn_DisConnect
            // 
            this.btn_DisConnect.Location = new System.Drawing.Point(126, 180);
            this.btn_DisConnect.Name = "btn_DisConnect";
            this.btn_DisConnect.Size = new System.Drawing.Size(100, 30);
            this.btn_DisConnect.TabIndex = 18;
            this.btn_DisConnect.Text = "断开服务器";
            this.btn_DisConnect.UseVisualStyleBackColor = true;
            this.btn_DisConnect.Click += new System.EventHandler(this.btn_DisConnect_Click);
            // 
            // btn_SendJSON
            // 
            this.btn_SendJSON.Location = new System.Drawing.Point(10, 324);
            this.btn_SendJSON.Name = "btn_SendJSON";
            this.btn_SendJSON.Size = new System.Drawing.Size(100, 30);
            this.btn_SendJSON.TabIndex = 17;
            this.btn_SendJSON.Text = "发送JSON";
            this.btn_SendJSON.UseVisualStyleBackColor = true;
            this.btn_SendJSON.Click += new System.EventHandler(this.btn_SendJSON_Click);
            // 
            // btn_SendUTF8
            // 
            this.btn_SendUTF8.Location = new System.Drawing.Point(126, 230);
            this.btn_SendUTF8.Name = "btn_SendUTF8";
            this.btn_SendUTF8.Size = new System.Drawing.Size(100, 30);
            this.btn_SendUTF8.TabIndex = 16;
            this.btn_SendUTF8.Text = "发送UTF8";
            this.btn_SendUTF8.UseVisualStyleBackColor = true;
            this.btn_SendUTF8.Click += new System.EventHandler(this.btn_SendUTF8_Click);
            // 
            // btn_SendFile
            // 
            this.btn_SendFile.Location = new System.Drawing.Point(126, 278);
            this.btn_SendFile.Name = "btn_SendFile";
            this.btn_SendFile.Size = new System.Drawing.Size(100, 30);
            this.btn_SendFile.TabIndex = 15;
            this.btn_SendFile.Text = "发送文件";
            this.btn_SendFile.UseVisualStyleBackColor = true;
            this.btn_SendFile.Click += new System.EventHandler(this.btn_SendFile_Click);
            // 
            // btn_SendASCII
            // 
            this.btn_SendASCII.Location = new System.Drawing.Point(10, 228);
            this.btn_SendASCII.Name = "btn_SendASCII";
            this.btn_SendASCII.Size = new System.Drawing.Size(100, 30);
            this.btn_SendASCII.TabIndex = 14;
            this.btn_SendASCII.Text = "发送ASCII";
            this.btn_SendASCII.UseVisualStyleBackColor = true;
            this.btn_SendASCII.Click += new System.EventHandler(this.btn_SendASCII_Click);
            // 
            // btn_SendHex
            // 
            this.btn_SendHex.Location = new System.Drawing.Point(10, 276);
            this.btn_SendHex.Name = "btn_SendHex";
            this.btn_SendHex.Size = new System.Drawing.Size(100, 30);
            this.btn_SendHex.TabIndex = 13;
            this.btn_SendHex.Text = "发送Hex";
            this.btn_SendHex.UseVisualStyleBackColor = true;
            this.btn_SendHex.Click += new System.EventHandler(this.btn_SendHex_Click);
            // 
            // Txt_UserName
            // 
            this.Txt_UserName.Location = new System.Drawing.Point(116, 113);
            this.Txt_UserName.Name = "Txt_UserName";
            this.Txt_UserName.Size = new System.Drawing.Size(103, 28);
            this.Txt_UserName.TabIndex = 9;
            this.Txt_UserName.Text = "大头模型";
            // 
            // btn_Connect
            // 
            this.btn_Connect.Location = new System.Drawing.Point(10, 180);
            this.btn_Connect.Name = "btn_Connect";
            this.btn_Connect.Size = new System.Drawing.Size(100, 30);
            this.btn_Connect.TabIndex = 4;
            this.btn_Connect.Text = "连接服务器";
            this.btn_Connect.UseVisualStyleBackColor = true;
            this.btn_Connect.Click += new System.EventHandler(this.btn_Connect_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 116);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 18);
            this.label4.TabIndex = 5;
            this.label4.Text = "用户名称:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 18);
            this.label3.TabIndex = 3;
            this.label3.Text = "服务器端口:";
            // 
            // txt_Port
            // 
            this.txt_Port.Location = new System.Drawing.Point(116, 66);
            this.txt_Port.Name = "txt_Port";
            this.txt_Port.Size = new System.Drawing.Size(103, 28);
            this.txt_Port.TabIndex = 2;
            this.txt_Port.Text = "8001";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 18);
            this.label2.TabIndex = 1;
            this.label2.Text = "服务器IP：";
            // 
            // txt_IP
            // 
            this.txt_IP.BackColor = System.Drawing.Color.White;
            this.txt_IP.Location = new System.Drawing.Point(116, 21);
            this.txt_IP.Name = "txt_IP";
            this.txt_IP.Size = new System.Drawing.Size(103, 28);
            this.txt_IP.TabIndex = 0;
            this.txt_IP.Text = "127.0.0.1";
            // 
            // FrmTCPClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(747, 509);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("黑体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FrmTCPClient";
            this.Text = "基于Socket实现TCP客户端";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmTCPClient_FormClosing);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListView lst_Rcv;
        private System.Windows.Forms.TextBox Txt_Send;
        private System.Windows.Forms.Button btn_SelectFile;
        private System.Windows.Forms.TextBox txt_File;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_IP;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_Port;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btn_Connect;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ColumnHeader infoTime;
        private System.Windows.Forms.ColumnHeader info;
        private System.Windows.Forms.TextBox Txt_UserName;
        private System.Windows.Forms.Button btn_SendJSON;
        private System.Windows.Forms.Button btn_SendUTF8;
        private System.Windows.Forms.Button btn_SendFile;
        private System.Windows.Forms.Button btn_SendASCII;
        private System.Windows.Forms.Button btn_SendHex;
        private System.Windows.Forms.Button btn_DisConnect;
    }
}

