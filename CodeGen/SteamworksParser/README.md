# SteamworksParser

这是一个针对[Steamworks](https://partner.steamgames.com/)头文件的简单解析器。

SteamworksParser 用来生成 [Steamworks.NET](https://github.com/RainCmd/Steamworks.NET) 通过绑定 [Steamworks.NET-CodeGen](https://github.com/RainCmd/Steamworks.NET-CodeGen).

你可能会疑惑，为什么不直接使用像 libclang 这样的工具来解析 C++ 代码呢？主要的原因是，我希望能够保留注释和格式信息。

## 使用示例

将这个包拖入您的项目文件夹中。

```python
    import sys
    from SteamworksParser import steamworksparser

    def main():
        if len(sys.argv) != 2:
            print('Usage: test.py <path/to/steamworks_sdk/sdk/public/steam/>')
            return

        steamworksparser.Settings.warn_utf8bom = True
        steamworksparser.Settings.warn_includeguardname = True
        steamworksparser.Settings.warn_spacing = True
        parser = steamworksparser.parse(sys.argv[1])

        with open('test.json', 'w') as out:
            out.write('{\n')

            out.write('    "typedefs":[\n')
            for typedef in parser.typedefs:
                out.write('        {\n')
                out.write('            "typedef":"' + typedef.name + '",\n')
                out.write('            "type":"' + typedef.type + '"\n')
                out.write('        },\n')


    if __name__ == '__main__':
        main()
```
