using MyUtility;
using System.Collections.Generic;
using UnityEngine;

namespace Client.SkillTrigger
{
    public enum BeHitState
    {
        kDefault = 0,
        kStand = 1,
        kStiffness = 2,
        kLauncher = 3,
        kKnockDown = 4,
    }

    public class StateImpact
    {
        public BeHitState m_State;
        public List<ImpactData> m_Impacts = new List<ImpactData>();
    }

    public class ImpactData
    {
        public int ImpactId;
        public int RemainTime;
    }

    public class SkillDamageManager
    {
        public SkillDamageManager(GameObject owner)
        {
            m_Owner = owner;
        }

        public GameObject GetOwner()
        {
            return m_Owner;
        }

        public bool IsContainObject(GameObject obj)
        {
            foreach (GameObject item_obj in m_DamagedObjects)
            {
                if (item_obj == obj)
                {
                    return true;
                }
            }
            return false;
        }

        public bool AddDamagedObject(GameObject obj)
        {
            if (!IsEnemy(m_Owner, obj))
            {
                return false;
            }
            if (!IsContainObject(obj))
            {
                m_IsDamagedEnemy = true;
                m_DamagedObjects.Add(obj);
                return true;
            }
            return false;
        }

        public void ClearDamagePoool()
        {
            m_DamagedObjects.Clear();
        }

        public void RemoveGameObject(GameObject obj)
        {
            m_DamagedObjects.Remove(obj);
        }

        public bool IsDamagedEnemy
        {
            get { return m_IsDamagedEnemy; }
            set { m_IsDamagedEnemy = value; }
        }

        public static bool IsEnemy(GameObject obj, GameObject other)
        {
            SharedGameObjectInfo obj_info = LogicSystem.GetSharedGameObjectInfo(obj);
            SharedGameObjectInfo other_info = LogicSystem.GetSharedGameObjectInfo(other);
            if (obj_info == null || other_info == null)
            {
                return false;
            }
            if (obj_info.CampId != other_info.CampId)
            {
                return true;
            }
            return false;
        }

        public static BeHitState GetBeHitState(GameObject obj)
        {
            SharedGameObjectInfo objinfo = LogicSystem.GetSharedGameObjectInfo(obj);
            if (objinfo == null)
            {
                return BeHitState.kDefault;
            }
            switch (objinfo.GfxStateFlag)
            {
                case (int)GfxCharacterState_Type.HitFly:
                    return BeHitState.kLauncher;
                case (int)GfxCharacterState_Type.KnockDown:
                    return BeHitState.kKnockDown;
                case (int)GfxCharacterState_Type.Stiffness:
                    return BeHitState.kStiffness;
                default:
                    return BeHitState.kStand;
            }
        }

        public void SendImpactToObject(GameObject source, GameObject target, Dictionary<BeHitState, StateImpact> stateimpacts, int skillid)
        {
            if (stateimpacts == null)
            {
                return;
            }
            BeHitState state = GetBeHitState(target);
            StateImpact stateimpact = null;
            if (stateimpacts.ContainsKey(state))
            {
                stateimpact = stateimpacts[state];
            }
            else if (stateimpacts.ContainsKey(BeHitState.kDefault))
            {
                stateimpact = stateimpacts[BeHitState.kDefault];
            }
            if (stateimpact == null)
            {
                return;
            }
            int final_skill_id = -1;
            GameObject damageOwner = TriggerUtil.GetFinalOwner(source, skillid, out final_skill_id);
            //Debug.Log("------------send impact to object " + target.name);
            foreach (ImpactData im in stateimpact.m_Impacts)
            {
                LogicSystem.NotifyGfxHitTarget(damageOwner, im.ImpactId, target, 1, final_skill_id,
                                                        im.RemainTime, source.transform.position,
                                                        TriggerUtil.GetObjFaceDir(source));
            }
        }

        public void SendImpactToObject(GameObject source, GameObject target, int impactid, int remaintime, int skillid)
        {
            int final_skill_id = skillid;
            GameObject damageOwner = TriggerUtil.GetFinalOwner(source, skillid, out final_skill_id);

            LogicSystem.NotifyGfxHitTarget(damageOwner, impactid, target, 1, final_skill_id,
                                                    remaintime, source.transform.position,
                                                    TriggerUtil.GetObjFaceDir(source));

        }

        private List<GameObject> m_DamagedObjects = new List<GameObject>();
        private bool m_IsDamagedEnemy = false;
        private GameObject m_Owner;
    }
}
