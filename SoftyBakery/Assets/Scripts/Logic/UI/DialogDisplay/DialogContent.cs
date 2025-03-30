using System;

namespace GameLogic
{

    [Serializable]
    public class DialogContent
    {
        //None,Left,Right,Both,高亮人物
        public DialogSpeaker Speaker;
        //对话框内容抬头
        //  SpeakerName : xxxxxxx
        //如果是空字符串，则不会添加 : 符号
        public string SpeakerName;
        //左侧人物ui预制体名字和人物表情
        public CharacterShown LeftCharacter;
        //右侧人物ui预制体名字和人物表情
        public CharacterShown RightCharacter;
        //弹出该段对话时，触发的事件名字
        public string Event;
        //播放该段对话的速度 单位：字符/秒
        public float SpeakSpeed; // 说话速度
        
        [NonSerialized]
        public string DialogKey; // 对话内容多语言键
    }

    [Serializable]
    public enum DialogSpeaker
    {
        None,
        Left,
        Right,
        Both,
    }

    [Serializable]
    public struct CharacterShown
    {
        public string Name;
        public string Expression;
    }
}