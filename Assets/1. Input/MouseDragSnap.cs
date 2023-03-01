using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class MouseDragSnap : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("Drag Setting")]
    // Variabel publik drag speed
    public float dragSpeed = 1f;

    // Komponen RectTransform dari image yang akan di-drag
    private RectTransform rectTransform;

    // Jarak maksimum untuk melepaskan kembali image yang sudah di snap
    public float maxDeltaPosition = 200f;

    // Tag untuk mencari target untuk snap image
    public string snapTargetTag = "SnapTarget";

    // Apakah image sudah di snap ke target
    private bool isSnapped = false;

    // Target untuk snap image
    private Transform snapTarget;

    // Jarak antara image dan target saat image sudah di snap
    private Vector2 snapDeltaPosition;

    [Header("Event Setting")]
    public UnityEvent SnapEvent;
    public UnityEvent ReleaseEvent;

    private void Start()
    {
        // Memastikan RectTransform ada pada image
        rectTransform = GetComponent<RectTransform>();

        // Mencari target untuk snap image berdasarkan tag
        snapTarget = GameObject.FindGameObjectWithTag(snapTargetTag).transform;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Menandai bahwa image belum di snap ke target
        isSnapped = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Mengubah posisi image sesuai dengan pergerakan mouse
        rectTransform.anchoredPosition += eventData.delta * dragSpeed;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Jika image belum di snap ke target
        if (!isSnapped)
        {
            // Mencari jarak antara image dan target
            Vector2 deltaPosition = rectTransform.anchoredPosition - snapTarget.GetComponent<RectTransform>().anchoredPosition;

            // Jika jaraknya kurang dari atau sama dengan maksimum delta position
            if (deltaPosition.magnitude <= maxDeltaPosition)
            {
                // Melekatkan image ke target
                rectTransform.anchoredPosition = snapTarget.GetComponent<RectTransform>().anchoredPosition;

                // Menandai bahwa image sudah di snap ke target
                isSnapped = true;

                // Menghitung jarak antara image dan target saat image sudah di snap
                snapDeltaPosition = rectTransform.anchoredPosition - snapTarget.GetComponent<RectTransform>().anchoredPosition;

                if (isSnapped)
                {
                    SnapEvent.Invoke();
                }
            } else
            {
                ReleaseEvent.Invoke();
            }
        }
    }

    private void Update()
    {
        // Jika image sudah di snap ke target
        if (isSnapped)
        {
            // Mencari jarak antara image dan target
            Vector2 deltaPosition = rectTransform.anchoredPosition - snapTarget.GetComponent<RectTransform>().anchoredPosition;

            // Jika jaraknya lebih besar dari maksimum delta position
            if (deltaPosition.magnitude > maxDeltaPosition)
            {
                // Melepaskan image dari target
                rectTransform.anchoredPosition = snapTarget.GetComponent<RectTransform>().anchoredPosition + snapDeltaPosition;

                // Menandai bahwa image belum di snap ke target
                isSnapped = false;
            }
        }
    }
}