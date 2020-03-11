using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace A11YTK
{

    public class SubtitleRenderer : MonoBehaviour
    {

        private const float MOVEMENT_SPEED = 5f;

        private const float ROTATION_SPEED = 5f;

        public Subtitle.Mode mode;

        public Subtitle.Position position;

        public Transform targetTransform;

        public Collider targetCollider;

        public bool billboardTowardsCamera;

        public float screenPadding = 10;

        public float objectPadding = 0.25f;

        public float backgroundPadding = 30;

        public bool isVisible => _canvas.enabled;

        [SerializeField]
        [TextArea(1, 10)]
        private string _text = "";

        [SerializeField]
        private Camera _camera;

        private Transform _cameraTransform;

        private Canvas _canvas;

        private RectTransform _canvasWrapperTransform;

        private TextMeshProUGUI _textMesh;

        private RectTransform _textMeshWrapperTransform;

        private Image _panel;

        private void Awake()
        {

            if (_camera == null)
            {

                _camera = Camera.main;

            }

            _cameraTransform = _camera.transform;

            _canvas = gameObject.GetComponentInChildren<Canvas>();

            _canvasWrapperTransform = _canvas.GetComponent<RectTransform>();

            _textMesh = gameObject.GetComponentInChildren<TextMeshProUGUI>();

            _textMeshWrapperTransform = _textMesh.GetComponent<RectTransform>();

            _panel = gameObject.GetComponentInChildren<Image>();

            if (targetTransform && targetCollider == null)
            {

                targetCollider = targetTransform.gameObject.GetComponent<Collider>();

            }

        }

        private void Start()
        {

            if (mode.Equals(Subtitle.Mode.HEADSET))
            {

                gameObject.transform.SetParent(_cameraTransform, false);

                _canvasWrapperTransform.localPosition = new Vector3(0, 0, 10);

                _canvasWrapperTransform.localScale =
                    _canvasWrapperTransform.ScaleBasedOnDistanceFromCamera(_camera);

                _canvasWrapperTransform.sizeDelta = _canvasWrapperTransform.ResizeToMatchCamera(_camera) / 2;

            }
            else if (mode.Equals(Subtitle.Mode.OBJECT))
            {

                _canvasWrapperTransform.position = targetTransform.position;

                _canvasWrapperTransform.localScale =
                    _canvasWrapperTransform.ScaleBasedOnDistanceFromCamera(_camera);

                _canvasWrapperTransform.sizeDelta =
                    targetCollider.bounds.size / _canvasWrapperTransform.localScale.x;

                if (billboardTowardsCamera)
                {

                    BillboardTowardsCamera();

                }

            }

            _canvas.worldCamera = _camera;

            if (_text != "")
            {

                Show();

                SetText(_text);

            }

        }

        private void Update()
        {

            if (mode.Equals(Subtitle.Mode.OBJECT))
            {
                _canvasWrapperTransform.position = Vector3.Lerp(
                    _canvasWrapperTransform.position,
                    targetTransform.position,
                    MOVEMENT_SPEED * Time.deltaTime);

                if (billboardTowardsCamera)
                {

                    BillboardTowardsCamera();

                }

            }

        }

        private void BillboardTowardsCamera()
        {

            _canvasWrapperTransform.rotation = Quaternion.Lerp(_canvasWrapperTransform.rotation,
                Quaternion.LookRotation(
                    _canvasWrapperTransform.position - _cameraTransform.position,
                    Vector3.up
                ), ROTATION_SPEED * Time.deltaTime);

        }

        public void SetOptions(SubtitleOptionsReference options)
        {

            billboardTowardsCamera = options.billboardTowardsCamera;
            screenPadding = options.screenPadding;
            objectPadding = options.objectPadding;
            backgroundPadding = options.backgroundPadding;

            _textMesh.color = options.fontColor;
            _textMesh.font = options.fontAsset;
            _textMesh.fontSize = options.fontSize;
            _textMesh.fontSharedMaterial = options.fontMaterial;
            _textMesh.alignment = options.textAlignment;

            if (_panel == null)
            {
                return;
            }

            _panel.enabled = options.showBackground;
            _panel.material.color = options.backgroundColor;

        }

        public void SetText(string value)
        {

            var padding = _canvasWrapperTransform.sizeDelta.x * (screenPadding / 100);

            var wrapWidth = _canvasWrapperTransform.ResizeToMatchCamera(_camera).x - padding;

            var wrappedText = _textMesh.WrapText(value, wrapWidth);

            var valueSizeDelta = _textMesh.GetPreferredValues(wrappedText);

            var paddingSizeDelta = _textMesh.GetPreferredValues(value);

            valueSizeDelta += Vector2.one * backgroundPadding;

            if (mode.Equals(Subtitle.Mode.OBJECT))
            {

                _canvasWrapperTransform.sizeDelta = Vector3.one;
                _textMeshWrapperTransform.sizeDelta = valueSizeDelta;

                if (position.Equals(Subtitle.Position.TOP))
                {

                    _textMeshWrapperTransform.pivot = new Vector2(0.5f, 0);

                    _textMeshWrapperTransform.position = targetTransform.position + new Vector3(
                        0,
                        objectPadding +
                        targetCollider.bounds.extents.y,
                        0);

                }
                else
                {

                    _textMeshWrapperTransform.pivot = new Vector2(0.5f, 1);

                    _textMeshWrapperTransform.position = targetTransform.position - new Vector3(
                        0,
                        objectPadding +
                        targetCollider.bounds.extents.y,
                        0);

                }

            }
            else
            {

                _textMeshWrapperTransform.SetInsetAndSizeFromParentEdge(
                    position.Equals(Subtitle.Position.TOP)
                        ? RectTransform.Edge.Top
                        : RectTransform.Edge.Bottom,
                    paddingSizeDelta.y,
                    valueSizeDelta.y);

                _textMeshWrapperTransform.sizeDelta = valueSizeDelta;

            }

            _textMesh.text = wrappedText;

        }

        public void Show()
        {

            _canvas.enabled = true;

        }

        public void Hide()
        {

            _canvas.enabled = false;

        }

        public void Remove()
        {

            Destroy(gameObject);

        }

        private void OnValidate()
        {

            if (_camera == null)
            {

                _camera = Camera.main;

            }

        }

    }

}
