# Pokemon Generator Application
Author: Justin Robb 

Date: 8/30/2016

[![Build status](https://ci.appveyor.com/api/projects/status/wtnwh8ar676m93m6?svg=true)](https://ci.appveyor.com/project/Reptarsrage/pok-mon-generator)
[![codecov](https://codecov.io/gh/Reptarsrage/Pok-mon-Generator/branch/master/graph/badge.svg)](https://codecov.io/gh/Reptarsrage/Pok-mon-Generator)

### Screenshots
![Configure your Setup](screens/main.png?raw=true "Configure your Setup")
![Tweak Options](screens/options.png?raw=true "Tweak Options")
![Choose your team](screens/choose.png?raw=true "Choose your team")

### Description
Generates a team of six Gen II pokemon for use in Pokemon Gold or Silver.
Built in order to supply Pokemon Stadium 2 with a better selection of Pokemon.

### Installation
1. Install [Project64](http://www.pj64-emu.com/).
2. Acquire Pokemon Stadium 2 and Pokemon Gold or Silver ROMs legally.
3. Install the [latest release](https://github.com/Reptarsrage/Pok-mon-Generator/releases).
4. Provide the locations for each executable in PokemonGenerator program.
5. Click Generate (or press CTRL+F12).
6. Launch Pokemon Stadium 2. Configure the controller and transfer pak if necessary using 
   [the instructions here](https://forums.emulator-zone.com/showthread.php?t=2449).


### Repository Contents
1. **PokemonGenerator:** A GUI to use to easily interact with the core executable.
2. **Installer:** A friendly installer project for quick deployments.
3. **PokemonGenerator.Tests.Integration:** Integration tests.
4. **PokemonGenerator.Tests.Unit:** Unit tests.
5. **Native Binaries:** Binaries needed to run Sql Server Compact.
6. **ThePokeBase.sdf** Compact Database containing all the info needed for Gen II Pokemon.
7. **Silver.sav** &amp; **Gold.sav** are bundled Gen II Save files.

### Logic for the curious
Pokemon teams are generated using a fairly strict set of rules, with only a bit of randomness,
to get the appearence of complete randomness without any of the downsides. There are three steps in this process.

#### Choosing a team of Pokemon
This is one of the easier steps. Given a level,
the program chooses six Gen II pokemon out of the available 251.
To do this, we first eliminate any pokemon which cannot exist at the level,
whether because it would have already evolved, or because it's previous evolution
would not have evolved yet. Then we can assign probabilities to each of the remaining pokemon.
Giving a higher chance of getting a normal pokemon, a very unlikely chance of getting a lenendary, and a zero
chance of getting something stupid like an unknown.

#### Calculate Stats
Once we have our team, we can pull all of their base stats from the database - these never change. The only
values that change are `IV` and `EV` values. You can read about them on bulbapedia, but basically an `IV` is a
stat value between 0-15 that is assigned to a pokemon at birth. An `EV` is a
number between 0 and 65535 which a pokemon accumulates the more it trains. Both of these values positivley
effect stats (the higher the better). To choose these values, we can use a sudo-gaussian algorithm which
tends to choose mid-range values and has a standard deviation of enough to get some variation.
(e.g. `IV` values may have a mean of 7.5 and a std deviation of 3). The rest of the stats are based on 
formulas defined for Gen II pokemon.

#### Choose Moves
This is by far the most complicated step as each pokemon has four move slots to fill, and a bunch of choices
to fill these slots. I will give a brief overview on my process.
* We can get a set of all moves that a pokemon can learn be summing up the moves that it can learn from 
  `HM`'s, `TM`'s, Breeding and leveling up.
* We want to choose on average of 3 attack moves that do damage, and one special move which alters stats for each pokemon. 
  This makes each individual move choice easier because it cuts the total move possiblities in half.
* We do not want to see the same move used everywhere, so we can go ahead and prune moves already chosen by pokemon in the current team. 
  We can also limit the `TM`'s available to a team so that, like in the game, a pokemon that consumes a `TM` to learn a move makes it so no 
  other pokemon can use that `TM`. (not completely realistic, as some TMs can be purchased or found multiple times, but close enough)
* With the ramaining moves to choose from, we can assign a probability value to each move. The higher this value is compared to all 
  of the other moves, the more likely this move is to be chosen.
* We Consider the following when assigning probabilities:
  1. The damage type of the move matches the pokemon's damage type 
     (physical or special based on the pokemon's base `attack` and base `spAttack` values).
  2. If the move has a certain effect, we can weigh it based on this effect. (e.g. moves that cause the user to faint are weighed very low).
  3. If we are concerned about damage, we prefer moves that do the most damage.
  4. If a move relies on another move being chosen in order to be effective, we must look at the previously chosen moves and 
     decide a weight based on prerequisites being filled.
  5. We should favor moves that are different. If a move has already been chosen, or a very simliar move has been chosen 
     (same effect, same type), then the move will be very unlikely to be picked.
  6. We also need to consider type advantages. If the move type compliments the pokemon's type,
     *either matches the type or is strong against pokemon that the pokemon is usually weak against*, then the move is preferred.

## TODO

- [x] Make congfig an injected global conifg
- [x] Split out player 1/2 forms into common code
- [ ] Add Config section for directories
- [ ] Create app settings degug/release for default directories (can use %APPDATA% and such)
- [x] Create provider for directory
- [x] AutoFac over ninject
- [x] XUnit over Nunit
- [x] Split tests into unit and integrationexit
- [x] Add PokemonGeneratorForm to injection
- [x] AssignIVsAndEVsToTeam take into account level
- [x] Condense interfaces
- [x] Add ability to add/remove possible pokemon
- [x] Status bar updates for saved/loaded/generated events
- [x] Add Additional Settings Window
- [x] Add Move Filter Window
- [x] Add Legendary/Special effects to pokemon settings window
- [x] Add ability to turn off randomness and pick your own team and moves
- [x] Add options window for pokemon likeliness
- [x] Fix Generator so that if it doesn't have enough to pick from it duplicates picks (team of charizards?)
- [ ] Fix allAvailablePokemon and don't display pokemon if level is too low
- [ ] Add tooltips
- [ ] Add ability to turn on/off level decisions like banning lvl 100 Pikachu (will always choose Riachu instead) (also it's funny that Pikachu is recognized by autocorrect but Riachu is not)
- [ ] Reduce size by truncating unused database tables and optimizing resources
- [ ] Save configurations on exit so that subsequent GUI load use saved information
- [ ] Support changing the macro from Ctrl+F12, in the GUI for generating pokemon
- [ ] Test installer on an x86 platform
