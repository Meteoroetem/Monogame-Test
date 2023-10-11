using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Monogame_Test;
/// <summary>
/// A 2D Player class. It loads a spritesheet and an array of rects to determine it's sprites. Has a NextFrame() method that needs to be called every frame.
/// </summary>
class Player
{
    /// <summary>
    /// Determines how many calculation cycles per second there will be.
    /// That will affect the animation speed aswell as the movement
    /// </summary>
    /// <value>Number of calculation cycles</value>
    public float cyclesPerSecond = 60;
    public float animationfps = 3;
    public float moveSpeed = 1;
    private int _spriteScale = 10;
    /// <value>
    /// The sprite's scale
    /// </value>
    public int SpriteScale{
        get => _spriteScale;
        set{
            _spriteScale = value <= 0 ? 1 : value;
        }
    }
    public Player(Texture2D _spriteSheet, Rectangle[] _idleFrames, Rectangle[] _rightFrames, Rectangle[] _leftFrames){
        spriteSheet = _spriteSheet;
        idleFrames = _idleFrames;
        rightFrames = _rightFrames;
        leftFrames = _leftFrames;
        _area = new Rectangle(0,0,11*_spriteScale,20*_spriteScale);
    }
	public readonly Texture2D spriteSheet;
	public readonly Rectangle[] idleFrames;
	public readonly Rectangle[] rightFrames;
	public readonly Rectangle[] leftFrames;
    private double _animationFrameTimer = 0;
    private double _cycleTimer = 0;
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
                    var _currentFrames = (Rectangle[])this.GetType().GetField(value.ToLower() + "Frames").GetValue(this);
                    _currentSprite = _currentFrames[_currentFrame];
                }
            }
        }
    }
    
    private Rectangle _area;
    private Vector2 _movementDue;
    /// <summary>
    /// A rectangle that represents the location and boundries of the player
    /// </summary>
    /// <value>The location and boundries of the player</value>
    public Rectangle Area =>
            new(_area.Location, new(11 * _spriteScale, 20 * _spriteScale));
    private Rectangle _currentSprite;
    /// <summary>
    /// The rectangle of the current sprite in the spritesheet
    /// </summary>
    /// <value>The location of the current sprite of the player on the spritesheet</value>
    public Rectangle CurrentSprite => _currentSprite;


	public void NextFrame(GameTime gameTime){
        _animationFrameTimer += gameTime.ElapsedGameTime.TotalSeconds;
        _cycleTimer += gameTime.ElapsedGameTime.TotalSeconds;
        if(_animationFrameTimer >= 1/animationfps){
            //? If the current frame is 1, it will become 0 and vice versa.
            _currentFrame = 1 - _currentFrame;
            var _currentFrames = (Rectangle[])
                this.GetType().GetField(_currentAnimation.ToLower() + "Frames").GetValue(this);
            _currentSprite = _currentFrames[_currentFrame];
            _animationFrameTimer = 0;
        }
        if(_cycleTimer >= 1/cyclesPerSecond){
            _area.Location = new(_area.Location.X + (int)_movementDue.X, _area.Location.Y + (int)_movementDue.Y);
            _movementDue = new(_movementDue.X - (int)_movementDue.X, _movementDue.Y - (int)_movementDue.Y);
            _cycleTimer = 0;
        }
    }
    ///<summary>
    ///Transforms the player by a Vector2 with a magnitude directly relative to the sprite's scale
    ///</summary>
    public void Transform(Vector2 byWhat){
        _movementDue += (_spriteScale*moveSpeed)*byWhat;
    }
}