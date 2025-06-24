using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
public class WebText : MonoBehaviour
{
    public Text MyText;

    async UniTaskVoid Go()
    {
        await UniTask.Delay(TimeSpan.FromHours(1));
    }

    private async UniTask Stop()
    {
        // stop
        
    }
    IEnumerator GetText() {
        
        
        string url = "https://openapi.naver.com/v1/search/news.json?query=손흥민&display=30";
        
        UnityWebRequest www = UnityWebRequest.Get(url);
        www.SetRequestHeader("X-Naver-Client-Id", "e3xw4mD4Ysb3xLzX8Ovf");
        www.SetRequestHeader("X-Naver-Client-Secret", "7NiMB_exRI");
        
        yield return www.SendWebRequest();
 
        if(www.isNetworkError || www.isHttpError) {
            Debug.Log(www.error);
        }
        else {
            // Show results as text
            MyText.text = www.downloadHandler.text;
        }
    }
}
