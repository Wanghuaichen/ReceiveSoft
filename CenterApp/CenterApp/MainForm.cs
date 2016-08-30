using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro.ColorTables;
using DevComponents.DotNetBar;
using System.Threading;

namespace CenterApp
{
    public delegate void delegateHandler();
    public partial class MainForm : DevComponents.DotNetBar.Metro.MetroAppForm
    {

        private void Register() 
        {
            //DevComponents.DotNetBar.MessageBoxEx.Show("ϵͳ���δ����뼤���ʹ�ã�", "[��ʾ]", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        
        public MainForm()
        {
            InitializeComponent();

            

            SetFormStyle();

            handle += new delegateHandler(OperationFun);

            Thread adddata = new Thread(new ThreadStart(ThreadAddData));
            adddata.IsBackground = true;
            adddata.Start();

            metroTabItem1_Click(null,null);

           
            SetStyle( Program.wrx.XMLObj.Style);


            TcpControl.TcpClient_Init(Program.wrx.XMLObj.UiTcpModel.IP, Program.wrx.XMLObj.UiTcpModel.PORT.ToString());


            ControlInit();

            Register();
        }
        
        LogInControl _LogInControl = null;
        SetControl _SetControl = null;
        private void ControlInit() 
        {
            _LogInControl = new LogInControl(styleManager1);
            _LogInControl.Dock = System.Windows.Forms.DockStyle.Fill;
            pageSliderPage1.Controls.Add(_LogInControl);
            _LogInControl.Show();


            _SetControl = new SetControl(styleManager1);
            _SetControl.Dock = System.Windows.Forms.DockStyle.Fill;
            pageSliderPage2.Controls.Add(_SetControl);
            _SetControl.Show();
        }

        private delegate void DelegateAddData();
        private void ThreadAddData()
        {
            while (true)
            {
                // �ж��Ƿ���ҪInvoke�����߳�ʱ��Ҫ
                if (this.InvokeRequired)
                {
                    try
                    {
                        // ͨ��ί�е���д���߳̿ؼ��ĳ��򣬴��ݲ�������object������
                        this.Invoke(new DelegateAddData(AddData));
                    }
                    catch { }
                }
                Thread.Sleep(100);
            }
        }

        private void AddData()
        {
            lock (TcpControl.DataQueue)
            {
                //while (TcpControl.DataQueue.Count > 0)
                //{
                //    //changeSTR  = TcpControl.DataQueue.Dequeue();
                //}
            }

        }


        #region ���յ�����ͼƬ�����������changeSTR
        private string str = null;
        public delegateHandler handle;
        public string changeSTR
        {
            get { return str; }
            set
            {
                str = value;
                handle();
            }
        }
        public void OperationFun()
        {
            if (changeSTR == "++++")
            {
                
            }
            else
            {
                

                //this.TopMost = true;
                //if (this.WindowState == FormWindowState.Minimized)
                //    this.WindowState = FormWindowState.Normal;
            }
        }
        
        #endregion

        #region ���ý�����
        MetroColorGeneratorParameters[] metroThemes;
        private void SetStyle(string style) 
        {
            foreach (var mt in metroThemes)
            {
                if (mt.ThemeName == style)
                {
                    this.styleManager1.MetroColorParameters = mt;
                    SetControlStyle();
                    return;
                }
            }
        }
        private void SetFormStyle() 
        {
            metroThemes = MetroColorGeneratorParameters.GetAllPredefinedThemes();
            foreach (MetroColorGeneratorParameters mt in metroThemes)
            {
                ButtonItem theme = new ButtonItem(mt.ThemeName, mt.ThemeName);
                theme.Click += new EventHandler(theme_Click);
                colorThemeButton.SubItems.Add(theme);
            }
        }
        private void SetControlStyle() 
        {
            //if (_ProfileControl != null)
            //{
            //    _ProfileControl.SetStyle(styleManager1);
            //}
            if (_LogInControl != null)
            {
                _LogInControl.SetStyle(styleManager1);
            }
            if (_SetControl != null) 
            {
                _SetControl.SetStyle(styleManager1);
                if (_SetControl._NetworkControl != null)
                { _SetControl._NetworkControl.SetStyle(styleManager1); }
                if (_SetControl._SetDBControl != null)
                { _SetControl._SetDBControl.SetStyle(styleManager1); }
                if (_SetControl._SetWebControl != null)
                { _SetControl._SetWebControl.SetStyle(styleManager1); }
            }
        }
        void theme_Click(object sender, EventArgs e)
        {
            SetStyle( ((ButtonItem)sender).Name);
            Program.wrx.XMLObj.Style = ((ButtonItem)sender).Name;
            Program.wrx.WriteXML();
        }
        #endregion


        private void metroTabItem1_Click(object sender, EventArgs e)
        {
            pageSlider1.SelectedPageIndex = 0;
        }

        private void metroTabItem2_Click(object sender, EventArgs e)
        {
            pageSlider1.SelectedPageIndex = 1;
        }

        private void metroTabItem3_Click(object sender, EventArgs e)
        {

        }

        private void metroTabItem4_Click(object sender, EventArgs e)
        {
            chartControlInit();
        }

        private void metroTabItem5_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        
        
       
        #region ��С�������̡���ԭ
        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Show();
                this.ShowInTaskbar = true;
                this.WindowState = FormWindowState.Normal;
                notifyIcon1.Visible = false;
            }
        }

        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized) //�ж��Ƿ���С��
            {
                this.ShowInTaskbar = false;  //����ʾ��ϵͳ������
                notifyIcon1.Visible = true;  //����ͼ��ɼ�
            }
        }


        protected override void WndProc(ref Message msg)
        {
            if (msg.Msg == 0x0112 && ((int)msg.WParam == 0xF060))
            {
                this.TopMost = false;
                // ���winform���Ϲرհ�ť 
                // ������Ҫ���߼�����
                DialogResult dr = DevComponents.DotNetBar.MessageBoxEx.Show("ȷ��Ҫ�ر�ϵͳ��\r\n\r\n�رա�Yes��  ��С����NO��", "[��ʾ]", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (dr == DialogResult.Yes) { System.Environment.Exit(0); }
                else
                { this.WindowState = FormWindowState.Minimized; }
                
            }
            base.WndProc(ref msg);
        }
        #endregion

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (TcpControl.Connected)
            {
                labelX1.BackgroundImage = global::CenterApp.Properties.Resources.Network_yes;
           
            }
            else
            { labelX1.BackgroundImage = global::CenterApp.Properties.Resources.Network_no; }
            if (Service.PublicBD.ConnectState)
            { 
                labelX4.BackgroundImage = global::CenterApp.Properties.Resources.data_yes;
                DataClass.GetCenterInfo();
            }
            else
            { labelX4.BackgroundImage = global::CenterApp.Properties.Resources.data_no; }
        }

        chartControl _chartControl = null;
        private void buttonItem2_Click(object sender, EventArgs e)
        {
            chartControlInit();
        }
        private void chartControlInit()
        {
            if (_chartControl != null)
            {
                _chartControl.Dispose();
            }
            pageSliderPage4.Controls.Clear();
            _chartControl = new chartControl(styleManager1);
            _chartControl.Dock = System.Windows.Forms.DockStyle.Fill;
            pageSliderPage4.Controls.Add(_chartControl);
            _chartControl.Show();
            pageSlider1.SelectedPageIndex = 3;
            metroTabItem4.Checked = true;
        }

        registerControl _registerControl = null;
        private void buttonItem3_Click(object sender, EventArgs e)
        {
            registerControlInit();
        }
        private void registerControlInit()
        {
            if (_registerControl != null)
            {
                _registerControl.Dispose();
            }
            pageSliderPage4.Controls.Clear();
            _registerControl = new registerControl(styleManager1);
            _registerControl.Dock = System.Windows.Forms.DockStyle.Fill;
            pageSliderPage4.Controls.Add(_registerControl);
            _registerControl.Show();
            pageSlider1.SelectedPageIndex = 3;
            metroTabItem4.Checked = true;
        }

        RTUCountControl _RTUCountControl = null;
        private void buttonItem4_Click(object sender, EventArgs e)
        {
            RTUCountControlInit();
        }
        private void RTUCountControlInit()
        {
            if (_RTUCountControl != null)
            {
                _RTUCountControl.Dispose();
            }
            pageSliderPage4.Controls.Clear();
            _RTUCountControl = new RTUCountControl(styleManager1);
            _RTUCountControl.Dock = System.Windows.Forms.DockStyle.Fill;
            pageSliderPage4.Controls.Add(_RTUCountControl);
            _RTUCountControl.Show();
            pageSlider1.SelectedPageIndex = 3;
            metroTabItem4.Checked = true;
        }
    }
}