using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [SerializeField] private RectTransform background;
    [SerializeField] private RectTransform handle;
    [SerializeField] private float dragThreshold = 0.6f;

    private Vector2 _input;
    private Vector2 _startPosition;
    private bool _isDragging;

    public Vector2 Direction => _input;

    private void Awake()
    {
        _startPosition = background.anchoredPosition;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _isDragging = true;
        OnDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!_isDragging) return;

        Vector2 position = RectTransformUtility.WorldToScreenPoint(null, background.position);
        Vector2 radius = background.sizeDelta / 2;
        _input = (eventData.position - position) / (radius * dragThreshold);
        
        if (_input.magnitude > 1)
            _input = _input.normalized;

        handle.anchoredPosition = _input * radius * dragThreshold;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isDragging = false;
        _input = Vector2.zero;
        handle.anchoredPosition = Vector2.zero;
    }

    public RectTransform GetRect() => background;
}