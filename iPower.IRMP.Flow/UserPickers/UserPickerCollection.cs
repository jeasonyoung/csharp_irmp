//================================================================================
//  FileName:UserPickerCollection.cs
//  Desc:
//
//  Called by
//
//  Auth:JeasonYoung
//  Date:2010-12-15 14:37:50
//================================================================================
//  Change History
//================================================================================
//  Date  Author  Description
// ----  ------  -----------
//
//================================================================================
//  Copyright (C) 2009-2010 Jeason Young Corporation
//================================================================================

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Yaesoft.IRMP.Flow.UserPickers
{
    /// <summary>
    /// 用户集合。
    /// </summary>
    public class UserPickerCollection<T> : ICollection<T>
    {
        #region 成员变量，构造函数。
        List<T> list = null;
        /// <summary>
        /// 构造函数。
        /// </summary>
        public UserPickerCollection()
        {
            this.list = new List<T>();
        }
        #endregion

        #region 属性。
        /// <summary>
        /// 
        /// </summary>
        protected List<T> Data
        {
            get { return this.list; }
        }
        #endregion

        #region ICollection<T> 成员
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        public void Add(T item)
        {
            if (item != null)
                this.list.Add(item);
        }
        /// <summary>
        /// 
        /// </summary>
        public void Clear()
        {
            this.list.Clear();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Contains(T item)
        {
            return this.list.Contains(item);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="arrayIndex"></param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            this.list.CopyTo(array, arrayIndex);
        }
        /// <summary>
        /// 
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
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Remove(T item)
        {
            return this.list.Remove(item);
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
    }
}
