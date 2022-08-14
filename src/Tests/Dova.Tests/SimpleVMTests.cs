using Dova.Core;
using Dova.Core.Runtime;
using Xunit;

namespace Dova.Tests;

public class SimpleVMTests
{
    [Fact]
    public void Create_VM_and_check_version()
    {
        var config = new DovaConfiguration
        {
        };
        
        DovaVM.Initialize(config);

        var version = DovaVM.Runtime.GetVersion();

        Assert.Equal(version, config.Version);
    }

    [Fact]
    public unsafe void Create_VM_and_try_print_Hello_World()
    {
        var config = new DovaConfiguration
        {
        };

        DovaVM.Initialize(config);

        var systemClass = DovaVM.Runtime.FindClass("java/lang/System");
        var staticFieldOut = DovaVM.Runtime.GetStaticFieldId(systemClass, "out", "Ljava/io/PrintStream;");

        var printStreamClass = DovaVM.Runtime.FindClass("java/io/PrintStream");
        var methodPrintln = DovaVM.Runtime.GetMethodId(printStreamClass, "println", "(Ljava/lang/String;)V");

        var staticObjectField = DovaVM.Runtime.GetStaticObjectField(systemClass, staticFieldOut);

        var newString = DovaVM.Runtime.GetString("Hello World from JVM");

        DovaVM.Runtime.CallVoidMethodA(staticObjectField, methodPrintln, newString);
        
        DovaGuard.CheckForException();
    }
}