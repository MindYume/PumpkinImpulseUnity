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

public class SoundPlayer : MonoBehaviour
{
    private static AudioSource[] _audioSources;
    public static AudioClip btn_hover, btn_click, wave, wave_end, hit, take_damage1, take_damage2, take_damage3, game_over;
    
    void Start()
    {
        _audioSources = GetComponents<AudioSource>();

        btn_hover = Resources.Load<AudioClip>("Sounds/btn_hover");
        btn_click = Resources.Load<AudioClip>("Sounds/btn_click");
        wave = Resources.Load<AudioClip>("Sounds/wave");
        wave_end = Resources.Load<AudioClip>("Sounds/wave_end");
        hit = Resources.Load<AudioClip>("Sounds/hit");
        take_damage1 = Resources.Load<AudioClip>("Sounds/take_damage1");
        take_damage2 = Resources.Load<AudioClip>("Sounds/take_damage2");
        take_damage3 = Resources.Load<AudioClip>("Sounds/take_damage3");
        game_over = Resources.Load<AudioClip>("Sounds/game_over");
    }

    public static void PlaySound(int audioSourcesIndex, AudioClip audioClip, float volume, float pitch)
    {
        _audioSources[audioSourcesIndex].pitch = pitch;
        _audioSources[audioSourcesIndex].PlayOneShot(audioClip, volume);
    }

}
