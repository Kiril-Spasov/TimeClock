using System;
using System.IO;
using System.Windows.Forms;

namespace TimeClock
{
    public partial class FormTimeClock : Form
    {
        public FormTimeClock()
        {
            InitializeComponent();
        }


        int totalTimeInMins = 0;

        private void BtnCalculate_Click(object sender, EventArgs e)
        {
            string line1 = "";
            string line2 = "";

            string path = Application.StartupPath + @"\clock.txt";
            StreamReader streamReader = new StreamReader(path);

            bool finished = false;

            while (!finished)
            {
                line1 = streamReader.ReadLine();
                line2 = streamReader.ReadLine();

                if (line1 == null || line2 == null)
                {
                    finished = true;
                }
                else
                {
                    CalculateTime(line1, line2);
                    Display();
                }
            }
        }

        private void CalculateTime(string time1, string time2)
        {

            int hour1 = Convert.ToInt32(time1.Substring(0, 2));
            int hour2 = Convert.ToInt32(time2.Substring(0, 2));
            int mins1 = Convert.ToInt32(time1.Substring(3, 2));
            int mins2 = Convert.ToInt32(time2.Substring(3, 2));

            //Convert time in mins.
            int time1InMins = 0;
            int time2InMins = 0;

            time1InMins = hour1 * 60 + mins1;
            time2InMins = hour2 * 60 + mins2;

            //For example if the first time is 09:13 and second 04:42.
            //We need to add 12h to second time to make it 16:42.
            if (hour1 > hour2)
            {
                totalTimeInMins = (time2InMins + 12 * 60) - time1InMins;
            }
            else
            {
                totalTimeInMins = time2InMins - time1InMins;
            }

        }

        private void Display()
        {
            string result = "";

            int hours = totalTimeInMins / 60;
            int mins = totalTimeInMins % 60;

            if (totalTimeInMins > 480)
            {
                hours = 8;
                mins = 0;
            }

            result += hours < 10 ? "0" + hours.ToString() : hours.ToString();

            result += ":";

            result += mins < 10 ? "0" + mins.ToString() : mins.ToString();

            TxtResult.Text +=  result + Environment.NewLine;
        }
    }
}
