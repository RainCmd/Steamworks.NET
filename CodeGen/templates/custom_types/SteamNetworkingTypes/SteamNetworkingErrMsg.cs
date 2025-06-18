namespace Steamworks
{
	/// Used to return English-language diagnostic error messages to caller.
	/// (For debugging or spewing to a console, etc.  Not intended for UI.)
	/// 用于向调用者返回英语诊断错误消息。(用于调试或喷吐到控制台等。不是用于UI的。)
	[System.Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public struct SteamNetworkingErrMsg
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = Constants.k_cchMaxSteamNetworkingErrMsg)]
		public byte[] m_SteamNetworkingErrMsg;
	}
}

#endif // !DISABLESTEAMWORKS