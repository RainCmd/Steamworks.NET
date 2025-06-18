namespace Steamworks
{
	/// An abstract way to represent the identity of a network host.  All identities can
	/// be represented as simple string.  Furthermore, this string representation is actually
	/// used on the wire in several places, even though it is less efficient, in order to
	/// facilitate forward compatibility.  (Old client code can handle an identity type that
	/// it doesn't understand.)
	/// 表示网络主机身份的一种抽象方式。所有的标识都可以用简单的字符串表示。此外，为了促进前向兼容性，
	/// 这种字符串表示实际上在几个地方使用，尽管效率较低。（旧的客户端代码可以处理它不理解的标识类型。）
	[System.Serializable]
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct SteamNetworkingIdentity : System.IEquatable<SteamNetworkingIdentity>
	{
		/// Type of identity.
		/// 身份类型。
		public ESteamNetworkingIdentityType m_eType;

		//
		// Internal representation.  Don't access this directly, use the accessors!
		// 内部表示。不要直接访问它，使用访问器！
		//
		// Number of bytes that are relevant below.  This MUST ALWAYS be
		// set.  (Use the accessors!)  This is important to enable old code to work
		// with new identity types.
		// 下面是相关的字节数。这必须总是被设置。（使用访问器！）
		// 这对于使旧代码能够使用新的标识类型非常重要。
		private int m_cbSize;

		// Note this is written out as such because we want this to be a blittable/unmanaged type.
		// 注意，之所以这样写是因为我们希望它是一个可篡改/非托管类型。
		private uint m_reserved0; // Pad structure to leave easy room for future expansion 垫层结构为将来的扩展留下方便的空间
		private uint m_reserved1;
		private uint m_reserved2;
		private uint m_reserved3;
		private uint m_reserved4;
		private uint m_reserved5;
		private uint m_reserved6;
		private uint m_reserved7;
		private uint m_reserved8;
		private uint m_reserved9;
		private uint m_reserved10;
		private uint m_reserved11;
		private uint m_reserved12;
		private uint m_reserved13;
		private uint m_reserved14;
		private uint m_reserved15;
		private uint m_reserved16;
		private uint m_reserved17;
		private uint m_reserved18;
		private uint m_reserved19;
		private uint m_reserved20;
		private uint m_reserved21;
		private uint m_reserved22;
		private uint m_reserved23;
		private uint m_reserved24;
		private uint m_reserved25;
		private uint m_reserved26;
		private uint m_reserved27;
		private uint m_reserved28;
		private uint m_reserved29;
		private uint m_reserved30;
		private uint m_reserved31;

		// Max sizes
		// Max length of the buffer needed to hold any identity, formatted in string format by ToString
		// 保存任何标识所需的缓冲区的最大长度，由ToString格式化为字符串格式
		public const int k_cchMaxString = 128;
		// Max length of the string for generic string identities.  Including terminating '\0'
		// 通用字符串标识的字符串的最大长度。包括终止‘\0’
		public const int k_cchMaxGenericString = 32;
		public const int k_cchMaxXboxPairwiseID = 33; // Including terminating '\0' 包括终止‘\0’
		public const int k_cbMaxGenericBytes = 32;

		//
		// Get/Set in various formats.
		// 以各种格式获取/设置。
		//

		public void Clear() {
			NativeMethods.SteamAPI_SteamNetworkingIdentity_Clear(ref this);
		}

		// Return true if we are the invalid type.  Does not make any other validity checks (e.g. is SteamID actually valid)
		// 如果是无效类型则返回true。没有进行任何其他有效性检查（例如，SteamID是否实际有效）
		public bool IsInvalid() {
			return NativeMethods.SteamAPI_SteamNetworkingIdentity_IsInvalid(ref this);
		}

		public void SetSteamID(CSteamID steamID) {
			NativeMethods.SteamAPI_SteamNetworkingIdentity_SetSteamID(ref this, (ulong)steamID);
		}

		// Return black CSteamID (!IsValid()) if identity is not a SteamID
		// 返回黑色CSteamID （!IsValid()）如果标识不是SteamID
		public CSteamID GetSteamID() {
			return (CSteamID)NativeMethods.SteamAPI_SteamNetworkingIdentity_GetSteamID(ref this);
		}

		// Takes SteamID as raw 64-bit number
		// 将SteamID作为原始64位数字
		public void SetSteamID64(ulong steamID) {
			NativeMethods.SteamAPI_SteamNetworkingIdentity_SetSteamID64(ref this, steamID);
		}

		// Returns 0 if identity is not SteamID
		// 如果标识不是SteamID则返回0
		public ulong GetSteamID64() {
			return NativeMethods.SteamAPI_SteamNetworkingIdentity_GetSteamID64(ref this);
		}

		// Returns false if invalid length
		// 如果长度无效返回false
		public bool SetXboxPairwiseID(string pszString)
		{
			using (var pszString2 = new InteropHelp.UTF8StringHandle(pszString)) {
				return NativeMethods.SteamAPI_SteamNetworkingIdentity_SetXboxPairwiseID(ref this, pszString2);
			}
		}

		// Returns nullptr if not Xbox ID
		// 如果不是Xbox ID则返回nullptr
		public string GetXboxPairwiseID()
		{
			return InteropHelp.PtrToStringUTF8(NativeMethods.SteamAPI_SteamNetworkingIdentity_GetXboxPairwiseID(ref this));
		}

		public void SetPSNID(ulong id)
		{
			NativeMethods.SteamAPI_SteamNetworkingIdentity_SetPSNID(ref this, id);
		}

		// Returns 0 if not PSN
		// 如果不是PSN则返回0
		public ulong GetPSNID()
		{
			return NativeMethods.SteamAPI_SteamNetworkingIdentity_GetPSNID(ref this);
		}

		// Set to specified IP:port
		// 设置为指定的IP：端口
		public void SetIPAddr(SteamNetworkingIPAddr addr) {
			NativeMethods.SteamAPI_SteamNetworkingIdentity_SetIPAddr(ref this, ref addr);
		}

		// returns null if we are not an IP address.
		// 如果不是IP地址，则返回null。
		public SteamNetworkingIPAddr GetIPAddr(){
			throw new System.NotImplementedException();
			// TODO: Should SteamNetworkingIPAddr be a class?
			//       or should this return some kind of pointer instead?
			//      SteamNetworkingIPAddr应该是一个类吗？还是应该返回某种指针？
			//return NativeMethods.SteamAPI_SteamNetworkingIdentity_GetIPAddr(ref this);
		}

		public void SetIPv4Addr(uint nIPv4, ushort nPort) {
			NativeMethods.SteamAPI_SteamNetworkingIdentity_SetIPv4Addr(ref this, nIPv4, nPort);
		}

		// returns 0 if we are not an IPv4 address.
		// 如果不是IPv4地址，则返回0。
		public uint GetIPv4() {
			return NativeMethods.SteamAPI_SteamNetworkingIdentity_GetIPv4(ref this);
		}

		public ESteamNetworkingFakeIPType GetFakeIPType() {
			return NativeMethods.SteamAPI_SteamNetworkingIdentity_GetFakeIPType(ref this);
		}

		public bool IsFakeIP() {
			return GetFakeIPType() > ESteamNetworkingFakeIPType.k_ESteamNetworkingFakeIPType_NotFake;
		}

		// "localhost" is equivalent for many purposes to "anonymous."  Our remote
		// will identify us by the network address we use.
		// Set to localhost.  (We always use IPv6 ::1 for this, not 127.0.0.1)
		// “localhost”在许多用途上等同于“anonymous”。
		// 我们的遥控器将通过我们使用的网络地址来识别我们。
		// 设置为localhost。（我们总是使用IPv6::1，而不是127.0.0.1）
		public void SetLocalHost() {
			NativeMethods.SteamAPI_SteamNetworkingIdentity_SetLocalHost(ref this);
		}

		// Return true if this identity is localhost.
		// 如果此标识为localhost，则返回true。
		public bool IsLocalHost() {
			return NativeMethods.SteamAPI_SteamNetworkingIdentity_IsLocalHost(ref this);
		}

		// Returns false if invalid length
		// 如果长度无效返回false
		public bool SetGenericString(string pszString) {
			using (var pszString2 = new InteropHelp.UTF8StringHandle(pszString)) {
				return NativeMethods.SteamAPI_SteamNetworkingIdentity_SetGenericString(ref this, pszString2);
			}
		}

		// Returns nullptr if not generic string type
		// 如果不是泛型字符串类型，则返回nullptr
		public string GetGenericString() {
			return InteropHelp.PtrToStringUTF8(NativeMethods.SteamAPI_SteamNetworkingIdentity_GetGenericString(ref this));
		}

		// Returns false if invalid size.
		// 如果大小无效则返回false。
		public bool SetGenericBytes(byte[] data, uint cbLen) {
			return NativeMethods.SteamAPI_SteamNetworkingIdentity_SetGenericBytes(ref this, data, cbLen);
		}

		// Returns null if not generic bytes type
		// 如果不是泛型字节类型则返回null
		public byte[] GetGenericBytes(out int cbLen) {
			throw new System.NotImplementedException();
			//return NativeMethods.SteamAPI_SteamNetworkingIdentity_GetGenericBytes(ref this, out cbLen);
		}

		/// See if two identities are identical
		/// 看看两个恒等式是否相同
		public bool Equals(SteamNetworkingIdentity x) {
			return NativeMethods.SteamAPI_SteamNetworkingIdentity_IsEqualTo(ref this, ref x);
		}

		/// Print to a human-readable string.  This is suitable for debug messages
		/// or any other time you need to encode the identity as a string.  It has a
		/// URL-like format (type:<type-data>).  Your buffer should be at least
		/// k_cchMaxString bytes big to avoid truncation.
		/// 打印为人类可读的字符串。这适用于调试消息或任何其他需要将标识编码为字符串的情况。
		/// 它有一个类似url的格式（类型：<type-data>）。您的缓冲区应该至少有k_cchMaxString字节大，以避免截断。
		///
		/// See also SteamNetworkingIPAddrRender
		public void ToString(out string buf) {
			IntPtr buf2 = Marshal.AllocHGlobal(k_cchMaxString);
			NativeMethods.SteamAPI_SteamNetworkingIdentity_ToString(ref this, buf2, k_cchMaxString);
			buf = InteropHelp.PtrToStringUTF8(buf2);
			Marshal.FreeHGlobal(buf2);
		}

		/// Parse back a string that was generated using ToString.  If we don't understand the
		/// string, but it looks "reasonable" (it matches the pattern type:<type-data> and doesn't
		/// have any funky characters, etc), then we will return true, and the type is set to
		/// k_ESteamNetworkingIdentityType_UnknownType.  false will only be returned if the string
		/// looks invalid.
		/// 解析使用ToString生成的字符串。如果我们不理解字符串，但它看起来“合理”
		/// （它匹配模式类型：<type-data>并且没有任何奇怪的字符，等等），
		/// 那么我们将返回true，并且类型设置为k_ESteamNetworkingIdentityType_UnknownType。
		/// 只有当字符串看起来无效时才会返回False。
		public bool ParseString(string pszStr) {
			using (var pszStr2 = new InteropHelp.UTF8StringHandle(pszStr)) {
				return NativeMethods.SteamAPI_SteamNetworkingIdentity_ParseString(ref this, pszStr2);
			}
		}
	}
}

#endif // !DISABLESTEAMWORKS
