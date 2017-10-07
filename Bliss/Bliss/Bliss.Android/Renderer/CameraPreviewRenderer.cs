using System;
using Android.Hardware;
using Bliss.Droid.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using System.IO;
using Android.Media;

[assembly: ExportRenderer(typeof(Bliss.Controls.CameraPreview), typeof(CameraPreviewRenderer))]
namespace Bliss.Droid.Renderer
{
    public class CameraPreviewRenderer : ViewRenderer<Bliss.Controls.CameraPreview, Bliss.Droid.Controls.CameraPreview>
    {
        Bliss.Droid.Controls.CameraPreview cameraPreview;

        protected override void OnElementChanged(ElementChangedEventArgs<Bliss.Controls.CameraPreview> e)
        {
            base.OnElementChanged(e);

            if (Control == null)
            {
                cameraPreview = new Bliss.Droid.Controls.CameraPreview(Context, OnTookPhoto);
                SetNativeControl(cameraPreview);
            }

            if (e.OldElement != null)
            {
                // Unsubscribe
                cameraPreview.Click -= OnCameraPreviewClicked;
                Element.OnTakePhotoRequest -= Element_OnTakePhotoRequest;
            }
            if (e.NewElement != null)
            {
                Control.Preview = Camera.Open((int)e.NewElement.Camera);               

                // Subscribe
                cameraPreview.Click += OnCameraPreviewClicked;
                Element.OnTakePhotoRequest += Element_OnTakePhotoRequest;
            }
        }

        private void Element_OnTakePhotoRequest(object sender, EventArgs e)
        {
            cameraPreview.TakePic();
        }

        void OnCameraPreviewClicked(object sender, EventArgs e)
        {
            //if (cameraPreview.IsPreviewing)
            //{
            //    cameraPreview.Preview.StopPreview();
            //    cameraPreview.IsPreviewing = false;
            //}
            //else
            //{
            //    cameraPreview.Preview.StartPreview();
            //    cameraPreview.IsPreviewing = true;
            //}
        }

        private void OnTookPhoto(byte[] data)
        {
            if (data != null && data.Length > 0)
                Element.InvokeTookPic(data);

            //ExifInterface ei = new ExifInterface(new MemoryStream(data));
            //int orientation = ei.GetAttributeInt(ExifInterface.TagOrientation, ExifInterface.);

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Control.Preview.Release();
            }
            base.Dispose(disposing);
        }

        static byte[] GetImageAsByteArray(string imageFilePath)
        {
            FileStream fileStream = new FileStream(imageFilePath, FileMode.Open, FileAccess.Read);
            BinaryReader binaryReader = new BinaryReader(fileStream);
            return binaryReader.ReadBytes((int)fileStream.Length);

            
        }
    }
}