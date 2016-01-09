using System;
using UnityEngine;
using MyUtility;

namespace Client.SkillTrigger
{
    /// <summary>
    /// settransform(startime, bone, position, rotate, relaitve_type);
    /// </summary>
    public class SetTransformTrigger : AbstractSkillTriger
    {
        public override ISkillTriger Clone()
        {
            SetTransformTrigger copy = new SetTransformTrigger();
            copy.m_StartTime = m_StartTime;
            copy.m_BoneName = m_BoneName;
            copy.m_Postion = m_Postion;
            copy.m_Rotate = m_Rotate;
            copy.m_RelativeType = m_RelativeType;
            copy.m_IsAttach = m_IsAttach;
            return copy;
        }

        public override void Reset()
        {
        }

        protected override void Load(CallData callData)
        {
            if (callData.GetParamNum() >= 5)
            {
                m_StartTime = long.Parse(callData.GetParamId(0));
                m_BoneName = callData.GetParamId(1);
                if (m_BoneName == " ")
                {
                    m_BoneName = "";
                }
                m_Postion = ScriptableDataUtility.CalcVector3(callData.GetParam(2) as CallData);
                m_Rotate = ScriptableDataUtility.CalcEularRotation(callData.GetParam(3) as CallData);
                m_RelativeType = callData.GetParamId(4);
                m_IsAttach = bool.Parse(callData.GetParamId(5));
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
            switch (m_RelativeType)
            {
                case "RelativeOwner":
                    SetTransformRelativeOwner(obj);
                    break;
                case "RelativeSelf":
                    SetTransformRelativeSelf(obj);
                    break;
                case "RelativeTarget":
                    SetTransformRelativeTarget(obj, instance.CustomDatas.GetData<MoveTargetInfo>());
                    break;
                case "RelativeWorld":
                    obj.transform.position = m_Postion;
                    obj.transform.rotation = m_Rotate;
                    break;
            }
            TriggerUtil.UpdateObjTransform(obj);
            return false;
        }

        private void SetTransformRelativeOwner(GameObject obj)
        {
            SharedGameObjectInfo shareobj = LogicSystem.GetSharedGameObjectInfo(obj);
            if (shareobj != null && shareobj.SummonOwnerActorId >= 0)
            {
                GameObject owner = LogicSystem.GetGameObject(shareobj.SummonOwnerActorId);
                AttachToObject(obj, owner);
            }
            else
            {
                SetTransformRelativeSelf(obj);
            }
        }

        private void SetTransformRelativeTarget(GameObject obj, MoveTargetInfo target_info)
        {
            if (target_info == null)
            {
                return;
            }
            AttachToObject(obj, target_info.Target);
        }

        private void AttachToObject(GameObject obj, GameObject owner)
        {
            Transform parent = TriggerUtil.GetChildNodeByName(owner, m_BoneName);
            if (parent == null)
            {
                parent = owner.transform;
            }
            obj.transform.parent = parent;
            Vector3 world_pos = parent.TransformPoint(m_Postion);
            TriggerUtil.MoveObjTo(obj, world_pos);
            obj.transform.localRotation = m_Rotate;
            if (!m_IsAttach)
            {
                obj.transform.parent = null;
            }
        }

        private void SetTransformRelativeSelf(GameObject obj)
        {
            Vector3 new_pos = obj.transform.TransformPoint(m_Postion);
            TriggerUtil.MoveObjTo(obj, new_pos);
            obj.transform.rotation *= m_Rotate;
        }

        private string m_BoneName;
        private string m_RelativeType;
        private Vector3 m_Postion;
        private Quaternion m_Rotate;
        private bool m_IsAttach;
    }
}
