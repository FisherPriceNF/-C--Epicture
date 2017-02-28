using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using System.Diagnostics;

namespace Epicture
{
    class DBoxUpload
    {
        private Grid _principal;
        private Button _btn_upload;

        // --- Constructeur
        public DBoxUpload(String name, ref Grid parent, int row, SolidColorBrush color)
        {
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
            this._btn_upload.Content = "Upload";
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
            Debug.WriteLine("Button upload Click a été click.");
        }
    }
}
