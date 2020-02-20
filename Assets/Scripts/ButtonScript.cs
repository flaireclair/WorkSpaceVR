using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    
    RaycastHit hit;
    public string AxisName;
    public KeyCode KeyCode;
    public GameObject canvas;
    int i = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Physics.Raycast(transform.position, transform.forward, out hit, 1000))
        {
            //Debug.Log("Now!");
            if (Input.GetAxis(AxisName) > 0.1)
            {
                Debug.Log("PULL!");
                //if we hit a button
                Button button = hit.transform.gameObject.GetComponent<Button>();

                if (button != null)
                {
                    Debug.Log("Click!");
                    button.onClick.Invoke();
                }

            }
        }
        
        if(Input.GetKey(KeyCode) || OVRInput.GetDown(OVRInput.Button.One))
        {
            WebViewObject webViewObject = gameObject.AddComponent<WebViewObject>();
            webViewObject.Init(null);
            string url = "https://google.com";
            webViewObject.LoadURL(url);
            webViewObject.SetVisibility(true);
        }
    }
}
