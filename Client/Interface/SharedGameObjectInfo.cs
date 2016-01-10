using System;
using System.Collections.Generic;

namespace Client
{
    public class SharedGameObjectInfo
    {
        public int m_ActorId = 0;
        public int m_LogicObjectId = 0;
        public int CampId = 0;
        public float Blood = 0;

        public int GfxStateFlag = 0;

        public int SummonOwnerActorId = -1;
        public int SummonOwnerSkillId = -1;
        public List<int> Summons = new List<int>();
        public bool DataChangedByGfx = false;

        public float FaceDir = 0;
        public float X = 0;
        public float Y = 0;
        public float Z = 0;

        //用来解决Logic层和技能效果同时控制动画时的变量
        public bool IsSkillGfxAnimation = false;
        public bool IsImpactGfxAnimation = false;
        //用来解决Walking和技能效果同时修改位置等信息的变量
        public bool IsSkillGfxMoveControl = false;
        public bool IsImpactGfxMoveControl = false;
        public bool IsSkillGfxRotateControl = false;
        public bool IsImpactGfxRotateControl = false;
        public bool IsGfxAnimation
        {
            get { return IsSkillGfxAnimation || IsImpactGfxAnimation; }
        }
        public bool IsGfxMoveControl
        {
            get { return IsSkillGfxMoveControl || IsImpactGfxMoveControl; }
        }
        public bool IsGfxRotateControl
        {
            get { return IsSkillGfxRotateControl || IsImpactGfxRotateControl; }
        }
    }
}
