using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace A11YTK
{

    public class SubtitleRenderer : MonoBehaviour
    {

        private const string CANVAS_WRAPPER_NAME = "Canvas (A11YTK)";

        private const string TEXT_MESH_NAME = "Text";

        private const string PANEL_NAME = "Panel Background";

        private const string RESOURCES_MATERIAL_FOLDER = "Materials/";

        private const string SUBTITLE_BACKGROUND_MATERIAL_NAME = "SubtitleBackground";

#pragma warning disable CS0649
        [SerializeField]
        private Camera _mainCamera;
#pragma warning restore CS0649

        public bool isVisible => _canvasWrapper != null;

        private SubtitleController _subtitleController;

        private GameObject _canvasWrapper;

        private RectTransform _canvasWrapperTransform;

        private Canvas _canvas;

        private GameObject _textMeshWrapper;

        private RectTransform _textMeshWrapperTransform;

        private GameObject _panelWrapper;

        private RectTransform _panelWrapperTransform;

        private Image _panelImage;

        private TextMeshProUGUI _textMesh;

        private Material _subtitleBackgroundMaterial;

        private void Awake()
        {

            if (_mainCamera == null)
            {

                _mainCamera = Camera.main;

            }

            _subtitleController = gameObject.GetComponent<SubtitleController>();

            _subtitleBackgroundMaterial = Resources.LoadAll<Material>(RESOURCES_MATERIAL_FOLDER)
                .First(material => material.name.Equals(SUBTITLE_BACKGROUND_MATERIAL_NAME));

        }

        public void Show()
        {

            if (_subtitleController == null ||
                _subtitleController.subtitleOptions == null ||
                !_subtitleController.subtitleOptions.enabled)
            {
                return;
            }

            if (_canvasWrapper == null)
            {

                CreateCanvasGameObjects();

                CreateTextGameObjects();

                SetupCanvasGameObjects();

                SetupTextGameObjects();

                SetOptions(_subtitleController.subtitleOptions);

            }

            _canvas.enabled = true;

        }

        private void CreateCanvasGameObjects()
        {

            if (_canvasWrapper != null)
            {
                return;
            }

            _canvasWrapper = new GameObject(CANVAS_WRAPPER_NAME, typeof(Canvas), typeof(CanvasScaler));

            _canvasWrapperTransform = _canvasWrapper.GetComponent<RectTransform>();

            _canvas = _canvasWrapper.GetComponent<Canvas>();

        }

        private void CreateTextGameObjects()
        {

            if (_textMeshWrapper != null && _panelWrapper != null)
            {
                return;
            }

            _textMeshWrapper = new GameObject(TEXT_MESH_NAME, typeof(RectTransform), typeof(TextMeshProUGUI));

            _textMeshWrapperTransform = _textMeshWrapper.GetComponent<RectTransform>();

            _textMeshWrapperTransform.SetParent(_canvasWrapperTransform, false);

            _textMesh = _textMeshWrapper.GetComponent<TextMeshProUGUI>();

            _panelWrapper = new GameObject(PANEL_NAME, typeof(RectTransform), typeof(Image));

            _panelWrapperTransform = _panelWrapper.GetComponent<RectTransform>();

            _panelWrapperTransform.SetParent(_textMeshWrapperTransform, false);

            _panelImage = _panelWrapper.GetComponent<Image>();

        }

        private void SetupCanvasGameObjects()
        {

            if (_subtitleController.type.Equals(Subtitle.Type.HEADSET))
            {

                _canvasWrapperTransform.SetParent(_mainCamera.transform, false);

                _canvasWrapperTransform.localPosition = new Vector3(0, 0, 10);

                _canvasWrapperTransform.ScaleBasedOnDistanceFromCamera(_mainCamera);

                _canvasWrapperTransform.ResizeToMatchCamera(_mainCamera);

                _canvas.renderMode = RenderMode.WorldSpace;

            }
            else if (_subtitleController.type.Equals(Subtitle.Type.OBJECT))
            {

                _canvasWrapperTransform.localPosition = gameObject.transform.position;

                _canvasWrapperTransform.ScaleBasedOnDistanceFromCamera(_mainCamera);

                _canvas.renderMode = RenderMode.WorldSpace;

            }
            else if (_subtitleController.type.Equals(Subtitle.Type.AUTO) ||
                     _subtitleController.type.Equals(Subtitle.Type.SCREEN))
            {

                _canvas.renderMode = RenderMode.ScreenSpaceCamera;

            }

            _canvas.worldCamera = _mainCamera;

        }

        private void SetupTextGameObjects()
        {

            if (_subtitleController.type.Equals(Subtitle.Type.OBJECT))
            {

                _textMeshWrapperTransform.ResetRectTransform();

            }

            _textMesh.raycastTarget = false;

            _panelWrapperTransform.ResetRectTransform();

            _panelImage.raycastTarget = false;

            if (_subtitleController.subtitleOptions.backgroundSprite != null)
            {

                _panelImage.sprite = _subtitleController.subtitleOptions.backgroundSprite;
                _panelImage.type = Image.Type.Sliced;

            }

            if (_canvas.renderMode.Equals(RenderMode.WorldSpace))
            {

                _panelImage.material = _subtitleBackgroundMaterial;

            }

        }

        private void SetOptions(SubtitleOptionsReference subtitleOptions)
        {

            if (_textMesh == null)
            {
                return;
            }

            _textMesh.color = subtitleOptions.fontColor;
            _textMesh.font = subtitleOptions.fontAsset;
            _textMesh.fontSize = subtitleOptions.fontSize;
            _textMesh.fontSharedMaterial = subtitleOptions.fontMaterial;
            _textMesh.alignment = subtitleOptions.textAlignment;

            if (_panelWrapper == null || _panelImage == null)
            {
                return;
            }

            _panelImage.enabled = subtitleOptions.showBackground;
            _panelImage.material.color = subtitleOptions.backgroundColor;

        }

        public void Hide()
        {

            Destroy(_canvasWrapper);

        }

        public void SetText(string value)
        {

            if (_textMesh == null || _canvasWrapper == null)
            {
                return;
            }

            var screenPadding = _canvasWrapperTransform.sizeDelta.x *
                                (_subtitleController.subtitleOptions.screenPadding / 100);

            var wrapWidth = _canvasWrapperTransform.sizeDelta.x - screenPadding;

            if (_subtitleController.type.Equals(Subtitle.Type.OBJECT))
            {

                wrapWidth = Screen.width / 2;

            }

            var wrappedText = _textMesh.WrapText(value, wrapWidth);

            var valueSizeDelta = _textMesh.GetPreferredValues(wrappedText);

            var paddingSizeDelta = _textMesh.GetPreferredValues(value);

            valueSizeDelta += Vector2.one * _subtitleController.subtitleOptions.backgroundPadding;

            if (_subtitleController.type.Equals(Subtitle.Type.OBJECT))
            {

                _canvasWrapperTransform.sizeDelta = valueSizeDelta;

                if (_subtitleController.position.Equals(Subtitle.Position.TOP))
                {

                    _canvasWrapperTransform.position += new Vector3(0,
                        (paddingSizeDelta.y + valueSizeDelta.y) * _canvasWrapperTransform.localScale.y, 0);

                }
                else
                {

                    _canvasWrapperTransform.position -= new Vector3(0,
                        (paddingSizeDelta.y + valueSizeDelta.y) * _canvasWrapperTransform.localScale.y, 0);

                }

            }
            else
            {

                _textMeshWrapperTransform.SetInsetAndSizeFromParentEdge(
                    _subtitleController.position.Equals(Subtitle.Position.TOP)
                        ? RectTransform.Edge.Top
                        : RectTransform.Edge.Bottom,
                    paddingSizeDelta.y,
                    valueSizeDelta.y);

                _textMeshWrapperTransform.sizeDelta = valueSizeDelta;

            }

            _textMesh.text = wrappedText;

        }

        private void OnValidate()
        {

            if (_mainCamera == null)
            {

                _mainCamera = Camera.main;

            }

        }

    }

}
