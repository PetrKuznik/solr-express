﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SolrExpress.Core {
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
    internal class Resource_pt_BR {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resource_pt_BR() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("SolrExpress.Core.Resource-pt-BR", typeof(Resource_pt_BR).Assembly);
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
        ///   Looks up a localized string similar to Parâmetro \&quot;{0}\&quot; não pode ser adicionado por já existir uma outra instância com o mesmo tipo.
        /// </summary>
        internal static string AllowMultipleInstanceOfParameterTypeException {
            get {
                return ResourceManager.GetString("AllowMultipleInstanceOfParameterTypeException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Um campo deve ser \&quot;indexed=true\&quot; para ser usado em facet.
        /// </summary>
        internal static string FieldMustBeIndexedTrueToBeUsedInAFacet {
            get {
                return ResourceManager.GetString("FieldMustBeIndexedTrueToBeUsedInAFacet", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Um campo deve ser \&quot;indexed=true\&quot; para ser usado em query.
        /// </summary>
        internal static string FieldMustBeIndexedTrueToBeUsedInAQuery {
            get {
                return ResourceManager.GetString("FieldMustBeIndexedTrueToBeUsedInAQuery", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Um campo deve ser numérico ou de data para ser usado em facet range.
        /// </summary>
        internal static string FieldMustBeNumericOrDateTimeToBeUsedInFacetRange {
            get {
                return ResourceManager.GetString("FieldMustBeNumericOrDateTimeToBeUsedInFacetRange", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Um campo deve ser \&quot;stored=true\&quot; para ser usado em fields.
        /// </summary>
        internal static string FieldMustBeStoredTrueToBeUsedInFields {
            get {
                return ResourceManager.GetString("FieldMustBeStoredTrueToBeUsedInFields", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Parâmetro \&quot;{0}\&quot; lançou a exceção:\r\n\&quot;{1}\&quot;.
        /// </summary>
        internal static string InvalidParameterTypeException {
            get {
                return ResourceManager.GetString("InvalidParameterTypeException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to O parâmetro \&quot;{0}\&quot; não foi encontrado no json de origem.
        /// </summary>
        internal static string UnexpectedJsonException {
            get {
                return ResourceManager.GetString("UnexpectedJsonException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Não foi possível efetuar a conversão por causa que o builder \&quot;{0}\&quot; não implementa IConvertJsonObject ou IConvertJsonPlainText.
        /// </summary>
        internal static string UnknownResolveResultBuilderException {
            get {
                return ResourceManager.GetString("UnknownResolveResultBuilderException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Ordenação do tipo descendente é um aspecto não suportado no Solr 4.
        /// </summary>
        internal static string UnsupportedSortTypeException {
            get {
                return ResourceManager.GetString("UnsupportedSortTypeException", resourceCulture);
            }
        }
    }
}
