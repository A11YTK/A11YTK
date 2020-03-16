using System.Linq;
using UnityEditor;
using UnityEngine;

namespace A11YTK
{

    [AddComponentMenu("A11YTK/Tooltip Controller")]
    public class TooltipController : MonoBehaviour
    {

#pragma warning disable CS0649
        [SerializeField]
        [TextArea(1, 10)]
        protected string _tooltipText = "Hello, world.";

        [SerializeField]
        protected Subtitle.Position _position = Subtitle.Position.AUTO;

        [SerializeField]
        protected Subtitle.Mode _mode = Subtitle.Mode.AUTO;

        [SerializeField]
        protected SubtitleOptionsReference _subtitleOptions;
#pragma warning restore CS0649

        public Subtitle.Position position =>
            _position.Equals(Subtitle.Position.AUTO) ? _subtitleOptions.defaultPosition : _position;

        public Subtitle.Mode mode =>
            _mode.Equals(Subtitle.Mode.AUTO) ? _subtitleOptions.defaultMode : _mode;

        public SubtitleOptionsReference subtitleOptions => _subtitleOptions;

        protected SubtitleRenderer _subtitleRenderer;

        private void Awake()
        {

            if (_subtitleOptions == null)
            {

                Debug.LogError("Subtitle options asset is missing!");

            }

            if (subtitleOptions.defaultMode.Equals(Subtitle.Mode.SCREEN) && UnityEngine.XR.XRSettings.enabled)
            {

                Debug.LogWarning("Subtitles will not render in SCREEN mode while running in VR!");

                subtitleOptions.defaultMode = Subtitle.Mode.HEADSET;

            }

            var subtitlePrefab =
                Resources.LoadAll<GameObject>("Prefabs")
                    .First(material =>
                        material.name.StartsWith("Subtitle") && material.name.Contains(mode.ToString()));

            _subtitleRenderer = Instantiate(subtitlePrefab).GetComponent<SubtitleRenderer>();

            _subtitleRenderer.mode = mode;
            _subtitleRenderer.position = position;
            _subtitleRenderer.targetTransform = gameObject.transform;
            _subtitleRenderer.targetCollider = gameObject.GetComponent<Collider>();

            _subtitleRenderer.Setup();
            _subtitleRenderer.SetOptions(_subtitleOptions);

        }

        private void Start()
        {

            _subtitleRenderer.SetText(_tooltipText);

            _subtitleRenderer.Show();

        }

        private void OnEnable()
        {

            _subtitleRenderer.Show();

        }

        private void OnDisable()
        {

            _subtitleRenderer.Hide();

        }

#if UNITY_EDITOR
        protected virtual void OnValidate()
        {

            if (_subtitleOptions == null)
            {

                _subtitleOptions =
                    AssetDatabase.LoadAssetAtPath<SubtitleOptionsReference>(AssetDatabase.GUIDToAssetPath(AssetDatabase
                        .FindAssets("t:SubtitleOptionsReference", null)
                        .FirstOrDefault()));

            }

        }
#endif

    }

}
