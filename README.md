# MHF-Quest-Resources
A lot of what is in this repo is experimental and is not guarenteed to be functional.

## Contributing 
If you have information, resources, or code to share, please do! Feel free to open an issue or a pull request. Any help would be greatly appreciated!

### TODO / IMPORTANT INFORMATION / THINGS TO CONTRIBUTE (Feel free to contribute if you know anything listed here).
- The quest tool cannot reliably edit files other than ones ending in `d0` (day, warm). Whether this affects replacing quests is untested.
    - It might be possible to simply duplicate the quest multiple times, replacing the `d0` with other values. If someone tested or knows about this, please open an issue.
    - Although the quest tool works the best on `d0` type quests, it does work on other types with mixed results. (Different offsets?) How this affects quest replacing is untested.
- The `FrontierTools\output-tests` directory includes a couple of examples of valid quest logs. You can use these as a reference point.

## Resources
- [MHFZ Progression Files](https://archive.org/details/mhfz_progression)
- [Standalone ReFrontier Tools](https://github.com/mhvuze/ReFrontier)
- [MHF-QuestEditor](https://github.com/Yuvi-App/MHF-QuestEditor)

## Information

### Quest Information
- Quest file format information: `Example: 21978d0.bin`
- `21978` - Quest ID(?)
- `d` / `n` - Day / Night
- `0` / `1` / `2` - Warm Season / Cold / Breeding
- Quest information is found in `mhfinf.bin`. **Note:** Whether ReFrontier tools cover this file sufficiently is unknown.

*Thanks to suzaku830 for this information!*

## Documentation
### Building

- Download [.NET SDK](https://dotnet.microsoft.com/download) / [Direct download](https://dotnet.microsoft.com/download/dotnet/thank-you/sdk-5.0.301-windows-x64-installer)
- Run `git clone https://github.com/ricochhet/MHF-Quest-Resources.git`
- Enter the soruce directory using `cd MHF-Quest-Resources\FrontierTools`
- To build; use `dotnet build`, or you can use it from source via `dotnet run <integration> <arguments>`
- If you are not using this tool from source, open a command prompt in the build directory (next to the .EXE).

## Usage P.1 (Integrations)
This tool implements both a quest reader and writer (limited), as well as ReFrontier tools (see: Resources).

## Quest Reader / Writer
- Integration name: `QuestTool`
- Example usage: `QuestTool <arguments>`.

### Commands
- `QuestTool ./path/to/quest.bin -log` - Logs quest information to a file. (`FrontierTools/output/`).
- `QuestTool -strToHex My Text` - Converts a string to a hex / byte string. 
- `QuestTool ./path/to/quest.bin -edit` - Initialize quest editor tools.

**Note:** See `FrontierTools\Utils\ObjectiveDictionary.cs` for values. THIS DETERMINES WHAT TYPE OF VALUES THE OBJECTIVE GOAL LOOKS FOR, E.G: ITEM / MONSTER (DELIVERY / SLAY)
- `QuestTool ./path/to/quest.bin -edit -mobjType` - Sets MAIN objective type. Example usage - `QuestTool ./path/to/quest.bin -edit -mobjType 01,00,00,00`
- `QuestTool ./path/to/quest.bin -edit -aobjType` - Sets SUB A objective type. Example usage - `QuestTool ./path/to/quest.bin -edit -aobjType 01,00,00,00`
- `QuestTool ./path/to/quest.bin -edit -bobjType` - Sets SUB B objective type. Example usage - `QuestTool ./path/to/quest.bin -edit -bobjType 01,00,00,00`

**Note:** See `FrontierTools\Utils\MonsterDictionary.cs` or `FrontierTools\Utils\ItemDictionary.cs` for values.
- `QuestTool ./path/to/quest.bin -edit -mobjGoal` - Sets MAIN objective goal. Example usage - `QuestTool ./path/to/quest.bin -edit -mobjGoal 176`
- `QuestTool ./path/to/quest.bin -edit -aobjGoal` - Sets SUB A objective goal. Example usage - `QuestTool ./path/to/quest.bin -edit -aobjGoal 176`
- `QuestTool ./path/to/quest.bin -edit -bobjGoal` - Sets SUB B objective goal. Example usage - `QuestTool ./path/to/quest.bin -edit -bobjGoal 176`

**Note:** See `FrontierTools\Utils\LocationDictionary.cs` for values.
- `QuestTool ./path/to/quest.bin -edit -objLocal` - Sets quest location. Example usage - `QuestTool ./path/to/quest.bin -edit -objLocal 25`

## ReFrontier 
- Integration name: `ReFrontier`
- Example usage: `ReFrontier <arguments>`

### Commands
**Note:** Enter `ReFrontier` without any arguments to see additional command options.
- `ReFrontier ./path/to/quest.bin -log -close` - Decompresses/dumps quest .BIN file. (Replace quest .BIN with `mhfdat.bin` if needed).
- `ReFrontier ./path/to/quest.bin -compress 3,100 -close` - Recompresses quest .BIN file.
- `ReFrontier ./path/to/mhfdat.bin -compress 4,100 -close` - Recompresses quest .BIN file.
- `ReFrontier ./path/to/quest.bin -encrypt -close` - Encrypts quest .BIN file. (Replace quest .BIN with `mhfdat.bin` if needed, `MHFDAT NEEDS TO BE ENCRYPTED`).

## FrontierDataTool
- Integration name: `FrontierDataTool`
- Example usage: `FrontierDataTool <arguments>`

### Commands
- `FrontierDataTool modshop ./path/to/mhfdat.bin` - Mods in-game shop to include all items.
- `FrontierDataTool dump suffix? ./path/to/mhfpac.bin ./path/to/mhfdat.bin ./path/to/mhfinf.bin` - Dumps mhfpac, mhfdat, mhfinf. (**Note:** Suffix is unknown).

## FrontierTextTool
- Integration name: `FrontierTextTool`
- Example usage: `FrontierTextTool <arguments>`

### Commands
**Note:** This tool is largely unknown and untested, if you know more information about it, please open an issue.
- `FrontierTextTool dump ./path/to/mhfdat.bin` - Dump mhfpac.bin 4416 1278872 / Dump mhfdat.bin 3072 3328538 (Taken from comments in file).
- `FrontierTextTool insert inputFile inputCsv` - Append translations and update pointers (Taken from comments in file).
- `FrontierTextTool merge oldCsv newCsv` - Merge old and updated csvs (Taken from comments in file).
- `FrontierTextTool cleanTrados file` - Clean pollution caused by Trados or other CAT (Taken from comments in file).
- `FrontierTextTool insertCAT catFile csvFile` - Insert CAT file to csv (Taken from comments in file).

## Credits
- [theBusBoy](https://github.com/theBusBoy)
- [Vuze](https://github.com/mhvuze)
- [suzaku830](https://github.com/suzaku830)