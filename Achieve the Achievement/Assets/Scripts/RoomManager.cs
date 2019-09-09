/**
 * RoomManager is responsible for acting on roomcompletions and generating new rooms.
 * It loads all rooms from the resources folder on awake and puts them into their own respective arrays (Are lists better? idk)
 * This script holds all rooms, current room, a player, scoremanager and room arrays
 * */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public List<RoomBase> Rooms;
    public RoomBase ActiveRoom;
    public int ActiveRoomNumber = 0;
    public GameObject Player;
    public ScoreManager _ScoreManager;

    LevelFader levelFader;

    Object[] puzzleRooms;
    Object[] enemyRooms;
    Object[] collectibleRooms;

    public enum MajorAchievementTypes
    {
        Puzzle,
        Enemy,
        Collectible
    }

    void Awake()
    {
        _ScoreManager = FindObjectOfType<ScoreManager>();
        levelFader = GetComponent<LevelFader>();
        //Load all room prefabs from the resources folder
        puzzleRooms = Resources.LoadAll("Rooms/PuzzleRooms", typeof(GameObject));
        enemyRooms = Resources.LoadAll("Rooms/EnemyRooms", typeof(GameObject));
        collectibleRooms = Resources.LoadAll("Rooms/CollectibleRooms", typeof(GameObject));
        Rooms = new List<RoomBase>();
    }

    void Start()
    {
        //Reset variables, spawn the player and replace the prefab with the instantiated player.
        StartCoroutine(GoToNextLevel());

        Player = Instantiate(Player, ActiveRoom.SpawnPoint.transform.position, Quaternion.identity);
    }

    public IEnumerator GoToNextLevel()
    {

        if(ActiveRoomNumber != 0)
        {
            yield return levelFader.FadeRoutine(LevelFader.FadeDirection.In);
            Rooms[ActiveRoomNumber - 1].gameObject.SetActive(false);
        }

        //Set the HighRoom value to the current completed room, not the next one to enter.
        ApplicationModel.HighRoom = ActiveRoomNumber;

        //Increase the roomnumber
        ActiveRoomNumber++;

        //Simple level check used to ensure the first 3 levels are always an enemy, collectible and puzzle before the timer starts
        switch (ActiveRoomNumber)
        {
            case 1:
                GenerateRoom(new Vector2(0, 0), MajorAchievementTypes.Puzzle);
                break;
            case 2:
                GenerateRoom(new Vector2(0, 0), MajorAchievementTypes.Enemy);
                break;
            case 3:
                GenerateRoom(new Vector2(0, 0), MajorAchievementTypes.Collectible);
                break;
            case 4:
                GenerateRoom(new Vector2(0, 0), (MajorAchievementTypes)Random.Range(0, 2));
                //Start the timer at room 4
                _ScoreManager.StartTimer();
                break;
            default:
                //After that just randomly keep spawning random rooms.
                GenerateRoom(new Vector2(0, 0), (MajorAchievementTypes)Random.Range(0, 2));
                break;
        }

        //Because Active room starts at 1 and arrays at 0
        ActiveRoom = Rooms[ActiveRoomNumber - 1];
        //Spawn the player in the active room.
        SpawnPlayer(ActiveRoom.SpawnPoint);

        yield return levelFader.FadeRoutine(LevelFader.FadeDirection.Out);
    }

    public void SpawnPlayer(SpawnPoint spawnPoint)
    {
        Player.transform.position = spawnPoint.transform.position;
    }

    //Generating a room, you can provide the position and achievementtype yourself so choosing a random room will be done OUTSIDE of this function to keep it flexible
    public void GenerateRoom(Vector3 position, MajorAchievementTypes achievementType)
    {
        GameObject go;
        switch (achievementType)
        {
            case MajorAchievementTypes.Puzzle:
                go = Instantiate(puzzleRooms[Random.Range(0, puzzleRooms.Length)], position, Quaternion.identity) as GameObject;
                Rooms.Add(go.GetComponent<RoomBase>());
                break;
            case MajorAchievementTypes.Enemy:
                go = Instantiate(enemyRooms[Random.Range(0, enemyRooms.Length)], position, Quaternion.identity) as GameObject;
                Rooms.Add(go.GetComponent<RoomBase>());
                break;
            case MajorAchievementTypes.Collectible:
                go = Instantiate(collectibleRooms[Random.Range(0, collectibleRooms.Length)], position, Quaternion.identity) as GameObject;
                Rooms.Add(go.GetComponent<RoomBase>());
                break;
        }

        //go.transform.parent = transform;
    }
}
