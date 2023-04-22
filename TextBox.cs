using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using vecs = VectSharp;
using VectSharp.Raster;


namespace Monogame_Test
{
    public class TextBox
    {
        public string Text{
            get => _text;
            set{
                box.Size = new Point((int)font.MeasureText(value).Width + 5, (int)font.MeasureText(value).Height + 5);
                _text = value;
            }
        }
        private string _text;
        public double fontSize = 20; 
        public Rectangle box;
        public vecs::Colour textFillColor = vecs::Colours.Black;
        public vecs::Colour textStrokeColor = vecs::Colours.Black;

        public readonly vecs::Font font;

        private Texture2D _textTexture;
        public Texture2D GetTexture2D(GraphicsDevice gd){
            vecs::Page page = new(box.Width, box.Height);
            vecs::Graphics graphics = page.Graphics;
            graphics.StrokeText(new vecs::Point(0, 0), Text, font, textStrokeColor);
            graphics.FillText(new vecs::Point(0, 0), Text, font, textFillColor);
            Stream stream = new MemoryStream();
            page.SaveAsPNG(stream);
            _textTexture = Texture2D.FromStream(gd, stream);
            return _textTexture;
        }
        public TextBox(Point position, vecs::FontFamily fontFamily, double _fontSize = 60, string _text = "Lorem Ipsum"){
            fontSize = _fontSize;
            font = new(fontFamily, fontSize);
            Text = _text;
            box = new(position, new Point((int)font.MeasureText(Text).Width + 5, (int)font.MeasureText(Text).Height + 5));
        }

    }
}