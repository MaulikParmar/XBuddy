using System;
using System.Collections.Generic;
using System.IO;
using Xamarin.Forms;

namespace Bliss.Views
{
    public partial class Home : ContentPage
    {
        public Home()
        {
            InitializeComponent();



            GetEmotions();
        }

		public Task<Bliss.Model.Emotion[]> GetEmotions(Stream stream)
		{
			var eClient = new EmotionServiceClient("b1ffaabad2a24bc4909260d0dba5214a");
			var eResults = await eClient.RecognizeAsync(stream);

			if (emotionResults == null || emotionResults.Count() == 0)
			{
				throw new Exception("Can't detect face");
			}

			return eResults;

		}
    }
}
