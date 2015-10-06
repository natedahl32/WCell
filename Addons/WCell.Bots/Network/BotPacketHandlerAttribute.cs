using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WCell.Constants;
using WCell.Core.Network;

namespace WCell.Bots.Network
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
    public sealed class BotPacketHandlerAttribute : PacketHandlerAttribute
    {

        public BotPacketHandlerAttribute(PacketId identifier)
            : base(identifier)
        {
        }

        public BotPacketHandlerAttribute(AuthServerOpCode identifier)
            : base(identifier)
        {
        }

        public BotPacketHandlerAttribute(RealmServerOpCode identifier)
            : base(identifier)
        {
        }
    }
}
