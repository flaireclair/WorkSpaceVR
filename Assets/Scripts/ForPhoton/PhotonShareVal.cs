using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

/// <summary>
/// Photonのデータ送受信
/// </summary>
/// 
public class PhotonShareVal : MonoBehaviourPun
{

    public int hours;
    public int days;
    public int progress;
    public bool send;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }


    /// <summary>
    /// Photonでの値の送受信
    /// 今回は時間のデータ
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="info"></param>
    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            //データの送信
            stream.SendNext(hours);
            stream.SendNext(days);
            stream.SendNext(progress);
            stream.SendNext(send);
        }
        else
        {
            //データの受信
            this.hours = (int)stream.ReceiveNext();
            this.days = (int)stream.ReceiveNext();
            this.progress = (int)stream.ReceiveNext();
            this.send = (bool)stream.ReceiveNext();
        }
    }
}
