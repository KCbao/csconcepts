## What is docker?
Docker operates at the deployment stage. Docker is a software container platform. Developers will package up n application with all of the parts it needs, such as frontend components, backend workers, as well as all libraries and other dependencies it requires, into container, and ship it all out as one package. 

## Docker workflow
1. Developer first defines a "Dockerfile", which describes steps to create a Docker image, just like a receipe. 
2. This "Dockerfile" then is used to create/build "Docker image", in "Docker image", you will have the application's all parts as well as its lib and dependencies
3. When you run a "Docker image", you get "Docker containers", so "Docker containers" are the runtime instances of a docker image, a single image can be used to create multiple containers. and these images could also be stored in an online cloud depository "Docker Hub", or you could store "Docker image" in any version-control systems or local repository. 

Interview Q: Where are images stored? Can be stored remotely (dockerhub) or locally. 

## Docker has a client-server architecture
- Docker client: the Command-Line Interface is the client, we post all docker commands there e.g., `docker pull`, `docker ps -a`
- Docker server/ Docker Daemon: it has all the containers
- Docker engine: all the client and server together forms the docker engine
- Registry: a place to store docker images. e.g, docker hub
- Docker Host: has Docker Daemon, Docker containers, and Docker images. 

The Docker server receives commands from Docker clients in the form of commands (docker commands) or REST API request

Docker client and Docker Daemon can be present on the same host (machine) or different hosts. 

Example:
1. `docker build`: build Docker Images in Docker Host using DockerFiles. 
2. `docker pull`: you type `docker pull` in terminal, it behaves as Docker client, through Docker daemon to registry to get that image, and save the image to Docker images in Docker Host. 
3. `docker run`: it checks if Docker Images in on local system Docker Host, if it is not, if auto-pull images from Regitory. And finally create a container about that image in Docker Host. 

## Advantage of Docker
1. Portability
Docker containers can run on any platforms (local system, aws ec2, virtualBox, etc)

2. Version Control
Docker has built-in version control system (can commit changes to Docker images and version control them)

3. Isolation
Every app works in isolation in its own container and does not interferes with other apps running on the same system

4. Productivity
Allows fast and efficient deployment

## Installation Docker on Windows
1. If you install the Docker Toolbox on a Windows machine, the installer auto-intalls Oracle Virtualbox to run the Docker VM. (Windows client <=> Linux VM)
2. In Linux, Docker client talks directly to Docker Daemon vs In Windows, Docker clients talks to Docker Host (i.e., a Linux VM), and Docker Daemon is inside this Docker Host. 

## Installation on WSL Ubuntu
0. Turn on "Windows Features", ensure "Hyper-V" checkbox is checked. 
1. first download [docker desktop in Windows](https://www.docker.com/products/docker-desktop)
2. open Docker Desktop using admin (right click icon and select "open as admin"), otherwise my computer is not responding when click docker desktop
3. in Docker Desktop Dashboard, Settings/General/ select "Expose daemon on tcp://localhost:2375 without TLS"
4. Follow steps [post](https://nickjanetakis.com/blog/setting-up-docker-for-windows-and-wsl-to-work-flawlessly). 
5. check if docker is installed `docker run hello-world`, `docker --version`, `docker-compose --version`

## Docker Commands
1. `docker version`: client and server
2. `docker --version`: version of docker
3. `docker info`: number of container etc
4. `docker --help` or `docker images --help` or `docker login --help`: to get help for specific set of commands
5. `docker login`: to log in dockerhub
5. `docker images`: a list of images you have 
6. `docker pull`: pull images, you could add tag to pull specific version, by default it pulls latest version
6. `docker rmi <image id>`: to remove the image, image id can be found in `docker images`
7. `docker ps -a`: list all available container, `docker ps`: list all running containers
7. `docker start <container id/name>`: to start container
8. `docker stop <container id/name>`
9. `docker stats`
10. `docker system df`: images, containers, all disks info
11. `docker system prune`: used to remove unused data, leaving only running images/containers
12. `docker container run <imageid:tag>`: if this image doesn't exist, it will auto-pull from dockerhub
13. `docker container rm [container_id]`: remove container
14. `docker run --name <container name> -it <image name>`: -it will give you an iterative shell for your image. e.g., your image is ubuntu, it will open you a ubuntu terminal 

## DockerFiles
- DockerFile is a text file with instructions used to build docker image

Steps to create a dockerfile
0. #: is comment
1. Create a file named "Dockerfile" (no extension)
2. `FROM <name of base image>`, if don't have any base image, type `FROM scratch` (scratch is an empty image on dockerhub)
3. `MAINTAINER <maintainer name of this image + email>`
4. `RUN <commands>`: commands that execute during building the iamge
4. `CMD [<commands>]`: these commands get executed only when you create container out from this image.  

- build image from Dockerfile. `docker build <path of dockerfile>` or `docker build -t <imageName:tagName> <path of dockerfile>`
- run image `docker run <imageName:tagName>`