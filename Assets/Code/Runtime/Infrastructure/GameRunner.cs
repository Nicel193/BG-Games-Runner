using Code.Runtime.Infrastructure.Bootstrappers;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Code.Runtime.Infrastructure
{
    public class GameRunner : MonoBehaviour
    {
        public static string StartScene { get; private set; }

        GameBootstrapper.Factory gameBootstrapperFactory;

        [Inject]
        void Construct(GameBootstrapper.Factory bootstrapperFactory) =>
            this.gameBootstrapperFactory = bootstrapperFactory;

        private void Awake()
        {
            if (string.IsNullOrEmpty(StartScene))
                StartScene = SceneManager.GetActiveScene().name;

            StartBootstrapper();
        }

        private void StartBootstrapper()
        {
            var bootstrapper = FindObjectOfType<GameBootstrapper>();

            if (bootstrapper != null) return;

            gameBootstrapperFactory.Create();
        }
    }
}