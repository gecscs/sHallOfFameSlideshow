﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HallOfFameSlideshow {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "17.11.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("10")]
        public int RandomValue {
            get {
                return ((int)(this["RandomValue"]));
            }
            set {
                this["RandomValue"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("10")]
        public int TrendingValue {
            get {
                return ((int)(this["TrendingValue"]));
            }
            set {
                this["TrendingValue"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("10")]
        public int RecentValue {
            get {
                return ((int)(this["RecentValue"]));
            }
            set {
                this["RecentValue"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("10")]
        public int ArcheologistValue {
            get {
                return ((int)(this["ArcheologistValue"]));
            }
            set {
                this["ArcheologistValue"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("10")]
        public int SupporterValue {
            get {
                return ((int)(this["SupporterValue"]));
            }
            set {
                this["SupporterValue"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("90")]
        public int ViewMaxAgeValue {
            get {
                return ((int)(this["ViewMaxAgeValue"]));
            }
            set {
                this["ViewMaxAgeValue"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("https://halloffame.cs2.mtq.io/api/v1/screenshots/weighted")]
        public string GetRandomWeightedImageApiEndpoint {
            get {
                return ((string)(this["GetRandomWeightedImageApiEndpoint"]));
            }
            set {
                this["GetRandomWeightedImageApiEndpoint"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0belix")]
        public string CreatorName {
            get {
                return ((string)(this["CreatorName"]));
            }
            set {
                this["CreatorName"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("319c6c50-9143-48fb-beba-152324af0536")]
        public string CreatorId {
            get {
                return ((string)(this["CreatorId"]));
            }
            set {
                this["CreatorId"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("paradox")]
        public string Provider {
            get {
                return ((string)(this["Provider"]));
            }
            set {
                this["Provider"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("4554")]
        public string HWID {
            get {
                return ((string)(this["HWID"]));
            }
            set {
                this["HWID"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("60")]
        public int SlideShowInterval {
            get {
                return ((int)(this["SlideShowInterval"]));
            }
            set {
                this["SlideShowInterval"] = value;
            }
        }
    }
}
