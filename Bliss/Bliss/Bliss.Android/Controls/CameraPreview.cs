using System;
using System.Collections.Generic;
using Android.Content;
using Android.Hardware;
using Android.Runtime;
using Android.Views;
using System.IO;
using Android.Widget;
using Android.Util;
using Bliss.Helper;

namespace Bliss.Droid.Controls
{
    public sealed class CameraPreview : ViewGroup, ISurfaceHolderCallback, 
        Camera.IPictureCallback,
        Camera.IPreviewCallback,
        Camera.IShutterCallback
    {
        SurfaceView surfaceView;
        ISurfaceHolder holder;
        Camera.Size previewSize;
        IList<Camera.Size> supportedPreviewSizes;
        Camera camera;
        IWindowManager windowManager;
        private Action<byte[]> _tookPhotoCallback;

        public bool IsPreviewing { get; set; }

        public Camera Preview
        {
            get { return camera; }
            set
            {
                camera = value;
                if (camera != null)
                {
                    supportedPreviewSizes = Preview.GetParameters().SupportedPreviewSizes;
                    RequestLayout();
                }
            }
        }

        public CameraPreview(Context context, Action<byte[]> callBack)
            : base(context)
        {
            _tookPhotoCallback = callBack;
            surfaceView = new SurfaceView(context);
            AddView(surfaceView);

            windowManager = Context.GetSystemService(Context.WindowService).JavaCast<IWindowManager>();

            IsPreviewing = false;
            holder = surfaceView.Holder;
            holder.AddCallback(this);
        }

        protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
        {
            int width = ResolveSize(SuggestedMinimumWidth, widthMeasureSpec);
            int height = ResolveSize(SuggestedMinimumHeight, heightMeasureSpec);
            SetMeasuredDimension(width, height);

            if (supportedPreviewSizes != null)
            {
                previewSize = GetOptimalPreviewSize(supportedPreviewSizes, width, height);
            }
        }

        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            var msw = MeasureSpec.MakeMeasureSpec(r - l, MeasureSpecMode.Exactly);
            var msh = MeasureSpec.MakeMeasureSpec(b - t, MeasureSpecMode.Exactly);

            surfaceView.Measure(msw, msh);
            surfaceView.Layout(0, 0, r - l, b - t);
        }

        public void SurfaceCreated(ISurfaceHolder holder)
        {
            try
            {
                if (Preview != null)
                {
                    Preview.SetPreviewDisplay(holder);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(@"ERROR: ", ex.Message);
            }
        }

        public void SurfaceDestroyed(ISurfaceHolder holder)
        {
            if (Preview != null)
            {
                Preview.StopPreview();
            }
        }

        public void SurfaceChanged(ISurfaceHolder holder, Android.Graphics.Format format, int width, int height)
        {
            var parameters = Preview.GetParameters();
            parameters.SetPreviewSize(previewSize.Width, previewSize.Height);
            RequestLayout();

            switch (windowManager.DefaultDisplay.Rotation)
            {
                case SurfaceOrientation.Rotation0:
                    camera.SetDisplayOrientation(90);
                    break;
                case SurfaceOrientation.Rotation90:
                    camera.SetDisplayOrientation(0);
                    break;
                case SurfaceOrientation.Rotation270:
                    camera.SetDisplayOrientation(180);
                    break;
            }

            Preview.SetParameters(parameters);
            Preview.StartPreview();
            IsPreviewing = true;           
        }

        Camera.Size GetOptimalPreviewSize(IList<Camera.Size> sizes, int w, int h)
        {
            const double AspectTolerance = 0.1;
            double targetRatio = (double)w / h;

            if (sizes == null)
            {
                return null;
            }

            Camera.Size optimalSize = null;
            double minDiff = double.MaxValue;

            int targetHeight = h;
            foreach (Camera.Size size in sizes)
            {
                double ratio = (double)size.Width / size.Height;

                if (Math.Abs(ratio - targetRatio) > AspectTolerance)
                    continue;
                if (Math.Abs(size.Height - targetHeight) < minDiff)
                {
                    optimalSize = size;
                    minDiff = Math.Abs(size.Height - targetHeight);
                }
            }

            if (optimalSize == null)
            {
                minDiff = double.MaxValue;
                foreach (Camera.Size size in sizes)
                {
                    if (Math.Abs(size.Height - targetHeight) < minDiff)
                    {
                        optimalSize = size;
                        minDiff = Math.Abs(size.Height - targetHeight);
                    }
                }
            }

            return optimalSize;
        }

        public void OnPictureTaken(byte[] data, Camera camera)
        {
            if (data != null)
            {
                //Preview.StopPreview();
                _tookPhotoCallback?.Invoke(data);
                Preview.StartPreview();
            }
        }

        public void OnPreviewFrame(byte[] data, Camera camera)
        {
           
        }

        public void OnShutter()
        {
           
        }

        public void TakePic()
        {
            // Call to take pic from camera
            Android.Hardware.Camera.Parameters p = Preview.GetParameters();
            p.PictureFormat = Android.Graphics.ImageFormatType.Jpeg;
            //var size = p.PreviewSize;
            Preview.SetParameters(p);
            Preview.TakePicture(this, this, this);
        }
    }

    public class CustomCameraView : FrameLayout,
        Camera.IPictureCallback,
        Camera.IPreviewCallback,
        Camera.IShutterCallback
    {
        Android.Graphics.SurfaceTexture _surface;      

        public CustomCameraView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
        }

        #region ICustomCameraView interface implementation

       
        #endregion

        //https://forums.xamarin.com/discussion/17625/custom-camera-takepicture
        void Camera.IPictureCallback.OnPictureTaken(byte[] data, Android.Hardware.Camera camera)
        {
            //File dataDir = Android.OS.Environment.ExternalStorageDirectory;
            //if (data != null)
            //{
            //    try
            //    {
            //        var path = dataDir + "/" + pictureName;
            //        //SaveFile(path, data);
            //        RotateBitmap(path, data);
            //        //Callback(path);
            //    }
            //    catch (FileNotFoundException e)
            //    {
            //        System.Console.Out.WriteLine(e.Message);
            //    }
            //    catch (IOException ie)
            //    {
            //        System.Console.Out.WriteLine(ie.Message);
            //    }
            //}
        }


        void Camera.IPreviewCallback.OnPreviewFrame(byte[] b, Android.Hardware.Camera c)
        {
        }

        void Camera.IShutterCallback.OnShutter()
        {
        }


    }

}