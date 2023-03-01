using UnityEngine;

public class MouseDraw : MonoBehaviour
{
    [Header("Draw Settings")]
    public float lineWidth = 0.1f;
    public Color lineColor = Color.black;

    private Camera _camera;
    private LineRenderer _lineRenderer;
    private RaycastHit _hit;

    private bool _isDrawing = false;
    private GameObject _currentDrawing;

    private void Start()
    {
        // mendapatkan referensi kamera utama
        _camera = Camera.main;
    }

    private void Update()
    {
        // membuat ray dari posisi mouse
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        // melakukan raycasting untuk mencari posisi di mana mouse berada di dunia game
        if (Physics.Raycast(ray, out _hit))
        {
            // mulai menggambar ketika tombol mouse kiri ditekan
            if (Input.GetMouseButtonDown(0))
            {
                // membuat objek baru untuk menggambar
                _currentDrawing = new GameObject("Drawing");
                _lineRenderer = _currentDrawing.AddComponent<LineRenderer>();

                // inisialisasi Line Renderer
                _lineRenderer.startWidth = lineWidth;
                _lineRenderer.endWidth = lineWidth;
                _lineRenderer.material.color = lineColor;

                // menambahkan titik awal ke Line Renderer
                _lineRenderer.positionCount = 1;
                _lineRenderer.SetPosition(0, _hit.point);

                _isDrawing = true;
            }

            // menambah titik baru ke Line Renderer setiap kali mouse digerakkan
            if (Input.GetMouseButton(0) && _isDrawing)
            {
                int positionCount = _lineRenderer.positionCount;
                _lineRenderer.positionCount = positionCount + 1;
                _lineRenderer.SetPosition(positionCount, _hit.point);
            }
        }

        // berhenti menggambar ketika tombol mouse kiri dilepas
        if (Input.GetMouseButtonUp(0) && _isDrawing)
        {
            _isDrawing = false;
        }
    }
}