﻿using System;

namespace CoreGameEngine.Shape
{
  public class Glyph
  {
    public ConsoleColor Color { get; set; }
    public char Character { get; set; }

    public Glyph(char character, ConsoleColor color = ConsoleColor.White)
    {
      Character = character;
      Color = color;
    }
  }
}