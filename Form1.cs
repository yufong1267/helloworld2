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

namespace Round_Robin_Algorithm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public int Quantum = 0;
        public int[] order = new int[100];
        public int total_time = 0, process_num = 0;

        public string reg = ""; //拉進來的檔案 先放進string
        public int[] num = new int[100];
        private void button1_Click(object sender, EventArgs e)
        {
            ProcessPPP[] process = new ProcessPPP[100];
            //初始化
            for (int i = 0; i < 100; i++)
            {
                process[i] = null;
                order[i] = 0;
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
                if ((reg[i + 1] < '0' || reg[i + 1] > '9') && check2)
                {
                    num[num_index] = Int32.Parse(reg1);
                    reg1 = "";
                    num_index++;
                }
            }
            

            //input data
            //process_num = 3;
            process_num = (num_index - 1) / 3;
           /*  process[0] = new ProcessPPP(8);
             process[1] = new ProcessPPP(4);
             process[2] = new ProcessPPP(12);
             total_time = process[0].BurstTime + process[1].BurstTime + process[2].BurstTime;*/
            for (int i = 0; i < ((num_index-1)) / 3; i++)
            {
                process[i] = new ProcessPPP(num[i * 2 + 1]);
                order[i] = num[process_num * 2 + i];
                total_time += process[i].BurstTime;
            }

            /*  order[0] = 3;
              order[1] = 1;
              order[2] = 2;*/
            //Quantum = 4;
            Quantum = num[num_index-1];
            ////////////////////////

            /*
             計算每個時間點是誰的process在工作
             */
            string final_answer = "";
            int index_counter = 0;
            bool check = false; //檢查是不是都有做到輸出，不然結果會少
            int time_jump = Quantum;
            for(int i = 0; i < total_time; i += time_jump)
            {
                check = false;
                while(!check)
                {
                      if (process[order[index_counter % process_num] - 1].BurstTime > 0)
                      {
                               if(process[order[index_counter % process_num] - 1].BurstTime >= Quantum)
                                {
                                  final_answer += "second " + i + "~" + (i + Quantum) + " process p" + order[index_counter % process_num];
                                  final_answer += "\n";
                                  process[order[index_counter % process_num] - 1].BurstTime -= Quantum;
                                  check = true; //做出結果可以跳出
                                  time_jump = Quantum;
                                }
                                 else
                                 {
                            int cc = process[order[index_counter % process_num] - 1].BurstTime;
                            final_answer += "second " + i + "~" + (i + cc) + " process p" + order[index_counter % process_num];
                            final_answer += "\n";
                            process[order[index_counter % process_num] - 1].BurstTime -= cc;
                            check = true; //做出結果可以跳出
                            time_jump = cc;
                        }
                               
                      }
                         index_counter++;
                }
            }
            MessageBox.Show(final_answer);


        }
    }
}
