using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Animation;

namespace DrongoControls.UWP
{
    public class CustomAnimations
    {
        public static Storyboard FadeIn(DependencyObject control, System.EventHandler<object> function)
        {
            Storyboard sb = new Storyboard();
            sb.Completed += function;
            DoubleAnimation da = new DoubleAnimation()
            {
                Duration = TimeSpan.FromMilliseconds(150),
                To = 1,
                EasingFunction = new CircleEase { EasingMode = EasingMode.EaseInOut }
            };

            da.SetValue(Storyboard.TargetPropertyProperty, "UIElement.Opacity");
            Storyboard.SetTarget(da, control);

            sb.Children.Add(da);
            return sb;
        }

        public static Storyboard FadeOut(DependencyObject control, System.EventHandler<object> function)
        {
            Storyboard sb = new Storyboard();
            sb.Completed += function;
            DoubleAnimation da = new DoubleAnimation()
            {
                Duration = TimeSpan.FromMilliseconds(150),
                To = 0,
                EasingFunction = new CircleEase { EasingMode = EasingMode.EaseInOut }
            };

            da.SetValue(Storyboard.TargetPropertyProperty, "UIElement.Opacity");
            Storyboard.SetTarget(da, control);

            sb.Children.Add(da);
            return sb;
        }

        public static Storyboard AnimateHeight(DependencyObject control, System.EventHandler<object> function, double oldHeight, double newHeight)
        {
            Storyboard sb = new Storyboard();
            sb.Completed += function;

            DoubleAnimationUsingKeyFrames da = new DoubleAnimationUsingKeyFrames()
            {
                EnableDependentAnimation = true
            };
            da.SetValue(Storyboard.TargetPropertyProperty, "FrameworkElement.Height");

            EasingDoubleKeyFrame oldHeightKeyFrame = new EasingDoubleKeyFrame() {
                KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0)),
                Value = oldHeight
            };

            EasingDoubleKeyFrame heightKeyFrame = new EasingDoubleKeyFrame()
            {
                KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(200)),
                Value = newHeight,
                EasingFunction = new CircleEase { EasingMode = EasingMode.EaseInOut }
            };

            da.KeyFrames.Add(oldHeightKeyFrame);
            da.KeyFrames.Add(heightKeyFrame);

            Storyboard.SetTarget(da, control);

            sb.Children.Add(da);
            return sb;
        }
    }
}
