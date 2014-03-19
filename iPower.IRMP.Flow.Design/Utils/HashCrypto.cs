//================================================================================
//  FileName: HashCrypto.cs
//  Desc:
//
//  Called by
//
//  Auth:杨勇（jeason1914@gmail.com）
//  Date: 2011/5/24
//================================================================================
//  Change History
//================================================================================
//  Date  Author  Description
//  ----    ------  -----------------
//
//================================================================================
//  Copyright (C) 2004-2009 Jeason Young Corporation
//================================================================================
using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;

namespace iPower.IRMP.Flow.Design.Utils
{
    /// <summary>
    /// 加密/解密工具类。
    /// </summary>
    public static class HashCrypto
    {
        /// <summary>
        /// Hash算法。
        /// </summary>
        /// <param name="input">被Hash的字节数组。</param>
        /// <returns>Hash结果字节数组。</returns>
        public static byte[] HashSHA1(byte[] input)
        {
            if (input == null)
                throw new ArgumentNullException("input", "被Hash的字节数组。");
            HashAlgorithm algorithm = new SHA1Managed();
            byte[] result = algorithm.ComputeHash(input);
            return result;
        }
        /// <summary>
        /// Hash算法。
        /// </summary>
        /// <param name="input">被Hash的字节流。</param>
        /// <returns>Hash结果字节数组。</returns>
        public static byte[] HashSHA1(Stream input)
        {
            if (input == null)
                throw new ArgumentNullException("input", "被Hash的字节流。");
            HashAlgorithm algorithm = new SHA1Managed();
            byte[] result = algorithm.ComputeHash(input);
            return result;
        }
        /// <summary>
        /// Hash算法。
        /// </summary>
        /// <param name="data">源数据。</param>
        /// <returns>Hash数据。</returns>
        public static string HashSHA1(string data)
        {
            if (!string.IsNullOrEmpty(data))
            {
                byte[] input = Encoding.UTF8.GetBytes(data);
                if (input != null)
                {
                    byte[] output = HashSHA1(input);
                    if (output != null)
                    {
                        return HexParser.ToHexString(output);
                    }
                }
            }
            return null;
        }
        /// <summary>
        /// Hash文件。
        /// </summary>
        /// <param name="fileName">被Hash的文件（包括路径）。</param>
        /// <returns>Hash结果字符串。</returns>
        public static string HashSHA1File(string fileName)
        {
            byte[] hashBytes = HashSHA1FileReturnRawData(fileName);
            if (hashBytes == null)
            {
                return null;
            }
            else
            {
                return HexParser.ToHexString(hashBytes);
            }
        }
        /// <summary>
        /// Hash数据流。
        /// </summary>
        /// <param name="fileStream">数据流。</param>
        /// <returns>Hash结果字符串。</returns>
        public static string HashSHA1File(Stream fileStream)
        {
            if (fileStream != null)
            {
                byte[] buf = HashSHA1(fileStream);
                if (buf != null)
                {
                    return HexParser.ToHexString(buf);
                }
            }
            return null;
        }

        /// <summary>
        /// Hash文件。
        /// </summary>
        /// <param name="fileName">被Hash的文件（包括路径）。</param>
        /// <returns>Hash结果。</returns>
        public static byte[] HashSHA1FileReturnRawData(string fileName)
        {
            using (FileStream fs = File.OpenRead(fileName))
            {
                return HashSHA1(fs);
            }
        }
    }
}
