using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;


// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Epicture
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private DBoxSearch _boxSearch;
        private Image _imageTest;
        private DGallery _gallery;
        private DBoxUpload _upload;
        private FlipView _filpView;
      
        // --- Constructeur.  
        public MainPage()
        {
            this.InitializeComponent();
            //this.initImageTest();

            this.setColumn();
            SolidColorBrush colorBoxSearch = new SolidColorBrush();
            colorBoxSearch.Color = Colors.Green;
            _boxSearch = new DBoxSearch("BoxSearch", ref Principal, 0, colorBoxSearch);

            SolidColorBrush colorGallery = new SolidColorBrush();
            colorGallery.Color = Color.FromArgb(255, 0, 102, 204);
            _gallery = new DGallery("Gallery", ref Principal, 1, colorGallery);

            SolidColorBrush colorUpload = new SolidColorBrush();
            colorUpload.Color = Color.FromArgb(255, 204, 0, 204);
            _upload = new DBoxUpload("BoxUpload", ref Principal, 2, colorUpload);

        }

        // --- Setteur.
        private void setColumn()
        {  
            RowDefinition rowBoxSearch = new RowDefinition();
            rowBoxSearch.Height = new GridLength(1, GridUnitType.Star);
            Principal.RowDefinitions.Add(rowBoxSearch);

            RowDefinition rowGallery = new RowDefinition();
            rowGallery.Height = new GridLength(10, GridUnitType.Star);
            Principal.RowDefinitions.Add(rowGallery);

            RowDefinition rowBoxUpload = new RowDefinition();
            rowBoxUpload.Height = new GridLength(1, GridUnitType.Star);
            Principal.RowDefinitions.Add(rowBoxUpload);
        }

        private void initImageTest()
        {
            this._imageTest = new Image();
            this._imageTest.Source = new BitmapImage(new Uri(@"http://www.1001-votes.com/vote/1234fonds/pays-1376782006273-t.jpg"));
            this._imageTest.Visibility = Visibility.Visible;
            this._imageTest.Name = "image Test";
            this._imageTest.VerticalAlignment = VerticalAlignment.Center;
            this._imageTest.HorizontalAlignment = HorizontalAlignment.Center;
            if (this.Principal != null)
            {
                this.Principal.Children.Add(this._imageTest);
            }
        }
        
        private async void RecupererImage()
        {
            try
            {
                var filePicker = new FileOpenPicker();
                filePicker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
                filePicker.FileTypeFilter.Add(".jpeg");
                filePicker.FileTypeFilter.Add(".jpg");
                filePicker.FileTypeFilter.Add(".png");

                Windows.Storage.StorageFile file = await filePicker.PickSingleFileAsync();
                if (file != null)
                {
                    var bitmap = new BitmapImage();
                    var stream = await file.OpenReadAsync();
                    await bitmap.SetSourceAsync(stream);
                    this._imageTest.Source = bitmap;
                }
            }
            catch
            {

            }
        }
    }
}
