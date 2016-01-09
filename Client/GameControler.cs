using System;

namespace Client
{
    class GameControler
    {
        public static bool IsPaused
        {
            get { return s_IsPaused; }
        }
        public void Init()
        {
            LogicSystem.SetLogicInvoker(s_LogicThread);
            s_IsInited = true;
        }
        public static void StartLogic()
        {
            s_LogicThread.Start();
        }
        public static void PauseLogic(bool isPause)
        {
            s_IsPaused = isPause;
        }
        public static void StopLogic()
        {
            s_LogicThread.Stop();
        }
        public void Tick()
        {
            GfxSystem.Instance.Tick();
        }

        private static GameLogicThread s_LogicThread = new GameLogicThread();
        private static bool s_IsInited = false;
        private static bool s_IsPaused = false;
    }
}
