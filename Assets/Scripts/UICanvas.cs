using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICanvas : MonoBehaviour
{

    Transform canvasTrans;
    [SerializeField] Transform playerTrans;
    [SerializeField] Vector3 canvasVec;  //Vector3(0, 1, -1)
    [SerializeField] Vector3 canvasRot;  //Vector3(45, 0, 0)

    void Awake()
    {
        canvasTrans = transform;
        canvasTrans.rotation = Quaternion.Euler(canvasRot);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LateUpdate()
    {
        //	cameraTrans.position = playerTrans.position + cameraVec;
        canvasVec = playerTrans.forward * 3.5f;
        if (playerTrans.position.y + canvasVec.y <= 0.5f)
            canvasTrans.position = Vector3.Lerp(canvasTrans.position, new Vector3(playerTrans.position.x + canvasVec.x, 0.5f, playerTrans.position.z + canvasVec.z), 2.0f * Time.deltaTime);
        else
            canvasTrans.position = Vector3.Lerp(canvasTrans.position, playerTrans.position + canvasVec, 2.0f * Time.deltaTime); 
        Vector3 direction = (playerTrans.position - canvasTrans.position);
        canvasTrans.forward = -direction;
        // canvasTrans.LookAt(playerTrans);
    }
}
