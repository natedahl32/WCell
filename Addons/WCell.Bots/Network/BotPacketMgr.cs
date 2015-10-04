using WCell.Util.Logging;
using WCell.Constants;
using WCell.Core.Network;
using WCell.PacketAnalysis;
using WCell.RealmServer.Debugging;
using WCell.Util;
using PacketHandler =
    System.Action<WCell.RealmServer.Network.IRealmClient, WCell.RealmServer.RealmPacketIn>;
using RealmPacketHandler =
    WCell.Core.Network.PacketHandler<WCell.RealmServer.Network.IRealmClient, WCell.RealmServer.RealmPacketIn>;
using WCell.RealmServer.Network;
using WCell.RealmServer;

namespace WCell.Bots.Network
{
    public class BotPacketMgr : PacketManager<IRealmClient, RealmPacketIn, ClientPacketHandlerAttribute>
    {
        private static readonly Logger s_log = LogManager.GetCurrentClassLogger();

		static BotPacketMgr()
		{
            Instance = new BotPacketMgr();
		}

        public static readonly BotPacketMgr Instance;

        protected BotPacketMgr()
		{
			UnhandledPacket -= DefaultUnhandledPacketHandler;
		}

		public override uint MaxHandlers
		{
			get { return (uint)RealmServerOpCode.Maximum; }
		}

		#region Handle Packets
		/// <summary>
		/// Attempts to handle an incoming packet. 
		/// Constraints:
		/// OpCode must be valid.
		/// GamePackets cannot be sent if ActiveCharacter == null.
		/// </summary>
		/// <param name="client">the client the packet is from</param>
		/// <param name="packet">the packet to be handled</param>
		/// <returns>true if the packet handler executed successfully; false otherwise</returns>
		public override bool HandlePacket(IRealmClient client, RealmPacketIn packet)
		{
#if DEBUG
			DebugUtil.DumpPacket(client.Account, packet, true, PacketSender.Server);
#endif

			var handlerDesc = m_handlers.Get(packet.PacketId.RawId);

			if (handlerDesc != null)
			{
				handlerDesc.Handler(client, packet);
				return true;
			}

		    HandleUnhandledPacket(client, packet);
		    return true;
		}
		#endregion
    }
}
