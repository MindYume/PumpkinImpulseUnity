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

public class ButtonHoverAndClick : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    private Button _button;
    private TextMeshProUGUI textMeshProUGUI;
    
    void Start()
    {
        _button = GetComponent<Button>();
        textMeshProUGUI = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        _button.image.sprite = Resources.Load<Sprite>("Images/button_hover_style");
        SoundPlayer.PlaySound(SoundPlayer.btn_hover, 1, 1);
        textMeshProUGUI.faceColor = new Color32(0, 255, 0, 255);
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        _button.image.sprite = Resources.Load<Sprite>("Images/button_style");
        textMeshProUGUI.faceColor = new Color32(0, 200, 0, 255);
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        _button.image.sprite = Resources.Load<Sprite>("Images/button_pressed_style");
        SoundPlayer.PlaySound(SoundPlayer.btn_click, 0.5f, 1);
    }
}
