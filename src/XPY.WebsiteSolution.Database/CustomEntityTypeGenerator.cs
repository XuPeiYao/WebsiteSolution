using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Scaffolding.Internal;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace XPY.WebsiteSolution.Database
{
    public class CustomEntityTypeGenerator : CSharpEntityTypeGenerator
    {
        public CustomEntityTypeGenerator(ICSharpHelper csharpHelper) : base(csharpHelper)
        { }

        public override string WriteCode(IEntityType entityType, string @namespace, bool useDataAnnotations)
        {
            var classStr = base.WriteCode(entityType, @namespace, useDataAnnotations);

            var defaultStr = "public partial class " + entityType.Name;
            var baseStr = "public partial class " + entityType.Name + "Dao";

            classStr = classStr.Replace(defaultStr, baseStr);

            return classStr;
        }
    }
}
