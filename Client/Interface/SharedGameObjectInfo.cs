using System;
using System.Collections.Generic;

namespace Client
{
    public class SharedGameObjectInfo
    {
        public int m_ActorId = 0;
        public int m_LogicObjectId = 0;
        public int CampId = 0;
        public int GfxStateFlag = 0;
        public int SummonOwnerActorId = -1;
        public int SummonOwnerSkillId = -1;
        public List<int> Summons = new List<int>();
    }
}
