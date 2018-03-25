using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace DrongoControls.UWP
{
    public class DrongoListBox: ListBox
    {
        CustomAnimations customAnimations = new CustomAnimations();
        Size oldSize;
        private object itemsToSet;
        private EventHandler<object> updateDataEvent;


        public DrongoListBox()
        {
            customAnimations.SetupEventHandlers(FadeIn_Completed, FadeOut_Completed, AnimateHeight_Completed);
        }

        public void SetUpdateDataEvent(EventHandler<object> updateDataEvent)
        {
            this.updateDataEvent = updateDataEvent;
        }

        public void SetCustomItemsSource(object newItems)
        {
            oldSize = DesiredSize;
            itemsToSet = newItems;
            customAnimations.FadeOut(this).Begin();
        }

        private void AnimateHeight_Completed(object sender, object e)
        {
            customAnimations.FadeIn(this).Begin();
            Height = Double.NaN;
        }

        private void FadeIn_Completed(object sender, object e)
        {

        }

        private void FadeOut_Completed(object sender, object e)
        {
            //ItemsSource = itemsToSet;
            updateDataEvent?.Invoke(null, itemsToSet);
            InvalidateArrange();
            InvalidateMeasure();
            Debug.WriteLine("mid height: " + Height);
            Measure(new Size(ActualWidth, Double.PositiveInfinity));
            Arrange(new Rect(0, 0, ActualWidth, DesiredSize.Height));

            UpdateLayout();

            customAnimations.AnimateHeight(this, oldSize.Height, DesiredSize.Height).Begin();
        }
    }
}
