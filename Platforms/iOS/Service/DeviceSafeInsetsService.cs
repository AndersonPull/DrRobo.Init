using System;
using UIKit;

namespace Drrobo.Modules.Shared.Services.Device
{
    public partial class DeviceSafeInsetsService
    {
        public partial double GetSafeAreaBottom()
        {
            if (UIDevice.CurrentDevice.CheckSystemVersion(11, 0))
            {
                UIWindow window = UIApplication.SharedApplication.Delegate.GetWindow();
                var bottomPadding = window.SafeAreaInsets.Bottom;
                return bottomPadding;
            }
            return 0;
        }

        public partial double GetSafeAreaTop()
        {
            if (UIDevice.CurrentDevice.CheckSystemVersion(11, 0))
            {
                UIWindow window = UIApplication.SharedApplication.Delegate.GetWindow();
                var TopPadding = window.SafeAreaInsets.Top;
                return TopPadding;
            }
            return 0;
        }

        public partial double GetSafeAreaLeft()
        {
            if (UIDevice.CurrentDevice.CheckSystemVersion(11, 0))
            {
                UIWindow window = UIApplication.SharedApplication.Delegate.GetWindow();
                var leftPadding = window.SafeAreaInsets.Left;
                return leftPadding;
            }
            return 0;
        }

        public partial double GetSafeAreaRight()
        {
            if (UIDevice.CurrentDevice.CheckSystemVersion(11, 0))
            {
                UIWindow window = UIApplication.SharedApplication.Delegate.GetWindow();
                var rightPadding = window.SafeAreaInsets.Right;
                return rightPadding;
            }
            return 0;
        }
    }
}

