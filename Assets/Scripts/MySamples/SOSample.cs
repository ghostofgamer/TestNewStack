using UnityEngine;

namespace MySamples
{
    [CreateAssetMenu(fileName = "TestConfig", menuName = "TestNewStack/SOSample")]
    public class SOSample : ScriptableObject
    {
        [field: SerializeField, Range(0, 10)] public float Speed { get; private set; }
    }
}