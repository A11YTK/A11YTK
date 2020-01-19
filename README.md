# ![A11YTK](logo.png)

> AR/VR context aware, spatialized subtitles for Unity

## Installation

<https://docs.unity3d.com/Packages/com.unity.package-manager-ui@2.0/manual/index.html>

```json
{
  "dependencies": {
    "com.scottdoxey.a11ytk": "https://github.com/neogeek/a11ytk.git#upm",
    ...
  }
}
```

Install [TextMeshPro](https://docs.unity3d.com/Manual/com.unity.textmeshpro.html) and then import `Essential Resources` and `Examples and Extras`.

## Usage

Attach a `SubtitleController` component to any GameObject that has an `Audio Source` component.

<img src="Screenshots/components.png" width="400">

Create a `SubtitleOptions` file via the create asset menu via `A11YTK > SubtitleOptionsReference`.

<img src="Screenshots/options.png" width="400">

Drag the `SubtitleOptions` file into the `SubtitleController` reference property.

Then use the following code to start the audio file and associated subtitles.

```csharp
using A11YTK;
using UnityEngine;

public class PlayAudioFile : MonoBehaviour
{

    [SerializeField]
    private SubtitleController _subtitleController;

    public void Play()
    {

        _subtitleController.PlayOneShot();

    }

}
```
