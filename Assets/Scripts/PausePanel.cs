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

public class PausePanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _panelText;
    [SerializeField] private TextMeshProUGUI _playButtonText;
    [SerializeField] private TextMeshProUGUI _menuButtonText;

    void Start()
    {
        setLanguage(GeneralSingleton.Instance.Language);
    }

    private void setLanguage(LanguageEnum language)
    {
        switch(language)
		{
			case LanguageEnum.English:
                _panelText.font      = Resources.Load<TMP_FontAsset>("Fonts/OpenSans/OpenSansOrange");
                _playButtonText.font = Resources.Load<TMP_FontAsset>("Fonts/OpenSans/OpenSansGreen");
                _menuButtonText.font = Resources.Load<TMP_FontAsset>("Fonts/OpenSans/OpenSansGreen");

				_panelText.text = "Game paused";
                _playButtonText.text = "Continue";
                _menuButtonText.text = "Main menu";
				break;
            
            case LanguageEnum.Japanese:
                _panelText.font      = Resources.Load<TMP_FontAsset>("Fonts/NotoSans/NotoSansOrange");
                _playButtonText.font = Resources.Load<TMP_FontAsset>("Fonts/NotoSans/NotoSansGreen");
                _menuButtonText.font = Resources.Load<TMP_FontAsset>("Fonts/NotoSans/NotoSansGreen");

                _panelText.text = "休止";
                _playButtonText.text = "ゲームを続ける";
                _menuButtonText.text = "メインメニュー";
				break;
			
			case LanguageEnum.Russian:
				_panelText.font      = Resources.Load<TMP_FontAsset>("Fonts/OpenSans/OpenSansOrange");
                _playButtonText.font = Resources.Load<TMP_FontAsset>("Fonts/OpenSans/OpenSansGreen");
                _menuButtonText.font = Resources.Load<TMP_FontAsset>("Fonts/OpenSans/OpenSansGreen");

				_panelText.text = "Пауза";
                _playButtonText.text = "Продолжить";
                _menuButtonText.text = "Главное меню";
				break;
            
            case LanguageEnum.Turkish:
                _panelText.font      = Resources.Load<TMP_FontAsset>("Fonts/OpenSans/OpenSansOrange");
                _playButtonText.font = Resources.Load<TMP_FontAsset>("Fonts/OpenSans/OpenSansGreen");
                _menuButtonText.font = Resources.Load<TMP_FontAsset>("Fonts/OpenSans/OpenSansGreen");

				_panelText.text = "Duraklat";
                _playButtonText.text = "Devam etmek";
                _menuButtonText.text = "Ana menü";
				break;
		}
    }

    public void SetTimeScale(float scale)
    {
        Time.timeScale = scale;
    }

    public void OnMainMenuButtonClicked()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
}
