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

public class Player : MonoBehaviour
{
    [SerializeField] private Bullet _bulletPrefab;
    private Rigidbody2D _rigidbody2D;
    private Transform _arrowTransform;
    private float _rotationDirection = 7.5f * Mathf.Rad2Deg;
    private float _bulletPower = 0;
    private EnergyEffect _energyEffect;
    private SpriteRenderer _sprite;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
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
    }

    //private void SpawnBullet(Vector2 position, float direction, float power)
    private void SpawnBullet()
    {
        Vector2 bulletDirection = new Vector2(-Mathf.Sin(_arrowTransform.eulerAngles.z*Mathf.Deg2Rad), Mathf.Cos(_arrowTransform.eulerAngles.z*Mathf.Deg2Rad));
        Vector3 bulletPosition = transform.position + (new Vector3(bulletDirection.x, bulletDirection.y, 0)) * 0.3f;
        Bullet bulletInstance = Instantiate<Bullet>(_bulletPrefab, bulletPosition, Quaternion.identity);
        bulletInstance.GetComponent<Rigidbody2D>().velocity = bulletDirection * _bulletPower * 4.5f;
        bulletInstance.SetPower(_bulletPower);

        //_generalSingleton.PlaySound("wave_end", (-10 + _bulletPower * 15), (0.75f / _bulletPower));
        SoundPlayer.PlaySound(1, SoundPlayer.wave_end, _bulletPower, (0.75f / _bulletPower));
    }
}
