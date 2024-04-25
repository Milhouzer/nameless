# Nameless
Nameless project. There are a bunch of Unity premade modules like inventory system, commands pattern module (tasks), UI framework etc.

## bd5fa4c - Everything

Commit containing what have been done since the beginning.

## b8e0b50 - Inventory Editor, implemented item properties.

### Main editor

* Created an editor that allows the user to select an ItemDatabase and display all of its - nested - informations.
* Display the list of items present in the DB. Possibility to select an item in the list.
* Buttons to add an existing item, create a new item asset, remove an item or remove and delete it completely from assets.
* When an item is selected its data are displayed on a dedicated panel to edit them.

### ItemProeprties

An item properties system has been creating allowing the user to define any type of properties as on a blackboard (see AI module).
Properties are like "stats" for the item, they allow fast behavior attribution to items, for instance we can set the float property "FUEL_POWER" to indicate that the item can be used as a source of energy in a furnace/engine/etc. This system allows us to avoid a ton of abstraction with many interfaces. They will eventually work in addition with flags to avoid mispelling on the properties.

* Created a nested editor on the main editor that allows to set/edit/delete properties.

