What is *devel package （debian-based distributions)?
- debian: 是完全由自由软体组成的类unix system
- 在安装package时，经常看到同样名字的package有两个，分别是带和不带devel后缀的， 不带后缀的时包含能让程序运营的动态库和配置文件，带后缀的，包含使用这个package开发程序的所有必须文件
- `*.h`: header files (e.g., source file, like `import *` in Python) that describe the interface of that application as well as a version-less symlink to the shared library (`*.so`: stands for shared object, ). Sometimes `*-devel` also include statically compiled versions of the libraries (`*.a`). 
- `*.so`: nomally package files will place `.so` files to `/lib` or `/usr/lib` when installing
- in C, `#include <*.h>`, compiler will found `*.h` in `/usr/include` directory, `#include` is like import in Python