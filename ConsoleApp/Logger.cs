﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class Logger
    {
        private Dictionary<DateTime, string> _logs = new Dictionary<DateTime, string>();

        public event PropertyChangedEventHandler? PropertyChanged;

        public event EventHandler<LoggerEventArgs>? MessageLogged;

        public void Log(string message)
        {
            var dateTime = DateTime.Now;
            _logs[dateTime] = message;
            MessageLogged?.Invoke(this, new LoggerEventArgs(dateTime, message));
        }

        public string GetLogs(DateTime from, DateTime to)
        {
            var result =  string.Join("\n",
                _logs.Where(x => x.Key >= from).Where(x => x.Key <= to)
                .Select(x => $"{x.Key.ToShortDateString()} {x.Key.ToShortTimeString()}: {x.Value}"));

            return result;
        }

        public Task<string> GetLogsAsync(DateTime from, DateTime to)
        {
            return Task.Run(() => GetLogs(from, to));
        }


        public class LoggerEventArgs : EventArgs
        {
            public DateTime DateTime { get; }
            public string Message { get; }

            public LoggerEventArgs(DateTime dateTime, string message)
            {
                DateTime = dateTime;
                Message = message;
            }
        }
    }
}
