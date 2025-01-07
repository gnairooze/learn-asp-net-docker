# asp.net web api on docker

## to copy files from the docker container volume to host directory

1. **List the Containers**: First, you need to know the name or ID of the container from which you want to copy the files.

```sh
docker ps -a
```

2. **Copy Files**: Use the `docker cp` command to copy files from the container to your host machine. Replace `container_id`, `/path/in/container`, and `/path/on/host` with your specific paths.

```sh
docker cp container_id:/path/in/container/file1.txt /path/on/host/file1.txt
docker cp container_id:/path/in/container/file2.txt /path/on/host/file2.txt
```

If you want to copy an entire directory, you can use:

```sh
docker cp container_id:/path/in/container/directory /path/on/host
```

## build after making changes

to reflect the changes in build, follow the following steps:

1. **connect to the container terminal**: 

```sh
 docker exec -it webapi-api /bin/bash
```

2. **delete files in app**: 

be sure you are in the `app` directory

```sh
rm *
```

3. **stop the containers**

4. **rebuild the containers**

```sh
docker compose up --build
```
