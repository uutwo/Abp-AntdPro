using System;

namespace TuDou.Grace
{
    /// <summary>
    /// 应用程序常量
    /// </summary>
    public class AppConsts
    {
        /// <summary>
        ///分页请求的默认页面大小。
        /// </summary>
        public const int DefaultPageSize = 10;

        /// <summary>
        /// 分页请求允许的最大页面大小。
        /// </summary>
        public const int MaxPageSize = 1000;

        /// <summary>
        /// SimpleStringCipher解密/加密操作的默认传递短语
        /// </summary>
        public const string DefaultPassPhrase = "gsKxGZ012HLL3MI5";

        public const int ResizedMaxProfilPictureBytesUserFriendlyValue = 1024;

        public const int MaxProfilPictureBytesUserFriendlyValue = 5;

        public const string TokenValidityKey = "token_validity_key";
        public const string SecurityStampKey = "AspNet.Identity.SecurityStamp";
        public const string TokenType = "token_type";
        public static string UserIdentifier = "user_identifier";
        public static TimeSpan AccessTokenExpiration = TimeSpan.FromDays(1);
        public static TimeSpan RefreshTokenExpiration = TimeSpan.FromDays(365);

        public const string DateTimeOffsetFormat = "yyyy-MM-ddTHH:mm:sszzz";
    }
}
