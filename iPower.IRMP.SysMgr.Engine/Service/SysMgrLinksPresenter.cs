//================================================================================
// FileName: SysMgrLinksPresenter.cs
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
using iPower.Platform.Engine.Service;
using iPower.Platform.Engine.DataSource;
using iPower.IRMP.SysMgr.Engine.Domain;
using iPower.IRMP.SysMgr.Engine.Persistence;
namespace iPower.IRMP.SysMgr.Engine.Service
{
	///<summary>
	/// ISysMgrLinksView�ӿڡ�
	///</summary>
	public interface ISysMgrLinksView: IModuleView
	{
        /// <summary>
        /// ������Ϣ
        /// </summary>
        /// <param name="Msg"></param>
        void ShowMessage(string Msg);
	}
    /// <summary>
    /// �б����ӿ�
    /// </summary>
    public interface ISysMgrLinksListview : ISysMgrLinksView
    {
        /// <summary>
        /// ��������
        /// </summary>
        string LinkName { get; }
    }
    /// <summary>
    /// �༭����ӿ�
    /// </summary>
    public interface ISysMgrLinksEditview : ISysMgrLinksView
    {
        /// <summary>
        /// �����ӷ�ʽ
        /// </summary>
        /// <param name="data"></param>
        void BindLinkTarget(IListControlsData data);
        /// <summary>
        /// ������״̬
        /// </summary>
        /// <param name="data"></param>
        void BindLinkStatus(IListControlsData data);
        /// <summary>
        /// LinkID
        /// </summary>
        GUIDEx LinkID { get; }
    }
	///<summary>
	/// SysMgrLinksPresenter��Ϊ�ࡣ
	///</summary>
	public class SysMgrLinksPresenter: ModulePresenter<ISysMgrLinksView>
	{
		#region ��Ա���������캯����
        SysMgrLinksEntity sysMgrLinksEntity = null;
		///<summary>
		///���캯����
		///</summary>
		public SysMgrLinksPresenter(ISysMgrLinksView view)
		: base(view)
		{
            this.View.SecurityID = ModuleConstants.Links_ModuleID;
            this.sysMgrLinksEntity = new SysMgrLinksEntity();
		}
		#endregion

		#region ���ݲ���������
        /// <summary>
        /// ��ȡ�б�����Դ
        /// </summary>
        public DataTable ListDataSource
        {
            get 
            {
                ISysMgrLinksListview ListView = this.View as ISysMgrLinksListview;
                if (ListView != null)
                {
                    DataTable dtSource = this.sysMgrLinksEntity.ListDataSource(ListView.LinkName);
                    if (dtSource != null)
                    {
                        dtSource.Columns.Add("LinkTargetName");
                        dtSource.Columns.Add("LinkStatusName");
                        string strEmployeeName = string.Empty;
                        foreach (DataRow row in dtSource.Rows)
                        {
                            row["LinkTargetName"] = this.GetEnumMemberName(typeof(EnumLinkTarget), Convert.ToInt32(row["LinkTarget"]));
                            row["LinkStatusName"] = this.GetEnumMemberName(typeof(EnumLinkStatus), Convert.ToInt32(row["LinkStatus"]));
                            strEmployeeName = Convert.ToString(row["EmployeeName"]);
                            if (string.IsNullOrEmpty(strEmployeeName))
                                row["EmployeeName"] = "[ȫ��]";
                        }
                    }
                    return dtSource;
                }
                return null;
            }
        }
		///<summary>
		///�༭ҳ��������ݡ�
		///</summary>
		///<param name="handler"></param>
		public void LoadEntityData(EventHandler<EntityEventArgs<SysMgrLinks>> handler)
		{
            ISysMgrLinksEditview editview = this.View as ISysMgrLinksEditview;
            if (editview != null && editview.LinkID.IsValid)
            {
                SysMgrLinks data = new SysMgrLinks();
                data.LinkID = editview.LinkID;
                if (this.sysMgrLinksEntity.LoadRecord(ref data))
                    handler(this, new EntityEventArgs<SysMgrLinks>(data));
            }
		}
        /// <summary>
        /// ����
        /// </summary>
        protected override void PreViewLoadData()
        {
            base.PreViewLoadData();
            ISysMgrLinksEditview editview = this.View as ISysMgrLinksEditview;
            if (editview != null)
            {
                editview.BindLinkTarget(this.EnumDataSource(typeof(EnumLinkTarget)));
                editview.BindLinkStatus(this.EnumDataSource(typeof(EnumLinkStatus)));
                return;
            }

        }
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool UpdateSysMgrLinks(SysMgrLinks data)
        {
            ISysMgrLinksEditview editview = this.View as ISysMgrLinksEditview;
            if (editview != null && data != null)
            {
                try
                {
                    return this.sysMgrLinksEntity.UpdateRecord(data);
                }
                catch (Exception e)
                {
                    editview.ShowMessage(e.Message);
                }
            }
            return false;
        }
		#endregion
        /// <summary>
        /// ����ɾ������
        /// </summary>
        /// <param name="priCollection"></param>
        /// <returns></returns>
        public bool BatchDleteSysMgrLinks(StringCollection priCollection)
        {
            bool result = false;
             if (priCollection != null && priCollection.Count > 0)
            {
                try
                {
                    result = this.sysMgrLinksEntity.DeleteRecord(priCollection);
                }
                catch (Exception e)
                {
                    this.View.ShowMessage(e.Message);
                }
            }
            return result;
        }

	}

}
