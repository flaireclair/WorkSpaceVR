using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Voice.Unity;

public class PhotonVoiceSet : MonoBehaviour {

	// Use this for initialization
	void Start () {
        var pv = GetComponent<PhotonView>();
        if(!pv.IsMine)
        {
            return;
        }
        var photonvoice = GetComponent<Recorder>();
        var Mute = GameObject.FindGameObjectWithTag("Player").GetComponent<PhotonVoiceMute>();
        Mute.SetRecoder(photonvoice);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
