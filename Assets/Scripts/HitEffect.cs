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

public class HitEffect : MonoBehaviour
{
    public Color Color;
    private SpriteRenderer[] _spriteFrames;
    private int _frameNumber;
    private float _time = 0;

    // Start is called before the first frame update
    void Start()
    {
        _spriteFrames = GetComponentsInChildren<SpriteRenderer>();
        _frameNumber = (int)UnityEngine.Random.Range(0, 2.99f);
        _spriteFrames[_frameNumber].enabled = true;

        _spriteFrames[_frameNumber].color = Color;
        _spriteFrames[_frameNumber+3].color = Color;
    }

    // Update is called once per frame
    void Update()
    {
        _time += Time.deltaTime;


        if (_time > 0.075f && _spriteFrames[_frameNumber].enabled)
        {
            _spriteFrames[_frameNumber].enabled = false;
            _spriteFrames[_frameNumber+3].enabled = true;
        }

        if (_time > 0.15f)
        {
            Destroy(gameObject);
        }
        
    }
}
