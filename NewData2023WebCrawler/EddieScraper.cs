using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;
namespace NewData2023WebCrawler
{
    class EddieScraper
    {
        
        private static string loginURL = "https://vet-eddie.com";
        private static List<List<KeyValuePair<String, String>>> cattleCases = new List<List<KeyValuePair<String, String>>>();
        private static List<List<KeyValuePair<String, String>>> sheepCases = new List<List<KeyValuePair<String, String>>>();
        private static List<List<KeyValuePair<String, String>>> goatCases = new List<List<KeyValuePair<String, String>>>();
        private static List<List<KeyValuePair<String, String>>> horsemuleCases = new List<List<KeyValuePair<String, String>>>();
        private static List<List<KeyValuePair<String, String>>> donkeyCases = new List<List<KeyValuePair<String, String>>>();
        private static List<List<KeyValuePair<String, String>>> = new List<List<KeyValuePair<String, String>>>();
            private static async Task GetCaseDetails(HttpClient client, string caseName, List<KeyValuePair<String, String>> tableData)
        {


            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>(caseName, caseName)

                });
            var result = await client.PostAsync("/cases/case_viewN.php", content);
            string resultContent = await result.Content.ReadAsStringAsync();
            Console.WriteLine(resultContent);

            HtmlDocument htmldocument = new HtmlDocument();
            htmldocument.LoadHtml(resultContent);
            HtmlNode root = htmldocument.DocumentNode;
            HtmlNode div = root.ChildNodes[2].ChildNodes[3].ChildNodes[3]; // div class container
            
            List<HtmlNode> caseTable= div.Elements("table").ToList();
            if (caseTable.Count <4)
            {
                Console.WriteLine();
                return; // skip dirty data
            }
            HtmlNode singleCaseFullReport = caseTable[0];
            List<HtmlNode> fullReportRows = singleCaseFullReport.Elements("tr").ToList();
            
            
            tableData.Add(new KeyValuePair<String, String>("caseID", caseName));

            foreach (HtmlNode row in fullReportRows)
            {
              var children = row.ChildNodes;
            tableData.Add(new KeyValuePair<String,String>(children[0].InnerText, children[1].InnerText));
            }
            HtmlNode mainSymptoms = caseTable[1];

            List<HtmlNode> symptomRows = mainSymptoms.Elements("tr").ToList();
            for (int i = 1; i < symptomRows.Count; i++)
            {
                var children = symptomRows[i].ChildNodes;
                tableData.Add(new KeyValuePair<String, String>(children[0].InnerText, children[1].InnerText));
            }

            HtmlNode expertChoice = caseTable[4];
            List<HtmlNode> expertChoicesRows = expertChoice.Elements("tr").ToList();
            foreach (var row in expertChoicesRows)
            {
                var children = row.ChildNodes;
                tableData.Add(new KeyValuePair<String, String>(children[0].InnerText, children[1].InnerText));

            }
            if(caseTable.Count>5) // we get here if zz_other
            {
                HtmlNode otherChoice= caseTable[5];
                List<HtmlNode> otherChoiceRows = otherChoice.Elements("tr").ToList();

                foreach (var row in otherChoiceRows)
                {
                    var children = row.ChildNodes;
                    tableData.Add(new KeyValuePair<String, String>(children[0].InnerText, children[1].InnerText));

                }
            }

            Console.WriteLine();

        }

        public static async Task PostAsync()
        {


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(loginURL);
                var content = new FormUrlEncodedContent(new[]
                {
                new KeyValuePair<string, string>("username", "crawford"),
                new KeyValuePair<string, string>("password","Btoy_2105")
                });
                var result = await client.PostAsync("/index.php", content);
                string resultContent = await result.Content.ReadAsStringAsync();
                //Console.WriteLine(resultContent);

                HtmlDocument htmldocument = new HtmlDocument();
                htmldocument.LoadHtml(resultContent);
                HtmlNode casesTable = htmldocument.GetElementbyId("dtBasicExample");
                List<HtmlNode> tableHeaders = casesTable.Elements("thead").ToList() ;
                HtmlNode tableBody = casesTable.Element("tbody");
                List<HtmlNode> tableData= tableBody.Elements("tr").ToList();
                LinkedList<String> tableHeadersNames = new LinkedList<string>();
                foreach (HtmlNode item in tableHeaders)
                {
                    var headerName = item.InnerText;
                    if (headerName == "" || headerName=="Export") // skip the headers we don't need
                        continue;
                    tableHeadersNames.AddLast(headerName);
                    //Console.WriteLine(headerName);

                }
                tableHeadersNames.AddLast("caseID");
                List<List<string>> caseData = new List<List<string>>();
                foreach (HtmlNode row in tableData)
                {
                    List<String> currentCase = new List<string>();
                    caseData.Add(currentCase);
                    foreach(HtmlNode columns in row.Elements("td"))
                    {
                        var caseInnerText = columns.InnerText;
                        if (caseInnerText == "")
                            continue;
                        if (caseInnerText == "delete")
                            continue;
                        if(caseInnerText=="detail")
                        {
                           HtmlAttributeCollection attribute = columns.FirstChild.Attributes;
                            var name = attribute[2].Value;
                            List<KeyValuePair<String, String>> td = new List<KeyValuePair<String, String>>();
                            try
                            {
                                GetCaseDetails(client, name, td).Wait();
                            } catch(Exception e)
                            {
                                Console.WriteLine(e.Message);
                                continue;
                            }
                            string caseID = name.Remove(0, 4); //remove the word case
                            currentCase.Add(caseID);
                            
                            continue;
                        }
                       
                        currentCase.Add(caseInnerText);
                    }
                }

                Console.WriteLine();


            };
        }
    }
       
}
