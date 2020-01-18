using UnityEngine;

namespace A11YTK
{

    public static class Utilities
    {

        public static void ResetRectTransform(this RectTransform rectTransform)
        {

            rectTransform.anchorMin = Vector2.zero;
            rectTransform.anchorMax = Vector2.one;

            rectTransform.offsetMin = Vector2.zero;
            rectTransform.offsetMax = Vector2.zero;

        }

    }

}
