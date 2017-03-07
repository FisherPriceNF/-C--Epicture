using System;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.ApplicationModel.Activation;
using Windows.UI.Core;
using Windows.Graphics.Display;
using System.Threading.Tasks;
using Epicture.PageApp.HomePage;

// Pour plus d'informations sur le modèle d'élément Page vierge, voir la page http://go.microsoft.com/fwlink/?LinkId=234238

namespace Epicture.HomePage
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class ExtendedSplash : Page
    {
        internal Rect splashImageRect; // stocke les coordonnées où l'image de l' écran de démarrage du système affichée pour l'application.
        internal bool dismissed = false; // pistes variables ou non l'écran de démarrage qui est affiché par le système a été rejeté.
        internal Frame rootFrame;

        private SplashScreen splash; // Variable to hold the splash screen object.
        private double ScaleFactor; // Variabele to hold the device scale factor.

        public ExtendedSplash(SplashScreen splashscreen, bool loadState)
        {
            this.InitializeComponent();
            DismissExtendedSplash();
            Window.Current.SizeChanged += Current_SizeChanged;
            ScaleFactor = (double)DisplayInformation.GetForCurrentView().ResolutionScale / 100;
            splash = splashscreen;
            if (splash != null)
            {
                splash.Dismissed += Splash_Dismissed;
                splashImageRect = splash.ImageLocation;
                PositionImage();
            }
            RestoreStateAsync(loadState);
        }

        private async void RestoreStateAsync(bool loadState)
        {
            if (loadState)
                await SuspensionManager.RestoreAsync();

        }

        private void PositionImage()
        {
            extendedSplashImage.SetValue(Canvas.LeftProperty, splashImageRect.Left);
            extendedSplashImage.SetValue(Canvas.TopProperty, splashImageRect.Top);
            extendedSplashImage.Width = splashImageRect.Width / ScaleFactor;
            extendedSplashImage.Height = splashImageRect.Height / ScaleFactor;
        }

        private void Splash_Dismissed(SplashScreen sender, object args)
        {
            dismissed = true;
        }

        private void Current_SizeChanged(object sender, WindowSizeChangedEventArgs e)
        {
           if (splash != null)
            {
                splashImageRect = splash.ImageLocation;
                PositionImage();
            }
        }

        private async void DismissExtendedSplash()
        {
            await Task.Delay(TimeSpan.FromSeconds(5));  // Delay for 3 seconds
            rootFrame = new Frame();
            Connection page = new Connection();
            rootFrame.Content = page;
            Window.Current.Content = rootFrame;
            rootFrame.Navigate(typeof(Connection));
        }
    }
}
