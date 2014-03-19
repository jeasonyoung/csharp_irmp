//================================================================================
// FileName: FlowStepPostEntity.cs
// Desc:
// Called by
// Auth: �������ɴ����������Զ����ɡ�
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
	///FlowStepPostEntityʵ���ࡣ
	///</summary>
	internal class FlowStepPostEntity: DbModuleEntity<FlowStepPost>
	{
		#region ��Ա���������캯����
		///<summary>
		///���캯��
		///</summary>
		public FlowStepPostEntity()
		{

		}
		#endregion

        #region ���ݴ���
        /// <summary>
        /// �������ݡ�
        /// </summary>
        /// <param name="stepID">����ID��</param>
        /// <param name="postID">��λID��</param>
        /// <param name="postName">��λ���ơ�</param>
        /// <returns></returns>
        public bool LoadFlowStepPost(string stepID, out string[] postID, out string[] postName)
        {
            postID = postName = new string[0];
            bool result = false;
            if (!string.IsNullOrEmpty(stepID))
            {
                List<string> listPostID = new List<string>(), listPostName = new List<string>();
                DataTable dtSource = this.GetAllRecord(string.Format("StepID='{0}'", stepID));
                foreach (DataRow row in dtSource.Rows)
                {
                    listPostID.Add(Convert.ToString(row["PostID"]));
                    listPostName.Add(Convert.ToString(row["PostName"]));
                }
                postID = new string[listPostID.Count];
                listPostID.CopyTo(postID);

                postName = new string[listPostName.Count];
                listPostName.CopyTo(postName);
                result = true;
            }
            return result;
        }
        /// <summary>
        /// ɾ������ӡ�
        /// </summary>
        /// <param name="stepID">����ID��</param>
        /// <param name="postID">��λID��</param>
        /// <param name="postName">��λ���ơ�</param>
        /// <returns></returns>
        public bool DeleteAllAndInsert(string stepID, string[] postID, string[] postName)
        {
            bool result = false;
            if (!string.IsNullOrEmpty(stepID))
            {
                result = this.DeleteRecord(string.Format("StepID='{0}'", stepID));

                if (postID != null && postName != null && (postID.Length == postName.Length))
                {
                    for (int i = 0; i < postID.Length; i++)
                    {
                        string id = postID[i];
                        if (!string.IsNullOrEmpty(id))
                        {
                            FlowStepPost flowStepPost = new FlowStepPost();
                            flowStepPost.StepID = stepID;
                            flowStepPost.PostID = id;
                            flowStepPost.PostName = postName[i];

                            result = this.UpdateRecord(flowStepPost);
                        }
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// �������ݡ�
        /// </summary>
        /// <param name="stepID">����ID��</param>
        /// <returns></returns>
        public StepPostCollection LoadStepPostCollection(GUIDEx stepID)
        {
            StepPostCollection collection = new StepPostCollection();
            if (stepID.IsValid)
            {
                DataTable dtSource = this.GetAllRecord(string.Format("StepID = '{0}'", stepID));
                List<FlowStepPost> list = this.ConvertDataSource(dtSource);
                if (list != null)
                {
                    foreach (FlowStepPost stepPost in list)
                    {
                        StepPost post = new StepPost();
                        post.PostID = stepPost.PostID;
                        post.PostName = stepPost.PostName;
                        collection.Add(post);
                    }
                }
            }
            return collection;
        }
        #endregion

        #region ���ء�
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
