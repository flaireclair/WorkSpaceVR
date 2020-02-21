using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ListView;

public class CanvasManager : MonoBehaviour
{
    private static GameObject canvas;
    [SerializeField] private GameObject button;

    // Start is called before the first frame update
    void Start()
    {
        canvas = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) RenderCanvas(new List<string>() { "a1", "b2", "c3" });
    }

    public void RenderCanvas(List<string> rooms)
    {
        int i = 0;
        foreach (string room in rooms)
        {
            Debug.Log(room);
            canvas.transform.GetChild(i%2).GetComponent<MoveScrollViewWithOVRInput>().AddDataType(room);
            i++;
        }
    }

    public void ChangeCanvasSize(bool isBigger)
    {
        
    }
}
