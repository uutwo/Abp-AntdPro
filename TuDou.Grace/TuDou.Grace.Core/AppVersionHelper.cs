using System;
using System.IO;
using Abp.Reflection.Extensions;

namespace TuDou.Grace
{
    /// <summary>
    /// 应用程序版本的中心点。
    /// </summary>
    public class AppVersionHelper
    {
        /// <summary>
        /// 获取应用程序的当前版本。
        /// 它也显示在网页上。
        /// </summary>
        public const string Version = "7.0.0.0";

        /// <summary>
        /// 获取应用程序的发布(最后构建)日期。
        /// 它也显示在网页上。
        /// </summary>
        public static DateTime ReleaseDate => new FileInfo(typeof(AppVersionHelper).GetAssembly().Location).LastWriteTime;
    }
}