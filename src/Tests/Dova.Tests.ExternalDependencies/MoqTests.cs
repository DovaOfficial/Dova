using FakeItEasy;
using Moq;
using Xunit;

namespace Dova.Tests.ExternalDependencies;

public class MoqTests
{
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