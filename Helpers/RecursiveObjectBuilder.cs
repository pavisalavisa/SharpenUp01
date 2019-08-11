using System;
using System.Linq;
using FizzWare.NBuilder;

namespace DynamicKeywordShowcase.Helpers
{
    public class RecursiveObjectBuilder
    {
        private readonly int _graphDepth;
        private readonly int _listSize;
        private readonly Builder _builder;

        public RecursiveObjectBuilder(int graphDepth, int listSize)
        {
            _graphDepth = graphDepth;
            _listSize = listSize;
            _builder = new Builder();
        }

        public T CreateGenericObject<T>(bool recursive = false)
        {
            return recursive ? (T)BuildGraphRecursively(typeof(T)) : Builder<T>.CreateNew().Build();
        }

        private object BuildGraphRecursively(Type type, int step = 0)
        {
            var objectType = type.IsEnumerable() ? type.GenericTypeArguments[0] : type;
            var obj = BuildGraph(objectType);

            foreach (var prop in objectType.GetProperties().Where(x => x.CanWrite && x.CanRead))
            {
                if (prop.PropertyType.IsSimpleType() || prop.PropertyType.IsValueType) continue;

                var propertyValue = step >= _graphDepth
                    ? BuildGraph(prop.PropertyType)
                    : BuildGraphRecursively(prop.PropertyType, step + 1);

                prop.SetValue(obj, propertyValue);
            }

            return type.IsEnumerable()
                ? Enumerable.Range(0, _listSize).Select(x => obj).ListOf(objectType)
                : obj;
        }

        private object BuildGraph(Type type)
        {
            dynamic objectBuilder;

            if (type.IsEnumerable())
            {
                objectBuilder = typeof(Builder)
                    .GetMethod("CreateListOfSize", new[] { typeof(int) })
                    ?.MakeGenericMethod(type.GenericTypeArguments[0])
                    .Invoke(_builder, new object[] { _listSize });
            }
            else
            {
                objectBuilder = typeof(Builder)
                    .GetMethod("CreateNew")
                    ?.MakeGenericMethod(type)
                    .Invoke(_builder, new object[] { });
            }

            return objectBuilder?.Build();
        }
    }
}
