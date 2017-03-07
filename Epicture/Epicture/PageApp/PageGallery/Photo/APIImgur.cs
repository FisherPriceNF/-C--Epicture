using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Epicture.PageApp.PageGallery.GestionGallery;
using System.Net.Http;
using System.Diagnostics;
using System.Net;
using Windows.UI.Xaml.Shapes;
using System.IO;
using Windows.Storage;
using Newtonsoft.Json;

namespace Epicture.PageApp.PageGallery.Photo
{
    class APIImgur
    {    
        public APIImgur()
        {          
    
        }

        private async Task<bool> isFilePresent()
        {
            try
            {
                StorageFile file = await Windows.Storage.ApplicationData.Current.LocalFolder.GetFileAsync(@"\Assets\resources.txt");
                Debug.WriteLine("File is exits.");
                return true;
            }
            catch
            {
                Debug.WriteLine("File does not exits.");
                return false;
            }
        }

        private async Task<List<string>> remplirList(string url)
        {
            string jsonEnString;
            List<string> _list = new List<string>();

            // On download le fichier "Json" en string.
            Downloadjson(url);
            StorageFile file = await Windows.Storage.ApplicationData.Current.LocalFolder.GetFileAsync(@"\Assets\resources.txt");

            bool res = await isFilePresent();
            if (res == true)
            {
                jsonEnString = await Windows.Storage.FileIO.ReadTextAsync(file);
                if (!string.IsNullOrEmpty(jsonEnString))
                {
                    jsonEnString = jsonEnString.Substring(10);
                    _list = creationLines(jsonEnString);
                }
                return (_list);
            }
            else
            {
                Debug.WriteLine("Le fichier en local du json existe.");
                return (null);
            }
        }

        private List<string> creationLines(string json)
        {
            string tmp = "{";
            List<string> list = new List<string>();

            for (int i = 0; i < json.Length; i++)
                {
                    if (i < json.Length - 5)
                    {
                        if (json[i] == ','
                            && json[i + 1] == '{'
                            && json[i + 2] == '\"' 
                            && json[i + 3] == 'i' 
                            && json[i + 4] == 'd' 
                            && json[i + 5] == '\"')
                        {
                            if (!string.IsNullOrEmpty(tmp))
                            {
                                list.Add(tmp);
                            }
                            tmp = "";
                            
                        }
                        else
                            tmp += json[i];
                    }
                }
            return (list);
        }

        public async Task<Dictionary<string, string>> getImage(int nbfois, string url)
        {
            List<string> listjson = new List<string>();
            Dictionary<string, string> mapphoto = new Dictionary<string, string>();
            listjson = await remplirList(url);

            //Debug.WriteLine("Nombre de Image voulue : " + nbfois);
            if (nbfois > listjson.Count)
                nbfois = listjson.Count;
            for (int i = 0; i < nbfois; i++)
            {
                if (listjson != null && i < listjson.Count)
                {
                    string path = "http://i.imgur.com/";
                    string title = "";

                    // On recupere une ligne dans la list.
                    string linejson = listjson.ElementAt(i);

                    if (linejson != null)
                    {
                        JToken response = JObject.Parse(linejson);
                        try
                        {
                            path = path + (string)response["cover"].ToString() + ".jpg";
                            title = (string)response["title"].ToString();
                            mapphoto.Add(path, title);
                        }
                        catch (JsonReaderException jex)
                        {
                            Debug.WriteLine(jex.Message);
                            Debug.WriteLine(linejson);
                        }
                        catch (Exception e)
                        {
                            Debug.WriteLine(e.Message);
                        }
                    }
                    else
                    {
                        String[] imagerandom = testImage(i);
                        mapphoto.Add(imagerandom[0], imagerandom[1]);
                    }
                }
                else
                {
                    String[] imagerandom = testImage(i);
                    mapphoto.Add(imagerandom[0], imagerandom[1]);
                }
            }
            return (mapphoto);
        }

        private async void Downloadjson(string url)
        {
           string jsonString;

            HttpClient httpClient = new HttpClient();
            var stream = await httpClient.GetStreamAsync(url);
            StreamReader reader = new StreamReader(stream);
            jsonString = reader.ReadToEnd();
            StorageFile file = await Windows.Storage.ApplicationData.Current.LocalFolder.GetFileAsync(@"\Assets\resources.txt");
            if (file != null)
            {
                await Windows.Storage.FileIO.WriteTextAsync(file, jsonString);
            }
            else
            {
                Debug.WriteLine("Variable Classe \"_file\" est null");
            }
        }

        private string[] testImage(int position)
        {
            string _name = "Image ";
            string _lien = "";
            int nbr_choix = 8;
            int _nbr_random = 0;

           int graine = DateTime.Now.Millisecond;
           Random r = new Random(graine);
           _nbr_random = r.Next(nbr_choix);

           switch (_nbr_random)
           {
                    case 1:
                        {
                            _lien = @"http://www.1001-votes.com/vote/1234fonds/pays-1376782006273-t.jpg";
                            break;
                        }
                    case 2:
                        {
                            _lien = @"http://pandoon.info/wp-content/uploads/2012/07/fond-ecran-paysage.jpg";
                            break;
                        }
                    case 3:
                        {
                            _lien = @"https://img.hebus.com/hebus_2016/11/07/preview/1478550210_19703.jpg";
                            break;
                        }
                    case 4:
                        {
                            _lien = @"https://img.hebus.com/hebus_2016/11/25/preview/1480098988_18317.jpg";
                            break;
                        }
                    case 5:
                        {
                            _lien = @"https://img.hebus.com/hebus_2011/07/14/preview/110714221335_45.jpg";
                            break;
                        }
                    case 6:
                        {
                            _lien = @"http://www.fond-ecran-hd.net/pc-driver/1216.jpg";
                            break;
                        }
                   case 7:
                        {
                            _lien = @"https://fleurchercheunemploi.files.wordpress.com/2014/09/ne-marche-pas.png";
                            break;
                        }
                    default:
                        {
                            _lien = @"http://www.1001-votes.com/vote/1234fonds/pays-1376782006273-t.jpg";
                            break;
                        }
             }
            _name = "Image " + (position + 1).ToString();
            return (new string[] { _lien, _name });
        }
    }
}
