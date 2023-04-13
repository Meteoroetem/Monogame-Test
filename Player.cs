using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Monogame_Test;

class Player
{
    public Player(Texture2D _spriteSheet, Rectangle[] _idleFrames, Rectangle[] _rightFrames, Rectangle[] _leftFrames){
        spriteSheet = _spriteSheet;
        idleFrames = _idleFrames;
        rightFrames = _rightFrames;
        leftFrames = _leftFrames;
        area = new Rectangle(0,0,220,400);
    }
	public readonly Texture2D spriteSheet;
	public readonly Rectangle[] idleFrames;
	public readonly Rectangle[] rightFrames;
	public readonly Rectangle[] leftFrames;
    private Rectangle area;
    public Rectangle GetArea() => area;
    private Rectangle currentSprite;
    public Rectangle GetCurrentSprite() => currentSprite;
    private string currentAnimation = "Idle";
    private int currentFrame = 0;

	public void NextFrame(double elapsedMillis){
        if(elapsedMillis % 30 == 0){
            currentFrame = currentFrame > 0 ? 0 : 1;
            try{
                switch (currentAnimation){
                    case "Idle":
                        currentSprite = idleFrames[currentFrame];
                        area.Height = currentSprite.Height*20;
                        break;
                    case "Right":
                        currentSprite = rightFrames[currentFrame];
                        break;
                    case "Left":
                    currentSprite = leftFrames[currentFrame];
                    break;
                }
            }
            catch(IndexOutOfRangeException){}
        }
    }
}