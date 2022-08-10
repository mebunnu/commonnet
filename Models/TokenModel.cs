namespace CommonNet.Models;

public class TokenModel
{
    public string Issuer { get; set; }

    public string Audience { get; set; }

    public Claim[] Claims { get; set; }

    public byte Expires { get; set;}

    public string SecurityKey { get; set; }
}
