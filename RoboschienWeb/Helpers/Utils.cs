using NLog;
using System;

namespace RoboschienWeb.Helpers
{
    public class Utils
    {

        private static Utils Instance=null;

        private Logger logger = null;
        public static Utils GetInstance()
        {
            if (Instance==null)
            {
                Instance = new Utils();

            }
            return Instance;
        }

        private Utils()
        {

        }
        public string getReferenceNumber()
        {
            lock (this)
            {
                Random rnd = new Random();

                long milliseconds = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
                string ReferenceNumber=""+milliseconds + rnd.Next(0, 9999).ToString("0000");

                //DateTime TodayDsate = DateTime.Now;

                //string ReferenceNumber = TodayDate.Year.ToString() + TodayDate.Month.ToString("00")
                //            + TodayDate.Day.ToString("00") + TodayDate.Hour.ToString("00") + TodayDate.Minute.ToString("00") +
                //            TodayDate.Second.ToString("00") + TodayDate.Millisecond.ToString("000") + rnd.Next(0, 999).ToString("000");
                return ReferenceNumber;
            }

        }

        public string getTimeStamp()
        {
            return DateTime.Now.ToString();
        }


        public Logger GetLogger()
        {
            if (logger==null)
            {
                logger= LogManager.GetCurrentClassLogger();
            }
            return logger;
        }

        public static string FirstLetterToUpper(string str)
        {
            if (str == null)
                return null;

            if (str.Length > 1)
                return char.ToUpper(str[0]) + str.Substring(1);

            return str.ToUpper();
        }

    }
}
