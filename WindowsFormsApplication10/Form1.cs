using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using System.Threading;

namespace WindowsFormsApplication10
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async Task<string> RequestRepoInfoAsync(string username)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://api.github.com/");
            client.DefaultRequestHeaders.Add("User-Agent", "Anything");
            var response = await client.GetAsync("users/" + username + "/repos");
            string responseBody = await response.Content.ReadAsStringAsync();
            return responseBody;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            lstRepoResults.Items.Clear();
            string responseBody = await RequestRepoInfoAsync(txtUserId.Text);
            var jsonResponse = JsonConvert.DeserializeObject(responseBody);
            dynamic responseDynamic = ((Newtonsoft.Json.Linq.JArray)jsonResponse);

            foreach (var item in responseDynamic)
            {
                JObject responseObj = JObject.Parse(item.ToString());
                string outputInfo = string.Format("Repo Name: {0}, Description: {1}, Created Date: {2}",
                    (string)responseObj.SelectToken("name"), (string)responseObj.SelectToken("description"),
                    (string)responseObj.SelectToken("created_at"));
                lstRepoResults.Items.Add(outputInfo);
            }
        }
    }
}