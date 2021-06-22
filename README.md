# MHF-Quest-Resources
**NOTE: This repo does not function in the slightest. Do not expect anything to function for the average person.**

**I can make no guarentees that anything here will be fully function, if at all, there's a lot to test, and a lot to learn. I WILL get around to everything eventually. But any help would be greatly appreciated.**

### TODO + NOTES (If you want to contribute, feel free!)
- Figure out how English translations are handled. (Needs ENG Patch files (Don't ask for these))
- See what different ReFrontier tools do, and how they function. 
- Figure out different files, i.e. txb, bin, pac, gab, suffix, mhfpac.bin, mhfdat.bin, mhfinf.bin
- **Important** figure out how ENGLISH TRANSLATIONS are handled, as well as how to (**MOST IMPORTANT**) ADD QUESTS, not just edit existing ones. 
    - Potentially related to `mhfdat.bin` or it's just as simple as creating a new ID?
    - Example: Are `21978d0, 21978d1, 21978d2, 21978n0, 21978n1, 21978n2` different quests? Are they related?
    - What do the `d/n 0-2` at the end of quests actually represent?
    - Reiterating. Can I add my own completely unique quest by using a random string, does it need to follow the format of the existing quests?
    - Assuming the above, I could in theory do something like `99999a5` or assuming it needs to follow a format, maybe, `99999d0`

Quest resources for Monster Hunter Frontier (and other stuff)

Progress enabler "quests" can be found here: [mhfz_progression](https://archive.org/details/mhfz_progression)

"Modified" version of [MHF-QuestEditor](https://github.com/Yuvi-App/MHF-QuestEditor) in C#

## Documentation
As of June 22, 2021, The batch script are obsolete but are kept for future reference points.
**Until a proper quest editor is made, I would recommend using a hex editor such as HxD or 010 to actually edit / write files.**

- This tool integrates [ReFrontier](https://github.com/mhvuze/ReFrontier). 
- Usage of ReFrontier tools can be started by `ReFrontier` / `FrontierDataTool` / `FrontierTextTool`
    - Documentation for these tools will be explained in the future. 

- The main tool `QuestTool` has various arguments and parameters. `QuestTool <arguments> <parameters>
    - **Note:** Debug mode is enabled by default, change the Debug variable in QuestWriter from true to false if you want to actually write.
    - `-log` - logs the specified file - `QuestTool ./path/to/file.bin -log`
    - `-strToHex` - converts string to hex code - `QuestTool -strToHex My Text`
    - `-edit` - starts quest editing functionality, and changes parameters made for read write.
        - **Note:** Replace the `m` with `a` or `b` to indicate subobjectives, e.g. `-mobjType` -> `bobjType` or `aobjType`.
        - `-edit -mobjType` - changes the quest goal, note this dictates dictionary to use. e.g. delivery -> item OR slay -> monster - `QuestTool ./path/to/file.bin -edit -mobjType 01,02,00,00`
        - `-edit -mobjGoal` - changes the quest goal - `QuestTool ./path/to/file.bin -edit -mobjGoal 176`
        - `-edit objLocal` - changes the quest location - `QuestTool ./path/to/file.bin -edit -objLocal 95`

### Prerequisites 
- HxD / 010
- Some way to view quest indexes and hex - [MHF-QuestEditor](https://github.com/Yuvi-App/MHF-QuestEditor) or Quest-Editor tools found in this repo.
- ReFrontier Tools by Vuze [here](https://github.com/mhvuze/ReFrontier)

**NOTE: This setup is not meant for noobs, you should be fairly knowledgeable about the subject before fully attempting all of this.**

**THESE STEPS ARE NOT FULLY TESTED AND MAY NOT BE 100% ACCURATE**

There is some batch scripts included. They have not been fully tested yet. 

Please make sure you compiled each ReFrontierTool and input the full path including filename to the exe, and whatever file is specified.

## Quest-Editor Info
- Build `/Quest-Editor`
- Open command line by exe, run via `Quest-Editor <FILE_PATH> <ARGUMENTS>`, e.x. `Quest-Editor ../QuestData/quests/21978d0.bin -log`.
- Assumed you have **decompiled the quest file FIRST** it should output a LOT of data for quest editing (helpful in something like HxD or 010).
- I've included an extra little tool for now to help with quest editing as well. This tool simply replaces a string with its HEX alternative.
- Run this tool via `Quest-Editor -strToHex <MY_STRING>`, e.x. `Quest-Editor -strToHex ≪樹海探索≫\n樹海の特産【下位】`.
- You SHOULD get an output that looks like this if you tried the example: `81 E1 8E F7 8A 43 92 54 8D F5 81 E2 0A 8E F7 8A 43 82 CC 93 C1 8E 59 81 79 89 BA 88 CA 81 7A`.

## Batch Script Info
- Batch scripts are categorized by: Number(Reference Only) Letter(Script Type).
- `0#A_*` scripts allow you to input path for each file during run.
- `0#B_*` scripts are for predefined paths (you'll have to open the file to edit the path variables before use).

### Setup Part 1
- Build ALL tools in ReFrontier. As they will be useful for different parts, although if you don't want to follow some of the other "extra" stuff, just build the `ReFrontier` tool.
- Download the `mhfz_progression` files linked above.
- The next step is *technically* option, but very strongly recommended.
- Place the quest files and `mhfdat.bin` in their respective locations. `Erupe\bin\quests`, `Monster Hunter Frontier\dat`.
- Run the Erupe server. (If you need help. Look in `Guides` folder., Other files needed can be found on `archive.org`).
- Run the game with GameGuard removed. (Information can be found in the guide.)
- Start a quest in-game, it's up to you which one you choose, but pleaes keep in mind, **this will be your base for quest editing, so pick a good one!**.
- Erupe should log the quest file being called. Example may be `21981d0.bin`.

### Setup Part 2
- Assuming you've built all of your tools, you are now ready for the next step. 
- Copy your quest file (example above), and put it by the ReFrontier tool exe. 
- Open a command line here, and input the following command. `ReFrontier 21981d0.bin -log -close`
**NOTE: I did not fully complete the rest of the steps from here, but I can give a rough reference.**
- Open the newly decompiled bin in HxD or 010. Use either the `Quest-Editor (advanced)` or the one from the link above for a reference point on what bytes are what.
- Edit different bytes based on values from the Editor, and `*Dictionary.cs` files in `Quest-Editor/Utils`.
- Drop the **EDITED** `*.bin` file back in the ReFrontier directory. 
- Run the following: `ReFrontier 21981d0.bin -compress 3,100 -close` (Please note the compression type and level is a guess, I haven't actually gotten this far. I'm too lazy :P).
- Run `ReFrontier 21981d0.bin -encrypt -close`. (In all fairness, I'm unsure this and the step before are actually necessary)
- Drop your edited quest file back in the `Erupe\bin\quests` directory. 
- Test it out in game.

### Setup Extra
- ReFrontier also has a tool that allows you to edit the shop files, for all items, item prices, and armor prices (incl. sell values(?)).
- If you want to do this, grab your `mhfdat.bin` and drop it in the ReFrontier tool directory.
- Open a command line here and run: `ReFrontier mhfdat.bin -log -close`
- Your `mhfdat.bin` should be successfully decompiled.
- Place the decomp'd bin file by the `FrontierDataTool` .exe.
- Open a command line here as well, and run `FrontierDataTool modshop mhfdat.bin`
- Assuming this was succesful, put this bin file back into the ReFrontier directory and run the following commands:
- `ReFrontier mhfdat.bin -compress 4,100 -close` (Note: I'm not entirely sure compression is needed, but these files can be big decompressed, so it's nice to have smol file :)) )
- Make sure you're encrypting the file found in output, and not a different one, and that the .meta file is next to the file in output. (.meta files are only generated if the file being decompressed has encryption, quest files are not encrypted.)
- `ReFrontier mhfdat.bin -encrypt -close` (This step is 100% required, otherwise you'll get an anomally error when you run the game.)

### Misc
- I want to test out some of the other ReFrontier tools, but I'm not 100% how or what they're supposed to do.
- I would also like to know what the `mhfpac.bin` is for.
- I'd also be curious if the mhfdat.bin is in control of adding new quests to the list or not, and if so, if the tools can be used to do this.
- **If you want to test these, please open an issue and give me any information you discovered, ideally, I will do this myself, but one thing at a time.**

## Credits
- [theBusBoy](https://github.com/theBusBoy)
- [Vuze](https://github.com/mhvuze)