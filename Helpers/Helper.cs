namespace CommonNet.Helpers;

/// <summary>
/// helper class
/// </summary>
public static class Helper
{
    /// <summary>
    /// converts string to base64
    /// </summary>
    /// <param name="str">string that is converted to base64</param>
    /// <returns>base64 encoded string</returns>
    public static string ToBase64(this string str)
    {
        return Convert.ToBase64String(Encoding.UTF8.GetBytes(str));
    }

    /// <summary>
    /// converts base64 string to string
    /// </summary>
    /// <param name="str">base64 string that is converted to string</param>
    /// <returns>base64 decoded string</returns>
    public static string FromBase64(this string str)
    {
        return Encoding.UTF8.GetString(Convert.FromBase64String(str));
    }

    /// <summary>
    /// generates token
    /// </summary>
    /// <param name="model">token model which is consumed to create the token</param>
    /// <returns>token</returns>
    public static string GenerateToken(TokenModel model)
    {
        JwtSecurityTokenHandler tokenHandler = new();
        SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = model.Issuer,
            Audience = model.Audience,
            Subject = new ClaimsIdentity(model.Claims),
            Expires = model.Expires > 0 ? DateTime.UtcNow.AddDays(model.Expires) : DateTime.UtcNow.AddDays(7),
            NotBefore = DateTime.UtcNow,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(model.SecurityKey)), SecurityAlgorithms.HmacSha256)
        };

        JwtSecurityToken token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }

    /// <summary>
    /// generates otp
    /// </summary>
    /// <param name="numberofcharacters">number of characters in otp, default is 4</param>
    /// <returns>otp</returns>
    public static int GenerateOTP(int numberofcharacters = 4)
    {
        string min = string.Empty;
        string max = string.Empty;

        for (int i = 0; i < numberofcharacters; i++)
        {
            if (i == 0)
            {
                min = "1";
            }
            else
            {
                min += "0";
            }

            max += "9";
        }


        return new Random().Next(Convert.ToInt32(min), Convert.ToInt32(max));
    }

    /// <summary>
    /// generates random password
    /// </summary>
    /// <returns>random password</returns>
    public static string GeneratePassword()
    {
        char[] PASSWORD_CHARS_LCASE = "abcdefgijkmnopqrstwxyz".ToCharArray();
        char[] PASSWORD_CHARS_UCASE = "ABCDEFGHJKLMNPQRSTWXYZ".ToCharArray();
        char[] PASSWORD_CHARS_NUMERIC = "23456789".ToCharArray();
        char[] PASSWORD_CHARS_SPECIAL = "*@$-+?_&=!%/".ToCharArray();
        Random random = new();

        char[] password = new char[32];

        for (int i = 0; i < password.Length; i++)
        {
            switch (random.Next(1, 12))
            {
                case 1:
                case 2:
                case 3:
                    password[i] = PASSWORD_CHARS_LCASE[random.Next(0, PASSWORD_CHARS_LCASE.Length - 1)];
                    break;
                case 4:
                case 5:
                case 6:
                case 7:
                    password[i] = PASSWORD_CHARS_UCASE[random.Next(0, PASSWORD_CHARS_UCASE.Length - 1)];
                    break;
                case 8:
                case 9:
                case 10:
                    password[i] = PASSWORD_CHARS_NUMERIC[random.Next(0, PASSWORD_CHARS_NUMERIC.Length - 1)];
                    break;
                case 11:
                case 12:
                    password[i] = PASSWORD_CHARS_SPECIAL[random.Next(0, PASSWORD_CHARS_SPECIAL.Length - 1)];
                    break;
            }
        }

        return new string(password);
    }

    /// <summary>
    /// converts file to bytes
    /// </summary>
    /// <param name="path">file path</param>
    /// <returns>byte array of file</returns>
    public static byte[] ReadFileBytes(string path)
    {
        return File.ReadAllBytes(path);
    }

    /// <summary>
    /// copies file from source to destination
    /// </summary>
    /// <param name="source">source file path</param>
    /// <param name="destination">destination file path</param>
    public static void CopyfromSourcetoDestination(string source, string destination)
    {
        File.Copy(source, destination);
    }

    /// <summary>
    /// checks if the file exists
    /// </summary>
    /// <param name="path">file path</param>
    /// <returns></returns>
    public static bool CheckIfFileExists(string path)
    {
        return File.Exists(path);
    }

    /// <summary>
    /// deletes file
    /// </summary>
    /// <param name="path">file path</param>
    public static void DeleteFile(string path)
    {
        File.Delete(path);
    }

    /// <summary>
    /// converts non seekable streams to seekable streams
    /// </summary>
    /// <param name="stream">stream that is to be copied to new stream</param>
    /// <returns>stream</returns>
    public static Stream CopyStreamtoNewStream(Stream stream)
    {
        return BinaryData.FromStream(stream).ToStream();
    }

    /// <summary>
    /// converts stream to bytes
    /// </summary>
    /// <param name="stream">stream</param>
    /// <returns>byte array</returns>
    public static byte[] ConvertStreamtoBytes(Stream stream)
    {
        return BinaryData.FromStream(stream).ToArray();
    }

    /// <summary>
    /// converts bytes to stream
    /// </summary>
    /// <param name="bytes">byte array</param>
    /// <returns>stream</returns>
    public static Stream ConvertBytestoStream(byte[] bytes)
    {
        return BinaryData.FromBytes(bytes).ToStream();
    }

    /// <summary>
    /// converts stream to string
    /// </summary>
    /// <param name="stream">stream</param>
    /// <returns>string</returns>
    public static string ConvertStreamtoString(Stream stream)
    {
        return BinaryData.FromStream(stream).ToString();
    }

    /// <summary>
    /// converts string to stream
    /// </summary>
    /// <param name="data">string</param>
    /// <returns>stream</returns>
    public static Stream ConvertStringtoStream(string data)
    {
        return BinaryData.FromString(data).ToStream();
    }

    /// <summary>
    /// converts file to stream
    /// </summary>
    /// <param name="file">IFormFile</param>
    /// <returns>stream</returns>
    public static Stream ConvertFileToStream(IFormFile file)
    {
        using MemoryStream ms = new MemoryStream();
        file.CopyTo(ms);

        return BinaryData.FromStream(ms).ToStream();
    }

    /// <summary>
    /// creates a cancellation token combining different tokens
    /// </summary>
    /// <param name="cancellationTokens"></param>
    /// <param name="minutes">minutes in which the task has to be cancelled, default is 2</param>
    /// <returns>cancellationtokensource, dispose it later</returns>
    public static CancellationTokenSource CreateCancellationToken(List<CancellationToken> cancellationTokens, double minutes = 2)
    {
        if(cancellationTokens == null)
        {
            cancellationTokens = new List<CancellationToken>();
        }

        CancellationTokenSource cancellationTokenSource = new();
        cancellationTokenSource.CancelAfter(TimeSpan.FromMinutes(minutes));
        cancellationTokens.Add(cancellationTokenSource.Token);
        return CancellationTokenSource.CreateLinkedTokenSource(cancellationTokens.ToArray());
    }
}