# ![A11YTK](logo.png)

> AR/VR context aware, spatialized subtitles for Unity

## Installation

### Unity Package Manager

<https://docs.unity3d.com/Packages/com.unity.package-manager-ui@2.0/manual/index.html>

#### Git

```json
{
  "dependencies": {
    "com.a11ytk.a11ytk": "https://github.com/a11ytk/a11ytk.git#upm",
    ...
  }
}
```

### Include tests

```json
{
  "dependencies": {
    ...
  },
  "testables": ["com.a11ytk.a11ytk"]
}
```

Install [TextMeshPro](https://docs.unity3d.com/Manual/com.unity.textmeshpro.html) and then import `Essential Resources` and `Examples and Extras`.

## Quick Start

Attach a `SubtitleAudioSourceController` or `SubtitleVideoPlayerController` component to any GameObject that has an `AudioSource` or `VideoPlayer` component respectively.

<img src="Screenshots/component.png" width="400">

Using an SRT file, either paste the contents or drag the asset reference into the `Subtitle Text` or `Subtitle Text Asset` property respectively.

Create a `Subtitle Options` asset via the create asset menu via `A11YTK > Subtitle Options` and populate with the values seen in the screenshot below.

<img src="Screenshots/options.png" width="400">

Drag the `Subtitle Options` asset into the `Subtitle Options` property of the `SubtitleAudioSourceController` or `SubtitleVideoPlayerController` component.

## Documentation

### SubtitleOptions

#### Properties

| Property           | Description                                                          | Default   |
| ------------------ | -------------------------------------------------------------------- | --------- |
| Enabled            | Toggle subtitles on or off.                                          | `Off`     |
| Default Position   | Position subtitles should render on screen. Either `TOP` or `BOTTOM` | `BOTTOM`  |
| Default Type       | Type of subtitle. Either `SCREEN`, `HEADSET`, or `OBJECT`            | `HEADSET` |
| Screen Padding     | Percentage of screen to be used as padding around subtitles.         | `10`      |
| Font Size          | Subtitle font size.                                                  | `60`      |
| Font Color         | Subtile font color                                                   | `White`   |
| Font Asset         | TextMeshPro font to render subtitle with.                            | `Null`    |
| Font Material      | TextMeshPro material to use with the TextMeshPro font.               | `Null`    |
| Text Alignment     | TextMeshPro text alignment property.                                 | `MidLine` |
| Show Background    | Toggle subtitle background on or off.                                | `On`      |
| Background Color   | Background color that appears behind subtitle text                   | `Black`   |
| Background Sprite  | Optional 9-splice sprite.                                            | `Null`    |
| Background Padding | Padding around the text inside the background.                       | `30`      |

#### Methods

##### Save

Save current state of the `SubtitleOptions` asset to file path.

```csharp
using A11YTK;
using UnityEngine;

public class SubtitleOptionManager : MonoBehaviour
{

    private const string SUBTITLE_OPTIONS_FILENAME = "subtitle_options.json";

    [SerializeField]
    private SubtitleOptionsReference _subtitleOptions;

    ...

    public void OnDisable()
    {

        _subtitleOptions.Save(SUBTITLE_OPTIONS_FILENAME);

    }

}
```

##### Load

Load saved state from a local file into the `SubtitleOptions` asset.

```csharp
using A11YTK;
using UnityEngine;

public class SubtitleOptionManager : MonoBehaviour
{

    private const string SUBTITLE_OPTIONS_FILENAME = "subtitle_options.json";

    [SerializeField]
    private SubtitleOptionsReference _subtitleOptions;

    public void OnEnable()
    {

        _subtitleOptions.Load(SUBTITLE_OPTIONS_FILENAME);

    }

    ...

}
```

##### Delete

Delete local file.

```csharp
_subtitleOptions.Delete("subtitle_options.json");
```

## Contributors

### Core Team

| <img src="https://avatars2.githubusercontent.com/u/6753?s=400&v=4" width="150"> | <img src="https://avatars2.githubusercontent.com/u/58125435?s=400&v=4" width="150"> | <img src="https://avatars2.githubusercontent.com/u/315474?s=400&v=4" width="150"> |
| ------------------------------------------------------------------------------- | ----------------------------------------------------------------------------------- | --------------------------------------------------------------------------------- |
| [Scott Doxey](https://github.com/neogeek)                                       | [Luigi Cody Nicastro](https://github.com/luiginicastro)                             | [Mo Kakwan](https://github.com/luiwavewashginicastro)                             |

## Contributing

Be sure to review the [Contributing Guidelines](https://github.com/neogeek/A11YTK/blob/master/CONTRIBUTING.md) before logging an issue or making a pull request.

## License

[MIT](https://github.com/neogeek/A11YTK/blob/master/LICENSE)
