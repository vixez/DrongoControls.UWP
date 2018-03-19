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
        Storyboard sbFadeIn = new Storyboard();
        Storyboard sbFadeOut = new Storyboard();
        Storyboard sbFadeAnimateHeight = new Storyboard();

        public void SetupEventHandlers(EventHandler<object> fadeIn, EventHandler<object> fadeOut, EventHandler<object> animateHeight)
        {
            sbFadeIn.Completed += fadeIn;
            sbFadeOut.Completed += fadeOut;
            sbFadeAnimateHeight.Completed += animateHeight;
        }

        public Storyboard FadeIn(DependencyObject control)
        {
            DoubleAnimation da = new DoubleAnimation()
            {
                Duration = TimeSpan.FromMilliseconds(150),
                To = 1,
                EasingFunction = new CircleEase { EasingMode = EasingMode.EaseInOut }
            };

            da.SetValue(Storyboard.TargetPropertyProperty, "UIElement.Opacity");
            Storyboard.SetTarget(da, control);

            sbFadeIn.Children.Add(da);
            return sbFadeIn;
        }

        public Storyboard FadeOut(DependencyObject control)
        {
            DoubleAnimation da = new DoubleAnimation()
            {
                Duration = TimeSpan.FromMilliseconds(150),
                To = 0,
                EasingFunction = new CircleEase { EasingMode = EasingMode.EaseInOut }
            };

            da.SetValue(Storyboard.TargetPropertyProperty, "UIElement.Opacity");
            Storyboard.SetTarget(da, control);

            sbFadeOut.Children.Add(da);
            return sbFadeOut;
        }

        public Storyboard AnimateHeight(DependencyObject control, double oldHeight, double newHeight)
        {
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

            sbFadeAnimateHeight.Children.Add(da);
            return sbFadeAnimateHeight;
        }
    }
}
