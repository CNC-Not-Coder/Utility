using System;

namespace MyUtility
{
    /// <summary>
    /// 这是一个场景数据类，服务器和客户端公用，在初始化时，所有数据都要服务器或者客户端赋值
    /// 旨在 能在公用逻辑中得到场景的数据
    /// </summary>
    public class SceneContextInfo
    {
        public NpcManager NpcManager
        {
            get { return m_NpcMgr; }
            set { m_NpcMgr = value; }
        }
        public UserManager UserManager
        {
            get { return m_UserMgr; }
            set { m_UserMgr = value; }
        }
        public CharacterInfo GetCharacterInfoById(int id)
        {
            CharacterInfo info = null;
            if (null != m_NpcMgr)
            {
                info = m_NpcMgr.GetNpcInfo(id);
            }
            if (null == info && null != m_UserMgr)
            {
                info = m_UserMgr.GetUserInfo(id);
            }
            return info;
        }
        private NpcManager m_NpcMgr = null;
        private UserManager m_UserMgr = null;
    }
}
