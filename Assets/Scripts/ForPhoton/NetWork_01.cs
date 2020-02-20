using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.Events;
using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using Photon.Realtime;

namespace Com.MyCompany.MyGame
{
    /// <summary>
    /// PhotonのManager
    /// </summary>
    public class NetWork_01 : MonoBehaviourPunCallbacks
    {
        [Header("PhotonのManager")]
        [SerializeField] UnityEvent unityEvent;
        public GameObject oculus;
        public GameObject LeftHand;
        public GameObject RightHand;
        public GameObject textObj;

        [SerializeField] private List<GameObject> DontDestroyObjList;
        [SerializeField] GameObject CenterEyeAnchor;
        private OVRScreenFade fade;

        // ハッシュテーブルを宣言
        private ExitGames.Client.Photon.Hashtable modelProperties;

        public struct Proparty
        {
            public string key;
            public Dictionary<string, int> dic;
            public Proparty(string key, Dictionary<string, int> dic)
            {
                this.key = key;
                this.dic = dic;
            }
        }

        public static Proparty myProp;
        private Proparty alProp;

        void Awake()
        {
            fade = CenterEyeAnchor.GetComponent<OVRScreenFade>();
            foreach(GameObject gameObject in DontDestroyObjList)
            {
                DontDestroyOnLoad(gameObject);
            }
        }

        void Start()
        {
            // Photonに接続する(引数でゲームのバージョンを指定できる)\
            Debug.Log("hey");

            //LoadBalancingClient loadBalancingClient = new LoadBalancingClient(PhotonNetwork.PhotonServerSettings.AppSettings.Server, PhotonNetwork.PhotonServerSettings.AppSettings.AppIdRealtime, PhotonNetwork.PhotonServerSettings.AppSettings.AppVersion, ExitGames.Client.Photon.ConnectionProtocol.Udp);
            //LoadBalancingClient loadBalancingClient = new LoadBalancingClient(ExitGames.Client.Photon.ConnectionProtocol.Udp);
            //loadBalancingClient.AddCallbackTarget(this);
            //loadBalancingClient.Connect();
            PhotonNetwork.GameVersion = "1.0";
            PhotonNetwork.ConnectUsingSettings();
        }

        private void OnConnectedToServer()
        {
            Debug.Log("way");
        }

        // ロビーに入ると呼ばれる
        public override void OnConnectedToMaster()
        {
            Debug.Log("マスターに接続しました。");

            // ルームに入室する
            PhotonNetwork.JoinRandomRoom();
        }

        public void RoomCreate()
        {
            PhotonNetwork.CreateRoom("myRoomName");
        }


        public void Join()
        {
            PhotonNetwork.JoinRandomRoom();
        }

        // ルームに入室すると呼ばれる
        public override void OnJoinedRoom()
        {
            Debug.Log("ルームへ入室しました。");
            //unityEvent.Invoke();

            //fade.FadeOn(1, 0, 0);
            //fade.FadeOn(0, 1, 1);

            GameObject objH = PhotonNetwork.Instantiate("CameraParent", oculus.transform.position, oculus.transform.rotation, 0);
            objH.AddComponent<ObjVal>().obj = oculus;
            DontDestroyOnLoad(objH);
            GameObject objL = PhotonNetwork.Instantiate("LeftHandAnchorPrefab", LeftHand.transform.position, LeftHand.transform.rotation, 0);
            objL.AddComponent<ObjVal>().obj = LeftHand;
            DontDestroyOnLoad(objL);
            GameObject objR = PhotonNetwork.Instantiate("RightHandAnchorPrefab", RightHand.transform.position, RightHand.transform.rotation, 0);
            objR.AddComponent<ObjVal>().obj = RightHand;
            DontDestroyOnLoad(objR);

            textObj.GetComponent<Text>().text = PhotonNetwork.CurrentRoom.Name;

            myProp = new Proparty(PhotonNetwork.LocalPlayer.ToString(),
                                  new Dictionary<string, int>()
                                  {
                                      { "Louver", 0},
                                      { "Light", 0}
                                  });
            alProp = new Proparty("all",
                                  new Dictionary<string, int>()
                                  {
                                      { "Louver", 0},
                                      { "Light", 0}
                                  });
            /*myProp = new Proparty(PhotonNetwork.player.ToString(), // デバッグ
                                  new Dictionary<string, int>()
                                  {
                                      {"Louver", PhotonNetwork.player.ID},
                                      {"Light", PhotonNetwork.player.ID}
                                  }); */
            SceneManager.LoadScene("WorkSpace");
        }

        // ルームの入室に失敗すると呼ばれる
        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            Debug.Log("ルームの入室に失敗しました。");
            Debug.Log("return : " + returnCode + " , " + message);

            // ルームがないと入室に失敗するため、その時は自分で作る
            // 引数でルーム名を指定できる
            PhotonNetwork.CreateRoom("myRoomName");
            Join();
        }

        // ルームのハッシュが送信されたら、送信されたハッシュが入ってくる(Photonの機能で戻り値の型、関数名、引数を一致させると勝手に呼ばれる)
        public void OnPhotonCustomRoomPropertiesChanged(ExitGames.Client.Photon.Hashtable changedRoomHash)
        {
            // 変更されたハッシュを受け取る
            modelProperties = changedRoomHash;
            Proparty tmpProp = myProp;
            foreach (object key in modelProperties.Keys)
            {
                Debug.Log(key.ToString());
                foreach (KeyValuePair<string, int> dic in modelProperties[key] as Dictionary<string, int>)
                {
                    Debug.Log("Value : " + dic.Key + ", " + dic.Value);
                }
                if (key.ToString() == myProp.key) myProp.dic = modelProperties[key] as Dictionary<string, int>;
                else if (key.ToString() == "all") alProp.dic = modelProperties[key] as Dictionary<string, int>;
            }
            Debug.Log(" Key : " + myProp.key);
            /*** モデルを変更する処理 ***/
            foreach (KeyValuePair<string, int> dic in myProp.dic)
            {
                Debug.Log("Value : " + dic.Key + ", " + dic.Value);
                if (dic.Value != tmpProp.dic[dic.Key]) ModelScript.ChangeModel(dic.Key, dic.Value);
            }
            foreach (KeyValuePair<string, int> dic in alProp.dic)
            {
                Debug.Log("Value : " + dic.Key + ", " + dic.Value);
                if (dic.Value != tmpProp.dic[dic.Key])
                {
                    ModelScript.ChangeModel(dic.Key, dic.Value);
                    myProp.dic[dic.Key] = dic.Value;
                }
            }
        }

    }
}