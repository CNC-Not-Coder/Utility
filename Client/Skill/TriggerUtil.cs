using System;
using System.Collections.Generic;
using MyUtility;
using UnityEngine;

namespace Client.SkillTrigger
{
    /// <summary>
    /// 在这里封装LogicSystem和GfxSystem的接口
    /// LogicSystem和GfxSystem只提供基础接口
    /// 这里全是对基础接口的封装
    /// </summary>
    class TriggerUtil
    {
        internal static StateImpact ParseStateImpact(CallData stCall)
        {
            throw new NotImplementedException();
        }

        internal static GameObject DrawCircle(Vector3 center, float m_Range, Color m_Color)
        {
            throw new NotImplementedException();
        }

        internal static float ConvertToSecond(long delta)
        {
            throw new NotImplementedException();
        }

        internal static GameObject GetCameraObj()
        {
            throw new NotImplementedException();
        }

        internal static void ControlCamera(bool v)
        {
            throw new NotImplementedException();
        }

        internal static object GetObjFaceDir(GameObject source)
        {
            throw new NotImplementedException();
        }

        internal static GameObject GetFinalOwner(GameObject source, int skillid, out int final_skill_id)
        {
            throw new NotImplementedException();
        }

        internal static float GetHeightWithGround(GameObject obj)
        {
            throw new NotImplementedException();
        }

        internal static List<GameObject> FindTargetInSector(Vector3 center, float m_Radius, Vector3 forward, Vector3 position, float m_Degree)
        {
            throw new NotImplementedException();
        }

        internal static List<GameObject> FiltEnimy(GameObject obj, List<GameObject> area_objects)
        {
            throw new NotImplementedException();
        }

        internal static GameObject GetObjectByPriority(GameObject obj, List<GameObject> filted_objects, float m_DistancePriority, float m_DegreePriority, float m_Radius, float m_Degree)
        {
            throw new NotImplementedException();
        }

        internal static void SetCameraFollowEnable(bool m_IsEnable)
        {
            throw new NotImplementedException();
        }

        internal static BeHitState GetBeHitStateFromStr(string v)
        {
            throw new NotImplementedException();
        }

        internal static Transform GetChildNodeByName(GameObject obj, string m_Bone)
        {
            throw new NotImplementedException();
        }

        internal static void UpdateObjTransform(GameObject obj)
        {
            throw new NotImplementedException();
        }

        internal static void UpdateObjPosition(GameObject obj)
        {
            throw new NotImplementedException();
        }

        internal static void MoveObjTo(GameObject obj, Vector3 motion_pos)
        {
            throw new NotImplementedException();
        }

        internal static void SetObjVisible(GameObject effectObj, bool v)
        {
            throw new NotImplementedException();
        }

        internal static void ChangeDir(GameObject obj, float direction)
        {
            Vector3 rotate = new Vector3(0, direction * 180 / Mathf.PI, 0);
            obj.transform.eulerAngles = rotate;
            LogicSystem.NotifyGfxUpdatePosition(obj, obj.transform.position.x, obj.transform.position.y,
                                                obj.transform.position.z, 0, direction, 0);
        }
        internal static void ChangeDir(GameObject obj, Vector3 dir)
        {
            dir.y = 0;
            obj.transform.forward = dir;
            Vector3 rotate = obj.transform.rotation.eulerAngles;
            LogicSystem.NotifyGfxUpdatePosition(obj, obj.transform.position.x, obj.transform.position.y,
                                                obj.transform.position.z, 0, rotate.y * Mathf.PI / 180, 0);
        }

        internal static void MoveChildToNode(GameObject obj, string m_ChildName, string m_NodeName)
        {
            throw new NotImplementedException();
        }
    }
}
