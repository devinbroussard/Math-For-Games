using System;
using System.Collections.Generic;
using System.Text;
using Math_Library;
using Raylib_cs;

namespace Math_For_Games
{
    class UIText : Actor
    {

        private string _text;
        private int _width;
        private int _height;
        Player _player;

        /// <summary>
        /// The text that will appear inside the text box
        /// </summary>
        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }

        /// <summary>
        /// How wide the text box is.  If the cursor is outside the max
        /// x position the text will wrap around.
        /// </summary>
        public int Width
        {
            get { return _width; }
            set { _width = value; }
        }

        /// <summary>
        /// How tall the text box is. If the cursor is outwide the max 
        /// y position the text is truncated.
        /// </summary>
        public int Height
        {
            get { return _height; }
            set { _height = value; }
        }

        //public UIText(Player player)
        //{
        //    _player = player;
        //}

        /// <summary>
        /// Sets the starting values for the text box
        /// </summary>
        /// <param name="x">The x position of the text box</param>
        /// <param name="y">The y position of the text box</param>
        /// <param name="name">The name of the text box</param>
        /// <param name="color">The color of the text</param>
        /// <param name="width">How wide the text box is</param>
        /// <param name="height">How tall the text box is</param>
        /// <param name="text">The tex that will be displayed</param>
        public UIText(float x, float y, string name, Color color, int width, int height, string text = "")
            : base('\0', x, y, color, name)
        {
            Text = text;
            Width = width;
            Height = height;
        }

        //public override void Draw()
        //{
        //    Console.SetCursorPosition(0, 0);
        //    Console.ForegroundColor = ConsoleColor.White;
        //    if (!_player.IsGameOver)
        //    {
        //        Console.WriteLine($"Current Stroke: {_player.StrokeCounter}");
        //        return;
        //    }
        //    Console.WriteLine($"You won in {_player.StrokeCounter} strokes!");

        //}

        public override void Draw()
        {
            //Store the position of the cursor
            int cursorPosX = (int)Position.X;
            int cursorPosY = (int)Position.Y;

            //Create a new icon to store the current character and color
            Icon currentLetter = new Icon { Color = Icon.Color };

            //Convert the string for text into a character array
            char[] textChars = Text.ToCharArray();

            //Iterate through all characters in the string
            for (int i = 0; i < textChars.Length; i++)
            {
                //Set the icon symbol to be the current character in the array
                currentLetter.Symbol = textChars[i];

                if (currentLetter.Symbol == '\n')
                {
                    cursorPosX = (int)Position.X;
                    cursorPosY++;
                    continue;
                }

                //Increment the cursor position so the letters are set side by side
                cursorPosX++;

                //Go to the next line if the cursor has reached the max position
                if (cursorPosX > (int)Position.X + Width)
                {
                    //Reset the cursor x position and increase the y position
                    cursorPosX = (int)Position.X;
                    cursorPosY++;
                }
                //If the cursor has reached the maximum height...
                if (cursorPosY > (int)Position.Y + Height)
                    //...leave the loop
                    break;
            }
        }
    }
}
