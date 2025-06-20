import os
import sys
from SteamworksParser import steamworksparser

class InternalConstant:
    def __init__(self, name, value, type_, precomments, comment, spacing):
        self.name = name
        self.value = value
        self.type = type_
        self.precomments = precomments
        self.comment = comment
        self.spacing = spacing

g_TypeDict = {
    # Not a bug... But, it's a giant hack.
    # 这不是什么漏洞……不过，这可真是个巨大的漏洞利用手段。
    # The issue is that most of these are used as the MarshalAs SizeConst in C# amongst other things and C# wont auto convert them.
    # 问题在于，其中大多数都被用作 C# 中的“MarshalAs SizeConst”之类的功能，但 C# 不会自动对其进行转换。
    "uint16": "ushort",

    "uint32": "int",
    "unsigned int": "int",

    "uint64": "ulong",
    "size_t": "int",
}

g_SkippedDefines = (
    "VALVE_COMPILE_TIME_ASSERT(",
    "REFERENCE(arg)",
    "STEAM_CALLBACK_BEGIN(",
    "STEAM_CALLBACK_MEMBER(",
    "STEAM_CALLBACK_ARRAY(",
    "END_CALLBACK_INTERNAL_BEGIN(",
    "END_CALLBACK_INTERNAL_SWITCH(",
    "END_CALLBACK_INTERNAL_END()",
    "STEAM_CALLBACK_END(",
    "INVALID_HTTPCOOKIE_HANDLE",
    "BChatMemberStateChangeRemoved(",
    "STEAM_COLOR_RED(",
    "STEAM_COLOR_GREEN(",
    "STEAM_COLOR_BLUE(",
    "STEAM_COLOR_ALPHA(",
    "INVALID_SCREENSHOT_HANDLE",
    "_snprintf",
    "S_API",
    "STEAM_CALLBACK(",
    "STEAM_CALLBACK_MANUAL(",
    "STEAM_GAMESERVER_CALLBACK(",
    "k_steamIDNil",
    "k_steamIDOutofDateGS",
    "k_steamIDLanModeGS",
    "k_steamIDNotInitYetGS",
    "k_steamIDNonSteamGS",
    "STEAM_PS3_PATH_MAX",
    "STEAM_PS3_SERVICE_ID_MAX",
    "STEAM_PS3_COMMUNICATION_ID_MAX",
    "STEAM_PS3_COMMUNICATION_SIG_MAX",
    "STEAM_PS3_LANGUAGE_MAX",
    "STEAM_PS3_REGION_CODE_MAX",
    "STEAM_PS3_CURRENT_PARAMS_VER",
    "STEAMPS3_MALLOC_INUSE",
    "STEAMPS3_MALLOC_SYSTEM",
    "STEAMPS3_MALLOC_OK",
    "S_CALLTYPE",
    "POSIX",
    "STEAM_PRIVATE_API(",
    "STEAMNETWORKINGSOCKETS_INTERFACE",
    "S_OVERRIDE",

    # We just create multiple versions of this struct, Valve renamed them.
    # 我们只是为这个结构体创建了多个版本，而Valve将其重新命名了。
    "ControllerAnalogActionData_t",
    "ControllerDigitalActionData_t",
    "ControllerMotionData_t",

    #"INVALID_HTTPREQUEST_HANDLE",
)

g_SkippedConstants = (
    # ISteamFriends
    "k_FriendsGroupID_Invalid",

    # ISteamHTMLSurface
    "INVALID_HTMLBROWSER",

    # ISteamInventory
    "k_SteamItemInstanceIDInvalid",
    "k_SteamInventoryResultInvalid",
    "k_SteamInventoryUpdateHandleInvalid",

    # ISteamMatchmaking
    "HSERVERQUERY_INVALID",

    # ISteamRemoteStorage
    "k_UGCHandleInvalid",
    "k_PublishedFileIdInvalid",
    "k_PublishedFileUpdateHandleInvalid",
    "k_UGCFileStreamHandleInvalid",

    # ISteamUGC
    "k_UGCQueryHandleInvalid",
    "k_UGCUpdateHandleInvalid",

    # SteamClientPublic
    "k_HAuthTicketInvalid",

    # SteamTypes
    "k_uAppIdInvalid",
    "k_uDepotIdInvalid",
    "k_uAPICallInvalid",
    "k_uAccountIdInvalid",

    # steamnetworkingtypes.h
    "k_HSteamNetConnection_Invalid",
    "k_HSteamListenSocket_Invalid",
    "k_HSteamNetPollGroup_Invalid",
    "k_SteamDatagramPOPID_dev",

    # steam_gameserver.h
    "MASTERSERVERUPDATERPORT_USEGAMESOCKETSHARE",
)

g_SkippedTypedefs = (
    "uint8",
    "int8",
    "uint16",
    "int32",
    "uint32",
    "int64",
    "uint64",
)

g_CustomDefines = {
    # "Name": ("Type", "Value"),
    "k_nMaxLobbyKeyLength": ("byte", None),
    "STEAM_CONTROLLER_HANDLE_ALL_CONTROLLERS": ("ulong", "0xFFFFFFFFFFFFFFFF"),
    "STEAM_CONTROLLER_MIN_ANALOG_ACTION_DATA": ("float", "-1.0f"),
    "STEAM_CONTROLLER_MAX_ANALOG_ACTION_DATA": ("float", "1.0f"),
    "STEAM_INPUT_HANDLE_ALL_CONTROLLERS": ("ulong", "0xFFFFFFFFFFFFFFFF"),
    "STEAM_INPUT_MIN_ANALOG_ACTION_DATA": ("float", "-1.0f"),
    "STEAM_INPUT_MAX_ANALOG_ACTION_DATA": ("float", "1.0f"),
}
g_translate_text = None

def main(parser, translate_text = None):
    global g_translate_text
    g_translate_text = translate_text
    try:
        os.makedirs("../com.rlabrecque.steamworks.net/Runtime/autogen/")
    except OSError:
        pass

    lines = []
    constants = parse(parser)
    for constant in constants:
        for precomment in constant.precomments:
            lines.append("//" + precomment)
        if g_translate_text:
            text = g_translate_text(constant.precomments)
            if text:
                if isinstance(text, list):
                    for t in text:
                        lines.append("// " + t)
                else:
                    lines.append("// " + text)
        lines.append("public const " + constant.type + " " + constant.name + constant.spacing + "= " + constant.value + ";" + constant.comment)

    with open("../com.rlabrecque.steamworks.net/Runtime/autogen/SteamConstants.cs", "w", encoding= 'utf-8') as out:
        with open("templates/header.txt", "r",encoding='utf-8') as f:
            out.write(f.read())
        out.write("namespace Steamworks {\n")
        out.write("\tpublic static class Constants {\n")
        for line in lines:
            out.write("\t\t" + line + "\n")
        out.write("\t}\n")
        out.write("}\n\n")
        out.write("#endif // !DISABLESTEAMWORKS\n")

def parse(parser):
    interfaceversions, defines = parse_defines(parser)
    constants = parse_constants(parser)
    return interfaceversions + constants + defines

def parse_defines(parser):
    out_defines = []
    out_interfaceversions = []
    for f in parser.files:
        for d in f.defines:
            if d.name in g_SkippedDefines:
                continue

            comment = ""
            if d.c.linecomment:
                comment = " //" + d.c.linecomment
                # 这块不知道是什么注释，反正没走到这里

            definetype = "int"
            definevalue = d.value
            customdefine = g_CustomDefines.get(d.name, False)
            if customdefine:
                if customdefine[0]:
                    definetype = customdefine[0]
                if customdefine[1]:
                    definevalue = customdefine[1]
            elif d.value.startswith('"'):
                definetype = "string"
                if d.name.startswith("STEAM"):
                    out_interfaceversions.append(InternalConstant(d.name, definevalue, definetype, d.c.precomments, comment, " "))
                    continue

            out_defines.append(InternalConstant(d.name, definevalue, definetype, d.c.precomments, comment, d.spacing))

    return (out_interfaceversions, out_defines)


def parse_constants(parser):
    out_constants = []
    for f in parser.files:
        for constant in f.constants:
            if constant.name in g_SkippedConstants:
                continue

            comment = ""
            if constant.c.linecomment:
                comment = " //" + constant.c.linecomment
                # 常量申明的行尾注释
                if g_translate_text:
                    text = g_translate_text(constant.c.linecomment)
                    if text:
                        comment += " " + text


            constanttype = constant.type
            for t in parser.typedefs:
                if t.name in g_SkippedTypedefs:
                    continue

                if t.name == constant.type:
                    constanttype = t.type
                    break
            constanttype = g_TypeDict.get(constanttype, constanttype)

            constantvalue = constant.value
            if constantvalue == "0xFFFFFFFF":
                constantvalue = "-1"
            elif constantvalue == "0xffffffffffffffffull":
                constantvalue = constantvalue[:-3]
            elif constantvalue.endswith(".f"):
                constantvalue = constantvalue[:-1] + "0f"

            out_constants.append(InternalConstant(constant.name, constantvalue, constanttype, constant.c.precomments, comment, " "))

    return out_constants

if __name__ == "__main__":
    if len(sys.argv) != 2:
        print("TODO: Usage Instructions")
        exit()

    steamworksparser.Settings.fake_gameserver_interfaces = True
    main(steamworksparser.parse(sys.argv[1]))
