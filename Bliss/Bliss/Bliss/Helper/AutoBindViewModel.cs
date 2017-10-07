using Xamarin.Forms;
using System.Reflection;
using System;

namespace Bliss.Helper
{
    public class AutoBindViewModel
    {
        public static readonly BindableProperty IsAutoBindProperty =
                BindableProperty.CreateAttached("IsAutoBind", typeof(bool), typeof(AutoBindViewModel), false, propertyChanged:OnPropertyChanged);

        public static bool GetIsAutoBind(BindableObject view)
        {
            return (bool)view.GetValue(IsAutoBindProperty);
        }

        public static void SetIsAutoBind(BindableObject view, bool value)
        {
            view.SetValue(IsAutoBindProperty, value);
        }

        private static void OnPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            // On Proeprty Changed
            if (bindable != null)
            {
                // Get namespace
                var _namespace = bindable.GetType()
                        .FullName
                        .Replace(".Views.", ".ViewModels.")
                        + "ViewModel";

                // Get instance
                var instace = Activator.CreateInstance(Type.GetType(_namespace));
                bindable.BindingContext = instace;
            }
        }

       
    }
}
