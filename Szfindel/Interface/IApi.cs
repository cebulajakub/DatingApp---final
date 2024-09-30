using Szfindel.Models;

namespace Szfindel.Interface
{
    public interface IApi
    {
        WeatherApi Get(string city);

    }
}
