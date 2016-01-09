﻿using System;
using UnityEngine;
using MyUtility;

namespace Client.SkillTrigger
{
    public class StopCurSectionTrigger : AbstractSkillTriger
    {
        public override ISkillTriger Clone()
        {
            StopCurSectionTrigger copy = new StopCurSectionTrigger();
            copy.m_StartTime = m_StartTime;
            return copy;
        }

        public override void Reset()
        {
        }

        protected override void Load(CallData callData)
        {
            if (callData.GetParamNum() >= 1)
            {
                m_StartTime = long.Parse(callData.GetParamId(0));
            }
        }

        public override bool Execute(object sender, SkillInstance instance, long delta, long curSectionTime)
        {
            if (curSectionTime < m_StartTime)
            {
                return true;
            }
            GameObject obj = sender as GameObject;
            if (obj == null)
            {
                return false;
            }
            instance.StopCurSection();
            return false;
        }
    }
}
