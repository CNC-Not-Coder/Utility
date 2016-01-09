using System;
using UnityEngine;

namespace Client
{
    public class ResourceSystem
    {
        internal static GameObject NewObject(string prefab, float liveTime)
        {
            UnityEngine.Object res = Resources.Load(prefab);
            GameObject ret = GameObject.Instantiate(res) as GameObject;
            GameObject.Destroy(ret, liveTime);
            return ret;
        }

        internal static void RecycleObject(GameObject effect)
        {
            throw new NotImplementedException();
        }

        internal static UnityEngine.Object GetSharedResource(string prefab)
        {
            return Resources.Load(prefab);
        }
    }
}
