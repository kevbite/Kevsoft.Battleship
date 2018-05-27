namespace Kevsoft.Battleship.ConsoleApp
{
    public interface IPositionReader
    {
        (char x, int y) ReadPosition();
    }
}