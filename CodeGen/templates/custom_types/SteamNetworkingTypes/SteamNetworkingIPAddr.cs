namespace Steamworks
{
	/// Store an IP and port.  IPv6 is always used; IPv4 is represented using
	/// "IPv4-mapped" addresses: IPv4 aa.bb.cc.dd => IPv6 ::ffff:aabb:ccdd
	/// (RFC 4291 section 2.5.5.2.)
	/// 存储IP和端口。总是使用IPv6；IPv4使用“IPv4映射”
	/// 地址表示：IPv4 aa.bb.cc.dd => IPv6::ffff:aabb:ccdd
	/// （RFC 4291 section 2.5.5.2）。
	[System.Serializable]
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct SteamNetworkingIPAddr : System.IEquatable<SteamNetworkingIPAddr>
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
		public byte[] m_ipv6;
		public ushort m_port; // Host byte order 主机字节顺序

		// Max length of the buffer needed to hold IP formatted using ToString, including '\0'
		// 保存使用ToString格式化的IP所需的最大缓冲区长度，包括‘\0’
		// ([0123:4567:89ab:cdef:0123:4567:89ab:cdef]:12345)
		public const int k_cchMaxString = 48;

		 // Set everything to zero.  E.g. [::]:0
		 // 把所有东西都设为0。例如:[:]:0
		public void Clear() {
			NativeMethods.SteamAPI_SteamNetworkingIPAddr_Clear(ref this);
		}

		// Return true if the IP is ::0.  (Doesn't check port.)
		// 如果IP为：：0，则返回true。（不检查端口。）
		public bool IsIPv6AllZeros() {
			return NativeMethods.SteamAPI_SteamNetworkingIPAddr_IsIPv6AllZeros(ref this);
		}

		// Set IPv6 address.  IP is interpreted as bytes, so there are no endian issues.  (Same as inaddr_in6.)  The IP can be a mapped IPv4 address
		// 配置IPv6地址。IP被解释为字节，所以没有尾端问题。（与inaddr_in6相同。）IP地址可以是映射的IPv4地址
		public void SetIPv6(byte[] ipv6, ushort nPort) {
			NativeMethods.SteamAPI_SteamNetworkingIPAddr_SetIPv6(ref this, ipv6, nPort);
		}

		// Sets to IPv4 mapped address.  IP and port are in host byte order.
		// 设置为IPv4映射地址。IP和端口按主机字节顺序排列。
		public void SetIPv4(uint nIP, ushort nPort) {
			NativeMethods.SteamAPI_SteamNetworkingIPAddr_SetIPv4(ref this, nIP, nPort);
		}

		// Return true if IP is mapped IPv4
		// 如果IP映射为IPv4，返回true
		public bool IsIPv4() {
			return NativeMethods.SteamAPI_SteamNetworkingIPAddr_IsIPv4(ref this);
		}

		// Returns IP in host byte order (e.g. aa.bb.cc.dd as 0xaabbccdd).  Returns 0 if IP is not mapped IPv4.
		// 返回主机字节顺序的IP（例如aa.bb.cc.dd为0xaabbccdd）。如果IP没有映射到IPv4，返回0。
		public uint GetIPv4() {
			return NativeMethods.SteamAPI_SteamNetworkingIPAddr_GetIPv4(ref this);
		}

		// Set to the IPv6 localhost address ::1, and the specified port.
		// 设置为IPv6本地主机地址：：1，指定端口。
		public void SetIPv6LocalHost(ushort nPort = 0) {
			NativeMethods.SteamAPI_SteamNetworkingIPAddr_SetIPv6LocalHost(ref this, nPort);
		}

		// Return true if this identity is localhost.  (Either IPv6 ::1, or IPv4 127.0.0.1)
		// 如果此标识为localhost，则返回true。（IPv6::1或IPv4 127.0.0.1）
		public bool IsLocalHost() {
			return NativeMethods.SteamAPI_SteamNetworkingIPAddr_IsLocalHost(ref this);
		}

		/// Print to a string, with or without the port.  Mapped IPv4 addresses are printed
		/// as dotted decimal (12.34.56.78), otherwise this will print the canonical
		/// form according to RFC5952.  If you include the port, IPv6 will be surrounded by
		/// brackets, e.g. [::1:2]:80.  Your buffer should be at least k_cchMaxString bytes
		/// to avoid truncation
		/// 打印为字符串，带或不带端口。映射的IPv4地址被打印为点分十进制（12.34.56.78），
		/// 否则将根据RFC5952打印规范化形式。如果包含端口，IPv6将被括号括起来，
		/// 例如[::1:2]:80。您的缓冲区应该至少是k_cchMaxString字节，以避免截断
		///
		/// See also SteamNetworkingIdentityRender
		public void ToString(out string buf, bool bWithPort) {
			IntPtr buf2 = Marshal.AllocHGlobal(k_cchMaxString);
			NativeMethods.SteamAPI_SteamNetworkingIPAddr_ToString(ref this, buf2, k_cchMaxString, bWithPort);
			buf = InteropHelp.PtrToStringUTF8(buf2);
			Marshal.FreeHGlobal(buf2);
		}

		/// Parse an IP address and optional port.  If a port is not present, it is set to 0.
		/// (This means that you cannot tell if a zero port was explicitly specified.)
		/// 解析IP地址和可选端口。如果端口不存在，则设置为0。（这意味着您无法判断是否显式指定了零端口。）
		public bool ParseString(string pszStr) {
			using (var pszStr2 = new InteropHelp.UTF8StringHandle(pszStr)) {
				return NativeMethods.SteamAPI_SteamNetworkingIPAddr_ParseString(ref this, pszStr2);
			}
		}

		/// See if two addresses are identical
		/// 看看两个地址是否相同
		public bool Equals(SteamNetworkingIPAddr x) {
			return NativeMethods.SteamAPI_SteamNetworkingIPAddr_IsEqualTo(ref this, ref x);
		}

		/// Classify address as FakeIP.  This function never returns
		/// k_ESteamNetworkingFakeIPType_Invalid.
		/// 将地址分类为FakeIP。这个函数永远不会返回k_ESteamNetworkingFakeIPType_Invalid。
		public ESteamNetworkingFakeIPType GetFakeIPType() {
			return NativeMethods.SteamAPI_SteamNetworkingIPAddr_GetFakeIPType(ref this);
		}

		/// Return true if we are a FakeIP
		/// 如果我们是FakeIP返回true
		public bool IsFakeIP() {
			return GetFakeIPType() > ESteamNetworkingFakeIPType.k_ESteamNetworkingFakeIPType_NotFake;
		}
	}
}

#endif // !DISABLESTEAMWORKS
