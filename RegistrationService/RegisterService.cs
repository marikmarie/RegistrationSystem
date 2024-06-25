﻿using System;
using System.ServiceProcess;
using System.Timers;
using System.Configuration; // Add this for ConfigurationManager
using RegisterLibrary;
using System.IO;
using System.Data.SqlClient;

namespace RegistrationService
{
    public partial class RegisterService : ServiceBase
    {
        private System.Timers.Timer timer;
        private QueueProcessor queueProcessor;
        private DatabaseHelper registrationDB;
        private string connectionString;
        private string logFilePath = @"D:\Pegasus\Logs\Log.txt"; 


        public RegisterService()
        {
            InitializeComponent();

            queueProcessor = new QueueProcessor();

           
            connectionString = ConfigurationManager.AppSettings["ConnectionString"];

            registrationDB = new DatabaseHelper(connectionString);
        }

        protected override void OnStart(string[] args)
        {
            timer = new System.Timers.Timer();
            timer.Interval = 30000;
            timer.Elapsed += OnElapsedTime;
            timer.Start();

            Log("Service Started");
        }

        private void OnElapsedTime(object source, ElapsedEventArgs e)
        {
            try
            {
                RegistrationDetails details = queueProcessor.ReceiveMessage();
                if (details != null)
                {
                    registrationDB.RegisterUser(details);
                    Log($"User registered successfully: {details.Email}");
                }
            }
            catch (SqlException sqlEx)
            {
                Log($"SQL Error processing queue message: {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                Log($"Error processing queue message: {ex.Message}");
            }
        }

        protected override void OnStop()
        {
            timer.Stop();
            timer.Dispose(); 
            Log("Service Stopped");
        }

        private void Log(string message)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(logFilePath, true))
                {
                    string logEntry = $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} - {message}";
                    writer.WriteLine(logEntry);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to write to log: {ex.Message}");
            }
        }
    }
}
