using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bliss.Model
{   

    public class EmotionsResponse
    {
        public Facerectangle faceRectangle { get; set; }
        public Scores scores { get; set; }
    }

    public class Facerectangle
    {
        public int height { get; set; }
        public int left { get; set; }
        public int top { get; set; }
        public int width { get; set; }
    }  

}
