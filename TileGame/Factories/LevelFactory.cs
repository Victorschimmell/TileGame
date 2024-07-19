using System.Collections.Generic;
using System.Linq;
using Tilegame.Entity;
using Tilegame.Utility.Enums;
using TileGame.Entity;

namespace TileGame.Factories;

public class LevelFactory : IModelFactory<Level>
{
    private const string WindowsNewline = "\r\n";
    private const string MacbookUnixNewline = "\n";

    public Level Parse(string data)
    {
        string mapStart = data.Split("Map:")[1];
        string mapEnd = mapStart.Split("Map/")[0];

        string newLine = mapEnd.Contains(WindowsNewline) ? WindowsNewline : MacbookUnixNewline;

        string[] map = mapEnd.Split(newLine)
            .Where(row => row.Length > 0)
            .ToArray()
            .Reverse()
            .ToArray();

        char[][] xs = map.Select(row => row.Select(column => column).ToArray()).ToArray();

        string metaStart = data.Split("Meta:")[1];
        string metaEnd = metaStart.Split("Meta/")[0];
        Dictionary<string, string> metadata = metaEnd.Split(newLine)
            .Select(line => line.Trim())
            .Where(line => line.Length > 0)
            .ToDictionary(line => line.Split(":")[0].Trim(), line => line.Split(":")[1].Trim());

        string name = metadata.TryGetValue("Name", out string value) ? value : null;
        TileType tileType = metadata.TryGetValue("TileType", out value) ? System.Enum.Parse<TileType>(value) : TileType.NonSpecified;

        Meta meta = new Meta(name, tileType);

        string legendStart = data.Split("Legend:")[1];
        string legendEnd = legendStart.Split("Legend/")[0];

        Dictionary<char, string> legend = legendEnd.Split(newLine)
            .Select(line => line.Trim())
            .Where(line => line.Length > 0)
            .ToDictionary(line => line[0], line => line.Split(")")[1].Trim());

        return new Level(xs, meta, legend);
    }
}