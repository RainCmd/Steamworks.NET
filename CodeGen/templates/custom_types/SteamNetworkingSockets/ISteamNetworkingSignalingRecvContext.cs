namespace Steamworks
{
	/// Interface used when a custom signal is received.
	/// See ISteamNetworkingSockets::ReceivedP2PCustomSignal
	/// 接收自定义信号时使用的接口。
	[System.Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public struct ISteamNetworkingSignalingRecvContext
	{
		/// Called when the signal represents a request for a new connection.
		/// 当信号表示对新连接的请求时调用。
		///
		/// If you want to ignore the request, just return NULL.  In this case,
		/// the peer will NOT receive any reply.  You should consider ignoring
		/// requests rather than actively rejecting them, as a security measure.
		/// If you actively reject requests, then this makes it possible to detect
		/// if a user is online or not, just by sending them a request.
		/// 如果您想忽略请求，只需返回NULL。在这种情况下，对等体将不会收到任何回复。
		/// 作为一种安全措施，您应该考虑忽略请求，而不是主动拒绝它们。
		/// 如果您主动拒绝请求，那么就可以通过向用户发送请求来检测用户是否在线。
		///
		/// If you wish to send back a rejection, then use
		/// ISteamNetworkingSockets::CloseConnection() and then return NULL.
		/// We will marshal a properly formatted rejection signal and
		/// call SendRejectionSignal() so you can send it to them.
		/// 如果你想发送回一个拒绝，那么使用ISteamNetworkingSockets::CloseConnection()，
		/// 然后返回NULL。我们将编组一个正确格式化的拒绝信号并调用SendRejectionSignal()，
		/// 这样您就可以将其发送给他们。
		///
		/// If you return a signaling object, the connection is NOT immediately
		/// accepted by default.  Instead, it stays in the "connecting" state,
		/// and the usual callback is posted, and your app can accept the
		/// connection using ISteamNetworkingSockets::AcceptConnection.  This
		/// may be useful so that these sorts of connections can be more similar
		/// to your application code as other types of connections accepted on
		/// a listen socket.  If this is not useful and you want to skip this
		/// callback process and immediately accept the connection, call
		/// ISteamNetworkingSockets::AcceptConnection before returning the
		/// signaling object.
		/// 如果返回一个信令对象，则默认情况下不会立即接受该连接。
		/// 相反，它保持在“连接”状态，并且通常的回调被发布，
		/// 并且你的应用程序可以使用ISteamNetworkingSockets::AcceptConnection接受连接。
		/// 这可能是有用的，这样这些类型的连接可以与您的应用程序代码更类似于侦听套接字上接受的其他类型的连接。
		/// 如果这没有用，并且你想跳过这个回调过程并立即接受连接，
		/// 在返回信令对象之前调用ISteamNetworkingSockets::AcceptConnection。
		///
		/// After accepting a connection (through either means), the connection
		/// will transition into the "finding route" state.
		/// 在接受连接后（通过任何一种方式），连接将转换到“寻找路由”状态。
		public IntPtr OnConnectRequest(HSteamNetConnection hConn, ref SteamNetworkingIdentity identityPeer, int nLocalVirtualPort) {
			return NativeMethods.SteamAPI_ISteamNetworkingSignalingRecvContext_OnConnectRequest(ref this, hConn, ref identityPeer, nLocalVirtualPort);
		}

		/// This is called to actively communicate rejection or failure
		/// to the incoming message.  If you intend to ignore all incoming requests
		/// that you do not wish to accept, then it's not strictly necessary to
		/// implement this.
		/// 调用它是为了主动地向传入消息传递拒绝或失败。
		/// 如果您打算忽略所有不希望接受的传入请求，则没有必要严格执行此操作。
		public void SendRejectionSignal(ref SteamNetworkingIdentity identityPeer, IntPtr pMsg, int cbMsg) {
			NativeMethods.SteamAPI_ISteamNetworkingSignalingRecvContext_SendRejectionSignal(ref this, ref identityPeer, pMsg, cbMsg);
		}
	}
}

#endif // !DISABLESTEAMWORKS