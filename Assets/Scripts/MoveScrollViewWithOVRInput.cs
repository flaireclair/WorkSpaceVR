using System;
using UnityEngine;

namespace ListView
{
    public class MoveScrollViewWithOVRInput : ListViewController
    {
        public float scrollSpeed = 10f;

        private void Update()
        {
            if (OVRInput.Get(OVRInput.RawButton.LThumbstickUp) || OVRInput.Get(OVRInput.RawButton.RThumbstickUp))
            {
                Debug.Log("アナログスティックを上に倒した");
                scrollOffset -= scrollSpeed * Time.deltaTime;
            }
            if (OVRInput.Get(OVRInput.RawButton.LThumbstickDown) || OVRInput.Get(OVRInput.RawButton.RThumbstickDown))
            {
                Debug.Log("アナログスティックを下に倒した");
                scrollOffset += scrollSpeed * Time.deltaTime;
            }
        }
        private void LateUpdate()
        {
            ViewUpdate();
        }

        public void AddDataType(string name)
        {
            Array.Resize(ref data, data.Length + 1);
            Debug.Log(data.Length);
            data[data.Length - 1] = new ListViewItemInspectorData();
            data[data.Length - 1].template = "Button";
        }
    }
}