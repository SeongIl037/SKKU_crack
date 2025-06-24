using TMPro;
using UnityEngine;

public class UI_Chat : MonoBehaviour
{
    public TextMeshProUGUI Text;

    public void TextSet(string text)
    {
        Text.text = text;
    }
}
