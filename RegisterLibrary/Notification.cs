using System;

namespace RegisterLibrary
{
    public class Notification
    {
        public int NotificationID { get; set; }
        public int UserID { get; set; }
        public string NotificationType { get; set; }
        public string NotificationContent { get; set; }
        public DateTime NotificationDateTime { get; set; }
        public bool IsRead { get; set; }
    }
}
