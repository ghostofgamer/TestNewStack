using System;
using UnityEngine;
using Zenject;

public class TestHint : MonoBehaviour
{
    private UIService _uiService;

    [Inject]
    private void Cinstruct(UIService uiService)
    {
        _uiService=uiService;
    }
    
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
            _uiService.ShowHint("TestHint");
            
    }
}
