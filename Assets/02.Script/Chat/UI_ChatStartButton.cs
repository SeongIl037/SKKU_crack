using UnityEngine;

public class UI_ChatStartButton : MonoBehaviour
{
    public GameObject ChatInfo;
    
    public void ChatStart()
    {
        Chat.Instance.ChatStart();
        ChatInfo.SetActive(false);
    }
}
