using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class AccountManager : MonoBehaviour
{
    public GameObject AuthTextBlock;

    public string ServerIP;
    private string csvPath;
    private string access_token;
    private bool haveAccessToken;

    private void setAccessToken()
    {
        if (access_token == "")
            return;

        using (FileStream fs = File.Create(csvPath))
        using (StreamWriter sw =new StreamWriter(fs))
        {
            sw.Write(access_token);
        }
    }

    private void readAccessToken()
    {
        using (FileStream fs = File.OpenRead(csvPath))
        using (StreamReader sr = new StreamReader(fs))
        {
            access_token = sr.ReadLine();
        }
    }

    private void startAuthentication()
    {
        Application.OpenURL("https://slack.com/oauth/authorize?client_id=962177358214.960581220469&scope=identify&redirect_uri=http://" + ServerIP + ":8000/");
        return;
    }

    public void OnClickedAuthentication()
    {
        string authenticationCode = AuthTextBlock.GetComponent<Text>().text;
        if (authenticationCode == "")
            return;
        else
            getToken(authenticationCode);
    }

    private void getToken(string AuthenticationCode)
    {
        StartCoroutine("OnSend", "http://" + ServerIP + ":8000/auth?code=" + AuthenticationCode);
    }

    IEnumerable OnSend(string url)
    {
        UnityWebRequest webRequest = UnityWebRequest.Get(url);
        
        yield return webRequest.SendWebRequest();

        if (webRequest.isNetworkError)
        {
            //通信失敗
        }
        else
        {
            access_token = webRequest.downloadHandler.text;
            setAccessToken();
            haveAccessToken = true;
        }
    }

    private void Start()
    {
        ServerIP = "172.31.115.104";
        csvPath = @"/mnt/sdcard/_usertoken.csv";

        haveAccessToken = File.Exists(csvPath);

        if (!haveAccessToken)
        {
            startAuthentication();
            // 認証用のUIを表示
        }
        else
        {
            readAccessToken();
        }
    }
}
