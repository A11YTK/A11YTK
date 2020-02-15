using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace A11YTK
{

    [AddComponentMenu("A11YTK/SubtitleRenderer")]
    [RequireComponent(typeof(SubtitleController))]
    public class SubtitleRenderer : MonoBehaviour
    {

        private const string CANVAS_WRAPPER_NAME = "Canvas (clone)";

        private const string TEXT_MESH_NAME = "Text (TMP)";

        private const string PANEL_NAME = "Panel";

        private const string RESOURCES_MATERIAL_FOLDER = "Materials/";

        private const string SUBTITLE_BACKGROUND_MATERIAL_NAME = "SubtitleBackground";

        private const float SUBTITLE_SCREEN_SCALE = 0.025f;

        private const float SUBTITLE_SCREEN_PADDING = 100;

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

        private GameObject _panel;

        private Image _panelImage;

        private TextMeshProUGUI _textMesh;

        private Sprite _subtitleBackgroundSprite;

        private Material _subtitleBackgroundMaterial;

        private void Awake()
        {

            if (_mainCamera == null)
            {

                _mainCamera = Camera.main;

            }

            _subtitleController = gameObject.GetComponent<SubtitleController>();

            _subtitleBackgroundSprite = _subtitleController.subtitleOptions.backgroundSprite;

            _subtitleBackgroundMaterial = Resources.LoadAll<Material>(RESOURCES_MATERIAL_FOLDER)
                .First(material => material.name.Equals(SUBTITLE_BACKGROUND_MATERIAL_NAME));

        }

        public void Show()
        {

            if (_subtitleController.subtitleOptions != null && !_subtitleController.subtitleOptions.enabled)
            {
                return;
            }

            if (_canvasWrapper == null)
            {

                SetupCanvasGameObjects();

                SetupTextGameObjects();

            }

            if (_subtitleController.subtitleOptions != null)
            {

                SetOptions(_subtitleController.subtitleOptions);

            }

            _canvas.enabled = true;

        }

        private void SetupCanvasGameObjects()
        {

            _canvasWrapper = new GameObject(CANVAS_WRAPPER_NAME, typeof(Canvas), typeof(CanvasScaler));

            _canvasWrapperTransform = _canvasWrapper.GetComponent<RectTransform>();

            _canvasWrapperTransform.SetParent(_mainCamera.transform, false);

            _canvasWrapperTransform.ResetRectTransform();

            _canvasWrapperTransform.transform.localPosition = new Vector3(0, 0, 10);

            _canvas = _canvasWrapper.GetComponent<Canvas>();

            _canvasWrapperTransform.ResizeRectTransformToMatchCamera(_mainCamera);

            _canvas.worldCamera = _mainCamera;

        }

        private void SetupTextGameObjects()
        {

            _textMeshWrapper = new GameObject(TEXT_MESH_NAME, typeof(RectTransform));

            _textMeshWrapperTransform = _textMeshWrapper.GetComponent<RectTransform>();

            _textMeshWrapperTransform.SetParent(_canvasWrapperTransform, false);

            _textMeshWrapperTransform.ResetRectTransform();

            _textMeshWrapperTransform.localScale = Vector3.one * SUBTITLE_SCREEN_SCALE;

            _textMesh = _textMeshWrapper.AddComponent<TextMeshProUGUI>();

            _textMesh.alignment = TextAlignmentOptions.Midline;

            _textMesh.raycastTarget = false;

            _panel = new GameObject(PANEL_NAME, typeof(RectTransform), typeof(Image));

            _panel.transform.SetParent(_textMeshWrapperTransform, false);

            _panel.GetComponent<RectTransform>().ResetRectTransform();

            _panelImage = _panel.GetComponent<Image>();

            _panelImage.raycastTarget = false;

            if (_subtitleBackgroundSprite != null)
            {

                _panelImage.sprite = _subtitleBackgroundSprite;
                _panelImage.type = Image.Type.Sliced;

            }

            _panelImage.material = _subtitleBackgroundMaterial;

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

            if (Equals(_panel, null) || Equals(_panelImage, null))
            {
                return;
            }

            _panelImage.enabled = subtitleOptions.showBackground;

            _panelImage.material.color = subtitleOptions.backgroundColor;

        }

        public void Hide()
        {

            TearDown();

        }

        public void SetText(string value)
        {

            if (_textMesh == null || _canvasWrapper == null)
            {
                return;
            }

            var wrappedText = _textMesh.WrapText(value, Mathf.Min(
                (_canvasWrapperTransform.sizeDelta.x / SUBTITLE_SCREEN_SCALE) - SUBTITLE_SCREEN_PADDING));

            var valueSizeDelta = _textMesh.GetPreferredValues(wrappedText);

            valueSizeDelta += Vector2.one * _subtitleController.subtitleOptions.backgroundPadding;

            _textMeshWrapperTransform.SetInsetAndSizeFromParentEdge(
                _subtitleController.position.Equals(Subtitle.Position.TOP)
                    ? RectTransform.Edge.Top
                    : RectTransform.Edge.Bottom, 1, valueSizeDelta.y * SUBTITLE_SCREEN_SCALE);

            _textMeshWrapperTransform.sizeDelta = valueSizeDelta;

            _textMesh.text = wrappedText;

        }

        private void TearDown()
        {

            Destroy(_canvasWrapper);

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
