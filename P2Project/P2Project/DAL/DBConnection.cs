using P2Project.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2Project.DAL
{
    static class DBConnection
    {
        static string connString = "Server=p2projectserver.database.windows.net; Database=P2Project; User Id=Azureadmin; Password=Azure123";

        public static void CreateUser(User user)
        {
            using(SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO [User] (username, usertype, textvisualpref, imagevisualpref, verbalpref, auditorypref, tactilepref, kinestheticpref) " +
                    "VALUES (@username, @usertype, @textvisualpref, @imagevisualpref, @verbalpref, @auditorypref, @tactilepref, @kinestheticpref)", conn); 
                cmd.Parameters.AddWithValue("@username", user.UserName);
                cmd.Parameters.AddWithValue("@usertype", (int)user.Type);
                cmd.Parameters.AddWithValue("@textvisualpref", user.Profile[0]);
                cmd.Parameters.AddWithValue("@imagevisualpref", user.Profile[1]);
                cmd.Parameters.AddWithValue("@verbalpref", user.Profile[2]);
                cmd.Parameters.AddWithValue("@auditorypref", user.Profile[3]);
                cmd.Parameters.AddWithValue("@tactilepref", user.Profile[4]);
                cmd.Parameters.AddWithValue("@kinestheticpref", user.Profile[5]);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static List<Exercise> GetAllExercises()
        {
            List<Exercise> exerciselist = new List<Exercise>();
            using(SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand("select * from Exercise", conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    List<double> profile = new List<double>();
                    for (int i = 4; i < 10; i++)
                        profile.Add(Convert.ToDouble(reader[i]));
                    ExerciseDescription desc = new ExerciseDescription(reader[10].ToString()) { VideoPath = reader[11].ToString(), AudioPath = reader[12].ToString(), SolutionPath = reader[13].ToString() };
                    Exercise exercise = new Exercise(reader[1].ToString(), desc, profile, reader[3].ToString(), Convert.ToDateTime(reader[2]), reader[14].ToString()) { ID = Convert.ToInt32(reader[0]) };
                    exerciselist.Add(exercise);
                }
                conn.Close();

                foreach (Exercise exercise in exerciselist)
                {
                    SqlCommand imagecmd = new SqlCommand("select * from ImagePath where exerciseid = @id", conn);
                    imagecmd.Parameters.AddWithValue("@id", exercise.ID);

                    conn.Open();
                    reader = imagecmd.ExecuteReader();
                    while (reader.Read())
                        exercise.Description.ImagePaths.Add(reader[1].ToString());
                    conn.Close();
                }
            }
            return exerciselist;
        }

        public static void InsertCompletedExercise(string userName, int id)
        {
            using(SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand("insert into UserExercise values(@username, @exerciseid)", conn);
                cmd.Parameters.AddWithValue("@username", userName);
                cmd.Parameters.AddWithValue("@exerciseid", id);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static User GetUserByUsername(string username)
        {
            User user = null;
            List<int> ids = new List<int>();
            using(SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand exercisecmd = new SqlCommand("select * from UserExercise where username = @username", conn);
                exercisecmd.Parameters.AddWithValue("@username", username);
                conn.Open();
                SqlDataReader exerciseReader = exercisecmd.ExecuteReader();
                while (exerciseReader.Read())
                    ids.Add(Convert.ToInt32(exerciseReader[1]));
                conn.Close();

                SqlCommand cmd = new SqlCommand("select * from [User] where username = @username", conn);
                cmd.Parameters.AddWithValue("@username", username);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    List<double> profile = new List<double>();
                    for (int i = 2; i < 8; i++)
                        profile.Add(Convert.ToDouble(reader[i]));
                    user = new User(reader[0].ToString(), profile, (UserType)reader[1], ids);
                }
            }
            return user;
        }

        public static bool UserExist(string username)
        {
            int userCount;
            using(SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand("select count(*) from [User] where username = @username", conn);
                cmd.Parameters.AddWithValue("@username", username);
                conn.Open();
                userCount = (int)cmd.ExecuteScalar();
            }
            return userCount != 0;
        }

        public static void CreateExercise(Exercise exercise)
        {
            int id = 0;
            using(SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO Exercise([name], creationdate, creator, textvisualpref, imagevisualpref, verbalpref, auditorypref, tactilepref, kinestheticpref, [description], videopath, audiopath, solutionpath, uri) " + 
                    "OUTPUT INSERTED.id VALUES(@name, @creationdate, @creator, @textvisual, @imagevisual, @verbal, @auditory, @tactile, @kinesthetic, @description, @videopath, @audiopath, @solutionpath, @uri)", conn);
                cmd.Parameters.AddWithValue("@name", exercise.Name);
                cmd.Parameters.AddWithValue("@creationdate", exercise.CreationDate);
                cmd.Parameters.AddWithValue("@creator", exercise.Creator);
                cmd.Parameters.AddWithValue("@textvisual", exercise.Profile[0]);
                cmd.Parameters.AddWithValue("@imagevisual", exercise.Profile[1]);
                cmd.Parameters.AddWithValue("@verbal", exercise.Profile[2]);
                cmd.Parameters.AddWithValue("@auditory", exercise.Profile[3]);
                cmd.Parameters.AddWithValue("@tactile", exercise.Profile[4]);
                cmd.Parameters.AddWithValue("@kinesthetic", exercise.Profile[5]);
                cmd.Parameters.AddWithValue("@description", exercise.Description.TextDescription);
                cmd.Parameters.AddWithValue("@videopath", (object)exercise.Description.VideoPath ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@audiopath", (object)exercise.Description.AudioPath ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@solutionpath", (object)exercise.Description.SolutionPath ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@uri", (object)exercise.URI ?? DBNull.Value);

                conn.Open();
                id = (int)cmd.ExecuteScalar();

                foreach (string path in exercise.Description.ImagePaths)
                {
                    SqlCommand imagecmd = new SqlCommand("INSERT INTO ImagePath VALUES(@id, @imagepath)", conn);
                    imagecmd.Parameters.AddWithValue("@id", id);
                    imagecmd.Parameters.AddWithValue("@imagepath", path);
                    imagecmd.ExecuteNonQuery();
                }
            }
        }

        public static void UpdateUserPrefs(User user)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand("update [User] set textvisualpref = @text, imagevisualpref = @image, verbalpref = @verbal, auditorypref = @auditory, tactilepref = @tactile, kinestheticpref = @kinesthetic", conn);
                cmd.Parameters.AddWithValue("@text", user.Profile[0]);
                cmd.Parameters.AddWithValue("@image", user.Profile[1]);
                cmd.Parameters.AddWithValue("@verbal", user.Profile[2]);
                cmd.Parameters.AddWithValue("@auditory", user.Profile[3]);
                cmd.Parameters.AddWithValue("@tactile", user.Profile[4]);
                cmd.Parameters.AddWithValue("@kinesthetic", user.Profile[5]);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
