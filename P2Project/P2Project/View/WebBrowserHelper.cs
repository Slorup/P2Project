using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace P2Project.View
{
    public class WebBrowserHelper
    {
        //Creates a new bindable property for the webbrowser
        public static readonly DependencyProperty BindableSource = DependencyProperty.RegisterAttached("BindableSource", typeof(string), typeof(WebBrowserHelper), new PropertyMetadata(BindableSourcePropertyChanged));

        public static object GetBindableSource(DependencyObject obj)
        {
            return (string)obj.GetValue(BindableSource);
        }

        public static void SetBindableSource(DependencyObject obj, object value)
        {
            obj.SetValue(BindableSource, value);
        }

        //Sets browser source to uri/string when bindablesource is changed
        private static void BindableSourcePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            WebBrowser browser = d as WebBrowser;
            if (browser == null) return;

            Uri uri = null;

            if (e.NewValue is Uri)
                uri = e.NewValue as Uri;
            else if(e.NewValue is string)
            {
                string uriString = e.NewValue as string;
                uri = string.IsNullOrWhiteSpace(uriString) ? null : new Uri(uriString);
            }
            browser.Source = uri;
        }
    }
}
