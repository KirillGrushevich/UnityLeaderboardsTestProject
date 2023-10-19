using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IScenesLoader
    {
        public Task LoadSceneAsyncAdditive(int sceneIndex);

        public Task UnloadSceneAsync(int sceneIndex);
    }
}