using UnityEngine;
using UnityEngine.EventSystems;

public class CanvasDrag : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [SerializeField] private RectTransform rectTransform;

    private Vector2 _input;
    private Vector2 _startPosition;
    private Vector2 _pointerPosition;
    private bool _isDragging;

    public Vector2 Direction => _input;

    public void OnPointerDown(PointerEventData eventData)
    {
        _isDragging = true;
        _startPosition = eventData.position;
        _pointerPosition = eventData.position;
        _input = Vector2.zero;
    }

    private void Update()
    {
        if (!_isDragging) return;

        Vector2 direction = _pointerPosition - _startPosition;
        _input = direction;
        _startPosition = _pointerPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!_isDragging) return;

        _pointerPosition = eventData.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isDragging = false;

        _input = Vector2.zero;
        _startPosition = eventData.position;
    }
}