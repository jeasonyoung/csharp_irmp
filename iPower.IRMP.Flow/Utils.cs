//================================================================================
//  FileName: Utils.cs
//  Desc:
//
//  Called by
//
//  Auth:杨勇（jeason1914@gmail.com）
//  Date: 2011/1/14
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
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml.Serialization;

using iPower;
using iPower.Utility;
using iPower.Cryptography;
namespace iPower.IRMP.Flow
{
    /// <summary>
    /// 工具类。
    /// </summary>
    public static class Utils
    {
        #region 成员变量。
        static object SynchronizedObject = new object();
        #endregion

        #region 序列化与反序列化。
        /// <summary>
        /// 序列化对象为Xml格式数据。
        /// </summary>
        /// <typeparam name="T">序列化类。</typeparam>
        /// <param name="output">存储Xml格式数据流。</param>
        /// <param name="instance">序列化的对象。</param>
        public static void Serializer<T>(Stream output, T instance)
            where T:class, new()
        {
            lock (Utils.SynchronizedObject)
            {
                Guard.ArgumentNotNull("存储Xml格式数据流。", output, true);
                Guard.ArgumentNotNull("序列化的对象。", instance, true);

                XmlSerializer serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(output, instance);
            }
        }
        /// <summary>
        /// Xml格式的数据反序列为对象。
        /// </summary>
        /// <typeparam name="T">反序列化类。</typeparam>
        /// <param name="input">Xml格式数据流。</param>
        /// <returns>对象。</returns>
        public static T DeSerializer<T>(Stream input)
            where T: class, new()
        {
            lock (Utils.SynchronizedObject)
            {
                Guard.ArgumentNotNull("Xml格式数据流。", input, true);
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                return serializer.Deserialize(input) as T;
            }
        }
        #endregion

        #region 对象/数据库存储。
        /// <summary>
        /// 将对象转化为数据库存储的Hex数据格式。
        /// </summary>
        /// <param name="instance">序列化的对象。</param>
        /// <param name="verify">生成验证码。</param>
        /// <returns>数据库存储的Hex数据格式。</returns>
        public static string SerializerDatabaseFormart<T>(T instance, out string verify)
            where T : class,new()
        {
            lock (Utils.SynchronizedObject)
            {
                string result = verify = string.Empty;
                Guard.ArgumentNotNull("序列化的对象。", instance, true);
                using (MemoryStream ms = new MemoryStream())
                {
                    Utils.Serializer<T>(ms, instance);
                    ms.Position = 0;
                    verify = HashCrypto.HashFile(ms, "md5");

                    byte[] data = ms.ToArray();
                    ms.Close();

                    result = HexParser.ToHexString(data);
                }
                return result;
            }
        }
        /// <summary>
        /// 将数据库存储的Hex格式数据转化为对象。
        /// </summary>
        /// <typeparam name="T">对象类。</typeparam>
        /// <param name="hexData">数据库存储Hex格式数据。</param>
        /// <param name="verify">数据库存储的验证码。</param>
        /// <returns></returns>
        public static T DeSerializerDatabaseFormart<T>(string hexData, string verify)
            where T : class, new()
        {
            lock (Utils.SynchronizedObject)
            {
                T result = default(T);
                Guard.ArgumentNotNullOrEmptyString("数据库存储Hex格式数据。", hexData, true);
                Guard.ArgumentNotNullOrEmptyString("数据库存储的验证码。", verify, true);
                byte[] data = HexParser.Parse(hexData);
                if (data != null && data.Length > 0)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        ms.Write(data, 0, data.Length);
                        ms.Position = 0;
                        string strVerify = HashCrypto.HashFile(ms, "md5");
                        if (!string.Equals(verify, strVerify, StringComparison.InvariantCulture | StringComparison.InvariantCultureIgnoreCase))
                        {
                            ms.Close();
                            throw new Exception("验证码不一致，数据被篡改！");
                        }
                        ms.Position = 0;
                        result = Utils.DeSerializer<T>(ms) as T;
                        ms.Close();
                    }
                }
                return result;
            }
        }
        /// <summary>
        /// 将数据库存储的Hex格式数据转化为对象。
        /// </summary>
        /// <typeparam name="T">对象类。</typeparam>
        /// <param name="hexData">数据库存储Hex格式数据。</param>
        /// <returns></returns>
        public static T DeSerializerDatabaseFormart<T>(string hexData)
             where T : class, new()
        {
            lock (Utils.SynchronizedObject)
            {
                Guard.ArgumentNotNullOrEmptyString("数据库存储Hex格式数据。", hexData, true);
                T result = default(T);
                byte[] data = HexParser.Parse(hexData);
                if (data != null && data.Length > 0)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        ms.Write(data, 0, data.Length);
                        ms.Position = 0;
                        result = Utils.DeSerializer<T>(ms) as T;
                        ms.Close();
                    }
                }
                return result;
            }
        }
        #endregion
    }
}
