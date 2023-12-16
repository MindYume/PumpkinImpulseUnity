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

public class EnemyWitch : Enemy
{
    [SerializeField] private Fire _firePrefab;
    [SerializeField] private Bullet _bulletPrefab;
    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _sprite;
    private Player _player;
    private Vector2 _movePoint;
    private float _health = 200;
    private float _minDistanceToPoint = 0.1f;
	private float _maxVelocity = 2;
    private bool _isMoving = false;
    private float _maxWaitTime = 1;
	private float _waitTime = 0;
    private float _fireSpawnDelay = 0.2f;
    private float _nextFireTime = 0;
    private float _bulletReloadDelay = 1.5f;
	private float _bulletReloadTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _sprite = transform.Find("WitchSprite").GetComponent<SpriteRenderer>();
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
            if (_isMoving)
			{
				Vector2 relativePointPosition = _movePoint - new Vector2(transform.localPosition.x, transform.localPosition.y);
                _rigidbody2D.velocity += relativePointPosition.normalized * Time.deltaTime * 5;
                _rigidbody2D.velocity = _rigidbody2D.velocity.normalized * Math.Clamp(_rigidbody2D.velocity.magnitude, 0, _maxVelocity);

				if (relativePointPosition.magnitude <= _minDistanceToPoint)
				{
					_isMoving = false;
				}

                _nextFireTime += Time.deltaTime;
				if (_nextFireTime >= _fireSpawnDelay && _rigidbody2D.velocity.magnitude >= _maxVelocity - 0.05f)
				{
					_nextFireTime = 0;

					// Spawn fire
                    SoundPlayer.PlaySound(6, SoundPlayer.fire, 0.45f + UnityEngine.Random.value / 2, (0.8f + UnityEngine.Random.value/2.5f));
					Fire fireInstance = Instantiate<Fire>(_firePrefab, transform.position, Quaternion.identity);
                    fireInstance.transform.position = transform.position;
				}
			}
			else
			{
				_waitTime += Time.deltaTime;
				if (_waitTime >= _maxWaitTime)
				{
					_isMoving = true;
					_waitTime = 0;
					_movePoint = new Vector2(-2.5f + UnityEngine.Random.value * 5, -2.5f + UnityEngine.Random.value * 5);

				}
			}

            
            _bulletReloadTime += Time.deltaTime;
            Vector3 relativePlayerPosition = _player.transform.localPosition - transform.localPosition;
            Vector2 relativePlayerPosition2D = new Vector2(relativePlayerPosition.x, relativePlayerPosition.y);
			if (relativePlayerPosition2D.magnitude < 2 && _bulletReloadTime >= _bulletReloadDelay && _rigidbody2D.velocity.x * (_player.transform.position.x - transform.position.x) >= 0)
            {
				_bulletReloadTime = 0;

                // Spawn bullet
                Bullet bulletInstance = Instantiate<Bullet>(_bulletPrefab, transform.position, Quaternion.identity);
                bulletInstance.SetTarget("player");
                bulletInstance.SetPower(0.7f);
                bulletInstance.SetColor(new Color(0.85f, 0.09f, 0), new Color(0.98f, 0.93f, 0.47f), new Color(1, 1, 0.78f));
                bulletInstance.GetRigidBody2d.velocity += relativePlayerPosition2D.normalized * 2.5f;

                // Play sounds
                float volume = 0.5f + UnityEngine.Random.value / 2;
                float pitch = 0.8f + UnityEngine.Random.value / 2.5f;
                SoundPlayer.PlaySound(5, SoundPlayer.fire, volume, pitch);
                SoundPlayer.PlaySound(5, SoundPlayer.wave_end, volume, pitch);
			}
        }

        if (_rigidbody2D.velocity.x > 0)
            {
                _sprite.flipX = true;
                _sprite.transform.localPosition = new Vector2(-0.04f, -0.01f);
            }
            else
            {
                _sprite.flipX = false;
                _sprite.transform.localPosition = new Vector2(0.04f, -0.01f);
            }

        if (transform.localPosition.x < -3 || transform.localPosition.x > 3 || transform.localPosition.y < -3 || transform.localPosition.y > 3)
        {
            Debug.Log("Witch is out of border");
            Destroy(gameObject);
        }
    }

    public void TakeDamage(float damage_value)
    {
        _health -= damage_value;
        if (_health <= 0)
        {
            HealthPoint.Spawn(Level.gameObjectStatic, transform.localPosition, 0.5f);
            Destroy(gameObject);
        }
    }
}
