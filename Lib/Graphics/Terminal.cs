using System;
using System.Collections.Generic;
using System.Text;
using GlassOS.Lib.Graphics;
using Cosmos.System.Graphics;

namespace GlassOS.Lib.Graphics
{
    public unsafe static class Terminal
    {
        // cursor
        public static int CursorX { get; private set; }
        public static int CursorY { get; private set; }

        // colors
        public static ConsoleColor BackgroundColor = ConsoleColor.Black;
        public static ConsoleColor ForegroundColor = ConsoleColor.White;
        public static byte* BackBuffer;

        public static void Update()
        {
            int l = 60 * 90 * 2 + 1;
            for (int i = 0; i < l+1; i++)
            {
                VGADriverII.Buffer[i] = BackBuffer[i];
            }            
        }

        // clear the screen
        public static void Clear(ConsoleColor bg)
        {
            for (int i = 0; i < (VGADriverII.Width * VGADriverII.Height) * 2; i += 2)
            { 
                BackBuffer[i] = 0x20;
                BackBuffer[i + 1] = (byte)((byte) bg << 4);
            }
            SetCursorPos(0, 0);
        }
        public static void Clear() { Clear(ConsoleColor.Black); }

        public unsafe static void ClearAppBuffer(ConsoleColor bg)
        {
            for (uint i = 0; i < (VGADriverII.Width * (VGADriverII.Height-2)) * 2; i += 2)
            {
                VGADriverII.Buffer[i] = 0x20;
                VGADriverII.Buffer[i + 1] = (byte)((byte) bg << 4); 
            }
            SetCursorPos(0, 0);
        }
    

        // draw character to position on screen
        public static unsafe void PutCharacter(int x, int y, char c, ConsoleColor fg, ConsoleColor bg)
        {
            uint index = (uint)(x + (y * VGADriverII.Width)) * 2;
            BackBuffer[index] = (byte)c;
            BackBuffer[index + 1] = ToAttribute((VGAColor)fg, (VGAColor)bg);
        }

        // print character to next position
        public static void WriteChar(char c, ConsoleColor fg, ConsoleColor bg)
        {
            if (c == '\n') { NewLine(); }
            else
            {
                PutCharacter(CursorX, CursorY, c, fg, bg);
                CursorX++;
                if (CursorX >= VGADriverII.Width) { NewLine(); }
                UpdateCursor();
            }
        }
        public static void WriteChar(char c, ConsoleColor fg) { WriteChar(c, fg, BackgroundColor); }
        public static void WriteChar(char c) { WriteChar(c, ForegroundColor, BackgroundColor); }

        // print string to next position
        public static void Write(string text, ConsoleColor fg, ConsoleColor bg)
        {
            for (int i = 0; i < text.Length; i++)
            {
                WriteChar(text[i], fg, bg);
            }
        }
        public static void Write(string text, ConsoleColor fg) { Write(text, fg, BackgroundColor); }
        public static void Write(string text) { Write(text, ForegroundColor, BackgroundColor); }

        // print line to next positoin
        public static void WriteLine(string text, ConsoleColor fg, ConsoleColor bg) { Write(text + "\n", fg, bg); }
        public static void WriteLine(string text, ConsoleColor fg) { WriteLine(text, fg, BackgroundColor); }
        public static void WriteLine(string text) { WriteLine(text, ForegroundColor, BackgroundColor); }
        public static void WriteLine() { WriteLine(""); }

        // backspace input
        private static void Backspace()
        {
            if (CursorX > 0)
            {
                SetCursorX(CursorX - 1);
                PutCharacter(CursorX, CursorY, ' ', ForegroundColor, BackgroundColor);
            }
            else if (CursorY > 0)
            {
                SetCursorPos(VGADriverII.Width - 1, CursorY - 1);
                PutCharacter(CursorX, CursorY, ' ', ForegroundColor, BackgroundColor);
            }
        }

        //draw empty line with specific bg 

        // read line of input
        public static string ReadLine()
        {
            string input = "";
            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                // character
                if (key.KeyChar >= 32 && key.KeyChar <= 126) { WriteChar(key.KeyChar); input += key.KeyChar; }
                // backspace
                else if (key.Key == ConsoleKey.Backspace)
                {
                    if (input.Length > 0)
                    {
                        input = input.Remove(input.Length - 1, 1);
                        Backspace();
                    }
                }
                // enter
                else if (key.Key == ConsoleKey.Enter) { WriteChar('\n'); break; }
            }
            return input;
        }

        // generate new line
        public static void NewLine()
        {
            CursorY++;
            if (CursorY >= VGADriverII.Height)
            {
                //Scroll();
                SetCursorPos(0, VGADriverII.Height - 1);
            }
            else { SetCursorPos(0, CursorY); }
        }

        // scroll by one line
        /*
        private static unsafe void Scroll()
        {
            VGADriverII.MemoryCopy(VGADriverII.Buffer + (VGADriverII.Width * 2), VGADriverII.Buffer, (VGADriverII.Width * (VGADriverII.Height - 1)) * 2);
            for (int i = 0; i < VGADriverII.Width; i++) { PutCharacter(i, VGADriverII.Height - 1, ' ', TextColor, BackColor); }
        }
        */

        // set cursor position
        public static unsafe void SetCursorPos(int x, int y)
        {
            VGADriverII.SetCursorPos((ushort)x, (ushort)y);
            CursorX = x; CursorY = y;
            BackBuffer[((x + (y * VGADriverII.Width)) * 2) + 1] = ToAttribute((VGAColor)ForegroundColor, (VGAColor)BackgroundColor);
        }

        public static void SetCursorX(int x) { SetCursorPos(x, CursorY); }
        public static void SetCursorY(int y) { SetCursorPos(CursorX, y); }
        public static void UpdateCursor() { SetCursorPos(CursorX, CursorY); }
        public static void DisableCursor() { VGADriverII.DisableCursor(); }

        // convert colors to attribute
        public static byte ToAttribute(VGAColor fg, VGAColor bg) { return (byte)((byte)fg | (byte)bg << 4); }

    }
}
