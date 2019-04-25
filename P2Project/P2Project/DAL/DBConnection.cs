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

        static string connString = "Server=p2project-server.database.windows.net; Database=P2ProjectDB; User Id=Slorup; Password=Password123";

        public static void CreateUser(User user)
        {
            using(SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO [User] (username, usertype, visualpref, auditorypref, kinestheticpref, verbalpref, uncertaintycoef) " +
                    "VALUES (@username, @usertype, @visualpref, @auditorypref, @kinestheticpref, @verbalpref, @uncertaintycoef)", conn); 
                cmd.Parameters.AddWithValue("@username", user.UserName);
                cmd.Parameters.AddWithValue("@usertype", (int)user.Type);
                cmd.Parameters.AddWithValue("@visualpref", user.Profile.Visual);
                cmd.Parameters.AddWithValue("@auditorypref", user.Profile.Auditory);
                cmd.Parameters.AddWithValue("@kinestheticpref", user.Profile.Kinesthetic);
                cmd.Parameters.AddWithValue("@verbalpref", user.Profile.Verbal);
                cmd.Parameters.AddWithValue("uncertaintycoef", 0);
                //TRY CATCH
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
                //TRYCATCH
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    ExerciseDescription desc = new ExerciseDescription(reader[6].ToString());
                    LearningProfile profile = new LearningProfile(Convert.ToDouble(reader[2]), Convert.ToDouble(reader[3]), Convert.ToDouble(reader[4]), Convert.ToDouble(reader[5]));
                    Exercise exercise = new Exercise(reader[1].ToString(), desc, profile) { ID = Convert.ToInt32(reader[0])};
                    exerciselist.Add(exercise);
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
                //trycatch
                cmd.ExecuteNonQuery();
            }
        }

        public static User GetUserByUsername(string username)
        {
            User user = null;
            using(SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand("select * from [User] where username = @username", conn);
                cmd.Parameters.AddWithValue("@username", username);
                //TRYCATCH
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    LearningProfile profile = new LearningProfile(Convert.ToDouble(reader[2]), Convert.ToDouble(reader[3]), Convert.ToDouble(reader[4]), Convert.ToDouble(reader[5]));
                    user = new User(reader[0].ToString(), profile, (UserType)reader[1], new List<int>());
                }
                conn.Close();
                //SET COMPLETED EXERCISE IDS

                if(user != null)
                {
                    SqlCommand exercisecmd = new SqlCommand("select * from UserExercise where username = @username", conn);
                    exercisecmd.Parameters.AddWithValue("@username", username);
                    conn.Open();
                    SqlDataReader exerciseReader = exercisecmd.ExecuteReader();
                    while (exerciseReader.Read())
                    {
                        user.CompletedExercisesID.Add(Convert.ToInt32(exerciseReader[1]));
                    }
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
                //TRY CATCH
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
                SqlCommand cmd = new SqlCommand("INSERT INTO Exercise([name], visualpref, auditorypref, kinestheticpref, verbalpref, description, videopath, audiopath) OUTPUT INSERTED.id VALUES(@name, @visual, @auditory, @kinesthetic, @verbal, @description, @videopath, @audiopath)", conn); //IMAGEPATHS
                cmd.Parameters.AddWithValue("@name", exercise.Name);
                cmd.Parameters.AddWithValue("@visual", exercise.Profile.Visual);
                cmd.Parameters.AddWithValue("@auditory", exercise.Profile.Auditory);
                cmd.Parameters.AddWithValue("@kinesthetic", exercise.Profile.Kinesthetic);
                cmd.Parameters.AddWithValue("@verbal", exercise.Profile.Verbal);
                cmd.Parameters.AddWithValue("@description", exercise.Description.TextDescription);
                cmd.Parameters.AddWithValue("@videopath", (object)exercise.Description.VideoPath ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@audiopath", (object)exercise.Description.AudioPath ?? DBNull.Value);
                //TRYCATCH
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

        public static Exercise GetExerciseByID(int id)
        {
            Exercise exercise = null;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand("select * from Exercise where id = @id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                //TRYCATCH
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    LearningProfile profile = new LearningProfile(Convert.ToDouble(reader[2]), Convert.ToDouble(reader[3]), Convert.ToDouble(reader[4]), Convert.ToDouble(reader[5]));
                    ExerciseDescription desc = new ExerciseDescription(reader[6].ToString()) { VideoPath = reader[7].ToString(), AudioPath = reader[8].ToString() };
                    exercise = new Exercise(reader[1].ToString(), desc, profile) { ID = Convert.ToInt32(reader[0]) };
                }
                conn.Close();

                if(exercise != null)
                {
                    SqlCommand imagecmd = new SqlCommand("select * from ImagePath where exerciseid = @id", conn);
                    imagecmd.Parameters.AddWithValue("@id", id);
                    conn.Open();
                    reader = imagecmd.ExecuteReader();
                    while (reader.Read())
                    {
                        exercise.Description.ImagePaths.Add(reader[1].ToString());
                    }
                }
            }
            return exercise;
        }


        /*
        public static User ImportUser(string username) //TODO: MASSER AF ERROR-HANDLING
        {
            User user = null;
            using (StreamReader reader = new StreamReader(File.Open("Users.txt", FileMode.OpenOrCreate)))
            {
                string currentLine;
                while((currentLine = reader.ReadLine()) != null)
                {
                    string[] userParts = currentLine.Split(';');
                    if (userParts.Length >= 6 && userParts[0] == username)
                    {
                        List<int> exercisesIDs = new List<int>();
                        for (int i = 6; i < userParts.Length; i++)  
                            exercisesIDs.Add(int.Parse(userParts[i])); //Check if null (crash hvis det slutter på ;)

                        LearningProfile profile = new LearningProfile(double.Parse(userParts[2]), double.Parse(userParts[3]), double.Parse(userParts[4]), double.Parse(userParts[5]));
                        user = new User(userParts[0], profile, (UserType)int.Parse(userParts[1]), exercisesIDs);
                        break;
                    }
                }
            }
            return user;
        }

        public static void CreateUser(User user) //TRY CATCH OG ALT DET DER
        {
            string path = "Users.txt";
            if (!File.Exists(path))
                File.Create(path);

            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLine($"{user.UserName};{(int)user.Type};{user.Profile.Visual};{user.Profile.Auditory};{user.Profile.Kinesthetic};{user.Profile.Verbal}");
            }
        }

        public static bool UserExist(string username)
        {
            bool userFound = false;
            using(StreamReader reader = new StreamReader("Users.txt")) //Laver ikke fil?
            {
                string currentLine;
                while ((currentLine = reader.ReadLine()) != null)
                {
                    if(currentLine.Split(';')[0] == username)
                    {
                        userFound = true;
                        break;
                    }
                }
            }
            return userFound;
        }

        public static void CreateExercise(Exercise exercise) //TRY CATCH OG ALT DET DER
        {
            string path = "Exercises.txt";
            if (!File.Exists(path))
                File.Create(path);

            using (StreamWriter writer = new StreamWriter(path, true))
            {
                string test = $"{exercise.Name};{exercise.ID};{exercise.Profile.Visual};{exercise.Profile.Auditory};{exercise.Profile.Kinesthetic};{exercise.Profile.Verbal};{exercise.Description.TextDescription};{exercise.Description.VideoPath};{exercise.Description.AudioPath}";
                foreach (string item in exercise.Description.ImagePaths)
                    test += $";{item}";
                writer.WriteLine(test);
            }
        }

        public static Exercise ImportExerciseByID(int id) //TODO: MASSER AF ERROR-HANDLING
        {
            Exercise exercise = null;
            using (StreamReader reader = new StreamReader(File.Open("Exercises.txt", FileMode.OpenOrCreate)))
            {
                string currentLine;
                while ((currentLine = reader.ReadLine()) != null)
                {
                    string[] parts = currentLine.Split(';');
                    if (parts.Length >= 9 && Convert.ToInt32(parts[1]) == id)
                    {

                        LearningProfile profile = new LearningProfile(double.Parse(parts[2]), double.Parse(parts[3]), double.Parse(parts[4]), double.Parse(parts[5]));
                        ExerciseDescription exDesc = new ExerciseDescription(parts[6]) { VideoPath = parts[7], AudioPath = parts[8] };
                        for (int i = 9; i < parts.Length; i++)
                            exDesc.ImagePaths.Add(parts[i]); //Check if null (crash hvis det slutter på ;)
                        exercise = new Exercise(parts[0], exDesc, profile);
                        break;
                    }
                }
            }
            return exercise;
        }
        */
    }
}
