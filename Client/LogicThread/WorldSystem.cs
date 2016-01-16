using System;
using MyUtility;
using System.Collections.Generic;

namespace Client
{
    public class WorldSystem
    {
        public static WorldSystem Instance
        {
            get { return s_Instance; }
        }
        public void Init()
        {

        }
        public void Tick()
        {
            TickImpact();
        }
        public CharacterInfo GetCharacterById(int id)
        {
            CharacterInfo obj = null;
            if (null != m_NpcMgr)
                obj = m_NpcMgr.GetNpcInfo(id);
            if (null != m_UserMgr && null == obj)
                obj = m_UserMgr.GetUserInfo(id);
            return obj;
        }
        
        private void TickImpact()
        {
            for (LinkedListNode<NpcInfo> linkNode = m_NpcMgr.Npcs.FirstValue; null != linkNode; linkNode = linkNode.Next)
            {
                CharacterInfo character = linkNode.Value;
                ImpactSystem.Instance.Tick(character);
            }

            for (LinkedListNode<UserInfo> linkNode = m_UserMgr.Users.FirstValue; null != linkNode; linkNode = linkNode.Next)
            {
                CharacterInfo character = linkNode.Value;
                ImpactSystem.Instance.Tick(character);
            }
        }

        private static WorldSystem s_Instance = new WorldSystem();
        private UserManager m_UserMgr = new UserManager();
        private NpcManager m_NpcMgr = new NpcManager();
    }
}
