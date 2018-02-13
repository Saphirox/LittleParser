using System.Net.Http;
using System.Windows;
using LittleParser.Application.Infrastructure;
using LittleParser.Services.Providers;
using LittleParser.Services.Providers.Impl;
using Microsoft.Win32;

namespace LittleParser.Application
{
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
                    await new LogFacade(_apacheLogParserProvider, client).ReadAndSendAsync(openFileDialog.FileName);
                }
            }

            Status.Text = "Success";
            
            Status.UpdateLayout();
            
            MessageBox.Show("Done");
        }
    }
}