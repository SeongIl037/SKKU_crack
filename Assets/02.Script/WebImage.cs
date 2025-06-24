using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class WebImage : MonoBehaviour
{ 
    public RawImage MyImage;
    
    private void Start() {
        StartCoroutine(GetTexture());
    }
 
    private IEnumerator GetTexture() {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture("https://kilsh.or.kr/wp-content/uploads/tistory/3002/img/img.png");
        yield return www.SendWebRequest();

        if(www.isNetworkError || www.isHttpError) {
            Debug.Log(www.error);
        }
        else {
            Texture myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            MyImage.texture = myTexture;
        }
    }
}
