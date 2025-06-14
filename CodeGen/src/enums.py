import os
import sys
from SteamworksParser import steamworksparser

g_FlagEnums = (
    # ISteamFriends
    "EPersonaChange",
    "EFriendFlags",

    #ISteamHTMLSurface
    "EHTMLKeyModifiers",

    #ISteamInput
    "EControllerHapticLocation",

    #ISteamInventory
    "ESteamItemFlags",

    # ISteamMatchmaking
    "EChatMemberStateChange",

    # ISteamRemoteStorage
    "ERemoteStoragePlatform",

    # ISteamUGC
    "EItemState",

    # SteamClientPublic
    "EChatSteamIDInstanceFlags",
    "EMarketNotAllowedReasonFlags",
    "EBetaBranchFlags",
)

g_SkippedEnums = {
    # This is defined within CGameID and we handwrote CGameID including this.
    # 这是在“CGameID”中定义的，我们还亲手编写了“CGameID”并将其包含在内。
    "EGameIDType": "steamclientpublic.h",

    # Valve redefined these twice, and ifdef decided which one to use. :(
    # Valve重新定义了这两个枚举两次，并且使用了ifdef来决定使用哪一个。:(
    # We use the newer ones from isteaminput.h and skip the ones in
    # 我们使用 isteaminput.h 中的较新的版本，并跳过那些版本。
    # isteamcontroller.h because it is deprecated.
    # isteamcontroller.h 因为它已被弃用。
    "EXboxOrigin": "isteamcontroller.h",
    "ESteamInputType": "isteamcontroller.h",
}

g_ValueConversionDict = {
    "0xffffffff": "-1",
    "0x80000000": "-2147483647",
    "k_unSteamAccountInstanceMask": "Constants.k_unSteamAccountInstanceMask",
    "( 1 << k_ESteamControllerPad_Left )": "( 1 << ESteamControllerPad.k_ESteamControllerPad_Left )",
    "( 1 << k_ESteamControllerPad_Right )": "( 1 << ESteamControllerPad.k_ESteamControllerPad_Right )",
    "( 1 << k_ESteamControllerPad_Left | 1 << k_ESteamControllerPad_Right )": "( 1 << ESteamControllerPad.k_ESteamControllerPad_Left | 1 << ESteamControllerPad.k_ESteamControllerPad_Right )",
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
    for f in parser.files:
        for enum in f.enums:
            if enum.name in g_SkippedEnums and g_SkippedEnums[enum.name] == f.name:
                continue

            for comment in enum.c.rawprecomments:
                if type(comment) is steamworksparser.BlankLine:
                    continue
                lines.append("\t" + comment)
            if g_translate_text:
                text = g_translate_text(enum.c.rawprecomments)
                if text:
                    lines.append("\t" + text)

            if enum.name in g_FlagEnums:
                lines.append("\t[Flags]")

            lines.append("\tpublic enum " + enum.name + " : int {")

            for field in enum.fields:
                for comment in field.c.rawprecomments:
                    if type(comment) is steamworksparser.BlankLine:
                        lines.append("")
                    else:
                        lines.append("\t" + comment)
                if g_translate_text:
                    text = g_translate_text(field.c.rawprecomments)
                    if text:
                        lines.append("\t" + text)
                line = "\t\t" + field.name
                if field.value:
                    if "<<" in field.value and enum.name not in g_FlagEnums:
                        print("[WARNING] Enum " + enum.name + " contains '<<' but is not marked as a flag! - " + f.name)

                    if field.value == "=" or field.value == "|":
                        line += " "
                    else:
                        line += field.prespacing + "=" + field.postspacing

                    value = field.value
                    for substring in g_ValueConversionDict:
                        if substring in field.value:
                            value = value.replace(substring, g_ValueConversionDict[substring], 1)
                            break

                    line += value
                if field.c.rawlinecomment:
                    line += field.c.rawlinecomment
                    if g_translate_text:
                        text = g_translate_text(field.c.rawlinecomment, True)
                        if text:
                            line += " " + text
                lines.append(line)

            for comment in enum.endcomments.rawprecomments:
                if type(comment) is steamworksparser.BlankLine:
                    lines.append("")
                else:
                    lines.append("\t" + comment)
            if g_translate_text:
                text = g_translate_text(enum.endcomments.rawprecomments)
                if text:
                    lines.append("\t" + text)

            lines.append("\t}")
            lines.append("")

    with open("../com.rlabrecque.steamworks.net/Runtime/autogen/SteamEnums.cs", "wb") as out:
        with open("templates/header.txt", "r") as f:
            out.write(bytes(f.read(), "utf-8"))
        out.write(bytes("using Flags = System.FlagsAttribute;\n\n", "utf-8"))
        out.write(bytes("namespace Steamworks {\n", "utf-8"))
        for line in lines:
            out.write(bytes(line + "\n", "utf-8"))
        out.write(bytes("}\n\n", "utf-8"))
        out.write(bytes("#endif // !DISABLESTEAMWORKS\n", "utf-8"))

if __name__ == "__main__":
    if len(sys.argv) != 2:
        print("TODO: Usage Instructions")
        exit()

    steamworksparser.Settings.fake_gameserver_interfaces = True
    main(steamworksparser.parse(sys.argv[1]))