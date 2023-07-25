namespace Api.utils;

public enum ErrorDefinations
{
    UserExists,
    UserNotFound,
    UserInvalidPassword,
    InvalidUserId,
    ImageNotFound,
    BlogNotFound,
    InvalidBlogId,
}


public static class ErrorsValueMapping
{
    private static readonly Dictionary<ErrorDefinations, string> Mapping = new Dictionary<ErrorDefinations, string>
    {
        { ErrorDefinations.UserExists, "User already exists please user other email." },
        { ErrorDefinations.UserNotFound, "User not found." },
        { ErrorDefinations.UserInvalidPassword, "Password doesnot match." },
        { ErrorDefinations.InvalidUserId, "User doesnot exists please user other id." },
        { ErrorDefinations.ImageNotFound, "User doesnot have the image to update , upload the image first." },
        { ErrorDefinations.BlogNotFound, "Blog not found." },
        { ErrorDefinations.InvalidBlogId, "Blog doesnot exists please user other id" },
    };

    public static string GetStringValue(ErrorDefinations errorName)
    {
        if (Mapping.TryGetValue(errorName, out string? value))
            return value;

        throw new ArgumentException($"No string value mapping found for error defination '{errorName}'.");
    }
}