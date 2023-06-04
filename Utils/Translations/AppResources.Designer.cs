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

        internal static string ConfigServerProfile
        { get { return ResourceManager.GetString("ConfigServerProfile", resourceCulture);}}
        #endregion Profile

        #region ConfigServer
        internal static string EnterServerName
        { get { return ResourceManager.GetString("EnterServerName", resourceCulture); } }

        internal static string EnterServerURL
        { get { return ResourceManager.GetString("EnterServerURL", resourceCulture); } }
        #endregion ConfigServer

        #region ConfigJoystick
        internal static string ConnectedServer
        { get { return ResourceManager.GetString("ConnectedServer", resourceCulture); } }
        #endregion ConfigJoystick

        #region Shared
        internal static string Cancel
        { get { return ResourceManager.GetString("Cancel", resourceCulture);}}

        internal static string Add
        { get { return ResourceManager.GetString("Add", resourceCulture);}}

        internal static string EmptyList
        { get { return ResourceManager.GetString("EmptyList", resourceCulture); } }
        #endregion Shared
    }
}

