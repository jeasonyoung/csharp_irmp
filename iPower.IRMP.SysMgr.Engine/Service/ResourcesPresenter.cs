//================================================================================
// FileName: ResourcesPresenter.cs
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
using iPower.Resources;
using iPower.Platform;
using iPower.Platform.Engine;
using iPower.Platform.Engine.Service;
using iPower.Platform.Engine.DataSource;
using iPower.IRMP.SysMgr.Engine.Domain;
using iPower.IRMP.SysMgr.Engine.Persistence;
namespace iPower.IRMP.SysMgr.Engine.Service
{
	///<summary>
	/// IResourcesView�ӿڡ�
	///</summary>
	public interface IResourcesView: IModuleView
	{
        /// <summary>
        /// ��ʾ��Ϣ��
        /// </summary>
        /// <param name="message"></param>
        void ShowMessage(string message);
	}
    /// <summary>
    /// �б����ӿڡ�
    /// </summary>
    public interface IResourcesListView : IResourcesView
    {
        /// <summary>
        /// ��ȡ��Դ������
        /// </summary>
        string ResKey { get; }
    }
    /// <summary>
    /// �༭����ӿڡ�
    /// </summary>
    public interface IResourcesEditView : IResourcesView
    {
        /// <summary>
        /// ��ȡ��Դ������
        /// </summary>
        string ResKey { get; }
    }
	///<summary>
	/// ResourcesPresenter��Ϊ�ࡣ
	///</summary>
	public class ResourcesPresenter: ModulePresenter<IResourcesView>
	{
		#region ��Ա���������캯����
		///<summary>
		///���캯����
		///</summary>
		public ResourcesPresenter(IResourcesView view)
		: base(view)
		{
            this.View.SecurityID = ModuleConstants.Resources_ModuleID;
		}
		#endregion

		#region ���ݲ���������
        /// <summary>
        /// ��ȡ�б�����Դ��
        /// </summary>
        public ResourceCollection ListDataSource
        {
            get
            {
                IResourcesListView listView = this.View as IResourcesListView;
                if (listView != null)
                {
                    ResourceFactory factory = ResourceFactory.Instance;
                    if (factory != null)
                    {
                        string resKey = listView.ResKey;
                        ResourceCollection collection = factory.Resources;
                        if (collection != null && collection.Count > 0)
                        {
                            if (!string.IsNullOrEmpty(resKey))
                            {
                                Resource[] array = new Resource[collection.Count];
                                collection.CopyTo(array, 0);
                                Resource[] results = Array.FindAll<Resource>(array, new Predicate<Resource>(delegate(Resource sender)
                                {
                                    return (sender != null) && sender.ResKey.IndexOf(resKey) > -1;
                                }));
                                if (results != null && results.Length > 0)
                                {
                                    collection = new ResourceCollection();
                                    foreach (Resource item in results)
                                        collection.Add(item);
                                }
                            }
                            return collection;
                        }
                    }
                }
                return null;
            }
        }
		///<summary>
		///�༭ҳ��������ݡ�
		///</summary>
		///<param name="handler"></param>
        public void LoadEntityData(EventHandler<EntityEventArgs<Resource>> handler)
        {
            IResourcesEditView editView = this.View as IResourcesEditView;
            if (editView != null && !string.IsNullOrEmpty(editView.ResKey))
            {
                ResourceFactory factory = ResourceFactory.Instance;
                if (factory != null)
                {
                    ResourceCollection collection = factory.Resources;
                    if (collection != null && collection.Count > 0)
                    {
                        Resource data = collection[editView.ResKey];
                        if (data != null)
                            handler(this, new EntityEventArgs<Resource>(data));
                    }
                }
            }
        }
        /// <summary>
        /// �������ݡ�
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool UpdateResources(Resource data)
        {
            bool result = false;
            try
            {
                ResourceFactory factory = ResourceFactory.Instance;
                if (factory != null && data != null)
                {
                    factory.AddResource(data.ResKey, data.ResValue, data.Description);
                    factory.Generate();
                    result = true;
                }
            }
            catch (Exception e)
            {
                this.View.ShowMessage(e.Message);
            }
            return result;
        }
        /// <summary>
        /// ����ɾ�����ݡ�
        /// </summary>
        /// <param name="priCollection"></param>
        /// <returns></returns>
        public bool BatchDeleteResources(StringCollection priCollection)
        {
            bool result = false;
            try
            {
                ResourceFactory factory = ResourceFactory.Instance;
                if (factory != null && priCollection != null && priCollection.Count > 0)
                {
                    foreach (string p in priCollection)
                    {
                        factory.RemoveResource(p);
                    }
                    factory.Generate();
                    result = true;
                }
            }
            catch (Exception e)
            {
                this.View.ShowMessage(e.Message);
            }
            return result;
        }
		#endregion

	}

}
