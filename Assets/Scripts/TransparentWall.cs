using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparentWall : MonoBehaviour
{
    private MeshRenderer mesh;
    private Collider col;
    private bool meshState = false;

    //　透明フロアのタイプ
    [SerializeField]
    private MeshType meshType;

    [SerializeField]
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        col = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {

        //meshState = Vector3.Distance(transform.position, player.transform.position) > 12f;

        if (Input.GetKeyDown(KeyCode.M) || (meshState && mesh.enabled == false) || (!meshState && mesh.enabled == true))
        {
            //　表示コンポーネントを反転
            mesh.enabled = !mesh.enabled;
            //　透明時に接触も出来ないタイプの場合はコライダも反転
            if (meshType == MeshType.sometimesTransparentAndCollider)
            {
                col.enabled = !col.enabled;
            }
        }
    }

    //　透明フロアのタイプ
    public enum MeshType
    {
        alwaysTransparent,                  //　通れない壁
        sometimesTransparentAndCollider,    //　透明時には接触も出来ない
        sometimesTransparent,               //　透明時に接触は出来る
    };
}