namespace TranspoDocMonitor.Service.Core.Authorization
{
    public static class PasswordBuilder
    {
        private const string Salt = "fsdkfmsdkgfkfsdgfqwfqwf";


        public static string ComputeHash(this string password)
        {
            using var md5 = System.Security.Cryptography.MD5.Create();
            var inputBytes = System.Text.Encoding.ASCII.GetBytes(password + Salt);
            var hashBytes = md5.ComputeHash(inputBytes);

            return Convert.ToHexString(hashBytes);
        }

        public static bool Equal(string password, string md5)
        {
            var actualHash = ComputeHash(password);

            return actualHash == md5;
        }

    }
}
