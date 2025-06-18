namespace Steamworks {
	/// Setup callback for debug output, and the desired verbosity you want.
	/// 为调试输出设置回调，以及所需的详细程度。
	[System.Runtime.InteropServices.UnmanagedFunctionPointer(System.Runtime.InteropServices.CallingConvention.Cdecl)]
	public delegate void FSteamNetworkingSocketsDebugOutput(ESteamNetworkingSocketsDebugOutputType nType, System.Text.StringBuilder pszMsg);
}

#endif // !DISABLESTEAMWORKS
