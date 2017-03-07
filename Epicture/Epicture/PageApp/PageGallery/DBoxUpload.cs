using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using System.Diagnostics;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Storage.Pickers;
using Windows.Storage;
using Imgur.API.Authentication.Impl;
using Imgur.API.Models;
using System.IO;
using Imgur.API.Endpoints.Impl;

namespace Epicture.PageApp.PageGallery
{
    class DBoxUpload
    {
        private Grid _principal;
        private Button _btn_upload;
        private bool _explorateur_windows;
        private string _clientID;
        private string _clientSECRET;

        // --- Constructeur
        public DBoxUpload(String name, ref Grid parent, int row, SolidColorBrush color, string clientID, string clientSECRET)
        {
            _clientID = clientID;
            _clientSECRET = clientSECRET;
            initGridPrincipal(name, ref parent, color, row);
            initButtom();
        }

        // --- Fonction Initilisation.
        private void initGridPrincipal(String name, ref Grid parent, SolidColorBrush color, int row)
        {
            this._principal = new Grid();
            this._principal.Name = name;
            _principal.Background = color;

            ColumnDefinition rowVide = new ColumnDefinition();
            rowVide.Width = new GridLength(10, GridUnitType.Star);
            _principal.ColumnDefinitions.Add(rowVide);

            ColumnDefinition rowBtnUpload = new ColumnDefinition();
            rowBtnUpload.Width = new GridLength(1, GridUnitType.Star);
            _principal.ColumnDefinitions.Add(rowBtnUpload);

            if (parent != null)
            {
                Grid.SetRow(this._principal, row);
                parent.Children.Add(this._principal);
            }
        }

        private void initButtom()
        {
            this._btn_upload = new Button();
            this._btn_upload.Name = "btn_upload";
            Image image = new Image();
            image.Source = new BitmapImage(new Uri("http://www.icone-png.com/png/2/1785.png"));
            this._btn_upload.Content = image;
            this._btn_upload.VerticalAlignment = VerticalAlignment.Center;
            this._btn_upload.HorizontalAlignment = HorizontalAlignment.Center;
            this._btn_upload.Click += _btn_upload_Click;
            if (this._principal != null)
            {
                Grid.SetColumn(this._btn_upload, 2);
                _principal.Children.Add(this._btn_upload);
            }
        }

        private void _btn_upload_Click(object sender, RoutedEventArgs e)
        {
            if (this._explorateur_windows == false)
            {
                Debug.WriteLine("Button upload Click a été click.");
                RecupererFile();
            }
            
        }

        private async void RecupererFile()
        {
            StorageFile file_upload;
            this._explorateur_windows = true;
            var filePicker = new FileOpenPicker();
            filePicker.ViewMode = PickerViewMode.Thumbnail;
            filePicker.FileTypeFilter.Add(".jpeg");
            filePicker.FileTypeFilter.Add(".jpg");
            filePicker.FileTypeFilter.Add(".png");

            file_upload = await filePicker.PickSingleFileAsync();

            if (file_upload != null)
            {
                Debug.WriteLine("Name file: " + file_upload.Name);
                Debug.WriteLine("Name file:" + file_upload.Path);
                this._explorateur_windows = false;
                var client = new ImgurClient(this._clientID, this._clientSECRET);
                var endpoint = new ImageEndpoint(client);
                var file = System.IO.File.ReadAllBytes(@file_upload.Path);
                var image = await endpoint.UploadImageBinaryAsync(file, "" , file_upload.Name, "");
            }
            this._explorateur_windows = false;
        }
    }
}
