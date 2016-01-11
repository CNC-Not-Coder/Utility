using System;
using System.Collections.Generic;

namespace MyUtility
{
    class ImpactLogicManager
    {
        public enum ImpactLogicId
        {
            ImpactLogic_General = 1,
        }
        private ImpactLogicManager()
        {
            m_ImpactLogics.Add((int)ImpactLogicId.ImpactLogic_General, new ImpactLogic_General());
        }
        public IImpactLogic GetImpactLogic(int id)
        {
            IImpactLogic logic = null;
            if (m_ImpactLogics.ContainsKey(id))
                logic = m_ImpactLogics[id];
            return logic;
        }
        public static ImpactLogicManager Instance
        {
            get { return s_Instance; }
        }

        private Dictionary<int, IImpactLogic> m_ImpactLogics = new Dictionary<int, IImpactLogic>();
        private static ImpactLogicManager s_Instance = new ImpactLogicManager();
    }
}
