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

using UnityEngine;
using UnityEngine.UI;

public class LanguagePanel : MonoBehaviour
{
    [SerializeField] private Toggle _toggleEnglish;
    [SerializeField] private Toggle _toggleJapanese;
    [SerializeField] private Toggle _toggleRussian;
    [SerializeField] private Toggle _toggleTurkish;
    // Start is called before the first frame update
    void Start()
    {
        switch(GeneralSingleton.Instance.Language)
		{
			case LanguageEnum.English:
				onEnglishClick();
				break;

            case LanguageEnum.Japanese:
				onJapaneseClick();
				break;
			
			case LanguageEnum.Russian:
				onRussianClick();
				break;

            case LanguageEnum.Turkish:
				onTurkishClick();
				break;
		}
    }

    public void onEnglishClick()
    {
        GeneralSingleton.Instance.Language = LanguageEnum.English;
        _toggleEnglish.isOn  = true;
        _toggleJapanese.isOn = false;
        _toggleRussian.isOn  = false;
        _toggleTurkish.isOn  = false;
    }

    public void onJapaneseClick()
    {
        GeneralSingleton.Instance.Language = LanguageEnum.Japanese;
        _toggleEnglish.isOn  = false;
        _toggleJapanese.isOn = true;
        _toggleRussian.isOn  = false;
        _toggleTurkish.isOn  = false;
    }

    public void onRussianClick()
    {
        GeneralSingleton.Instance.Language = LanguageEnum.Russian;
        _toggleEnglish.isOn  = false;
        _toggleJapanese.isOn = false;
        _toggleRussian.isOn  = true;
        _toggleTurkish.isOn  = false;
    }

    public void onTurkishClick()
    {
        GeneralSingleton.Instance.Language = LanguageEnum.Turkish;
        _toggleEnglish.isOn  = false;
        _toggleJapanese.isOn = false;
        _toggleRussian.isOn  = false;
        _toggleTurkish.isOn  = true;
    }
}
