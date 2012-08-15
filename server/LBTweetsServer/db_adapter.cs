using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Globalization;

namespace LBTweetsServer
{
    public class db_adapter
    {
        // Provide the following information
        private static string           m_UserName;
        private static string           m_Password;
        private static string           m_DataSource;
        private static string           m_Database;
        private static int              m_cutoff = 10000;
        private static SqlConnection    m_conn = null;

        db_adapter()
        {
        }

        ~db_adapter()
        {
            if (m_conn != null)
                ((IDisposable)m_conn).Dispose();
        }

        public static void db_init(string username, string password, string datasource, string database)
        {
            m_UserName = username;
            m_Password = password;
            m_DataSource = datasource;
            m_Database = database;

            SqlConnectionStringBuilder connStringBuilder;
            connStringBuilder = new SqlConnectionStringBuilder();
            connStringBuilder.DataSource = m_DataSource;
            connStringBuilder.InitialCatalog = m_Database;
            connStringBuilder.Encrypt = true;
            connStringBuilder.TrustServerCertificate = false;
            connStringBuilder.UserID = m_UserName;
            connStringBuilder.Password = m_Password;

            // Connect to the master database and create the sample database
            m_conn = new SqlConnection(connStringBuilder.ToString());
            m_conn.Open();
        }

        public static void db_deinit()
        {
            m_conn.Close();
        }

        public static string db_register_user(long phone_number,
                                            string password,
                                            string interests)
        {
            using (SqlCommand command = m_conn.CreateCommand())
            {
                try
                {
                    command.CommandText = "INSERT INTO user_profile values (" + phone_number + ", '" + password + "', '" + interests + "')";
                    command.ExecuteNonQuery();
                    return "User registration successful.";
                }
                catch (SqlException e)
                {
                    return e.Message;
                }
            }
        }

        public static string db_authenticate_user(  long phone_number,
                                                    string password )
        {
            using (SqlCommand command = m_conn.CreateCommand())
            {
                try
                {
                    command.CommandText = "SELECT passwrd from user_profile WHERE phone_number=" + phone_number;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Loop over the results
                        if (reader.Read())
                        {
                            string  strPass = reader["passwrd"].ToString().Trim();
                            if (strPass.CompareTo(password) == 0)
                            {
                                return "User authentication successful.";
                            }
                        }
                    }
                    return "User authentication failed.";
                }
                catch (SqlException e)
                {
                    return e.Message;
                }
            }
        }

        public static string db_subscribe(long phone_number,
                                          string tags)
        {
            using (SqlCommand command = m_conn.CreateCommand())
            {
                try
                {
                    command.CommandText = "UPDATE user_profile SET interests= '" + tags + "' WHERE phone_number=" + phone_number;
                    command.ExecuteNonQuery();
                    return "User subscription update successful.";
                }
                catch (SqlException e)
                {
                    return e.Message;
                }
            }
        }

        public static void db_tweet(string tweet_value,
                                    double latitude,
                                    double longitude,
                                    string tags)
        {
            int ttl = 0;
            int i = 0;
            using (SqlCommand command = m_conn.CreateCommand())
            {
                /* tokenize tags */
                string[] tokens_tags = tags.Split(',');
            
                // calculate ttl as max of applcable tags
                for (i = 0; i < tokens_tags.Length; i++)
                {
                    if (tokens_tags[i].CompareTo("") != 0)
                    {
                        command.CommandText = "SELECT ttl FROM tag_to_ttl WHERE tag='" + tokens_tags[i] + "'";
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // Loop over the results
                            while (reader.Read())
                            {
                                if (ttl < System.Convert.ToInt32(reader["ttl"].ToString().Trim()))
                                    ttl = System.Convert.ToInt32(reader["ttl"].ToString().Trim());
                            }
                        }
                    }
                } // end of for loop
                // calculate current_time
                command.CommandText = "INSERT INTO tweets_tags values ('" 
                                        + tweet_value + "', '" 
                                        + tags + "', "
                                        + ttl + ", dbo.get_current_time(), " 
                                        + latitude + ", " 
                                        + longitude + ")";
                command.ExecuteNonQuery();
            } // end of (SqlCommand command = m_conn.CreateCommand())
        } // end of function

        public static tweets[] db_get_tweet(long phone_number,
                                            double latitude,
                                            double longitude,
                                            int radius)
        {
            int i = 0;
            string interests = null;
            List<tweets> results = new List<tweets>();
            if (radius == 0)
                radius = m_cutoff;

            using (SqlCommand command = m_conn.CreateCommand())
            {
                /* 
                 * get tweet_values from tweet_tags: filtered by tags and time
                 */

                /* get interests form user_profile */
                command.CommandText = "SELECT interests FROM user_profile WHERE phone_number=" + phone_number;
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    // Loop over the results
                    while (reader.Read())
                    {
                        interests = reader["interests"].ToString().Trim();
                    }
                }
                if (interests == null)
                    return null;

                bool flag = false;

                string[] tokens_tags = interests.Split(',');
                command.CommandText = "SELECT * FROM tweets_tags WHERE (";
                for (i = 0; i < tokens_tags.Length - 1; i++)
                {
                    if (tokens_tags[i].CompareTo("") != 0)
                    {
                        command.CommandText = command.CommandText + "tags LIKE '%" + tokens_tags[i] + "%' OR ";
                        flag = true;
                    }
                }
                // calculate current_time
                if (tokens_tags[i].CompareTo("") != 0)
                {
                    command.CommandText = command.CommandText + "tags LIKE '%" + tokens_tags[i] + "%' OR ";
                    flag = true;
                }

                if (flag == true)
                {
                    command.CommandText = command.CommandText.Remove(command.CommandText.Length - 3);
                    command.CommandText = command.CommandText + ") AND ";
                }
                else
                {
                    return null;
                    //command.CommandText = command.CommandText.Remove(command.CommandText.Length - 1);
                }
                command.CommandText = command.CommandText + "ttl+time_stamp > dbo.get_current_time()";
                command.CommandText = command.CommandText + " AND dbo.distance(latitude, longitude, "
                    + latitude + ", " + longitude + ") < " + radius;
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tweets t = new tweets();
                        t.m_tags = reader["tags"].ToString().Trim();
                        t.m_tweet_value = reader["tweet_value"].ToString().Trim();
                        results.Add(t);
                    }// while
                }
            }
            return results.ToArray();
        }// db_get_tweet

        public static tweets[] db_get_tweet_by_tag( double latitude,
                                                    double longitude,
                                                    int radius,
                                                    string tags)
        {
            int i = 0;
            List<tweets> results = new List<tweets>();
            if (radius == 0)
                radius = m_cutoff;

            using (SqlCommand command = m_conn.CreateCommand())
            {
                /* 
                 * get tweet_values from tweet_tags: filtered by tags and time
                 */

                string[] tokens_tags = tags.Split(',');
                command.CommandText = "SELECT * FROM tweets_tags WHERE (";
                for (i = 0; i < tokens_tags.Length - 1; i++)
                {
                    if (tokens_tags[i].CompareTo("") != 0)
                        command.CommandText = command.CommandText + "tags LIKE '%" + tokens_tags[i] + "%' OR ";
                }
                // calculate current_time
                if (tokens_tags[i].CompareTo("") != 0)
                    command.CommandText = command.CommandText + "tags LIKE '%" + tokens_tags[i] + "%' ";
                else
                {
                    command.CommandText = command.CommandText.Remove(command.CommandText.Length - 3);
                }
                command.CommandText = command.CommandText + ") AND ttl+time_stamp > dbo.get_current_time()";
                command.CommandText = command.CommandText + " AND dbo.distance(latitude, longitude, "
                    + latitude + ", " + longitude + ") < " + radius;
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tweets t = new tweets();
                        t.m_tags = reader["tags"].ToString().Trim();
                        t.m_tweet_value = reader["tweet_value"].ToString().Trim();
                        results.Add(t);
                    }// while
                }
            }
            return results.ToArray();
        }// db_get_tweet_by_tag
    }
}