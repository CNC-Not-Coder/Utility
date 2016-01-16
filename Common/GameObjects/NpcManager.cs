using System;

namespace MyUtility
{
    public class NpcManager
    {
        public LinkedListDictionary<int, NpcInfo> Npcs
        {
            get { return m_Npcs; }
        }
        public CharacterInfo GetNpcInfo(int id)
        {
            NpcInfo npc;
            m_Npcs.TryGetValue(id, out npc);
            return npc;
        }
        private LinkedListDictionary<int, NpcInfo> m_Npcs = new LinkedListDictionary<int, NpcInfo>();
    }
}
