using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Bliss.ViewModels
{
    public class TrackingViewModel : BaseViewModel
    {
        #region Constructor
        public TrackingViewModel()
        {
            TookPhotoCommand = new Command<byte[]>(OnTookPhoto);
            StartStopCommand = new Command(OnStartStop);
        }       
        #endregion

        #region Commands
        public ICommand TookPhotoCommand { get; private set; }
        public ICommand StartStopCommand { get; private set; }

        #endregion

        #region Actions
        private void OnTookPhoto(byte[] data)
        {
           
        }

        private void OnStartStop()
        {

        }
        #endregion
    }
}
