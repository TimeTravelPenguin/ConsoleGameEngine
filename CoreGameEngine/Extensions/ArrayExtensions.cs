using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CoreGameEngine.Resources;

namespace CoreGameEngine.Extensions
{
  public enum Rotation
  {
    Clockwise,
    CounterClockwise
  }

  public static class ArrayExtensions
  {
    public static T[,] Transpose<T>(this T[,] matrix)
    {
      if (matrix is null)
      {
        throw new ArgumentNullException(nameof(matrix), Exceptions.Argument_IsNull);
      }

      var w = matrix.GetLength(0);
      var h = matrix.GetLength(1);

      var result = new T[h, w];

      for (var i = 0; i < w; i++)
      {
        for (var j = 0; j < h; j++)
        {
          result[j, i] = matrix[i, j];
        }
      }

      return result;
    }

    public static T[][] ToJaggadArray<T>(this IList<IList<T>> glyphs)
    {
      if (glyphs is null)
      {
        throw new ArgumentNullException(nameof(glyphs), Exceptions.Argument_IsNull);
      }

      var rows = new T[glyphs.Count][];
      for (var i = 0; i < glyphs.Count; i++)
      {
        rows[i] = Enumerable.Range(0, glyphs.Count)
          .Select(x => glyphs[x])
          .Cast<T>()
          .ToArray();
      }

      return rows;
    }

    public static T[][] RotateMatrix<T>(this IList<IList<T>> glyphs, Rotation rotation)
    {
      return glyphs.ToJaggadArray().RotateMatrix(rotation);
    }

    public static T[,] RotateMatrix<T>(this T[,] matrix, Rotation rotation)
    {
      if (matrix is null)
      {
        throw new ArgumentNullException(nameof(matrix), Exceptions.Argument_IsNull);
      }

      var transpose = matrix.Transpose();

      var tRow = transpose.GetLength(0);
      var tCol = transpose.GetLength(1);

      var newArr = new T[tRow, tCol];

      switch (rotation)
      {
        case Rotation.Clockwise:
        {
          for (var y = 0; y < tRow; y++)
          {
            for (var x = 0; x < tCol; x++)
            {
              newArr[y, x] = transpose[y, tCol - x - 1];
            }
          }

          break;
        }
        case Rotation.CounterClockwise:
        {
          for (var y = 0; y < tRow; y++)
          {
            for (var x = 0; x < tCol; x++)
            {
              newArr[y, x] = transpose[tRow - y - 1, x];
            }
          }

          break;
        }
        default:
          throw new ArgumentOutOfRangeException(nameof(rotation), rotation, null);
      }

      return newArr;
    }

    public static IDictionary<Point, T> RotateMatrix<T>(this IDictionary<Point, T> matrix, Rotation rotation)
    {
      if (matrix is null)
      {
        throw new ArgumentNullException(nameof(matrix), Exceptions.Argument_IsNull);
      }

      var minX = matrix.Keys.Select(x => x.X).Min();
      var maxX = matrix.Keys.Select(x => x.X).Max();
      var minY = matrix.Keys.Select(y => y.Y).Min();
      var maxY = matrix.Keys.Select(y => y.Y).Max();

      var matrixArr = new int[maxY - minY + 1, maxX - minX + 1];

      return new Dictionary<Point, T>();
    }

    public static T[][] RotateMatrix<T>(this T[][] jaggadArray, Rotation rotation)
    {
      var arr2D = jaggadArray.To2DArray();
      var rotated2D = arr2D.RotateMatrix(rotation);
      return rotated2D.ToJaggedArray();
    }

    public static T[,] To2DArray<T>(this T[][] jaggadArray)
    {
      if (jaggadArray is null)
      {
        throw new ArgumentNullException(nameof(jaggadArray), Exceptions.Argument_IsNull);
      }

      if (!jaggadArray.IsRectangular())
      {
        throw new InvalidOperationException(Exceptions.Array_JaggadNotRectangular);
      }

      var columns = jaggadArray.Length;

      // throws InvalidOperationException if jaggadArray is not rectangular
      var row = jaggadArray.GroupBy(r => r.Length)
        .Single()
        .Key;

      var result = new T[columns, row];
      for (var i = 0; i < columns; ++i)
      {
        for (var j = 0; j < row; ++j)
        {
          result[i, j] = jaggadArray[i][j];
        }
      }

      return result;
    }

    public static T[][] ToJaggedArray<T>(this T[,] array)
    {
      var rowsFirstIndex = array.GetLowerBound(0);
      var rowsLastIndex = array.GetUpperBound(0);
      var numberOfRows = rowsLastIndex - rowsFirstIndex + 1;

      var columnsFirstIndex = array.GetLowerBound(1);
      var columnsLastIndex = array.GetUpperBound(1);
      var numberOfColumns = columnsLastIndex - columnsFirstIndex + 1;

      var jaggedArray = new T[numberOfRows][];
      for (var i = 0; i < numberOfRows; i++)
      {
        jaggedArray[i] = new T[numberOfColumns];

        for (var j = 0; j < numberOfColumns; j++)
        {
          jaggedArray[i][j] = array[i + rowsFirstIndex, j + columnsFirstIndex];
        }
      }

      return jaggedArray;
    }

    public static bool IsRectangular<T>(this T[][] matrix)
    {
      return matrix.All(x => x.Length == matrix.GetLength(0));
    }
  }
}