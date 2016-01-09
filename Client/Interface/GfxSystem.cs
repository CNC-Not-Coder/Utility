using System;
using System.Collections.Generic;
using UnityEngine;
using MyUtility;

namespace Client
{
    public class GfxSystem
    {
        internal GameObject GetGameObject(int id)
        {
            GameObject ret = null;
            if (m_GameObjects.Contains(id))
                ret = m_GameObjects[id].ObjectInstance;
            return ret;
        }
        internal SharedGameObjectInfo GetSharedGameObjectInfo(int id)
        {
            SharedGameObjectInfo ret = null;
            if (m_GameObjects.Contains(id))
                ret = m_GameObjects[id].ObjectInfo;
            return ret;
        }
        internal SharedGameObjectInfo GetSharedGameObjectInfo(GameObject obj)
        {
            int id = GetGameObjectId(obj);
            return GetSharedGameObjectInfo(id);
        }
        internal bool ExistGameObject(GameObject obj)
        {
            int id = GetGameObjectId(obj);
            return id > 0;
        }
        private GameObjectInfo GetGameObjectInfo(int id)
        {
            GameObjectInfo ret = null;
            if (m_GameObjects.Contains(id))
                ret = m_GameObjects[id];
            return ret;
        }
        private int GetGameObjectId(GameObject obj)
        {
            int ret = 0;
            if (m_GameObjectIds.ContainsKey(obj))
            {
                ret = m_GameObjectIds[obj];
            }
            return ret;
        }
        private void RememberGameObject(int id, GameObject obj)
        {
            RememberGameObject(id, obj, null);
        }
        private void RememberGameObject(int id, GameObject obj, SharedGameObjectInfo info)
        {
            if (m_GameObjects.Contains(id))
            {
                GameObject oldObj = m_GameObjects[id].ObjectInstance;
                oldObj.SetActive(false);
                m_GameObjectIds.Remove(oldObj);
                GameObject.Destroy(oldObj);
                m_GameObjects[id] = new GameObjectInfo(obj, info);
            }
            else
            {
                m_GameObjects.AddLast(id, new GameObjectInfo(obj, info));
            }
            m_GameObjectIds.Add(obj, id);
        }
        private void ForgetGameObject(int id, GameObject obj)
        {
            m_GameObjects.Remove(id);
            m_GameObjectIds.Remove(obj);
        }

        internal static GfxSystem Instance
        {
            get
            {
                return s_Instance;
            }
        }
        private static GfxSystem s_Instance = new GfxSystem();
        private class GameObjectInfo
        {
            public GameObject ObjectInstance;
            public SharedGameObjectInfo ObjectInfo;

            public GameObjectInfo(GameObject o, SharedGameObjectInfo i)
            {
                ObjectInstance = o;
                ObjectInfo = i;
            }
        }
        private LinkedListDictionary<int, GameObjectInfo> m_GameObjects = new LinkedListDictionary<int, GameObjectInfo>();
        private MyDictionary<GameObject, int> m_GameObjectIds = new MyDictionary<GameObject, int>();
    }
}
