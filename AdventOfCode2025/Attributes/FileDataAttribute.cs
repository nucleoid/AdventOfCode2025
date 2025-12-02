using System.Reflection;
using Xunit.Sdk;

namespace AdventOfCode2025.Attributes
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    public class FileDataAttribute(string filePath) : DataAttribute
    {
        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            using var csvFile = new StreamReader(filePath);
            while (csvFile.ReadLine() is { } line)
            {
                yield return [line];
            }
        }
    }
}
