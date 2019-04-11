using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace P2Project.Model
{
    class ExerciseDescription
    {
        public string TextDescription { get; set; }
        public string VideoPath { get; set; }
        public string AudioPath { get; set; }
        public List<string> ImagePaths { get; set; }

        public ExerciseDescription(string textDescription, string videoPath, string audioPath, List<string> imagePaths)
        {
            TextDescription = textDescription;
            VideoPath = videoPath;
            AudioPath = audioPath;
            ImagePaths = imagePaths;
        }

        public ExerciseDescription(string textDescription) : this(textDescription, null, null, null) { }
        public ExerciseDescription(string textDescription, string videoPath) : this(textDescription, videoPath, null, null) { }
        //TODO
    }
}
