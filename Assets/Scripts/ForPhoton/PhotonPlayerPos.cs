using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PhotonPlayerPos : MonoBehaviour
{

    private PhotonView photonView;
    GameObject player;
    Transform R_hand, L_hand;
    [SerializeField] GameObject R_Child, L_Child, human;

    bool isobject;

    // Use this for initialization
    void Start()
    {
        photonView = GetComponent<PhotonView>();
        if (photonView.IsMine)
        {
            if (human.activeSelf)
                human.SetActive(false);

            player = GameObject.FindGameObjectWithTag("Player");
            R_hand = GameObject.FindGameObjectWithTag("R_Hand").transform;

            L_hand = GameObject.FindGameObjectWithTag("L_Hand").transform;
            isobject = (player != null) && (R_hand != null) && (L_hand != null);
            StartCoroutine(find());

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            if (human.activeSelf)
                human.SetActive(false);
            transform.SetPositionAndRotation(player.transform.position, player.transform.rotation);
            if (R_hand != null)
                R_Child.transform.SetPositionAndRotation(R_hand.position, R_hand.rotation);
            if (L_hand != null)
                L_Child.transform.SetPositionAndRotation(L_hand.position, L_hand.rotation);


        }


    }

    IEnumerator find()
    {
        while (isobject)
        {
            yield return new WaitForSeconds(1f);
            if (player == null)
                player = GameObject.FindGameObjectWithTag("Player");
            if (L_hand == null)
                L_hand = GameObject.FindGameObjectWithTag("L_Hand").transform;
            if (R_hand == null)
                R_hand = GameObject.FindGameObjectWithTag("R_Hand").transform;

            isobject = (player != null) && (R_hand != null) && (L_hand != null);
        }
    }

}
