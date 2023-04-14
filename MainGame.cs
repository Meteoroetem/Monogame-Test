﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Monogame_Test;

public class MainGame : Game
{
	private GraphicsDeviceManager _graphics;
	private SpriteBatch spriteBatch;
    Player myPlayer;
    bool isKeyJustPressed = false;
    bool wasKeyPressed = false;


    public MainGame()
	{
		_graphics = new GraphicsDeviceManager(this);
		Content.RootDirectory = "Content";
		IsMouseVisible = true;
	}

	protected override void Initialize()
	{
		// TODO: Add your initialization logic here 
		base.Initialize();
	}

	protected override void LoadContent()
	{
		spriteBatch = new SpriteBatch(GraphicsDevice);
        // TODO: use this.Content to load your game content here
        myPlayer = new(
            Content.Load<Texture2D>("Short_Sunflower_Sprite_Sheet"),
            new Rectangle[2]{
                new(0,0,11,20), new(0,21,11,20)}, 
            new Rectangle[2]{
                new(12,0,11,20), new(12,21,11,20)
            }, 
            new Rectangle[2]{
                new(24,0,11,20), new(24,21,11,20)
            });
	}

	protected override void Update(GameTime gameTime)
	{
		if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
			Exit();
        
        // TODO: Add your update logic here
        if(Keyboard.GetState().IsKeyDown(Keys.Right)){
            isKeyJustPressed = wasKeyPressed ? false : true;
            myPlayer.CurrentAnimation = "Right";
            if(Keyboard.GetState().IsKeyDown(Keys.Left)){
                myPlayer.CurrentAnimation = "Idle";
            }
        }
        else if(Keyboard.GetState().IsKeyDown(Keys.Left)){
            isKeyJustPressed = wasKeyPressed ? false : true;
            myPlayer.CurrentAnimation = "Left";
        }
        else{
            myPlayer.CurrentAnimation = "Idle";
        }
        wasKeyPressed = !Keyboard.GetState().IsKeyUp(Keys.Right) || !Keyboard.GetState().IsKeyUp(Keys.Left);

        myPlayer.FrameTimer += gameTime.ElapsedGameTime.TotalSeconds;
        myPlayer.NextFrame();
        base.Update(gameTime);
	}

	protected override void Draw(GameTime gameTime)
	{
		GraphicsDevice.Clear(Color.CornflowerBlue);

		// TODO: Add your drawing code here
        spriteBatch.Begin(samplerState:SamplerState.PointClamp);
        spriteBatch.Draw(myPlayer.spriteSheet, myPlayer.Area, 
            myPlayer.CurrentSprite, Color.White);
        spriteBatch.End();
        base.Draw(gameTime);
	}
}
