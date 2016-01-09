using MyUtility;
using System;
using UnityEngine;

namespace Client
{
    public sealed partial class LogicSystem
    {
        public static void SetLogicInvoker(IActionQueue invoker)
        {
            s_Instance.m_LogicInvoker = invoker;
        }
        public static void NotifyGfxSetCrossFadeTime(GameObject obj, string m_TargetAnimType, float m_CrossFadeTime)
        {
            throw new NotImplementedException();
        }

        public static SharedGameObjectInfo GetSharedGameObjectInfo(GameObject obj)
        {
            return GfxSystem.Instance.GetSharedGameObjectInfo(obj);
        }
        public static SharedGameObjectInfo GetSharedGameObjectInfo(int id)
        {
            return GfxSystem.Instance.GetSharedGameObjectInfo(id);
        }

        public static void NotifyGfxHitTarget(GameObject damageOwner, int impactId, GameObject target, int v1, int final_skill_id, int remainTime, Vector3 position, object v2)
        {
            throw new NotImplementedException();
        }

        public static void NotifyGfxMoveControlFinish(GameObject targetObj, int skillId, bool v)
        {
            throw new NotImplementedException();
        }

        public static void NotifyGfxDestroySummonObject(GameObject obj)
        {
            throw new NotImplementedException();
        }

        public static void NotifyGfxDestroyObj(GameObject obj)
        {
            throw new NotImplementedException();
        }

        public static void NotifyGfxMoveControlStart(GameObject targetObj, int skillId, bool v)
        {
            throw new NotImplementedException();
        }

        public static GameObject GetGameObject(int v)
        {
            throw new NotImplementedException();
        }

        public static void NotifyGfxUpdatePosition(GameObject obj, float x, float y, float z, int v1, float direction, int v2)
        {
            throw new NotImplementedException();
        }

        public static Transform FindChildRecursive(Transform transform, string m_AttachPath)
        {
            throw new NotImplementedException();
        }

        public static void NotifyGfxSummonNpc(GameObject obj, int skillId, int m_NpcTypeId, string m_ModelPrefab, int m_SkillId, float x, float y, float z)
        {
            throw new NotImplementedException();
        }

        public static void NotifyGfxUpdatePosition(GameObject obj, float x, float y, float z)
        {
            throw new NotImplementedException();
        }
    }
}
