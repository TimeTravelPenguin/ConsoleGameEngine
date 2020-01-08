using System.Collections.Generic;
using ConsoleGameEngine.Extensions;
using ConsoleGameEngine.Sprites;

namespace ConsoleGameEngine.Container
{
  public abstract class SpriteContainer
  {
    protected readonly IDictionary<string, ISprite> Sprites;

    protected SpriteContainer()
    {
      Sprites = new Dictionary<string, ISprite>();
    }

    public void AddSprite(string key, ISprite sprite)
    {
      Sprites.DictAdd(key, sprite);
    }

    public void RemoveSprite(string key)
    {
      Sprites.DictRemove(key);
    }

    public void RemoveSprite(ISprite sprite)
    {
      Sprites.DictRemove(sprite);
    }
  }
}