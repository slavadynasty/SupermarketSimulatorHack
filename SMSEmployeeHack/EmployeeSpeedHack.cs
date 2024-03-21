using System;
using System.Collections;
using System.Reflection;
using UnityEngine;

namespace SMSEmployeeHack
{
    public class EmployeeSpeedHack : MonoBehaviour
    {
        private readonly Rect _helloRect = new Rect(10, 10, 200, 40);
        private string _hackStatus;
        
        #region AutomatedCheckout

        private AutomatedCheckout[] _automatedCheckout;
        private FieldInfo _scanningInterval;
        
        private readonly Rect _speedSliderRect = new Rect(10, 30, 200, 40);
        private readonly Rect _speedLabelRect = new Rect(10, 40, 200, 40);
        private float _hackScanningSpeed = 0.5f;

        #endregion

        public EmployeeSpeedHack()
        {
            Start();
        }
        
        private void OnGUI()
        {
            GUI.Label(_helloRect, _hackStatus);

            if (_scanningInterval == null) return;
            
            _hackScanningSpeed = GUI.HorizontalSlider(_speedSliderRect, _hackScanningSpeed, 0.2f, 2f);
            GUI.Label(_speedLabelRect, $"Current Speed: {_hackScanningSpeed:F1}");
        }

        private IEnumerator SemiUpdate()
        {
            while (true)
            {
                if (_automatedCheckout == null)
                {
                    _automatedCheckout = FindObjectsOfType<AutomatedCheckout>();
                }
                
                if (_automatedCheckout.Length == 0)
                {
                    _automatedCheckout = FindObjectsOfType<AutomatedCheckout>();
                }
                
                yield return new WaitForSeconds(1);

                if (_automatedCheckout == null) continue;
                if (_automatedCheckout.Length == 0) continue;

                foreach (var checkout in _automatedCheckout)
                {
                    _scanningInterval?.SetValue(checkout, _hackScanningSpeed);
                }
            }
            // ReSharper disable once IteratorNeverReturns
        }

        private void Start()
        {
            try
            {
                #region AutomatedCheckout

                _scanningInterval = typeof(AutomatedCheckout).GetField("m_ScanningInterval",
                    BindingFlags.NonPublic | BindingFlags.Instance);

                StartCoroutine(SemiUpdate());
                
                #endregion
                
                _hackStatus = "Cashier Speed Hack Activated!";
            }
            catch (Exception e)
            {
                _hackStatus = e.Message;
            }
        }
    }
}