using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.ViewModel
{
    public static class Messager
    {
        public static void AnouncementBroadCast(bool value1, bool value2, bool value3)
        {
            if (AnouncementMessageTransmitted != null)
                AnouncementMessageTransmitted(value1, value2, value3);
        }
        public static void AnouncementDetailBroadCast(string value)
        {
            if (AnouncementDetailMessageTransmitted != null)
                AnouncementDetailMessageTransmitted(value);
        }
        public static void CurrentPageBroadCast(string value)
        {
            if (CurrnentPageMessageTransmitted != null)
                CurrnentPageMessageTransmitted(value);
        }

        public static Action<bool, bool, bool> AnouncementMessageTransmitted;
        public static Action<string> AnouncementDetailMessageTransmitted;
        public static Action<string> CurrnentPageMessageTransmitted;
    }
}
