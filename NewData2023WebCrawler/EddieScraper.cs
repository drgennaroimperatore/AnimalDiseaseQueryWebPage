using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Text.Json;
using System.Text.Json.Nodes;
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
        private static List<List<KeyValuePair<String, String>>> camelCases= new List<List<KeyValuePair<String, String>>>();

        private static void SerialiseToJsonAndSave(List<List<KeyValuePair<String, String>>> cases, String filename)
        {
            string jsonString = JsonSerializer.Serialize(cases);

            string FilePath  = System.IO.Directory.GetCurrentDirectory()+"\\"+filename+".json";
            System.IO.File.WriteAllText(@FilePath, jsonString);
        }

        public static List<List<KeyValuePair<String, String>>> DeserialiseJsonFile(String filename)
        {
            return 
                JsonSerializer.Deserialize<List<List<KeyValuePair<String, String>>>>( System.IO.File.ReadAllText(System.IO.Directory.GetCurrentDirectory() + "\\" + filename + ".json"));
        }

        public static List<List<KeyValuePair<String, String>>> CleanJsonFile(string filename)
        {

            JsonNode root = JsonNode.Parse( System.IO.File.ReadAllText(System.IO.Directory.GetCurrentDirectory() + "\\" + filename + ".json"));
            
            JsonArray array = root.AsArray();
            
            foreach(var element in array)
            {
                JsonArray currCase = element.AsArray();


            }

            return null;
        }

        private static String parseMainCaseData(HtmlNode mainCase, String caseName, List<KeyValuePair<String, String>> tableData)
        {
            List<HtmlNode> fullReportRows = mainCase.Elements("tr").ToList();

            String species = "";

            tableData.Add(new KeyValuePair<String, String>("caseID", caseName));

            foreach (HtmlNode row in fullReportRows)
            {

                var children = row.ChildNodes;
                if (children.Count == 0)
                {
                    Console.WriteLine();
                }
                if (children[0].InnerText == "Species")
                    species = children[1].InnerText;
                tableData.Add(new KeyValuePair<String, String>(children[0].InnerText, children[1].InnerText));
            }
            return species;
        }

        private static void parseMainSymptoms(HtmlNode mainSymptoms, List<KeyValuePair<String, String>> tableData )
        {
            List<HtmlNode> symptomRows = mainSymptoms.Elements("tr").ToList();
            for (int i = 1; i < symptomRows.Count; i++)
            {
                var children = symptomRows[i].ChildNodes;
                tableData.Add(new KeyValuePair<String, String>(children[0].InnerText, children[1].InnerText));
            }

            
        }

        private static List<KeyValuePair<String, String>> parseAppRank(HtmlNode appRank)
        {
            return null;
        }

        private static void parseExpertChoice(HtmlNode expertChoice, List<KeyValuePair<String, String>> tableData)
        {
            List<HtmlNode> expertChoicesRows = expertChoice.Elements("tr").ToList();
            foreach (var row in expertChoicesRows)
            {
                if (row.ChildNodes.Count < 2)
                {
                    Console.WriteLine();
                    return;
                }
                var children = row.ChildNodes;
                tableData.Add(new KeyValuePair<String, String>(children[0].InnerText, children[1].InnerText));

            }

           
        }

        private static void parseOtherChoice(HtmlNode otherChoice, List<KeyValuePair<String, String>> tableData)
        {
            List<HtmlNode> otherChoiceRows = otherChoice.Elements("tr").ToList();

            foreach (var row in otherChoiceRows)
            {
                if (row.ChildNodes.Count < 2)
                    return; // if we get here there is a main treatment table

                var children = row.ChildNodes;
                tableData.Add(new KeyValuePair<String, String>(children[0].InnerText, children[1].InnerText));

            }
        }

        private static List<KeyValuePair<String, String>> parseTreatments(HtmlNode treatments)
        {
            return null;
        }


        private static HtmlNode getMainDiv(String htmlString)
        {
            HtmlDocument htmldocument = new HtmlDocument();
            htmldocument.LoadHtml(htmlString);
            HtmlNode root = htmldocument.DocumentNode;
            return root.ChildNodes[2].ChildNodes[3].ChildNodes[3]; // div class container

        }


        private static async Task GetCaseDetails(HttpClient client, string caseName, List<KeyValuePair<String, String>> tableData)
        {
            try
            {
                var content = new FormUrlEncodedContent(new[]
                {
                new KeyValuePair<string, string>(caseName, caseName)

                });
                var result = await client.PostAsync("/cases/case_viewN.php", content);
                string resultContent = await result.Content.ReadAsStringAsync();
                // Console.WriteLine(resultContent);
                

                List<HtmlNode> caseTable = getMainDiv(resultContent).Elements("table").ToList();
                if (caseTable.Count <=3)
                {
                    Console.WriteLine();
                    return; // skip dirty data
                }

                HtmlNode expertChoice = null;
                if (caseTable.Count==4)
                {

                    if (caseTable[1].InnerText.Contains("App Disease Rank"))
                        return; // skip dirty data
                    else
                    {
                        expertChoice = caseTable[3];
                        Console.WriteLine();
                    }
                }

                HtmlNode singleCaseFullReport = caseTable[0];

                String species = parseMainCaseData(singleCaseFullReport, caseName, tableData);

                HtmlNode mainSymptoms = caseTable[1];

                

               

                if (caseTable.Count==5)
                {
                    Console.WriteLine();
                    
                    if (caseTable[3].InnerText.Contains("Expert Choice"))
                        expertChoice = caseTable[3];
                    

                    if(caseTable[4].InnerText.Contains("Expert Choice"))
                    {
                        expertChoice = caseTable[3];
                          
                    }

                    if(caseTable[0].InnerText.Contains("Symptoms"))
                    {
                        Console.WriteLine();
                    }
                    
                }
                HtmlNode otherChoice = null;

                if (caseTable.Count == 6)
                {
                    mainSymptoms = caseTable[1];
                    expertChoice = caseTable[4];
                    otherChoice = caseTable[5];
                }

               
                if (caseTable.Count==7)
                {
                    mainSymptoms = caseTable[1];
                    expertChoice = caseTable[4];
                    otherChoice = caseTable[5];
                    
                }

                
                
                
                parseMainSymptoms(mainSymptoms, tableData);
                if(expertChoice!=null) parseExpertChoice(expertChoice, tableData);
                if (otherChoice != null) parseOtherChoice(otherChoice, tableData);

                Console.WriteLine("");

                bool hasOtherDisease = false;


                foreach (var k in tableData)
                {
                    if (k.Key.Contains( "Other Disease"))
                        hasOtherDisease = true;
                }

                if (!hasOtherDisease)
                    tableData.Add(new KeyValuePair<string, string>("Other Disease Selected:", "")); // padding for other disease

                Console.WriteLine();
                switch (species)
                {
                    case "Cattle":
                        cattleCases.Add(tableData);
                        break;
                    case "Sheep":
                        sheepCases.Add(tableData);
                        break;
                    case "Goats":
                        goatCases.Add(tableData);
                        break;
                    case "Camel":
                        camelCases.Add(tableData);
                        break;
                    case "Horse/Mule":
                        horsemuleCases.Add(tableData);
                        break;
                    case "Donkey":
                        donkeyCases.Add(tableData);
                        break;

                }

                Console.Clear();
                Console.WriteLine("Cattle Cases: " + cattleCases.Count);
                Console.WriteLine("Sheep Cases: " + sheepCases.Count);
                Console.WriteLine("Goat Cases: " + goatCases.Count);
                Console.WriteLine("Camel Cases: " + camelCases.Count);
                Console.WriteLine("Horse and Mule Cases: " + horsemuleCases.Count);
                Console.WriteLine("Donkey Cases: " + donkeyCases.Count);
            }
            catch (AggregateException ae)
            {
                foreach (var e in ae.Flatten().InnerExceptions)

                {
                    var stack = ae.StackTrace;

                    Console.WriteLine(e.Message);
                }
            }
            catch (Exception e)
            {
                var stack = e.StackTrace;
                Console.WriteLine(e.Message);
            }

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
                            
                                GetCaseDetails(client, name, td).Wait();
                           
                                
                            
                            string caseID = name.Remove(0, 4); //remove the word case
                            currentCase.Add(caseID);
                            
                            continue;
                        }
                       
                        currentCase.Add(caseInnerText);
                    }
                }

                SerialiseToJsonAndSave(sheepCases, "sheep");

                Console.WriteLine();


            };
        }
    }
       
}
