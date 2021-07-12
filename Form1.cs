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
            //getpokemon();
            Graphics g = Graphics.FromHwnd(this.Handle);



           this.imageList1.Images.Add(Image.FromFile(@"C:\Users\richa\Downloads\3.jpg"));

            this.imageList1.ImageSize = new Size(256, 256);


            //this.imageList1.Draw(g, new Point(40, 40),0);

            //this.pictureBox1.Image = this.imageList1.Images[0];

            //this.listBox1.Items.Add(this.imageList1.Images[0]);

            Bitmap bitmap = new Bitmap(@"C:\Users\richa\Downloads\3.jpg");

            this.listView1.LargeImageList.Images.Add(Image.FromFile(@"C:\Users\richa\Downloads\3.jpg"));

            this.pictureBox1.Image = bitmap;

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
                this.listBox1.Items.Add(personList[i].capital);
                //this.listBox1.Text = personList[0].name;
            }
            //this.listBox1.Items.Add(personList[0].name);
            //this.listBox1.Text = personList[0].name;
        }

        public async void getpokemon()
        {
            var client = new HttpClient { BaseAddress = new Uri("https://pokeapi.co/api/v2/") };

            // Assign default header (Json Serialization)
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Make an API call and receive HttpResponseMessage
            var responseMessage = await client.GetAsync("pokemon?offset=0&limit=100", HttpCompletionOption.ResponseContentRead);

            // Convert the HttpResponseMessage to string
            var resultArray = await responseMessage.Content.ReadAsStringAsync();



            // Deserialize the Json string into type using JsonConvert
            var pokemonList = JsonConvert.DeserializeObject<Pokedex>(resultArray);


            for (int i = 0; i < pokemonList.Results.Count; i++)

                //var pokename = pokemonList[0].Next
            {
                this.listBox1.Items.Add(pokemonList.Results[i].Name);
                //this.listBox1.Text = personList[0].name;
            }
            //this.listBox1.Items.Add(personList[0].name);
            //this.listBox1.Text = personList[0].name;
        }
    



        private void button1_Click(object sender, EventArgs e)
        {
            List<RestCountry> testcountry = new List<RestCountry>();


            //RestCountry restCountry = testcountry[0].Name as RestCountry;

            //this.textBox1.Text = per;
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
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
   


        public class Pokedex
        {
            
            public long Count { get; set; }

          
            public Uri Next { get; set; }

           
            public Uri Previous { get; set; }

            
            public List<Result> Results { get; set; }
        }

        public  class Result
        {
            
            public string Name { get; set; }

           
            public Uri Url { get; set; }
        }

        
    

}
