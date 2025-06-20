﻿// This file is provided under The MIT License as part of Steamworks.NET.
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

﻿using System.Text;

namespace Steamworks {
	//-----------------------------------------------------------------------------
	// Purpose: Data describing a single server
	//-----------------------------------------------------------------------------
	[StructLayout(LayoutKind.Sequential, Size = 372, Pack = 4)]
	[System.Serializable]
	public class gameserveritem_t {
		public string GetGameDir() {
			return Encoding.UTF8.GetString(m_szGameDir, 0, System.Array.IndexOf<byte>(m_szGameDir, 0));
		}

		public void SetGameDir(string dir) {
			m_szGameDir = Encoding.UTF8.GetBytes(dir + '\0');
		}

		public string GetMap() {
			return Encoding.UTF8.GetString(m_szMap, 0, System.Array.IndexOf<byte>(m_szMap, 0));
		}

		public void SetMap(string map) {
			m_szMap = Encoding.UTF8.GetBytes(map + '\0');
		}

		public string GetGameDescription() {
			return Encoding.UTF8.GetString(m_szGameDescription, 0, System.Array.IndexOf<byte>(m_szGameDescription, 0));
		}

		public void SetGameDescription(string desc) {
			m_szGameDescription = Encoding.UTF8.GetBytes(desc + '\0');
		}

		public string GetServerName() {
			// Use the IP address as the name if nothing is set yet.
			// 如果尚未设置任何内容，则使用IP地址作为名称。
			if (m_szServerName[0] == 0)
				return m_NetAdr.GetConnectionAddressString();
			else
				return Encoding.UTF8.GetString(m_szServerName, 0, System.Array.IndexOf<byte>(m_szServerName, 0));
		}

		public void SetServerName(string name) {
			m_szServerName = Encoding.UTF8.GetBytes(name + '\0');
		}

		public string GetGameTags() {
			return Encoding.UTF8.GetString(m_szGameTags, 0, System.Array.IndexOf<byte>(m_szGameTags, 0));
		}

		public void SetGameTags(string tags) {
			m_szGameTags = Encoding.UTF8.GetBytes(tags + '\0');
		}

		public servernetadr_t m_NetAdr;										///< IP/Query Port/Connection Port for this server IP/查询端口/连接端口
		public int m_nPing;													///< current ping time in milliseconds 当前ping时间，以毫秒为单位
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bHadSuccessfulResponse;								///< server has responded successfully in the past 服务器过去已成功响应
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bDoNotRefresh;										///< server is marked as not responding and should no longer be refreshed 服务器被标记为没有响应，不应该再刷新
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = Constants.k_cbMaxGameServerGameDir)]
		private byte[] m_szGameDir;											///< current game directory 当前游戏目录
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = Constants.k_cbMaxGameServerMapName)]
		private byte[] m_szMap;												///< current map
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = Constants.k_cbMaxGameServerGameDescription)]
		private byte[] m_szGameDescription;									///< game description 游戏描述
		public uint m_nAppID;												///< Steam App ID of this server 该服务器的Steam应用ID
		public int m_nPlayers;												///< total number of players currently on the server.  INCLUDES BOTS!! 当前服务器上的玩家总数。包括机器人!
		public int m_nMaxPlayers;											///< Maximum players that can join this server 可以加入此服务器的最大玩家数
		public int m_nBotPlayers;											///< Number of bots (i.e simulated players) on this server 这个服务器上的bot（即模拟玩家）的数量
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bPassword;											///< true if this server needs a password to join 如果此服务器需要密码才能加入，则为True
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bSecure;												///< Is this server protected by VAC 这个服务器受VAC保护吗
		public uint m_ulTimeLastPlayed;										///< time (in unix time) when this server was last played on (for favorite/history servers) 该服务器最后一次运行的时间（以Unix时间为单位）（用于收藏/历史记录服务器）
		public int	m_nServerVersion;										///< server version as reported to Steam 服务器版本报告给Steam

		// Game server name
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = Constants.k_cbMaxGameServerName)]
		private byte[] m_szServerName;

		// the tags this server exposes 
		// 此服务器公开的标记
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = Constants.k_cbMaxGameServerTags)]
		private byte[] m_szGameTags;

		// steamID of the game server - invalid if it's doesn't have one (old server, or not connected to Steam) 
		// 游戏服务器的Steam -如果没有（旧服务器，或者没有连接到Steam），则无效
		public CSteamID m_steamID;
	}
}

#endif // !DISABLESTEAMWORKS
