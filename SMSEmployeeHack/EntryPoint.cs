using UnityEngine;

namespace SMSEmployeeHack
{
    public static class EntryPoint
    {
        private static GameObject _smsInstance;
        
        public static void Load()
        {
            EmployeeSpeedHack[] oldHacks = Object.FindObjectsOfType<EmployeeSpeedHack>();
            foreach (EmployeeSpeedHack oldHack in oldHacks) Object.DestroyImmediate(oldHack.gameObject);
            
            _smsInstance = new GameObject("SMS_Employee_SpeedHack");
            Object.DontDestroyOnLoad(_smsInstance);

            _smsInstance.AddComponent<EmployeeSpeedHack>();
        }
    }
}