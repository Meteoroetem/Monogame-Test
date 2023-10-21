using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Monogame_Test;

public class TilemapTestGame : Game
{
	private readonly GraphicsDeviceManager _graphics;
	private SpriteBatch spriteBatch;
	public Tile TestLevelKey(char c) => c switch{
		'.' => new(new(GraphicsDevice,18,18), TileType.blank),
		'<' => new(Content.Load<Texture2D>("Tiles/Cloud-Left"), TileType.platform),
		'_' => new(Content.Load<Texture2D>("Tiles/Cloud-Middle"), TileType.platform),
		'>' => new(Content.Load<Texture2D>("Tiles/Cloud-Right"), TileType.platform),
		'~' => new(Content.Load<Texture2D>("Tiles/Grass-Top"), TileType.solid),
		'#' => new(Content.Load<Texture2D>("Tiles/Dirt"), TileType.solid),
		_ => throw new ArgumentOutOfRangeException(nameof(c), $"Not expected char value: {c}")
	};
	Level testLevel;

	public TilemapTestGame(){
		_graphics = new GraphicsDeviceManager(this);
		Content.RootDirectory = "Content";
		IsMouseVisible = true;
	}

	protected override void Initialize(){
		// TODO: Add your initialization logic here
		base.Initialize();
	}

	protected override void LoadContent(){
		spriteBatch = new SpriteBatch(GraphicsDevice);
        // TODO: use this.Content to load your game content here
        using Stream levelStream = TitleContainer.OpenStream("Content/Levels/TestLevel.lvl");
        testLevel = new(
            new(
                levelStream,
                TestLevelKey),
            new(0, 0, 180, 180));
    }

	protected override void Update(GameTime gameTime){
		// TODO: Add your update logic here

		base.Update(gameTime);
	}

	protected override void Draw(GameTime gameTime){
		GraphicsDevice.Clear(Color.CornflowerBlue);

		// TODO: Add your drawing code here
		spriteBatch.Begin(samplerState:SamplerState.PointClamp);
		testLevel.Draw(ref spriteBatch);
		spriteBatch.End();
		base.Draw(gameTime);
	}
}
