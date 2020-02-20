using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Voice.Unity;

public class PhotonVoiceMute : MonoBehaviour {
    Recorder recorder;
	// Use this for initialization
	void Start () {
		
	}

    public void SetRecoder(Recorder _recoder)
    {
        recorder = _recoder;
    }

    public void Mute()
    {
        Debug.Log("今のTransmit:" + recorder.TransmitEnabled);
        recorder.TransmitEnabled = !recorder.TransmitEnabled;
        Debug.Log("変更後のTransmit:" + recorder.TransmitEnabled);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
