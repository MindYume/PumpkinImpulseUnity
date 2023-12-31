/*
MIT License

Copyright (c) 2023 Viktor Grachev

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textMeshMaxWave;
    [SerializeField] private TextMeshProUGUI _textMeshPlay;
    [SerializeField] private TextMeshProUGUI _textMeshLanguage;
    
    void Start()
    {
        GeneralSingleton.Instance.onLanguageChanged += OnLanguageCahnged;
        OnLanguageCahnged(GeneralSingleton.Instance.Language);

        //Debug.Log($"Main Menu: {Resources.Load<TMP_FontAsset>("Fonts/OpenSans/OpenSansOrange")}");
    }

    private void OnLanguageCahnged(LanguageEnum language)
    {
        switch(language)
		{
			case LanguageEnum.English:
                _textMeshMaxWave.font  = Resources.Load<TMP_FontAsset>("Fonts/OpenSans/OpenSansOrange");
                _textMeshPlay.font     = Resources.Load<TMP_FontAsset>("Fonts/OpenSans/OpenSansGreen");
                _textMeshLanguage.font = Resources.Load<TMP_FontAsset>("Fonts/OpenSans/OpenSansGreen");

                _textMeshMaxWave.text = $"Max Wave: <font=\"OpenSansBrightGreen\">{GeneralSingleton.Instance.MaxWave}</font>";
				_textMeshPlay.text = "Play";
				_textMeshLanguage.text = "Language";
				break;

            case LanguageEnum.Japanese:
                _textMeshMaxWave.font  = Resources.Load<TMP_FontAsset>("Fonts/NotoSans/NotoSansOrange");
                _textMeshPlay.font     = Resources.Load<TMP_FontAsset>("Fonts/NotoSans/NotoSansGreen");
                _textMeshLanguage.font = Resources.Load<TMP_FontAsset>("Fonts/NotoSans/NotoSansGreen");

                _textMeshMaxWave.text = $"最大ステージ: <font=\"OpenSansBrightGreen\">{GeneralSingleton.Instance.MaxWave}</font>";
				_textMeshPlay.text = "プレー";
				_textMeshLanguage.text = "言語";
				break;
			
			case LanguageEnum.Russian:
                _textMeshMaxWave.font  = Resources.Load<TMP_FontAsset>("Fonts/OpenSans/OpenSansOrange");
                _textMeshPlay.font     = Resources.Load<TMP_FontAsset>("Fonts/OpenSans/OpenSansGreen");
                _textMeshLanguage.font = Resources.Load<TMP_FontAsset>("Fonts/OpenSans/OpenSansGreen");

                _textMeshMaxWave.text = $"Макс. волна: <font=\"OpenSansBrightGreen\">{GeneralSingleton.Instance.MaxWave}</font>";
                _textMeshPlay.text = "Играть";
                _textMeshLanguage.text = "Язык";
				break;

            case LanguageEnum.Turkish:
                _textMeshMaxWave.font  = Resources.Load<TMP_FontAsset>("Fonts/OpenSans/OpenSansOrange");
                _textMeshPlay.font     = Resources.Load<TMP_FontAsset>("Fonts/OpenSans/OpenSansGreen");
                _textMeshLanguage.font = Resources.Load<TMP_FontAsset>("Fonts/OpenSans/OpenSansGreen");

                _textMeshMaxWave.text = $"Maks. dalga: <font=\"OpenSansBrightGreen\">{GeneralSingleton.Instance.MaxWave}</font>";
				_textMeshPlay.text = "Oynamak";
				_textMeshLanguage.text = "Dil";
				break;
		}
    }

    public void OnPlayButtonClicked()
    {
        SceneManager.LoadScene("GameWindow");
    }
}
