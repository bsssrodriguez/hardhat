# Win

## Package Manager

### Chocolatey

Install [Chocolatey](https://chocolatey.org/) - See [usage](https://chocolatey.org/docs/commandslist)

Open terminal **as Administrator** and run this command:

```bash
@"%SystemRoot%\System32\WindowsPowerShell\v1.0\powershell.exe" -NoProfile -InputFormat None -ExecutionPolicy Bypass -Command "iex ((New-Object System.Net.WebClient).DownloadString('https://chocolatey.org/install.ps1'))" && SET "PATH=%PATH%;%ALLUSERSPROFILE%\chocolatey\bin"
```

## Install

```bash
choco install jdk8
choco install android-sdk
choco install gradle --version 4.6
choco install git.install
choco install git-lfs.install
choco install nodejs-lts
choco install sigcheck --ignore-checksums
```

## NPM Packages

```bash
npm i -g npm
npm i -g gulp 
npm i -g eslint
```

## Android SDK

```bash
sdkmanager "emulator"
sdkmanager "ndk-bundle"
sdkmanager "build-tools;28.0.3"
sdkmanager "platforms;android-29"
sdkmanager "platforms;android-28"
sdkmanager "platforms;android-27"
sdkmanager "platforms;android-26"
sdkmanager "platforms;android-25"
sdkmanager "platforms;android-24"
sdkmanager "platforms;android-23"
sdkmanager "platforms;android-22"
sdkmanager "platforms;android-21"
sdkmanager "tools"
```

### Useful Commands

```bash
sdkmanager --list 
sdkmanager --update
```

[Complete command list](https://developer.android.com/studio/command-line/sdkmanager)

## Software

```bash
choco install vscode
choco install androidstudio
choco install sourcetree
choco install googlechrome
```