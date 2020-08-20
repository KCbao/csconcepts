## .env file
- called "dotenv file": it must be ignored from version control because it will expose sensitive information as part of the project. 
- it has one environment variable assignment per line
- in debug mode, VScode uses up "launch.json" to configure, so add `"env":{"PYTHONPATH": "C:\\"}` under Python list in "configuration" section

## setup.py
It tells you that the module/package you are about to  install has been packaged and distributed with Distuils (standard for distributing Python modules). This allows us to easily install Python packages by `pip install .` or `python setup.py install`. This command create symlinks to the your package's source directory within site-packages, so it's quite fast, especially for large packages. 

1. create setup.py. 
Package tree of foo
```
foo
|-- foo1
|   |--__init__.py
|   |-- internals.py
|
|-- README.md
|-- requirements.txt
|-- setup.py
```

```
# setup.py
from setuptools import setup, find_packages

setup(
    name='foo', # name of your package, the outside name foo not foo1
    version='0.1', # version of this package you write
    description = ' This is a module for foo', #must be a one-sentence summary of the package
    long_description='', #detailed description
    author='casie',
    author_email='casie_bao@gmail.com',
    packages=["foo"], # list of packages that should be included in the distribution package, you can enter this list manually, or use find_packages() to auto-discover all pacjages and subpackages.

)

```

### Terminology
- package: a folder/dir that contains "__init__.py" file
- module: a valid python file with ".py" extension
- distribution: how one package related to other packages and modules

## __pycache__
When you run a program in python, the interpreter compiles it to bytecode first (this is an oversimplification) and stores it in the __pycache__ folder. If you look in there you will find a bunch of files sharing the names of the .py files in your project's folder. 

## .egg
when you do `python setup.py bdist_egg`, this will generate lot of outputs, but when it’s completed you’ll see that you have three new folders: build, dist, and mymath.egg-info. The only folder that we care about is the dist folder where you'll find your .egg file, mymath-0.1-py3.5.egg with your default python (installation) version number(mine here: 3.5)

## Python doesn't have switch
But you could use dictionary to perform the same feature

## pip install cert
Either `pip install --cert <cert path> <package name>` or set cert path globally. In WSL Ubuntu, Create a file under "~/.pip/pip.conf", inside it, add `[global] cert=<cert path>`. `pip install --editable .` cannot add `--cert` flag. 

