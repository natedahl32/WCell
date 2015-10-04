using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WCell.AuthServer;
using WCell.AuthServer.Database;
using WCell.Bots.AI;
using WCell.Bots.Entities;
using WCell.Core.Addons;
using WCell.Core.Initialization;
using WCell.RealmServer.Entities;

namespace WCell.Bots
{
    public class Bots : WCellAddonBase
    {
        #region Declarations

        // Need a reference to the realm server for bot creation
        private readonly RealmServer.RealmServer mRealmServer;

        #endregion

        #region Constructors

        public static Bots Instance
		{
			get; private set;
		}

        public Bots()
		{
			Instance = this;
		}

        #endregion

        #region Properties

        public override string Name
        {
            get { return "Bots Addon"; }
        }

        public override string ShortName
        {
            get { return "Bots"; }
        }

        public override string Author
        {
            get { return "Kazadoom"; }
        }

        public override string Website
        {
            get { return string.Empty; }
        }

        #endregion

        #region Public Methods

        public override void TearDown()
        {
            // Nothing to do
        }

        public override string GetLocalizedName(System.Globalization.CultureInfo culture)
        {
            return "Bots Addon";
        }

        /// <summary>
        /// Add something to the Last Initialization-Step, so that WCell is already fully initialized before this method is called.
        /// </summary>
        [Initialization(InitializationPass.Last, "Initialize Bots Addon")]
        public static void InitBots()
        {
            Character.LoggedIn += (chr, isFirst) =>
            {
                // when we log in, activate the brains for our Bots
                if (chr is BotPlayer)
                {
                    chr.Brain.OnActivate();
                }
            };
        }

        #endregion
    }
}
