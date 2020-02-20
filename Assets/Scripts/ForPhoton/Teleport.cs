namespace VRTK.Prefabs.CameraRig.UnityXRCameraRig.Input
{
    using UnityEngine;
    using Malimbe.PropertySerializationAttribute;
    using Malimbe.XmlDocumentationAttribute;
    using Zinnia.Action;
    using UnityEngine.UI;

    /// <summary>
    /// Listens for the specified key state and emits the appropriate action.
    /// </summary>
    public class Teleport : MonoBehaviour
    {
        private bool is_clicked = false;

        [SerializeField]
        GameObject ovr_Rig;

        [SerializeField]
        GameObject centerCamera;

        [SerializeField]
        Transform warpPoint;

        public void OnClick()
        {
            Debug.Log(true);
            is_clicked = true;
        }

        void Update()
        {
            Vector3 ovr_Rig_Pos = ovr_Rig.transform.position;
            Vector3 centerCamera_Pos = centerCamera.transform.position;

            if (is_clicked)
            {
                ovr_Rig.transform.position = warpPoint.position;
                ovr_Rig.transform.position += new Vector3(ovr_Rig_Pos.x - centerCamera_Pos.x, 0, ovr_Rig_Pos.z - centerCamera_Pos.z);
            }
            is_clicked = false;
        }
    }
}
