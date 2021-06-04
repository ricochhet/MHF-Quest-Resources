# MHF-Quest-Resources
**NOTE: This repo does not function in the slightest. Do not expect anything to function for the average person.**

Quest resources for Monster Hunter Frontier (and other stuff)

Progress enabler "quests" can be found here: [mhfz_progression](https://archive.org/details/mhfz_progression)

"Modified" version of [MHF-QuestEditor](https://github.com/Yuvi-App/MHF-QuestEditor) in C#

## Documentation
**Until a proper quest editor is made, I would recommend using a hex editor such as HxD or 010 to actually edit / write files.**

### Prerequisites 
- HxD / 010
- Some way to view quest offsets / hex - [MHF-QuestEditor](https://github.com/Yuvi-App/MHF-QuestEditor) or Quest-Editor tools found in this repo.
- ReFrontier Tools by Vuze [here](https://github.com/mhvuze/ReFrontier)

**NOTE: This setup is not meant for noobs, you should be fairly knowledgeable about the subject before fully attempting all of this.**

**THESE STEPS ARE NOT FULLY TESTED AND MAY NOT BE 100% ACCURATE**

There is some batch scripts included. They have not been fully tested yet. 

Please make sure you compiled each ReFrontierTool and input the full path including filename to the exe, and whatever file is specified.

## Quest-Editor Info
- Build `/Quest-Editor`
- Open command line by exe, run via `Quest-Editor <FILE_PATH>`, e.x. `Quest-Editor ../QuestData/quests/21978d0.bin`.
- Assumed you have **decompiled the quest file FIRST** it should output a LOT of data for quest editing (helpful in something like HxD or 010).

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
- `ReFrontier mhfdat.bin -encrypt -close` (This step is 100% required, otherwise you'll get an anomally error when you run the game.)

### Misc
- I want to test out some of the other ReFrontier tools, but I'm not 100% how or what they're supposed to do.
- I would also like to know what the `mhfpac.bin` is for.
- I'd also be curious if the mhfdat.bin is in control of adding new quests to the list or not, and if so, if the tools can be used to do this.
- **If you want to test these, please open an issue and give me any information you discovered, ideally, I will do this myself, but one thing at a time.**

## Credits
- [theBusBoy](https://github.com/theBusBoy)
- [Vuze](https://github.com/mhvuze)