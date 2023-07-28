using UnityEngine;
using Zenject;

namespace Visual.PlayerVisualComponents
{
    public class MenuPlayerVisual : PlayerVisual
    {
        [Inject]
        private void Construct(PlayerSelector selector)
        {
            selector.OnModelChanged += ChangeModel;
        }
    }
}