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
using UnityEngine.Events;

public class Level : MonoBehaviour
{
    [SerializeField] private EnemyBat _enemyBatPrefab;
    [SerializeField] private EnemyGhost _enemyGhostPrefab;
    [SerializeField] private EnemyPumpkin _enemyPumpkinPrefab;
    [SerializeField] private EnemyWitch _enemyWitchPrefab;
    static public System.Action<int> onWaveChange;

    public static GameObject gameObjectStatic;

    private Player _player;
    private int _wave = 1;
    private float _waveScore = 1;
    private float _minWaveScore = 1;
    private bool _isBreakTime = false;
    private int _enemiesAmount = 0;
    private float _NextEnemySpawnDelay = 10;
    private float _NextEnemySpawnTime = 1;
    public int Wave
    {
        set
        {
            _wave = value;
            onWaveChange.Invoke(_wave);
        }
        get {return _wave;}
    }
    
    // Start is called before the first frame update
    void Start()
    {
        gameObjectStatic = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (_player == null && GeneralSingleton.Instance.PlayerInstance != null)
        {
            _player = GeneralSingleton.Instance.PlayerInstance;
        }
        else
        {
            _NextEnemySpawnTime -= Time.deltaTime;

            if (_enemiesAmount == 0 && !_isBreakTime && _NextEnemySpawnTime > 1)
            {
                _NextEnemySpawnTime = 1;
            }

            if (_NextEnemySpawnTime <= 0  && _waveScore >= _minWaveScore)
            {
                _isBreakTime = false;
                while(true)
                {
                    System.Random rand = new System.Random();
                    int enemyNum = rand.Next(4);
                    if(enemyNum == 0)
                    {
                        SpawnEnemy("Bat");
                        _waveScore -= _minWaveScore;
                        break;
                    }
                    else if(enemyNum == 1 && _waveScore >= 3)
                    {
                        SpawnEnemy("Ghost");
                        _waveScore -= 3;
                        break;
                    }
                    else if(enemyNum == 2 && _waveScore >= 4)
                    {
                        SpawnEnemy("Pumpkin");
                        _waveScore -= 4;
                        break;
                    }
                    else if(enemyNum == 3 && _waveScore >= 5)
                    {
                        SpawnEnemy("Witch");
                        _waveScore -= 5;
                        break;
                    }
                }
                _NextEnemySpawnTime = _NextEnemySpawnDelay;
            }

            if (_waveScore < _minWaveScore && _enemiesAmount <= 0)
            {
                if (Wave > GeneralSingleton.Instance.MaxWave)
                {
                    GeneralSingleton.Instance.MaxWave = Wave;
                }

                Wave += 1;
                _NextEnemySpawnDelay = _NextEnemySpawnDelay * 0.9f;
                _waveScore = Wave * 1.2f;
                _isBreakTime = true;
                _NextEnemySpawnTime = 3f;

                SoundPlayer.PlaySound(0, SoundPlayer.wave_complete, 0.5f, 1);
            }
        }
    }

    private void SpawnEnemy(string enemyName)
    {
        // Set enemy spawn position
        Vector3 spawnPosition = Vector3.zero;
        for(int i = 1000; i >= 0; i--)
        {
            float x = -2.75f + Random.value * 5.5f;
            float y = -2.75f + Random.value * 5.5f;
            spawnPosition = new Vector3(x,y,_player.transform.position.z);
            if ((spawnPosition - _player.transform.localPosition).magnitude >= 3)
            {
                break;
            }

            if(i == 0)
            {
                Debug.Log("The enemy could not find a position to spawn");
            }
        }

        // Spawn enemy
        Enemy enemy = GetComponent<Enemy>();
        switch(enemyName)
        {
            case "Bat":
                enemy  = Instantiate<EnemyBat>(_enemyBatPrefab, Vector3.zero, Quaternion.identity);
                break;
            case "Ghost":
                enemy  = Instantiate<EnemyGhost>(_enemyGhostPrefab, Vector3.zero, Quaternion.identity);
                break;
            case "Pumpkin":
                enemy  = Instantiate<EnemyPumpkin>(_enemyPumpkinPrefab, Vector3.zero, Quaternion.identity);
                break;
            case "Witch":
                enemy  = Instantiate<EnemyWitch>(_enemyWitchPrefab, Vector3.zero, Quaternion.identity);
                break;
            default:
                throw new System.Exception("Wrong enemy name");
        }

        enemy.transform.parent = gameObject.transform;
        enemy.transform.localPosition = spawnPosition;
        enemy.OnDestroyActionAdd(OnEmenyDefeat);

        _enemiesAmount += 1;
    }

    public void OnEmenyDefeat()
    {
        _enemiesAmount -= 1;
    }
}
