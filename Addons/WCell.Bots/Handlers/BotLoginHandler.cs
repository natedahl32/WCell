using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WCell.Bots.Entities;
using WCell.Constants;
using WCell.Constants.Login;
using WCell.RealmServer.Chat;
using WCell.RealmServer.Global;
using WCell.RealmServer.Handlers;
using WCell.RealmServer.Network;
using WCell.RealmServer.Res;
using WCell.Util.Graphics;
using WCell.Util.Logging;

namespace WCell.Bots.Handlers
{
    public static class BotLoginHandler
    {
        private static readonly Logger log = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Checks whether the bot is allowed to login and -if so- logs it in
        /// </summary>
        /// <remarks>Executed in IO-Context.</remarks>
        /// <param name="client"></param>
        /// <param name="charLowId"></param>
        public static void LoginBot(IRealmClient client, uint charLowId)
        {
            var acc = client.Account;
            if (acc == null)
            {
                return;
            }

            var record = client.Account.GetCharacterRecord(charLowId);

            if (record == null)
            {
                //log.Error(String.Format(WCell_RealmServer.CharacterNotFound, charLowId, acc.Name));

                LoginHandler.SendCharacterLoginFail(client, LoginErrorCode.CHAR_LOGIN_NO_CHARACTER);
            }
            else if (client.ActiveCharacter == null)
            {
                BotPlayer chr = null;
                try
                {
                    // TODO: Fix this so we can get before login events for bots. Do we even need though?
                    //var evt = LoginHandler.BeforeLogin;
                    //if (evt != null)
                    //{
                    //    record = evt(client, record);
                    //    if (record == null)
                    //    {
                    //        throw new ArgumentNullException("record", "BeforeLogin returned null");
                    //    }
                    //}
                    chr = new BotPlayer();
                    chr.CreateBot(acc, record, client);
                    chr.LoadAndLoginBot();

                    var message = String.Format("Welcome to " + RealmServer.RealmServer.FormattedTitle);

                    //chr.SendSystemMessage(message);
                    MiscHandler.SendMotd(client, message);

                    if (CharacterHandler.NotifyPlayerStatus)
                    {
                        World.Broadcast("{0} is now " + ChatUtility.Colorize("Online", Color.Green) + ".", chr.Name);
                    }
                }
                catch (Exception ex)
                {
                    LogUtil.ErrorException(ex, "Failed to load Character from Record: " + record);
                    if (chr != null)
                    {
                        // Force client to relog
                        chr.Dispose();
                        client.Disconnect();
                    }
                }
            }
        }
    }
}
