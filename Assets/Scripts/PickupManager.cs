using UnityEngine;

public class PickupManager : MonoBehaviour
{
    [SerializeField] private CanvasGame _canvasGame;
    [SerializeField] private Transform _itemHandle;
    [SerializeField] private float _distance;
    [SerializeField] private float _dropForce;

    private Item _heldItem;
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        if(Input.touchCount <= 0)
        {
            return;
        }

        Touch touch = Input.GetTouch(0);
        if(touch.phase != TouchPhase.Began)
        {
            return;
        }

        Ray ray = _camera.ScreenPointToRay(touch.position);
        if(Physics.Raycast(ray, out RaycastHit hit, _distance))
        {
            if(hit.transform.TryGetComponent(out Item item) && _heldItem == null)
            {
                _heldItem = item;
                _heldItem.transform.SetParent(_itemHandle);
                _heldItem.transform.localPosition = Vector3.zero;
                _heldItem.transform.localRotation = Quaternion.identity;
                _heldItem.GetComponent<Collider>().enabled = false;
                _heldItem.GetComponent<Rigidbody>().isKinematic = true;
                _canvasGame.DropButton.gameObject.SetActive(true);
            }
        }
    }

    public void DropItem()
    {
        _heldItem.transform.SetParent(null);
        _heldItem.GetComponent<Collider>().enabled = true;

        Rigidbody rigidbody = _heldItem.GetComponent<Rigidbody>();
        rigidbody.isKinematic = false;
        rigidbody.AddForce(_camera.transform.forward * _dropForce);

        _canvasGame.DropButton.gameObject.SetActive(false);
        _heldItem = null;
    }
}
