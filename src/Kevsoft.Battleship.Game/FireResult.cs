namespace Kevsoft.Battleship.Game
{
    public class FireResult
    {
        public FireResult(bool shotFired)
        {
            ShotFired = shotFired;
        }

        public bool ShotFired { get; }
    }
}