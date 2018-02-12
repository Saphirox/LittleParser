using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows;
using LittleParser.Application.Infrastructure;
using LittleParser.Common.Constants;
using LittleParser.Models.Entities;
using LittleParser.Services.Facades;
using LittleParser.Services.Facades.Impl;
using Microsoft.Win32;

namespace LittleParser.Application
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private readonly IApacheLogParserFacade _apacheLogParserFacade; 
        
        public MainWindow()
        {
            _apacheLogParserFacade = new ApacheLogParserFacade();
            
            InitializeComponent();
        }

        private async void DownloadFileForParsing(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            
            if (openFileDialog.ShowDialog() == true)
            {
                Status.Text = "Wait please...";

                using (var client = new HttpClientFacade(new HttpClient()))
                {
                    await new LogReader(_apacheLogParserFacade, client).ReadAndSendAsync(openFileDialog.FileName);                
                }
            }

            Status.Text = "Success";
            
            Status.UpdateLayout();
            
            MessageBox.Show("Done");
        }
    }
}