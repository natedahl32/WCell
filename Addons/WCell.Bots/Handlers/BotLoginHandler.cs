using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WCell.RealmServer.Network;

namespace WCell.Bots.Handlers
{
    /// <summary>
    /// Handles bot logins to the game server. This is a simulated login since Bots don't really need to login, we just need to create them appropriately.
    /// </summary>
    public class BotLoginHandler
    {
        /// <summary>
        /// Checks whether the client is allowed to login and -if so- logs it in
        /// </summary>
        /// <remarks>Executed in IO-Context.</remarks>
        /// <param name="client"></param>
        /// <param name="charLowId"></param>
        private static void LoginCharacter(IRealmClient client, uint charLowId)
        {
            //var acc = client.Account;
            //if (acc == null)
            //{
            //    return;
            //}

            //var record = client.Account.GetCharacterRecord(charLowId);

            //if (record == null)
            //{
            //    log.Error(String.Format(WCell_RealmServer.CharacterNotFound, charLowId, acc.Name));

            //    SendCharacterLoginFail(client, LoginErrorCode.CHAR_LOGIN_NO_CHARACTER);
            //}
            //else if (record.CharacterFlags.HasAnyFlag(CharEnumFlags.NeedsRename | CharEnumFlags.LockedForBilling))
            //{
            //    // TODO: Check in Char Enum?
            //    SendCharacterLoginFail(client, LoginErrorCode.AUTH_BILLING_EXPIRED);
            //}
            //else if (client.ActiveCharacter == null)
            //{
            //    Character chr = null;
            //    try
            //    {
            //        var evt = BeforeLogin;
            //        if (evt != null)
            //        {
            //            record = evt(client, record);
            //            if (record == null)
            //            {
            //                throw new ArgumentNullException("record", "BeforeLogin returned null");
            //            }
            //        }
            //        chr = record.CreateCharacter();
            //        chr.Create(acc, record, client);
            //        chr.LoadAndLogin();

            //        var message = String.Format("Welcome to " + RealmServer.FormattedTitle);

            //        //chr.SendSystemMessage(message);
            //        MiscHandler.SendMotd(client, message);

            //        if (CharacterHandler.NotifyPlayerStatus)
            //        {
            //            World.Broadcast("{0} is now " + ChatUtility.Colorize("Online", Color.Green) + ".", chr.Name);
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        LogUtil.ErrorException(ex, "Failed to load Character from Record: " + record);
            //        if (chr != null)
            //        {
            //            // Force client to relog
            //            chr.Dispose();
            //            client.Disconnect();
            //        }
            //    }
            //}
        }
    }
}
