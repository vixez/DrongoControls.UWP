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

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace DrongoControls.UWP
{
    public sealed partial class DrongoTextBlock : UserControl
    {
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
                return tbCurrent.Text;
            }
            set
            {
                this.SetValue(TextProperty, value);
                Animate(value, fixedWidth:true);
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
                return tbCurrent.Text;
            }
            set
            {
                tbCurrent.Text = value;
            }
        }

        bool IsText;
        object NewContent;

        public DrongoTextBlock()
        {
            this.InitializeComponent();
        }

        public TextBlock CalculateHeight(TextBlock currentTextBlock, double width)
        {
            var tb = new TextBlock { Text = currentTextBlock.Text, FontSize = currentTextBlock.FontSize };
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

        public void AddInline(Inline item)
        {
            tbCurrent.Inlines.Add(item);
        }

        public void SetInline(List<Inline> collection)
        {
            Animate(collection, isText:false, fixedWidth: true);
            //SetInlinePrivate(tbCurrent, collection);
        }

        public InlineCollection GetInlineCollection()
        {
            return tbCurrent.Inlines;
        }

        private void SetInlinePrivate(TextBlock dTb, List<Inline> collection, bool animate = true)
        {
            if (collection == null) return;
            if (animate)
            {
                Animate(collection, isText:false, fixedWidth: true);
            }

            dTb.Inlines.Clear();
            foreach (var item in collection)
            {
                dTb.Inlines.Add(item);
            }
        }

        public void Animate(object newContent, bool isText = true, bool fixedWidth = true)
        {
            //Get current size
            Size oldSize = CalculateHeight(tbCurrent, tbCurrent.RenderSize.Width).DesiredSize;

            DrongoTextBlock dTb = new DrongoTextBlock();
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
                SetInlinePrivate(dTb.tbCurrent, (List<Inline>)newContent, false);
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

            dTb.tbCurrent.Inlines.Clear();


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
                SetInlinePrivate(tbCurrent, (List<Inline>)NewContent, false);
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
