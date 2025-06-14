Steamworks.NET CodeGen
======================

这是一种特殊的程序，它会生成“autogen/”和“types/”这两个目录。

它使用 [SteamworksParser](https://github.com/rlabrecque/SteamworksParser) 先解析 Steamworks 的 C++ 头文件，然后将其转换为 C# 语言。

用法
-----

1. 如有需要，请更新`steam/`文件。 
2. 打开命令提示符窗口，进入“CodeGen”目录。（该脚本必须从该目录中运行，否则相对路径将会失效。）
3. 运行 `python3 Steamworks.NET_CodeGen.py`, 最好在基于 Linux 的操作系统上进行操作，以确保生成正确的行尾格式。（我使用 WSL 来实现这一点。）
4. 如果更新steam文件后，生成的cs文件中有乱码，可以先运行`python3 to_utf8.py ./steam/`将头文件编码都转换为utf8，再执行第三步