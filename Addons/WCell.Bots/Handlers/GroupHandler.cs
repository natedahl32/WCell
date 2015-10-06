using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WCell.Bots.Network;
using WCell.Constants;
using WCell.RealmServer;
using WCell.RealmServer.Network;
using GrpHandler = WCell.RealmServer.Handlers.GroupHandler;

namespace WCell.Bots.Handlers
{
    public static class GroupHandler
    {
        /// <summary>
        /// Handles an incoming group invite request (/invite Player)
        /// </summary>
        /// <param name="client">the Session the incoming packet belongs to</param>
        /// <param name="packet">the full packet</param>
        [BotPacketHandler(RealmServerOpCode.SMSG_GROUP_INVITE)]
        public static void GroupInviteRequest(IRealmClient client, RealmPacketIn packet)
        {
            // TODO: Add some logical conditions where the bot might not accept the group invite

            // Accept the invite
            GrpHandler.GroupAccept(client, packet);
        }
    }
}
