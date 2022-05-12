using System.Reflection;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using FastMember;
using ImmediateReflection;
using TypeAccessor = FastMember.TypeAccessor;

namespace ReflectionBenchmark;

internal class Program
{
    private static void Main()
    {
        var summary = BenchmarkRunner.Run<TypeBenchmark>();
    }
}

public class TestType
{
    public int Id { get; set; }

    public string Name { get; set; }
}

public class TypeBenchmark
{
    private readonly Member _fastPropertyAccessor =
        TypeAccessor.Create(typeof(TestType)).GetMembers().Single(m => m.Name == "Id");

    private readonly TypeAccessor _fastTypeAccessor = TypeAccessor.Create(typeof(TestType));

    private readonly ImmediateProperty _propertyAccessor =
        ImmediateReflection.TypeAccessor.Get<TestType>().GetProperty("Id")!;

    private readonly PropertyInfo _propertyInfo = typeof(TestType).GetProperty("Id");
    private readonly TestType _testInstance = new() {Id = 100, Name = "Test"};
    private readonly Type _type = typeof(TestType);
    private readonly ImmediateType _typeAccessor = ImmediateReflection.TypeAccessor.Get<TestType>();

    [Benchmark]
    public PropertyInfo[] GetReflectionProperties()
    {
        return _type.GetProperties();
    }

    [Benchmark]
    public ImmediateProperty[] GetImmediateReflectionProperties()
    {
        return _typeAccessor.GetProperties().ToArray();
    }

    [Benchmark]
    public Member[] GetFastMemberProperties()
    {
        return _fastTypeAccessor.GetMembers().ToArray();
    }

    [Benchmark]
    public object ReadReflectionIntProperty()
    {
        return _propertyInfo.GetValue(_testInstance, null);
    }

    [Benchmark]
    public object ReadImmediateReflectionIntProperty()
    {
        return _propertyAccessor.GetValue(_testInstance);
    }

    [Benchmark]
    public object ReadFastIntProperty()
    {
        return _fastTypeAccessor[_testInstance, "Id"];
    }

    [Benchmark]
    public void WriteReflectionIntProperty()
    {
        _propertyInfo.SetValue(_testInstance, 200);
    }

    [Benchmark]
    public void WriteImmediateReflectionIntProperty()
    {
        _propertyAccessor.SetValue(_testInstance, 200);
    }

    [Benchmark]
    public void WriteFastIntProperty()
    {
        _fastTypeAccessor[_testInstance, "Id"] = 200;
    }
}