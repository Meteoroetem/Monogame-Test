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
    /// <summary>
    /// The only constructor for the Tilemap class, it takes a text file and coverts it to a 2D array of Tiles using the provided key
    /// </summary>
    /// <param name="levelFile">The text file containing the level. It's important that all of the lines are at the same length</param>
    /// <param name="parserKey">A function that takes a character as a parameter and returns a Tile</param>
    public Tilemap(Stream levelFile, Func<char, Tile> parserKey){
        using StreamReader reader = new(levelFile);
        string line = reader.ReadLine();
        int tilemapHeight = (int)reader.BaseStream.Length/line.Length;
        char[][] levelCode = new char[tilemapHeight][];
        int cnt = 0;
        while (line != null){
            levelCode[cnt] = line.ToCharArray();
            cnt++;
            line = reader.ReadLine();
        }
        var tiles = levelCode.Select(lines => lines.Select(character => parserKey(character)).ToList()).ToList();
        AddRange(tiles);
    }
    public Texture2D GetTexture2D(GraphicsDevice gd){
        var returnTexture = new Texture2D(
            gd, this[0].Count * this[0][0].Texture.Width, this.Count * this[0][0].Texture.Width);
        Color[] texturePixelData = new Color[this[0].Count * this[0][0].Texture.Width * this.Count * this[0][0].Texture.Width];

        Color[][][] sourceTilesPixels = new Color[this.Count][][];

        //* Foreach tile in the tilemap
        for(int row = 0; row < this.Count; row++){
            for(int col = 0; col < this[row].Count; col++){
                this[row][col].Texture.GetData<Color>(sourceTilesPixels[row][col]);
            }
        }

        for(int i = 0; i < texturePixelData.Length; i++){
            int tileIndex = i / this[0][0].Texture.Width;
            int tileRow = tileIndex / this[0].Count;
            int tileCol = tileIndex % this[0].Count;
            texturePixelData[i] = sourceTilesPixels[tileRow][tileCol][i/this[tileRow][tileCol].Texture.Width];
        }
        returnTexture.SetData<Color>(texturePixelData);
        return returnTexture;
    }
}
