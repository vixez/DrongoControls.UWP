using System;
using System.Collections.Generic;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Documents;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace DrongoControls.UWP
{
    public sealed partial class DrongoRichTextBlock : UserControl
    {
        double _oldHeightKeyFrame = 0;
        double _heightKeyFrame = 0;
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
        CustomAnimations customAnimations = new CustomAnimations();

        public DrongoRichTextBlock()
        {
            this.InitializeComponent();
            customAnimations.SetupEventHandlers(FadeIn_Completed, FadeOut_Completed, AnimateHeight_Completed);
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

        public void SetInline(List<Block> collection, string newText)
        {
            Animate(collection, isText: false, fixedWidth: true, newText:newText);
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

        public void Animate(object newContent, bool isText = true, bool fixedWidth = true, string newText = "")
        {
            //Get current size
            Size oldSize = CalculateHeight(tbCurrent, tbCurrent.RenderSize.Width).DesiredSize;
            TextNonAnimated = newText;

            DrongoRichTextBlock dTb = new DrongoRichTextBlock();
            if (fixedWidth)
            {
                dTb.Width = tbCurrent.RenderSize.Width;
                _oldHeightKeyFrame = oldSize.Height;
            }
            else
            {
                dTb.Height = tbCurrent.RenderSize.Height;
            }

            if (isText)
            {
               
               
            }
            else
            {
                
            }

            dTb.TextNonAnimated = newText;
            SetInlinePrivate(dTb.tbCurrent, (List<Block>)newContent, false);

            // New size
            Size newSize = dTb.CalculateHeight(dTb.tbCurrent, oldSize.Width).DesiredSize;

            if (fixedWidth)
            {
                //tbCurrent.Height = newSize.Height;

                _heightKeyFrame = newSize.Height;
            }
            else
            {
                //tbCurrent.Width = newSize.Width;
            }

            dTb.tbCurrent.Blocks.Clear();


            NewSize = newSize;

            IsText = isText;
            NewContent = newContent;

            customAnimations.FadeOut(tbCurrent).Begin();
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
            customAnimations.FadeIn(tbCurrent).Begin();
        }

        private void FadeIn_Completed(object sender, object e)
        {

        }

        private void FadeOut_Completed(object sender, object e)
        {
            customAnimations.AnimateHeight(tbCurrent, _oldHeightKeyFrame, _heightKeyFrame).Begin();
        }
    }
}
