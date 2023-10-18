using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface ISceneLoader
    {
        public Task LoadSceneAsyncAdditive(int sceneIndex);

        public Task UnloadSceneAsync(int sceneIndex);
    }
}