namespace CommonNet.Helpers;

public static class Helper
{
    public static string Base64Encode(string plainText)
    {
        return Convert.ToBase64String(Encoding.UTF8.GetBytes(plainText));
    }

    public static string Base64Decode(string base64encode)
    {
        return Encoding.UTF8.GetString(Convert.FromBase64String(base64encode));
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

    public static int GenerateOtp(int numberofcharacters = 4)
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
        char[] PASSWORD_CHARS_SPECIAL = "*$-+?_&=!%{}/".ToCharArray();

        int passminLength = 16;
        int passmaxLength = 24;

        Random random = new();

        char[] password = new char[random.Next(passminLength, passmaxLength)];

        for (int i = 0; i < password.Length; i++)
        {
            switch (random.Next(0, 12))
            {
                case 0:
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
}