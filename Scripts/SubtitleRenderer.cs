using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace A11YTK
{

    [RequireComponent(typeof(SubtitleController))]
    public class SubtitleRenderer : MonoBehaviour
    {

        private const string CANVAS_WRAPPER_NAME = "Canvas (clone)";

        private const string TEXT_MESH_NAME = "Text (TMP)";

#pragma warning disable CS0649
        [SerializeField]
        private Camera _mainCamera;
#pragma warning restore CS0649

        private SubtitleController _subtitleController;

        private GameObject _canvasWrapper;

        private Canvas _canvas;

        private GameObject _textMeshWrapper;

        private TextMeshProUGUI _textMesh;

        private void Awake()
        {

            if (_mainCamera == null)
            {

                _mainCamera = Camera.main;

            }

            _subtitleController = gameObject.GetComponent<SubtitleController>();

        }

        public void Show()
        {

            if (!_subtitleController.subtitleOptions.enabled)
            {
                return;
            }

            if (Equals(_canvasWrapper, null))
            {

                _canvasWrapper = new GameObject(CANVAS_WRAPPER_NAME, typeof(Canvas), typeof(CanvasScaler));

                _canvasWrapper.transform.SetParent(_mainCamera.transform);

                _canvas = _canvasWrapper.GetComponent<Canvas>();

                _textMeshWrapper = new GameObject(TEXT_MESH_NAME);

                _textMeshWrapper.transform.SetParent(_canvasWrapper.transform);

                _textMesh = _textMeshWrapper.AddComponent<TextMeshProUGUI>();

                var _textMeshWrapperRectTransform = _textMeshWrapper.transform.GetComponent<RectTransform>();

                _textMeshWrapperRectTransform.ResetRectTransform();

                if (_subtitleController.subtitleOptions.showBackgroundColor)
                {

                    var _panel = new GameObject("Panel", typeof(Image));

                    _panel.transform.SetParent(_textMeshWrapper.transform);

                    _panel.transform.GetComponent<RectTransform>().ResetRectTransform();

                    _textMesh.color = _subtitleController.subtitleOptions.fontForegroundColor;
                    _panel.GetComponent<Image>().color = _subtitleController.subtitleOptions.fontBackgroundColor;

                }

                var position = _subtitleController.position;

                if (position.Equals(Subtitle.Position.AUTO))
                {

                    position = _subtitleController.subtitleOptions.defaultPosition;

                }

                _canvasWrapper.transform.localScale = Vector3.one * 0.025f;

                _textMesh.font = _subtitleController.subtitleOptions.fontAsset;
                _textMesh.fontSize = _subtitleController.subtitleOptions.fontSize;
                _textMesh.fontSharedMaterial = _subtitleController.subtitleOptions.fontMaterial;

            }

            if (Equals(_textMesh, null))
            {
                return;
            }

            _canvas.enabled = true;

        }

        public void Hide()
        {

            if (Equals(_textMesh, null))
            {
                return;
            }

            _canvas.enabled = false;

        }

        public void SetText(string value)
        {

            if (Equals(_textMesh, null))
            {
                return;
            }

            var valueSizeDelta = _textMesh.GetPreferredValues(value);

            _canvasWrapper.GetComponent<RectTransform>().sizeDelta = valueSizeDelta;

            _textMesh.text = value;

        }

    }

}
