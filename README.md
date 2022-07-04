# key-rebinder
.NET core application that rebinds keys per process

I came accross an issue when playing an old PC video game (Company of Heroes 2006) that doesn't have the option to remap keyboard keys. So I made this little software myself, hope you'll find it useful.

There is no UI currently so if you wish to remap keys for a video game, find its process name (alt+tab after you launch the game, or any other program for that matter) and put the name of the process in the WindowGameName property (StartGameBindingsQuery.cs) in Program.cs in the console application. You also need to create a class that inherits from InitialSqlLiteDatabase to support the keys you want (since there is no UI to do it), see CompanyOfHeroesProfileInitDepository.cs as an example.
