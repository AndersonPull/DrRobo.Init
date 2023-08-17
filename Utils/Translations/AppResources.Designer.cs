using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace Drrobo.Utils.Translations
{
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class AppResources
    {
        private static global::System.Resources.ResourceManager resourceMan;
        private static global::System.Globalization.CultureInfo resourceCulture;
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal AppResources() { }

        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager
        {
            get
            {
                if (object.ReferenceEquals(resourceMan, null))
                {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Drrobo.Utils.Translations.AppResources", typeof(AppResources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }

        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture
        {
            get
            {
                return resourceCulture;
            }
            set
            {
                resourceCulture = value;
            }
        }

        #region Dashboard
        internal static string DevicesConnectedToTheNetwork
        { get { return ResourceManager.GetString("DevicesConnectedToTheNetwork", resourceCulture);}}

        internal static string BannerRoboPart1
        { get { return ResourceManager.GetString("BannerRoboPart1", resourceCulture);}}

        internal static string BannerRoboPart2
        { get { return ResourceManager.GetString("BannerRoboPart2", resourceCulture);}}
        #endregion Dasboard

        #region Profile
        internal static string Profile
        { get { return ResourceManager.GetString("Profile", resourceCulture);}}

        internal static string Language
        { get { return ResourceManager.GetString("Language", resourceCulture);}}

        internal static string General
        { get { return ResourceManager.GetString("General", resourceCulture);}}
        #endregion Profile

        #region ConfigureDevice
        internal static string ConfigDevices
        { get { return ResourceManager.GetString("ConfigDevices", resourceCulture); } }

        internal static string DeviceName
        { get { return ResourceManager.GetString("DeviceName", resourceCulture); } }

        internal static string SelectTheTypeOfCommunication
        { get { return ResourceManager.GetString("SelectTheTypeOfCommunication", resourceCulture); } }

        internal static string EnterDeviceURL
        { get { return ResourceManager.GetString("EnterDeviceURL", resourceCulture); } }

        internal static string DoesThisDeviceHaveACamera
        { get { return ResourceManager.GetString("DoesThisDeviceHaveACamera", resourceCulture); } }

        internal static string EnterCameraURL
        { get { return ResourceManager.GetString("EnterCameraURL", resourceCulture); } }
        #endregion ConfigureDevice

        #region Terminal
        internal static string CommandNotKnown
        { get { return ResourceManager.GetString("CommandNotKnown", resourceCulture); } }

        internal static string NoDeviceFound
        { get { return ResourceManager.GetString("NoDeviceFound", resourceCulture); } }

        internal static string NotHaveCamera
        { get { return ResourceManager.GetString("NotHaveCamera", resourceCulture); } }

        internal static string DisconnectedAlert
        { get { return ResourceManager.GetString("DisconnectedAlert", resourceCulture); } }

        internal static string AlreadyExists
        { get { return ResourceManager.GetString("AlreadyExists", resourceCulture); } }

        internal static string DeviceTypes
        { get { return ResourceManager.GetString("DeviceTypes", resourceCulture); } }

        internal static string WhatTypeOfYourDevice
        { get { return ResourceManager.GetString("WhatTypeOfYourDevice", resourceCulture); } }

        internal static string CommunicationType
        { get { return ResourceManager.GetString("CommunicationType", resourceCulture); } }

        internal static string UnrecognizedType
        { get { return ResourceManager.GetString("UnrecognizedType", resourceCulture); } }

        internal static string HaveCam
        { get { return ResourceManager.GetString("HaveCam", resourceCulture); } }

        internal static string NotValidURL
        { get { return ResourceManager.GetString("NotValidURL", resourceCulture); } }

        internal static string SuccessfullyCreated
        { get { return ResourceManager.GetString("SuccessfullyCreated", resourceCulture); } }

        internal static string SuccessfullyRemoved
        { get { return ResourceManager.GetString("SuccessfullyRemoved", resourceCulture); } }

        internal static string ErrorCreate
        { get { return ResourceManager.GetString("ErrorCreate", resourceCulture); } }

        #endregion Termial

        #region Shared
        internal static string Cancel
        { get { return ResourceManager.GetString("Cancel", resourceCulture); } }

        internal static string Add
        { get { return ResourceManager.GetString("Add", resourceCulture); } }

        internal static string EmptyList
        { get { return ResourceManager.GetString("EmptyList", resourceCulture); } }

        internal static string Update
        { get { return ResourceManager.GetString("Update", resourceCulture); } }

        internal static string Bluetooth
        { get { return ResourceManager.GetString("Bluetooth", resourceCulture); } }

        internal static string Wifi
        { get { return ResourceManager.GetString("Wifi", resourceCulture); } }

        internal static string Yes
        { get { return ResourceManager.GetString("yes", resourceCulture); } }

        internal static string No
        { get { return ResourceManager.GetString("No", resourceCulture); } }

        internal static string NotFound
        { get { return ResourceManager.GetString("NotFound", resourceCulture); } }
        #endregion Shared
    }
}

