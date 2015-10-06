using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WCell.Bots.Network;
using WCell.Constants;
using WCell.RealmServer;
using WCell.RealmServer.Network;
using PlayerGuildHandler = WCell.RealmServer.Handlers.GuildHandler;

namespace WCell.Bots.Handlers
{
    public static class GuildHandlers
    {
        /// <summary>
        /// Handles an incoming guild invite request (/ginvite Player)
        /// </summary>
        /// <param name="client">the Session the incoming packet belongs to</param>
        /// <param name="packet">the full packet</param>
        [BotPacketHandler(RealmServerOpCode.SMSG_GUILD_INVITE)]
        public static void InviteRequest(IRealmClient client, RealmPacketIn packet)
        {
            // TODO: Add some logic where a bot might choose to reject a guild inviate

            // Auto-accepts guild invite
            PlayerGuildHandler.Accept(client, packet);
        }
    }
}
