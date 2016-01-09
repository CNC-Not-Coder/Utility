using UnityEngine;
using MyUtility;

public class ColliderScript : MonoBehaviour
{
    public void SetOnTriggerEnter(MyAction<Collider> onEnter)
    {
        m_OnTrigerEnter += onEnter;
    }
    public void SetOnTriggerExit(MyAction<Collider> onExit)
    {
        m_OnTrigerExit += onExit;
    }

    public void SetOnDestroy(MyAction onDestroy)
    {
        m_OnDestroy += onDestroy;
    }

    void OnDestroy()
    {
        if (m_OnDestroy != null)
        {
            m_OnDestroy();
        }
    }

    internal void OnTriggerEnter(Collider collider)
    {
        if (null != m_OnTrigerEnter)
        {
            m_OnTrigerEnter(collider);
        }
    }
    internal void OnTriggerExit(Collider collider)
    {
        if (null != m_OnTrigerExit)
        {
            m_OnTrigerExit(collider);
        }
    }

    private MyAction<Collider> m_OnTrigerEnter;
    private MyAction<Collider> m_OnTrigerExit;
    private MyAction m_OnDestroy;
}