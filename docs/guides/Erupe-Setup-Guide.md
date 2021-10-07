# Erupe Experimental Server Setup Guide
The erupe server is still heavily in development and you should expect numerous bugs, crashes, and other unintended behavior during use.

Really take a moment to figure out *why* you want to do this setup, and if you're capable enough *to* do it. This guide tries to make everything as simple as possible, but will still require a fairly good understanding of how computers operate. 

This server is *experimental*, many bugs, crashes, and other unintended behavior ***WILL*** occur. This is not suited for gameplay, you can *play* the game, but keep in mind the above. This is primarily for development and research purposes. 

This guide is intended for use on **Windows 10** platforms.

## Resources
- [Erupe Server](https://github.com/ErupeServer/Erupe)
- [Progression files](https://archive.org/details/mhfz_progression)

# Server Side Installation

## Step 1: Download the Server files (Server)
- Download the [code repository (repo)](https://github.com/ErupeServer/Erupe) using the dropdown within the green “Code” button and choose: Download ZIP.
- Extract the contents of the folder into the directory of you choice (I would recommend avoiding Program Files due to possible permission errors).

## Step 2: Download and Install PostgreSQL (Database)
- Download the [PostgreSQL Installer](https://www.enterprisedb.com/downloads/postgres-postgresql-downloads) for your operating system and choose the latest PostgreSQL version.
- Run the Installer
    - Ensure these components are checked to install:
        - PostgreSQL Server (the actual database technology).
        - pgAdmin 4 (a GUI management tool for our PostgreSQL server).
        - Command Line Tools (a command line tool for managing PostgreSQL servers) - not used in this tutorial but you’ll want it for some management stuff in the future.
    - Select the Data Directory of your choice (the default pre-filled option should be fine).
    - Create a password for the default database user (postgres).
    - Select a port for the database server to listen to (the default 5432 should be fine).
    - Select the Locale (the default should be fine).
    - Finish the installation.

## Step 3: Download and Install Golang (Server language)
- Download the [Golang Installer](https://golang.org/dl/) for your operating system.
- Run the installer using the default options.

## Step 4: Prepare the Database & Install Migration Tools (Facilitates setting up the tables to store information in the database)
### Phase 1: Create a database called `erupe` on our PostgreSQL instance.
- Open pgAdmin 4 and connect using the credentials you supplied in the Download and Install PostgreSQL steps (the default username is postgres).
    - Right click on your PostgreSQL server and select Create -> Database (photo below for assistance).
    - Fill in the Database field with the value: `erupe`.
        - Click save.
    - You should now see a second database called `erupe` created in the browser on the left.

### Phase 2: Get the programs and tools to install `golang-migrate`.
- Download the latest version of [PowerShell](https://github.com/PowerShell/PowerShell/releases) from its Github releases page.
    - Most users will want the file under assets named something like: `PowerShell-7.0.4-win-x64.msi` (the version may change depending on when you access this page).
    - Run the installer.
- Open Powershell and use it to install [Scoop](https://scoop.sh/) by typing the command: `iwr -useb get.scoop.sh | iex`.
- Install [golang-migrate](https://github.com/golang-migrate/migrate/tree/master/cmd/migrate) by opening a new Powershell window and typing the command:
    - `scoop install migrate`.

### Phase 3: Run the migrations in the erupe server files to create the database tables.
- Navigate to the root directory of your erupe server files (image for context below).
- In the file browser path, type in cmd and then press enter to launch a command prompt at this location.
- Run the command: 
    - `migrate -database postgres://postgres:password@localhost:5432/erupe?sslmode=disable -path migrations up`.
    - **NOTE:** Replace the `password` with the password you set up during the database installation step. If you changed the default port from 5432 during the database installation step, replace it.

## Step 5: Edit the config.json
- Open the `config.json` file with the text editor of your choice.
- Under the “database” section, find the port, username, and password fields and change their values to be whatever you chose during the database installation steps.
- **NOTE:** You may not need to change the port value if you kept the default `5432` port. The default PostgreSQL username is `postgres`.
- Replace the 127.0.0.1 (localhost) with your external IPV4 address (of your router).

## Step 6: Port-forwarding (OPTIONAL)
Your network is set to block incoming requests by default (so that malicious actors aren’t able to get into your network), but sometimes we want users outside of our network to be able to reach services (for example, the server software) without being stopped by our network security. The solution to this is port forwarding, where we essentially say “Hey, if users try to reach resources on this port, let them through”.

In the config.json, there are port entries for the following services (these values can change if you’ve edited your config to use non-default values):

```
Database: 5432
Launcher: 80
Sign: 53312
Channel: 54001
Entrance: 53310
```

You will want to forward ports for everything below the line. You could forward the port for the database, but you likely don’t want external users having access directly to the PostgreSQL database because they interact with the server which interacts with the database on their behalf. If you forward the port for the database, the only thing stopping users from doing anything they want on your database instance is not knowing your database user (which is the default postgres so they actually do know that) and its password (which they really shouldn’t know). If that password is compromised, all bets are off. In short, don’t port forward the database port unless you have a reason to and know what you’re doing. Once you’ve forwarded these ports for the various services that use them, outside clients IN THEORY should be able to connect to your server (once it’s running in Step 7).

## Step 7: Run the server
- Navigate to the root directory of your erupe server files.
- In the file browser path, type in cmd and then press enter to launch a command prompt at this location.
- Enter the following command: `go run .`.
- Assuming everything has been set up correctly, you should now have a functioning server that clients are able to connect to (they need to follow the section about adding entries to their hosts files here, but instead of 127.0.0.1 they should be entering the IP you found in step 6.
    - In short, what this does is change the outbound request for the urls (example being `mhfg.capcom.com.tw`) routing from that actual location to the newly specified IP address. This enables the server to get this request and act as if it’s the (now offline) Capcom server.
    - If you want to play the game on the same machine you’re hosting the server on, you also need to do the host entries instructions above but you WILL KEEP the `127.0.0.1` entries.
- To close the server, press `CTRL + C`.

# Client Side Installation

## Step 1: Download the Japanese Pre-Installed Client Files
- Get the [MHF-ZZ_Installed_Files.zip from archive.org](https://archive.org/download/mhfzzinstalledfiles_20200204).
- Extract the contents of this folder wherever you want the game.

## Step 2: Download Python & Install Frida
- [Download the latest Python 3 release](https://www.python.org/downloads/).
- Install and **MAKE SURE YOU SELECT THE OPTION TO ADD PYTHON TO PATH.**
- Open a new command prompt (not a python shell).
- Enter the command: `pip install frida`.

## Step 3: Download / Copy the Client Patcher
- Within your `MFH-ZZ_Installed_Files` folder (or whatever you named the folder containing mhf.exe) either [download this script](https://gist.github.com/Andoryuuta/a51d9f79114d64946b9e0656cdc0a72e) or copy and paste it into the same named file within this directory.
- **NOTE:** This has to be done for two reasons, there are protections on the mhf.exe itself called AsProtect and an ~~Anti-Cheat~~ Malware program called GameGuard. Both prevent us from being unable to connect to private servers, and this python script bypasses both to allow us to successfully launch the game.

## Step 4: Edit your HOSTS file
- We need to fool our client into thinking it’s reaching out to the official capcom jp or tw servers when in reality it’s connecting to our local ip (127.0.0.1) or an external host ip (the external ipv4 address of whomever is hosting).
- Navigate to `C:/Windows/System32/drivers/etc/hosts` and add the following entries:

```
127.0.0.1 mhfg.capcom.com.tw
127.0.0.1 mhf-n.capcom.com.tw
127.0.0.1 cog-members.mhf-z.jp
127.0.0.1 www.capcom-onlinegames.jp
127.0.0.1 srv-mhf.capcom-networks.jp
```

**NOTE:** Any time you’re messing with files in System32, really take an extra minute to verify you know what operation you’re doing and why. I imagine a lot of less technically inclined people will try this, and in general anytime you find yourself in some part of the System32 directory ensure that you’re not being led horribly astray. That’s why throughout this guide I try to give you the “why” of what you’re doing.

## Step 5: Run the Client Patcher
- For this to work, the server has to be running in order to authenticate against our private server.
- Open a command prompt as an administrator and navigate to the root directory of your MHF-ZZ_Installed_Files folder (or wherever you extracted its contents) and enter the following: `py no_gg_jp.py`.
    - The game launcher should now open and bring you to a screen with a username and password. Enter anything for these as the server will create a new entry in the db if the user doesn’t exist.
    - Select the premade character and enter the game. This will allow you to make your actual character in-game.


# Credits
Huge thanks goes to [theBusBoy](https://github.com/theBusBoy) for his help. I honestly wouldn't have ever worked on this project had it not been for his help. I've kept the original PDF format of his guide in `\Docs\guides\Setting up Erupe.pdf`.