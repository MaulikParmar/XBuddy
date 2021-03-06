﻿using System;
using System.Collections.Generic;

namespace Bliss.Model
{
    public class Emotion
    {
        public FaceRectangle faceRectangle { get; set; }
		public Scores scores { get; set; }

		
    }

    public class EmotionList
    {

        public List<Emotion> CEmotions
        {
            get;
            set;
        }
    }


	public class FaceRectangle
	{
		public int top { get; set; }
		public int left { get; set; }
		public int width { get; set; }
		public int height { get; set; }
	}

	public class Scores
	{
		public double anger { get; set; }
		public double contempt { get; set; }
		public double disgust { get; set; }
		public double fear { get; set; }
		public double happiness { get; set; }
		public double neutral { get; set; }
		public double sadness { get; set; }
		public double surprise { get; set; }
	}
}
