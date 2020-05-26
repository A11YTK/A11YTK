using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.XR;

namespace A11YTK
{

    public abstract class SubtitleController : MonoBehaviour
    {

#pragma warning disable CS0649
        [SerializeField]
        protected Subtitle.Mode _mode = Subtitle.Mode.AUTO;

        [SerializeField]
        protected Subtitle.Position _position = Subtitle.Position.AUTO;

        [SerializeField]
        protected SubtitleOptionsReference _subtitleOptions;

        [SerializeField]
        protected float maxWrapWidth;
#pragma warning restore CS0649

        public Subtitle.Position position =>
            _position.Equals(Subtitle.Position.AUTO) ? _subtitleOptions.defaultPosition : _position;

        public Subtitle.Mode mode =>
            _mode.Equals(Subtitle.Mode.AUTO) ? _subtitleOptions.defaultMode : _mode;

        public SubtitleOptionsReference subtitleOptions => _subtitleOptions;

        protected SubtitleRenderer _subtitleRenderer;

        protected virtual void Awake()
        {

            if (_subtitleOptions == null)
            {

                Debug.LogError("Subtitle options asset is missing!");

            }

            if (subtitleOptions.defaultMode.Equals(Subtitle.Mode.SCREEN) && XRSettings.enabled)
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
            _subtitleRenderer.maxWrapWidth = maxWrapWidth;
            _subtitleRenderer.targetTransform = gameObject.transform;
            _subtitleRenderer.targetCollider = gameObject.GetComponent<Collider>();

            _subtitleRenderer.Setup();
            _subtitleRenderer.SetOptions(_subtitleOptions);

        }

        protected virtual void OnEnable()
        {

            _subtitleRenderer.Show();

        }

        protected virtual void OnDisable()
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

        private void OnDrawGizmosSelected()
        {

            if (maxWrapWidth <= 0)
            {
                return;
            }

            Gizmos.color = Color.green;

            var position = gameObject.transform.position;
            var scale = gameObject.transform.localScale;

            var width = Mathf.Max(scale.x, maxWrapWidth);
            var height = Mathf.Max(scale.y, 1);

            var collider = gameObject.GetComponent<Collider>();

            Gizmos.DrawWireCube(position + Vector3.down * (collider.bounds.extents.y + height),
                new Vector3(width, height, 0));

        }
#endif

    }

}
