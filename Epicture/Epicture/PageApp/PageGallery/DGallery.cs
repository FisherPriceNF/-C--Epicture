using System;
using System.Diagnostics;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using System.Collections.ObjectModel;
using Windows.UI.Xaml;
using Epicture.PageApp.PageGallery.Photo;
using System.Net;
using Newtonsoft.Json.Linq;
using Epicture.PageApp.PageGallery.GestionGallery;
using Windows.UI.Xaml.Controls.Primitives;
using System.Collections.Generic;

namespace Epicture.PageApp.PageGallery
{
    class DGallery
    {
        private Grid _principal;
        private Grid _parent;
        private ScrollViewer _scrollbar;
        private ObservableCollection<Grid> _list;
        private GridView _gridView;
        private MainPage _page;
        private SolidColorBrush _color;
        private APIImgur _api;
        private int _position;
        private string nameGrid;
        private int positionJson;

        // --- Constructeur
        public DGallery(String name, ref Grid parent, int row, SolidColorBrush color, MainPage page)
        {

            this.nameGrid = name;
            this._parent = parent;
            this._position = row;
            this._color = color;
            this._page = page;
            this.positionJson = 0;
            this._api = new APIImgur();
            initGridPrincipal();
            _list = new ObservableCollection<Grid>();
            initGridView();
            changementImage(60, @"https://api.imgur.com/3/gallery/hot/viral/0.json");
        }

        // --- Changement des Images.
        public void addPhoto(String name, String path)
        {
            CPhoto newPhoto = new CPhoto(name, path, ref this._page);
            _list.Add(newPhoto.Principal);
        }

        public async void changementImage(int nombreImageCharger, string url)
        {
            if (url != null)
            {
                Dictionary<string, string> mapphoto = new Dictionary<string, string>();
                mapphoto = await _api.getImage(nombreImageCharger, url);

                foreach (var item in mapphoto)
                {
                    //Debug.WriteLine("Name: " + item.Value + " | Path: " + item.Key);
                    addPhoto(item.Value, item.Key);
                }
                this.positionJson += nombreImageCharger;
            }
        }


        // --- Fonction Initilisation
        private void initGridPrincipal()
        {
            this._principal = new Grid();
            this._principal.Name = this.nameGrid;
            _principal.Background = this._color;
            this._principal.Width = double.NaN;

            if (this._parent != null)
            {
                Grid.SetRow(this._principal, this._position);
                this._parent.Children.Add(this._principal);
            }
        }

        private void initGridView()
        {
            _gridView = new GridView();
            _gridView.Name = this.nameGrid;
            _gridView.Background = this._color;
            _gridView.ItemsSource = this._list;
            _gridView.VerticalAlignment = VerticalAlignment.Stretch;
            _gridView.HorizontalAlignment = HorizontalAlignment.Stretch;
            
      
            if (this._principal != null)
            {
                this._principal.Children.Add(_gridView);
            }
        }

        // --- Fonction Evenement.
        private void OnEndScrollbar(object sender, ScrollViewerViewChangedEventArgs e)
        {
            ScrollViewer sb = (sender as ScrollViewer); 
            Debug.WriteLine("Scroll");
        }
    }
}
