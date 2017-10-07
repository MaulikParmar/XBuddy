using Bliss.Helper;
using System;
using Xamarin.Forms;

namespace Bliss.Controls
{
    public class CameraPreview : View
    {
        public event TakePictureDelegate OnTookPicture;
        public event EventHandler OnTakePhotoRequest;

        public static readonly BindableProperty CameraProperty = BindableProperty.Create(
            propertyName: "Camera",
            returnType: typeof(CameraOptions),
            declaringType: typeof(CameraPreview),
            defaultValue: CameraOptions.Rear);

        public CameraOptions Camera
        {
            get { return (CameraOptions)GetValue(CameraProperty); }
            set { SetValue(CameraProperty, value); }
        }

        public void TakePic()
        {
            OnTakePhotoRequest(this, null);
        }

        public void InvokeTookPic(byte[] data)
        {
            OnTookPicture?.Invoke(data);
        }
    }
}
