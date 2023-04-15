using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Monogame_Test;

public class MainGame : Game
{
	private GraphicsDeviceManager _graphics;
	private SpriteBatch spriteBatch;
    Player myPlayer;
    /*bool isKeyJustPressed = false;
    bool wasKeyPressed = false;*/


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
        bool leftKeyPressed = Keyboard.GetState().IsKeyDown(Keys.Left);
        bool rightKeyPressed = Keyboard.GetState().IsKeyDown(Keys.Right);
        bool upKeyPressed = Keyboard.GetState().IsKeyDown(Keys.Up);
        bool downKeyPressed = Keyboard.GetState().IsKeyDown(Keys.Down);
        bool spaceBarPressed = Keyboard.GetState().IsKeyDown(Keys.Space);
        bool shiftKeyPressed = Keyboard.GetState().IsKeyDown(Keys.LeftShift);

        if(rightKeyPressed)
        {
            myPlayer.CurrentAnimation = "Right";
            myPlayer.Transform(Vector2.UnitX);

            if(leftKeyPressed)
                myPlayer.CurrentAnimation = "Idle";
        }
        else if(leftKeyPressed)
        {
            myPlayer.CurrentAnimation = "Left";
            myPlayer.Transform(-Vector2.UnitX);
        }
        else{
            myPlayer.CurrentAnimation = "Idle";
        }

        if(downKeyPressed)
            myPlayer.spriteScale += 1;
        else if(upKeyPressed)
            myPlayer.spriteScale -= 1;

        if(spaceBarPressed){
            myPlayer.moveSpeed = 1;
            myPlayer.animationSpeed = 6;
        }
        else{
            myPlayer.moveSpeed = 0.5f;
            myPlayer.animationSpeed = 3;
        }

        myPlayer.NextFrame(gameTime);
        base.Update(gameTime);
	}

	protected override void Draw(GameTime gameTime)
	{
		GraphicsDevice.Clear(Color.CornflowerBlue);

        spriteBatch.Begin(samplerState:SamplerState.PointClamp);
        spriteBatch.Draw(myPlayer.spriteSheet, myPlayer.Area, 
            myPlayer.CurrentSprite, Color.White);
        spriteBatch.End();
        base.Draw(gameTime);
	}
}
