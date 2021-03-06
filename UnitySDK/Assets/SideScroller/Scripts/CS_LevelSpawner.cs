﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//////////////////////////////////////////////////////////////////
//Created by: Daniel McCluskey
//Project: CT6007 - Machine Learning SideScrolling game
//Script Purpose: Script which spawns the blocks that make up a level
//////////////////////////////////////////////////////////////////
public class CS_LevelSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject m_goLevelBlock;//The block that forms the level

    [SerializeField]
    private GameObject m_goHoleBlock;//The block that fills holes in the level

    [SerializeField]
    private GameObject m_goGoalBlock;

    [SerializeField]
    private int m_iLevelWidth = 10;//The width of the level in blocks/tiles

    [SerializeField]
    private int m_iLevelHeight = 10;//The height of the level in blocks/tiles

    private List<GameObject> m_lLevelBlockList = new List<GameObject>();//List of blocks used in the level
    private List<GameObject> m_lEnemyBlockList = new List<GameObject>();//List of enemies used in the level
    private List<GameObject> m_lCoinList = new List<GameObject>();//List of enemies used in the level

    [SerializeField]
    private GameObject m_goEnemyPrefab;//The prefab used for enemies

    [SerializeField]
    private GameObject m_goCoinPrefab;//Prefab that is spawned for coins

    // Start is called before the first frame update
    private void Start()
    {
        DrawLevel();
    }

    /// <summary>
    /// Spawns the given level
    /// </summary>
    private void DrawLevel()
    {
        for (int y = 0; y < m_iLevelHeight; y++)//Loop through the map array
        {
            for (int x = 0; x < m_iLevelWidth; x++)//Loop through the map array
            {
                if (LevelMaps.Level1[y * m_iLevelWidth + x] == 1)//Normal block
                {
                    GameObject goNewLevelBlock = Instantiate(m_goLevelBlock, transform);
                    goNewLevelBlock.transform.position = transform.position;
                    goNewLevelBlock.transform.Translate(x * goNewLevelBlock.transform.localScale.x, -(y * goNewLevelBlock.transform.localScale.y), 0);
                }
                if (LevelMaps.Level1[y * m_iLevelWidth + x] == 2)//Hole
                {
                    GameObject goNewLevelBlock = Instantiate(m_goHoleBlock, transform);
                    goNewLevelBlock.transform.position = transform.position;
                    goNewLevelBlock.transform.Translate(x * goNewLevelBlock.transform.localScale.x, -(y * goNewLevelBlock.transform.localScale.y), 0);
                }
                if (LevelMaps.Level1[y * m_iLevelWidth + x] == 5)//Goal block
                {
                    GameObject goNewLevelBlock = Instantiate(m_goGoalBlock, transform);
                    goNewLevelBlock.transform.position = transform.position;
                    goNewLevelBlock.transform.Translate(x * goNewLevelBlock.transform.localScale.x, -(y * goNewLevelBlock.transform.localScale.y), 0);
                }
                if (LevelMaps.Level1[y * m_iLevelWidth + x] == 3)//Enemy
                {
                    GameObject goNewLevelBlock = Instantiate(m_goEnemyPrefab, transform);
                    goNewLevelBlock.transform.position = transform.position;
                    goNewLevelBlock.transform.Translate(x * goNewLevelBlock.transform.localScale.x, -(y * goNewLevelBlock.transform.localScale.y), 0);
                    m_lEnemyBlockList.Add(goNewLevelBlock);
                }
                if (LevelMaps.Level1[y * m_iLevelWidth + x] == 4)//Coin
                {
                    GameObject goNewLevelBlock = Instantiate(m_goCoinPrefab, transform);
                    goNewLevelBlock.transform.position = transform.position;
                    goNewLevelBlock.transform.Translate(x * goNewLevelBlock.transform.localScale.x, -(y * goNewLevelBlock.transform.localScale.y), 0);
                    m_lCoinList.Add(goNewLevelBlock);
                }
            }
        }
    }

    /// <summary>
    /// Destroys the enemies in the level.
    /// </summary>
    private void DestroyEnemies()
    {
        foreach (GameObject enemy in m_lEnemyBlockList)//Loop through the enemy list
        {
            Destroy(enemy);//Destroy that enemy
        }
        m_lEnemyBlockList.Clear();//Clear the enemy list
    }

    /// <summary>
    /// Destroys the coins.
    /// </summary>
    private void DestroyCoins()
    {
        foreach (GameObject coin in m_lCoinList)//Loop through the enemy list
        {
            Destroy(coin);//Destroy that enemy
        }
        m_lCoinList.Clear();//Clear the enemy list
    }

    /// <summary>
    /// Respawns the enemies.
    /// </summary>
    private void RespawnEnemies()
    {
        DestroyEnemies();//Destroy all remaining enemies
        DestroyCoins();
        for (int y = 0; y < m_iLevelHeight; y++)//Loop through the map array
        {
            for (int x = 0; x < m_iLevelWidth; x++)
            {
                if (LevelMaps.Level1[y * m_iLevelWidth + x] == 3)//If the current iteration is a enemy tile
                {
                    GameObject goNewLevelBlock = Instantiate(m_goEnemyPrefab, transform);//Spawn an enemy
                    goNewLevelBlock.transform.position = transform.position;//Move it to the start pos
                    goNewLevelBlock.transform.Translate(x * goNewLevelBlock.transform.localScale.x, -(y * goNewLevelBlock.transform.localScale.y), 0);//Move it to its intended position
                    m_lEnemyBlockList.Add(goNewLevelBlock);//Add it to the enemy list
                }
                if (LevelMaps.Level1[y * m_iLevelWidth + x] == 4)//Coin
                {
                    GameObject goNewLevelBlock = Instantiate(m_goCoinPrefab, transform);
                    goNewLevelBlock.transform.position = transform.position;
                    goNewLevelBlock.transform.Translate(x * goNewLevelBlock.transform.localScale.x, -(y * goNewLevelBlock.transform.localScale.y), 0);
                    m_lCoinList.Add(goNewLevelBlock);
                }
            }
        }
    }

    /// <summary>
    /// Resets the level.
    /// </summary>
    public void ResetLevel()
    {
        RespawnEnemies();
    }
}