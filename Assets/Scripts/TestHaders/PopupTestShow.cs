using UI.Popup;
using UnityEngine;
using Zenject;

namespace TestHaders
{
    public class PopupTestShow : MonoBehaviour
    {
        private PopupService _popupService;
    
        [Inject]
        private void Construct(PopupService popupService)
        {
            _popupService = popupService;
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.F))
                _popupService.Show(new FloatingPopupData { Value = 100 });
        }
    }
}