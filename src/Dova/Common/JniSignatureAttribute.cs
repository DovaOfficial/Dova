namespace Dova.Common;

public class JniSignatureAttribute : Attribute
{
    public string Signature { get; }
    public string Modifiers { get; }

    public JniSignatureAttribute(string signature, string modifiers)
    {
        Signature = signature;
        Modifiers = modifiers;
    }
}