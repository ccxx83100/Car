using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private double speed = 0;
    private double engineRpm;                       //エンジン回転数
    private double gearRatio = 0, minGearRatio;     //変速比                    
    private double finalRatio = 3.692f;             //最終減速比
    private float tireDiameter_mm = 662.581f;       //タイヤ直径
    private int manualTransmission;                 //MT（マニュアルトランスミッション)
    private Dictionary<int, float> mtDic;           //ギアチェンジデータベース
    public TextMeshProUGUI Text_engineRpm, Text_gearRatio, Text_manualTransmission, Text_speed;

    void Awake()
    {
        manualTransmission = 1;
        engineRpm = 0f;
        speed = 0f;
        mtDic = new Dictionary<int, float>(){
           //MT,変速比(gearRatio)
           {1,3.6264f},
           {2,2.2000f},
           {3,1.5412f},
           {4,1.2132f},
           {5,1.0000f},
           {6,0.7674f},
        };
        minGearRatio = mtDic[manualTransmission];
    }

    void Start()
    {
        TextView();
    }

    /*
    ----変速比  総合減速比
	1速	3.296	11.407
    2速	1.958	6.777
	3速	1.348	4.665
	4速	1.000	3.461
	5速	0.725	2.509
	6速	0.582	2.014
    */

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            ChanegeGearUP();
        }

        if (Input.GetMouseButton(0))
        {
            StepAccelerator();
        }
        else
        {
            AutoSpeedDown();
        }
        SpeedProcess();
        TextView();
    }


    ///-------------------------------------------------------------------------------
    /// <summary>
    /// 自動減速処理
    /// </summary>
    ///-------------------------------------------------------------------------------
    private void AutoSpeedDown()
    {
        engineRpm--;
        if (engineRpm <= 0) engineRpm = 0;
        gearRatio -= 0.01;
        if (gearRatio <= minGearRatio) gearRatio = minGearRatio;
    }

    ///-------------------------------------------------------------------------------
    /// <summary>
    /// アクセル
    /// </summary>
    ///-------------------------------------------------------------------------------
    public void StepAccelerator()
    {
        engineRpm += 10;
        if (engineRpm > 10000) engineRpm = 10000;
    }

    ///-------------------------------------------------------------------------------
    /// <summary>
    /// ブレーキ
    /// </summary>
    ///-------------------------------------------------------------------------------
    public void StepBrake()
    {
        engineRpm = 0;
    }

    ///-------------------------------------------------------------------------------
    /// <summary>
    /// ギアチェンジ
    /// </summary>
    ///-------------------------------------------------------------------------------
    public void ChanegeGearUP()
    {
        manualTransmission++;
        if (manualTransmission > 6)
        {
            manualTransmission = 6;
        }
        minGearRatio = mtDic[manualTransmission];
    }

    public void ChanegeGearDown()
    {
        manualTransmission--;
        if (manualTransmission <= 1)
        {
            manualTransmission = 1;
        }
        minGearRatio = mtDic[manualTransmission];
    }

    ///-------------------------------------------------------------------------------
    /// <summary>
    /// スピード処理
    /// </summary>
    ///-------------------------------------------------------------------------------
    private void SpeedProcess()
    {
        speed = engineRpm * 60 * tireDiameter_mm * 3.14 / gearRatio / finalRatio / 1000000;
    }

    ///-------------------------------------------------------------------------------
    /// <summary>
    /// UIテキストの処理
    /// </summary>
    ///-------------------------------------------------------------------------------
    private void TextView()
    {
        Text_engineRpm.text = engineRpm.ToString();
        Text_gearRatio.text = gearRatio.ToString("F4");
        Text_manualTransmission.text = manualTransmission.ToString();
        Text_speed.text = speed.ToString("F1");
    }
}





/*
減速比とは
減速比とはトランスミッションで変速された回転数とタイヤの回転数の比率のことで、
デファレンシャル比（デフ比）とも最終減速比ともいいます。
エンジンの回転数とタイヤの回転数の比率は変速比と減速比がわかれば、下記の計算式で算出できます。

.エンジンの回転数とタイヤの回転数の比率＝変速比×減速比
*/


//A18*60*$C9*3.14/$B13/$C10/1000000



/*
C#のメソッドには一定の命名規則があります。
インスタンスを帰すメソッド名は先頭にCreateかNew、TrueかFalseを帰すメソッド名は先頭にIsかCanかHas、
型変換を行うメソッド名は先頭にToを付けます。
これにより一目でメソッドの機能がわかるようになります

非同期で動作するメソッドの名前には、以下のように末尾にAsyncを付けます。
*/