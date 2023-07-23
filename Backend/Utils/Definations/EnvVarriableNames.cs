namespace Utils.Definations;

public enum EnvVarriableNames
{
    HelloWorld,
    Development,
    DataBaseConnection,
    TokenIssuer,
    TokenAudience,
    TokenExpiryInMinutes,
    TokenSecretNormal,
    UserNormal,
    UserAdmin
}

public static class EnvVarriableNamesMapping
{
    private static readonly Dictionary<EnvVarriableNames, string> Mapping = new Dictionary<EnvVarriableNames, string>
    {
        { EnvVarriableNames.HelloWorld, "HELLO_WORLD" },
        { EnvVarriableNames.Development, "ENV_DEVELOPMENT" },
        { EnvVarriableNames.DataBaseConnection, "DATABASE_CONNECTION" },
        { EnvVarriableNames.TokenIssuer, "TOKEN_ISSUER" },
        { EnvVarriableNames.TokenAudience, "TOKEN_AUDIENCE" },
        { EnvVarriableNames.TokenExpiryInMinutes, "TOKEN_EXPIRY_IN_MINUTES" },
        { EnvVarriableNames.TokenSecretNormal, "TOKEN_SECRET_NORMAL" },
        { EnvVarriableNames.UserNormal, "NORMAL" },
        { EnvVarriableNames.UserAdmin, "ADMIN" }
    };

    public static string GetStringValue(EnvVarriableNames varriableName)
    {
        if (Mapping.TryGetValue(varriableName, out string? value))
            return value;

        throw new ArgumentException($"No string value mapping found for enum member '{varriableName}'.");
    }
}
