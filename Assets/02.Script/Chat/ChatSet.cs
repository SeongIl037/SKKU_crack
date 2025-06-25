using System;
using UnityEngine;

public class ChatSet
{
    // 초기
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

    public ChatSet(string name, int age, string personality, string story, Sprite image, string description ,string firstTalk, string role, string purpose,
        string expression)
    {
        // 1. 문자열 유효성 검사 (이름은 필수 값으로 가정)
        if (string.IsNullOrWhiteSpace(name))
        {
            // 이름이 null, 비어있거나, 공백 문자로만 이루어져 있을 경우 예외 발생
            throw new Exception("Name cannot be null or whitespace.");
        }

        // 2. 숫자 범위 유효성 검사 (나이는 음수일 수 없음)
        if (age < 0)
        {
            // 나이가 0보다 작을 경우 예외 발생
            throw new ArgumentOutOfRangeException(nameof(age), "Age cannot be negative.");
        }

        // 3. 참조 타입 Null 검사 (이미지는 필수 값으로 가정)
        if (image == null)
        {
            // Sprite 이미지가 null일 경우 예외 발생
            throw new ArgumentNullException(nameof(image), "Image cannot be null.");
        }

        // 4. 나머지 참조 타입들에 대한 Null 검사 (빈 문자열은 허용하되, null은 방지)
        // 만약 빈 문자열도 허용하고 싶지 않다면 string.IsNullOrEmpty() 를 사용하세요.
        Personality = personality ?? throw new ArgumentNullException(nameof(personality));
        Story = story ?? throw new ArgumentNullException(nameof(story));
        Role = role ?? throw new ArgumentNullException(nameof(role));
        Purpose = purpose ?? throw new ArgumentNullException(nameof(purpose));
        Expression = expression ?? throw new ArgumentNullException(nameof(expression));

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

    public ChatSetDTO ToDTO()
    {
        return new ChatSetDTO(this);
    }
}
