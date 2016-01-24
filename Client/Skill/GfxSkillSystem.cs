using UnityEngine;
using MyUtility;
using System.Collections.Generic;
using System;

namespace Client
{
    public class GfxSkillSystem
    {
        private class SkillInstanceInfo
        {
            public int m_SkillId;
            public SkillInstance m_SkillInstance;
            public bool m_IsUsed;
        }
        private class SkillLogicInfo
        {
            public GameObject Sender
            {
                get { return m_Sender; }
            }
            public int SkillId
            {
                get { return m_SkillInfo.m_SkillId; }
            }
            public SkillInstance SkillInst
            {
                get { return m_SkillInfo.m_SkillInstance; }
            }
            public SkillInstanceInfo Info
            {
                get { return m_SkillInfo; }
            }

            public SkillLogicInfo(GameObject obj, SkillInstanceInfo info)
            {
                m_Sender = obj;
                m_SkillInfo = info;
            }

            private GameObject m_Sender;
            private SkillInstanceInfo m_SkillInfo;
        }
        public static GfxSkillSystem Instance
        {
            get { return s_Instance; }
        }
        public void Tick()
        {
            int ct = m_SkillLogicInfos.Count;
            long delta = (long)(Time.deltaTime * 1000 * 1000);
            for (int ix = ct - 1; ix >= 0; --ix)
            {
                SkillLogicInfo info = m_SkillLogicInfos[ix];
                bool exist = GfxSystem.Instance.ExistGameObject(info.Sender);
                if (exist)
                {
                    info.SkillInst.Tick(info.Sender, delta);
                }
                if (!exist || info.SkillInst.IsFinished)
                {
                    StopSkillInstance(info, false);
                }
            }
        }
        public void StartSkill(int actorId, int skillId)
        {
            GameObject obj = LogicSystem.GetGameObject(actorId);
            if (null != obj)
            {
                SkillLogicInfo logicInfo = m_SkillLogicInfos.Find(info => info.Sender == obj && info.SkillId == skillId);
                if (logicInfo != null)
                {
                    return;
                }
                SkillInstanceInfo inst = NewSkillInstance(skillId);
                if (null != inst)
                {
                    m_SkillLogicInfos.Add(new SkillLogicInfo(obj, inst));
                }
                else
                {
                    LogicSystem.NotifyGfxStopSkill(obj, skillId);
                    return;
                }

                logicInfo = m_SkillLogicInfos.Find(info => info.Sender == obj && info.SkillId == skillId);
                if (null != logicInfo)
                {
                    LogicSystem.NotifyGfxAnimationStart(obj, true);
                    logicInfo.SkillInst.Start(logicInfo.Sender);
                }
            }
        }
        private void StopSkillInstance(SkillLogicInfo info, bool isInterrupt)
        {
            if (!isInterrupt)
            {
                info.SkillInst.OnSkillStop(info.Sender, 0);
            }
            else
            {
                info.SkillInst.OnInterrupt(info.Sender, 0);
            }

            if (info.SkillInst.IsControlMove)
            {
                LogicSystem.NotifyGfxMoveControlFinish(info.Sender, info.SkillId, true);
                info.SkillInst.IsControlMove = false;
            }
            LogicSystem.NotifyGfxAnimationFinish(info.Sender, true);
            LogicSystem.NotifyGfxStopSkill(info.Sender, info.SkillId);

            RecycleSkillInstance(info.Info);

            m_SkillLogicInfos.Remove(info);
        }
        private SkillInstanceInfo NewSkillInstance(int skillId)
        {
            SkillInstanceInfo instInfo = GetUnusedSkillInstanceInfoFromPool(skillId);
            if (null == instInfo)
            {
                string filePath = "";//TODO: 读表 HomePath.GetAbsolutePath(FilePathDefine_Client.C_SkillDslPath + SkillDataFile);
                if (string.IsNullOrEmpty(filePath))
                {
                    SkillConfigManager.Instance.LoadSkillIfNotExist(skillId, filePath);
                    SkillInstance inst = SkillConfigManager.Instance.NewSkillInstance(skillId);

                    if (inst == null)
                    {
                        Logger.Error("Can't load skill config, skill:{0} !", skillId);
                        return null;
                    }
                    SkillInstanceInfo res = new SkillInstanceInfo();
                    res.m_SkillId = skillId;
                    res.m_SkillInstance = inst;
                    res.m_IsUsed = true;

                    AddSkillInstanceInfoToPool(skillId, res);
                    return res;
                }
                else
                {
                    Logger.Error("Can't find skill config, skill:{0} !", skillId);
                    return null;
                }
            }
            else
            {
                instInfo.m_IsUsed = true;
                return instInfo;
            }
        }

        private void AddSkillInstanceInfoToPool(int skillId, SkillInstanceInfo info)
        {
            if (m_SkillInstancePool.ContainsKey(skillId))
            {
                List<SkillInstanceInfo> infos = m_SkillInstancePool[skillId];
                infos.Add(info);
            }
            else
            {
                List<SkillInstanceInfo> infos = new List<SkillInstanceInfo>();
                infos.Add(info);
                m_SkillInstancePool.Add(skillId, infos);
            }
        }
        private SkillInstanceInfo GetUnusedSkillInstanceInfoFromPool(int skillId)
        {
            SkillInstanceInfo info = null;
            if (m_SkillInstancePool.ContainsKey(skillId))
            {
                List<SkillInstanceInfo> infos = m_SkillInstancePool[skillId];
                int ct = infos.Count;
                for (int ix = 0; ix < ct; ++ix)
                {
                    if (!infos[ix].m_IsUsed)
                    {
                        info = infos[ix];
                        break;
                    }
                }
            }
            return info;
        }
        private void RecycleSkillInstance(SkillInstanceInfo info)
        {
            info.m_SkillInstance.Reset();
            info.m_IsUsed = false;
        }

        

        private static GfxSkillSystem s_Instance = new GfxSkillSystem();
        private List<SkillLogicInfo> m_SkillLogicInfos = new List<SkillLogicInfo>();
        private Dictionary<int, List<SkillInstanceInfo>> m_SkillInstancePool = new Dictionary<int, List<SkillInstanceInfo>>();
    }
}
