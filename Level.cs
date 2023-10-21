using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Runtime.CompilerServices;

namespace Monogame_Test;

public class Level
{
	public readonly string Name;
	public readonly byte Id;
	public readonly Tilemap Tilemap;
	public readonly Rectangle CameraRect;
	public Level(Tilemap tilemap, Rectangle cameraRect){
		this.Tilemap = tilemap;
		this.CameraRect = cameraRect;
	}
	public void Draw(ref SpriteBatch spriteBatch){
		int tileWidth = Tilemap[0][0].Texture.Width;
		int tileDrawWidth = CameraRect.Width/tileWidth;
		int tileDrawHeight = CameraRect.Width/tileWidth;
		
		for (int i = 0; i < tileDrawHeight; i++){
			for (int j = 0; j < tileDrawWidth; j++){
				try{
					spriteBatch.Draw(
						Tilemap[i][j].Texture,
						new Vector2(j*tileWidth,i*tileWidth),
						Color.White);
				}
				catch (ArgumentOutOfRangeException){
					break;
				}
			}
		}
	}
}
