using UnityEngine;
using UnityEngine.UI;

public class ItemPickup : MonoBehaviour
{
    [Header("Item Settings")]
    public float pickupRange = 3f;
    public Transform itemHoldPosition;

    [Header("Throw Settings")]
    public float throwForce = 10f;

    [Header("UI")]
    public Button throwButton;

    private GameObject _heldItem;
    private Rigidbody _heldItemRigidbody;
    private Collider _heldItemCollider;

    private Camera _mainCamera;

    private void Start()
    {
        _mainCamera = Camera.main;
        throwButton.gameObject.SetActive(false);

        throwButton.onClick.AddListener(ThrowItem);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_heldItem == null)
            {
                TryPickUpItem();
            }
        }
    }

    private void TryPickUpItem()
    {
        RaycastHit hit;
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, pickupRange))
        {
            GameObject hitObject = hit.collider.gameObject;

            PickableItem pickable = hitObject.GetComponent<PickableItem>();
            if (pickable != null)
            {
                PickUpItem(hitObject);
            }
        }
    }

    private void PickUpItem(GameObject item)
    {
        _heldItem = item;
        _heldItemRigidbody = item.GetComponent<Rigidbody>();
        _heldItemCollider = item.GetComponent<Collider>();

        _heldItemRigidbody.isKinematic = true;
        _heldItemCollider.enabled = false;


        _heldItem.transform.SetParent(itemHoldPosition);
        _heldItem.transform.localPosition = Vector3.zero;
        _heldItem.transform.localRotation = Quaternion.identity;

        throwButton.gameObject.SetActive(true);
    }

    private void ThrowItem()
    {
        if (_heldItem == null) return;

        _heldItem.transform.SetParent(null);

        _heldItemRigidbody.isKinematic = false;
        _heldItemCollider.enabled = true;

        _heldItemRigidbody.AddForce(_mainCamera.transform.forward * throwForce, ForceMode.Impulse);

        _heldItem = null;
        _heldItemRigidbody = null;
        _heldItemCollider = null;


        throwButton.gameObject.SetActive(false);
    }
}