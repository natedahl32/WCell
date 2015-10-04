using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using WCell.AuthServer.Accounts;
using WCell.Bots.Handlers;
using WCell.Bots.Network;
using WCell.Constants;
using WCell.Intercommunication.DataTypes;
using WCell.RealmServer;
using WCell.RealmServer.Commands;
using WCell.RealmServer.Database;
using WCell.RealmServer.Database.Entities;
using WCell.RealmServer.Handlers;
using WCell.RealmServer.Network;
using WCell.RealmServer.RacesClasses;
using WCell.Util;
using WCell.Util.Commands;
using WCell.Util.Logging;
using WCell.Util.Threading;

namespace WCell.Bots.Commands
{
    /// <summary>
    /// Commands to manage bots on a server
    /// </summary>
    public class BotCommands : RealmServerCommand
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        #region Properties

        public override RoleStatus RequiredStatusDefault
        {
            get
            {
                // Bot commands require admin status
                return RoleStatus.Admin;
            }
        }

        #endregion

        protected override void Initialize()
        {
            Init("Bots", "Bot");
            EnglishDescription = "Provides commands for admins to manage bots on the server";
        }

        /// <summary>
        /// Gets the realm client for a Bot account
        /// </summary>
        /// <param name="accountName">Name of the account</param>
        /// <returns></returns>
        static protected BotRealmClient GetRealmClient(string accountName)
        {
            var client = new BotRealmClient();
            RealmAccount.InitializeBotAccount(client, accountName);
            return client;
        }

        #region Add Bot Command

        public class AddBotCommand : SubCommand
        {
            protected override void Initialize()
            {
                Init("add");
                EnglishParamInfo = "[-n]";
                EnglishDescription = "Adds a random pre-created bot to the world. If no bots have been created yet it will create one. -n forces a new bot to be created";
            }

            public override void Process(CmdTrigger<RealmServerCmdArgs> trigger)
            {
                BotRealmClient client;
                string characterName;
                

                // If -n, create a new account and a new character.
                var mod = trigger.Text.NextModifiers();
                if (mod == "n")
                {
                    // TODO: Get a random character name that does not exist
                    characterName = "Abaddon";
                    client = this.CreateNewCharacter(characterName);
                    if (client == null)
                        return;
                }
                else // Else, check if we have any already created bots available. If not create one. If we do pull a random one and log it in.
                {
                    // TODO: Get a character name that already exists but is not logged in and we need their account name
                    client = BotCommands.GetRealmClient("BOT1");
                    characterName = client.Account.Characters[0].Name;
                    // TODO: If not character names available that are not already logged in we need to create a new character
                }

                // Get character id of first character on the account
                uint characterId = client.Account.Characters[0].EntityLowId;
                
                // Login the bot
                BotLoginHandler.LoginBot(client, characterId);
            }

            protected BotRealmClient CreateNewCharacter(string characterName)
            {
                // Get the realm client
                var client = BotCommands.GetRealmClient("BOT1");

                // Create the character
                var record = CharacterRecord.CreateNewCharacterRecord(client.Account, characterName);

                var chrRace = RaceId.Orc;
                var chrClass = ClassId.Warrior;

                var archetype = ArchetypeMgr.GetArchetype(chrRace, chrClass);
                if (archetype == null)
                {
                    Log.Error("Archetype for race {0} and class {1} is null", chrRace, chrClass);
                    return null;
                }

                record.Gender = GenderType.Male;
                record.Skin = (byte)Utility.Random(byte.MaxValue); //packet.ReadByte();
                record.Face = (byte)Utility.Random(byte.MaxValue);
                record.HairStyle = (byte)Utility.Random(byte.MaxValue);
                record.HairColor = (byte)Utility.Random(byte.MaxValue);
                record.FacialHair = (byte)Utility.Random(byte.MaxValue);
                record.Outfit = (byte)Utility.Random(byte.MaxValue);
                record.GodMode = false;
                record.IsBot = true;

                record.SetupNewRecord(archetype);

                var charCreateTask = new Message2<IRealmClient, CharacterRecord>
                {
                    Callback = CharCreateCallback,
                    Parameter1 = client,
                    Parameter2 = record
                };

                RealmServer.RealmServer.IOQueue.AddMessage(charCreateTask);

                // Try to save new character to database
                try
                {
                    RealmWorldDBMgr.DatabaseProvider.SaveOrUpdate(record);
                }
                catch (Exception e)
                {
                    try
                    {
                        RealmWorldDBMgr.OnDBError(e);
                        RealmWorldDBMgr.DatabaseProvider.SaveOrUpdate(record);
                    }
                    catch (Exception)
                    {
                        return null;
                    }
                }

                // Add character to the account
                client.Account.Characters.Add(record);

                return client;
            }

            private static void CharCreateCallback(IRealmClient client, CharacterRecord newCharRecord)
            {
                // Do nothing. Maybe this can be null?
            }
        }

        #endregion
    }
}
