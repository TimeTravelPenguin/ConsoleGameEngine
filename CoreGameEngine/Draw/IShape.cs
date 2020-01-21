﻿#region Title Header

// Name: Phillip Smith
// 
// Solution: CoreGameEngine
// Project: CoreGameEngine
// File Name: IShape.cs
// 
// Current Data:
// 2020-01-20 8:48 PM
// 
// Creation Date:
// -- 

#endregion

using System.Collections.Generic;
using System.Drawing;
using CoreGameEngine.Extensions;
using CoreGameEngine.Structs;

namespace CoreGameEngine.Draw
{
  public interface IShape
  {
    IDictionary<Point, Glyph> Glyphs { get; }
    Point3D Position { get; set; }
    void Rotate(Rotation rotation);
  }
}