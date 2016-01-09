using System;
using System.Collections.Generic;
using MyUtility;
using UnityEngine;

namespace Client.SkillTrigger
{
    /// <summary>
    /// areadamage(start_time,center_x, center_y, center_z, range, is_clear_when_finish[,impact_id,...]) {
    ///   showtip(time, color_r, color_g, color_b);
    ///   stateimpact(statename, impact_id[,impact_id...]); 
    /// };
    /// </summary>
    internal class AreaDamageTriger : AbstractSkillTriger
    {
        public override ISkillTriger Clone()
        {
            AreaDamageTriger triger = new AreaDamageTriger();
            triger.m_StartTime = m_StartTime;
            triger.m_RelativeCenter = m_RelativeCenter;
            triger.m_Range = m_Range;
            triger.m_ImpactList = m_ImpactList;
            triger.m_IsClearWhenFinish = m_IsClearWhenFinish;
            triger.m_IsShowTip = m_IsShowTip;
            triger.m_ShowTime = m_ShowTime;
            triger.m_Color = m_Color;
            foreach (StateImpact impact in m_StateImpacts.Values)
            {
                triger.m_StateImpacts[impact.m_State] = impact;
            }
            return triger;
        }
        public override bool Execute(object sender, SkillInstance instance, long delta, long curSectionTime)
        {
            if (curSectionTime >= m_StartTime)
            {
                GameObject obj = sender as GameObject;
                if (null == obj)
                {
                    return false;
                }
                if (!instance.IsDamageEnable)
                {
                    return false;
                }
                Vector3 center = obj.transform.TransformPoint(m_RelativeCenter);
                Collider[] hits = Physics.OverlapSphere(center, m_Range, 1 << LayerMask.NameToLayer("Character"));
                SkillDamageManager damage_manager = instance.CustomDatas.GetData<SkillDamageManager>();
                if (damage_manager == null)
                {
                    damage_manager = new SkillDamageManager(obj);
                    instance.CustomDatas.AddData<SkillDamageManager>(damage_manager);
                }
                if (m_IsShowTip)
                {
                    GameObject circle = TriggerUtil.DrawCircle(center, m_Range, m_Color);
                    GameObject.Destroy(circle, m_ShowTime / 1000.0f);
                }
                foreach (Collider hit in hits)
                {
                    if (!SkillDamageManager.IsEnemy(obj, hit.gameObject))
                    {
                        continue;
                    }
                    if (!damage_manager.IsContainObject(hit.gameObject))
                    {
                        damage_manager.IsDamagedEnemy = true;
                        damage_manager.SendImpactToObject(obj, hit.gameObject, m_StateImpacts, instance.SkillId);
                        if (!m_IsClearWhenFinish)
                        {
                            damage_manager.AddDamagedObject(hit.gameObject);
                        }
                    }
                }
                if (damage_manager.IsDamagedEnemy)
                {
                    instance.SendMessage("oncollide");
                }
                return false;
            }
            else
            {
                return true;
            }
        }

        protected override void Load(CallData callData)
        {
            int num = callData.GetParamNum();
            if (num >= 6)
            {
                m_StartTime = long.Parse(callData.GetParamId(0));
                m_RelativeCenter.x = float.Parse(callData.GetParamId(1));
                m_RelativeCenter.y = float.Parse(callData.GetParamId(2));
                m_RelativeCenter.z = float.Parse(callData.GetParamId(3));
                m_Range = float.Parse(callData.GetParamId(4));
                m_IsClearWhenFinish = bool.Parse(callData.GetParamId(5));
            }
        }

        protected override void Load(FunctionData funcData)
        {
            CallData callData = funcData.Call;
            if (null != callData)
            {
                Load(callData);

                foreach (ISyntaxComponent statement in funcData.Statements)
                {
                    CallData stCall = statement as CallData;
                    if (null != stCall)
                    {
                        string id = stCall.GetId();
                        if (id == "stateimpact")
                        {
                            StateImpact stateimpact = TriggerUtil.ParseStateImpact(stCall);
                            m_StateImpacts[stateimpact.m_State] = stateimpact;
                        }
                        else if (id == "showtip")
                        {
                            m_IsShowTip = true;
                            m_ShowTime = long.Parse(stCall.GetParamId(0));
                            if (stCall.GetParamNum() >= 4)
                            {
                                float r = float.Parse(stCall.GetParamId(1));
                                float g = float.Parse(stCall.GetParamId(2));
                                float b = float.Parse(stCall.GetParamId(3));
                                m_Color = new Color(r, g, b, 1);
                            }
                        }
                    }
                }
            }
        }

        private Vector3 m_RelativeCenter = Vector3.zero;
        private float m_Range = 0;
        private bool m_IsClearWhenFinish = false;
        private List<int> m_ImpactList = new List<int>();
        private Dictionary<BeHitState, StateImpact> m_StateImpacts = new Dictionary<BeHitState, StateImpact>();
        private bool m_IsShowTip = false;
        private long m_ShowTime = 0;
        private Color m_Color = new Color(0.5f, 1.0f, 0.5f);
    }
}
