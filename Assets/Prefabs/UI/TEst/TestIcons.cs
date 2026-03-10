using UnityEngine;
using Zenject;

namespace Prefabs.UI.TEst
{
    public class TestIcons : BaseScreen
    {
        [Inject]
        public void Construct(ServiceUI serviceUI)
        {
            base.Construct(serviceUI);
            Debug.Log("Construct TestIcons");
        }
    }
}