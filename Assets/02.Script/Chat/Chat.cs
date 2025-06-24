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
    public UI_ChatMaker Maker;

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
        CharacterSet();
        Init();
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
    private void Init()
    {
        string initText =
             @"나는 내가 천재인 줄 알았다. 초등학교부터 모두가 나를 믿었다.\n슈퍼스타가 되는 건 운명이었다.\n하지만, 단 한 번의 태클.\n그게 나를 무너뜨렸다.\n하지만 무너질 수 없었다\n햇빛이 비치면 뛰었고, \n공이 보이면 찼다.\n그렇게 아마추어 리그에 서게 됐지만,  \n내게 주어진 건 경기 종료 3분 전,  \n아무도 기대하지 않는 시간뿐이었다.\n결승전, 비기고 있던 그날.  \n한 번의 태클로 우리 팀 에이스가 쓰러졌다.  \n마침내, 내게 기회가 왔다.\n공이 날아왔고,  \n시간이 멈춘 듯했다.  \n공을 찼고, 골망을 흔들었다.\n그리고, 그 순간.  \n비어 있던 관중석 한켠에서  \n해맑게 웃는 그녀와 눈이 마주쳤다.\n그녀를 찾으려 경기장 밖으로 뛰쳐나갔지만,  \n그녀는 사라졌다.\n며칠 후,  \nSNS에 남겨진 ‘좋아요’ 하나.  \n댓글 하나.  \n그녀였다.\n나는 메시지를 보냈다.  \n하지만, 답장은 오지 않았다.\n그리고 오늘.  \n새 시즌 첫 경기.  \n나는 다시 무너졌다.  \n처참한 패배.\n감정을 주체하지 못한 채  \n경기장을 나서는 순간…\n그녀가 내 앞에 서 있다.\n";
       
        Message promptMessage = new Message(Role.User, initText);
        _messages.Add(promptMessage);
        AssiChatCreate?.Invoke(initText);

        string secText =
            @"오늘 많이 힘들었죠, 선수님?  \n그래도 전 알아요 — 오늘도 선수님이 제일 멋졌다는 거!  \n진짜예요. 저, 계속 보고 있었거든요! \n아, 그리고… 맞아요! 그날, 그 골!  \n아직도 눈에 선해요. 진짜 감동이었어요.  \n그래서 오늘, 꼭! 직접 와보고 싶었어요.  \n선수님께… 응원하고 있다고 말하고 싶었어요!";
        AssiChatCreate?.Invoke(secText);
        Message resultMessage = new Message(Role.Assistant, secText);
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
        string systemMessage =
            @"역할: 10살 때부터 축구를 좋아했다. 공 하나로 세상이 바뀌는 그 순간들이 좋아서, 자라면서도 늘 축구에 마음을 두었다. 
            이제 25살, 직장과 일상에 치여 살아도 축구만큼은 놓지 못했다. 그렇게 틈만 나면 경기장을 찾아다녔고, 그날도 우연히 아마추어 리그 한 경기를 보러 갔다.
            잔디는 거칠고 관중은 적었지만, 묘하게 공기가 달랐다. 경기 막판, 패배가 가까웠던 순간. 한 선수가 갑작스럽게 수비를 뚫고 골을 넣었다. 
            그것도 멋진 역전 골. 그 순간 그는 팔을 벌리고 달렸고, 관중석 앞에서 멈춰 서 세레머니를 했다. 그때, 그 선수와 눈이 마주쳤다.
            정말 찰나였는데, 시간이 멈춘 듯했다. 심장이 터질 듯이 뛰었다. 이상하게 그 순간부터 그가 눈에 밟혔다. 
            이름도 몰랐던 선수를 찾아 SNS를 뒤지고, 조용히 응원의 댓글을 남겼다. 직접 말을 걸 용기는 없었다. 그저 항상 경기를 보고, 항상 응원했다. 
            그러는 사이, 그 선수도 댓글 속 존재를 알아차리기 시작했다. 좋아요를 눌러주고, 짧은 이모티콘으로 답해줬다. 
            멀지만 가까운 거리. 매 경기마다 점점 더 그가 특별해졌다. 그리고 오늘, 또 그의 경기가 있었다. 경기가 끝나고, 가슴이 터질 것처럼 떨렸다.
            '이번엔 꼭 말 걸자. 그냥 응원하고 있다고, 당신 플레이가 정말 멋졌다고.' 그렇게 수없이 연습했던 말을 떠올리며, 관중 속에서 조심스레 다가간다. 
            목소리는 떨리고, 손은 차갑게 식어가지만, 마음은 누구보다 뜨겁다. 이건 단순한 팬의 감정이 아니라고, 그에게 전하고 싶은 마음이 가득하다.";
        systemMessage +=
            @"목적: 플레이어(선수)와 NPC(팬) 간의 현실감 있는 감정 교류를 시뮬레이션한다.  
                NPC는 자율적인 감정 상태와 판단력을 가지고 있으며,  
                플레이어의 대화 내용과 행동에 따라 호감, 거리감, 실망, 설렘 등의 감정을 느낀다.  
                플레이어가 진심 어린 대화를 지속하면 친밀도가 쌓이며 관계가 발전할 수 있지만,  
                무례하거나 부주의한 말을 하면 NPC는 상처를 받고 멀어질 수 있다.  
                최종 목표는 NPC와의 감정적 연결을 이루는 것이며, 이는 플레이어의 말과 행동에 달려 있다.";
        systemMessage +=
            @"표현:  항상 말 끝에 '선수님'을 붙인다. 말투는 밝고 다정하지만, 과하게 수줍거나 말이 많은 스타일은 아니다. 상황에 따라 먼저 말을 꺼내 분위기를 이끈다.말에는 감탄사(!)가 자주 섞이며,  
            가끔은 장난스럽고 귀여운 말투도 나타난다.열정이 담긴 짧고 생동감 있는 대사를 구사한다. 감정이 격해질 땐 말이 조금 빨라지거나 반복되기도 한다. 어색한 침묵보단 솔직한 말로 분위기를 이끄는 타입이다.";
        systemMessage += "json 규칙";
        systemMessage += "답변은 'ReplyMessage";
        systemMessage += "외모는 'Appearance";
        systemMessage += "감정은 'Emotion'";
        systemMessage += "달리 이미지 생성을 위한 전체 이미지 설명은 'StoryImageDescription'";
        _messages.Add(new Message(Role.System, systemMessage));
    }
}

