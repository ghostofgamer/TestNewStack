using UnityEngine;
using Zenject;

public class AudioTestStatus : MonoBehaviour
{
    private IAudioService _audio;

    [Inject]
    private void Construct(IAudioService audio)
    {
        _audio = audio;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            _audio.GetStatus();
    }
}
