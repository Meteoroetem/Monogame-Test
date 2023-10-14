using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Monogame_Test;

public class Tilemap : List<List<Tile>>{
    //public readonly Tile[][] tiles;

    /// <summary>
    /// The only constructor for the Tilemap class, it takes a text file and coverts it to a 2D array of Tiles using the provided key
    /// </summary>
    /// <param name="levelFile">The text file containing the level. It's important that all of the lines are at the same length</param>
    /// <param name="parserKey">A function that takes a character as a parameter and returns a Tile</param>
    public Tilemap(Stream levelFile, Func<char, Tile> parserKey){
        using StreamReader reader = new(levelFile);
        string? line = reader.ReadLine();
        char[][] levelCode = new char[reader.ToString().Length/line.Length][];
        byte cnt = 0;
        while (line != null){
            levelCode[cnt] = line.ToCharArray();
            cnt++;
            line = reader.ReadLine();
        }
        var tiles = levelCode.Select(lines => lines.Select(character => parserKey(character)).ToList()).ToList();
        AddRange(tiles);
    }
}
