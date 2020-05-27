using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Odbc;

namespace VetEddieAutomaticCopier
{
    class Copier
    {
        OdbcConnection d3fConnection = new OdbcConnection("DSN=d3fdb");

        OdbcConnection vetEddieConnection = new OdbcConnection("DSN=veteddie");

        public void CopyNewCases()
        {
            /*  const string driver = "DRIVER={MySQL ODBC 8.0 ANSI Driver};";
              const string server = "SERVER =188.121.44.186;";
              const string D3Fdatabase = "DATABASE=DebugFramework;";
              const string vetEddieDatabase = "DATABASE=veteddie_eddie;";
              const string user = "UID=gennaro2;";
              const string password = "PASSOWORD=sha9tTer;";
              const string option = "OPTION=3;";
              const string D3FconnectionString = driver + server + D3Fdatabase + user + password + option;
              const string vetEddieConnectionString = driver + server + vetEddieDatabase + user + password + option;*/

         
            try
            {

                Console.WriteLine("Attempting connection to D3F vet eddie");
                d3fConnection.Open();
                Console.WriteLine("Attempting connection to vet eddie");
                vetEddieConnection.Open();

                OdbcCommand d3fQuery = new OdbcCommand("SELECT COUNT(*) FROM cases", d3fConnection);
                OdbcCommand vetEddieQuery = new OdbcCommand("SELECT COUNT(*) FROM cases", vetEddieConnection);

                // int vetEddieCount = 
                Console.WriteLine("There are currently {0} cases in d3f vet eddie", d3fQuery.ExecuteScalar());
                Console.WriteLine("There are currently {0} cases in original vet eddie", vetEddieQuery.ExecuteScalar());

                OdbcCommand d3fTableCommand = new OdbcCommand("SHOW TABLES", d3fConnection);
                OdbcCommand vetEddieTableCommand = new OdbcCommand("SHOW TABLES", vetEddieConnection);

                // declare reader objects for tables and columns
                OdbcDataReader d3fTableReader;
                

                OdbcDataReader vetEddieTableReader;
               

                // queries that return result set are executed by ExecuteReader()
                // If you are to run queries like insert, update, delete then
                // you would invoke them by using ExecuteNonQuery() 
                d3fTableReader = d3fTableCommand.ExecuteReader();
                vetEddieTableReader = vetEddieTableCommand.ExecuteReader();


                List<String> d3fTableNames = new List<string>();
                List<String> vetEddieTableNames = new List<String>();

                int d3FTableCount = 0;
                int vetEddieTableCount = 0;

                Console.WriteLine("Reading d3f vet eddie table structure...");

                while (d3fTableReader.Read())
                {

                    Console.Write(d3fTableReader.GetValue(0)+" ");
                    d3fTableNames.Add(d3fTableReader.GetValue(0).ToString());
                }
                d3FTableCount = d3fTableNames.Count;

                Console.WriteLine("There are {0} tables in d3f version of vet eddie", d3FTableCount);

                Console.WriteLine("Reading vet eddie table structure...");

                while (vetEddieTableReader.Read())
                {
                    Console.Write(vetEddieTableReader.GetValue(0)+" ");
                    vetEddieTableNames.Add(vetEddieTableReader.GetValue(0).ToString());
                }
                vetEddieTableCount = vetEddieTableNames.Count;

                Console.WriteLine("There are {0} tables in original version of vet eddie", vetEddieTableCount);

                if(vetEddieTableCount == d3FTableCount)
                {
                    Console.WriteLine("Table count matches");
                    bool tableNamesMatch =vetEddieTableNames.SequenceEqual(d3fTableNames);

                    if(tableNamesMatch)
                    {
                        Console.WriteLine("The table names match");

                        Console.WriteLine("Starting to sync table data");
                        Console.WriteLine("Checking data integrity before attempting sync...");
                        bool integrityOK = true;

                        Dictionary<String, List<String>> columnLookup = new Dictionary<string, List<string>>();

                       

                        foreach (String tableName in vetEddieTableNames)
                        {
                           
                            Console.WriteLine("Checking integrity for table {0} ", tableName);

                            OdbcCommand vetEddieColumnCommand = new OdbcCommand("SHOW COLUMNS IN " + tableName, vetEddieConnection);
                            OdbcCommand d3fColumnCommand = new OdbcCommand("SHOW COLUMNS IN " + tableName, d3fConnection);

                            OdbcDataReader vetEddieColumnReader = vetEddieColumnCommand.ExecuteReader();
                            OdbcDataReader d3fColumnReader = d3fColumnCommand.ExecuteReader();

                            List<String> d3ftableColumns = new List<string>();
                            List<String> vetEddieTableColumns = new List<string>();

                            while (vetEddieColumnReader.Read())
                            {
                                vetEddieTableColumns.Add(vetEddieColumnReader.GetValue(0).ToString());
                            }

                            int vetEddieColumnCount = vetEddieTableColumns.Count;

                            while (d3fColumnReader.Read())
                            {
                                d3ftableColumns.Add(d3fColumnReader.GetValue(0).ToString());
                            }

                            int d3fColumnCount = d3ftableColumns.Count;

                            Console.WriteLine("Found {0} columns in d3f version of vet eddie for table {1}", d3fColumnCount,tableName);
                            Console.WriteLine("Found {0} columns in original vet eddie for table {1}", vetEddieColumnCount, tableName);

                            if(vetEddieColumnCount != d3fColumnCount)
                            {
                                Console.WriteLine("Disparity between column count found in table {0}", tableName);
                                if (d3fColumnCount > vetEddieColumnCount)
                                    Console.WriteLine("d3f version of vet eddie has {0} column(s) more than original vet eddie " + (d3fColumnCount - vetEddieColumnCount).ToString());
                                if(vetEddieColumnCount > d3fColumnCount)
                                    Console.WriteLine("original version of vet eddie has {0} column(s) more than d3f version of vet eddie " + (vetEddieColumnCount - d3fColumnCount).ToString());

                                integrityOK = false;
                                break;
                            }

                            columnLookup.Add(tableName, vetEddieTableColumns);
                            
                
                    
                            vetEddieColumnReader.Close();
                            d3fColumnReader.Close();



                        }

                        if(integrityOK)
                        {
                            Console.WriteLine("Integrity check successful");

                            //StringBuilder insertion = new StringBuilder();
                            int c = 0;

                            foreach (String tableName in vetEddieTableNames)
                            {
                                c++;
                                Console.WriteLine("Syncing table {0}/{1}", c,vetEddieTableCount);


                                String query = syncTables(tableName, columnLookup[tableName]);
                                if (query.Equals(""))
                                    continue;
                                OdbcCommand insertIntoD3F = new OdbcCommand(query, d3fConnection);
                                int rowsAffectedByInsertion = insertIntoD3F.ExecuteNonQuery();

                                Console.WriteLine("Sync complete for table {0} ", c);

                                //break; // just do for one for debug,.. 
                            }



                        }
                        else
                        {
                            Console.WriteLine("Integtrity check failed please check!");
                            return;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Some table names have been changed please check!");
                   
                        Console.WriteLine("Stopping exection");

                        return;
                    }
                }

                else
                {
                    Console.WriteLine("Table count does not match please check!");
                    Console.WriteLine("Stopping execution");
                }


                d3fTableReader.Close();
                vetEddieTableReader.Close();

                d3fConnection.Close();
                vetEddieConnection.Close();


            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                d3fConnection.Close();
                vetEddieConnection.Close();

            }

        }

       
        private string syncTables(string tableName, List<string> columnNames)
        {
            Console.WriteLine("Syncing {0}", tableName);
            OdbcCommand deletealld3f = new OdbcCommand("DELETE FROM " + tableName, d3fConnection); 
            OdbcCommand getAllRowsInVetEddieTable = new OdbcCommand("SELECT * FROM "+ tableName,vetEddieConnection);
            OdbcCommand getRowsCountInVetEddieTable = new OdbcCommand("SELECT COUNT(*) FROM " + tableName,vetEddieConnection);

            int totalRowsinVetEddie = Convert.ToInt32(getRowsCountInVetEddieTable.ExecuteScalar());
            if (totalRowsinVetEddie == 0)
                return "";
      

            int totalRowsDeleted = deletealld3f.ExecuteNonQuery();
            Console.WriteLine("Cleaned {0} - Total Rows Affected {1} ", tableName, totalRowsDeleted);
            



            // queries that return result set are executed by ExecuteReader()
            // If you are to run queries like insert, update, delete then
            // you would invoke them by using ExecuteNonQuery() 

            OdbcDataReader rowsInVetEddieTable = getAllRowsInVetEddieTable.ExecuteReader();
            StringBuilder insertionQuery = new StringBuilder();
            int progressCount = 0;

            string[] skip = { "AMH", "ORO", "SOM", "TIG" };

            StringBuilder insertIntoD3fStringBuilder = new StringBuilder("INSERT INTO " + tableName);
            insertIntoD3fStringBuilder.Append(" (");

            bool allSkipsHaveBeenRemoved = false;
            // ignore language columns
            foreach (string sk in skip)
               allSkipsHaveBeenRemoved= columnNames.Remove(sk);


            foreach (String name in columnNames)
            {
                insertIntoD3fStringBuilder.Append(name);
                if (columnNames.IndexOf(name) < columnNames.Count - 1)
                    insertIntoD3fStringBuilder.Append(",");

            }
            insertIntoD3fStringBuilder.Append(") ");
            insertIntoD3fStringBuilder.Append("VALUES");

           int rowIndex = 1;

            while (rowsInVetEddieTable.Read())
           {
                 
                insertIntoD3fStringBuilder.Append("(");
              


                int columnCount = rowsInVetEddieTable.FieldCount;
                if (allSkipsHaveBeenRemoved)
                    columnCount = columnCount - skip.Length;//adjust the length of columns if we have removed the language ones

                for(int i=0; i<columnCount; i++)
                {
                    if (rowsInVetEddieTable.GetName(i).Equals("AMH") || 
                        rowsInVetEddieTable.GetName(i).Equals("ORO") || 
                        rowsInVetEddieTable.GetName(i).Equals("SOM") || 
                        rowsInVetEddieTable.GetName(i).Equals("TIG"))
                        continue;

                   string val = rowsInVetEddieTable.GetValue(i).ToString();
                    String type = rowsInVetEddieTable.GetDataTypeName(i);
                   // Console.Write(" " + type + " ");

                    if(type.Equals("varchar") || type.Equals("date"))
                    {
                        insertIntoD3fStringBuilder.Append("'");

                        if (type.Equals("date")&& !val.Equals("0000-00-00"))
                        {
                            val = val.Split(' ')[0];
                            //  Console.WriteLine(val);
                            try
                            {
                                DateTime temp = DateTime.ParseExact(val, "dd/MM/yyyy", null);
                                val = temp.ToString("yyyy-MM-dd");
                            }
                            catch(Exception e)
                            {
                                Console.WriteLine("Problem with date {0} ", val);
                            }
                           
                           
                        }
                        insertIntoD3fStringBuilder.Append(val);
                        insertIntoD3fStringBuilder.Append("'");
                    }
                    else
                       insertIntoD3fStringBuilder.Append(val);
                    if (i < columnCount - 1)
                        insertIntoD3fStringBuilder.Append(",");
             
                }
                insertIntoD3fStringBuilder.Append(")");
                //Console.WriteLine(insertIntoD3fStringBuilder.ToString());
                if (rowIndex < totalRowsinVetEddie)
                    insertIntoD3fStringBuilder.Append(",");

               insertIntoD3fStringBuilder.AppendLine();
                rowIndex++;

              //  Console.WriteLine(rowIndex);
               
                progressCount++;
                double progress = ((float)progressCount / (float)totalRowsinVetEddie) * 100.0f;
                Console.WriteLine("Syncing Progress {0}% ", progress);
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                if(progress< 99)
                    ClearCurrentConsoleLine();

                // break;//DELETE LATER
                
            }
            
            rowsInVetEddieTable.Close();

            Console.WriteLine(insertIntoD3fStringBuilder.ToString());

            return insertIntoD3fStringBuilder.ToString();
        }

        private void ClearCurrentConsoleLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }


    }
}
