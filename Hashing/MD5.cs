namespace CommonNet.Hashing;

/// <summary>
/// md5 hashing
/// </summary>
public static class MD5
{

    /// <summary>
    /// sets hashing key and iv
    /// </summary>
    /// <param name="key">hashing key - 32 characters</param>
    /// <param name="iv">iv - 16 characters</param>
    public static void SetKeyandIV(string key, string iv)
    {
        Keys.MD5KEY = Encoding.ASCII.GetBytes(key);
        Keys.MD5IV = Encoding.ASCII.GetBytes(iv);
    }

    /// <summary>
    /// encrypt
    /// </summary>
    /// <param name="plainText">plain text</param>
    /// <returns>string</returns>
    public static string Encrypt(string plainText)
    {
        byte[] array;
        using Aes aes = Aes.Create();
        aes.Key = Keys.MD5KEY;
        aes.IV = Keys.MD5IV;
        ICryptoTransform cryptoTransform = aes.CreateEncryptor(aes.Key, aes.IV);
        using MemoryStream ms = new MemoryStream();
        using CryptoStream cryptoStream = new((Stream)ms, cryptoTransform, CryptoStreamMode.Write);
        using (StreamWriter sw = new((Stream)cryptoStream))
        {
            sw.Write(plainText);
        }
        array = ms.ToArray();
        cryptoStream.Flush();

        return Convert.ToBase64String(array);
    }

    /// <summary>
    /// decrypt
    /// </summary>
    /// <param name="cipherText">cipher text</param>
    /// <returns>string</returns>
    public static string Decrypt(string cipherText)
    {
        byte[] buffer = Convert.FromBase64String(cipherText);
        using Aes aes = Aes.Create();
        aes.Key = Keys.MD5KEY;
        aes.IV = Keys.MD5IV;
        aes.Padding = PaddingMode.PKCS7;
        ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
        using MemoryStream ms = new MemoryStream(buffer);
        using CryptoStream cryptoStream = new CryptoStream((Stream)ms, decryptor, CryptoStreamMode.Read);
        using StreamReader sr = new StreamReader(cryptoStream);
        cryptoStream.Flush();
        return sr.ReadToEnd();
    }
}
