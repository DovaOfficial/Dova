using System.Runtime;

namespace Dova.Common.InterfaceFactory;

public sealed class DefaultInterfaceObjectProvider : IInterfaceObjectProvider
{
    /// <summary>
    /// By default Dova doesn't support interface-type object creation (yet).
    /// In the future this might change, but for now it is a good practise to use external library for object creation.
    /// For more information (and working nuget), please see:
    /// https://github.com/DovaOfficial/Dova.InterfaceProviders.Moq
    ///
    /// After installing the nuget, please setup the implementation as so:
    /// DovaInterfaceFactory.Provider = new MoqInterfaceObjectProvider();
    ///
    /// Until Dova change how it handles interfaces, this is a correct way of assigning fully-working interface object provider.
    /// </summary>
    public TInterface Get<TInterface>(IntPtr currentRefPtr) where TInterface : class, IJavaObject => 
        throw new AmbiguousImplementationException($"Please use a valid type of {nameof(IInterfaceObjectProvider)}, see: https://github.com/DovaOfficial/Dova.InterfaceProviders.Moq");
}