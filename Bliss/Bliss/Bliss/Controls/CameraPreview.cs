using Bliss.Helper;
using System;
using System.IO;
using System.Windows.Input;
using Xamarin.Forms;

namespace Bliss.Controls
{
    public class CameraPreview : View
    {
        #region Events
        public event TakePictureDelegate OnTookPicture;
        public event EventHandler OnTakePhotoRequest;
        #endregion

        #region Bindable Property
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

        public static readonly BindableProperty TookPhotoCommandProperty = BindableProperty.Create(
           propertyName: "TookPhotoCommand",
           returnType: typeof(ICommand),
           declaringType: typeof(CameraPreview),
           defaultValue: null);

        public ICommand TookPhotoCommand
        {
            get { return (ICommand)GetValue(CameraProperty); }
            set { SetValue(CameraProperty, value); }
        }

        #endregion

        #region Actions
        public void TakePic()
        {
            OnTakePhotoRequest(this, null);
        }

        public void InvokeTookPic(byte[] stream)
        {
            OnTookPicture?.Invoke(stream);
            // Execute command
            if (TookPhotoCommand?.CanExecute(stream)??false)
            {
                TookPhotoCommand?.Execute(stream);
            }
        }
        #endregion

    }
}
