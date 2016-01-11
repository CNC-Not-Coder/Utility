using System;

namespace MyUtility
{
    public enum ImpactType
    {
        BUFF = 1,    // continuous impact 持续作用，需要走Tick
        INSTANT = 2, // instant impact  直接作用，不需要走Tick
    }
    public class ImpactLogicData
    {
        public int ImpactId = -1;//表格Id
        public int ImpactLogicId = -1;//逻辑Id
        public int ImpactType = -1;//枚举ImpactType
        public int BuffDataId = -1;//Buff效果，用来提升攻击力等属性
        public int ImpactTime = 0;//持续时间
        public int ImpactGfxLogicId = -1;//表现效果，击飞，硬直等
    }
    public class ImpactInfo
    {
        public ImpactLogicData ConfigData = null;

        public int m_ImpactId = -1;//表格Id
        public int m_ImpactType = -1;//枚举ImpactType
        public int m_BuffDataId = -1;//Buff效果，用来提升攻击力等属性
        public int m_ImpactDuration = 0;//持续时间
        
        public long m_StartTime = 0;
        public bool m_IsActivated = false;//只有为True才会走逻辑
        public bool m_IsGfxControl = false;//ImpactGfx效果是否结束，目前用于是否该在Tick移除Impact，一般情况下等Gfx效果结束才移除
        public int m_ImpactSenderId = -1; //这个Impact是谁发送给自己的
        public bool m_HasEffectApplyed = false;//标记量，用于计算间隔伤害

    }
}
