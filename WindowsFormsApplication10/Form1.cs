﻿using System;
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
        static List<RepoDetails> repoList = new List<RepoDetails>();

        public Form1()
        {
            InitializeComponent();
        }

        private async Task DownloadRepoInfoAsync(string username)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://api.github.com/");
            client.DefaultRequestHeaders.Add("User-Agent", "Anything");
            var response = await client.GetAsync("users/" + username + "/repos");
            string responseBody = await response.Content.ReadAsStringAsync();
            var jsonResponse = JsonConvert.DeserializeObject(responseBody);
            dynamic responseDynamic = ((Newtonsoft.Json.Linq.JArray)jsonResponse);

            foreach (var item in responseDynamic)
            {
                RepoDetails repoObj = new RepoDetails();
                JObject responseObj = JObject.Parse(item.ToString());
                repoObj.Name = (string)responseObj.SelectToken("name");
                repoObj.Description = (string)responseObj.SelectToken("description");
                repoObj.CreateDate = (string)responseObj.SelectToken("created_at");
                repoList.Add(repoObj);
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            lstRepoResults.Items.Clear();
            await DownloadRepoInfoAsync(txtUserId.Text);

            if (repoList.Count == 0)
                lstRepoResults.Items.Add("No Repositories found for this user id!");
            else
            {
                foreach (var item in repoList)
                {
                    string outputInfo = string.Format("Repo Name: {0}, Description: {1}, Created Date: {2}", item.Name, item.Description, item.CreateDate);
                    lstRepoResults.Items.Add(outputInfo);
                }
            }
            repoList.Clear();
        }
    }
}