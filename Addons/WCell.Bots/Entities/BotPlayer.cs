using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WCell.Bots.AI;
using WCell.RealmServer.AI;
using WCell.RealmServer.Entities;

namespace WCell.Bots.Entities
{
    /// <summary>
    /// Bot player is an AI controlled character in our game world. These are characters that act and play like PC's but are
    /// controlled by "BOT" AI.
    /// </summary>
    public class BotPlayer : Character
    {
        #region Constructors

        public BotPlayer() : base()
        {
            // Create a brain for our bot players
            this.Brain = new BotBrain(this);
            this.Brain.DefaultState = BrainState.Idle;
        }

        #endregion

        #region Private Methods

        internal void CreateBot(RealmServer.RealmAccount acc, RealmServer.Database.Entities.CharacterRecord record, RealmServer.Network.IRealmClient client)
        {
            this.Create(acc, record, client);
        }

        internal void LoadAndLoginBot()
        {
            this.LoadAndLogin();
        }

        #endregion
    }
}
