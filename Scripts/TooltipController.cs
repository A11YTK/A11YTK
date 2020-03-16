using UnityEngine;

namespace A11YTK
{

    [AddComponentMenu("A11YTK/Tooltip Controller")]
    public class TooltipController : SubtitleController
    {

#pragma warning disable CS0649
        [SerializeField]
        [TextArea(1, 10)]
        protected string _text = "Hello, world.";
#pragma warning restore CS0649

        protected virtual void Start()
        {

            _subtitleRenderer.SetText(_text);

            _subtitleRenderer.Show();

        }

    }

}
