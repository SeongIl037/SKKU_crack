using UnityEngine;

public class ChatSetDTO
{
    public readonly string Name;
    public readonly int Age;
    public readonly string Personality;
    public readonly string Story;
    public readonly Sprite Image;
    public readonly string Description;
    public readonly string FirstTalk;
    // ai 역할
    public readonly string Role;
    public readonly string Purpose;
    public readonly string Expression;

    public ChatSetDTO(string name, int age, string personality, string story, Sprite image,string description, string firstTalk,
        string role, string purpose, string expression)
    {
        Name = name;
        Age = age;
        Personality = personality;
        Story = story;
        Image = image;
        FirstTalk = firstTalk;
        Role = role;
        Purpose = purpose;
        Expression = expression;
        Description = description;
    }

    public ChatSetDTO(ChatSet set)
    {
        Name = set.Name;
        Age = set.Age;
        Personality = set.Personality;
        Story = set.Story;
        Image = set.Image;
        FirstTalk = set.FirstTalk;
        Role = set.Role;
        Purpose = set.Purpose;
        Expression = set.Expression;
        Description = set.Description;
    }
}
