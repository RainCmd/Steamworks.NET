namespace Steamworks
{
	/// In a few places we need to set configuration options on listen sockets and connections, and
	/// have them take effect *before* the listen socket or connection really starts doing anything.
	/// Creating the object and then setting the options "immediately" after creation doesn't work
	/// completely, because network packets could be received between the time the object is created and
	/// when the options are applied.  To set options at creation time in a reliable way, they must be
	/// passed to the creation function.  This structure is used to pass those options.
	/// 在一些地方，我们需要设置侦听套接字和连接的配置选项，并让它们在侦听套接字或连接真正开始做任何事情之前生效。
	/// 创建对象然后在创建后“立即”设置选项并不完全有效，因为在创建对象和应用选项之间可能会接收网络数据包。
	/// 为了在创建时以可靠的方式设置选项，必须将它们传递给创建函数。该结构用于传递这些选项。
	///
	/// For the meaning of these fields, see ISteamNetworkingUtils::SetConfigValue.  Basically
	/// when the object is created, we just iterate over the list of options and call
	/// ISteamNetworkingUtils::SetConfigValueStruct, where the scope arguments are supplied by the
	/// object being created.
	/// 这些字段的含义请参见ISteamNetworkingUtils::SetConfigValue。
	/// 基本上，当对象被创建时，我们只是遍历选项列表并调用ISteamNetworkingUtils::SetConfigValueStruct，
	/// 其中范围参数由被创建的对象提供。
	[System.Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public struct SteamNetworkingConfigValue_t
	{
		/// Which option is being set 设置了哪个选项
		public ESteamNetworkingConfigValue m_eValue;

		/// Which field below did you fill in? 下面哪个字段是你填的？
		public ESteamNetworkingConfigDataType m_eDataType;

		/// Option value
		public OptionValue m_val;

		[StructLayout(LayoutKind.Explicit)]
		public struct OptionValue
		{
			[FieldOffset(0)]
			public int m_int32;

			[FieldOffset(0)]
			public long m_int64;

			[FieldOffset(0)]
			public float m_float;

			[FieldOffset(0)]
			public IntPtr m_string; // Points to your '\0'-terminated buffer 指向以‘\0’结尾的缓冲区

			[FieldOffset(0)]
			public IntPtr m_functionPtr;
		}
	}
}

#endif // !DISABLESTEAMWORKS