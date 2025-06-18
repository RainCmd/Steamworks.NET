namespace Steamworks
{
	/// Interface used to send signaling messages for a particular connection.
	/// 用于为特定连接发送信令消息的接口。
	///
	/// - For connections initiated locally, you will construct it and pass
	///   it to ISteamNetworkingSockets::ConnectP2PCustomSignaling.
	/// - For connections initiated remotely and "accepted" locally, you
	///   will return it from ISteamNetworkingSignalingRecvContext::OnConnectRequest
	/// - 对于本地发起的连接，您将构造它并将其传递给ISteamNetworkingSockets::ConnectP2PCustomSignaling。
	/// - 对于远程发起并在本地“接受”的连接，您将从ISteamNetworkingSignalingRecvContext::OnConnectRequest返回它
	[System.Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public struct ISteamNetworkingConnectionSignaling
	{
		/// Called to send a rendezvous message to the remote peer.  This may be called
		/// from any thread, at any time, so you need to be thread-safe!  Don't take
		/// any locks that might hold while calling into SteamNetworkingSockets functions,
		/// because this could lead to deadlocks.
		/// 调用以向远程对等端发送会合消息。这可以在任何时间从任何线程调用，所以您需要线程安全！
		/// 不要在调用SteamNetworkingSockets函数时使用任何可能持有的锁，因为这可能导致死锁。
		///
		/// Note that when initiating a connection, we may not know the identity
		/// of the peer, if you did not specify it in ConnectP2PCustomSignaling.
		/// 注意，在初始化连接时，如果没有在ConnectP2PCustomSignaling中指定，我们可能不知道对等体的身份。
		///
		/// Return true if a best-effort attempt was made to deliver the message.
		/// If you return false, it is assumed that the situation is fatal;
		/// the connection will be closed, and Release() will be called
		/// eventually.
		/// 如果已尽最大努力尝试传递消息，则返回true。如果返回false，
		/// 则假定情况是致命的；连接将被关闭，Release（）最终将被调用。
		///
		/// Signaling objects will not be shared between connections.
		/// You can assume that the same value of hConn will be used
		/// every time.
		/// 信令对象不会在连接之间共享。你可以假设每次都使用相同的hConn值。
		public bool SendSignal(HSteamNetConnection hConn, ref SteamNetConnectionInfo_t info, IntPtr pMsg, int cbMsg) {
			return NativeMethods.SteamAPI_ISteamNetworkingConnectionSignaling_SendSignal(ref this, hConn, ref info, pMsg, cbMsg);
		}

		/// Called when the connection no longer needs to send signals.
		/// Note that this happens eventually (but not immediately) after
		/// the connection is closed.  Signals may need to be sent for a brief
		/// time after the connection is closed, to clean up the connection.
		/// 当连接不再需要发送信号时调用。注意，这在连接关闭后最终（但不是立即）发生。
		/// 信号可能需要在连接关闭后的一小段时间内发送，以清理连接。
		///
		/// If you do not need to save any additional per-connection information
		/// and can handle SendSignal() using only the arguments supplied, you do
		/// not need to actually create different objects per connection.  In that
		/// case, it is valid for all connections to use the same global object, and
		/// for this function to do nothing.
		/// 如果您不需要保存任何额外的每个连接信息，并且可以仅使用提供的参数来处理SendSignal()，
		/// 则不需要实际地为每个连接创建不同的对象。在这种情况下，
		/// 所有连接都可以使用相同的全局对象，而这个函数什么都不做是有效的。
		public void Release() {
			NativeMethods.SteamAPI_ISteamNetworkingConnectionSignaling_Release(ref this);
		}
	}
}

#endif // !DISABLESTEAMWORKS