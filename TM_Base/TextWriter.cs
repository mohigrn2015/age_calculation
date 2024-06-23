using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace TM_Console.TM_Base
{
    internal class TextWriter
    {
        private Object _LogFileLockFlag;
        private string _LogFileLocation;
        private System.Diagnostics.EventLog _Eventlog;

        internal TextWriter()
        {
            string fValue = ConfigurationManager.AppSettings["ReqLogDirectory"];

            _LogFileLockFlag = new object();

            if (!Directory.Exists(fValue))
            {
                _LogFileLocation = AppDomain.CurrentDomain.BaseDirectory;
            }
            else
            {
                _LogFileLocation = ConfigurationManager.AppSettings["LogDirectory"];
            }
        }

         
        public async Task LogAsync(string message)
        {
            try
            {
                string fileName =DateTime.Now.ToString("yyyy_MM_dd") + ".txt";

                // Use async StreamWriter to write the log file
                using (StreamWriter strm = File.CreateText(Path.Combine(_LogFileLocation, fileName)))
                {
                    await strm.WriteLineAsync(message);
                }
            }
            catch (Exception ex)
            {
                _Eventlog.WriteEntry("Log: " + ex.ToString(), System.Diagnostics.EventLogEntryType.Error);
            }
        }


        public async Task LogV2Async(string message)
        {
            try
            {
                string fValue = ConfigurationManager.AppSettings["LogDirectory"];

                if (!Directory.Exists(fValue))
                {
                    _LogFileLocation = AppDomain.CurrentDomain.BaseDirectory;
                }
                else
                {
                    _LogFileLocation = ConfigurationManager.AppSettings["LogDirectory"];
                }

                string fileName =DateTime.Now.ToString("yyyy_MM_dd") + ".txt";

                lock (_LogFileLockFlag)
                {
                    StreamWriter strm = File.CreateText(_LogFileLocation + "//" + fileName);
                    strm.Flush();
                    strm.Close();
                    File.AppendAllText(_LogFileLocation + "\\" + fileName, message + Environment.NewLine);
                }
            }
            catch (Exception ex)
            {
                _Eventlog.WriteEntry("Log: " + ex.ToString(), System.Diagnostics.EventLogEntryType.Error);
            }
        }

        internal async Task BAReqTouristSimLogAsync(string message, long token)
        {
            try
            {
                string fileName = token + "_" + DateTime.Now.ToString("yyyy_MM_dd") + ".txt";

                using (StreamWriter strm = File.CreateText(Path.Combine(_LogFileLocation, fileName)))
                {
                    await strm.WriteLineAsync(message);
                }
            }
            catch (Exception ex)
            {
                _Eventlog.WriteEntry("Log: " + ex.ToString(), System.Diagnostics.EventLogEntryType.Error);
            }
        }

    }
}
