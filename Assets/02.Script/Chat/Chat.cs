using System;
using UnityEngine;
using System.Collections.Generic;
using OpenAI;
using OpenAI.Audio;
using OpenAI.Chat;
using OpenAI.Images;
using OpenAI.Models;
using TMPro;
using UnityEngine.UI;

public class Chat : MonoBehaviour
{
    public static Chat Instance;
    private OpenAIClient _api; 
    
    public TMP_InputField MyInputField;
    public Button SendButton;

    private List<Message> _messages = new List<Message>();
   
    public event Action<string> AssiChatCreate;
    public event Action<string> UserChatCreate;

    private ChatSet _set;
    public ChatSetDTO SetDTO => _set.ToDTO();
    [SerializeField] private ChatDataSO _settingData;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }

    private void Start()
    {
        _api = new OpenAIClient(APIKeys.OPENAI_API_KEY);
        ChatSet chatSet = new ChatSet(_settingData.Name, _settingData.Age, _settingData.Personality, _settingData.Story, _settingData.Image, _settingData.Description ,_settingData.FirstTalk, _settingData.Role,_settingData.Purpose,_settingData.Expression);
        _set = chatSet;
    }

    public async void SendMessage()
    {
        string prompt = MyInputField.text;
        if (string.IsNullOrEmpty(prompt))
        {
            return;
        }
        MyInputField.text = string.Empty;
        
        SendButton.interactable = false;
        // 2. 메시지 작성 후 메시지's 리스트에 추가
        Message promptMessage = new Message(Role.User, prompt);
        _messages.Add(promptMessage);
        
        // 3. 메시지 보내기
        var chatRequest = new ChatRequest(_messages, Model.GPT4o);
        UserChatCreate?.Invoke(prompt);
        
        // 4. 답변 받기
        var (npcResponse, response) = await _api.ChatEndpoint.GetCompletionAsync<NpcResponse>(chatRequest);
        
        // 5. 답변 선택
        var choice = response.FirstChoice;
        
        // 6. 답변 출력
        AssiChatCreate?.Invoke(npcResponse.ReplyMessage);
        // 7. 답변도 message's 추가
        Message resultMessage = new Message(Role.Assistant, choice.Message);
        _messages.Add(resultMessage);
        
        SendButton.interactable = true;
    }
    // 처음 상황, 캐릭터의 처음 대사
    public void ChatStart()
    {
        CharacterSet();
        Message resultMessage = new Message(Role.Assistant, SetDTO.FirstTalk);
        AssiChatCreate?.Invoke(SetDTO.FirstTalk);
        _messages.Add(resultMessage);
    }
// 캐릭터 설정하는 중...
//  {⊂_ヽ
//     ＼＼ Λ＿Λ
// 　　 ＼( ‘ㅅ’ ) 두둠칫
// 　　　 >　⌒ヽ
// 　　　/ 　 へ＼
// 　　 /　　/　＼＼
// 　　 ﾚ　ノ　　 ヽ_つ
// 　　/　/두둠칫
// 　 /　/|
// 　(　(ヽ
// 　|　|、＼
// 　| 丿 ＼ ⌒)
// 　| |　　) /
// `ノ )　　Lﾉ
// }    
    private void CharacterSet()
    {
        string systemMessage = SetDTO.Role;
        systemMessage += SetDTO.Purpose;
        systemMessage += SetDTO.Expression;
        systemMessage += "json 규칙";
        systemMessage += "답변은 'ReplyMessage";
        systemMessage += "외모는 'Appearance";
        systemMessage += "감정은 'Emotion'";
        systemMessage += "달리 이미지 생성을 위한 전체 이미지 설명은 'StoryImageDescription'";
        _messages.Add(new Message(Role.System, systemMessage));
    }
}

