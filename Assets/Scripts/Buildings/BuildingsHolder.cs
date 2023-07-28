using GameComponents;
using UnityEngine;
using Zenject;

namespace Buildings
{
    public class BuildingsHolder : MonoBehaviour, IGameStartListener, IGameDisposable
    {
        [SerializeField] private Building[] buildings;
        
        
        [Inject]
        private void Construct(Game game)
        {
            game.AddListener(this);
        }

        public void OnGameStarted()
        {
            foreach (var building in buildings)
            {
                building.Reset();
                building.EnableBuilding();
            }
        }

        public void OnGameDispose()
        {
            foreach (var building in buildings)
            {
                building.DisableBuilding();
            }
        }
    }
}