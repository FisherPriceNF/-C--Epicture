using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI;

// Pour plus d'informations sur le modèle d'élément Page vierge, voir la page http://go.microsoft.com/fwlink/?LinkId=234238

namespace Epicture
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class SimplePhotoPage : Page
    {
        Grid _principal;
        DBoxImage _image;
        DTitleImage _title;
        DBoxFavori _favori;

        public SimplePhotoPage(string name, string path)
        {
            this.InitializeComponent();
            this.initGridPrincipal();

            SolidColorBrush colorBoxImage = new SolidColorBrush();
            colorBoxImage.Color = Color.FromArgb(255, 255, 0, 0);
            _image = new DBoxImage("BoxImage", _principal, 0, colorBoxImage);

            SolidColorBrush colorTitleImage = new SolidColorBrush();
            colorTitleImage.Color = Color.FromArgb(255, 0, 102, 204);
            _title = new DTitleImage("Title", _principal, 1, colorTitleImage);

            SolidColorBrush colorBoxFavori = new SolidColorBrush();
            colorBoxFavori.Color = Color.FromArgb(255, 204, 0, 204);
            _favori = new DBoxFavori("BoxFavori", _principal, 2, colorBoxFavori);
        }

        private void initGridPrincipal()
        {
            this._principal = new Grid();
            _principal.Name = "SimplePhoto";
            _principal.Background = new SolidColorBrush(Colors.Red);
            _principal.Width = double.NaN;
            _principal.Height = double.NaN;

            RowDefinition rowBoxSearch = new RowDefinition();
            rowBoxSearch.Height = new GridLength(0, GridUnitType.Star);
            _principal.RowDefinitions.Add(rowBoxSearch);

            RowDefinition rowGallery = new RowDefinition();
            rowGallery.Height = new GridLength(10, GridUnitType.Star);
            _principal.RowDefinitions.Add(rowGallery);

            RowDefinition rowBoxUpload = new RowDefinition();
            rowBoxUpload.Height = new GridLength(0, GridUnitType.Star);
            _principal.RowDefinitions.Add(rowBoxUpload);
            if (this.Parent != null)
            {

            }
        }

        public void Page_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
