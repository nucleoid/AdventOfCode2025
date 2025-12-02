using System.Reflection;
using Xunit.Sdk;

namespace AdventOfCode2025.Attributes
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    public class CsvDataAttribute(string filePath) : DataAttribute
    {
        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            var pars = testMethod.GetParameters();
            var parameterTypes = pars.Select(par => par.ParameterType).ToArray();
            using var csvFile = new StreamReader(filePath);
            while (csvFile.ReadLine() is { } line)
            {
                var row = line.Split(',');
                yield return ConvertParameters(row, parameterTypes);
            }
        }

        private static object[] ConvertParameters(IReadOnlyList<object> values, IReadOnlyList<Type> parameterTypes)
        {
            var result = new object[parameterTypes.Count];
            for (var idx = 0; idx < parameterTypes.Count; idx++)
            {
                result[idx] = ConvertParameter(values[idx], parameterTypes[idx]);
            }

            return result;
        }

        private static object ConvertParameter(object parameter, Type parameterType)
        {
            return parameterType == typeof(int) ? Convert.ToInt32(parameter) : parameter;
        }
    }
}
