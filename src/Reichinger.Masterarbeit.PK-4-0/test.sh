RUNNING=$(docker inspect --format="{{ .State.Running }}" psqldatabase 2> /dev/null)

if [ $RUNNING ]
then
 docker exec -t psqldatabase pg_dumpall -c -U postgres > /data/backup/pk_database_dump_`date +%d-%m-%Y"_"%H_%M_%S`.sql
fi

