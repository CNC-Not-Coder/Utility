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
        public static void NotifyGfxSetCrossFadeTime(GameObject obj, string fadeTargetAnim, float fadeTime)
        {
            SharedGameObjectInfo info = GfxSystem.Instance.GetSharedGameObjectInfo(obj);
            if (null != info)
            {
                QueueLogicAction(LogicActionImpl.OnGfxSetCrossFadeTime, info.m_LogicObjectId, fadeTargetAnim, fadeTime);
            }
        }

        public static SharedGameObjectInfo GetSharedGameObjectInfo(GameObject obj)
        {
            return GfxSystem.Instance.GetSharedGameObjectInfo(obj);
        }
        public static SharedGameObjectInfo GetSharedGameObjectInfo(int id)
        {
            return GfxSystem.Instance.GetSharedGameObjectInfo(id);
        }

        public static void NotifyGfxHitTarget(GameObject src, int impactId, GameObject target, int hitCount, int skillId, int duration, Vector3 srcPos, float srcDir)
        {
            SharedGameObjectInfo srcInfo = GfxSystem.Instance.GetSharedGameObjectInfo(src);
            SharedGameObjectInfo targetInfo = GfxSystem.Instance.GetSharedGameObjectInfo(target);
            if (null != srcInfo && null != targetInfo)
            {
                QueueLogicAction(LogicActionImpl.OnGfxHitTarget, srcInfo.m_LogicObjectId, impactId, targetInfo.m_LogicObjectId, hitCount, skillId, duration, srcPos.x, srcPos.y, srcPos.z, srcDir);
            }
            else
            {
                Logger.Info("NotifyGfxHitTarget:{0} {1} {2} {3}, can't find object !", src.name, impactId, target.name, hitCount);
            }
        }
        public static void NotifyGfxMoveControlStart(GameObject obj, int id, bool isSkill)
        {
            SharedGameObjectInfo info = GfxSystem.Instance.GetSharedGameObjectInfo(obj);
            if (null != info)
            {
                if (isSkill)
                {
                    info.IsSkillGfxMoveControl = true;
                    info.IsSkillGfxRotateControl = true;
                }
                else
                {
                    info.IsImpactGfxMoveControl = true;
                    info.IsImpactGfxRotateControl = true;
                }
                QueueLogicAction(LogicActionImpl.OnGfxControlMoveStart, info.m_LogicObjectId, id, isSkill);
            }
            else
            {
                Logger.Info("NotifyGfxMoveControlStart:{0}, can't find object !", obj.name);
            }
        }

        internal static void NotifyGfxAnimationFinish(GameObject sender, bool v)
        {
            throw new NotImplementedException();
        }

        public static void NotifyGfxMoveControlFinish(GameObject obj, int id, bool isSkill)
        {
            SharedGameObjectInfo info = GfxSystem.Instance.GetSharedGameObjectInfo(obj);
            if (null != info)
            {
                if (isSkill)
                {
                    info.IsSkillGfxMoveControl = false;
                    info.IsSkillGfxRotateControl = false;
                }
                else
                {
                    info.IsImpactGfxMoveControl = false;
                    info.IsImpactGfxRotateControl = false;
                }
                QueueLogicAction(LogicActionImpl.OnGfxControlMoveStop, info.m_LogicObjectId, id, isSkill);
            }
            else
            {
                Logger.Info("NotifyGfxMoveControlFinish:{0}, can't find object !", obj.name);
            }
        }

        internal static void NotifyGfxAnimationStart(GameObject obj, bool v)
        {
            throw new NotImplementedException();
        }

        public static void NotifyGfxDestroySummonObject(GameObject obj)
        {
            SharedGameObjectInfo info = GfxSystem.Instance.GetSharedGameObjectInfo(obj);
            if (null != info)
            {
                QueueLogicAction(LogicActionImpl.OnGfxDestroySummonObject, info.m_LogicObjectId);
            }
        }

        public static void NotifyGfxDestroyObj(GameObject obj)
        {
            SharedGameObjectInfo info = GfxSystem.Instance.GetSharedGameObjectInfo(obj);
            if (null != info)
            {
                QueueLogicAction(LogicActionImpl.OnGfxDestroyObj, info.m_LogicObjectId);
            }
        }

        public static GameObject GetGameObject(int id)
        {
            return GfxSystem.Instance.GetGameObject(id);
        }

        public static void NotifyGfxSummonNpc(GameObject obj, int owner_skillid, int npc_type_id, string model, int skillid,
                                          float pos_x, float pos_y, float pos_z)
        {
            SharedGameObjectInfo info = GfxSystem.Instance.GetSharedGameObjectInfo(obj);
            if (null != info)
            {
                QueueLogicAction(LogicActionImpl.OnGfxSummonNpc, info.m_LogicObjectId, owner_skillid, npc_type_id, model, skillid, pos_x, pos_y, pos_z);
            }
        }
        public static void NotifyGfxUpdatePosition(GameObject obj, float x, float y, float z)
        {
            lock (GfxSystem.SyncLock)
            {
                SharedGameObjectInfo info = GfxSystem.Instance.GetSharedGameObjectInfo(obj);
                if (null != info)
                {
                    info.X = x;
                    info.Y = y;
                    info.Z = z;
                    info.DataChangedByGfx = true;//告知需要将位置信息同步给Logic线程
                }
                else
                {
                    Logger.Error("NotifyGfxUpdatePosition:{0} {1} {2} {3}, can't find object !", obj.name, x, y, z);
                }
            }
        }
        public static void NotifyGfxUpdatePosition(GameObject obj, float x, float y, float z, float rx, float ry, float rz)
        {
            lock (GfxSystem.SyncLock)
            {
                SharedGameObjectInfo info = GfxSystem.Instance.GetSharedGameObjectInfo(obj);
                if (null != info)
                {
                    info.X = x;
                    info.Y = y;
                    info.Z = z;
                    info.FaceDir = ry;
                    info.DataChangedByGfx = true;//告知需要将位置信息同步给Logic线程
                }
                else
                {
                    Logger.Error("NotifyGfxUpdatePosition:{0} {1} {2} {3} {4} {5} {6}, can't find object !", obj.name, x, y, z, rx, ry, rz);
                }
            }
        }
    }
}
