using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Monogame_Test;

class Player
{
    /// <summary>
    /// Determines how many frames per second the animation will have
    /// </summary>
    /// <value>The fps of the animation</value>
    public float animationSpeed = 3;
    public float moveSpeed = 0.5f;
    public int spriteScale = 10;
    public Player(Texture2D _spriteSheet, Rectangle[] _idleFrames, Rectangle[] _rightFrames, Rectangle[] _leftFrames){
        spriteSheet = _spriteSheet;
        idleFrames = _idleFrames;
        rightFrames = _rightFrames;
        leftFrames = _leftFrames;
        _area = new Rectangle(0,0,11*spriteScale,20*spriteScale);
    }
	public readonly Texture2D spriteSheet;
	public readonly Rectangle[] idleFrames;
	public readonly Rectangle[] rightFrames;
	public readonly Rectangle[] leftFrames;
    private double _frameTimer = 0;
    private int _currentFrame = 0;
    private string _currentAnimation = "Idle";
    /// <summary>
    /// Gets the a string representing the current animation.
    /// Setter only allows "Idle", "Right" or "Left" as values, any other value will be ignored.
    /// After setting, the animation will immediately change with the current frame as the starting point.
    /// </summary>
    /// <value>A string that represents the current animation</value>
    public string CurrentAnimation{
        get => _currentAnimation;
        set{
            if(value == "Idle" || value == "Right" || value == "Left"){
                if (_currentAnimation != value)
                {
                    _currentAnimation = value;
                    var currentFrames = (Rectangle[])this.GetType().GetField(value.ToLower() + "Frames").GetValue(this);
                    _currentSprite = currentFrames[_currentFrame];
                }
            }
        }
    }
    /// <value>
    /// The sprite's scale
    /// </value>
    private Rectangle _area;
    /// <summary>
    /// A rectangle that represents the location and boundries of the player
    /// </summary>
    /// <value>The location and boundries of the player</value>
    public Rectangle Area =>
            new Rectangle(_area.Location, new(11 * spriteScale, 20 * spriteScale));
    private Rectangle _currentSprite;
    /// <summary>
    /// The rectangle of the current sprite in the spritesheet
    /// </summary>
    /// <value>The location of the current sprite of the player on the spritesheet</value>
    public Rectangle CurrentSprite => _currentSprite;


	public void NextFrame(GameTime gameTime){
        _frameTimer += gameTime.ElapsedGameTime.TotalSeconds;
        if(_frameTimer >= 1/animationSpeed){
            //? If the current frame is 1, it will become 0 and vice versa.
            _currentFrame = 1 - _currentFrame;
            var _currentFrames = (Rectangle[])
                this.GetType().GetField(_currentAnimation.ToLower() + "Frames").GetValue(this);
            _currentSprite = _currentFrames[_currentFrame];
            _frameTimer = 0;
        }
    }
    public void Transform(Vector2 byWhat){
        _area.Location = (_area.Location.ToVector2() + spriteScale*moveSpeed*byWhat).ToPoint();
    }
}