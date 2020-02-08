# ![A11YTK](logo.png)

> AR/VR context aware, spatialized subtitles for Unity

## Installation

### Unity Package Manager

<https://docs.unity3d.com/Packages/com.unity.package-manager-ui@2.0/manual/index.html>

#### Git

```json
{
  "dependencies": {
    "com.scottdoxey.a11ytk": "https://github.com/neogeek/a11ytk.git#upm",
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
  "testables": ["com.scottdoxey.a11ytk"]
}
```

Install [TextMeshPro](https://docs.unity3d.com/Manual/com.unity.textmeshpro.html) and then import `Essential Resources` and `Examples and Extras`.

## Usage

Attach a `SubtitleAudioSourceController` or `SubtitleVideoPlayerController` component to any GameObject that has an `AudioSource` or `VideoPlayer` component respectively.

<img src="Screenshots/components.png" width="400">

Using an SRT file, either paste the contents or drag the asset reference into the `Subtitle Text` or `Subtitle Text Asset` property respectively.

Create a `Subtitle Options` file via the create asset menu via `A11YTK > Subtitle Options`.

<img src="Screenshots/options.png" width="400">

Drag the `SubtitleOptions` asset into the `Subtitle Options` property of the `SubtitleAudioSourceController` or `SubtitleVideoPlayerController` component.

## Contributors

### Core Team

| <img src="https://avatars2.githubusercontent.com/u/6753?s=400&v=4" width="150"> | <img src="https://avatars2.githubusercontent.com/u/58125435?s=400&v=4" width="150"> | <img src="https://avatars2.githubusercontent.com/u/315474?s=400&v=4" width="150"> |
| --------------------------------------------------------------------------- | ------------------------------------------------------------------------------- | ----------------------------------------------------------------------------- |
| [Scott Doxey](https://github.com/neogeek)                                   | [Luigi Cody Nicastro](https://github.com/luiginicastro)                         | [Mo Kakwan](https://github.com/luiwavewashginicastro)                         |

## Contributing

Be sure to review the [Contributing Guidelines](https://github.com/neogeek/A11YTK/blob/master/CONTRIBUTING.md) before logging an issue or making a pull request.

## License

[MIT](https://github.com/neogeek/A11YTK/blob/master/LICENSE)
