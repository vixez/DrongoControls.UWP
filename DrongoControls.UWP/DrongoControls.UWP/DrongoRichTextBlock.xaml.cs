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
    public sealed partial class DrongoRichTextBlock : UserControl
    {
        private string _text;

        public static DependencyProperty NewHeightProperty = DependencyProperty.Register("NewHeight", typeof(double), typeof(DrongoTextBlock), new PropertyMetadata("NewHeight"));
        public double NewHeight
        {
            get { return (double)GetValue(NewHeightProperty); }
            set { this.SetValue(NewHeightProperty, value); }
        }

        Size NewSize;

        public static DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(DrongoTextBlock), new PropertyMetadata("Text"));
        public string Text
        {
            get
            {
                return _text;
            }
            set
            {
                this.SetValue(TextProperty, value);
                Animate(value, fixedWidth: true);
            }
        }

        public static DependencyProperty AutoHeightProperty = DependencyProperty.Register("AutoHeight", typeof(bool), typeof(DrongoTextBlock), new PropertyMetadata("AutoHeight"));
        public bool AutoHeight
        {
            get { return (bool)GetValue(AutoHeightProperty); }

            set
            {
                this.SetValue(AutoHeightProperty, value);
                if (value)
                {
                    tbCurrent.Width = Double.NaN;
                }
            }
        }

        public string TextNonAnimated
        {
            get
            {
                return _text;
            }
            set
            {
                _text = value;
            }
        }

        bool IsText;
        object NewContent;

        public DrongoRichTextBlock()
        {
            this.InitializeComponent();
        }


        public RichTextBlock CalculateHeight(RichTextBlock currentTextBlock, double width)
        {
            var tb = new RichTextBlock { FontSize = currentTextBlock.FontSize };
            if (_text == null) return tb;
            // Create run and set text
            Run run = new Run();
            run.Text = _text;

            // Create paragraph
            Paragraph paragraph = new Paragraph();

            // Add run to the paragraph
            paragraph.Inlines.Add(run);
            tb.Blocks.Add(paragraph);

            tb.MinWidth = width;
            tb.MaxWidth = width;
            tb.Width = width;
            tb.MaxHeight = Double.PositiveInfinity;
            tb.TextWrapping = TextWrapping.WrapWholeWords;

            tb.Measure(new Size(width, Double.PositiveInfinity));
            tb.Arrange(new Rect(0, 0, width, tb.DesiredSize.Height));
            tb.UpdateLayout();

            return tb;
        }

        public void AddInline(Block item)
        {
            tbCurrent.Blocks.Add(item);
        }

        public void SetInline(List<Block> collection)
        {
            Animate(collection, isText: false, fixedWidth: true);
            //SetInlinePrivate(tbCurrent, collection);
        }

        public BlockCollection GetInlineCollection()
        {
            return tbCurrent.Blocks;
        }

        private void SetInlinePrivate(RichTextBlock dTb, List<Block> collection, bool animate = true)
        {
            if (collection == null) return;
            if (animate)
            {
                Animate(collection, isText: false, fixedWidth: true);
            }

            dTb.Blocks.Clear();
            foreach (var item in collection)
            {
                dTb.Blocks.Add(item);
            }
        }

        public void Animate(object newContent, bool isText = true, bool fixedWidth = true)
        {
            //Get current size
            Size oldSize = CalculateHeight(tbCurrent, tbCurrent.RenderSize.Width).DesiredSize;

            DrongoRichTextBlock dTb = new DrongoRichTextBlock();
            if (fixedWidth)
            {
                dTb.Width = tbCurrent.RenderSize.Width;
                oldHeightKeyFrame.Value = oldSize.Height;
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
                SetInlinePrivate(dTb.tbCurrent, (List<Block>)newContent, false);
            }

            // New size
            Size newSize = dTb.CalculateHeight(dTb.tbCurrent, oldSize.Width).DesiredSize;

            if (fixedWidth)
            {
                //tbCurrent.Height = newSize.Height;

                heightKeyFrame.Value = newSize.Height;
            }
            else
            {
                //tbCurrent.Width = newSize.Width;
            }

            dTb.tbCurrent.Blocks.Clear();


            NewSize = newSize;

            IsText = isText;
            NewContent = newContent;

            FadeOut.Begin();
        }

        private void AnimateHeight_Completed(object sender, object e)
        {
            if (IsText)
            {
                TextNonAnimated = (string)NewContent;
            }
            else
            {
                SetInlinePrivate(tbCurrent, (List<Block>)NewContent, false);
            }
            tbCurrent.Height = Double.NaN;
            FadeIn.Begin();
        }

        private void FadeIn_Completed(object sender, object e)
        {

        }

        private void FadeOut_Completed(object sender, object e)
        {
            AnimateHeight.Begin();
        }
    }
}
