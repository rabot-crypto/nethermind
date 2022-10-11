# Rabot.Nethermind

This folder is dedicated for publishing nethermind packages. The routines and mechanics used here are in such a way written TO NOT touch the nethermind sources. By using these packages the provided .dll's are ONLY meant to be used during compile time but not during runtime. Because Nethermind has a plugin system it does provide you to inject a custom assembly.

## Upgrade

When you want to upgrade you need to update `VERSION_CORE` inside `./scripts/publish-package.sh`. 

## Publish

Run `./scripts/publish-package.sh` in `<repo>/rabot/` (workdir).

## Binding Packages

The binding directory located at `./binding` provides projects that produces "binding"-packages. They are intended for writing plugins.

### Facts

1. Produced packages contains the .dll of the first level ProjectReference's and their project dependencies.
2. Produced packages contains the external package references. The project references that would be implicitly be included as package references are NOT included.
3. The package provided .dll's are ONLY meant to be used during compile time but not during runtime. Why? See [Write a plugin](#write-a-plugin)

### Write a plugin

A project that is intended to be a plugin has the following expectations:

- it never has to be executed as a stand-alone process
- its produced .dll is about to be loaded into the SAME assembly context as Nethermind

This leads to some interestings facts for a project:

- it does need Nethermind dependencies during build process
- it is not necessary having Nethermind dependencies present as build or publish artifacts

## Functional Packages

The functional directory located at `./functional` provides projects that produces "functional"-packages. They are intended for writing stand-alone programs while using the full-fledged capabiltiies of Nethermind.