public class Neighbors
{
    public ITile north;
    public ITile south;
    public ITile east;
    public ITile west;

    public Neighbors()
    {
        north = null;
        south = null;
        east = null;
        west = null;
    }

    public Neighbors(ITile n, ITile s, ITile e, ITile w)
    {
        north = n;
        south = s;
        east = e;
        west = w;
    }
}
