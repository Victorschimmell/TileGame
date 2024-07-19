namespace TileGame.Factories;

public interface IModelFactory<out T>
{
    T Parse(string @string);
}