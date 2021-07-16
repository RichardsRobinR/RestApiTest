using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Urls;
using RestClient.Net;

using System.Net.Http.Headers;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using System.Net;
using System.IO;

namespace RestApiTest
{
    public partial class Form1 : Form
    {
        private const string V = "https://restcountries.eu/rest/v2";
       
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listView1.View = View.Details;
            ImageList imageListSmall = new ImageList();

            imageListSmall.ImageSize = new Size(50, 50);

            listView1.SmallImageList = imageListSmall;
            listView1.Columns.Add("ID");
            listView1.Columns.Add("Name");

            this.listView1.Visible = true;

            var t = Task.Run(() => getpokemon(imageListSmall));
            
            
            //getpokemon(imageListSmall);

           
        }


        public async void getdata()
        {
           // var response = await "https://restcountries.eu/rest/v2".ToAbsoluteUrl;


            var client = new HttpClient { BaseAddress = new Uri("https://restcountries.eu/") };

            // Assign default header (Json Serialization)
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Make an API call and receive HttpResponseMessage
            var responseMessage = await client.GetAsync("rest/v2", HttpCompletionOption.ResponseContentRead);

            // Convert the HttpResponseMessage to string
            var resultArray = await responseMessage.Content.ReadAsStringAsync();


           
            // Deserialize the Json string into type using JsonConvert
            var personList = JsonConvert.DeserializeObject<List<RestCountry>>(resultArray);


            for (int i = 0; i < personList.Count; i++)
            {
                //this.listBox1.Items.Add(personList[i].capital);
                //this.listBox1.Text = personList[0].name;
            }
            //this.listBox1.Items.Add(personList[0].name);
            //this.listBox1.Text = personList[0].name;
        }

        public async void getpokemon(dynamic imageListSmall)
        {


            List<string> imageurl = new List<string>();

            WebClient webClient = new WebClient();
            MemoryStream memoryStream;

            HttpClient httpclient = new HttpClient();

            // Make an API call and receive HttpResponseMessage
            var responseMessage = await httpclient.GetAsync("https://api.jsonbin.io/b/60772efa0ed6f819beac4c31");

            // Convert the HttpResponseMessage to string
            var resultArray = await responseMessage.Content.ReadAsStringAsync();


            // Deserialize the Json string into type using JsonConvert
            var pokemonList = JsonConvert.DeserializeObject<Pokedex>(resultArray);


           
            try
            {
                
                       

                for (int i = 0; i < 40; i++)
                {

                    /* WebRequest request = WebRequest.Create(pokemonList.Pokemon[i].Imageurl);
                     WebResponse resp = request.GetResponse();
                     respStream = resp.GetResponseStream();
                     bmp = new Bitmap(respStream);
 
                     respStream.Dispose();*/


                    // adding url to string list
                    imageurl.Add(pokemonList.Pokemon[i].Imageurl.ToString());

                    if (listView1.InvokeRequired)
                    {
                        listView1.Invoke((MethodInvoker)delegate ()
                        {

                            ListViewItem item1 = new ListViewItem(pokemonList.Pokemon[i].Id);
                            item1.SubItems.Add(pokemonList.Pokemon[i].Name);
                            listView1.Items.Add(item1);
                            item1.ImageIndex = i;

                        });
                    }
                    
                    byte[] imageByte = webClient.DownloadData(imageurl[i]);
                    memoryStream = new MemoryStream(imageByte);

                    Image image = Image.FromStream(memoryStream);
                    imageListSmall.Images.Add(image);

                  



                }
                    
            }
            catch (Exception e)
            {

                throw;
            }
            finally
            {
                webClient.Dispose();
                
               
            }
            
           

        }

        private void listview_colownWitdh(object sender, ColumnWidthChangingEventArgs e)
        {
            e.Cancel = true;
            e.NewWidth = 120;
        }

  

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }
    }

    public class RestCountry
    {
        public string name { get; set; }


        public List<string> topLevelDomain { get; set; }
        public string alpha2Code { get; set; }
        public string alpha3Code { get; set; }
        public List<string> callingCodes { get; set; }
        public string capital { get; set; }
        public List<object> altSpellings { get; set; }
        public string region { get; set; }
        public string subregion { get; set; }
        public int population { get; set; }
        public List<object> latlng { get; set; }
        public string demonym { get; set; }
        public double? area { get; set; }
        public double? gini { get; set; }
        public List<string> timezones { get; set; }
        public List<object> borders { get; set; }
        public string nativeName { get; set; }
        public string numericCode { get; set; }
       // public List<Currency> currencies { get; set; }
        //public List<Language> languages { get; set; }
       // public Translations translations { get; set; }
        public string flag { get; set; }
        public List<object> regionalBlocs { get; set; }
        public string cioc { get; set; }
    }




    //
    // To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
    //



    public partial class Pokedex
    {

        public Pokemon[] Pokemon { get; set; }
    }

    public partial class Pokemon
    {

        public string Name { get; set; }

        public string Id { get; set; }


        public Uri Imageurl { get; set; }

        public string Xdescription { get; set; }

        public string Ydescription { get; set; }


        public string Height { get; set; }


        public string Category { get; set; }

        public string Weight { get; set; }

    }


    }
