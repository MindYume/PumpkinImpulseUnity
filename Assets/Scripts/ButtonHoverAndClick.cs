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
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonHoverAndClick : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private bool _changeStyle = true;
    [SerializeField] private bool _playSound = true;
    [SerializeField] private bool _JapaneseLanguage = false;
    [SerializeField] private Sprite _nornalSprite;
    [SerializeField] private Sprite _pressedSprite;
    [SerializeField] private Sprite _hoverSprite;
    private Button _button;
    private TextMeshProUGUI _textMeshProUGUI;
    private bool _isPointerHover;
    private bool _isPointerDown;
    
    void Start()
    {
        _button = GetComponent<Button>();
        _textMeshProUGUI = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void UpdateStyle()
    {
        if (_changeStyle)
        {
            if (_isPointerHover)
            {
                if (_isPointerDown)
                {
                    _button.image.sprite = _pressedSprite;

                    if (GeneralSingleton.Instance.Language == LanguageEnum.Japanese || _JapaneseLanguage)
                    {
                        _textMeshProUGUI.font = Resources.Load<TMP_FontAsset>("Fonts/NotoSans/NotoSansBrightGreen");
                    }
                    else
                    {
                        _textMeshProUGUI.font = Resources.Load<TMP_FontAsset>("Fonts/OpenSans/OpenSansBrightGreen");
                    }
                }
                else
                {
                    _button.image.sprite = _hoverSprite;
                    if (GeneralSingleton.Instance.Language == LanguageEnum.Japanese || _JapaneseLanguage)
                    {
                        _textMeshProUGUI.font = Resources.Load<TMP_FontAsset>("Fonts/NotoSans/NotoSansBrightGreen");
                    }
                    else
                    {
                        _textMeshProUGUI.font = Resources.Load<TMP_FontAsset>("Fonts/OpenSans/OpenSansBrightGreen");
                    }
                }
            }
            else
            {
                _button.image.sprite = _nornalSprite;
                if (GeneralSingleton.Instance.Language == LanguageEnum.Japanese || _JapaneseLanguage)
                {
                    _textMeshProUGUI.font = Resources.Load<TMP_FontAsset>("Fonts/NotoSans/NotoSansGreen");
                }
                else
                {
                    _textMeshProUGUI.font = Resources.Load<TMP_FontAsset>("Fonts/OpenSans/OpenSansGreen");
                }
            }
        }
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        if (_playSound)
        {
            SoundPlayer.PlaySound(0, SoundPlayer.btn_hover, 1, 1);
        }
        _isPointerHover = true;
        UpdateStyle();
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        _isPointerHover = false;
        UpdateStyle();
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        if (_playSound)
        {
            SoundPlayer.PlaySound(0, SoundPlayer.btn_click, 0.5f, 1);
        }
        _isPointerDown = true;
        UpdateStyle();
    }

    public void OnPointerUp(PointerEventData pointerEventData)
    {
        _isPointerDown = false;
        UpdateStyle();
    }
}
