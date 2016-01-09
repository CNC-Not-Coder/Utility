using System;
using UnityEngine;

namespace Client
{
    public class LogicSystem
    {
        internal static void NotifyGfxSetCrossFadeTime(GameObject obj, string m_TargetAnimType, float m_CrossFadeTime)
        {
            throw new NotImplementedException();
        }

        internal static SharedGameObjectInfo GetSharedGameObjectInfo(GameObject obj)
        {
            return GfxSystem.Instance.GetSharedGameObjectInfo(obj);
        }
        internal static SharedGameObjectInfo GetSharedGameObjectInfo(int id)
        {
            return GfxSystem.Instance.GetSharedGameObjectInfo(id);
        }

        internal static void NotifyGfxHitTarget(GameObject damageOwner, int impactId, GameObject target, int v1, int final_skill_id, int remainTime, Vector3 position, object v2)
        {
            throw new NotImplementedException();
        }

        internal static void NotifyGfxMoveControlFinish(GameObject targetObj, int skillId, bool v)
        {
            throw new NotImplementedException();
        }

        internal static void NotifyGfxDestroySummonObject(GameObject obj)
        {
            throw new NotImplementedException();
        }

        internal static void NotifyGfxDestroyObj(GameObject obj)
        {
            throw new NotImplementedException();
        }

        internal static void NotifyGfxMoveControlStart(GameObject targetObj, int skillId, bool v)
        {
            throw new NotImplementedException();
        }

        internal static GameObject GetGameObject(int v)
        {
            throw new NotImplementedException();
        }

        internal static void NotifyGfxUpdatePosition(GameObject obj, float x, float y, float z, int v1, float direction, int v2)
        {
            throw new NotImplementedException();
        }

        internal static Transform FindChildRecursive(Transform transform, string m_AttachPath)
        {
            throw new NotImplementedException();
        }

        internal static void NotifyGfxSummonNpc(GameObject obj, int skillId, int m_NpcTypeId, string m_ModelPrefab, int m_SkillId, float x, float y, float z)
        {
            throw new NotImplementedException();
        }

        internal static void NotifyGfxUpdatePosition(GameObject obj, float x, float y, float z)
        {
            throw new NotImplementedException();
        }
    }
}
