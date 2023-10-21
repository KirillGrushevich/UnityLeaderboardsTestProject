using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IScreenLoader
    {
        public Task LoadScreen(GameScreen gameScreen);
    }
}