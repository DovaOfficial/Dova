using Dova.Core;
using Dova.Core.Runtime;
using Xunit;

namespace Dova.Tests;

public class SimpleVMTests
{
    [Fact]
    public void Create_VM_without_job()
    {
        using (var vm = new DovaVM(new DovaConfiguration()))
        {
        }
    }

    [Fact]
    public void Create_VM_and_check_version()
    {
        var config = new DovaConfiguration
        {
        };
        
        using (var vm = new DovaVM(config))
        {
            var version = vm.Runtime.GetVersion();
            
            Assert.Equal(version, config.Version);
        }
    }

    [Fact]
    public unsafe void Create_VM_and_try_print_Hello_World()
    {
        var config = new DovaConfiguration
        {
        };

        using (var vm = new DovaVM(config))
        {
            var systemClass = vm.Runtime.FindClass("java/lang/System");
            var staticFieldOut = vm.Runtime.GetStaticFieldId(systemClass, "out", "Ljava/io/PrintStream;");

            var printStreamClass = vm.Runtime.FindClass("java/io/PrintStream");
            var methodPrintln = vm.Runtime.GetMethodId(printStreamClass, "println", "(Ljava/lang/String;)V");   
            
            var staticObjectField = vm.Runtime.GetStaticObjectField(systemClass, staticFieldOut);

            var newString = vm.Runtime.GetString("Hello World from JVM");

            vm.Runtime.CallVoidMethodA(staticObjectField, methodPrintln, newString);
        }
    }
}