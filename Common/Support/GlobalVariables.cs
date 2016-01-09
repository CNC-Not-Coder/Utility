using System;

namespace MyUtility
{
    public class GlobalVariables
    {
        public bool IsClient { get; set; }
        public static GlobalVariables Instance
        {
            get
            {
                return s_Instance;
            }
        }
        private static GlobalVariables s_Instance = new GlobalVariables();
    }
}
