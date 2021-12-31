The AnomalyDynamicArtifactsGenerator.exe is a self contained .net core application compiled from https://github.com/fredlllll/AnomalyArtifactGenerator

Once you run it it will generate 4500 randomized artifacts and spit out a gamedata folder that you can then copy to your anomaly game directory.
Dont overwrite any existing files in there(unless you know its the ones from this mod) or other mods might not work anymore. If you have file collisions, deactivate mods using those files or merge them by hand.

The exe takes one argument to overwrite the amount of artifacts generated. i chose 4500 as default cause the game kinda manages it. the debug menu is still kinda usable,
and the svarog freezes the game only for a few seconds on the first use after a loading screen. more artifacts will make the svarog freeze the game even longer, and over 50000
makes the game take much longer to even start up.

The PropertyStats.txt file is used to determine the values that are generated for each property. the more are added the more artifacts can potentially be generated,
but i have to thin them out a lot to get down to 4500. if a property name ends in [] it means the following values will be interpreted as a list. if it ends normally,
the following values will be interpreted as min,max,steps

Property[],1,2,3,4,5  will generate artifact variations with 1,2,3,4 or 5 for this property
Property,1,5,5 will do the same. 1 is minimum value, 5 is maximum, and 5 steps are to be generated. if you use negative values, this will include 0 and can lead to artifacts with 0 value properties, so just stick to the above syntax if you dont want that

all property values are floating point numbers

You can regenerate artifacts if you have the feeling that you only find junk. the newly generated amount cant be less than before, or it could break savegames.
