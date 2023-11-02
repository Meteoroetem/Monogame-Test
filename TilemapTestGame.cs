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
	Player sunflower;
	Vector2 playerVelocity;
	Vector2 playerAcceleration;

	public TilemapTestGame(){
		_graphics = new GraphicsDeviceManager(this);
		IsMouseVisible = true;
	}

	protected override void Initialize(){
		// TODO: Add your initialization logic here
		playerAcceleration = new(0, 0.05f);
		playerVelocity = new(0,0);

		base.Initialize();
	}

	protected override void LoadContent(){
		spriteBatch = new SpriteBatch(GraphicsDevice);
		Content.RootDirectory = "Content";
        using Stream levelStream = TitleContainer.OpenStream("Content/Levels/TestLevel.lvl");
        testLevel = new(
            new(
                levelStream,
                TestLevelKey),
            new(0,0,18*10,18*15));

        sunflower = new(
            Content.Load<Texture2D>("Short_Sunflower_Sprite_Sheet"),
            new Rectangle[2]{
                new(0,0,11,20), new(0,21,11,20)},
            new Rectangle[2]{
                new(12,0,11,20), new(12,21,11,20)
            },
            new Rectangle[2]{
                new(24,0,11,20), new(24,21,11,20)
            }){
            	SpriteScale = 5
        	};
    }

	protected override void Update(GameTime gameTime){
		bool leftKeyPressed = Keyboard.GetState().IsKeyDown(Keys.Left);
        bool rightKeyPressed = Keyboard.GetState().IsKeyDown(Keys.Right);
        //bool upKeyPressed = Keyboard.GetState().IsKeyDown(Keys.Up);
        //bool downKeyPressed = Keyboard.GetState().IsKeyDown(Keys.Down);
        //bool spaceBarPressed = Keyboard.GetState().IsKeyDown(Keys.Space);

		playerVelocity += playerAcceleration;
		playerAcceleration.X = playerVelocity.X < 0 ? 1 : playerVelocity.X > 0 ? -1 : 0;

		if(rightKeyPressed)
        {
            if(leftKeyPressed)
                sunflower.CurrentAnimation = "Idle";
            else
            {
                sunflower.CurrentAnimation = "Right";
                playerVelocity.X = 1;
            }
        }
        else if(leftKeyPressed)
        {
            sunflower.CurrentAnimation = "Left";
            playerVelocity.X = -1;
        }
        else{
            sunflower.CurrentAnimation = "Idle";
        }

		/*
		testLevel.CameraRect.X += leftKeyPressed && testLevel.CameraRect.X >= 5 ? -5 : (rightKeyPressed ? +5 : 0);
		testLevel.CameraRect.Y += upKeyPressed  && testLevel.CameraRect.Y >= 5 ? -5 : (downKeyPressed ? +5 : 0);
		*/
		if (sunflower.Area.Location.Y >= _graphics.PreferredBackBufferHeight-sunflower.Area.Height){
			playerVelocity.Y = 0;
		}
		sunflower.Transform(playerVelocity);
		sunflower.NextFrame(gameTime);
		base.Update(gameTime);
	}

	protected override void Draw(GameTime gameTime){
		GraphicsDevice.Clear(Color.CornflowerBlue);

		// TODO: Add your drawing code here
		spriteBatch.Begin(samplerState:SamplerState.PointClamp);
		testLevel.Draw(ref spriteBatch, _graphics);
		spriteBatch.Draw(sunflower.SpriteSheet, sunflower.Area, 
            sunflower.CurrentSprite, Color.White);
		spriteBatch.End();
		base.Draw(gameTime);
	}
}
