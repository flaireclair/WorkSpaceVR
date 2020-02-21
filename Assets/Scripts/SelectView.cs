using System;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectView : MonoBehaviour
{
    public GameObject myCam = new GameObject();
    public static GameObject mainCam;
    public static GameObject returnButton;
    public static GameObject userCanvas;
    public static GameObject menuButton;
    public static GameObject closeButton;
    public static GameObject confirmButton;
    public static GameObject confirmNowPlayerButton;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.AddComponent<Button>().onClick.AddListener(ChooseCamera);
        gameObject.GetComponent<Button>().onClick.AddListener(() => userCanvas.SetActive(false));
        gameObject.GetComponent<Button>().onClick.AddListener(() => closeButton.SetActive(false));
        gameObject.GetComponent<Button>().onClick.AddListener(() => menuButton.SetActive(true));
        gameObject.GetComponent<Button>().onClick.AddListener(() => confirmButton.SetActive(false));
        gameObject.GetComponent<Button>().onClick.AddListener(() => confirmNowPlayerButton.SetActive(true));
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space)) Debug.Log(gameObject.name + " : " + NetWork_02.cam[myCam.cameraID]);
    }

    //スクリプトがアタッチされているViewのカメラを検索
    //メインカメラのCameraVal.objに指定したカメラをアタッチ
    //CanvasをSetActive(false)

    public void ChooseCamera()
    {
        //mainCam.GetComponent<CameraVal>().obj = NetWork_02.cam[myCam.cameraID];
        returnButton.SetActive(true);
        transform.root.root.gameObject.SetActive(false);
        //PhotonShareVal.myProp.key = "#0" + myCam.playerID.ToString() + " ";
    }

}
