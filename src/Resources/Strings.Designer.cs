﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Laserfiche.Oauth.Api.Client.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Strings {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Strings() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Laserfiche.Oauth.Api.Client.Resources.Strings", typeof(Strings).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The access key is invalid or null..
        /// </summary>
        public static string INVALID_ACCESS_KEY {
            get {
                return ResourceManager.GetString("INVALID_ACCESS_KEY", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The client ID is either not set or incorrect..
        /// </summary>
        public static string INVALID_CLIENT_ID {
            get {
                return ResourceManager.GetString("INVALID_CLIENT_ID", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The customer ID is either not set or incorrect..
        /// </summary>
        public static string INVALID_CUSTOMER_ID {
            get {
                return ResourceManager.GetString("INVALID_CUSTOMER_ID", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The domain is either not set or incorrect..
        /// </summary>
        public static string INVALID_DOMAIN {
            get {
                return ResourceManager.GetString("INVALID_DOMAIN", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The service principal key is either not set or incorrect..
        /// </summary>
        public static string INVALID_SERVICE_PRINCIPAL_KEY {
            get {
                return ResourceManager.GetString("INVALID_SERVICE_PRINCIPAL_KEY", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Unable to read the oauth response..
        /// </summary>
        public static string UNABLE_TO_READ_RESPONSE {
            get {
                return ResourceManager.GetString("UNABLE_TO_READ_RESPONSE", resourceCulture);
            }
        }
    }
}
