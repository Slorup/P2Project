﻿using P2Project.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2Project.DAL
{
    static class FileData
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

        public static User GetUserByUsername(string username)
        {
            return null;
        }

        public static bool UserExist(string username)
        {
            return false;
        }

        public static void CreateExercise(Exercise exercise)
        {
            using(SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO Exercise([name], visualpref, auditorypref, kinestheticpref, verbalpref, description, videopath, audiopath) " + 
                    "VALUES(@name, @visual, @auditory, @kinesthetic, @verbal, @description, @videopath, @audiopath)"); //IMAGEPATHS
                //evt output id: INSERT INTO Exercise (.....) OUTPUT INSERTED.id VALUES (@.., @..)
                cmd.Parameters.AddWithValue("@name", exercise.Name);
                cmd.Parameters.AddWithValue("@visual", exercise.Profile.Visual);
                cmd.Parameters.AddWithValue("@auditory", exercise.Profile.Auditory);
                cmd.Parameters.AddWithValue("@kinesthetic", exercise.Profile.Kinesthetic);
                cmd.Parameters.AddWithValue("@verbal", exercise.Profile.Verbal);
                cmd.Parameters.AddWithValue("@description", exercise.Description.TextDescription);
                cmd.Parameters.AddWithValue("@videopath", exercise.Description.VideoPath);
                cmd.Parameters.AddWithValue("@audiopath", exercise.Description.AudioPath);
                //TRYCATCH
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static Exercise GetExerciseByID(int id)
        {
            return null;
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
