using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class UI_Chat : MonoBehaviour
{
    public TextMeshProUGUI Text;
    public TextMeshProUGUI Name;
    public Image Portrait;
    public void AssiTextSet(string text, ChatSetDTO set)
    {
        Text.text = text;
        Name.text = set.Name;
        Portrait.sprite = set.Image;
    }

    public void UserTextSet(string text)
    {
        Text.text = text;
    }
}
