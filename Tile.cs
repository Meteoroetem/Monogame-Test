using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Monogame_Test;

#nullable enable
public record Tile(Texture2D? Texture, TileType Type){
    public Rectangle Area {get; init;}
}

public enum TileType{
    platform,
    solid,
    foreground,
    blank
}