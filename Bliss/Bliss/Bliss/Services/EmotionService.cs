using System;
using Microsoft.ProjectOxford.Emotion;
using Microsoft.ProjectOxford.Emotion.Contract;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
namespace Bliss.Services
{
    public class EmotionService
    {
        internal Task<Bliss.Model.Emotion[]> GetEmotions(Stream stream)
        {
            var eClient=new EmotionServiceClient("");
			var eResults = await eClient.RecognizeAsync(stream);

			if (emotionResults == null || emotionResults.Count() == 0)
			{
				throw new Exception("Can't detect face");
			}

			return eResults;

        }

    }
}
