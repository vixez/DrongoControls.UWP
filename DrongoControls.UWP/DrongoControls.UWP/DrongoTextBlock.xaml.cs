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
                Animate(value, fixedWidth:true);
                //tbCurrent.Text = value;
            }
        }

        public string TextNonAnimated
        {
            get
            {
                return tbCurrent.Text;
            }
            set
            {
                tbCurrent.Text = value;
            }
        }

        public DrongoTextBlock()
        {
            this.InitializeComponent();
        }

        public Size CalculateHeight(TextBlock currentTextBlock)
        {
            var tb = new TextBlock { Text = currentTextBlock.Text, FontSize = currentTextBlock.FontSize };
            tb.MaxWidth = currentTextBlock.RenderSize.Width;
            tb.TextWrapping = TextWrapping.WrapWholeWords;

            tb.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
           
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

        private void SetInlinePrivate(TextBlock dTb, InlineCollection collection, bool animate = true)
        {
            if (collection == null) return;
            if (animate)
            {
                Animate(collection, fixedWidth: true);
            }

            tbCurrent.Inlines.Clear();
            foreach (var item in collection.ToList())
            {
                tbCurrent.Inlines.Add(item);
            }
        }

        public void Animate(object newContent, bool isText = true, bool fixedWidth = true)
        {
            //Get current size
            Size oldSize = CalculateHeight(tbCurrent);

            DrongoTextBlock dTb = new DrongoTextBlock();
            if (fixedWidth)
            {
                dTb.Width = tbCurrent.RenderSize.Width;
            }
            else
            {
                dTb.Height = tbCurrent.RenderSize.Height;
            }

            if (isText)
            {
                dTb.TextNonAnimated = (string)newContent;
            }
            else
            {
                SetInlinePrivate(dTb.tbCurrent, (InlineCollection)newContent, false);
            }

            // New size
            Size newSize = dTb.CalculateHeight(dTb.tbCurrent);
            newSize = newSize;

            if (fixedWidth)
            {
                tbCurrent.Height = newSize.Height;
            }
            else
            {
                tbCurrent.Width = newSize.Width;
            }

            if (isText)
            {
                TextNonAnimated = (string)newContent;
            }
            else
            {
                SetInlinePrivate(tbCurrent, (InlineCollection)newContent, false);
            }
        }
    }
}
