﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UnitTests {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("UnitTests.Resources", typeof(Resources).Assembly);
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
        ///   Looks up a localized string similar to &lt;?xml version=&quot;1.0&quot;?&gt;
        ///&lt;ArrayOfItem xmlns=&quot;http://schemas.datacontract.org/2004/07/FreezerOrganizer.Model&quot; xmlns:i=&quot;http://www.w3.org/2001/XMLSchema-instance&quot;&gt;
        ///  &lt;Item&gt;
        ///    &lt;DateOfFreezing&gt;2014-09-07T00:00:00+02:00&lt;/DateOfFreezing&gt;
        ///    &lt;Name&gt;appelsinmarmelade&lt;/Name&gt;
        ///    &lt;Number&gt;2&lt;/Number&gt;
        ///  &lt;/Item&gt;
        ///  &lt;Item&gt;
        ///    &lt;DateOfFreezing&gt;2014-09-08T00:00:00+02:00&lt;/DateOfFreezing&gt;
        ///    &lt;Name&gt;banan&lt;/Name&gt;
        ///    &lt;Number&gt;9&lt;/Number&gt;
        ///  &lt;/Item&gt;
        ///  &lt;Item&gt;
        ///    &lt;DateOfFreezing&gt;2014-09-07T00:00:00+02:00&lt;/DateOfFreezing&gt;        /// [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string TestData {
            get {
                return ResourceManager.GetString("TestData", resourceCulture);
            }
        }
    }
}
