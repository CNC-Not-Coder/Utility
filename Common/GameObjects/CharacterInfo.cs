using System;
using System.Collections.Generic;

namespace MyUtility
{
    public class CharacterInfo
    {
        public class CharStateInfo
        {//角色状态量
            public bool BuffChanged = false;//BUFF有改变，需要重新计算属性
        }

        public List<ImpactInfo> GetAllImpact()
        {
            return m_ImpactList;
        }

        public void RemoveImpact(int impactId)
        {
            ImpactInfo oriImpact = GetImpactInfoById(impactId);
            if (oriImpact != null)
            {
                if ((int)ImpactType.BUFF == oriImpact.m_ImpactType)
                {
                    m_CharStateInfo.BuffChanged = true;
                }
                m_ImpactList.Remove(oriImpact);
            }
        }

        public int GetId()
        {
            return m_Id;
        }

        public ImpactInfo GetImpactInfoById(int impactId)
        {
            return m_ImpactList.Find(info => info.m_ImpactId == impactId);
        }

        public void AddImpact(ImpactInfo info)
        {
            ImpactInfo oriImpact = GetImpactInfoById(info.m_ImpactId);
            if (oriImpact == null)
            {
                m_ImpactList.Add(info);
            }
            else
            {
                m_ImpactList.Remove(oriImpact);
                m_ImpactList.Add(info);
            }
            if ((int)ImpactType.BUFF == info.m_ImpactType)
            {
                m_CharStateInfo.BuffChanged = true;
            }
        }

        public SceneContextInfo SceneContext = null;
        private List<ImpactInfo> m_ImpactList = new List<ImpactInfo>(); // 效果容器
        private CharStateInfo m_CharStateInfo = new CharStateInfo();
        private int m_Id = -1;
    }
}
