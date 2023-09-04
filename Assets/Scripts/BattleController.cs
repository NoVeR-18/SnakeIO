using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BattleController : MonoBehaviour
{
    [SerializeField]
    Tilemap _backGround;
    [SerializeField]
    Transform _player;

    public List<Transform> tails = new List<Transform>();
    public int CountToWin = 10;
    private float _addingSpeed = 0.075f;
    private float moveDelay = 1f;
    private float timer = 0;


    public GameObject snakeBodyPrefab;
    public GameObject apple;
    private Transform appleTransform;


    private void Start()
    {
        GenerateObstacles();
        CreateApple();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= moveDelay)
        {
            for (int i = tails.Count - 1; i >= 0; i--)
            {
                tails[i].GetComponent<Moving>().Move();
            }
            _player.GetComponent<Moving>().Move();
            CheckObstacles();
            timer = 0;
        }
    }

    private void Grow()
    {
        var tailPosition = tails[tails.Count - 1];
        SnakeSegment snakeSegment = Instantiate(snakeBodyPrefab, tailPosition.position, Quaternion.identity).GetComponent<SnakeSegment>();
        tails.Add(snakeSegment.transform);
        snakeSegment.targetSegment = tailPosition;
    }

    void CheckObstacles()
    {
        var pos = Vector3Int.FloorToInt((Vector2)_player.transform.position);
        if (!_backGround.HasTile(pos))
        {
            //Debug.Log(Vector3Int.FloorToInt(_player.transform.position));
            GameManager.instance.ShowLoseMenu();
            enabled = false;
        }
        foreach (var tail in tails)
        {
            if (pos == Vector3Int.FloorToInt((Vector2)tail.position))
            {

                GameManager.instance.ShowLoseMenu();
                enabled = false;
                //Debug.Log(Vector3Int.FloorToInt(_player.transform.position));
            }
        }
        if (pos == Vector3Int.FloorToInt(appleTransform.position))
        {
            Debug.Log("eat");
            Grow();
            moveDelay -= _addingSpeed;
            Destroy(appleTransform.gameObject);
            CreateApple();
            if (tails.Count >= CountToWin)
            {
                GameManager.instance.ShowWinMenu();
                enabled = false;
            }
        }
    }
    void CreateApple()
    {
        Vector3 randomPosition = new Vector3(
               Random.Range(_backGround.cellBounds.x, _backGround.cellBounds.xMax),
               Random.Range(_backGround.cellBounds.y, _backGround.cellBounds.yMax),
               0
           );
        if (_backGround.HasTile(Vector3Int.FloorToInt(randomPosition)) && !contain())
            appleTransform = Instantiate(apple, randomPosition + new Vector3(0.5f, 0.5f), Quaternion.identity).transform;
        else
            CreateApple();

        bool contain()
        {
            foreach (var tail in tails)
            {
                if (randomPosition == Vector3Int.FloorToInt((Vector2)tail.position))
                {
                    return true;
                }
            }
            return false;
        }
    }

    void GenerateObstacles()
    {
        int obstacleCount = Random.Range(3, 6); // Генерируем случайное количество препятствий от 3 до 5

        for (int i = 0; i < obstacleCount; i++)
        {
            Vector3Int randomPosition = new Vector3Int(
                Random.Range(_backGround.cellBounds.x, _backGround.cellBounds.xMax),
                Random.Range(_backGround.cellBounds.y, _backGround.cellBounds.yMax),
                0
            );
            if (IsVectorInRange(randomPosition, Vector3Int.FloorToInt(_player.transform.position / 2)))
            {
                i--;
                continue;
            }
            _backGround.SetTile(randomPosition, null);
        }
    }
    bool IsVectorInRange(Vector3Int vector, Vector3Int player)
    {
        return vector.x >= player.x - 2 && vector.x <= player.x + 2 &&
               vector.y >= player.y - 3 && vector.y <= player.y + 2;
    }
}
