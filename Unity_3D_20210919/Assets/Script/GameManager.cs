using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Turn turn = Turn.My;

    [Header("怪物陣列")]
    public GameObject[] goEnemys;
    [Header("慶暨貓")]
    public GameObject goMarble;
    [Header("棋盤群駔")]
    public Transform traCheckboard;
    [Header("生成數量最大值")]
    public Vector2Int v2randomEnemyCount = new Vector2Int(1, 7);
    [SerializeField]
    private Transform[] traCheckboards;
    [SerializeField]
    /// <summary>
    /// 第二列：生成怪物的棋盤
    /// </summary>
    private Transform[] traColumnSecond;
    /// <summary>
    /// 棋盤欄位數量
    /// </summary>
    private int countRow = 8;
    /// <summary>
    /// 第二列的索引值：處理怪物生成不重複
    /// </summary>
    [SerializeField]
    private List<int> indexColumnsecond = new List<int>();

    private void Awake()
    {
        // 棋盤陣列 = 棋盤群組.取得子物件的元件<變形元件>()
        traCheckboards = traCheckboard.GetComponentsInChildren<Transform>();
        
        // 初始第二列數量
        traColumnSecond = new Transform[countRow];
        // 取得第二列的棋盤
        for (int i = 9; i < 9 +countRow; i++)
        {
            traColumnSecond[i - countRow - 1] = traCheckboards[i];
        }

        SpawnEnemy();
    }

    /// <summary>
    /// 生成敵人：隨機數量 v2RandomEnemyCount
    /// </summary>
    private void SpawnEnemy()
    {
        int countEnemy = Random.Range(v2randomEnemyCount.x, v2randomEnemyCount.y);

        indexColumnsecond.Clear();

        for (int i = 0; i < 8; i++) indexColumnsecond.Add(i);

        for (int i = 0; i < countEnemy; i++)
        {
            int randomEnemy = Random.Range(0, goEnemys.Length);     // 0 ~ 2 - 隨機 0 或 1

            int randomColumnSecond = Random.Range(0, indexColumnsecond.Count);

            Instantiate(goEnemys[randomEnemy], traColumnSecond[indexColumnsecond[randomColumnSecond]].position, Quaternion.Euler(0 ,180 ,0));

            indexColumnsecond.RemoveAt(randomColumnSecond);
        }

        int randomMarble = Random.Range(0, indexColumnsecond.Count);
        Instantiate(
            goMarble,
            traColumnSecond[indexColumnsecond[randomMarble]].position + Vector3.up,
            Quaternion.identity);
    }

    /// <summary>
    /// 切換回合
    /// </summary>
    /// <param name="isMyTurn">是否玩家回合</param>
    public void SwitchTurn(bool isMyTurn)
    {
        if (isMyTurn) turn = Turn.My;
        else turn = Turn.Enemy;
    }
}

/// <summary>
/// 回合：我方與敵方
/// </summary>
public enum Turn
{
    My, Enemy
}
