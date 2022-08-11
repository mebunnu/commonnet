namespace CommonNet.Models;

/// <summary>
/// token model used for creating the token
/// </summary>
public class TokenModel
{
    /// <summary>
    /// issuer of the token
    /// </summary>
    public string Issuer { get; set; }

    /// <summary>
    /// audience of the token
    /// </summary>
    public string Audience { get; set; }

    /// <summary>
    /// claims of the token
    /// </summary>
    public Claim[] Claims { get; set; }

    /// <summary>
    /// expires in days. default is 7
    /// </summary>
    public byte Expires { get; set;}

    /// <summary>
    /// security key of token
    /// </summary>
    public string SecurityKey { get; set; }
}
