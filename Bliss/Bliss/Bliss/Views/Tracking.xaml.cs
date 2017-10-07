using Bliss.Services;
using ExifLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Xamarin.Forms;

namespace Bliss.Views
{
    public partial class Tracking : ContentPage
    {
        #region Properties
        private EmotionService service;
        bool isCalled = false;
        #endregion

        #region Constructor
        public Tracking()
        {
            InitializeComponent();

            cameraControl.OnTookPicture += CameraControl_OnTookPictureAsync;
            StartTimer();
        }
        #endregion

        #region Events
        private async void CameraControl_OnTookPictureAsync(byte[] stream)
        {
            if (service == null)
            {
                service = new EmotionService();

                var result = await service.MakeRequest(stream);
                //var result = await service.GetEmotionsAsync(stream);

            }

            Stream memory = new MemoryStream(stream);
            img.Source = ImageSource.FromStream(() => memory);

            var picInfo = ExifReader.ReadJpeg(new MemoryStream(stream));
            ExifOrientation orientation = picInfo.Orientation;
        }
        #endregion

        #region Actions
        private void StartTimer()
        {
            Device.StartTimer(TimeSpan.FromSeconds(20), () =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    cameraControl.TakePic();
                });

                return true;
            });
        }
        #endregion

        private void GetMaxValue(Model.Scores score)
        {
            var result = score.GetType()
                            .GetRuntimeProperties()
                            .Where(x => x.PropertyType == typeof(double))
                            .Select(x => new { Property = x.Name, Value = Convert.ToDouble(x.GetValue(score)) })
                            .OrderBy(x => x.Value)
                            .First();
        }
    }
}
