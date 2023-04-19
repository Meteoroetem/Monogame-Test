using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using vecs = VectSharp;


namespace Monogame_Test
{
    public class TextBox
    {
        public string text;
        public double fontSize = 20; 
        public Rectangle boundries;
        public readonly vecs::Font font;
        public readonly Texture2D textTexture;
        public TextBox(Rectangle rect, vecs::FontFamily fontFamily){
            boundries = rect;
            font = new(fontFamily, fontSize);
        }

    }
}