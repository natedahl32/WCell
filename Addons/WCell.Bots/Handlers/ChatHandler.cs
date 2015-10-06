using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WCell.Bots.Entities;
using WCell.Bots.Network;
using WCell.Constants;
using WCell.Constants.Misc;
using WCell.Core;
using WCell.RealmServer;
using WCell.RealmServer.Chat;
using WCell.RealmServer.Handlers;
using WCell.RealmServer.Network;
using WCell.Util;

namespace WCell.Bots.Handlers
{
    public static partial class BotChatMgr
    {
        /// <summary>
        /// Handles an incoming chat message.
        /// </summary>
        /// <param name="client">the client that sent to us</param>
        /// <param name="packet">the full packet</param>
        [BotPacketHandler(RealmServerOpCode.SMSG_MESSAGECHAT, RequiresLogin = false)]	// one can also chat while logging out
        public static void HandleChatMessage(IRealmClient client, RealmPacketIn packet)
        {
            ChatMsgType msgType = (ChatMsgType)packet.ReadByte();
            ChatLanguage msgLang = (ChatLanguage)packet.ReadUInt32();
            EntityId msgSender = packet.ReadEntityId();
            packet.ReadInt32();
            EntityId recipient = packet.ReadEntityId();
            string msg = ReadMessage(packet);
            ChatTag msgChatTag = (ChatTag)packet.ReadByte();

            var chr = client.ActiveCharacter;
            if (client.IsConnected && chr != null)
            {
                // Get the sender entity if we have a valid entity
                var sender = chr.Map.GetObject(msgSender) as IChatter;

                // If we are the sender, don't respond to it, we know what we sent
                if (sender != null && (sender is BotPlayer) && chr.EntityId == (sender as BotPlayer).EntityId)
                    return;
                
                // Handle the chat message
                HandleBotChatMessage(sender, msg, msgLang, msgType, chr);
            }
        }

        /// <summary>
        /// Reads a string from a packet, and treats it like a chat message, purifying it.
        /// </summary>
        /// <param name="packet">the packet to read from</param>
        /// <returns>the purified chat message</returns>
        private static string ReadMessage(RealmPacketIn packet)
        {
            var msg = packet.ReadPascalStringUInt();

            return msg;
        }

        /// <summary>
        /// Handles an incoming chat message.
        /// </summary>
        /// <param name="chatter">the person hatting</param>
        /// <param name="message">the chat message</param>
        /// <param name="lang">the language of the message</param>
        /// <param name="chatType">the type of message</param>
        /// <param name="target">the target of the message (channel, whisper, etc)</param>
        public static void HandleBotChatMessage(IChatter chatter, string message, ChatLanguage lang,
            ChatMsgType chatType, IGenericChatTarget target)
        {
            // Only handle bot chat received
            var botPlayer = target as BotPlayer;
            if (botPlayer == null)
                return;

            // TODO: Just an exmaple. Say something!
            if (chatType == ChatMsgType.Whisper)
                ChatMgr.SayYellEmote(botPlayer, ChatMsgType.Yell, botPlayer.SpokenLanguage, "You talked to me!", 1000f);
        }
    }
}
