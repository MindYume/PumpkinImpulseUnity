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

public class Bullet : MonoBehaviour
{
    [SerializeField] private HitEffect _hitEffectPrefab;
    [SerializeField] private SpriteRenderer _circle1;
    [SerializeField] private SpriteRenderer _circle2;
    [SerializeField] private LineRenderer _tail;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    private float _power;
    private bool _isPowerSetDelayed;
    private string _target = "enemy";

    public Rigidbody2D GetRigidBody2d => _rigidbody2D;

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
            if (power <= 0 || (_target == "player" && power <= 0.3f))
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

    public void SetColor(Color bulletColor1, Color bulletColor2, Color tailColor)
	{
        _circle1.color = bulletColor1;
        _circle2.color = bulletColor2;
        _tail.endColor = tailColor;
	}

    public void SetTarget(string target)
	{
		_target = target;
		if(target == "enemy")
		{
            gameObject.layer = 8;
		}
		else if (target == "player")
		{
            gameObject.layer = 9;
		}
	}

    void OnCollisionEnter2D(Collision2D other)
    {
        
        if ((_target == "enemy" && other.gameObject.tag == "Enemy") || (_target == "player" && other.gameObject.tag == "Player"))
		{
            other.gameObject.SendMessage("TakeDamage", _power * 100);
            SoundPlayer.PlaySound(2, SoundPlayer.hit, _power, 2);

            // Spawn bullet effect
            HitEffect hitEffectInstance = Instantiate(_hitEffectPrefab, (transform.position + other.transform.position) / 2 - new Vector3(0,0,1), Quaternion.identity);
            hitEffectInstance.transform.localScale = Vector3.one * _power;
            hitEffectInstance.Color = _tail.endColor;

            Destroy(gameObject);
		}
    }
}
