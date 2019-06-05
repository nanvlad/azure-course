using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.ProjectOxford.Face.Contract;

namespace Functions
{
    public class FaceAnalysisResults
    {
        public string ImageId { get; set; }
        public Face[] Faces { get; set; }
    }
}
