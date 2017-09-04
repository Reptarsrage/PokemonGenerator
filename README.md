<h1>Pokemon Generator Application</h1>
<hr />

Author: Justin Robb <br>
Date: 8/30/2016

<h3>Description:</h3>
Generates a team of six Gen II pokemon for use in Pokemon Gold or Silver.
Built in order to supply Pokemon Stadium 2 with a better selection of Pokemon.

<h3>Installation</h3>
<ol>
    <li>Install <a href="http://www.pj64-emu.com/">Project64</a>.</li>
	<li>Acquire Pokemon Stadium 2 and Pokemon Gold or Silver ROMs legally.</li>
	<li>Install and launch <a href="https://github.com/Reptarsrage/Pok-mon-Generator/releases/tag/1.0">PokemonGenerator</a>.</li>
	<li>Provide the locations for each executable in PokemonGenerator program.</li>
    <li>Click Generate (or press CTRL+F12).</li>
    <li>Launch Pokemon Stadium 2. Configure the controller and transfer pak if necessary using the <a href="https://forums.emulator-zone.com/showthread.php?t=2449">instructions here</a>.</li>
</ol>

<h3>Contents</h3>
<ol>
    <li><b>PokemonGenerator:</b> The core executable that generates sav files with the pokemon.</li>
    <li><b>PokemonGenerator:</b> A GUI to use to easily interact with the core executable.</li>
    <li><b>PokemonGeneratorInstaller:</b> A friendly installer project for quick deployments.</li>
</ol>

<h3>Logic for the curious</h3>
<p>
    Pokemon teams are generated using a fairly strict set of rules, with only a bit of randomness,
    to get the appearence of complete randomness without any of the downsides. There are three steps in this process.
</p>
<ol>
    <li>
        <h4>Choosing a team of Pokemon</h4>
        <p>
            This is one of the easier steps. Given a level,
            the program chooses six Gen ii pokemon out of the available 251.
            To do this, we first eliminate any pokemon which cannot exist at the level,
            with because it would have already evolved
            (yes, we ignore the fact that a trainier can force a pokemon to not evolve), or because it's previous evolution
            would not have evolved yet. Then we can assign probabilities to each of the remaining pokemon.
            Giving a higher chance of getting a normal pokemon, a very unlikely chance of getting a lenendary, and a zero
            chance of getting something  stupid liek an unknown.
        </p>
    </li>
    <li>
        <h4>Calculate Stats</h4>
        <p>
            Once we have our team, we can pull all of their base stats from the database - these never change. The only
            values that change are IV's and EV's. You can read about them on bulbapedia, but basically an IV is a
            stat value between 0-15 that is assigned to a pokemon at birth, and these values never change. An EV is a
            number between 0 and 65535 which a pokemon accumulates the more it trains. Both of these values positivley
            effect stats (the higher the better). To choose these values, we can use a sudo-gaussian algorithm which
            tends to choose mid-range values and has a standard deviation of enough to get some variation.
            (e.g. IV values may have a mean of 7.5 and a std deviation of 3).
        </p>
    </li>
    <li>
        <h4>Choose Moves</h4>
        <p>
            This is by far the most complicated step as each pokemon has four move slots to fill, and a bunch of choices
            to fill these slots. I will give a brief overview on my process.
            <ul>
                <li>
                    WE can get a set of all moves that a pokemon can learn be summing up the
                    moves that it can learn from HMs, YMs, Breeding and leveling up.
                </li>
                <li>
                    We want to choose on average of 3 attack moves that do damage,
                    and one special move which alters stats for each pokemon.
                    This makes each individual move choice easier because it cuts the total move possiblities in half.
                </li>
                <li>
                    We do not want to see the same move used everywhere, so we can go ahead and prune moves already chosen by
                    pokemon in the current team. We can also limit the TM's available to a team so that, like in the game, a pokemon
                    that consumes a TM to learn a move makes it so no other pokemon can use that TM. (not completely realistic, as some
                    TMs can be purchased or found multiple times, but close enough)
                </li>
                <li>
                    With the ramaining moves to choose from, we can assign a probability value to each move.
                    The higher this value is compared to all of the other moves, the more likely this move is to be chosen.
                </li>
                <li>
                    We Consider the following when assigning probabilities:
                    <ol>
                        <li>
                            The damage type of the move matches the pokemon's damage type
                            (physical or special based on the pokemon's base attack and base spAttack values).
                        </li>
                        <li>
                            If the move has a certain effect, we can weigh it based on this effect.
                            (e.g. moves that cause the user to faint are wighed very low).
                        </li>
                        <li>
                            If we are concerned about damage, we prefer moves that do the most damage.
                        </li>
                        <li>
                            If a move relies on another move being chosen in order to be effective,
                            we must look at the previously chosen moves and decide a weight based on prerequisites being filled.
                        </li>
                        <li>
                            We should favor moves that are different. If a move has already been chosen,
                            or a very simliar move has been chosen (same effect, same type),
                            then the move will be very unlikely to be picked.
                        </li>
                        <li>
                            We also need to consider type advantages. If the move type compliments the pokemon's type
                             - either matches the type or is strong against pokemon that the current pokemon is usually weak against -
                            then the move is preferred.
                        </li>
                    </ol>
                </li>
            </ul>
        </p>
    </li>
</ol>



<h3>TODO</h3>
<ul>
    <li>Write unit tests</li>
	<li>Reduce size by truncating unused database tables and optimizing resources.</li>
    <li>Make executable a class library and only have the GUI as an executable.</li>
    <li>Implement better user input validation on front end (detect duplicate entries and correct extensions).</li>
    <li>Save configurations on exit so that subsequent GUI load use saved information.</li>
    <li>Handle errors in core executable</li>
    <li>Different entrropy levels to control generated pokemon randomness</li>
    <li>Move certain constants to a config file so that pokemon genneration can be tweaked without needing a recompile.</li>
    <li>Add GUI controls and core controls to generate user hand-selected pokemon and moves and stats.</li>
    <li><b>DONE</b> Support renaming trainier in sav file.</li>
    <li>Support changing the macro from Ctrl+F12, in the GUI for generating pokemon.</li>
    <li>Add a view in the GUI to change and tweak pokemon generation values.</li>
    <li>Test installer on an x86 platform.</li>
</ul>