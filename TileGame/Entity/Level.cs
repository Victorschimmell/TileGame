using System.Collections.Generic;
using Tilegame.Entity;

namespace TileGame.Entity;

public class Level
{

    public char[][] Map { get; private set; }

    public Meta Meta { get; private set; }

    public Dictionary<char, string> Legends { get; private set; }

    public Level(char[][] map, Meta meta, Dictionary<char, string> legends)
    {
        Map = map;
        Meta = meta;
        Legends = legends;
    }
}