# Mykaelos Unity Library

MUL is a pile of reusable code that I drag from Unity project to Unity project. It's meant to make basic, repeated tasks easier each time I start something new. I change any and all of this code constantly to fit my new way of doing things, and it evolves with my new standards. So expect it to change, a lot. MUL is best used for examples and ideas on how to solve a problem so you can write far better code then I did.

The other side goal of this library (that I need to constantly remind myself) is to be used in [Ludum Dare](https://ldjam.com/). I want to stay compliant with the [Compo rules](http://ludumdare.com/compo/rules/):

1. You must work alone (solo).
2. Your game, all your content (i.e. Art, Music, Sound, etc) must be created in 48 hours.
3. Source code must be included. You're free to use any tools or libraries to create your game. You're free to start with any base-code you may have. At the end, you will be required to share your source code.

Making MUL open and available to everyone, and essentially base code that I made myself, satisfies #3. This also provides a great opportunity to quickly learn how to do things that I might otherwise just buy as an asset from the Unity store. Also, it's faster to use code that I created and know intimately. Finally, I'm slowly realizing that creating a large base of tools is the only way to build enough momentum to create truly great games, especially in painfully limited time frames (AKA game jams).


## How to add to your project

MUL is designed to be added to a Unity project as a git submodule. This allows me to make changes to MUL directly as I make new games and allows MUL to evolve over time with concrete use cases. The easiest way to add MUL is to just plop it down in the Assets folder.

```
cd UnityGameProjectFolder
git submodule add https://github.com/Mykaelos/MykaelosUnityLibrary.git Assets/lib/MykaelosUnityLibrary
```


## Authors

* **Mykaelos** - [Mykaelos on Github](https://github.com/Mykaelos) [@Mykaelos](https://twitter.com/Mykaelos) [Blog](http://www.mykaelos.com)


## License

This project is licensed under the Apache License 2.0 - see the [LICENSE.md](LICENSE.md) file for details.
Pretty much do whatever you want with this code. It either came from me or CC0 sources. I can't promise any of it working, ever, and you use the code at your own risk. Also, most of this code will be in a constant state of flux, because I change it as my projects evolve. So MUL may be better used as examples of ways to solve specific coding problems, and as a starting point to your own better solutions.
