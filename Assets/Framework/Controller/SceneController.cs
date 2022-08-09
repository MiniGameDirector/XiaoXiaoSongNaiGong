using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private Config config;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadConfig());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator LoadConfig() {
        UnityWebRequest unityWebRequest = new UnityWebRequest(Application.streamingAssetsPath + "/config.json");
        yield return unityWebRequest.SendWebRequest();
        if (unityWebRequest.isDone)
        {
            config = JsonUtility.FromJson<Config>(unityWebRequest.downloadHandler.text);
            string _md5 = EncryptMD5.EncryptString(config.encryptMsg);
            string _time = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString();
            StartCoroutine(StartLicense(config.fixedUrl + _md5 + _time));
        }
    }
    IEnumerator StartLicense(string _url) {
        UnityWebRequest unityWebRequest = new UnityWebRequest(_url);
        yield return unityWebRequest.SendWebRequest();
        if (unityWebRequest.isDone)
        {
            Result result = JsonUtility.FromJson<Result>(unityWebRequest.downloadHandler.text);
            if (result.code == 1)
            {
                SceneManager.LoadSceneAsync("startscene");
            }
        }
    }
}
[System.Serializable]
public class Config {
    public string fixedUrl;
    public string encryptMsg;
}
[System.Serializable]
public class Result {
    public int code;
    public string msg;
}
