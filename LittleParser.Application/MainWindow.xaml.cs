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
using Microsoft.Win32;

namespace LittleParser.Application
{
    using LittleParser.Services.Providers.Impl;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private readonly IApacheLogParserProvider _apacheLogParserProvider; 
        
        public MainWindow()
        {
            _apacheLogParserProvider = new ApacheLogParserProvider();
            
            InitializeComponent();
        }

        private async void DownloadFileForParsing(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            
            if (openFileDialog.ShowDialog() == true)
            {
                Status.Text = "Wait please...";

               using (var client = new HttpClientProvider(new HttpClient()))
               {
                    await new LogReader(_apacheLogParserProvider, client).ReadAndSendAsync(openFileDialog.FileName);                
               }

            }

            Status.Text = "Success";
            
            Status.UpdateLayout();
            
            MessageBox.Show("Done");
        }
    }
}