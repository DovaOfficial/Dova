namespace Dova.JDK;

public class JniSignatureAttribute : Attribute
{
    public string Signature { get; }

    public JniSignatureAttribute(string signature)
    {
        Signature = signature;
    }
}