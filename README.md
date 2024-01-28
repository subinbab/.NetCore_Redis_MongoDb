This project will explain you how to implement redis caching in .net core 8 application.
Here we use mongo db as database and entity framework
it is a web api project with swagger integration

1. Install Redis on your computer
     go throught the link below :
         https://github.com/microsoftarchive/redis/releases
         download latest redis msi installer
         install it in your computer
    Check whether it working
        for that , go and find redis-cli.exe file and redis-server and open it .Probably it may found in Programe Files folder

    if it gives like  : localhost:6379>  on cli type on ping and it will response like PONG . And there you have sucessfully installed it !
2. Install Mongo db Compass on your computer
     go through the link below
       https://fastdl.mongodb.org/windows/mongodb-windows-x86_64-7.0.5-signed.msi
       install it
       open mongo db
       get your connection uri from mongo db GUI
       keep it !
3. Create .net core web api project
     Create Folders for Models,Services,Utilities
     Models has => MongoDBSettings.cs , Playlist.cs and ReddisDbSettings.cs files
   MongoDBSettings.cs for mapping the mongodb creds from appsettings
   Playlist.cs is our sample table
   ReddisDbSettings.cs for mapping the redis creds from appsettings

   Services has CacheService.cs and MongoDbservice.cs files
   each will be responsible for there logic implementation

   Utilities has ConnectionHelper.cs file it will carry the redis connection
   
   
