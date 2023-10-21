using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Monogame_Test;

#nullable enable
public class Tile{
    public readonly Texture2D? Texture;
    public readonly TileType Type;
    public Tile(Texture2D? texture, TileType type){
        this.Texture = texture;
        this.Type = type;
    }

}

public enum TileType{
    platform,
    solid,
    foreground,
    blank
}