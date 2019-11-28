using System;
using System.Linq;
using GraphQL.Language.AST;
using GraphQL.Types;

namespace TuDou.Grace.Core.Extensions
{
    public static class ContextExtensions
    {
        public static ResolveFieldContext<object> ContainsArgument<TArgType>(this ResolveFieldContext<object> context,
            string argumentName,
            Action<TArgType> argumentContainsAction)
        {
            if (context.Arguments.ContainsKey(argumentName))
            {
                argumentContainsAction(context.GetArgument<TArgType>(argumentName));
            }

            return context;
        }

        /// <summary>
        /// 如果给定的fieldSelector存在于查询的选择中，则返回true。
        /// </summary>
        /// <param name="context">工作环境</param>
        /// <param name="fieldSelector">字段选择器的查询。例如项目:organizationUnits: displayName</param>
        /// <param name="namespaceSeperator">fieldSelector的分隔符。默认是:</param>
        /// <returns></returns>
        public static bool HasSelectionField(this ResolveFieldContext<object> context, string fieldSelector, char namespaceSeperator = ':')
        {
            if (string.IsNullOrWhiteSpace(fieldSelector))
            {
                return false;
            }

            if (context.SubFields == null)
            {
                return false;
            }

            var fragments = fieldSelector.Split(new[] { namespaceSeperator }, StringSplitOptions.RemoveEmptyEntries);

            if (fragments.Length == 1)
            {
                return context.SubFields.ContainsKey(fragments[0]);
            }

            if (context.SubFields[fragments[0]] == null)
            {
                return false;
            }

            if (context.SubFields[fragments[0]].SelectionSet == null)
            {
                return false;
            }

            if (context.SubFields[fragments[0]].SelectionSet.Selections == null)
            {
                return false;
            }


            var selections = context.SubFields[fragments[0]].SelectionSet.Selections;

            for (var i = 1; i < fragments.Length; i++)
            {
                if (selections == null)
                {
                    return false;
                }

                var field = selections.Select(selection => (Field)selection).FirstOrDefault(f => f.Name == fragments[i]);
                if (field == null)
                {
                    return false;
                }

                if (i == fragments.Length - 1)
                {
                    return true;
                }

                selections = field.SelectionSet?.Selections;
            }

            return true;
        }
    }
}