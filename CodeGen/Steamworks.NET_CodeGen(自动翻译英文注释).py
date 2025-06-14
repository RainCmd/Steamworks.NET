from SteamworksParser import steamworksparser
from src import interfaces
from src import constants
from src import enums
from src import structs
from src import typedefs

from time import sleep
from googletrans import Translator
import re

count = 0
translator = Translator()
def get_prefix(text):
    match = re.match(r'^\s*//+\s*', text)
    if match:
        return match.group(0)
    return ""
def is_nothing(text):
    return not re.search(r'\w', text)
def trim_text(text):
    match = re.match(r'^\s*//+(.*)', text)
    if match:
        return match.group(1).strip()
    else:
        return text.strip()

def translate(text):
    global count
    count += 1
    print(f"正在翻译第 {count} 个文本: {text}")
    return "临时测试:《" + text + "》"

    while True:
        try:
            translated = translator.translate(text, dest='zh-cn')
            print(f"翻译结果: {translated.text}")
            return translated.text
        except Exception:
            print(f"翻译失败，10秒后重试")
            sleep(10)  # 请求过于频繁时等待10秒
            print(f"正在重新翻译第 {count} 个文本: {text}")
def translate_text(text, forceTrim = False):
    if isinstance(text, list):
        if len(text) == 0:
            return ""
        prefix = ""
        tmp = []
        cur = ""
        for t in text:
            if type(t) is not str:
                continue
            if is_nothing(t):
                if cur:
                    tmp.append(cur.strip())
                    cur = ""
                continue
            if not prefix:
                prefix = get_prefix(t)
            t = trim_text(t)
            if t:
                cur += t + " "
        if cur:
            tmp.append(cur.strip())
        if not prefix:
            for t in text:
                if type(t) is not str:
                    continue
                prefix = get_prefix(t)
                break
        result = []
        for t in tmp:
            t = translate(t)
            if not forceTrim and prefix:
                result.append(prefix + t)
            else:
                result.append(t)
        if not forceTrim and prefix:
            return "\n".join(result)
        else:
            return " ".join(result)
    
    if is_nothing(text):
        return ""
    prefix = get_prefix(text)
    text = translate(trim_text(text))
    if not forceTrim and prefix:
        return prefix + text
    else:
        return text

def main():
    """
    启用自动生成中文注释功能需要安装googletrans库
    pip install googletrans==4.0.0-rc1
    如果出现httpcore._exceptions.ConnectTimeout: timed out，应该是谷歌被墙了
    可以使用代理或VPN翻墙，或者使用其他翻译服务
    例如：deepl、百度翻译等
    """
    steam_path = "steam/"

    steamworksparser.Settings.fake_gameserver_interfaces = True
    ___parser = steamworksparser.parse(steam_path)

    interfaces.main(___parser, translate_text)
    constants.main(___parser, translate_text)
    enums.main(___parser, translate_text)
    structs.main(___parser, translate_text)
    typedefs.main(___parser, translate_text)

if __name__ == "__main__":
    main()
