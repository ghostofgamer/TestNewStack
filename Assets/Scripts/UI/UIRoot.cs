using UnityEngine;

namespace UI
{
    public class UIRoot : MonoBehaviour
    {
        [field:SerializeField] public Transform WindowRoot{ get; private set; }
        [field:SerializeField] public Transform PopupRoot{ get; private set; }
    }
}
