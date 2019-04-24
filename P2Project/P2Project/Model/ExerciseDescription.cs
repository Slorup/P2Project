﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace P2Project.Model
{
    public class ExerciseDescription
    {
        public string TextDescription { get; set; } 
        public string VideoPath { get; set; }
        public string AudioPath { get; set; }
        public List<string> ImagePaths { get; set; } //MAX IMAGE SIZE

        public ExerciseDescription(string textDescription)
        {
            TextDescription = textDescription;
            ImagePaths = new List<string>();
            //ToDo
        }

    }
}
