﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BLL.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class PubResources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal PubResources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("BLL.Properties.PubResources", typeof(PubResources).Assembly);
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
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &quot;The Mended Drum&quot; pub.
        /// </summary>
        internal static string Pub {
            get {
                return ResourceManager.GetString("Pub", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to That&apos;s strange. You lost a chance to survive. *These players, sometimes, are so illogical..
        /// </summary>
        internal static string PubLoseGame {
            get {
                return ResourceManager.GetString("PubLoseGame", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Unfortunately, you didn&apos;t have enough money to buy a pint of beer..
        /// </summary>
        internal static string PubPlayerLackOfMoneyPostscript {
            get {
                return ResourceManager.GetString("PubPlayerLackOfMoneyPostscript", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Welcome to the &quot;The Mended Drum&quot; pub. Our price : 1 pint of beer = 2$ AM..
        /// </summary>
        internal static string PubWelcomeMessage {
            get {
                return ResourceManager.GetString("PubWelcomeMessage", resourceCulture);
            }
        }
    }
}