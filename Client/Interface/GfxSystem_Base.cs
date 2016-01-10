using System;
using MyUtility;
using UnityEngine;
using System.Collections.Generic;

namespace Client
{
    /// <summary>
    /// 这个分类提供基本方法给GfxSystem.cs使用
    /// 这是私有的，不应该被外部访问
    /// </summary>
    public sealed partial class GfxSystem
    {
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
        private static void QueueGfxAction(MyAction action)
        {
            QueueGfxActionWithDelegation(action);
        }
        private static void QueueGfxAction<T1>(MyAction<T1> action, T1 t1)
        {
            QueueGfxActionWithDelegation(action, t1);
        }
        private static void QueueGfxAction<T1, T2>(MyAction<T1, T2> action, T1 t1, T2 t2)
        {
            QueueGfxActionWithDelegation(action, t1, t2);
        }
        private static void QueueGfxAction<T1, T2, T3>(MyAction<T1, T2, T3> action, T1 t1, T2 t2, T3 t3)
        {
            QueueGfxActionWithDelegation(action, t1, t2, t3);
        }
        private static void QueueGfxAction<T1, T2, T3, T4>(MyAction<T1, T2, T3, T4> action, T1 t1, T2 t2, T3 t3, T4 t4)
        {
            QueueGfxActionWithDelegation(action, t1, t2, t3, t4);
        }
        private static void QueueGfxAction<T1, T2, T3, T4, T5>(MyAction<T1, T2, T3, T4, T5> action, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5)
        {
            QueueGfxActionWithDelegation(action, t1, t2, t3, t4, t5);
        }
        private static void QueueGfxAction<T1, T2, T3, T4, T5, T6>(MyAction<T1, T2, T3, T4, T5, T6> action, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6)
        {
            QueueGfxActionWithDelegation(action, t1, t2, t3, t4, t5, t6);
        }
        private static void QueueGfxAction<T1, T2, T3, T4, T5, T6, T7>(MyAction<T1, T2, T3, T4, T5, T6, T7> action, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7)
        {
            QueueGfxActionWithDelegation(action, t1, t2, t3, t4, t5, t6, t7);
        }
        private static void QueueGfxAction<T1, T2, T3, T4, T5, T6, T7, T8>(MyAction<T1, T2, T3, T4, T5, T6, T7, T8> action, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8)
        {
            QueueGfxActionWithDelegation(action, t1, t2, t3, t4, t5, t6, t7, t8);
        }
        private static void QueueGfxAction<T1, T2, T3, T4, T5, T6, T7, T8, T9>(MyAction<T1, T2, T3, T4, T5, T6, T7, T8, T9> action, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9)
        {
            QueueGfxActionWithDelegation(action, t1, t2, t3, t4, t5, t6, t7, t8, t9);
        }
        private static void QueueGfxAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(MyAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> action, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10)
        {
            QueueGfxActionWithDelegation(action, t1, t2, t3, t4, t5, t6, t7, t8, t9, t10);
        }
        private static void QueueGfxAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(MyAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> action, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11)
        {
            QueueGfxActionWithDelegation(action, t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11);
        }
        private static void QueueGfxAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(MyAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> action, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12)
        {
            QueueGfxActionWithDelegation(action, t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12);
        }
        private static void QueueGfxAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(MyAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> action, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12, T13 t13)
        {
            QueueGfxActionWithDelegation(action, t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13);
        }
        private static void QueueGfxAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(MyAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> action, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12, T13 t13, T14 t14)
        {
            QueueGfxActionWithDelegation(action, t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14);
        }
        private static void QueueGfxAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(MyAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> action, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12, T13 t13, T14 t14, T15 t15)
        {
            QueueGfxActionWithDelegation(action, t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, t15);
        }
        private static void QueueGfxAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(MyAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> action, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12, T13 t13, T14 t14, T15 t15, T16 t16)
        {
            QueueGfxActionWithDelegation(action, t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, t15, t16);
        }
        private static void QueueGfxAction<R>(MyFunc<R> action)
        {
            QueueGfxActionWithDelegation(action);
        }
        private static void QueueGfxAction<T1, R>(MyFunc<T1, R> action, T1 t1)
        {
            QueueGfxActionWithDelegation(action, t1);
        }
        private static void QueueGfxAction<T1, T2, R>(MyFunc<T1, T2, R> action, T1 t1, T2 t2)
        {
            QueueGfxActionWithDelegation(action, t1, t2);
        }
        private static void QueueGfxAction<T1, T2, T3, R>(MyFunc<T1, T2, T3, R> action, T1 t1, T2 t2, T3 t3)
        {
            QueueGfxActionWithDelegation(action, t1, t2, t3);
        }
        private static void QueueGfxAction<T1, T2, T3, T4, R>(MyFunc<T1, T2, T3, T4, R> action, T1 t1, T2 t2, T3 t3, T4 t4)
        {
            QueueGfxActionWithDelegation(action, t1, t2, t3, t4);
        }
        private static void QueueGfxAction<T1, T2, T3, T4, T5, R>(MyFunc<T1, T2, T3, T4, T5, R> action, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5)
        {
            QueueGfxActionWithDelegation(action, t1, t2, t3, t4, t5);
        }
        private static void QueueGfxAction<T1, T2, T3, T4, T5, T6, R>(MyFunc<T1, T2, T3, T4, T5, T6, R> action, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6)
        {
            QueueGfxActionWithDelegation(action, t1, t2, t3, t4, t5, t6);
        }
        private static void QueueGfxAction<T1, T2, T3, T4, T5, T6, T7, R>(MyFunc<T1, T2, T3, T4, T5, T6, T7, R> action, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7)
        {
            QueueGfxActionWithDelegation(action, t1, t2, t3, t4, t5, t6, t7);
        }
        private static void QueueGfxAction<T1, T2, T3, T4, T5, T6, T7, T8, R>(MyFunc<T1, T2, T3, T4, T5, T6, T7, T8, R> action, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8)
        {
            QueueGfxActionWithDelegation(action, t1, t2, t3, t4, t5, t6, t7, t8);
        }
        private static void QueueGfxAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, R>(MyFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, R> action, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9)
        {
            QueueGfxActionWithDelegation(action, t1, t2, t3, t4, t5, t6, t7, t8, t9);
        }
        private static void QueueGfxAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, R>(MyFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, R> action, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10)
        {
            QueueGfxActionWithDelegation(action, t1, t2, t3, t4, t5, t6, t7, t8, t9, t10);
        }
        private static void QueueGfxAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, R>(MyFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, R> action, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11)
        {
            QueueGfxActionWithDelegation(action, t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11);
        }
        private static void QueueGfxAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, R>(MyFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, R> action, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12)
        {
            QueueGfxActionWithDelegation(action, t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12);
        }
        private static void QueueGfxAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, R>(MyFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, R> action, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12, T13 t13)
        {
            QueueGfxActionWithDelegation(action, t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13);
        }
        private static void QueueGfxAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, R>(MyFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, R> action, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12, T13 t13, T14 t14)
        {
            QueueGfxActionWithDelegation(action, t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14);
        }
        private static void QueueGfxAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, R>(MyFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, R> action, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12, T13 t13, T14 t14, T15 t15)
        {
            QueueGfxActionWithDelegation(action, t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, t15);
        }
        private static void QueueGfxAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, R>(MyFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, R> action, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12, T13 t13, T14 t14, T15 t15, T16 t16)
        {
            QueueGfxActionWithDelegation(action, t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, t15, t16);
        }
        private static void QueueGfxActionWithDelegation(Delegate action, params object[] args)
        {
            if (null != s_Instance.m_GfxInvoker)
            {
                s_Instance.m_GfxInvoker.QueueActionWithDelegation(action, args);
            }
        }
        private void TickImpl()
        {
            m_GfxInvoker.HandleActions(4096);
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
        
        private LinkedListDictionary<int, GameObjectInfo> m_GameObjects = new LinkedListDictionary<int, GameObjectInfo>();
        private MyDictionary<GameObject, int> m_GameObjectIds = new MyDictionary<GameObject, int>();
        private AsyncActionProcessor m_GfxInvoker = new AsyncActionProcessor();
        private object m_syncLock = new object();
    }
}
