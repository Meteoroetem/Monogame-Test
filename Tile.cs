using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Monogame_Test;

public class Tile{
    public readonly Texture2D texture;
    public readonly TileType type;
    public Tile(Texture2D texture, TileType type){
        this.texture = texture;
        this.type = type;
    }

}

public enum TileType{
    platform,
    solid,
    foreground
}