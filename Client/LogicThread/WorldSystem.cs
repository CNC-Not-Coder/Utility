using System;

namespace Client
{
    public class WorldSystem
    {
        public void Init()
        {

        }
        public void Tick()
        {

        }
        public static WorldSystem Instance
        {
            get { return s_Instance; }
        }
        private static WorldSystem s_Instance = new WorldSystem();
    }
}
