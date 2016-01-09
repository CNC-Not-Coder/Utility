using System;
using MyUtility;

namespace Client
{
    public class GameLogicThread : MyThread
    {
        protected override void OnStart()
        {
        }

        protected override void OnTick()
        {
            //这里是在逻辑线程执行的tick，渲染线程的在GameControler.cs:Tick里。
            try
            {
                TimeUtility.SampleClientTick();

                long curTime = TimeUtility.GetLocalMilliseconds();
                if (m_LastLogTime + 10000 < curTime)
                {
                    m_LastLogTime = curTime;

                    if (this.CurActionNum > 10)
                    {
                        Logger.Info("LogicThread.Tick actionNum {0}", this.CurActionNum);
                    }

                    DebugPoolCount((string msg) => {
                        Logger.Info("LogicActionQueue {0}", msg);
                    });
                }

                if (!GameControler.IsPaused)
                {
                    WorldSystem.Instance.Tick();
                }
            }
            catch (Exception ex)
            {
                Logger.Error("GameLogicThread.Tick throw Exception:{0}\n{1}", ex.Message, ex.StackTrace);
            }
        }

        protected override void OnQuit()
        {
        }

        private long m_LastLogTime = 0;
    }
}
