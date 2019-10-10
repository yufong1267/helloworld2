using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Shortest_Job_First
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        ProcessPPP[] process = new ProcessPPP[100];
        
        public int total_time = 0 , process_num = 0;
        public bool[] check = new bool[100];
        public string reg = "";

        int[] num = new int[100]; //宣告一個存取所有擷取出來的數字

        private void clear() {
            for(int i = 0; i < 100; i++)
            {
                check[i] = false;             
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //初始化
            for(int i = 0; i < 100; i++)
            {
                process[i] = null;
                check[i] = false;
                num[i] = 0;
            }

            //開啟檔案
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Select file";
            dialog.InitialDirectory = ".\\";
            // dialog.Filter = "xls files (*.*)|*.xls";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var sr = new StreamReader(dialog.FileName);
                reg = sr.ReadToEnd();
            }
            MessageBox.Show("" + reg);

            string reg1 = "";
            int num_index = 0;
            bool check2 = false;
            //做字串處理
            for (int i = 0; i < reg.Length - 1; i++)
            {
                check2 = false;
                if (reg[i] >= '0' && reg[i] <= '9')
                {
                    reg1 += reg[i];
                    check2 = true;

                }
                //如果下一位不是數字 就可以轉換了
                if ((reg[i + 1] <= '0' || reg[i + 1] >= '9') && check2)
                {
                    num[num_index] = Int32.Parse(reg1);
                    reg1 = "";
                    num_index++;
                }
            }

            
            total_time = 0;


            //input data
            /*process[0] = new ProcessPPP(3, 3);
            process[1] = new ProcessPPP(0, 6);
            process[2] = new ProcessPPP(1, 4);
            total_time = process[0].BurstTime + process[1].BurstTime + process[2].BurstTime;*/
            for(int i = 0; i < num_index/4; i++)
            {
                process[i] = new ProcessPPP(num[i * 4 + 2], num[i * 4 + 3]);
                total_time += process[i].BurstTime;
            }
            //process_num = 3;
            process_num = num_index / 4;
            /////////////////////////////////////////

            
            string final_ans = "";
            //計算每個時間點的工作process
            for (int i = 0; i < total_time; i++)
            {
                //每次都要重新計算重複的部分
                clear();
                for(int j = 0; j < process_num; j++)
                {
                    if(process[j].ArricalTime == i)
                    {
                        check[j] = true;
                    }
                }

                //獲得所有重複裡面BurstTime最小的那個process
                int minest_process = select_small();
                final_ans += "P" + (minest_process + 1) + "  "+ i + "~" + (i + 1) + "\n";
                //做起點跟距離的更新
                for(int j = 0; j < process_num; j++)
                {
                    if(check[j] == true && j == minest_process)
                    {
                        process[j].ArricalTime++;
                        process[j].BurstTime--;
                    }
                    else if(check[j] == true)
                    {
                        process[j].ArricalTime++;
                    }
                }
            }
            MessageBox.Show(final_ans);
        }

        public int select_small() {
            int r = 0 , min = 999999;
            for(int i = 0; i < process_num; i++)
            {
                if(check[i] == true && process[i].BurstTime < min && process[i].BurstTime > 0)
                {
                    min = process[i].BurstTime;
                    r = i;
                }
            }
            return r;
        }
    }
}
