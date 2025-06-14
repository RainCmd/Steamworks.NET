from SteamworksParser import steamworksparser
from src import interfaces
from src import constants
from src import enums
from src import structs
from src import typedefs

from time import sleep
from googletrans import Translator

count = 0
translator = Translator()
def translate_text(text, dest_language='zh-cn'):
    global count
    count += 1
    print(f"正在翻译第 {count} 个文本: {text}")
    return f"临时测试:《{text}》"

    while True:
        try:
            translated = translator.translate(text, dest=dest_language)
            print(f"翻译结果: {translated.text}")
            return translated.text
        except Exception:
            print(f"翻译失败，10秒后重试")
            sleep(10)  # 请求过于频繁时等待10秒
            print(f"正在重新翻译第 {count} 个文本: {text}")


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
