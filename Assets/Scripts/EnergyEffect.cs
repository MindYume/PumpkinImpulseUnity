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

public class EnergyEffect : MonoBehaviour
{
    [SerializeField] private Color _color;
    [SerializeField] public bool _playSound;
    private Transform[] _circleTransforms;
    private SpriteRenderer[] _circleSprites;

    private float _energyValue = 0;

    void Start()
    {
        _circleSprites = GetComponentsInChildren<SpriteRenderer>();
        _circleTransforms = new Transform[3];
        _circleTransforms[0] = transform.Find("Circle1");
        _circleTransforms[1] = transform.Find("Circle2");
        _circleTransforms[2] = transform.Find("Circle3");
    }
    void Update()
    {
        for (int i = 0; i < _circleTransforms.Length; i++)
        {
            _circleSprites[i].color = new Color(_color.r, _color.g, _color.b, (1 - _circleTransforms[i].localScale.x) * _energyValue);
            
            _circleTransforms[i].localScale -= Vector3.one * Time.deltaTime * 4f;

            if (_circleTransforms[i].localScale.x <= 0)
            {
                _circleTransforms[i].localScale += Vector3.one;
                _circleSprites[i].color = new Color(_color.r, _color.g, _color.b, 0);
                
                if (_playSound)
                {
                    //_generalSingleton.PlaySound("wave", (-50 + _circles[i].Scale.x  * _energyValue * 60), 1);
                    SoundPlayer.PlaySound(0, SoundPlayer.wave, _energyValue*2, 1);
                } 
            }
        }
    }

    public float EnergyValue
    {
        set {_energyValue = value;}
    }
}
