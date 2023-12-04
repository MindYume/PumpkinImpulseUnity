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

using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private LineRenderer _tail;
    private Rigidbody2D _rigidbody2D;
    private float _power;
    private bool _isPowerSetDelayed;

    // Start is called before the first frame update
    void Start()
    {
        _tail = GetComponentInChildren<LineRenderer>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (_isPowerSetDelayed)
        {
            SetPower(_power);
        }
        else
        {
            _tail.SetPosition(1, -_rigidbody2D.velocity / 30);
            SetPower(_power - Time.fixedDeltaTime * 0.2f);
        }
    }

    public void SetPower(float power)
	{
        _power = power;
        if (_rigidbody2D is null)
        {
            _isPowerSetDelayed = true;
        }
        else
        {
            if (power <= 0)
            {
                Destroy(gameObject);
            }
            else
            {
                transform.localScale = new Vector3(power * 5, power * 5, 1);
                _rigidbody2D.mass = power/100;
                _isPowerSetDelayed = false;
            }
        }
	}
}
