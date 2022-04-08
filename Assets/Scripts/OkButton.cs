using UnityEngine;
using UnityEngine.UI;

public class OkButton : MonoBehaviour
{
    [SerializeField] private GameObject _notification;

    public void DeleteNot() => Destroy(_notification);
}
