//================================================================================
//  FileName: WFBaseCollection.cs
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
using System.Collections;
using System.Collections.Generic;
namespace iPower.IRMP.Flow.Design.Data
{
    /// <summary>
    /// 集合基础类。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class WFCollection<T> : ICollection, ICollection<T>
        where T : class
    {
        #region 成员变量，构造函数。
        List<T> list = null;
        /// <summary>
        /// 构造函数。
        /// </summary>
        public WFCollection()
        {
            this.list = new List<T>();
        }
        #endregion

        #region 索引属性。
        /// <summary>
        /// 获取数据对象。
        /// </summary>
        /// <param name="index">对象索引。</param>
        /// <returns>数据对象。</returns>
        public T this[int index]
        {
            get
            {
                return this.list[index];
            }
        }
        #endregion

        #region 保护性属性。
        /// <summary>
        /// 获取数据。
        /// </summary>
        protected List<T> DataCollection
        {
            get { return this.list; }
        }
        #endregion

        #region ICollection<T> 成员
        /// <summary>
        /// 添加元素。
        /// </summary>
        /// <param name="item">元素。</param>
        public void Add(T item)
        {
            if (item != null)
                this.list.Add(item);
        }
        /// <summary>
        /// 清空全部元素。
        /// </summary>
        public void Clear()
        {
            this.list.Clear();
        }
        /// <summary>
        /// 元素是否存在。
        /// </summary>
        /// <param name="item">元素。</param>
        /// <returns></returns>
        public bool Contains(T item)
        {
            if (item != null)
                return this.list.Contains(item);
            return false;
        }
        /// <summary>
        /// 复制到数据。
        /// </summary>
        /// <param name="array"></param>
        /// <param name="arrayIndex"></param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            this.list.CopyTo(array, arrayIndex);
        }
        /// <summary>
        /// 获取元素数量。
        /// </summary>
        public int Count
        {
            get { return this.list.Count; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool IsReadOnly
        {
            get { return false; }
        }
        /// <summary>
        /// 移除给定的元素。
        /// </summary>
        /// <param name="item">元素。</param>
        /// <returns></returns>
        public bool Remove(T item)
        {
            if (item != null)
                return this.list.Remove(item);
            return false;
        }

        #endregion

        #region IEnumerable<T> 成员
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerator<T> GetEnumerator()
        {
            return this.list.GetEnumerator();
        }

        #endregion

        #region IEnumerable 成员
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            foreach (T t in this.list)
                yield return t;
        }

        #endregion

        #region ICollection 成员
        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        public void CopyTo(Array array, int index)
        {
            this.list.CopyTo((T[])array, index);
        }
        /// <summary>
        /// 
        /// </summary>
        public bool IsSynchronized
        {
            get { return false; }
        }
        /// <summary>
        /// 
        /// </summary>
        public object SyncRoot
        {
            get { return this; }
        }

        #endregion
    }
}
