using System;
using System.Collections.Generic;
using UnityEngine;
using MyUtility;

namespace Client
{
    /// <summary>
    /// 这个分类全是提供给外部访问的方法
    /// </summary>
    public sealed partial class GfxSystem
    {
        public GameObject GetGameObject(int id)
        {
            GameObject ret = null;
            if (m_GameObjects.Contains(id))
                ret = m_GameObjects[id].ObjectInstance;
            return ret;
        }
        public SharedGameObjectInfo GetSharedGameObjectInfo(int id)
        {
            SharedGameObjectInfo ret = null;
            if (m_GameObjects.Contains(id))
                ret = m_GameObjects[id].ObjectInfo;
            return ret;
        }
        public SharedGameObjectInfo GetSharedGameObjectInfo(GameObject obj)
        {
            int id = GetGameObjectId(obj);
            return GetSharedGameObjectInfo(id);
        }
        public bool ExistGameObject(GameObject obj)
        {
            int id = GetGameObjectId(obj);
            return id > 0;
        }
        public void Tick()
        {
            TickImpl();
        }
        public Transform FindChildRecursive(Transform parent, string bonePath)
        {
            Transform t = parent.Find(bonePath);
            if (null != t)
            {
                return t;
            }
            else
            {
                int ct = parent.childCount;
                for (int i = 0; i < ct; ++i)
                {
                    t = FindChildRecursive(parent.GetChild(i), bonePath);
                    if (null != t)
                    {
                        return t;
                    }
                }
            }
            return null;
        }
        public static object SyncLock
        {
            get { return s_Instance.m_syncLock; }
        }
    }
}
