namespace Dova.Common.InterfaceFactory;

public static class DovaInterfaceFactory
{
    /// <summary>
    /// Provider which will be used to create new instances of a specified types.
    /// </summary>
    public static IInterfaceObjectProvider Provider { get; set; } = new DefaultInterfaceObjectProvider();

    public static TInterface Get<TInterface>(IntPtr currentRefPtr) where TInterface : class, IJavaObject => 
        Provider.Get<TInterface>(currentRefPtr);
}