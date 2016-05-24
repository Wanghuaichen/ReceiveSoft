using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using System.Xml;

namespace YYApp
{
    public partial class SetHLJForm : DevComponents.DotNetBar.Metro.MetroForm
    {
        WriteReadXML wrxml = new WriteReadXML();
        public SetHLJForm()
        {
            InitializeComponent();
        }



        private void SetHLJForm_Load(object sender, EventArgs e)
        {
            wrxml.SetPath(System.Windows.Forms.Application.StartupPath + "/Resave_hlj.xml");
            if (wrxml.GetPath() == System.Windows.Forms.Application.StartupPath + "/System.xml")
            {
                DevComponents.DotNetBar.MessageBoxEx.Show("û���ҵ�ת����Ϣ��xml�ļ�������ϵϵͳ�����Ա!");
            }
            else 
            {
                string Type, Sourc, DataBase, UserName, PassWord;
                wrxml.ReadDBXML(out Type,out Sourc ,out DataBase ,out UserName ,out PassWord );
                textBox_Source.Text =Sourc ;
                textBox_DataBase.Text=DataBase ;
                textBox_UserName.Text=UserName;
                textBox_PassWord.Text = PassWord;
            }
        }

        int ConState = 0; //0Ĭ��  1�ɹ�   2ʧ��
        private void button_OK_Click(object sender, EventArgs e)
        {
            string Msg = DBValidate();
            if (Msg != "")
            {
                DevComponents.DotNetBar.MessageBoxEx.Show(Msg, "[��ʾ]", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                
                string Type = "MSSQL";

                if (wrxml.GetPath() == System.Windows.Forms.Application.StartupPath + "/System.xml")
                {
                    DevComponents.DotNetBar.MessageBoxEx.Show("û���ҵ�ת����Ϣ��xml�ļ�������ϵϵͳ�����Ա!");
                }
                else
                {
                    wrxml.WriteDBXML(Type, textBox_Source.Text.Trim(), textBox_DataBase.Text.Trim(), textBox_UserName.Text.Trim(), textBox_PassWord.Text.Trim());

                    Service._51Data dt = new Service._51Data(wrxml.GetPath());
                    try
                    {
                        dt.conn.Open();
                        dt.conn.Close();
                        DevComponents.DotNetBar.MessageBoxEx.Show("ת����Ϣ���óɹ�,��������������ͨѶ����!");
                        ConState = 1;
                    }
                    catch
                    {
                        DevComponents.DotNetBar.MessageBoxEx.Show("ת����Ϣ�����޷�����ת���,����������!");
                        ConState = 2;
                    }
                    
                }
            }

        }

        private string DBValidate()
        {
            string Msg = "";
            if (textBox_Source.Text.Trim() == "")
            {
                Msg = "����д���������ƣ�" + "\n";
            }
            if (textBox_DataBase.Text.Trim() == "")
            {
                Msg += "����д���ݿ����ƣ�" + "\n";
            }
            if (textBox_UserName.Text.Trim() == "")
            {
                Msg += "����д�û�����" + "\n";
            }

            return Msg;
        }

        private void button_Close_Click(object sender, EventArgs e)
        {
            if (ConState == 2)
            {
                if (DevComponents.DotNetBar.MessageBoxEx.Show(" ת�����ݿ����Ӳ���ʧ�ܣ�ȷ�Ϲر����ô��ڣ�", "[��ʾ]", MessageBoxButtons.YesNo) == DialogResult.Yes)
                { this.Close(); }
            }
            else { this.Close(); }
            
        }

    }
}