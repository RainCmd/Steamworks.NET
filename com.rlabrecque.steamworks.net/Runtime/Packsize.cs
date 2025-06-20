// This file is provided under The MIT License as part of Steamworks.NET.
// Copyright (c) 2013-2022 Riley Labrecque
// Please see the included LICENSE.txt for additional information.

// This file is automatically generated.
// Changes to this file will be reverted when you update Steamworks.NET

#if !(UNITY_STANDALONE_WIN || UNITY_STANDALONE_LINUX || UNITY_STANDALONE_OSX || STEAMWORKS_WIN || STEAMWORKS_LIN_OSX)
#define DISABLESTEAMWORKS
#endif

#if !DISABLESTEAMWORKS

// If we're running in the Unity Editor we need the editors platform.
// 如果我们是在使用 Unity 编辑器进行操作，那么我们就需要使用编辑器平台。
#if UNITY_EDITOR_WIN
	#define VALVE_CALLBACK_PACK_LARGE
#elif UNITY_EDITOR_OSX || UNITY_EDITOR_LINUX
	#define VALVE_CALLBACK_PACK_SMALL

// Otherwise we want the target platform.
// 否则，我们需要的是目标平台。
#elif UNITY_STANDALONE_WIN || STEAMWORKS_WIN
	#define VALVE_CALLBACK_PACK_LARGE
#elif UNITY_STANDALONE_LINUX || UNITY_STANDALONE_OSX || STEAMWORKS_LIN_OSX
	#define VALVE_CALLBACK_PACK_SMALL

// We do not want to throw a warning when we're building in Unity but for an unsupported platform. So we'll silently let this slip by.
// It would be nice if Unity itself would define 'UNITY' or something like that...
// 我们在使用 Unity 进行开发时，并不想因为是在不支持的平台上进行操作而发出警告。所以我们会选择默默忽略这一情况，让它自行过去。
// 如果 Unity 本身能明确定义“UNITY”或者类似的概念就好了……
#elif UNITY_3_5 || UNITY_4_0 || UNITY_4_1 || UNITY_4_2 || UNITY_4_3 || UNITY_4_5 || UNITY_4_6 || UNITY_4_7 || UNITY_5 || UNITY_2017_1_OR_NEWER
	#define VALVE_CALLBACK_PACK_SMALL

// But we do want to be explicit on the Standalone build for XNA/Monogame.
// 但我们确实希望明确说明一下针对 XNA/Monogame 的独立版本的相关事宜。
#else
	#define VALVE_CALLBACK_PACK_LARGE
	#warning You need to define STEAMWORKS_WIN, or STEAMWORKS_LIN_OSX. Refer to the readme for more details.
	#warning 您需要定义“STEAMWORKS_WIN”或者“STEAMWORKS_LIN_OSX”。有关更多详细信息，请参考“readme”文件。
#endif

using System.Runtime.InteropServices;
using IntPtr = System.IntPtr;

namespace Steamworks {
	public static class Packsize {
#if VALVE_CALLBACK_PACK_LARGE
		public const int value = 8;
#elif VALVE_CALLBACK_PACK_SMALL
		public const int value = 4;
#endif

		public static bool Test() {
			int sentinelSize = Marshal.SizeOf(typeof(ValvePackingSentinel_t));
			int subscribedFilesSize = Marshal.SizeOf(typeof(RemoteStorageEnumerateUserSubscribedFilesResult_t));
#if VALVE_CALLBACK_PACK_LARGE
			if (sentinelSize != 32 || subscribedFilesSize != (1 + 1 + 1 + 50 + 100) * 4 + 4)
				return false;
#elif VALVE_CALLBACK_PACK_SMALL
			if (sentinelSize != 24 || subscribedFilesSize != (1 + 1 + 1 + 50 + 100) * 4)
				return false;
#endif
			return true;
		}

		[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
		struct ValvePackingSentinel_t {
			uint m_u32;
			ulong m_u64;
			ushort m_u16;
			double m_d;
		};
	}
}

#endif // !DISABLESTEAMWORKS
