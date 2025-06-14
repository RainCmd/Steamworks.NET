// This file is provided under The MIT License as part of Steamworks.NET.
// Copyright (c) 2013-2022 Riley Labrecque
// Please see the included LICENSE.txt for additional information.

// This file is automatically generated.
// Changes to this file will be reverted when you update Steamworks.NET

#if !(UNITY_STANDALONE_WIN || UNITY_STANDALONE_LINUX || UNITY_STANDALONE_OSX || STEAMWORKS_WIN || STEAMWORKS_LIN_OSX)
	#define DISABLESTEAMWORKS
#endif

#if !DISABLESTEAMWORKS

using System.Runtime.InteropServices;
using IntPtr = System.IntPtr;

namespace Steamworks {
	public static class SteamGameServer {
		/// <summary>
		/// <para> Game product identifier.  This is currently used by the master server for version checking purposes.</para>
		/// <para> It's a required field, but will eventually will go away, and the AppID will be used for this purpose.</para>
		/// <para>游戏产品标识符。目前用于主服务器的版式检查目的。这是一个必需字段，但最终将不再使用，AppID 将用于此目的。</para>
		/// </summary>
		public static void SetProduct(string pszProduct) {
			InteropHelp.TestIfAvailableGameServer();
			using (var pszProduct2 = new InteropHelp.UTF8StringHandle(pszProduct)) {
				NativeMethods.ISteamGameServer_SetProduct(CSteamGameServerAPIContext.GetSteamGameServer(), pszProduct2);
			}
		}

		/// <summary>
		/// <para> Description of the game.  This is a required field and is displayed in the steam server browser....for now.</para>
		/// <para> This is a required field, but it will go away eventually, as the data should be determined from the AppID.</para>
		/// <para>游戏描述。这是必需字段，目前显示在Steam服务器浏览器中…… 现阶段如此。这是一个必需字段，但它最终会消失，因为数据应从AppID确定。</para>
		/// </summary>
		public static void SetGameDescription(string pszGameDescription) {
			InteropHelp.TestIfAvailableGameServer();
			using (var pszGameDescription2 = new InteropHelp.UTF8StringHandle(pszGameDescription)) {
				NativeMethods.ISteamGameServer_SetGameDescription(CSteamGameServerAPIContext.GetSteamGameServer(), pszGameDescription2);
			}
		}

		/// <summary>
		/// <para> If your game is a "mod," pass the string that identifies it.  The default is an empty string, meaning</para>
		/// <para> this application is the original game, not a mod.</para>
		/// <para> @see k_cbMaxGameServerGameDir</para>
		/// <para>如果你的游戏是“模组”，请传递标识它的字符串。默认值为一个空字符串，表示此应用程序是原始游戏，而不是模组。</para>
		/// <para>@see k_cbMaxGameServerGameDir</para>
		/// </summary>
		public static void SetModDir(string pszModDir) {
			InteropHelp.TestIfAvailableGameServer();
			using (var pszModDir2 = new InteropHelp.UTF8StringHandle(pszModDir)) {
				NativeMethods.ISteamGameServer_SetModDir(CSteamGameServerAPIContext.GetSteamGameServer(), pszModDir2);
			}
		}

		/// <summary>
		/// <para> Is this is a dedicated server?  The default value is false.</para>
		/// <para>这是一个专用服务器吗？ 默认值为 false。</para>
		/// </summary>
		public static void SetDedicatedServer(bool bDedicated) {
			InteropHelp.TestIfAvailableGameServer();
			NativeMethods.ISteamGameServer_SetDedicatedServer(CSteamGameServerAPIContext.GetSteamGameServer(), bDedicated);
		}

		/// <summary>
		/// <para> Login</para>
		/// <para> Begin process to login to a persistent game server account</para>
		/// <para> You need to register for callbacks to determine the result of this operation.</para>
		/// <para> @see SteamServersConnected_t</para>
		/// <para> @see SteamServerConnectFailure_t</para>
		/// <para> @see SteamServersDisconnected_t</para>
		/// <para>登录</para>
		/// <para>开始登录持久性游戏服务器账户流程</para>
		/// <para>您需要注册回调以确定此操作的结果。 @see SteamServersConnected_t @see SteamServerConnectFailure_t @see SteamServersDisconnected_t</para>
		/// </summary>
		public static void LogOn(string pszToken) {
			InteropHelp.TestIfAvailableGameServer();
			using (var pszToken2 = new InteropHelp.UTF8StringHandle(pszToken)) {
				NativeMethods.ISteamGameServer_LogOn(CSteamGameServerAPIContext.GetSteamGameServer(), pszToken2);
			}
		}

		/// <summary>
		/// <para> Login to a generic, anonymous account.</para>
		/// <para> Note: in previous versions of the SDK, this was automatically called within SteamGameServer_Init,</para>
		/// <para> but this is no longer the case.</para>
		/// <para>登录一个通用、匿名帐户。</para>
		/// <para>注意：在以前的SDK版本中，这会自动在SteamGameServer_Init中调用，但现在不再是这样。</para>
		/// </summary>
		public static void LogOnAnonymous() {
			InteropHelp.TestIfAvailableGameServer();
			NativeMethods.ISteamGameServer_LogOnAnonymous(CSteamGameServerAPIContext.GetSteamGameServer());
		}

		/// <summary>
		/// <para> Begin process of logging game server out of steam</para>
		/// <para>开始注销游戏服务器。</para>
		/// </summary>
		public static void LogOff() {
			InteropHelp.TestIfAvailableGameServer();
			NativeMethods.ISteamGameServer_LogOff(CSteamGameServerAPIContext.GetSteamGameServer());
		}

		/// <summary>
		/// <para> status functions</para>
		/// <para>状态函数</para>
		/// </summary>
		public static bool BLoggedOn() {
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamGameServer_BLoggedOn(CSteamGameServerAPIContext.GetSteamGameServer());
		}

		public static bool BSecure() {
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamGameServer_BSecure(CSteamGameServerAPIContext.GetSteamGameServer());
		}

		public static CSteamID GetSteamID() {
			InteropHelp.TestIfAvailableGameServer();
			return (CSteamID)NativeMethods.ISteamGameServer_GetSteamID(CSteamGameServerAPIContext.GetSteamGameServer());
		}

		/// <summary>
		/// <para> Returns true if the master server has requested a restart.</para>
		/// <para> Only returns true once per request.</para>
		/// <para>如果主服务器已请求重启，则返回 true。仅在一次请求中返回 true。</para>
		/// </summary>
		public static bool WasRestartRequested() {
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamGameServer_WasRestartRequested(CSteamGameServerAPIContext.GetSteamGameServer());
		}

		/// <summary>
		/// <para> Server state.  These properties may be changed at any time.</para>
		/// <para> Max player count that will be reported to server browser and client queries</para>
		/// <para>服务器状态。这些属性可能随时更改。</para>
		/// <para>玩家数量报告给服务器浏览器和客户端查询的最大数量</para>
		/// </summary>
		public static void SetMaxPlayerCount(int cPlayersMax) {
			InteropHelp.TestIfAvailableGameServer();
			NativeMethods.ISteamGameServer_SetMaxPlayerCount(CSteamGameServerAPIContext.GetSteamGameServer(), cPlayersMax);
		}

		/// <summary>
		/// <para> Number of bots.  Default value is zero</para>
		/// <para>机器人数量。默认值为零。</para>
		/// </summary>
		public static void SetBotPlayerCount(int cBotplayers) {
			InteropHelp.TestIfAvailableGameServer();
			NativeMethods.ISteamGameServer_SetBotPlayerCount(CSteamGameServerAPIContext.GetSteamGameServer(), cBotplayers);
		}

		/// <summary>
		/// <para> Set the name of server as it will appear in the server browser</para>
		/// <para> @see k_cbMaxGameServerName</para>
		/// <para>设置服务器名称，以便在服务器浏览器中显示</para>
		/// <para>@see k_cbMaxGameServerName</para>
		/// </summary>
		public static void SetServerName(string pszServerName) {
			InteropHelp.TestIfAvailableGameServer();
			using (var pszServerName2 = new InteropHelp.UTF8StringHandle(pszServerName)) {
				NativeMethods.ISteamGameServer_SetServerName(CSteamGameServerAPIContext.GetSteamGameServer(), pszServerName2);
			}
		}

		/// <summary>
		/// <para> Set name of map to report in the server browser</para>
		/// <para> @see k_cbMaxGameServerMapName</para>
		/// <para>设置地图名称在服务器浏览器中</para>
		/// <para>@see k_cbMaxGameServerMapName</para>
		/// </summary>
		public static void SetMapName(string pszMapName) {
			InteropHelp.TestIfAvailableGameServer();
			using (var pszMapName2 = new InteropHelp.UTF8StringHandle(pszMapName)) {
				NativeMethods.ISteamGameServer_SetMapName(CSteamGameServerAPIContext.GetSteamGameServer(), pszMapName2);
			}
		}

		/// <summary>
		/// <para> Let people know if your server will require a password</para>
		/// <para>请告知您的服务器是否需要密码。</para>
		/// </summary>
		public static void SetPasswordProtected(bool bPasswordProtected) {
			InteropHelp.TestIfAvailableGameServer();
			NativeMethods.ISteamGameServer_SetPasswordProtected(CSteamGameServerAPIContext.GetSteamGameServer(), bPasswordProtected);
		}

		/// <summary>
		/// <para> Spectator server port to advertise.  The default value is zero, meaning the</para>
		/// <para> service is not used.  If your server receives any info requests on the LAN,</para>
		/// <para> this is the value that will be placed into the reply for such local queries.</para>
		/// <para> This is also the value that will be advertised by the master server.</para>
		/// <para> The only exception is if your server is using a FakeIP.  Then then the second</para>
		/// <para> fake port number (index 1) assigned to your server will be listed on the master</para>
		/// <para> server as the spectator port, if you set this value to any nonzero value.</para>
		/// <para> This function merely controls the values that are advertised -- it's up to you to</para>
		/// <para> configure the server to actually listen on this port and handle any spectator traffic</para>
		/// <para>旁观者服务器的广播端口。默认值为零，表示服务未启用。如果您的服务器收到任何局域网上的信息请求，则此值将被放置在对这些本地查询的回复中。</para>
		/// <para>这也是主服务器会广播的值。唯一例外情况是如果你的服务器在使用假IP的情况下。这时主服务器会把你的服务器分配给的第二个假端口号（索引 1）列为观众端口，如果将该值设置为任何非零值。</para>
		/// <para>这个函数仅仅控制所宣称的值——它取决于你来配置服务器，使其实际监听该端口并处理任何观众流量。</para>
		/// </summary>
		public static void SetSpectatorPort(ushort unSpectatorPort) {
			InteropHelp.TestIfAvailableGameServer();
			NativeMethods.ISteamGameServer_SetSpectatorPort(CSteamGameServerAPIContext.GetSteamGameServer(), unSpectatorPort);
		}

		/// <summary>
		/// <para> Name of the spectator server.  (Only used if spectator port is nonzero.)</para>
		/// <para> @see k_cbMaxGameServerMapName</para>
		/// <para>观众服务器名称。 (仅当观众端口不为零时使用。)</para>
		/// <para>@see k_cbMaxGameServerMapName</para>
		/// </summary>
		public static void SetSpectatorServerName(string pszSpectatorServerName) {
			InteropHelp.TestIfAvailableGameServer();
			using (var pszSpectatorServerName2 = new InteropHelp.UTF8StringHandle(pszSpectatorServerName)) {
				NativeMethods.ISteamGameServer_SetSpectatorServerName(CSteamGameServerAPIContext.GetSteamGameServer(), pszSpectatorServerName2);
			}
		}

		/// <summary>
		/// <para> Call this to clear the whole list of key/values that are sent in rules queries.</para>
		/// <para>Call this to clear the whole list of key/values that are sent in rules queries.</para>
		/// </summary>
		public static void ClearAllKeyValues() {
			InteropHelp.TestIfAvailableGameServer();
			NativeMethods.ISteamGameServer_ClearAllKeyValues(CSteamGameServerAPIContext.GetSteamGameServer());
		}

		/// <summary>
		/// <para> Call this to add/update a key/value pair.</para>
		/// <para>请调用此方法来添加/更新键值对。</para>
		/// </summary>
		public static void SetKeyValue(string pKey, string pValue) {
			InteropHelp.TestIfAvailableGameServer();
			using (var pKey2 = new InteropHelp.UTF8StringHandle(pKey))
			using (var pValue2 = new InteropHelp.UTF8StringHandle(pValue)) {
				NativeMethods.ISteamGameServer_SetKeyValue(CSteamGameServerAPIContext.GetSteamGameServer(), pKey2, pValue2);
			}
		}

		/// <summary>
		/// <para> Sets a string defining the "gametags" for this server, this is optional, but if it is set</para>
		/// <para> it allows users to filter in the matchmaking/server-browser interfaces based on the value</para>
		/// <para> @see k_cbMaxGameServerTags</para>
		/// <para>设置一个字符串，定义此服务器的“gametags”，这可选，但如果设置了，它允许用户在匹配/服务器浏览器界面中根据该值进行过滤。</para>
		/// <para>@see k_cbMaxGameServerTags</para>
		/// </summary>
		public static void SetGameTags(string pchGameTags) {
			InteropHelp.TestIfAvailableGameServer();
			using (var pchGameTags2 = new InteropHelp.UTF8StringHandle(pchGameTags)) {
				NativeMethods.ISteamGameServer_SetGameTags(CSteamGameServerAPIContext.GetSteamGameServer(), pchGameTags2);
			}
		}

		/// <summary>
		/// <para> Sets a string defining the "gamedata" for this server, this is optional, but if it is set</para>
		/// <para> it allows users to filter in the matchmaking/server-browser interfaces based on the value</para>
		/// <para> @see k_cbMaxGameServerGameData</para>
		/// <para>设置一个字符串，定义此服务器的“gamedata”，这可选，但如果设置了，它允许用户在匹配/服务器浏览器界面中根据值进行过滤。</para>
		/// <para>@see k_cbMaxGameServerGameData</para>
		/// </summary>
		public static void SetGameData(string pchGameData) {
			InteropHelp.TestIfAvailableGameServer();
			using (var pchGameData2 = new InteropHelp.UTF8StringHandle(pchGameData)) {
				NativeMethods.ISteamGameServer_SetGameData(CSteamGameServerAPIContext.GetSteamGameServer(), pchGameData2);
			}
		}

		/// <summary>
		/// <para> Region identifier.  This is an optional field, the default value is empty, meaning the "world" region</para>
		/// <para>区域标识符。这是一个可选字段，默认值为空，表示“世界”区域。</para>
		/// </summary>
		public static void SetRegion(string pszRegion) {
			InteropHelp.TestIfAvailableGameServer();
			using (var pszRegion2 = new InteropHelp.UTF8StringHandle(pszRegion)) {
				NativeMethods.ISteamGameServer_SetRegion(CSteamGameServerAPIContext.GetSteamGameServer(), pszRegion2);
			}
		}

		/// <summary>
		/// <para> Indicate whether you wish to be listed on the master server list</para>
		/// <para> and/or respond to server browser / LAN discovery packets.</para>
		/// <para> The server starts with this value set to false.  You should set all</para>
		/// <para> relevant server parameters before enabling advertisement on the server.</para>
		/// <para> (This function used to be named EnableHeartbeats, so if you are wondering</para>
		/// <para> where that function went, it's right here.  It does the same thing as before,</para>
		/// <para> the old name was just confusing.)</para>
		/// <para>指示您是否希望在主服务器列表中列出以及/或响应服务器浏览器/LAN发现数据包。服务器最初设置为false。您应该在启用服务器广告之前设置所有相关服务器参数。</para>
		/// <para>（这个函数之前叫做EnableHeartbeats，如果你在想它去了哪里，它就在这里。它以前的功能和之前一样，旧的名字只是让人困惑。）</para>
		/// </summary>
		public static void SetAdvertiseServerActive(bool bActive) {
			InteropHelp.TestIfAvailableGameServer();
			NativeMethods.ISteamGameServer_SetAdvertiseServerActive(CSteamGameServerAPIContext.GetSteamGameServer(), bActive);
		}

		/// <summary>
		/// <para> Player list management / authentication.</para>
		/// <para> Retrieve ticket to be sent to the entity who wishes to authenticate you ( using BeginAuthSession API ).</para>
		/// <para> pcbTicket retrieves the length of the actual ticket.</para>
		/// <para> SteamNetworkingIdentity is an optional parameter to hold the public IP address of the entity you are connecting to</para>
		/// <para> if an IP address is passed Steam will only allow the ticket to be used by an entity with that IP address</para>
		/// <para>玩家列表管理/认证。</para>
		/// <para>获取用于发送给希望认证你的实体（使用 BeginAuthSession API）的票据。pcbTicket 检索实际票据的长度。SteamNetworkingIdentity 是一个可选参数，用于存储你连接到的实体的公共 IP 地址，如果传递了 IP 地址，则只会允许该票据被具有该 IP 地址的实体使用。</para>
		/// </summary>
		public static HAuthTicket GetAuthSessionTicket(byte[] pTicket, int cbMaxTicket, out uint pcbTicket, ref SteamNetworkingIdentity pSnid) {
			InteropHelp.TestIfAvailableGameServer();
			return (HAuthTicket)NativeMethods.ISteamGameServer_GetAuthSessionTicket(CSteamGameServerAPIContext.GetSteamGameServer(), pTicket, cbMaxTicket, out pcbTicket, ref pSnid);
		}

		/// <summary>
		/// <para> Authenticate ticket ( from GetAuthSessionTicket ) from entity steamID to be sure it is valid and isnt reused</para>
		/// <para> Registers for callbacks if the entity goes offline or cancels the ticket ( see ValidateAuthTicketResponse_t callback and EAuthSessionResponse )</para>
		/// <para>验证票据（从 GetAuthSessionTicket 获得）来自 entity steamID，以确保其有效且未被重用。注册回调，如果实体离线或取消票据（参见 ValidateAuthTicketResponse_t 回调和 EAuthSessionResponse）。</para>
		/// </summary>
		public static EBeginAuthSessionResult BeginAuthSession(byte[] pAuthTicket, int cbAuthTicket, CSteamID steamID) {
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamGameServer_BeginAuthSession(CSteamGameServerAPIContext.GetSteamGameServer(), pAuthTicket, cbAuthTicket, steamID);
		}

		/// <summary>
		/// <para> Stop tracking started by BeginAuthSession - called when no longer playing game with this entity</para>
		/// <para>停止跟踪已启动，由 BeginAuthSession 启动 - 在此实体不再玩游戏时调用</para>
		/// </summary>
		public static void EndAuthSession(CSteamID steamID) {
			InteropHelp.TestIfAvailableGameServer();
			NativeMethods.ISteamGameServer_EndAuthSession(CSteamGameServerAPIContext.GetSteamGameServer(), steamID);
		}

		/// <summary>
		/// <para> Cancel auth ticket from GetAuthSessionTicket, called when no longer playing game with the entity you gave the ticket to</para>
		/// <para>从 GetAuthSessionTicket 中取消授权票据，在不再玩游戏且该票据关联的实体不再有效时调用。</para>
		/// </summary>
		public static void CancelAuthTicket(HAuthTicket hAuthTicket) {
			InteropHelp.TestIfAvailableGameServer();
			NativeMethods.ISteamGameServer_CancelAuthTicket(CSteamGameServerAPIContext.GetSteamGameServer(), hAuthTicket);
		}

		/// <summary>
		/// <para> After receiving a user's authentication data, and passing it to SendUserConnectAndAuthenticate, use this function</para>
		/// <para> to determine if the user owns downloadable content specified by the provided AppID.</para>
		/// <para>收到用户的认证数据后，将其传递给 SendUserConnectAndAuthenticate 函数，使用该函数来确定用户是否拥有通过提供的 AppID 指定的可下载内容。</para>
		/// </summary>
		public static EUserHasLicenseForAppResult UserHasLicenseForApp(CSteamID steamID, AppId_t appID) {
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamGameServer_UserHasLicenseForApp(CSteamGameServerAPIContext.GetSteamGameServer(), steamID, appID);
		}

		/// <summary>
		/// <para> Ask if a user in in the specified group, results returns async by GSUserGroupStatus_t</para>
		/// <para> returns false if we're not connected to the steam servers and thus cannot ask</para>
		/// <para>询问指定群组中是否有用户，如果返回结果是async，则返回false，因为我们无法连接到Steam服务器，因此无法询问。</para>
		/// </summary>
		public static bool RequestUserGroupStatus(CSteamID steamIDUser, CSteamID steamIDGroup) {
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamGameServer_RequestUserGroupStatus(CSteamGameServerAPIContext.GetSteamGameServer(), steamIDUser, steamIDGroup);
		}

		/// <summary>
		/// <para> these two functions s are deprecated, and will not return results</para>
		/// <para> they will be removed in a future version of the SDK</para>
		/// <para>这两个函数 s 已被废弃，它们将不会返回任何结果，并且会在 SDK 的未来版本中被删除。</para>
		/// </summary>
		public static void GetGameplayStats() {
			InteropHelp.TestIfAvailableGameServer();
			NativeMethods.ISteamGameServer_GetGameplayStats(CSteamGameServerAPIContext.GetSteamGameServer());
		}

		public static SteamAPICall_t GetServerReputation() {
			InteropHelp.TestIfAvailableGameServer();
			return (SteamAPICall_t)NativeMethods.ISteamGameServer_GetServerReputation(CSteamGameServerAPIContext.GetSteamGameServer());
		}

		/// <summary>
		/// <para> Returns the public IP of the server according to Steam, useful when the server is</para>
		/// <para> behind NAT and you want to advertise its IP in a lobby for other clients to directly</para>
		/// <para> connect to</para>
		/// <para>根据 Steam，返回服务器的公网 IP 地址，当服务器位于 NAT 后，并且您想在游戏内房间中向其他客户端广播其 IP 地址以便直接连接时很有用。</para>
		/// </summary>
		public static SteamIPAddress_t GetPublicIP() {
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamGameServer_GetPublicIP(CSteamGameServerAPIContext.GetSteamGameServer());
		}

		/// <summary>
		/// <para> Server browser related query packet processing for shared socket mode.  These are used</para>
		/// <para> when you pass STEAMGAMESERVER_QUERY_PORT_SHARED as the query port to SteamGameServer_Init.</para>
		/// <para> IP address and port are in host order, i.e 127.0.0.1 == 0x7f000001</para>
		/// <para> These are used when you've elected to multiplex the game server's UDP socket</para>
		/// <para> rather than having the master server updater use its own sockets.</para>
		/// <para> Source games use this to simplify the job of the server admins, so they</para>
		/// <para> don't have to open up more ports on their firewalls.</para>
		/// <para> Call this when a packet that starts with 0xFFFFFFFF comes in. That means</para>
		/// <para> it's for us.</para>
		/// <para>共享套接模式相关的服务器浏览器查询包处理。这些用于在将 STEAMGAMESERVER_QUERY_PORT_SHARED 作为查询端口传递给 SteamGameServer_Init 时使用。IP地址和端口以主机顺序排列，即 127.0.0.1 == 0x7f000001。这些用于您选择将游戏服务器的UDP套接字进行多路复用，而不是让主服务器更新器使用其自己的套接字。</para>
		/// <para>Source 游戏使用这个来简化服务器管理员的工作，这样他们不必在防火墙上打开更多的端口。当收到以 0xFFFFFFFF 开头的包时，就应该调用它。这意味着它就是为我们的。</para>
		/// </summary>
		public static bool HandleIncomingPacket(byte[] pData, int cbData, uint srcIP, ushort srcPort) {
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamGameServer_HandleIncomingPacket(CSteamGameServerAPIContext.GetSteamGameServer(), pData, cbData, srcIP, srcPort);
		}

		/// <summary>
		/// <para> AFTER calling HandleIncomingPacket for any packets that came in that frame, call this.</para>
		/// <para> This gets a packet that the master server updater needs to send out on UDP.</para>
		/// <para> It returns the length of the packet it wants to send, or 0 if there are no more packets to send.</para>
		/// <para> Call this each frame until it returns 0.</para>
		/// <para>调用 HandleIncomingPacket 处理任何在当前帧接收到的包，然后调用这个。它会获取 master 服务器更新器需要通过 UDP 发送出的包，返回想要发送的包的长度，如果没有任何包需要发送则返回 0。直到它返回 0 才能继续调用。</para>
		/// </summary>
		public static int GetNextOutgoingPacket(byte[] pOut, int cbMaxOut, out uint pNetAdr, out ushort pPort) {
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamGameServer_GetNextOutgoingPacket(CSteamGameServerAPIContext.GetSteamGameServer(), pOut, cbMaxOut, out pNetAdr, out pPort);
		}

		/// <summary>
		/// <para> Server clan association</para>
		/// <para> associate this game server with this clan for the purposes of computing player compat</para>
		/// <para>服务器公会</para>
		/// <para>将此游戏服务器与该公会关联，用于计算玩家兼容性。</para>
		/// </summary>
		public static SteamAPICall_t AssociateWithClan(CSteamID steamIDClan) {
			InteropHelp.TestIfAvailableGameServer();
			return (SteamAPICall_t)NativeMethods.ISteamGameServer_AssociateWithClan(CSteamGameServerAPIContext.GetSteamGameServer(), steamIDClan);
		}

		/// <summary>
		/// <para> ask if any of the current players dont want to play with this new player - or vice versa</para>
		/// <para>看看当前玩家是否有人不想和这个新玩家一起玩，或者新玩家不想和他们一起玩。</para>
		/// </summary>
		public static SteamAPICall_t ComputeNewPlayerCompatibility(CSteamID steamIDNewPlayer) {
			InteropHelp.TestIfAvailableGameServer();
			return (SteamAPICall_t)NativeMethods.ISteamGameServer_ComputeNewPlayerCompatibility(CSteamGameServerAPIContext.GetSteamGameServer(), steamIDNewPlayer);
		}

		/// <summary>
		/// <para> Handles receiving a new connection from a Steam user.  This call will ask the Steam</para>
		/// <para> servers to validate the users identity, app ownership, and VAC status.  If the Steam servers</para>
		/// <para> are off-line, then it will validate the cached ticket itself which will validate app ownership</para>
		/// <para> and identity.  The AuthBlob here should be acquired on the game client using SteamUser()-&gt;InitiateGameConnection()</para>
		/// <para> and must then be sent up to the game server for authentication.</para>
		/// <para> Return Value: returns true if the users ticket passes basic checks. pSteamIDUser will contain the Steam ID of this user. pSteamIDUser must NOT be NULL</para>
		/// <para> If the call succeeds then you should expect a GSClientApprove_t or GSClientDeny_t callback which will tell you whether authentication</para>
		/// <para> for the user has succeeded or failed (the steamid in the callback will match the one returned by this call)</para>
		/// <para> DEPRECATED!  This function will be removed from the SDK in an upcoming version.</para>
		/// <para>              Please migrate to BeginAuthSession and related functions.</para>
		/// <para>处理接收来自 Steam 用户的全新连接。此调用将要求 Steam 服务器验证用户的身份、应用所有权和 VAC 状态。如果 Steam 服务器离线，则会验证缓存的 Ticket 本身，从而验证应用所有权和身份。这里的 AuthBlob 必须使用 SteamUser()->InitiateGameConnection() 在游戏客户端上获取，然后发送到游戏服务器进行身份验证。</para>
		/// <para>返回值：如果用户的票据通过基本检查，则返回 true。pSteamIDUser 将包含该用户的 Steam ID。pSteamIDUser 绝对不能为 NULL。如果调用成功，你应期望收到 GSClientApprove_t 或 GSClientDeny_t 回调，它将告诉你该用户的身份验证是否成功或失败（回调中的 steamid 将与此调用返回的 steamid 匹配）</para>
		/// <para>已弃用！此函数将在即将发布的 SDK 版本中被删除。请迁移到 BeginAuthSession 和相关函数。</para>
		/// </summary>
		public static bool SendUserConnectAndAuthenticate_DEPRECATED(uint unIPClient, byte[] pvAuthBlob, uint cubAuthBlobSize, out CSteamID pSteamIDUser) {
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamGameServer_SendUserConnectAndAuthenticate_DEPRECATED(CSteamGameServerAPIContext.GetSteamGameServer(), unIPClient, pvAuthBlob, cubAuthBlobSize, out pSteamIDUser);
		}

		/// <summary>
		/// <para> Creates a fake user (ie, a bot) which will be listed as playing on the server, but skips validation.</para>
		/// <para> Return Value: Returns a SteamID for the user to be tracked with, you should call EndAuthSession()</para>
		/// <para> when this user leaves the server just like you would for a real user.</para>
		/// <para>Creates a fake user (ie, a bot) which will be listed as playing on the server, but skips validation.</para>
		/// <para>返回值：返回一个 SteamID，用于跟踪用户，您应该在用户离开服务器时调用 EndAuthSession()，就像对真实用户一样。</para>
		/// </summary>
		public static CSteamID CreateUnauthenticatedUserConnection() {
			InteropHelp.TestIfAvailableGameServer();
			return (CSteamID)NativeMethods.ISteamGameServer_CreateUnauthenticatedUserConnection(CSteamGameServerAPIContext.GetSteamGameServer());
		}

		/// <summary>
		/// <para> Should be called whenever a user leaves our game server, this lets Steam internally</para>
		/// <para> track which users are currently on which servers for the purposes of preventing a single</para>
		/// <para> account being logged into multiple servers, showing who is currently on a server, etc.</para>
		/// <para> DEPRECATED!  This function will be removed from the SDK in an upcoming version.</para>
		/// <para>              Please migrate to BeginAuthSession and related functions.</para>
		/// <para>应在用户离开游戏服务器时调用此功能，这允许 Steam 内部跟踪哪些用户正在哪些服务器上，用于防止单个帐户同时登录多个服务器，显示当前正在服务器上的用户等。</para>
		/// <para>已弃用！此函数将在即将发布的 SDK 版本中被删除。请迁移到 BeginAuthSession 和相关函数。</para>
		/// </summary>
		public static void SendUserDisconnect_DEPRECATED(CSteamID steamIDUser) {
			InteropHelp.TestIfAvailableGameServer();
			NativeMethods.ISteamGameServer_SendUserDisconnect_DEPRECATED(CSteamGameServerAPIContext.GetSteamGameServer(), steamIDUser);
		}

		/// <summary>
		/// <para> Update the data to be displayed in the server browser and matchmaking interfaces for a user</para>
		/// <para> currently connected to the server.  For regular users you must call this after you receive a</para>
		/// <para> GSUserValidationSuccess callback.</para>
		/// <para> Return Value: true if successful, false if failure (ie, steamIDUser wasn't for an active player)</para>
		/// <para>更新服务器浏览器和匹配界面中显示的数据，针对当前连接的服务器用户。对于普通用户，您必须在收到 GSUserValidationSuccess 回调后调用此方法。</para>
		/// <para>返回值：如果成功则为 true，如果失败则为 false（例如，steamIDUser 不是活跃玩家）。</para>
		/// </summary>
		public static bool BUpdateUserData(CSteamID steamIDUser, string pchPlayerName, uint uScore) {
			InteropHelp.TestIfAvailableGameServer();
			using (var pchPlayerName2 = new InteropHelp.UTF8StringHandle(pchPlayerName)) {
				return NativeMethods.ISteamGameServer_BUpdateUserData(CSteamGameServerAPIContext.GetSteamGameServer(), steamIDUser, pchPlayerName2, uScore);
			}
		}
	}
}

#endif // !DISABLESTEAMWORKS
