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

public class WaveLabel : MonoBehaviour
{
    private TextMeshProUGUI _textMeshProUGUI;
    // Start is called before the first frame update
    void Start()
    {
        _textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        Level.onWaveChange += OnWaveValueChanged;
        setLanguage(GeneralSingleton.Instance.Language, 1);
    }

    private void setLanguage(LanguageEnum language, int wave)
    {
        switch(language)
		{
			case LanguageEnum.English:
                _textMeshProUGUI.font = Resources.Load<TMP_FontAsset>("Fonts/OpenSans/OpenSansOrange");
                _textMeshProUGUI.text = $"Wave: <font=\"OpenSansBrightGreen\">{wave}</font>";
				break;
            
            case LanguageEnum.Japanese:
				_textMeshProUGUI.font = Resources.Load<TMP_FontAsset>("Fonts/NotoSans/NotoSansOrange");
                _textMeshProUGUI.text = $"ステージ: <font=\"OpenSansBrightGreen\">{wave}</font>";
				break;
			
			case LanguageEnum.Russian:
				_textMeshProUGUI.font = Resources.Load<TMP_FontAsset>("Fonts/OpenSans/OpenSansOrange");
                _textMeshProUGUI.text = $"Волна: <font=\"OpenSansBrightGreen\">{wave}</font>";
				break;
            
            case LanguageEnum.Turkish:
                _textMeshProUGUI.font = Resources.Load<TMP_FontAsset>("Fonts/OpenSans/OpenSansOrange");
                _textMeshProUGUI.text = $"Dalga: <font=\"OpenSansBrightGreen\">{wave}</font>";
				break;
		}
    }

    public void OnWaveValueChanged(int wave)
    {
        setLanguage(GeneralSingleton.Instance.Language, wave);
    }
}
