using UnityEngine;

[CreateAssetMenu(fileName = "ChatDataSO", menuName = "Scriptable Objects/ChatDataSO")]
public class ChatDataSO : ScriptableObject
{
    [SerializeField]
    private string _name;
    public string Name => _name;
    
    [SerializeField]
    private int _age;
    public int Age => _age;
    
    [SerializeField]
    private string _personality;
    public string Personality => _personality;
    
    [SerializeField]
    private string _story;
    public string Story => _story;
    
    [SerializeField]
    private Sprite _image;
    public Sprite Image => _image;

    
    [SerializeField]
    private string _description;
    public string Description => _description;
    
    [SerializeField] 
    private string _firstTalk;
    public string FirstTalk => _firstTalk;
    
    // ai 역할
    [SerializeField]
    private string _role;
    public string Role => _role;
    
    [SerializeField]
    private string _purpose;
    public string Purpose => _purpose;
    
    [SerializeField]
    private string _expression;
    public string Expression => _expression;
}
