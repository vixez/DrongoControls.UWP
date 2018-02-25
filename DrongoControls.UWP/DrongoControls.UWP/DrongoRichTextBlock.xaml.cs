using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace DrongoControls.UWP
{
    public sealed partial class DrongoRichTextBlock : UserControl
    {
        public static DependencyProperty HeaderTitleProperty = DependencyProperty.Register("Text", typeof(string), typeof(DrongoRichTextBlock), new PropertyMetadata("Text"));
        public string Text
        {
            get { return (string)GetValue(HeaderTitleProperty); }
            set { this.SetValue(HeaderTitleProperty, value); }
        }


        public DrongoRichTextBlock()
        {
            this.InitializeComponent();
        }
    }
}
