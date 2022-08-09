using Moq;
using Xunit;

namespace Dova.Tests.ExternalDependencies;

public class MoqTests
{
    public class TestClass
    {
        public virtual int X { get; }

        public int MaxX()
        {
            return X + 2;
        }
    }
    
    public interface ITestInterface
    {
        int GetValue()
        {
            return 7;
        }
    }
    
    [Fact]
    public void Should_create_an_interface_type_with_method_declared_Moq()
    {
        var mock = new Mock<ITestInterface>(MockBehavior.Loose) {CallBase = true};
        var obj = mock.Object;
        var value = obj.GetValue();
        
        Assert.True(value == 7);
    }

    [Fact]
    public void Should_create_a_class_type_with_method_declared_Moq()
    {
        var mock = new Mock<TestClass>(MockBehavior.Loose) {CallBase = true};
        var ret = 9;

        mock.Setup(x => x.X).Returns(ret);

        var obj = mock.Object;
        var value = obj.MaxX();
        
        Assert.True(ret + 2 == value);
    }

    // [Fact]
    // public void Should_create_an_interface_type_with_method_declared_FakeItEasy()
    // {
    //     var mock = new Fake<ITestInterface>();
    //     var obj = mock.FakedObject;
    //     var value = obj.GetValue();
    //     
    //     Assert.True(value == 7);
    // }
}