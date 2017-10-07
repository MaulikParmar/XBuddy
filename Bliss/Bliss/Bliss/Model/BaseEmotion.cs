using System;
namespace Bliss.Model
{
    public class BaseEmotion
    {
        public int UserId
        {
            get;
            set;
        }

        public EmotionList MyEmotion
        {
            get;
            set;
        }
        public DateTime  DateUtc
        {
            get;
            set;
        }

        [Microsoft.WindowsAzure.MobileServices.Version]
        public string AzureVersion
        {
            get;
            set;
        }
    }
}
