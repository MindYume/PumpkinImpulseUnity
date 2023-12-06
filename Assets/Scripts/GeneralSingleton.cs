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

public enum LanguageEnum
{
    English,
    Japanese,
    Russian,
    Turkish
}
public class GeneralSingleton
{
    private static GeneralSingleton _instance;
    public static GeneralSingleton Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GeneralSingleton();
            }

            return _instance;
        }
    }

    private Player _player;
    private LanguageEnum _language = LanguageEnum.English;
    public int MaxWave = 0;

    public Action<LanguageEnum> onLanguageCahnged;

    public LanguageEnum Language
    {
        get => _language;
        set
        {
            _language = value;
            onLanguageCahnged?.Invoke(_language);
        }
    }

    public Player PlayerInstance
    {
        set => _player = value;
        get => _player;
    }
 
}
