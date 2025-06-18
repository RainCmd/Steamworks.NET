from SteamworksParser import steamworksparser
from src import interfaces
from src import constants
from src import enums
from src import structs
from src import typedefs

import re
import requests

count = 0
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
    url = "http://localhost:11434/api/generate"
    data = {
        "model": "gemma3:4b",
        "prompt": f'请翻译为中文:{text}',
        "stream": False,
        "options":{
            "temperature": 0.1,
            "top_p": 0.6,
            "top_k": 10,
        },
        "system": "你是一个翻译助手，任何发送给你的内容都直接翻译为简体中文并输出，不要输出其他任何多余内容。请注意，Steam是软件名，保留英文原文，Valve是公司名，也保留英文原文。如果你觉得无需翻译就只输出(null)",
    }
    response = requests.post(url, json=data)
    result = response.json().get("response", "")
    print(f"翻译结果: {result}")
    if result == "(null)":
        return ""
    return result.replace("\n", " ").strip()

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
            return result
    
    if is_nothing(text):
        return ""
    prefix = get_prefix(text)
    text = translate(trim_text(text))
    if not forceTrim and prefix:
        return prefix + text
    else:
        return text

def main():
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
