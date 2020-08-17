using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Scaffolding;
using Microsoft.EntityFrameworkCore.Scaffolding.Internal;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace XPY.WebsiteSolution.Database
{
    public class CustomDbContextGenerator : CSharpDbContextGenerator
    {
        public CustomDbContextGenerator(
            IProviderConfigurationCodeGenerator providerConfigurationCodeGenerator,
            IAnnotationCodeGenerator annotationCodeGenerator,
            ICSharpHelper cSharpHelper)  : base(providerConfigurationCodeGenerator, annotationCodeGenerator, cSharpHelper)
        { }

        public override string WriteCode(IModel model, string contextName, string connectionString, string contextNamespace, string modelNamespace, bool useDataAnnotations, bool suppressConnectionStringWarning)
        {
            var result = base.WriteCode(model, contextName, connectionString, contextNamespace, modelNamespace, useDataAnnotations, suppressConnectionStringWarning);

            Regex dbSet = new Regex("DbSet<(?<TypeName>.+)>");

            foreach (var match in dbSet.Matches(result).ToList())
            {
                var typename = match.Groups["TypeName"].Value;
                result = result.Replace($"<{typename}>", $"<{typename}Dao>");                
            }
            
            return result;
        }
    }
}
