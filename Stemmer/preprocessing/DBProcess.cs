using System.Data.SQLite;
using System.Text;

namespace Stemmer.preprocessing
{
    public class DBProcess
    {
        public String SearchResult( )
        {
            string title = "ጄኔፈር ሎፔዝ በአራተኛ ፍቺዋ ከቤን አፍሌክ ጋር ልትለያይ ነው";
            byte[] utf8Bytes = Encoding.UTF8.GetBytes(title);
            string retrievedTitle = " ";
            string connectionString = @"Data Source= ../Database/SearchEngineDB.db;Version=3;";
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
            if(retrievedTitle==" ") { retrievedTitle = "Did work"; };
            return retrievedTitle;

        }

        public bool AddIRdata(string title, string body, string stemmedtitle, string stemmedbody)
        {
            bool check = false;
            string connectionString = @"Data Source= ../Database/SearchEngineDB.db;Version=3;";
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
