This is a Death's Door mod that allows one to play planned randomizers, also known as
"plandos" - games where the item placements are predetermined, rather than randomized.

# How to create a plando

Write a text file with the extension `.ddplando`, containing one line for each item you
wish to place. Each line should name the item and location, in that order, separated by an
at-sign (`@`).

Locations that are not mentioned in the file will be left as vanilla.

The list of valid items and locations can be found [in ItemChanger's code][ic-pdef].
There are a few examples of working plandos in the `Examples` directory.

[ic-pdef]: https://github.com/dpinela/DeathsDoor.ItemChanger/blob/main/ItemChanger/Predefined.cs

# How to play a plando

To play a plando, place its `.ddplando` file anywhere inside the Plugins directory in your
BepInEx installation. After that, launch the game, select any empty save slot, and press
left or right while the "Start" option is highlighted until you see
"START <name-of-plando>"; then, start the file.

# Dependencies

This mod uses [AlternativeGameModes][] and [ItemChanger][] to provide almost all its
functionality. Their assemblies, as well as the game's, should be placed in the
`Plando/Deps` directory before building the mod.

[AlternativeGameModes]: https://github.com/dpinela/DeathsDoor.AlternativeGameModes
[ItemChanger]: https://github.com/dpinela/DeathsDoor.ItemChanger
