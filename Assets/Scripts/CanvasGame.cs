using UnityEngine;

public class CanvasGame : MonoBehaviour
{
    [SerializeField] private PickupManager _pickupManager;
    [SerializeField] private GameObject _dropButton;

    public GameObject DropButton => _dropButton;

    public void OnDropButtonPressed() => _pickupManager.DropItem();
}
