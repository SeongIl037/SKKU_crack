using Newtonsoft.Json;
using UnityEngine;

public class NpcResponse : MonoBehaviour
{
    [JsonProperty("ReplyMessage")]
    public string ReplyMessage {get;set;}
    
    [JsonProperty("Appearance")]
    public string Appearance {get;set;}
    
    [JsonProperty("Eemotion")]
    public string Emotion {get;set;}
    
    [JsonProperty("StoryImageDescription")]
    public string StoryImageDescription {get;set;}
}
