namespace Dova.Common.InterfaceFactory;

/// <summary>
/// In general, used as a wrapper for creating new interface-type objects.
/// By default Dova don't want to decide how the interface objects should be created / provided.
/// It is up to developers to decide.
/// For basic (fully-working) implementation, please see:
/// https://github.com/DovaOfficial/Dova.InterfaceProviders.Moq
/// </summary>
/// <author>https://github.com/DovaOfficial</author>
public interface IInterfaceObjectProvider
{
    /// <typeparam name="TInterface">Interface of which new instance should be provided.</typeparam>
    /// <returns>New instance of a specified interface.</returns>
    TInterface Get<TInterface>(IntPtr currentRefPtr) where TInterface : class, IJavaObject;
}