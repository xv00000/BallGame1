using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    private int goldCount = 0;
    [SerializeField] Text goldText;
    [SerializeField] GameObject winPanel;
    public static UIManager Instance => instance;
    private UIManager()
    {
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void GainGold()
    {
        goldCount++;
        goldText.text = "金币数量：" + goldCount;
    }

}
