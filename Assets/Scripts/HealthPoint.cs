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

public class HealthPoint : MonoBehaviour
{
    private Player _player;

    // Update is called once per frame
    void Update()
    {
        if (_player == null)
        {
            _player = GeneralSingleton.Instance.PlayerInstance;
        }
        else
        {
            Vector3 relativePosition = _player.transform.position-transform.position;
            float distanceToPlayer = relativePosition.magnitude;
            if (distanceToPlayer < 0.75f)
            {
                transform.position += relativePosition * 10 * Time.deltaTime;
            }
        }
    }

    public static void Spawn(GameObject parent, Vector2 position, float spawnProbability)
    {
        if (Random.value <= spawnProbability)
        {
            HealthPoint healthPointInstance = Instantiate<HealthPoint>(Resources.Load<HealthPoint>("Prefabs/HealthPoint"), Vector3.zero, Quaternion.identity);
            healthPointInstance.transform.parent = parent.transform;
            healthPointInstance.transform.localPosition = position;
        }
    }

    private void handleCollison(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
		{
            other.gameObject.GetComponent<Player>().TakeHealth();
            SoundPlayer.PlaySound(0, SoundPlayer.healthpoint, 1.5f, 1);

            Destroy(gameObject);
		}
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        handleCollison(other);
    }
    void OnTriggerStay2D(Collider2D other)
    {
        handleCollison(other);
    }
}