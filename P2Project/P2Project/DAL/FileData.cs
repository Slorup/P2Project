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
            using (StreamReader reader = new StreamReader("PATH"))
            {
                string currentline;
                while((currentline = reader.ReadLine()) != null)
                {
                    string[] userParts = currentline.Split(';');
                    if (userParts.Length >= 6 && userParts[0] == username)
                    {
                        List<int> exercisesIDs = new List<int>();
                        for (int i = 6; i < userParts.Length; i++)   //TEST
                            exercisesIDs.Add(int.Parse(userParts[i]));

                        LearningProfile profile = new LearningProfile(double.Parse(userParts[2]), double.Parse(userParts[3]), double.Parse(userParts[4]), double.Parse(userParts[5]));
                        user = new User(userParts[0], profile, (UserType)int.Parse(userParts[1]), exercisesIDs);
                    }
                }
            }
            return user;
        }
    }
}
