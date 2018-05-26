namespace Kevsoft.Battleship.Game
{
    public interface IShotValidator
    {
        bool Validate((char x, int y) position, IBattlefield battlefield);
    }
}