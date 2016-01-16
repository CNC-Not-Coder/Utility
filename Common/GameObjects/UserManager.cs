

using System;

namespace MyUtility
{
    public class UserManager
    {
        public LinkedListDictionary<int, UserInfo> Users
        {
            get { return m_Users; }
        }
        public UserInfo GetUserInfo(int id)
        {
            UserInfo info;
            m_Users.TryGetValue(id, out info);
            return info;
        }
        private LinkedListDictionary<int, UserInfo> m_Users = new LinkedListDictionary<int, UserInfo>();
    }
}
