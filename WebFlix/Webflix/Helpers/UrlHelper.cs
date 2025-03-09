namespace Webflix.Helpers;

public static class UrlHelper
{
    public static string EnsureHttps(string url)
    {
        if (url.StartsWith("http://"))
        {
            return "https://" + url.Substring(7);
        }
        return url;
    }
}