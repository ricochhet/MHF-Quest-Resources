# MHF-Quest-Resources
A lot of what is in this repo is experimental and is not guarenteed to be functional.

The quest editor currently has a lot of the necessary functions to be used for most purposes. 
It is now in state that is "usable." Although very clunky and messy. I primarily plan to work on 
ease-of-use rather than adding any extra features at this moment in time.

## Contributing 
If you have information, resources, or code to share, please do! Feel free to open an issue or a pull request. Any help would be greatly appreciated!
**Note:** When contributing code, please try to use the `main` branch, `legacy` is the original, and should not be updated.

I don't have a lot of free time, so I work on bits and pieces while I can. I would really appreciate any help that I can get. I would work more on this if I could, but it's slow going. At the current rate, you can expect 1-2 updates per month.

### TODO / IMPORTANT INFORMATION / THINGS TO CONTRIBUTE (Feel free to contribute if you know anything listed here).
- The quest tool cannot reliably edit files other than ones ending in `d0` (day, warm). Whether this affects replacing quests is untested.
    - It might be possible to simply duplicate the quest multiple times, replacing the `d0` with other values. If someone tested or knows about this, please open an issue.
    - Although the quest tool works the best on `d0` type quests, it does work on other types with mixed results. (Different offsets?) How this affects quest replacing is untested.
- The `FrontierTools\output-tests` directory includes a couple of examples of valid quest logs. You can use these as a reference point.

## Resources
- Guides can be found in: `\Docs\guides\`.
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
- Enter the source directory using `cd MHF-Quest-Resources\FrontierTools`
- To build; use `dotnet build`, or you can use it from source via `dotnet run <integration> <arguments>`
- If you are not using this tool from source, open a command prompt in the build directory (next to the .EXE).

## Usage P.1 (Integrations)
This tool implements both a quest reader and writer (limited), as well as ReFrontier tools (see: Resources).

## Quest Reader / Writer
**Note:** If you want to actually write to file via the quest writer. Change Debug variable in `FrontierTools\Tools\QuestTool\QuestWriter.cs` to false.
- Integration name: `QuestTool`
- Example usage: `QuestTool <arguments>`.

### Commands
- `QuestTool ./path/to/quest.bin -log` - Logs quest information to a file. (`FrontierTools/output/`).
- `QuestTool -strToHex My Text` - Converts a string to a hex / byte string.
- `QuestTool -decToByte My Decimal` - Converts a decimal value to hex / decimal bytes. Example usage - `QuestTool -decToByte 7000000`
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

**Note:** Working with 4 byte values can be tricky. For example, the bytes for 7,000,000,000 is `C0, CF, 6A, 00`, but each byte needs to be converted to a decimal.
Converting each byte will give you the proper output of `192 207 106 00`. There is a command to help with this. Check: `-decToByte`.
- `QuestTool ./path/to/quest.bin -edit -questFee` - Sets the quest fee. Example usage - `QuestTool ./path/to/quest.bin -edit -questFee 100,00` 
- Unsafe Flag: It's not fully tested whether the quest fee can / uses all 4 bytes in any scenario. The `-unsafe` flag allows you to use a maximum of 4 bytes.
You can use this flag via ``QuestTool ./path/to/quest.bin -edit -unsafe -questFee 192,207,106,00` as an example. Without it, you can use a maximum of 2 bytes. Although you can write in any amount of bytes you want without the unsafe flag, it will always take a maximum of 2.

- `QuestTool ./path/to/quest.bin -edit -questRewardMain` - Sets the primary quest reward. Example usage - `QuestTool ./path/to/quest.bin -edit -questRewardMain 100,00` 
- Unsafe Flag: It's not fully tested whether the primary quest reward can / uses all 4 bytes in any scenario. The `-unsafe` flag allows you to use a maximum of 4 bytes.
You can use this flag via ``QuestTool ./path/to/quest.bin -edit -unsafe -questRewardMain 192,207,106,00` as an example. Without it, you can use a maximum of 2 bytes. Although you can write in any amount of bytes you want without the unsafe flag, it will always take a maximum of 2.

- `QuestTool ./path/to/quest.bin -edit -questRewardA` - Sets the primary quest reward. Example usage - `QuestTool ./path/to/quest.bin -edit -questRewardA 100,00` 
- Unsafe Flag: It's not fully tested whether the primary quest reward can / uses all 4 bytes in any scenario. The `-unsafe` flag allows you to use a maximum of 4 bytes.
You can use this flag via ``QuestTool ./path/to/quest.bin -edit -unsafe -questRewardA 192,207,106,00` as an example. Without it, you can use a maximum of 2 bytes. Although you can write in any amount of bytes you want without the unsafe flag, it will always take a maximum of 2.

- `QuestTool ./path/to/quest.bin -edit -questRewardB` - Sets the primary quest reward. Example usage - `QuestTool ./path/to/quest.bin -edit -questRewardB 100,00` 
- Unsafe Flag: It's not fully tested whether the primary quest reward can / uses all 4 bytes in any scenario. The `-unsafe` flag allows you to use a maximum of 4 bytes.
You can use this flag via ``QuestTool ./path/to/quest.bin -edit -unsafe -questRewardB 192,207,106,00` as an example. Without it, you can use a maximum of 2 bytes. Although you can write in any amount of bytes you want without the unsafe flag, it will always take a maximum of 2.

**Note:** See `FrontierTools\Utils\RankDictionary.cs` for values.
- `QuestTool ./path/to/quest.bin -edit -questRank` - Sets quest rank. Example usage - `QuestTool ./path/to/quest.bin -edit -questRank 11`

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
- [pacomusume](https://github.com/pacomusume)