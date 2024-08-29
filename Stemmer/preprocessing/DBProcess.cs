using System.Data.SQLite;
using System.Reflection.Metadata;
using System.Text;

namespace Stemmer.preprocessing
{
    public class Document
    {
        // Constructor that initializes fields and concatenates stemmedTitle and stemmedBody into stems
        public Document(int primaryKey, string title, string stemmedTitle, string body, string stemmedBody)
        {
            this.primaryKey = primaryKey;
            this.title = title;
            this.stemmedTitle = stemmedTitle;
            this.body = body;
            this.stemmedBody = stemmedBody;
            this.stems = stemmedTitle + " " + stemmedBody;
        }

        public int primaryKey;
        public string title;
        public string stemmedTitle;
        public string body;
        public string stemmedBody;
        public string? stems;
    }
    public class DBProcess
    {
        string connectionString = @"Data Source= .\Database\SearchEngineDB.db;Version=3;";
        public List<Document> GetAllDocuments()
        {
            List<Document> documents = new List<Document>();
            string query = "SELECT ID, Title, StemmedTitle, Body, StemmedBody FROM IR_Datas";

            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    using (var command = new SQLiteCommand(query, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int primaryKey = reader.GetInt32(0); // Assuming ID is an integer
                                string title = reader.IsDBNull(1) ? null : reader.GetString(1);
                                string stemmedTitle = reader.IsDBNull(2) ? null : reader.GetString(2);
                                string body = reader.IsDBNull(3) ? null : reader.GetString(3);
                                string stemmedBody = reader.IsDBNull(4) ? null : reader.GetString(4);

                                Document document = new Document(primaryKey, title, stemmedTitle, body, stemmedBody);
                                documents.Add(document);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            return documents;
        }
        public String SearchResult()
        {
            string title = "ጄኔፈር ሎፔዝ በአራተኛ ፍቺዋ ከቤን አፍሌክ ጋር ልትለያይ ነው";
            byte[] utf8Bytes = Encoding.UTF8.GetBytes(title);
            string retrievedTitle = " ";
            string query = "SELECT BODY FROM IR_data WHERE TITLE = @title";

            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                using (var command = new SQLiteCommand(query, connection))
                {
                    // Add the parameter value to avoid SQL injection
                    command.Parameters.AddWithValue("@title", "%" + title + "%");
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            retrievedTitle = reader.GetString(1);
                            //  string body = reader.GetString(1);

                        }
                    }
                }
            }
            Console.WriteLine(retrievedTitle);
            if (retrievedTitle == " ") { retrievedTitle = "Did work"; };
            return retrievedTitle;

        }

        public bool AddIRdata(string title, string body, string stemmedtitle, string stemmedbody)
        {
            bool check = false;
            string query = "INSERT INTO IR_Datas (TITLE, BODY, STEMMEDTITLE, STEMMEDBODY) VALUES (@title, @body, @stemmedtitle, @stemmedbody)";

            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    using (var command = new SQLiteCommand(query, connection))
                    {
                        // Add the parameters to the command
                        command.Parameters.AddWithValue("@title", title);
                        command.Parameters.AddWithValue("@body", body);
                        command.Parameters.AddWithValue("@stemmedtitle", stemmedtitle);
                        command.Parameters.AddWithValue("@stemmedbody", stemmedbody);

                        // Execute the query and check the result
                        int result = command.ExecuteNonQuery();
                        if (result > 0)
                        {
                            check = true;
                        }
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                // Log or print the exception message for debugging
                Console.WriteLine("Error: " + ex.Message);
            }

            return check;
        }

    }
}
