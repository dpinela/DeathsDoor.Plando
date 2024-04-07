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

## Special directives

Instead of an item placement, a line may instead contain one of a few directives, each of
which apply a specific one-off effect to the game:

- `#start-night`: starts the game at night, instead of during the day as is the default.
- `#start-weapon-daggers`: changes the starting weapon to be the Rogue Daggers instead of the Reaper's Sword.
- `#start-weapon-umbrella`: changes the starting weapon to the Discarded Umbrella.
- `#start-weapon-greatsword`: changes the starting weapon to the Repaer's Greatsword.
- `#start-weapon-hammer`: changes the starting weapon to the Thunder Hammer.

Note that if you change the starting weapon, the
Reaper's Sword is not given at the start of the game;
you must explicity place it at some location if it
is to be obtainable.

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
