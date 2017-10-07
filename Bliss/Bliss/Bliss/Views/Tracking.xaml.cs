using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Bliss.Views
{
    public partial class Tracking : ContentPage
    {

        public Tracking()
        {
            InitializeComponent();

            cameraControl.OnTookPicture += CameraControl_OnTookPicture;
            StartTimer();
        }

        private void CameraControl_OnTookPicture(byte[] data)
        {
            
        }

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
    }
}
