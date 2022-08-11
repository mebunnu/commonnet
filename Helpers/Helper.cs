namespace CommonNet.Helpers;

public static class Helper
{
    public static string ToBase64(this string str)
    {
        return Convert.ToBase64String(Encoding.UTF8.GetBytes(str));
    }

    public static string FromBase64(this string str)
    {
        return Encoding.UTF8.GetString(Convert.FromBase64String(str));
    }

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

    public static byte[] ReadFileBytes(string path)
    {
        return File.ReadAllBytes(path);
    }

    public static void CopyfromSourcetoDestination(string source, string destination)
    {
        File.Copy(source, destination);
    }

    public static bool CheckIfFileExists(string path)
    {
        return File.Exists(path);
    }

    public static void DeleteFile(string path)
    {
        File.Delete(path);
    }

    //converts non seekable streams to seekable streams
    public static Stream CopyStreamtoNewStream(Stream stream)
    {
        return BinaryData.FromStream(stream).ToStream();
    }

    public static byte[] ConvertStreamtoBytes(Stream stream)
    {
        return BinaryData.FromStream(stream).ToArray();
    }

    public static Stream ConvertBytestoStream(byte[] bytes)
    {
        return BinaryData.FromBytes(bytes).ToStream();
    }

    public static string ConvertStreamtoString(Stream stream)
    {
        return BinaryData.FromStream(stream).ToString();
    }

    public static Stream ConvertStringtoStream(string data)
    {
        return BinaryData.FromString(data).ToStream();
    }

    public static CancellationTokenSource CreateCancellationToken(double minutes, List<CancellationToken> cancellationTokens)
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