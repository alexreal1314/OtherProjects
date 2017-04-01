using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;

namespace FacebookApp
{
    public sealed class LogFileRecorder
    {
        private static volatile LogFileRecorder s_FileRecorderInstance;
        private int m_ActionCount = 0;

        public static LogFileRecorder Instance
        {
            get
            {
                if (s_FileRecorderInstance == null)
                {
                    s_FileRecorderInstance = new LogFileRecorder();
                }

                return s_FileRecorderInstance;
            }
        }

        public string LogFilePath { get; set; }

        private LogFileRecorder()
        {
        }

        public void createLogFile(string i_UserLoggedIn)
        {         
            LogFilePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\FbAppLogFile.txt";

            using (StreamWriter sw = File.CreateText(LogFilePath))
            {
                sw.Write(string.Format("**Login** {0}: User Logged In as - {1}", DateTime.Now.ToString(), i_UserLoggedIn));
                sw.WriteLine();
            }
        }

        public void WriteActionToLogFile(string i_ControlPressed)
        {
            using (StreamWriter sw = new StreamWriter(LogFilePath, true))
            {
                sw.Write(string.Format("#{0}| {1}: {2}", m_ActionCount++, DateTime.Now.ToString(), i_ControlPressed));
                sw.WriteLine();
            }
        }
    }
}