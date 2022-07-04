using Dova.Core;
using Xunit;

namespace Dova.Tests;

public class SimpleVMTests
{
    [Fact]
    public void Create_VM_without_job()
    {
        using (var vm = new DovaVM())
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
}