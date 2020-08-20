Windows Server Installation Guide

1. Enable the windows subsystem for linuxs. Go to the Windows search bar, type 'features' to bring up the Turn Windows Features on and off dialog. Scroll down and check Windows Subsystem for Linux
2. Manually download [WSL distros package](https://docs.microsoft.com/en-us/windows/wsl/install-manual) since MDA disables Microsoft App store. Here we download Ubuntu 18.04 (not download the most recent 20.04 because 18.04 is most common and stable one)
3. Install distro in powershell `Add-AppxPackage .\<distro file name>.appx
4. Unzip and Extract <distro file name>, it has `ubuntu1804.exe` click and it will install and open ubuntu shell. 
5. in powershell, type `wsl` then ubuntu will be activated and now we are in WSL

Install Anaconda
1. Follow step on [Install Anaconda on Linux](https://problemsolvingwithpython.com/01-Orientation/01.05-Installing-Anaconda-on-Linux/), with specific anaconda version found at [anaconda repo](https://repo.anaconda.com/archive/). For MDA computer, I choose Anaconda3-5.2.0-Linux-x86_64.sh. 

Find anaconda python interpreter path 
[instruction](https://docs.anaconda.com/anaconda/user-guide/tasks/integration/python-path/)

Configure in VScode
1. [follow instructions](https://code.visualstudio.com/docs/remote/wsl-tutorial) to install extensions (1)install WSL extension and (2) re-install Python on WSL ubuntu. 

## Mount C drive
One day I open /mnt/c/users/an007936 and only see "desktop" but no other folders. 

Solution: 
1: Option 1, cd `/etc/wsl.conf`: and check if `[automount]` is there
2. Option 2, `sudo mount -t drvfs C: /mnt/c`: to re-mount C drive to /mnt/c, it needs sudo because "only root can use --types options"