using System;
using UnityEngine;

public class UI_ChatMaker : MonoBehaviour
{
    public GameObject AssistChatPrefab;
    public GameObject UserChatPrefab;
    public Transform Content;
    
    
    private void Start()
    {
        Chat.Instance.AssiChatCreate += AssistChatMake;
        Chat.Instance.UserChatCreate += UserChatMake;
    }

    public void AssistChatMake(string text)
    {
        GameObject chat = Instantiate(AssistChatPrefab, Content);
        chat.GetComponent<UI_Chat>().TextSet(text);
    }

    public void UserChatMake(string text)
    {
        GameObject chat = Instantiate(UserChatPrefab, Content);
        chat.GetComponent<UI_Chat>().TextSet(text);
    }

}
