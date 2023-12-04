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

using UnityEngine.EventSystems;
using UnityEngine;

public class PlayerButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private EnergyEffect _energyEffect;
    private float _timeAfterPress = 0;
    private bool _isPressed = false;

    // Update is called once per frame
    void Update()
    {
        if (_isPressed)
        {
            _timeAfterPress += Time.deltaTime * 2;
            _energyEffect.EnergyValue = Mathf.Clamp(_timeAfterPress, 0, 1);
        }
        else
        {
            _timeAfterPress = 0;
            _energyEffect.EnergyValue = 0;
        }
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        _isPressed = true;
    }

    public void OnPointerUp(PointerEventData pointerEventData)
    {
        _isPressed = false;
    }
}
