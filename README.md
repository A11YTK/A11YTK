# A11YTK

> AR/VR context aware, spatialized subtitles for Unity

## Installation

<https://docs.unity3d.com/Packages/com.unity.package-manager-ui@2.0/manual/index.html>

```json
{
  "dependencies": {
    "com.scottdoxey.a11ytk": "https://github.com/neogeek/a11ytk.git#upm"
  }
}
```

## Usage

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
