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

using System;
using UnityEngine;

public class EnemyBat : Enemy
{
    [SerializeField] private HitEffect _hitEffectPrefab;
    Rigidbody2D _rigidbody2D;
    SpriteRenderer _spriteRenderer;
    private Player _player;
    private float _health = 100;
    private float _maxSpeed = 1.25f;
    private float _acceleration = 1;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_player == null)
        {
            _player = GeneralSingleton.Instance.PlayerInstance;
        }
        else
        {
            Vector2 relativePlayerPosition = _player.transform.localPosition - transform.position;
            float LinearVelocityAtan2 = Mathf.Atan2(_rigidbody2D.velocity.y, _rigidbody2D.velocity.x);
            float relativePlayerPositionAtan2 = Mathf.Atan2(relativePlayerPosition.y, relativePlayerPosition.x);
            float directionDifference = LinearVelocityAtan2 - relativePlayerPositionAtan2;
            if (Mathf.Cos(directionDifference) * _rigidbody2D.velocity.magnitude < _maxSpeed)
            {
                Vector3 accelerationDirection = (_player.transform.position-transform.position).normalized*_acceleration * Time.deltaTime;
                _rigidbody2D.velocity += new Vector2(accelerationDirection.x, accelerationDirection.y);
            }

            if (_player.transform.position.x > transform.position.x)
            {
                _spriteRenderer.flipX = true;
            }
            else
            {
                _spriteRenderer.flipX = false;
            }
        }

        if (transform.localPosition.x < -3 || transform.localPosition.x > 3 || transform.localPosition.y < -3 || transform.localPosition.y > 3)
        {
            Debug.Log("Bat is out of border");
            Destroy(gameObject);
        }
    }

    public void TakeDamage(float damage_value)
    {
        _health -= damage_value;
        if (_health <= 0)
        {
            HealthPoint.Spawn(Level.gameObjectStatic, transform.localPosition, 0.1f);
            Destroy(gameObject);
        }
    }

    private void handleCollison(Collision2D other)
    {
        if (other.gameObject.tag == "Player" && other.gameObject.GetComponent<Player>().IsInvinvible == false)
		{
            other.gameObject.GetComponent<Player>().TakeDamage(0);
            SoundPlayer.PlaySound(2, SoundPlayer.hit, 0.4f, 1.5f);

            // Spawn hit effect
            HitEffect hitEffectInstance = Instantiate(_hitEffectPrefab, (transform.position + other.transform.position) / 2 - new Vector3(0,0,1), Quaternion.identity);
            hitEffectInstance.transform.localScale = Vector3.one * 0.875f;
            hitEffectInstance.Color = new Color(0.75f, 1, 1, 1);

            Destroy(gameObject);
		}
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        handleCollison(other);
    }
    void OnCollisionStay2D(Collision2D other)
    {
        handleCollison(other);
    }
}
