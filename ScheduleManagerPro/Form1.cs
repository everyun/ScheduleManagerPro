using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScheduleManagerPro
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public int[] daysInMonthOfYeapyear = { 31, 29, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
        public int[] daysInMonthOfNonyeapYear = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
        public int firstTime = 0;

    public bool isLeapYear(int year)
        {
            if (year % 4 == 0)
            {
                return true;
            }
            else
                return false;
        }
        public int getFirstDayIndexOfWeekOfThisMonth(DateTime time)
        {
            DateTime dt = time;    
            DateTime firstDayOfThisMonth = dt.AddDays(1 - (dt.Day));
            return Convert.ToInt32(firstDayOfThisMonth.DayOfWeek);
        }

        private void Form1_Load_1()
        {
            DateTime stime = dateTimePicker1.Value;
            string[] weekDays = new string[] {
                "星期一","星期二","星期三","星期四","星期五","星期六","星期日"
            };
            Button[] weekButtons = new Button[7];
            Button[] buttons = new Button[43];
            int firstDayIndex = getFirstDayIndexOfWeekOfThisMonth(stime) + 1;
            for (int i = 0; i< weekButtons.Length; i++)
            {
                weekButtons[i] = new Button();
                weekButtons[i].Name = "weekButtions"+i;
                weekButtons[i].Text = weekDays[i];
                weekButtons[i].Location = new Point(200 + i * 120, 0);
                weekButtons[i].Click += new EventHandler(Week_Buttons_Click);
                weekButtons[i].Size = new System.Drawing.Size(120, 40);
            }
            this.Controls.AddRange(weekButtons);
           // MessageBox.Show(firstDayIndex.ToString());
            for (int i = 1; i < buttons.Length; i++)
            {
                int DayofThisMonth = isLeapYear(stime.Year) ? daysInMonthOfYeapyear[stime.Month] : daysInMonthOfNonyeapYear[stime.Month];
                buttons[i] = new Button();
                if(i >= firstDayIndex)
               { 
                    if(i <= DayofThisMonth +  firstDayIndex)
                    {
                        buttons[i].Text = (i - firstDayIndex + 1).ToString();
                    }
                }
                buttons[i].Name = "dayButton" + i;
                buttons[i].Location = new Point(200 + (i-1) % 7 * 120, 50 + (i-1) / 7 * 102);
                buttons[i].Click += new EventHandler(Buttons_Click);
                buttons[i].Size = new System.Drawing.Size(120, 100);
            }
            if(firstTime != 0)
            {
                for(int i = 1; i <= 42; i++)
                {
                    this.Controls.RemoveByKey("dayButton" + i);
                }
            }
            this.Controls.AddRange(buttons);
            buttons = null;
            firstTime++;
        }
        public void Buttons_Click(object sender, EventArgs e)
        {
            this.Text = (sender as Button).Text;
        }
        public void Week_Buttons_Click(object sender, EventArgs e)
        {
           // MessageBox.Show(getFirstDayOfThisMonth().ToString());
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            Form1_Load_1();
        }
    }
}
