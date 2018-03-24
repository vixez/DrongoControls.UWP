using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;

namespace DrongoControls.UWP
{
    public class CustomAnimations
    {
        private EventHandler<object> fadeIn;
        private EventHandler<object> fadeOut;
        private EventHandler<object> animateHeight;


        public void SetupEventHandlers(EventHandler<object> fadeIn, EventHandler<object> fadeOut, EventHandler<object> animateHeight)
        {
            this.fadeIn = fadeIn;
            this.fadeOut = fadeOut;
            this.animateHeight = animateHeight;
        }

        public Storyboard FadeIn(DependencyObject control)
        {
            Storyboard sbFadeIn = new Storyboard();
            sbFadeIn.Completed += fadeIn;

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
            Storyboard sbFadeOut = new Storyboard();
            sbFadeOut.Completed += fadeOut;

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
            Storyboard sbFadeAnimateHeight = new Storyboard();
            sbFadeAnimateHeight.Completed += animateHeight;

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
                KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(400)),
                Value = newHeight,
                EasingFunction = new CircleEase { EasingMode = EasingMode.EaseInOut }
            };

            da.KeyFrames.Add(oldHeightKeyFrame);
            da.KeyFrames.Add(heightKeyFrame);

            Storyboard.SetTarget(da, control);

            sbFadeAnimateHeight.Children.Add(da);
            return sbFadeAnimateHeight;
        }

        public Storyboard IncreaseScale(UIElement control, bool fadeIn = false)
        {
            int time = 1200;
            Storyboard storyboard = new Storyboard();

            ScaleTransform scale = new ScaleTransform();
            control.RenderTransformOrigin = new Point(0.5, 0.5);
            control.RenderTransform = scale;

            DoubleAnimation growAnimation = new DoubleAnimation();
            growAnimation.Duration = TimeSpan.FromMilliseconds(time);
            growAnimation.From = 1;
            growAnimation.To = 1.3;
            growAnimation.EasingFunction = new CircleEase {EasingMode = EasingMode.EaseOut };
            storyboard.Children.Add(growAnimation);

            DoubleAnimation growAnimationY = new DoubleAnimation();
            growAnimationY.Duration = TimeSpan.FromMilliseconds(time);
            growAnimationY.From = 1;
            growAnimationY.To = 1.3;
            growAnimationY.EasingFunction = new CircleEase { EasingMode = EasingMode.EaseOut };
            storyboard.Children.Add(growAnimationY);

            if (fadeIn)
            {
                DoubleAnimation da = new DoubleAnimation()
                {
                    Duration = TimeSpan.FromMilliseconds(Convert.ToInt32(time / 2)),
                    From = 0,
                    To = 1,
                    EasingFunction = new CircleEase { EasingMode = EasingMode.EaseInOut }
                };

                da.SetValue(Storyboard.TargetPropertyProperty, "UIElement.Opacity");
                Storyboard.SetTarget(da, control);

                storyboard.Children.Add(da);
            }

            Storyboard.SetTargetProperty(growAnimation, "(UIElement.RenderTransform).(ScaleTransform.ScaleX)");
            Storyboard.SetTargetProperty(growAnimationY, "(UIElement.RenderTransform).(ScaleTransform.ScaleY)");

            Storyboard.SetTarget(growAnimation, control);
            Storyboard.SetTarget(growAnimationY, control);

            return storyboard;
        }

        public Storyboard FadeInAndAnimateHeight(UIElement control, bool setOpacityToZero, double oldHeight, double newHeight)
        {
            Storyboard sb = new Storyboard();
            // Opacity
            DoubleAnimation daOpacity = new DoubleAnimation()
            {
                Duration = TimeSpan.FromMilliseconds(150),
                To = 1,
                EasingFunction = new CircleEase { EasingMode = EasingMode.EaseInOut }
            };

            if (setOpacityToZero)
            {
                daOpacity.From = 0;
            }

            daOpacity.SetValue(Storyboard.TargetPropertyProperty, "UIElement.Opacity");
            Storyboard.SetTarget(daOpacity, control);

            sb.Children.Add(daOpacity);


            // Height
            DoubleAnimationUsingKeyFrames da = new DoubleAnimationUsingKeyFrames()
            {
                EnableDependentAnimation = true
            };
            da.SetValue(Storyboard.TargetPropertyProperty, "FrameworkElement.Height");

            EasingDoubleKeyFrame oldHeightKeyFrame = new EasingDoubleKeyFrame()
            {
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
