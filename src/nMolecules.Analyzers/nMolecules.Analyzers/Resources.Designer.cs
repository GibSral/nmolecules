﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.Reflection;

namespace NMolecules.Analyzers {
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
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("DDDAnalyzer.Resources", typeof(Resources).GetTypeInfo().Assembly);
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
        ///   Looks up a localized string similar to Value objects must be immutable.
        /// </summary>
        internal static string ValueObjectMustBeImmutableDescription {
            get {
                return ResourceManager.GetString("ValueObjectMustBeImmutableDescription", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ValueObjects must be immutable.
        /// </summary>
        internal static string ValueObjectMustBeImmutableMessageFormat {
            get {
                return ResourceManager.GetString("ValueObjectMustBeImmutableMessageFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ValueObjects must be immutable.
        /// </summary>
        internal static string ValueObjectMustBeImmutableTitle {
            get {
                return ResourceManager.GetString("ValueObjectMustBeImmutableTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Value objects must be declared as sealed.
        /// </summary>
        internal static string ValueObjectMustBeSealedDescription {
            get {
                return ResourceManager.GetString("ValueObjectMustBeSealedDescription", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Value objects must be declared as sealed.
        /// </summary>
        internal static string ValueObjectMustBeSealedMessageFormat {
            get {
                return ResourceManager.GetString("ValueObjectMustBeSealedMessageFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Value objects must be declared as sealed.
        /// </summary>
        internal static string ValueObjectMustBeSealedTitle {
            get {
                return ResourceManager.GetString("ValueObjectMustBeSealedTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Value objects must implement IEqautable&lt;ValueObject&gt;.
        /// </summary>
        internal static string ValueObjectMustImplementIEquatableDescription {
            get {
                return ResourceManager.GetString("ValueObjectMustImplementIEquatableDescription", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Value objects must implement IEqautable&lt;{0}&gt;.
        /// </summary>
        internal static string ValueObjectMustImplementIEquatableMessageFormat {
            get {
                return ResourceManager.GetString("ValueObjectMustImplementIEquatableMessageFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Value objects must implement IEqautable&lt;ValueObject&gt;.
        /// </summary>
        internal static string ValueObjectMustImplementIEquatableTitle {
            get {
                return ResourceManager.GetString("ValueObjectMustImplementIEquatableTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Value objects should not reference entities.
        /// </summary>
        internal static string ValueObjectUsesEntityDescription {
            get {
                return ResourceManager.GetString("ValueObjectUsesEntityDescription", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ValueObjects must not use entities.
        /// </summary>
        internal static string ValueObjectUsesEntityMessageFormat {
            get {
                return ResourceManager.GetString("ValueObjectUsesEntityMessageFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ValueObject uses entities.
        /// </summary>
        internal static string ValueObjectUsesEntityTitle {
            get {
                return ResourceManager.GetString("ValueObjectUsesEntityTitle", resourceCulture);
            }
        }
    }
}
