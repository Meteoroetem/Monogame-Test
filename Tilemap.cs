using System;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Monogame_Test;

public class Tilemap{
    public readonly Func<char,Tile> parserKey;
    public readonly Tile[][] tileGrid;
    public Tilemap(Stream levelFile, Func<char, Tile> parserKey){
        this.parserKey = parserKey;
        using StreamReader reader = new(levelFile);
        string? line = reader.ReadLine();
        char[][] levelCode = new char[reader.ToString().Length/line.Length][];
        byte cnt = 0;
        while (line != null){
            levelCode[cnt] = line.ToCharArray();
            cnt++;
            line = reader.ReadLine();
        }
        this.tileGrid = (Tile[][])levelCode.Select(rows => rows.Select(character => parserKey(character))).ToArray();
    }
}
