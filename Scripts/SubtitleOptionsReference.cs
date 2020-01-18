using TMPro;
using UnityEngine;

namespace A11YTK
{

    [CreateAssetMenu(fileName = "SubtitleOptions", menuName = "A11YTK/SubtitleOptionsReference")]
    public class SubtitleOptionsReference : ScriptableObject
    {

        public bool enabled = true;

        public Subtitle.Position defaultPosition = Subtitle.Position.BOTTOM;

        public float fontSize = 30;

        public Color fontForegroundColor = Color.white;

        public Color fontBackgroundColor = Color.black;

        public bool showBackgroundColor = true;

        public TMP_FontAsset fontAsset;

        public Material fontMaterial;

    }

}
