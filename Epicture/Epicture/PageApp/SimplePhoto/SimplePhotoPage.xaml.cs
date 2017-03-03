using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI;
using Epicture.PageApp.SimplePhoto;

// Pour plus d'informations sur le modèle d'élément Page vierge, voir la page http://go.microsoft.com/fwlink/?LinkId=234238

namespace Epicture
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class SimplePhotoPage : Page
    {
        private DBoxImage _image;
        private DTitleImage _title;
        private DBoxFavori _favori;
        private MainPage _page; 

        public SimplePhotoPage(string name, string path, ref MainPage page)
        {
            this.InitializeComponent();

            this.setColum();
            SolidColorBrush colorBoxImage = new SolidColorBrush();
            colorBoxImage.Color = Color.FromArgb(255, 48, 139, 87);
            _image = new DBoxImage("BoxImage", ref Principal, 1, colorBoxImage);

            SolidColorBrush colorTitleImage = new SolidColorBrush();
            colorTitleImage.Color = Color.FromArgb(255, 0, 128, 0);
            _title = new DTitleImage("Title", ref Principal, 0, colorTitleImage);

            SolidColorBrush colorBoxFavori = new SolidColorBrush();
            colorBoxFavori.Color = Color.FromArgb(255, 0, 128, 0);
            _favori = new DBoxFavori("BoxFavori", ref Principal, 2, colorBoxFavori, ref page);

            _image.display(path);
            _title.setContent(name);
        }

        private void setColum()
        {
          
            RowDefinition rowTitle = new RowDefinition();
            rowTitle.Height = new GridLength(1, GridUnitType.Star);
            Principal.RowDefinitions.Add(rowTitle);

            RowDefinition rowImage = new RowDefinition();
            rowImage.Height = new GridLength(10, GridUnitType.Star);
            Principal.RowDefinitions.Add(rowImage);

            RowDefinition rowButton = new RowDefinition();
            rowButton.Height = new GridLength(1, GridUnitType.Star);
            Principal.RowDefinitions.Add(rowButton);
        }

        public void  navigationMainPage()
        {
            Frame.Navigate(typeof(MainPage));
        }
    }
}
