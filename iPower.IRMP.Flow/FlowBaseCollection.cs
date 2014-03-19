//================================================================================
//  FileName:FlowCollection.cs
//  Desc:
//
//  Called by
//
//  Auth:JeasonYoung
//  Date:2010-12-09 11:28:10
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

using iPower.Data;
namespace iPower.IRMP.Flow
{
    /// <summary>
    /// 集合基础类。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class FlowBaseCollection<T> : DataCollection<T>
        where T : class, new()
    {

    }
}
