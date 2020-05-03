using System;
using System.Collections.Generic;
using System.IO;
//using System.Threading;
//using System.Windows.Forms;

namespace OurGame
{
    class FileLog
    {
        /// <summary> интервал записи данных из буфера в файл, мкс </summary>
        private const int WRITE_FILE_INTERVAL = 2000; 

        private string _fileName;

        List<string> _buffer;  // буфер для накопления сообщений

        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();

        public FileLog(string fileName1)
        {
            _fileName = fileName1;
                        
            if (!File.Exists(_fileName))
            {
                using (new StreamWriter(_fileName, false))
                {
                    // только создать файл, если его нет
                }
            }

            _buffer = new List<string>();

            // запуск таймера
            timer.Interval = WRITE_FILE_INTERVAL;
            timer.Tick += Timer_Tick;
            timer.Start();

            _buffer.Add("-------------------------------");
            _buffer.Add($"{DateTime.Now}: Начало сеанса.");
        }

        ~FileLog()
        {
            _buffer.Add($"{DateTime.Now}: Конец сеанса."); 

            Timer_Tick(null, null);   // переписать из буфера в файл

            timer.Stop();
        }
        
        /// <summary>
        /// Записать сообщение в лог
        /// </summary>
        /// <param name="msg">сообщение</param>
        public void Log(string msg)
        {            
            _buffer.Add($"{DateTime.Now}: {msg}");
        }

        private void Timer_Tick(object sender, EventArgs e)
        {

            if (_buffer.Count == 0) return; 
                      
            try
            {
                using (StreamWriter sw = new StreamWriter(_fileName, true))
                {
                    foreach (string str in _buffer)
                        sw.WriteLine(str);
                }
                _buffer.Clear();

            }
            catch (IOException exc)   // не знаю, что делать с исключениями
            {
                _buffer.Add(exc.Message);
            }

        }
        //public void 

    }
}
