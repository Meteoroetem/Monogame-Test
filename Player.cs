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
    private int _currentFrame = 0;
    private bool _nextFrame = false;
    /// <value>
    /// The sprite's scale
    /// </value>
    private Rectangle _area;
    /// <summary>
    /// A rectangle that represents the location and boundries of the player
    /// </summary>
    /// <value>The location and boundries of the player</value>
    public Rectangle Area => _area;

    private Rectangle _currentSprite;
    /// <summary>
    /// The rectangle of the current sprite in the spritesheet
    /// </summary>
    /// <value>The location of the current sprite of the player on the spritesheet</value>
    public Rectangle CurrentSprite => _currentSprite;

    private double _frameTimer = 0;
    /// <summary>
    /// Counts how many seconds have passed since the last frame change.
    /// Changes in relation to the animation speed.
    /// </summary>
    /// <value>The seconds that have passed since the last frame</value>
    public double FrameTimer {
        get => _frameTimer;
        set{
            if(value >= 1/animationSpeed){
                _nextFrame = true;
                _frameTimer = 0;
            }
            else{
                _frameTimer = value;
            }
        }
    }

	public void NextFrame(){
        if(_nextFrame){
            _currentFrame = 1 - _currentFrame;
            var _currentFrames =
                (Rectangle[])
                this.GetType().GetField(_currentAnimation.ToLower() + "Frames").GetValue(this);
            _currentSprite = _currentFrames[_currentFrame];
            _nextFrame = false;
        }
    }
}