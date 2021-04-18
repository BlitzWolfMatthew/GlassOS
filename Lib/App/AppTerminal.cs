using System;
using System.Collections.Generic;
using System.Text;

using GlassOS.Lib.Graphics;

namespace GlassOS.Lib.App
{
    class AppTerminal
    {
        public static int CursorX = 0;
        public static int CursorY = 0;

        // colors
        public ConsoleColor BackgroundColor = ConsoleColor.Black;
        public ConsoleColor ForegroundColor = ConsoleColor.White;

        // clear the screen
        public void Clear()
        {
            CursorX = 0; CursorY = 0;
            Terminal.ClearAppBuffer(BackgroundColor);
        }

        // print string to next position
        public void Write(string text)
        {
            Terminal.SetCursorPos(CursorX, CursorY);
            Terminal.Write(text, ForegroundColor, BackgroundColor);
            UpdateCursorPos();
        }

        public string ReadLine()
        {
            Terminal.SetCursorPos(CursorX, CursorY);
            string input = Terminal.ReadLine();
            UpdateCursorPos();
            return input;
        }

        private void UpdateCursorPos()
        {
            CursorX = Terminal.CursorX;
            CursorY = Terminal.CursorY;
        }
    }
}

