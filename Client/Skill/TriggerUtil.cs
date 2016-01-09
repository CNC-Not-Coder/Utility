using System;
using System.Collections.Generic;
using MyUtility;
using UnityEngine;

namespace Client.SkillTrigger
{
    /// <summary>
    /// 在这里封装LogicSystem和GfxSystem的接口
    /// LogicSystem和GfxSystem只提供基础接口
    /// 这里全是对基础接口的封装以及帮助方法
    /// </summary>
    class TriggerUtil
    {
        internal static StateImpact ParseStateImpact(CallData stCall)
        {
            StateImpact stateimpact = new StateImpact();
            stateimpact.m_State = GetBeHitStateFromStr(stCall.GetParamId(0));
            for (int i = 1; i < stCall.GetParamNum(); i = i + 2)
            {
                ImpactData im = new ImpactData();
                im.ImpactId = int.Parse(stCall.GetParamId(i));
                if (stCall.GetParamNum() > i + 1)
                {
                    im.RemainTime = int.Parse(stCall.GetParamId(i + 1));
                }
                else
                {
                    im.RemainTime = -1;
                }
                stateimpact.m_Impacts.Add(im);
            }
            return stateimpact;
        }

        internal static GameObject DrawCircle(Vector3 center, float radius, Color color, float circle_step = 0.05f)
        {
            GameObject obj = new GameObject();
            LineRenderer linerender = obj.AddComponent<LineRenderer>();
            linerender.SetWidth(0.05f, 0.05f);

            Shader shader = Shader.Find("Particles/Additive");
            if (shader != null)
            {
                linerender.material = new Material(shader);
            }
            linerender.SetColors(color, color);

            float step_degree = Mathf.Atan(circle_step / 2) * 2;
            int count = (int)(2 * Mathf.PI / step_degree);

            linerender.SetVertexCount(count + 1);

            for (int i = 0; i < count + 1; i++)
            {
                float angle = 2 * Mathf.PI / count * i;
                Vector3 pos = center + new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radius;
                linerender.SetPosition(i, pos);
            }
            return obj;
        }

        internal static float ConvertToSecond(long delta)
        {
            return delta / 1000000.0f;
        }

        internal static GameObject GetCameraObj()
        {
            GameObject gfx_root = GameObject.Find("GfxGameRoot");
            return gfx_root;
        }

        internal static void ControlCamera(bool is_control)
        {
            GameObject gfx_root = GameObject.Find("GfxGameRoot");
            if (gfx_root != null)
            {
                if (is_control)
                {
                    gfx_root.SendMessage("BeginShake");
                }
                else
                {
                    gfx_root.SendMessage("EndShake");
                }
            }
        }

        internal static float GetObjFaceDir(GameObject obj)
        {
            return obj.transform.rotation.eulerAngles.y * UnityEngine.Mathf.PI / 180.0f;
        }

        internal static GameObject GetFinalOwner(GameObject source, int skillid, out int final_skillid)
        {
            SharedGameObjectInfo result = null;
            SharedGameObjectInfo si = LogicSystem.GetSharedGameObjectInfo(source);
            final_skillid = skillid;
            int break_protector = 10000;
            while (si != null)
            {
                result = si;
                if (si.SummonOwnerActorId >= 0)
                {
                    final_skillid = si.SummonOwnerSkillId;
                    si = LogicSystem.GetSharedGameObjectInfo(si.SummonOwnerActorId);
                }
                else
                {
                    break;
                }
                if (break_protector-- <= 0)
                {
                    break;
                }
            }
            if (result != null)
            {
                return LogicSystem.GetGameObject(result.m_ActorId);
            }
            else
            {
                return source;
            }
        }

        internal static float GetHeightWithGround(GameObject obj)
        {
            return GetHeightWithGround(obj.transform.position);
        }
        internal static float GetHeightWithGround(Vector3 pos)
        {
            if (Terrain.activeTerrain != null)
            {
                return pos.y - Terrain.activeTerrain.SampleHeight(pos);
            }
            else
            {
                RaycastHit hit;
                Vector3 higher_pos = pos;
                higher_pos.y += 2;
                float RayCastMaxDistance = 50;
                if (Physics.Raycast(higher_pos, -Vector3.up, out hit, RayCastMaxDistance, LayerMask.GetMask("Terrain")))
                {
                    return pos.y - hit.point.y;
                }
                return RayCastMaxDistance;
            }
        }

        internal static List<GameObject> FindTargetInSector(Vector3 center, float radius, Vector3 direction, Vector3 degreeCenter, float degree)
        {
            List<GameObject> result = new List<GameObject>();
            Collider[] colliders = Physics.OverlapSphere(center, radius, 1 << LayerMask.NameToLayer("Character"));
            direction.y = 0;
            foreach (Collider co in colliders)
            {
                GameObject obj = co.gameObject;
                Vector3 targetDir = obj.transform.position - degreeCenter;
                targetDir.y = 0;
                if (Mathf.Abs(Vector3.Angle(targetDir, direction)) <= degree)
                {
                    result.Add(obj);
                }
            }
            return result;
        }

        internal static List<GameObject> FiltEnemy(GameObject source, List<GameObject> list)
        {
            List<GameObject> result = new List<GameObject>();
            foreach (GameObject obj in list)
            {
                if (SkillDamageManager.IsEnemy(source, obj) && !IsObjectDead(obj))
                {
                    result.Add(obj);
                }
            }
            return result;
        }
        internal static bool IsObjectDead(GameObject obj)
        {
            SharedGameObjectInfo si = LogicSystem.GetSharedGameObjectInfo(obj);
            if (si.Blood <= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        internal static GameObject GetObjectByPriority(GameObject source, List<GameObject> list, float distance_priority, float degree_priority, float max_distance, float max_degree)
        {
            GameObject target = null;
            float max_score = -1;
            foreach (GameObject obj in list)
            {
                float distance = (obj.transform.position - source.transform.position).magnitude;
                float distance_score = 1 - distance / max_distance;
                Vector3 targetDir = obj.transform.position - source.transform.position;
                float angle = Vector3.Angle(targetDir, source.transform.forward);
                float degree_score = 1 - angle / max_degree;
                float final_score = distance_score * distance_priority + degree_score * degree_priority;
                if (final_score > max_score)
                {
                    target = obj;
                    max_score = final_score;
                }
            }
            return target;
        }

        internal static void SetCameraFollowEnable(bool m_IsEnable)
        {
            GameObject go = GetCameraObj();
            if (go != null) go.SendMessage("FollowEnable", m_IsEnable, SendMessageOptions.DontRequireReceiver);
        }

        internal static BeHitState GetBeHitStateFromStr(string str)
        {
            BeHitState result = BeHitState.kDefault;
            if (str.Equals("kDefault"))
            {
                result = BeHitState.kDefault;
            }
            else if (str.Equals("kStand"))
            {
                result = BeHitState.kStand;
            }
            else if (str.Equals("kStiffness"))
            {
                result = BeHitState.kStiffness;
            }
            else if (str.Equals("kKnockDown"))
            {
                result = BeHitState.kKnockDown;
            }
            else if (str.Equals("kLauncher"))
            {
                result = BeHitState.kLauncher;
            }
            return result;
        }

        internal static Transform GetChildNodeByName(GameObject gameobj, string name)
        {
            if (gameobj == null)
            {
                return null;
            }
            if (string.IsNullOrEmpty(name))
            {
                return null;
            }
            Transform[] ts = gameobj.transform.GetComponentsInChildren<Transform>();
            foreach (Transform t in ts)
            {
                if (t.name == name)
                {
                    return t;
                }
            }
            return null;
        }

        internal static void UpdateObjTransform(GameObject obj)
        {
            if (obj == null)
            {
                return;
            }
            LogicSystem.NotifyGfxUpdatePosition(obj, obj.transform.position.x, obj.transform.position.y, obj.transform.position.z,
                                                         0, (float)(obj.transform.rotation.eulerAngles.y * Math.PI / 180.0f), 0);
        }

        internal static void UpdateObjPosition(GameObject obj)
        {
            if (obj == null)
            {
                return;
            }
            LogicSystem.NotifyGfxUpdatePosition(obj, obj.transform.position.x, obj.transform.position.y, obj.transform.position.z);
        }

        internal static void MoveObjTo(GameObject obj, Vector3 position)
        {
            CharacterController ctrl = obj.GetComponent<CharacterController>();
            if (null != ctrl)
            {
                ctrl.Move(position - obj.transform.position);
            }
            else
            {
                obj.transform.position = position;
            }
        }

        internal static void SetObjVisible(GameObject obj, bool isShow)
        {
            Renderer[] renders = obj.GetComponentsInChildren<Renderer>();
            foreach (Renderer r in renders)
            {
                r.enabled = isShow;
            }
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

        internal static void MoveChildToNode(GameObject obj, string childname, string nodename)
        {
            Transform child = GetChildNodeByName(obj, childname);
            if (child == null)
            {
                Logger.Info("----not find child! {0} on {1}", childname, obj.name);
                return;
            }
            Transform togglenode = TriggerUtil.GetChildNodeByName(obj, nodename);
            if (togglenode == null)
            {
                Logger.Info("----not find node! {0} on {1}", nodename, obj.name);
                return;
            }
            child.parent = togglenode;
            child.localRotation = Quaternion.identity;
            child.localPosition = Vector3.zero;
        }
    }
}
