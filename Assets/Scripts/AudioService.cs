using UnityEngine;

public class AudioService : IAudioService
{
    public void GetStatus()
    {
        Debug.Log("Статус аудио сейчас: ");
    }
}