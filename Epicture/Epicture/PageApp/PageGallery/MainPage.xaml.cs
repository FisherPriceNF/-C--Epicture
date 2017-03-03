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
using Epicture.PageApp.PageGallery;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Epicture
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private DBoxSearch _boxSearch;
        private DGallery _gallery;
        private DBoxUpload _upload;
        //private FlipView _filpView;
      
        // --- Constructeur.  
        public MainPage()
        {
            this.InitializeComponent();

            this.setColumn();
            SolidColorBrush colorBoxSearch = new SolidColorBrush();
            colorBoxSearch.Color = Color.FromArgb(255, 0, 128, 0);
            _boxSearch = new DBoxSearch("BoxSearch", ref Principal, 0, colorBoxSearch);

            SolidColorBrush colorGallery = new SolidColorBrush();
            colorGallery.Color = Color.FromArgb(255, 48, 139, 87);
            _gallery = new DGallery("Gallery", ref Principal, 1, colorGallery, this);

            SolidColorBrush colorUpload = new SolidColorBrush();
            colorUpload.Color = Color.FromArgb(255, 0, 128, 0);
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
    }
}
