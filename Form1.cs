using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace B10517025
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //存取從text裡面讀取的數字
        private String HW_Message = "123";
        private int send_Time = 0; //紀錄寄送幾次

        //儲存地點 起點 / 終點 / 內容
        private Process2[] res = new Process2[100];
        private Process2[] des = new Process2[100];
        private String[] content = new String[100];
        private Mailsheil communicate_place = null;

        private String final_ans = "";


        private  void button1_Click(object sender, EventArgs e)
        {
            /* Process2 p1 = null;
             p1 = new Process2(1, "asc");
             p1.Start();
             MessageBox.Show("" + p1.Id);*/

            //Store data reset
            Data_Reset();

            //預計用共同存取的變數來當成每次的傳輸data
            //預先處理拿進來的資料  分成來源跟終點、內容  第一個index當成(a + index)來源        
            Message_preset();

            //建立共同的存取地方
            communicate_place = new Mailsheil();
            

           /* //Process傳送資料
            Message_transfer();*/

            
            
        }

        private void Message_transfer() {
            //顯示哪個pid跟process要寄送甚麼內容
            for (int i = 0; i < 2; i++)
            {
                final_ans += res[i].Id + " " + res[i].process_name + " send " + content[i] + "\n";
            }
            //顯示哪個process/pid要接收甚麼資料
            for (int i = 0; i < 2; i++)
            {
                final_ans += des[i].Id + " " + res[i].process_name + " get " + content[i] + " from process " + res[i].Id + " " + res[i].process_name + "\n";
            }
        }

        private void Data_Reset() {
            final_ans = "";

              for (int i = 0; i < 100; i++)
            {
                res[i] = null;
                des[i] = null;
                content[i] = "";
            }
        }

        private void Message_preset() {

            try
            {
                using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "Text Documents|*.txt", Multiselect = false, ValidateNames = true })
                {
                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        using (StreamReader sr = new StreamReader(ofd.FileName, Encoding.GetEncoding(950), true))
                        {

                            HW_Message = sr.ReadToEnd();

                            //確認有沒有讀取到
                            //MessageBox.Show("" + HW_Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("讀檔失敗!!!!");
            }
            int store_index = 0;
            string[] a_whole_new_world = HW_Message.Split('\n');
            
            foreach(string reg in a_whole_new_world)
            {
                //確認可以分成2行
                //MessageBox.Show("" + reg);
                string[] Aladdin = reg.Split(',');
                int count = 0; //判斷要變成des / src / content
                count = 0;
                foreach(string reg2 in Aladdin)
                {
                    //MessageBox.Show("" + reg2);
                    switch(count)
                    {
                        case 0:
                            //賦予一個名稱 並啟動
                            res[store_index] = new Process2(""+reg2);
                            res[store_index].Start();
                            break;
                        case 1:
                            //賦予一個名稱 並啟動
                            des[store_index] = new Process2("" + reg2);
                            des[store_index].Start();
                            break;
                        case 2:
                            content[store_index] = "" + reg2;
                            break;
                    }

                    count++;
                }
                store_index++; //儲存下一組       
                send_Time++;
            }
            

            //顯示哪個pid跟process要寄送甚麼內容
            for (int i = 0; i < store_index; i++)
            {
                final_ans += res[i].Id + " " + res[i].process_name + " send " + content[i] + "\n";
            }
            //顯示哪個process/pid要接收甚麼資料
            for (int i = 0; i < store_index; i++)
            {
                final_ans += des[i].Id + " " + res[i].process_name + " get " + content[i] + " from process " + res[i].Id + " " + res[i].process_name + "\n";
            }

            MessageBox.Show("" + final_ans);

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
