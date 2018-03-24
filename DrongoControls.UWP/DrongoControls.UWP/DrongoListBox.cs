using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;

namespace DrongoControls.UWP
{
    public class DrongoListBox: ListBox
    {  
        CustomAnimations customAnimations = new CustomAnimations();
        Size oldSize;
        Size newSize;
        private object itemsToSet;

        public DrongoListBox()
        {
            customAnimations.SetupEventHandlers(FadeIn_Completed, FadeOut_Completed, AnimateHeight_Completed);
        }

        public void SetCustomItemsSource(object newItems)
        {
            oldSize = DesiredSize;
            newSize = CalculateHeight(newItems).DesiredSize;
            itemsToSet = newItems;
            //customAnimations.FadeInAndAnimateHeight(this, true, oldSize.Height, newSize.Height);
            customAnimations.FadeOut(this).Begin();
        }

        private void AnimateHeight_Completed(object sender, object e)
        {
            customAnimations.FadeIn(this).Begin();
        }

        private void FadeIn_Completed(object sender, object e)
        {

        }

        private void FadeOut_Completed(object sender, object e)
        {
            ItemsSource = itemsToSet;
            customAnimations.AnimateHeight(this, oldSize.Height, newSize.Height).Begin();
        }

        private ListBox CalculateHeight(object newItems)
        {
            ListBox lb = new ListBox();
            lb.ItemTemplate = ItemTemplate;
            lb.ItemTemplateSelector = ItemTemplateSelector;
            lb.Template = Template;
            lb.Width = lb.Width;
            lb.ItemsSource = newItems;

            lb.Measure(new Size(ActualWidth, Double.PositiveInfinity));
            lb.Arrange(new Rect(0, 0, ActualWidth, lb.DesiredSize.Height));
            lb.UpdateLayout();

            return lb;
        }
    }
}
