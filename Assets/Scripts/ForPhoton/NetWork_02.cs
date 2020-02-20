using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using Photon.Pun;

namespace Com.MyCompany.MyGame
{
    /// <summary>
    /// PhotonのManager
    /// </summary>
    public class NetWork_02 : MonoBehaviour
    {

        public GameObject cam;

        void Start()
        {
            // Photonに接続する(引数でゲームのバージョンを指定できる)
            PhotonNetwork.ConnectUsingSettings();
        }

        // ロビーに入ると呼ばれる
        void OnJoinedLobby()
        {
            Debug.Log("ロビーに入りました。");

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
        void OnJoinedRoom()
        {
            Debug.Log("ルームへ入室しました。");
            StartCoroutine("FindCamParent");
        }

        // ルームの入室に失敗すると呼ばれる
        void OnPhotonRandomJoinFailed()
        {
            Debug.Log("ルームの入室に失敗しました。");

            // ルームがないと入室に失敗するため、その時は自分で作る
            // 引数でルーム名を指定できる
            //PhotonNetwork.CreateRoom("myRoomName");
            Join();
        }

        IEnumerator FindCamParent()
        {
            yield return null;
            Debug.Log(GameObject.FindWithTag("CameraParent"));
            if (GameObject.Find("CameraParent(Clone)"))
            {
                cam.AddComponent<CameraVal>().obj = GameObject.FindWithTag("CameraParent");
                yield break;
            }
        }
    }
}