namespace Nix.Tasks.UrlShortener.Infrastructure
{
    public static class IntExtension
    {
        public static string ToBase62(this uint number)
        {
            const string symbols = "ghijklmnopqrstuvwxyz0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdef";
            
            if (number == 0) return symbols[0].ToString();

            const ushort basis = 62;
            var n = number;
            var res = string.Empty;
            while (n > 0)
            {
                res = symbols[(int)(n % basis)] + res;
                n /= basis;
            }
            return res;
        }
    }
}
