﻿using System;
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
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace DrongoControls.UWPTest
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void btnSetText_Tapped(object sender, TappedRoutedEventArgs e)
        {
            drongoTextBlock.Text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit";
        }

        private void btnSetTextLarge_Tapped(object sender, TappedRoutedEventArgs e)
        {
            drongoTextBlock.Text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat" +
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat" +
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.";
        }

        private void btnInlineText_Tapped(object sender, TappedRoutedEventArgs e)
        {
            List<Inline> ic = new List<Inline>();


            Run ln = new Run();
            ln.Text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, ";

            Run ln1 = new Run();
            ln1.Text = "link";

            Run ln2 = new Run();
            ln2.Text = " sed do eiusmod tempor incididunt ut";

            Hyperlink hp = new Hyperlink();
            hp.NavigateUri = new Uri("http://www.google.com");
            hp.Inlines.Add(ln1);

            ic.Add(ln);
            ic.Add(hp);
            ic.Add(ln2);

            drongoTextBlock.SetInline(ic);
        }
    }
}
