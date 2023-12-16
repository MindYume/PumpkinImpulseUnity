using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PauseButton : MonoBehaviour
{
    private TextMeshProUGUI _textMeshPro;
    // Start is called before the first frame update
    void Start()
    {
        _textMeshPro = GetComponentInChildren<TextMeshProUGUI>();

        switch(GeneralSingleton.Instance.Language)
		{
			case LanguageEnum.English:
                _textMeshPro.font = Resources.Load<TMP_FontAsset>("Fonts/OpenSans/OpenSansGreen");
                _textMeshPro.text = "Pause";
				break;
            
            case LanguageEnum.Japanese:
                _textMeshPro.font = Resources.Load<TMP_FontAsset>("Fonts/NotoSans/NotoSansGreen");
                _textMeshPro.text = "一時停止";
				break;
			
			case LanguageEnum.Russian:
                _textMeshPro.font = Resources.Load<TMP_FontAsset>("Fonts/OpenSans/OpenSansGreen");
                _textMeshPro.text = "Пауза";
				break;
            
            case LanguageEnum.Turkish:
                _textMeshPro.font = Resources.Load<TMP_FontAsset>("Fonts/OpenSans/OpenSansGreen");
                _textMeshPro.text = "Duraklat";
				break;
		}
    }
}
