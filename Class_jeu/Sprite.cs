using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace pacman;
public class Sprite
{//
    //TEST 3
    protected Texture2D texture;
    public Rectangle rectangle;
    public Vector2 position;
    private int size;
    public int Size { get => size; set => size = value; }


    public Sprite(Texture2D texture, Vector2 position, int size)
    {
        this.texture = texture;
        this.Size = size;
        this.position = position;
        
        rectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        
        spriteBatch.Draw(texture, rectangle, Color.Yellow);
    }
}
