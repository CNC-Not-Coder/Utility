using System;

namespace Client
{
    /// <summary>
    /// 负责实现逻辑线程LoigcSystem对应接口的实现
    /// 本类所有方法都在逻辑线程中运行
    /// </summary>
    class LogicActionImpl
    {
        internal static void OnGfxHitTarget(int id, int impactId, int targetId, int hitCount, int skillId, int duration, float x, float y, float z, float dir)
        {
            throw new NotImplementedException();
        }

        internal static void OnGfxSummonNpc(int owner_id, int owner_skill_id, int npc_type_id, string modelPrefab, int skillid, float x, float y, float z)
        {
            throw new NotImplementedException();
        }

        internal static void OnGfxSetCrossFadeTime(int id, string fadeTargetAnim, float crossTime)
        {
            throw new NotImplementedException();
        }

        internal static void OnGfxDestroySummonObject(int id)
        {
            throw new NotImplementedException();
        }

        internal static void OnGfxDestroyObj(int id)
        {
            throw new NotImplementedException();
        }

        internal static void OnGfxControlMoveStart(int objId, int id, bool isSkill)
        {
            throw new NotImplementedException();
        }

        internal static void OnGfxControlMoveStop(int objId, int id, bool isSkill)
        {
            throw new NotImplementedException();
        }
    }
}
