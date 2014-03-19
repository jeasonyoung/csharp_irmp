//================================================================================
// FileName: FlowStepRoleEntity.cs
// Desc:
// Called by
// Auth: 本代码由代码生成器自动生成。
// Date:
//================================================================================
// Change History
//================================================================================
// Date  Author  Description
// ----  ------  -----------
//
//================================================================================
// Copyright (C) 2009-2010 Jeason Young Corporation
//================================================================================
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Data;

using iPower;
using iPower.Platform;
using iPower.Platform.Engine;
using iPower.Platform.Engine.DataSource;
using iPower.Platform.Engine.Persistence;
using iPower.IRMP.Flow.Engine.Domain;
namespace iPower.IRMP.Flow.Engine.Persistence
{
	///<summary>
	///FlowStepRoleEntity实体类。
	///</summary>
	internal class FlowStepRoleEntity: DbModuleEntity<FlowStepRole>
	{
		#region 成员变量，构造函数。
		///<summary>
		///构造函数
		///</summary>
		public FlowStepRoleEntity()
		{

		}
		#endregion

        #region 数据处理。
        /// <summary>
        /// 加载数据。
        /// </summary>
        /// <param name="stepID">步骤ID。</param>
        /// <param name="roleID">角色ID。</param>
        /// <param name="roleName">角色名称。</param>
        /// <returns></returns>
        public bool LoadFlowStepRole(string stepID, out string[] roleID, out string[] roleName)
        {
            roleID = roleName = new string[0];
            bool result = false;
            if (!string.IsNullOrEmpty(stepID))
            {
                List<string> listRoleID = new List<string>(), listRoleName = new List<string>();
                DataTable dtSource = this.GetAllRecord(string.Format("StepID='{0}'", stepID));
                foreach (DataRow row in dtSource.Rows)
                {
                    listRoleID.Add(Convert.ToString(row["RoleID"]));
                    listRoleName.Add(Convert.ToString(row["RoleName"]));
                }
                roleID = new string[listRoleID.Count];
                listRoleID.CopyTo(roleID);

                roleName = new string[listRoleName.Count];
                listRoleName.CopyTo(roleName);
                result = true;
            }
            return result;
        }
        /// <summary>
        /// 删除并添加。
        /// </summary>
        /// <param name="stepID">步骤ID。</param>
        /// <param name="roleID">角色ID。</param>
        /// <param name="roleName">角色名称。</param>
        /// <returns></returns>
        public bool DeleteAllAndInsert(string stepID, string[] roleID, string[] roleName)
        {
            bool result = false;
            if (!string.IsNullOrEmpty(stepID))
            {
                result = this.DeleteRecord(string.Format("StepID='{0}'", stepID));

                if (roleID != null && roleName != null && (roleID.Length == roleName.Length))
                {
                    for (int i = 0; i < roleID.Length; i++)
                    {
                        string id = roleID[i];
                        if (!string.IsNullOrEmpty(id))
                        {
                            FlowStepRole flowStepRole = new FlowStepRole();
                            flowStepRole.StepID = stepID;
                            flowStepRole.RoleID = id;
                            flowStepRole.RoleName = roleName[i];

                            result = this.UpdateRecord(flowStepRole);
                        }
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// 加载数据。
        /// </summary>
        /// <param name="stepID">步骤ID。</param>
        /// <returns></returns>
        public StepRoleCollection LoadStepRoleCollection(GUIDEx stepID)
        {
            StepRoleCollection collection = new StepRoleCollection();
            if (stepID.IsValid)
            {
                DataTable dtSource = this.GetAllRecord(string.Format("StepID='{0}'", stepID));
                List<FlowStepRole> list = this.ConvertDataSource(dtSource);
                if (list != null)
                {
                    foreach (FlowStepRole stepRole in list)
                    {
                        StepRole r = new StepRole();
                        r.RoleID = stepRole.RoleID;
                        r.RoleName = stepRole.RoleName;
                        collection.Add(r);
                    }
                }
            }
            return collection;
        }
        #endregion

        #region 重载。
        public override bool DeleteRecord(StringCollection primaryValues)
        {
            if (primaryValues != null && primaryValues.Count > 0)
            {
                string[] p = new string[primaryValues.Count];
                primaryValues.CopyTo(p, 0);

                const string sql = "delete from {0} where StepID in ('{1}')";

                return this.DatabaseAccess.ExecuteNonQuery(string.Format(sql, this.TableName, string.Join("','", p))) > 0;
                //return base.DeleteRecord(primaryValues);
            }
            return false;
        }
        #endregion
    }

}
