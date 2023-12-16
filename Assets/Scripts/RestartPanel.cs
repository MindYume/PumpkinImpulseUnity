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

public class RestartPanel : MonoBehaviour
{
    [SerializeField] private Level _level;
    [SerializeField] private TextMeshProUGUI _wavesMaxText;
    [SerializeField] private TextMeshProUGUI _wavesNowText;
    [SerializeField] private TextMeshProUGUI _menuButtonText;
    [SerializeField] private TextMeshProUGUI _againButtonText;

    void Start()
    {
        setLanguage(GeneralSingleton.Instance.Language);
    }

    private void setLanguage(LanguageEnum language)
    {
        switch(language)
		{
			case LanguageEnum.English:
                _wavesMaxText.font    = Resources.Load<TMP_FontAsset>("Fonts/OpenSans/OpenSansOrange");
                _wavesNowText.font    = Resources.Load<TMP_FontAsset>("Fonts/OpenSans/OpenSansOrange");
                _menuButtonText.font  = Resources.Load<TMP_FontAsset>("Fonts/OpenSans/OpenSansGreen");
                _againButtonText.font = Resources.Load<TMP_FontAsset>("Fonts/OpenSans/OpenSansGreen");

				_wavesMaxText.text    = $"Max wave: <font=\"OpenSansBrightGreen\">{GeneralSingleton.Instance.MaxWave}</font>";
                _wavesNowText.text    = $"Completed waves: <font=\"OpenSansBrightGreen\">{_level.Wave-1}</font>";
                _menuButtonText.text  = "Menu";
                _againButtonText.text = "Again";
				break;
            
            case LanguageEnum.Japanese:
                _wavesMaxText.font    = Resources.Load<TMP_FontAsset>("Fonts/NotoSans/NotoSansOrange");
                _wavesNowText.font    = Resources.Load<TMP_FontAsset>("Fonts/NotoSans/NotoSansOrange");
                _menuButtonText.font  = Resources.Load<TMP_FontAsset>("Fonts/NotoSans/NotoSansGreen");
                _againButtonText.font = Resources.Load<TMP_FontAsset>("Fonts/NotoSans/NotoSansGreen");

				_wavesMaxText.text    = $"最大ステージ: <font=\"OpenSansBrightGreen\">{GeneralSingleton.Instance.MaxWave}</font>";
                _wavesNowText.text    = $"完了したステージ: <font=\"OpenSansBrightGreen\">{_level.Wave-1}</font>";
                _menuButtonText.text  = "メニュー";
                _againButtonText.text = "再びプレー";
				break;
			
			case LanguageEnum.Russian:
				_wavesMaxText.font    = Resources.Load<TMP_FontAsset>("Fonts/OpenSans/OpenSansOrange");
                _wavesNowText.font    = Resources.Load<TMP_FontAsset>("Fonts/OpenSans/OpenSansOrange");
                _menuButtonText.font  = Resources.Load<TMP_FontAsset>("Fonts/OpenSans/OpenSansGreen");
                _againButtonText.font = Resources.Load<TMP_FontAsset>("Fonts/OpenSans/OpenSansGreen");

				_wavesMaxText.text    = $"Максимальная волна: <font=\"OpenSansBrightGreen\">{GeneralSingleton.Instance.MaxWave}</font>";
                _wavesNowText.text    = $"Пройденые волны: <font=\"OpenSansBrightGreen\">{_level.Wave-1}</font>";
                _menuButtonText.text  = "Меню";
                _againButtonText.text = "Играть снова";
				break;
            
            case LanguageEnum.Turkish:
                _wavesMaxText.font    = Resources.Load<TMP_FontAsset>("Fonts/OpenSans/OpenSansOrange");
                _wavesNowText.font    = Resources.Load<TMP_FontAsset>("Fonts/OpenSans/OpenSansOrange");
                _menuButtonText.font  = Resources.Load<TMP_FontAsset>("Fonts/OpenSans/OpenSansGreen");
                _againButtonText.font = Resources.Load<TMP_FontAsset>("Fonts/OpenSans/OpenSansGreen");

				_wavesMaxText.text    = $"Maks. dalga: <font=\"OpenSansBrightGreen\">{GeneralSingleton.Instance.MaxWave}</font>";
                _wavesNowText.text    = $"Tamamlanmış dalgalar: <font=\"OpenSansBrightGreen\">{_level.Wave-1}</font>";
                _menuButtonText.text  = "Menü";
                _againButtonText.text = "Tekrar";
				break;
		}
    }
    
    public void Show()
    {
        gameObject.SetActive(true);
        setLanguage(GeneralSingleton.Instance.Language);
        Time.timeScale = 0;
    }

    public void OnMenuButtonPressed()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    public void OnAgainButtonPressed()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("GameWindow");
    }
}
