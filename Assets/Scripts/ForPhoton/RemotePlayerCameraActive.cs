using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RemotePlayerCameraActive : MonoBehaviour 
{
    PhotonView pv;
    [SerializeField]GameObject go;
	// Use this for initialization
	void Start () {
        pv = GetComponent<PhotonView>();
        if (pv.IsMine)
            go.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if (pv.IsMine)
            transform.rotation = Camera.main.transform.rotation;
	}
}
