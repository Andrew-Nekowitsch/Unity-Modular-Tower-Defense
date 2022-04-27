public class Neighbors
{
    public IModule north;
    public IModule south;
    public IModule east;
    public IModule west;

    public Neighbors()
    {
        north = null;
        south = null;
        east = null;
        west = null;
    }

    public Neighbors(IModule n, IModule s, IModule e, IModule w)
    {
        north = n;
        south = s;
        east = e;
        west = w;
    }
}
