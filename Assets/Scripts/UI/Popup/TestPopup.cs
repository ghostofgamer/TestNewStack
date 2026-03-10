using UnityEngine;
using Zenject;

public class TestPopup : MonoBehaviour
{
    [Inject] private UIService _uiService;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            _uiService.ShowLevelUp(5);

        /*if (Input.GetKeyDown(KeyCode.Alpha2))
            _uiService.ShowCoinsEarned(100);

        if (Input.GetKeyDown(KeyCode.Alpha3))
            _uiService.ShowChest("Gold Chest");*/
    }
}
