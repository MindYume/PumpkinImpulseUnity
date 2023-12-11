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
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private Bullet _bulletPrefab;
    private Rigidbody2D _rigidbody2D;
    private Transform _arrowTransform;
    private float _rotationDirection = 7.5f * Mathf.Rad2Deg;
    private float _health = 3;
    private float _bulletPower = 0;
    private EnergyEffect _energyEffect;
    private Animator _animator;
    private SpriteRenderer _sprite;
    private bool _isInvinvible = false;
    private float _invincibilityDuration = 1;
    private float _invincibilityTime = 0;

    public bool IsInvinvible => _isInvinvible;

    static public Action<float> onHealthChanged;
    [SerializeField] public UnityEvent onDead;

    // Start is called before the first frame update
    void Start()
    {
        GeneralSingleton.Instance.PlayerInstance = this;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponentInChildren<Animator>();
        _arrowTransform = transform.Find("Arrow").transform;
        _energyEffect = GetComponentInChildren<EnergyEffect>();
        _sprite = transform.Find("WitchSprite").GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale != 0)
        {
            _arrowTransform.Rotate(new Vector3(0,0,1), _rotationDirection * Time.deltaTime * (0.1f+_bulletPower*0.9f));
        
            if (Input.GetMouseButton(0))
            {
                _bulletPower += Time.deltaTime * 2;
                _bulletPower = Mathf.Clamp(_bulletPower, 0, 1);
                _energyEffect.EnergyValue = _bulletPower;
            }

            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                _rotationDirection = -_rotationDirection;
                SpawnBullet();
                _rigidbody2D.velocity += new Vector2(Mathf.Sin(_arrowTransform.eulerAngles.z*Mathf.Deg2Rad), -Mathf.Cos(_arrowTransform.eulerAngles.z*Mathf.Deg2Rad)) * _bulletPower * 3;
                _bulletPower = 0;
                _energyEffect.EnergyValue = 0;
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
        }

        if (transform.localPosition.x < -3 || transform.localPosition.x > 3 || transform.localPosition.y < -3 || transform.localPosition.y > 3)
        {
            Debug.Log("Player is out of border");
            transform.localPosition = Vector3.zero;
        }

        if (_isInvinvible)
        {
            _invincibilityTime += Time.deltaTime;
            if (_invincibilityTime >= _invincibilityDuration)
            {
                _isInvinvible = false;
                _animator.SetLayerWeight(1, 0);
                _invincibilityTime = 0;
            }
        }
    }

    //private void SpawnBullet(Vector2 position, float direction, float power)
    private void SpawnBullet()
    {
        Vector2 bulletDirection = new Vector2(-Mathf.Sin(_arrowTransform.eulerAngles.z*Mathf.Deg2Rad), Mathf.Cos(_arrowTransform.eulerAngles.z*Mathf.Deg2Rad));
        Vector3 bulletPosition = transform.position + (new Vector3(bulletDirection.x, bulletDirection.y, 0)) * 0.3f;
        Bullet bulletInstance = Instantiate<Bullet>(_bulletPrefab, bulletPosition, Quaternion.identity);
        bulletInstance.GetComponent<Rigidbody2D>().velocity = bulletDirection * _bulletPower * 4.5f;
        bulletInstance.SetPower(_bulletPower);

        SoundPlayer.PlaySound(1, SoundPlayer.wave_end, _bulletPower, (0.75f / _bulletPower));
    }

    public void TakeDamage(float _)
    {
        if (!_isInvinvible)
        {
            _health -= 1;
            onHealthChanged.Invoke(_health);

            if (_health <= 0)
            {
                onDead.Invoke();
                SoundPlayer.PlaySound(4, SoundPlayer.game_over, 0.75f, 1);
            }

            _isInvinvible = true;
            _animator.SetLayerWeight(1, 1);

            System.Random rand = new System.Random();
            switch(rand.Next(0,3))
            {
                case 0:
                    SoundPlayer.PlaySound(3, SoundPlayer.take_damage1, 0.875f, 1.1f);
                    break;
                case 1:
                    SoundPlayer.PlaySound(3, SoundPlayer.take_damage2, 0.875f, 1.1f);
                    break;
                case 2:
                    SoundPlayer.PlaySound(3, SoundPlayer.take_damage3, 0.75f, 1.1f);
                    break;
            } 
        }
    }
}
