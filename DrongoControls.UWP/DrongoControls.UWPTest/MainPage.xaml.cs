using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Text;
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
            //drongoRichTextBlock.Text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit";
        }

        private void btnSetTextLarge_Tapped(object sender, TappedRoutedEventArgs e)
        {
            drongoTextBlock.Text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat" +
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat" +
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.";
            //drongoRichTextBlock.Text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat" +
            //    "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat" +
            //    "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.";

        }

        private void btnInlineText_Tapped(object sender, TappedRoutedEventArgs e)
        {
            List<Inline> ic = new List<Inline>();
            List<Block> bc = new List<Block>();


            Run ln = new Run();
            ln.Text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, ";

            Run ln1 = new Run();
            ln1.Text = "link";

            Run ln2 = new Run();
            ln2.Text = " sed do eiusmod tempor incididunt ut";

            Hyperlink hp = new Hyperlink();
            hp.NavigateUri = new Uri("http://www.google.com");
            hp.Inlines.Add(ln1);

            // TextBlock
            //ic.Add(ln);
            //ic.Add(hp);
            //ic.Add(ln2);
            //drongoTextBlock.SetInline(ic);

            // RichTextBlocksour
            Paragraph para = new Paragraph();

            para.Inlines.Add(ln);

            InlineUIContainer iuic = new InlineUIContainer();
            TextBlock hpb = new TextBlock();
            hpb.Text = "link";
            hpb.Tag = "tag clicked";
            hpb.Tapped += Hpb_Click;
            hpb.TextDecorations = TextDecorations.Underline;
            hpb.Foreground = new SolidColorBrush((Color)this.Resources["SystemAccentColor"]);
            hpb.PointerEntered += Hpb_PointerEntered;
            hpb.PointerExited += Hpb_PointerExited;
            iuic.Child = hpb;
           
            para.Inlines.Add(iuic);

            para.Inlines.Add(ln2);
            bc.Add(para);

            // End
            string text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, link sed do eiusmod tempor incididunt ut";
            

            // drongoRichTextBlock.Text = 

            drongoRichTextBlock.SetInline(bc, text);
        }

        private void Hpb_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 2);
        }

        private void Hpb_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 1);
        }

        private void Hpb_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Tag: " + ((TextBlock)sender).Tag);
        }

        private void btnListBoxItems_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Random rnd = new Random();
            List<string> list = new List<string>();
            int random = rnd.Next(3, 10);
            for (int i = 0; i < random; i++)
            {
                list.Add("String: " + i);
            }

            drongoListBox.SetCustomItemsSource(list);
        }
    }
}
