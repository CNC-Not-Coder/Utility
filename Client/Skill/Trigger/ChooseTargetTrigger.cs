using System.Collections.Generic;
using MyUtility;
using UnityEngine;

namespace Client.SkillTrigger
{
    public class MoveTargetInfo
    {
        public GameObject Target;
        public float ToTargetDistanceRatio;
        public float ToTargetConstDistance;
    }

    public class ChooseTargetTrigger : AbstractSkillTriger
    {
        public override ISkillTriger Clone()
        {
            ChooseTargetTrigger copy = new ChooseTargetTrigger();
            copy.m_StartTime = m_StartTime;
            copy.m_Center = m_Center;
            copy.m_Radius = m_Radius;
            copy.m_Degree = m_Degree;
            copy.m_DistancePriority = m_DistancePriority;
            copy.m_DegreePriority = m_DegreePriority;
            copy.m_ToTargetDistanceRatio = m_ToTargetDistanceRatio;
            copy.m_ToTargetConstDistance = m_ToTargetConstDistance;
            copy.m_FiltStates.AddRange(m_FiltStates);
            return copy;
        }

        public override void Reset()
        {
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
            Vector3 center = obj.transform.TransformPoint(m_Center);
            List<GameObject> area_objects = TriggerUtil.FindTargetInSector(center, m_Radius,
                                                                           obj.transform.forward,
                                                                           obj.transform.position, m_Degree);
            List<GameObject> filted_objects = FiltStateObjects(TriggerUtil.FiltEnimy(obj, area_objects));
            GameObject target = TriggerUtil.GetObjectByPriority(obj, filted_objects,
                                                                m_DistancePriority, m_DegreePriority,
                                                                m_Radius, m_Degree);

            if (target != null)
            {
                MoveTargetInfo targetinfo = new MoveTargetInfo();
                targetinfo.Target = target;
                targetinfo.ToTargetDistanceRatio = m_ToTargetDistanceRatio;
                targetinfo.ToTargetConstDistance = m_ToTargetConstDistance;
                instance.CustomDatas.AddData<MoveTargetInfo>(targetinfo);
            }
            return false;
        }

        protected override void Load(CallData callData)
        {
            int num = callData.GetParamNum();
            if (num >= 8)
            {
                m_StartTime = long.Parse(callData.GetParamId(0));
                CallData vect_param1 = callData.GetParam(1) as CallData;
                m_Center = ScriptableDataUtility.CalcVector3(vect_param1);
                m_Radius = float.Parse(callData.GetParamId(2));
                m_Degree = float.Parse(callData.GetParamId(3));
                m_DistancePriority = float.Parse(callData.GetParamId(4));
                m_DegreePriority = float.Parse(callData.GetParamId(5));
                m_ToTargetDistanceRatio = float.Parse(callData.GetParamId(6));
                m_ToTargetConstDistance = float.Parse(callData.GetParamId(7));
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
                    string id = stCall.GetId();
                    if (id == "filtstate")
                    {
                        LoadFiltStateConfig(stCall);
                    }
                }
            }
        }

        private void LoadFiltStateConfig(CallData stCall)
        {
            if (stCall.GetParamNum() >= 1)
            {
                BeHitState filtstate = TriggerUtil.GetBeHitStateFromStr(stCall.GetParamId(0));
                if (!IsFiltState(filtstate))
                {
                    m_FiltStates.Add(filtstate);
                }
            }
        }

        private List<GameObject> FiltStateObjects(List<GameObject> objects)
        {
            List<GameObject> result = new List<GameObject>();
            if (objects == null)
            {
                return result;
            }
            foreach (GameObject obj in objects)
            {
                BeHitState state = SkillDamageManager.GetBeHitState(obj);
                if (!IsFiltState(state))
                {
                    result.Add(obj);
                }
            }
            return result;
        }

        private bool IsFiltState(BeHitState state)
        {
            foreach (BeHitState bs in m_FiltStates)
            {
                if (bs == state)
                {
                    return true;
                }
            }
            return false;
        }

        private Vector3 m_Center;
        private float m_Radius;
        private float m_Degree;
        private float m_DistancePriority;
        private float m_DegreePriority;
        private float m_ToTargetDistanceRatio;
        private float m_ToTargetConstDistance;
        private List<BeHitState> m_FiltStates = new List<BeHitState>();
    }
}
