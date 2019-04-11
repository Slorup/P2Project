using P2Project.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2Project.DAL
{
    static class FileData
    {
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
                    if (parts.Length >= 9 && Convert.ToInt32(parts[0]) == id)
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

    }
}
