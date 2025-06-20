namespace Steamworks {
	[System.Serializable]
	[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 4)]
	public struct CSteamID : System.IEquatable<CSteamID>, System.IComparable<CSteamID> {
		public static readonly CSteamID Nil = new CSteamID();
		public static readonly CSteamID OutofDateGS = new CSteamID(new AccountID_t(0), 0, EUniverse.k_EUniverseInvalid, EAccountType.k_EAccountTypeInvalid);
		public static readonly CSteamID LanModeGS = new CSteamID(new AccountID_t(0), 0, EUniverse.k_EUniversePublic, EAccountType.k_EAccountTypeInvalid);
		public static readonly CSteamID NotInitYetGS = new CSteamID(new AccountID_t(1), 0, EUniverse.k_EUniverseInvalid, EAccountType.k_EAccountTypeInvalid);
		public static readonly CSteamID NonSteamGS = new CSteamID(new AccountID_t(2), 0, EUniverse.k_EUniverseInvalid, EAccountType.k_EAccountTypeInvalid);
		public ulong m_SteamID;

		public CSteamID(AccountID_t unAccountID, EUniverse eUniverse, EAccountType eAccountType) {
			m_SteamID = 0;
			Set(unAccountID, eUniverse, eAccountType);
		}

		public CSteamID(AccountID_t unAccountID, uint unAccountInstance, EUniverse eUniverse, EAccountType eAccountType) {
			m_SteamID = 0;
#if _SERVER && Assert
		Assert( ! ( ( EAccountType.k_EAccountTypeIndividual == eAccountType ) && ( unAccountInstance > k_unSteamUserWebInstance ) ) );	// enforce that for individual accounts, instance is always 1
#endif // _SERVER
			InstancedSet(unAccountID, unAccountInstance, eUniverse, eAccountType);
		}

		public CSteamID(ulong ulSteamID) {
			m_SteamID = ulSteamID;
		}

		public void Set(AccountID_t unAccountID, EUniverse eUniverse, EAccountType eAccountType) {
			SetAccountID(unAccountID);
			SetEUniverse(eUniverse);
			SetEAccountType(eAccountType);

			if (eAccountType == EAccountType.k_EAccountTypeClan || eAccountType == EAccountType.k_EAccountTypeGameServer) {
				SetAccountInstance(0);
			}
			else {
				SetAccountInstance(Constants.k_unSteamUserDefaultInstance);
			}
		}

		public void InstancedSet(AccountID_t unAccountID, uint unInstance, EUniverse eUniverse, EAccountType eAccountType) {
			SetAccountID(unAccountID);
			SetEUniverse(eUniverse);
			SetEAccountType(eAccountType);
			SetAccountInstance(unInstance);
		}

		public void Clear() {
			m_SteamID = 0;
		}

		public void CreateBlankAnonLogon(EUniverse eUniverse) {
			SetAccountID(new AccountID_t(0));
			SetEUniverse(eUniverse);
			SetEAccountType(EAccountType.k_EAccountTypeAnonGameServer);
			SetAccountInstance(0);
		}

		public void CreateBlankAnonUserLogon(EUniverse eUniverse) {
			SetAccountID(new AccountID_t(0));
			SetEUniverse(eUniverse);
			SetEAccountType(EAccountType.k_EAccountTypeAnonUser);
			SetAccountInstance(0);
		}

		//-----------------------------------------------------------------------------
		// Purpose: Is this an anonymous game server login that will be filled in?
		// 目的：这是一个将被填写的匿名游戏服务器登录吗？
		//-----------------------------------------------------------------------------
		public bool BBlankAnonAccount() {
			return GetAccountID() == new AccountID_t(0) && BAnonAccount() && GetUnAccountInstance() == 0;
		}

		//-----------------------------------------------------------------------------
		// Purpose: Is this a game server account id?  (Either persistent or anonymous)
		// 目的：这是一个游戏服务器账号id吗？（持久化或匿名）
		//-----------------------------------------------------------------------------
		public bool BGameServerAccount() {
			return GetEAccountType() == EAccountType.k_EAccountTypeGameServer || GetEAccountType() == EAccountType.k_EAccountTypeAnonGameServer;
		}

		//-----------------------------------------------------------------------------
		// Purpose: Is this a persistent (not anonymous) game server account id?
		// 目的：这是一个持久的（不是匿名的）游戏服务器账号id吗？
		//-----------------------------------------------------------------------------
		public bool BPersistentGameServerAccount() {
			return GetEAccountType() == EAccountType.k_EAccountTypeGameServer;
		}

		//-----------------------------------------------------------------------------
		// Purpose: Is this an anonymous game server account id?
		// 目的：这是一个匿名的游戏服务器账号id吗？
		//-----------------------------------------------------------------------------
		public bool BAnonGameServerAccount() {
			return GetEAccountType() == EAccountType.k_EAccountTypeAnonGameServer;
		}

		//-----------------------------------------------------------------------------
		// Purpose: Is this a content server account id?
		// 目的：这是一个内容服务器帐户id吗？
		//-----------------------------------------------------------------------------
		public bool BContentServerAccount() {
			return GetEAccountType() == EAccountType.k_EAccountTypeContentServer;
		}


		//-----------------------------------------------------------------------------
		// Purpose: Is this a clan account id?
		// 目的：这是一个部落账号id吗？
		//-----------------------------------------------------------------------------
		public bool BClanAccount() {
			return GetEAccountType() == EAccountType.k_EAccountTypeClan;
		}


		//-----------------------------------------------------------------------------
		// Purpose: Is this a chat account id?
		// 目的：这是一个聊天账号id吗？
		//-----------------------------------------------------------------------------
		public bool BChatAccount() {
			return GetEAccountType() == EAccountType.k_EAccountTypeChat;
		}

		//-----------------------------------------------------------------------------
		// Purpose: Is this a chat account id?
		// 目的：这是一个聊天账号id吗？
		//-----------------------------------------------------------------------------
		public bool IsLobby() {
			return (GetEAccountType() == EAccountType.k_EAccountTypeChat)
				&& (GetUnAccountInstance() & (int)EChatSteamIDInstanceFlags.k_EChatInstanceFlagLobby) != 0;
		}


		//-----------------------------------------------------------------------------
		// Purpose: Is this an individual user account id?
		// 目的：这是一个个人用户帐户id吗？
		//-----------------------------------------------------------------------------
		public bool BIndividualAccount() {
			return GetEAccountType() == EAccountType.k_EAccountTypeIndividual || GetEAccountType() == EAccountType.k_EAccountTypeConsoleUser;
		}


		//-----------------------------------------------------------------------------
		// Purpose: Is this an anonymous account?
		// 目的：这是一个匿名账户吗？
		//-----------------------------------------------------------------------------
		public bool BAnonAccount() {
			return GetEAccountType() == EAccountType.k_EAccountTypeAnonUser || GetEAccountType() == EAccountType.k_EAccountTypeAnonGameServer;
		}

		//-----------------------------------------------------------------------------
		// Purpose: Is this an anonymous user account? ( used to create an account or reset a password )
		// 目的：这是一个匿名用户帐户吗？（用于创建帐户或重置密码）
		//-----------------------------------------------------------------------------
		public bool BAnonUserAccount() {
			return GetEAccountType() == EAccountType.k_EAccountTypeAnonUser;
		}

		//-----------------------------------------------------------------------------
		// Purpose: Is this a faked up Steam ID for a PSN friend account?
		// 目的：这是一个伪造的PSN好友帐号的Steam ID吗？
		//-----------------------------------------------------------------------------
		public bool BConsoleUserAccount() {
			return GetEAccountType() == EAccountType.k_EAccountTypeConsoleUser;
		}

		public void SetAccountID(AccountID_t other) {
			m_SteamID = (m_SteamID & ~(0xFFFFFFFFul << (ushort)0)) | (((ulong)(other) & 0xFFFFFFFFul) << (ushort)0);
		}

		public void SetAccountInstance(uint other) {
			m_SteamID = (m_SteamID & ~(0xFFFFFul << (ushort)32)) | (((ulong)(other) & 0xFFFFFul) << (ushort)32);
		}

		// This is a non standard/custom function not found in C++ Steamworks
		// 这是一个在c++ Steamworks中找不到的非标准/自定义函数
		public void SetEAccountType(EAccountType other) {
			m_SteamID = (m_SteamID & ~(0xFul << (ushort)52)) | (((ulong)(other) & 0xFul) << (ushort)52);
		}

		public void SetEUniverse(EUniverse other) {
			m_SteamID = (m_SteamID & ~(0xFFul << (ushort)56)) | (((ulong)(other) & 0xFFul) << (ushort)56);
		}

		public AccountID_t GetAccountID() {
			return new AccountID_t((uint)(m_SteamID & 0xFFFFFFFFul));
		}

		public uint GetUnAccountInstance() {
			return (uint)((m_SteamID >> 32) & 0xFFFFFul);
		}

		public EAccountType GetEAccountType() {
			return (EAccountType)((m_SteamID >> 52) & 0xFul);
		}

		public EUniverse GetEUniverse() {
			return (EUniverse)((m_SteamID >> 56) & 0xFFul);
		}

		public bool IsValid() {
			if (GetEAccountType() <= EAccountType.k_EAccountTypeInvalid || GetEAccountType() >= EAccountType.k_EAccountTypeMax)
				return false;

			if (GetEUniverse() <= EUniverse.k_EUniverseInvalid || GetEUniverse() >= EUniverse.k_EUniverseMax)
				return false;

			if (GetEAccountType() == EAccountType.k_EAccountTypeIndividual) {
				if (GetAccountID() == new AccountID_t(0) || GetUnAccountInstance() > Constants.k_unSteamUserDefaultInstance)
					return false;
			}

			if (GetEAccountType() == EAccountType.k_EAccountTypeClan) {
				if (GetAccountID() == new AccountID_t(0) || GetUnAccountInstance() != 0)
					return false;
			}

			if (GetEAccountType() == EAccountType.k_EAccountTypeGameServer) {
				if (GetAccountID() == new AccountID_t(0))
					return false;
				// Any limit on instances?  We use them for local users and bots
				// 对实例有限制吗？我们将它们用于本地用户和机器人
			}
			return true;
		}

		#region Overrides
		public override string ToString() {
			return m_SteamID.ToString();
		}

		public override bool Equals(object other) {
			return other is CSteamID && this == (CSteamID)other;
		}

		public override int GetHashCode() {
			return m_SteamID.GetHashCode();
		}

		public static bool operator ==(CSteamID x, CSteamID y) {
			return x.m_SteamID == y.m_SteamID;
		}

		public static bool operator !=(CSteamID x, CSteamID y) {
			return !(x == y);
		}

		public static explicit operator CSteamID(ulong value) {
			return new CSteamID(value);
		}
		public static explicit operator ulong(CSteamID that) {
			return that.m_SteamID;
		}

		public bool Equals(CSteamID other) {
			return m_SteamID == other.m_SteamID;
		}

		public int CompareTo(CSteamID other) {
			return m_SteamID.CompareTo(other.m_SteamID);
		}
		#endregion
	}
}

#endif // !DISABLESTEAMWORKS
