using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace pacman;

public class Point : Sprite
{
    public Point(Texture2D texture, Vector2 position, int size) : base(texture, position, size) 
    {
        
        rectangle = new Rectangle((int)position.X, (int)position.Y, size, size);
    }
}
