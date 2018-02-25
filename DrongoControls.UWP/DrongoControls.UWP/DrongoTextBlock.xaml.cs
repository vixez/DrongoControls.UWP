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
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace DrongoControls.UWP
{
    public sealed partial class DrongoTextBlock : UserControl
    {
        public string Text
        {
            get
            {
                return tbCurrent.Text;
            }
            set
            {
                Animate(value);
                //tbCurrent.Text = value;
            }
        }

        public DrongoTextBlock()
        {
            this.InitializeComponent();
        }

        public Size CalculateHeight(TextBlock currentTextBlock)
        {
            var tb = new TextBlock { Text = currentTextBlock.Text, FontSize = currentTextBlock.FontSize };
            tb.Measure(new Size(Double.PositiveInfinity, currentTextBlock.ActualWidth));
            return tb.DesiredSize;
        }

        public void AddInline(Inline item)
        {
            tbCurrent.Inlines.Add(item);
        }

        public void SetInline(InlineCollection collection)
        {
            SetInlinePrivate(tbCurrent, collection);
        }

        private void SetInlinePrivate(TextBlock dTb, InlineCollection collection)
        {
            if (collection == null) return;
            Animate(collection);


            tbCurrent.Inlines.Clear();
            foreach (var item in collection.ToList())
            {
                tbCurrent.Inlines.Add(item);
            }
        }

        public void AnimatedText(DrongoTextBlock dTb, string text)
        {
            dTb.Text = text;
        }

        public void AnimatedInline(DrongoTextBlock dTb, InlineCollection collection)
        {
            dTb.SetInlinePrivate(dTb.tbCurrent, collection);
        }

        public void Animate(object newContent, bool isText = true)
        {
            //Get current size
            Size oldSize = CalculateHeight(tbCurrent);

            DrongoTextBlock dTb = new DrongoTextBlock();
            dTb.Width = tbCurrent.ActualWidth;
            dTb.Height = tbCurrent.ActualHeight;
            dTb.SetInline(tbCurrent.Inlines);
            if (isText)
            {
                AnimatedText(dTb, (string)newContent);
            }
            else
            {
                AnimatedInline(dTb, (InlineCollection)newContent);
            }

            Size newSize = dTb.CalculateHeight(dTb.tbCurrent);
            newSize = newSize;
        }
    }
}
