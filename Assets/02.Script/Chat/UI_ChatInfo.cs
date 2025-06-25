using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class UI_ChatInfo : MonoBehaviour
{
    public TextMeshProUGUI StoryText;
    public TextMeshProUGUI DescriptionText;
    public TextMeshProUGUI NameText;
    public TextMeshProUGUI PersonalityText;
    public TextMeshProUGUI AgeText;
    public Image CharacterImage;


    private void OnEnable()
    {
        this.gameObject.transform.DOScale(1f, 0.2f).SetEase(Ease.OutCirc);
    }

    public void Refresh()
    {
        gameObject.SetActive(true);
        StoryText.text = Chat.Instance.SetDTO.Story;
        DescriptionText.text = "배경\n" + Chat.Instance.SetDTO.Description;
        NameText.text = "이름 :" + Chat.Instance.SetDTO.Name;
        PersonalityText.text = "성격 :" + Chat.Instance.SetDTO.Personality;
        AgeText.text = "나이 :" + Chat.Instance.SetDTO.Age.ToString();
        CharacterImage.sprite = Chat.Instance.SetDTO.Image;
    }

    private void OnDisable()
    {
        this.gameObject.transform.DOScale(0, 0.2f).SetEase(Ease.OutCirc);
    }
}
