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

public class EnemyPumpkin : Enemy
{
    [SerializeField] private Bullet _bulletPrefab;
    private Rigidbody2D _rigidbody2D;
    private Player _player;
    private Animator _animator;
    private float _health = 200;
    private float _reloadDelay = 1.5f;
    private float _reloadTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponentInChildren<Animator>();
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
            Vector3 relativePlayerPosition = _player.transform.localPosition-transform.localPosition;
            Vector2 relativePlayerPosition2D = new Vector2(relativePlayerPosition.x, relativePlayerPosition.y);
            float distanceToPlayer = relativePlayerPosition2D.magnitude;
            if (distanceToPlayer > 2)
            {
                _rigidbody2D.velocity += relativePlayerPosition2D * Time.deltaTime;
            }
            else
            {
                _rigidbody2D.velocity -= relativePlayerPosition2D * Time.deltaTime;
            }

            _reloadTime += Time.deltaTime;
            if (_reloadTime >= _reloadDelay)
            {
                _reloadTime -= _reloadDelay;

                // Spawn bullet
                Bullet bulletInstance = Instantiate<Bullet>(_bulletPrefab, transform.position, Quaternion.identity);
                bulletInstance.SetTarget("player");
                bulletInstance.SetPower(0.7f);
                bulletInstance.SetColor(new Color(1, 0.78f, 0.4f), new Color(1, 0.5f, 0), new Color(1, 1, 0.78f));
                bulletInstance.GetRigidBody2d.velocity += relativePlayerPosition2D.normalized * 2.5f;

                // Play sounds
                float volume = 0.5f + UnityEngine.Random.value / 2;
                float pitch = 0.8f + UnityEngine.Random.value / 2.5f;
                SoundPlayer.PlaySound(5, SoundPlayer.fire, volume, pitch);
                SoundPlayer.PlaySound(5, SoundPlayer.wave_end, volume, pitch);
            }

            // Set eyes direction
            _animator.SetFloat("x", relativePlayerPosition.x);
            _animator.SetFloat("y", relativePlayerPosition.y);

        }

        if (transform.localPosition.x < -3 || transform.localPosition.x > 3 || transform.localPosition.y < -3 || transform.localPosition.y > 3)
        {
            Debug.Log("Pumpkin is out of border");
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
