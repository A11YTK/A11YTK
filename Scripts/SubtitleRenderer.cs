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

#pragma warning disable CS0649
        [SerializeField]
        private Camera _mainCamera;
#pragma warning restore CS0649

        private SubtitleController _subtitleController;

        private GameObject _canvasWrapper;

        private Canvas _canvas;

        private GameObject _textMeshWrapper;

        private GameObject _panel;

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

            if (_subtitleController.subtitleOptions != null && !_subtitleController.subtitleOptions.enabled)
            {
                return;
            }

            if (_canvasWrapper == null)
            {
                SetupGameObjects();
            }

            if (_subtitleController.subtitleOptions != null)
            {

                SetOptions(_subtitleController.subtitleOptions);

            }

            _canvas.enabled = true;

        }

        private void SetupGameObjects()
        {

            _canvasWrapper = new GameObject(CANVAS_WRAPPER_NAME, typeof(Canvas), typeof(CanvasScaler));

            _canvas = _canvasWrapper.GetComponent<Canvas>();

            _textMeshWrapper = new GameObject(TEXT_MESH_NAME);

            _textMeshWrapper.transform.SetParent(_canvasWrapper.transform);

            _textMesh = _textMeshWrapper.AddComponent<TextMeshProUGUI>();

            _textMesh.raycastTarget = false;

            _textMeshWrapper.transform.GetComponent<RectTransform>().ResetRectTransform();

            _panel = new GameObject(PANEL_NAME, typeof(Image));

            _panel.transform.SetParent(_textMeshWrapper.transform);

            _panel.transform.GetComponent<RectTransform>().ResetRectTransform();

            _panelImage = _panel.GetComponent<Image>();

            _panelImage.raycastTarget = false;

            _panelImage.material = _subtitleBackgroundMaterial;

            _canvasWrapper.GetComponent<RectTransform>().transform.localPosition = new Vector3(0, 0, 10);

            _canvasWrapper.transform.SetParent(_mainCamera.transform, false);

            _canvas.ScaleCanvasToMatchCamera(_mainCamera);

        }

        private void SetOptions(SubtitleOptionsReference subtitleOptions)
        {

            if (_textMesh == null)
            {
                return;
            }

            _textMesh.color = subtitleOptions.fontForegroundColor;
            _textMesh.font = subtitleOptions.fontAsset;
            _textMesh.fontSize = subtitleOptions.fontSize;
            _textMesh.fontSharedMaterial = subtitleOptions.fontMaterial;

            if (Equals(_panel, null) || Equals(_panelImage, null))
            {
                return;
            }

            _panelImage.enabled = subtitleOptions.showBackgroundColor;

            _panelImage.material.color = subtitleOptions.fontBackgroundColor;

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

            var valueSizeDelta = _textMesh.GetPreferredValues(value);

            _canvasWrapper.GetComponent<RectTransform>().sizeDelta = valueSizeDelta;

            _textMesh.text = value;

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
