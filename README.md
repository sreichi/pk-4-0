# masterarbeit-pk-4-0
[![Build Status](https://travis-ci.com/sreichi/masterarbeit-pk-4-0.svg?token=MzErPkYPu5HbCPXTR97U&branch=master)](https://travis-ci.com/sreichi/masterarbeit-pk-4-0)

This will be fun


## Usefull Docker commands

Docker remove all Containers
```
docker rm -f $(docker ps -a -q)
```
Docker remove all Images
```
docker rmi $(docker images -q)
```
Docker run Container with specific Volumes on host.
```
docker run -v /var/log/postgresql:/var/log/postgresql -v /var/lib/postgresql/9.3/main/base:/var/lib/postgresql/9.3/main/base -p 5432:5432 -d [IMAGE_NAME]
```
Docker list all volumes
``` 
docker volume ls
```
Docker delete all volumes
```
docker volume rm $(docker volume ls -q)
```

Backup all databases from docker container
```
docker exec -t [your-db-container] pg_dumpall -c -U postgres > dump_`date +%d-%m-%Y"_"%H_%M_%S`.sql
```

Restore database from backup file
``` 
cat [your_dump.sql] | docker exec -i [your-db-container] psql -U postgres
```

Docker compose with file reference
```
cat [your_dump.sql] | docker exec -i [your-db-container] psql -U postgres
```

## Entity Framework Commands

Apply all Migrations (needs to be at the same level as project.json)
```
dotnet ef database update -e Production
```

Create Migration in specific folder
```
dotnet ef migrations add {migration-name} --output-dir {path}
```

Create Migration for Identity Server PersistedGrant Tables
```
dotnet ef migrations add InitialIdentityServerPersistedGrantDbMigration -c PersistedGrantDbContext -o Database/Migrations/IdentityServer/PersistedGrantDb -e Production
```

Create Migration for Identity Server ConfigurationDb Tables
```
dotnet ef migrations add InitialIdentityServerConfigurationDbMigration -c ConfigurationDbContext -o Database/Migrations/IdentityServer/ConfigurationDb -e Production
```