using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace MLPOCUtility
{
    public partial class Default : System.Web.UI.Page
    {
        static ListBox LBox = null;
        static ListBox LBox1 = null;
        static Label Lbl = null;
        static DropDownList ddl4 = null;

        public class StringTable
        {
            public string[] ColumnNames { get; set; }
            public string[,] Values { get; set; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //listbox1.SelectedValueChanged += new EventHandler(Listbox1_SelectedValueChanged);
            //ListBox1.SelectedIndexChanged += new EventHandler(ListBox1_SelectedIndexChanged);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            LBox = ListBox1;
            Lbl = Label6;
            Dictionary<string, StringTable> Inputs = new Dictionary<string, StringTable>() { 
                { 
                    "input1", 
                    new StringTable() 
                    {
                        ColumnNames = new string[] {"JobID", "Condition", "Fluid Leaks", "Drivability", "IMPACT_1", "HEADER"},
                        Values = new string[,] {  { "1", DropDownList1.SelectedValue, DropDownList2.SelectedValue, DropDownList3.SelectedValue, DropDownList4.SelectedValue, "" } }
                    }
                },
            };     

            InvokeRequestResponseService(Inputs).Wait();
        }

        /// <summary>
        /// method to fetch data from the records
        /// </summary>
        /// <param name="Inputs"></param>
        /// <returns></returns>

        static async Task InvokeRequestResponseService(Dictionary<string, StringTable> Inputs)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var scoreRequest = new
                    {
                        Inputs,
                        GlobalParameters = new Dictionary<string, string>()
                        {
                        }
                    };
                    const string apiKey = "d8zRyoPPBbVCyaPFbb/FWLN3w0Yh/EzReuP5H1kcvXhvcILvVYkP1yb9YE6MJHK2OVRCqeWjVz4vPC3vj2Sbjw=="; // Replace this with the API key for the web service
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

                    client.BaseAddress = new Uri("https://ussouthcentral.services.azureml.net/workspaces/f096fd6c5a8744e6b2a1f9e75626547d/services/a6a93eb9becb4cd0983be6e0dd7901a0/execute?api-version=2.0&details=true");

                    HttpResponseMessage response = await client.PostAsJsonAsync("", scoreRequest).ConfigureAwait(false);
                    Hashtable data = new Hashtable();

                    if (response.IsSuccessStatusCode)
                    {
                        string result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        JObject obj = JObject.Parse(result);
                        JObject resultJsonObj = obj["Results"].Value<JObject>();
                        JObject outputJson = resultJsonObj["output1"].Value<JObject>();
                        JObject valueJson = outputJson["value"].Value<JObject>();
                        JArray columnNamesJson = valueJson["ColumnNames"].Value<JArray>();
                        JArray valuesJson = valueJson["Values"].Value<JArray>();
                        JArray valuesJson1 = valuesJson[0].Value<JArray>();

                        for (int i = 5; i < columnNamesJson.Count - 1; i++)
                        {
                            data.Add(Convert.ToDouble(valuesJson1[i].Value<string>()), columnNamesJson[i].Value<string>());
                        }
                        displayResult(data);
                      }
                    else
                    {
                        Console.WriteLine(string.Format("The request failed with status code: {0}", response.StatusCode));

                        // Print the headers - they include the requert ID and the timestamp, which are useful for debugging the failure
                        Console.WriteLine(response.Headers.ToString());

                        string responseContent = await response.Content.ReadAsStringAsync();
                        Console.WriteLine(responseContent);
                    }
                }
            }
            catch(Exception e)
            {
                Console.Write(e.StackTrace);
            }
        }
        /// <summary>
        /// Method to Display Data to UI
        /// </summary>
        /// <param name="data"></param>
        private static void displayResult(Hashtable data)
        {
            List<string> list = data.Cast<DictionaryEntry>().OrderByDescending(y=>y.Key).Select(x=>x.Value.ToString()).ToList<string>();
            for (int i = 0; i < list.Count;i++ )
            {
                list[i]=list[i].Replace("Scored Probabilities for Class",string.Empty).Replace("\"",string.Empty).Trim();
            }
            LBox.DataSource = list;
            LBox.DataBind();
            LBox.Visible = true;
            Lbl.Visible = true;
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            DropDownList1.SelectedIndex = 0;
            DropDownList2.SelectedIndex = 0;
            DropDownList3.SelectedIndex = 0;
            DropDownList4.SelectedIndex = 0;
            ListBox1.Visible = false;
            Label6.Visible = false;
            ListBox2.Visible = false;
        }

        protected void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            LBox = ListBox1;
            ddl4 = DropDownList4;
            LBox1 = ListBox2;
            InvokeRequestResponseService1().Wait();
        }


        static async Task InvokeRequestResponseService1(){
            try{
                using (var client = new HttpClient())
                {
                    var scoreRequest = new
                    {

                        Inputs = new Dictionary<string, StringTable>() { 
                        { 
                            "input1", 
                            new StringTable() 
                            {
                                ColumnNames = new string[] {"JobID", "Condition", "Fluid Leaks", "Drivability", "IMPACT_1", "HEADER", "LINE_DESC"},
                                Values = new string[,] {  { "0", "0", "value", "value", ddl4.SelectedValue,LBox.SelectedItem.Text, "value" }}
                            }
                        },
                    },
                        GlobalParameters = new Dictionary<string, string>()
                        {
                        }
                    };
                    const string apiKey = "ZWjK1BtRqAxS/dyk5lCT2OF7li2iOR7qCVfaZEcLe4W+q0lgf3r/QEkiopqOdf9krBxA7CBBoRJVKjKSnf/y4g=="; // Replace this with the API key for the web service
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

                    client.BaseAddress = new Uri("https://ussouthcentral.services.azureml.net/workspaces/2e82ac96ba9b4137bdeaf3a3d16c98d2/services/ef3d169457cf4946b3aa3fc977a54e5a/execute?api-version=2.0&details=true");

                    // WARNING: The 'await' statement below can result in a deadlock if you are calling this code from the UI thread of an ASP.Net application.
                    // One way to address this would be to call ConfigureAwait(false) so that the execution does not attempt to resume on the original context.
                    // For instance, replace code such as:
                    //      result = await DoSomeTask()
                    // with the following:
                    //      result = await DoSomeTask().ConfigureAwait(false)


                    HttpResponseMessage response = await client.PostAsJsonAsync("", scoreRequest).ConfigureAwait(false);

                   // Hashtable data1 = new Hashtable();
                    //Dictionary<double,string> data1 =new Dictionary<double,string>;
                    List<SubParts> subPartList = new List<SubParts> ();
                    if (response.IsSuccessStatusCode)
                    {
                        string result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        JObject ob = JObject.Parse(result);
                        JObject resultJsonObj = ob["Results"].Value<JObject>();
                        JObject outputJson = resultJsonObj["output1"].Value<JObject>();
                        JObject valueJson = outputJson["value"].Value<JObject>();
                        JArray columnNamesJson = valueJson["ColumnNames"].Value<JArray>();
                        JArray valuesJson = valueJson["Values"].Value<JArray>();
                        JArray valuesJson1 = valuesJson[0].Value<JArray>();

                        for (int i = 3; i < columnNamesJson.Count - 1; i++)
                        {
                            subPartList.Add(new SubParts {Probability=Convert.ToDouble(valuesJson1[i].Value<string>()), PartName=columnNamesJson[i].Value<string>() });
                            //data1.Add(Convert.ToDouble(valuesJson1[i].Value<string>()), columnNamesJson[i].Value<string>());
                        }
                        displaySubParts(subPartList);
                    }
                    else
                    {
                        Console.WriteLine(string.Format("The request failed with status code: {0}", response.StatusCode));

                        // Print the headers - they include the requert ID and the timestamp, which are useful for debugging the failure
                        Console.WriteLine(response.Headers.ToString());

                        string responseContent = await response.Content.ReadAsStringAsync();
                        Console.WriteLine(responseContent);
                    }
                }
            }
            catch (Exception e)
            {
                Console.Write(e.StackTrace);
            }
        }

        private static void displaySubParts(List<SubParts> subPartList)
        {
            List<string> list1 = subPartList.OrderByDescending(y => y.Probability).Select(x => x.PartName.ToString()).Take(4).ToList<string>();
        

            //List<string> list1 = data1.Cast<DictionaryEntry>().OrderByDescending(y => y.Key).Select(x => x.Value.ToString()).ToList<string>();
            for (int i = 0; i < list1.Count; i++)
            {
                list1[i] = list1[i].Replace("Scored Probabilities for Class", string.Empty).Replace("\"", string.Empty).Trim();
            }
            LBox1.DataSource = list1;
            LBox1.DataBind();
            LBox1.Visible = true;
            //Lbl.Visible = true;
        }
    }
}