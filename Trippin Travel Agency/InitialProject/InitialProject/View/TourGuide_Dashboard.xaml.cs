using System.Configuration;
using System.Data.SQLite;
using System.Windows.Controls;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for TourGuide_Dashboard.xaml
    /// </summary>
    public partial class TourGuide_Dashboard : UserControl
    {
        public TourGuide_Dashboard()
        {
            InitializeComponent();
            //username = GetUsername(loggedInUserId);
            //DataContext = this;
        }
        /*private string GetUsername(int userId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand("SELECT username FROM Users WHERE id = @userId", connection))
                {
                    command.Parameters.AddWithValue("@userId", userId);

                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return reader["username"].ToString();
                        }
                    }
                }
            }
            return string.Empty;
        }*/
    }
}
