using System;
using System.Collections.Generic;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using OpenAI;
using OpenAI.Audio;
using OpenAI.Chat;
using OpenAI.Images;
using OpenAI.Models;

public class ChatGPTTest : MonoBehaviour
{
    public TextMeshProUGUI ResultTextUI;
    public TMP_InputField InputField;
    public Button SendButton;
    private List<Message> _messages = new List<Message>();
    private const string OPEN_API_KEY = "1";
    public AudioSource _audioSource;

    private const string TYPECASTE_KEY =
        "1";

    private OpenAIClient _api;
    public RawImage _image;
    private void Start()
    {
        // 1. API 클라이언트 초기화 -> ChatGPT 접속
        _api = new OpenAIClient(OPEN_API_KEY);      

        //chat-f
        // c : context : 문맥, 상황을 많이 알려줘라
        // h : hint : 예시 답변을 많이 줘라
        // a : as A role : 역할을 제공한다.
        // t : target : 답변의 타겟을 알려줘라
        // f : format : 답변의 형태를 지정하라
        string systemMessage = "역할 : 너는 이제부터 게임 NPC다. 자신을 실제 게임속 고양이 인간이라고 생각한다.";
        systemMessage += "목적 : 실제 사람처럼 대화하는 게임 NPC 모드";
        systemMessage += "표현 : 말 끝마다 '냥~'을 붙인다. 항상 100글자 이내로 대답한다.";
        systemMessage += "json 규칙";
        systemMessage += "답변은 'ReplyMessage";
        systemMessage += "외모는 'Appearance";
        systemMessage += "감정은 'Emotion'";
        systemMessage += "달리 이미지 생성을 위한 전체 이미지 설명은 'StoryImageDescription'";
        _messages.Add(new Message(Role.System, systemMessage));
    }
    
    public async void Send()
    {
        // 0. 프롬프트(=AI에게 원하는 명령을 적은 텍스트)를 읽어온다.
        string prompt = InputField.text;
        if (string.IsNullOrEmpty(prompt))
        {
            return;
        }
        InputField.text = string.Empty;
        
        SendButton.interactable = false;
        
        // 2. 메시지 작성 후 메시지's 리스트에 추가
        Message promptMessage = new Message(Role.User, prompt);
        _messages.Add(promptMessage);
        
        // 3. 메시지 보내기
        //var chatRequest = new ChatRequest(_messages, Model.GPT4oAudioMini, audioConfig:Voice.Alloy);
        var chatRequest = new ChatRequest(_messages, Model.GPT4o);
        
        
        // 4. 답변 받기
        // var response = await api.ChatEndpoint.GetCompletionAsync(chatRequest);
        var (npcResponse, response) = await _api.ChatEndpoint.GetCompletionAsync<NpcResponse>(chatRequest);
        
        Debug.Log(npcResponse.ReplyMessage);
        
        // 5. 답변 선택
        var choice = response.FirstChoice;
        
        // 6. 답변 출력
        ResultTextUI.text = npcResponse.ReplyMessage;
        
        // 7. 답변도 message's 추가
        Message resultMessage = new Message(Role.Assistant, choice.Message);
        _messages.Add(resultMessage);
        
        // 8. 답변 오디오 재생
        // MyAudioSource.PlayOneShot(response.FirstChoice.Message.AudioOutput.AudioClip);
        PlayTTS(npcResponse.ReplyMessage);
        
        GenerateImage(npcResponse.StoryImageDescription);
    }

    private async void PlayTTS(string text)
    {

        var request = new SpeechRequest(text);
        var speechClip = await _api.AudioEndpoint.GetSpeechAsync(request);
        _audioSource.PlayOneShot(speechClip);
        Debug.Log(speechClip);
    }

    private async void GenerateImage(string imageprompt)
    {
        var request = new ImageGenerationRequest(imageprompt, Model.DallE_3);
        var imageResults = await _api.ImagesEndPoint.GenerateImageAsync(request);

        foreach (var result in imageResults)
        {
            _image.texture = result.Texture;
        }
        
        SendButton.interactable = true;
    }
}