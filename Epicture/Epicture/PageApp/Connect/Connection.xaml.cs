using System;
using System.Diagnostics;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Text;
using Imgur.API.Authentication.Impl;
using Imgur.API.Authentication;
using Imgur.API.Endpoints.Impl;
using Imgur.API.Models;
using System.Threading.Tasks;
using Imgur.API.Models.Impl;
using Windows.UI.Xaml.Controls;
using Windows.System;
using System.Net.Http;
using System.Net.Http.Headers;
using Windows.UI.Popups;
using System.Collections.Generic;
using System.Net;

// Pour plus d'informations sur le modèle d'élément Page vierge, voir la page http://go.microsoft.com/fwlink/?LinkId=234238

namespace Epicture
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class Connection : Page
    {
        TextBlock _indication;
        TextBox _email;
        TextBox _password;
        Windows.UI.Xaml.Controls.Image _image;
        Button _connection;
        Grid _passemail;

        public Connection()
        {
            this.InitializeComponent();

            ApplicationView.PreferredLaunchViewSize = new Size(/*x*/600, /*y*/800);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;

            SolidColorBrush color = new SolidColorBrush();
            color.Color = Color.FromArgb(255, 0, 128, 0);
            Principal.Background = color;

            this.setColumn();
            initImage();
            initTextBoxIndication();
            //initPasswordEmail();
            initButtom();
            //this._email.Height = this._passemail.ActualHeight;
            //this._email.Width = Window.Current.Bounds.Width / 1.8;
            //this._password.Width = Window.Current.Bounds.Width / 1.8;
            //this._password.Height = this._passemail.ActualHeight  - this._email.ActualHeight + 2;
        }

        private void initImage()
        {
            this._image = new Windows.UI.Xaml.Controls.Image();
            this._image.Name = "image Imgur";
            this._image.Stretch = Stretch.Fill;
            this._image.Source = new BitmapImage(new Uri(@"http://img4.hostingpics.net/pics/699274unnamed.png"));
            this._image.Visibility = Visibility.Visible;
            this._image.Width = Window.Current.Bounds.Width / 1.8;
            this._image.Height = Window.Current.Bounds.Height / 1.8 - 90;
            this._image.VerticalAlignment = VerticalAlignment.Center;
            this._image.HorizontalAlignment = HorizontalAlignment.Center;

            if (Principal != null)
            {
                Grid.SetRow(this._image, 0);
                Principal.Children.Add(this._image);
            }
        }

        private void initPasswordEmail()
        {
            SolidColorBrush color = new SolidColorBrush();
            color.Color = Color.FromArgb(255, 0, 128, 0);
            this._passemail = new Grid();
            this._passemail.Background = color;
            this._email = new TextBox();
            this._email.Name = "email";
            this._email.TextWrapping = TextWrapping.NoWrap;
            this._email.AcceptsReturn = true;
            this._email.FontFamily = new FontFamily("Segeo UI");
            this._email.TextWrapping = TextWrapping.Wrap;
            this._email.PlaceholderText = "entrez votre email";
            SolidColorBrush coloremail = new SolidColorBrush();
            coloremail.Color = Color.FromArgb(255, 0, 0, 0);
            this._email.Foreground = coloremail;
            this._email.FontSize = 14;
            this._email.FontStyle = FontStyle.Normal;
            //this._email.FontWeight = FontWeights.SemiBold;
           // this._email.VerticalAlignment = VerticalAlignment.Top;
            this._email.HorizontalAlignment = HorizontalAlignment.Center;
            if (this._passemail != null)
            {
                this._passemail.Children.Add(this._email);
            }

            this._password = new TextBox();
            this._password.Name = "password";
            this._password.FontFamily = new FontFamily("Segeo UI");
            this._password.TextWrapping = TextWrapping.NoWrap;
            this._password.AcceptsReturn = true;
            this._password.MaxLength = 10;
            this._password.PlaceholderText = "entrez votre password";
            SolidColorBrush colorpass = new SolidColorBrush();
            colorpass.Color = Color.FromArgb(255, 0, 0, 0);
            this._password.Foreground = colorpass;
            this._password.FontSize = 14;
            this._password.FontStyle = FontStyle.Normal;
            //this._password.FontWeight = FontWeights.SemiBold;
            this._password.VerticalAlignment = VerticalAlignment.Bottom;
            this._password.HorizontalAlignment = HorizontalAlignment.Center;
            if (this._passemail != null)
            {
                this._passemail.Children.Add(this._password);
            }

            if (Principal != null)
            {
                Grid.SetRow(this._passemail, 1);
                Principal.Children.Add(this._passemail);
            }
        }

        private void initTextBoxPass()
        {   if (Principal != null)
            {
                Grid.SetRow(this._password, 1);
                Principal.Children.Add(this._password);
            }
        }

        private void initButtom()
        {
            this._connection = new Button();
            this._connection.Name = "btn_upload";
            Windows.UI.Xaml.Controls.Image image = new Windows.UI.Xaml.Controls.Image();
            image.Source = new BitmapImage(new Uri("https://cdn3.iconfinder.com/data/icons/gray-toolbar-2/512/login_user_profile_account-512.png"));
            this._connection.Content = image;
            this._connection.VerticalAlignment = VerticalAlignment.Top;
            this._connection.HorizontalAlignment = HorizontalAlignment.Center;

            this._connection.Click += _connection_Click;
            if (Principal != null)
            {
                Grid.SetRow(this._connection, 2);
                Principal.Children.Add(this._connection);
            }
        }

        private void initTextBoxIndication()
        {
            this._indication = new TextBlock();
            this._indication.Name = "indication";
            this._indication.FontFamily = new FontFamily("Segeo UI");
            this._indication.TextWrapping = TextWrapping.Wrap;
            this._indication.Text = "Connection à Imgur";
            SolidColorBrush color = new SolidColorBrush();
            color.Color = Color.FromArgb(255, 0, 0, 255);
            this._indication.Foreground = color;
            this._indication.FontSize = 30;
            this._indication.FontStyle = FontStyle.Normal;
            this._indication.FontWeight = FontWeights.SemiBold;
            this._indication.VerticalAlignment = VerticalAlignment.Top;
            this._indication.HorizontalAlignment = HorizontalAlignment.Center;
            if (this.Principal != null)
            {
                Grid.SetRow(this._indication, 1);
                Principal.Children.Add(this._indication);
            }
        }

        private void setColumn()
        {
            RowDefinition image = new RowDefinition();
            image.Height = new GridLength(5, GridUnitType.Star);
            Principal.RowDefinitions.Add(image);

            RowDefinition indication = new RowDefinition();
            indication.Height = new GridLength(2, GridUnitType.Star);
            Principal.RowDefinitions.Add(indication);

            RowDefinition button = new RowDefinition();
            button.Height = new GridLength(2, GridUnitType.Star);
            Principal.RowDefinitions.Add(button);
        }

        private void _connection_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("J'ai click sur le bouton connection ");
            connectionImgur();
        }

        public Task<IOAuth2Token> RefreshToken()
        {
            var client = new ImgurClient("e407172228739ac", "7273de19c0c0cd09154b2974596fbf4065547eed");
            var endpoint = new OAuth2Endpoint(client);
            var token = endpoint.GetTokenByRefreshTokenAsync("2419200");
            return (token);
        }

        public IOAuth2Token CreateToken()
        {
            var token = new OAuth2Token(/*TOKEN_ACCESS*/"", /*REFRESH_TOKEN*/ "2419200", /*TOKEN_TYPE*/"",/*ID_ACCOUNT*/ "e407172228739ac", /*EXPIRES_IN*/int.Parse("2419200"));
            return token;
        }

        private async  void connectionImgur()
        {
            // Recupération d'un client Imgur.
            string clientID = "fc4d408943ba656";
            string clientSecret = "e3186911f15a9d4c11a73f4193185a93c84ca760";
            var client = new ImgurClient(/*ClientID*/clientID, /*Client secret*/clientSecret);
            var endpoint = new OAuth2Endpoint(client);

            // On recupérer l'url d'autorisation. : https://api.imgur.com/oauth2/authorize/?client_id=CLIENT_ID&response_type=TOKEN_TYPE.
            string authorizationUrl = endpoint.GetAuthorizationUrl(OAuth2ResponseType.Token, "TOKEN_TYPE");

            // Création du serveur d'URL de redirection après l'autorisation d'imgur. | url de redirection : https://localhost:8081/ImgurResponse
            var httpClient = new HttpClient();
            var url = "https://localhost:8081/ImgurResponse";
            var parameters = new Dictionary<string, string> { { "code", "1" } };
            var encodedContent = new FormUrlEncodedContent(parameters);

            // On va aller sur une page web de l'url autorisation.
            var options = new LauncherOptions();
            options.TreatAsUntrusted = true; // On demande à l'utilisateur s'il veut aller sur la page web.
            // On va sur la page web.
            var succes = await Windows.System.Launcher.LaunchUriAsync(new Uri(authorizationUrl), options);

            // On recuperer une request POST.
            try
            {
                HttpResponseMessage response = await httpClient.PostAsync(url, encodedContent).ConfigureAwait(false);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    // Do something with response. Example get content:
                    string urlRedirection = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    Debug.WriteLine("UrlRedirection :" + urlRedirection);

                }

                // On regard l'état de la page web.
                if (succes == true)
                {
                    Debug.WriteLine("Succes pageWeb : " + succes);

                    // On recupére le code renvoyait par Imgur sur l'url : https://localhost:8081/ImgurResponse.
                    //string code = urlRedirection.Substring("https://localhost:8081/ImgurResponse?code=".Length);
                    //Debug.WriteLine("code: " + code);


                    //var token = await endpoint.GetTokenByCodeAsync(code);
                    // allerpage(clientID, clientSecret);
                }
                Debug.WriteLine("Succes pageWeb : " + succes);
            }
            catch (HttpRequestException requestException)
            {
                if (requestException.InnerException is WebException &&
                  ((WebException)requestException.InnerException).Status == WebExceptionStatus.NameResolutionFailure)
                {
                    Debug.Write("WebExceptionStatus.NameResolutionFailure = true" + requestException.ToString());
                }
                
                Debug.Write("WebExceptionStatus.NameResolutionFailure = false" + requestException.ToString());
            }
            allerpage(clientID, clientSecret);
        }

        private void allerpage(string clientID, string ClientSecret)
        {
            var Frame = Window.Current.Content as Frame;
            if (Frame != null)
            {
                MainPage page = new MainPage(clientID, ClientSecret);
                Frame.Content = page;
                Window.Current.Content = Frame;
                if (Frame.Content == null)
                {
                    Frame.Navigate(typeof(MainPage));
                }
            }
        }
    }
}
