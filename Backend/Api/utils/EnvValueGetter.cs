namespace Api.utils;

public static class EnvValueGetter
{
    public static T1 Get<T1>(string envKey)
    {
        string value = Environment.GetEnvironmentVariable(envKey)!;

        if (string.IsNullOrEmpty(value))
            throw new Exception($"Environment variable '{envKey}' is not set.");

        return (T1)Convert.ChangeType(value, typeof(T1));
    }
}
