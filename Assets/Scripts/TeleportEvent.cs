using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public enum EEventType : byte
{
    Teleport = 199,
}

public class TeleportEvent : MonoBehaviourPun
{
    [SerializeField]
    GameObject ovr_Rig;

    [SerializeField]
    GameObject centerCamera;
    
    public static RaiseEventOptions option = new RaiseEventOptions()
    {
        TargetActors = null,
        Receivers = ReceiverGroup.All,
    };
    public static SendOptions sendOptions = new SendOptions { Reliability = true };
void Awake()
    {
        TransformSerializer.Register();
    }

    public void Teleport(Transform tran)
    {
        PhotonNetwork.RaiseEvent((byte)EEventType.Teleport, tran, option, sendOptions);
    }

    private void OnRaiseEvent(byte i_eventcode, object i_content, int i_senderid)
    {
        string eventMessage = null;

        var eventType = (EEventType)i_eventcode;
        switch (eventType)
        {
            case EEventType.Teleport:
                eventMessage = string.Format("[{0}] Transform - Sender({1})", eventType, i_senderid);
                break;
            default:
                break;
        }

        if (!string.IsNullOrEmpty(eventMessage))
        {
            Debug.Log(eventMessage);
        }

        if(PhotonNetwork.LocalPlayer != PhotonNetwork.MasterClient)
        { 
            Vector3 ovr_Rig_Pos = ovr_Rig.transform.position;
            Vector3 centerCamera_Pos = centerCamera.transform.position;

            if (ovr_Rig.transform != null)
            {
                ovr_Rig.transform.position = (i_content as Transform).position;
                ovr_Rig.transform.position += new Vector3(ovr_Rig_Pos.x - centerCamera_Pos.x, 0, ovr_Rig_Pos.z - centerCamera_Pos.z);
            }
        }
    }
}
    