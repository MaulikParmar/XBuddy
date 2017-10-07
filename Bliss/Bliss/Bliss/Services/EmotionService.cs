using System;
using Microsoft.ProjectOxford.Emotion;
using Microsoft.ProjectOxford.Emotion.Contract;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Bliss.Services
{
    public class EmotionService
    {
        internal async Task<Emotion[]> GetEmotionsAsync(Stream stream)
        {
            var eClient=new EmotionServiceClient("");
			var eResults = await eClient.RecognizeAsync(stream);

			if (eResults == null || eResults.Count() == 0)
			{
				throw new Exception("Can't detect face");
			}

			return eResults;

        }

        public  async Task<List<Model.EmotionsResponse>> MakeRequest(byte[] data)
        {
            var client = new HttpClient();

            // Request headers - replace this example key with your valid key.
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "b1ffaabad2a24bc4909260d0dba5214a");

            // NOTE: You must use the same region in your REST call as you used to obtain your subscription keys.
            //   For example, if you obtained your subscription keys from westcentralus, replace "westus" in the 
            //   URI below with "westcentralus".
            string uri = "https://westus.api.cognitive.microsoft.com/emotion/v1.0/recognize?";
            HttpResponseMessage response;
            string responseContent;

            // Request body. Try this sample with a locally stored JPEG image.
            byte[] byteData =data;

            using (var content = new ByteArrayContent(byteData))
            {
                // This example uses content type "application/octet-stream".
                // The other content types you can use are "application/json" and "multipart/form-data".
                content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                response = await client.PostAsync(uri, content);
                responseContent = response.Content.ReadAsStringAsync().Result;

                var result = JsonConvert.DeserializeObject<List<Model.EmotionsResponse>>(responseContent);
                return result;
            }


            //A peak at the JSON response.
            //Console.WriteLine(responseContent);
        }

       

    }
}
