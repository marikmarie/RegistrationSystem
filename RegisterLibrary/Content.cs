using System;

namespace RegisterLibrary
{
    public class Content
    {
        public int MessageID { get; set; }
        public int SenderUserID { get; set; }
        public int ReceiverUserID { get; set; }
        public string MessageContent { get; set; }
        public DateTime SentDateTime { get; set; }
    }
}
