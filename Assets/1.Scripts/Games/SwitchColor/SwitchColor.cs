using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SwitchColor : MonoBehaviour
{
    [SerializeField] float floorSpeed = 10f;
    [SerializeField] FloorBlock[] floors;

    [Header("Score UI")]
    [SerializeField] TextMeshProUGUI scoreText;
    [Header("Score UI")]
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] TextMeshProUGUI gameOverScoreText;

    private Transform playerStartPos;

    private int score = 0;
    private int level = 1;

    private bool isGame = false;

    private FaceController faceController;

    private void Start()
    {
        faceController = FindObjectOfType<FaceController>();
        if (faceController)
        {
            faceController.Event.StartListening((int)FaceEvent.MouthOpen, SetNextColor);
        }

        playerStartPos = transform;

        Init();
        StartGame();
    }

    #region Game Flow

    public void StartGame()
    {
        for (int i = 0; i < floors.Length; i++)
        {
            floors[i].SetSpeed(floorSpeed);
        }

        transform.position = playerStartPos.position;
        isGame = true;
        level = 1;

        ScoreTime();
    }

    public void StopGame()
    {
        if (!isGame) return;

        transform.position = playerStartPos.position;
        isGame = false;
        StopCoroutine(ScoreUP());

        GameOverUI();
    }

    public void LevelUP()
    {
        level += 1;
        for (int i = 0; i < floors.Length; i++)
        {
            floors[i].SetSpeed(floorSpeed * level);
        }
    }

    #endregion

    #region Player

    #region Get

    [SerializeField] private float speed = 5f;
    public ColorEnum ColorType { get { return colorType; } }

    #endregion

    #region Variable

    private MeshRenderer renderer = null;
    private ColorEnum colorType = ColorEnum.Red;

    #endregion

    private void Awake()
    {
        renderer = GetComponentInChildren<MeshRenderer>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SetNextColor();
        }

        Move();
    }

    private void Init()
    {
        StartListening();
        SetColor(ColorEnum.Yellow);
    }

    #region Move

    private void Move()
    {
        if (Input.GetKey(KeyCode.A)) transform.Translate(Vector3.forward * speed * Time.deltaTime);
        else if (Input.GetKey(KeyCode.D)) transform.Translate(Vector3.back * speed * Time.deltaTime);

        Wall();
    }

    private void Wall()
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        if (pos.x < 0f) pos.x = 0f;
        if (pos.x > 1f) pos.x = 1f;
        transform.position = Camera.main.ViewportToWorldPoint(pos);
    }

    #endregion

    #region Color
    private void SetColor(ColorEnum nextColor)
    {
        colorType = nextColor;
        renderer.material.color = GetColor.GetColorEnumToColor(colorType);
    }

    private void SetNextColor()
    {
        int index = (int)colorType + 1;
        if (index >= (int)ColorEnum.Count)
        {
            index = (int)ColorEnum.None + 1;
        }

        SetColor((ColorEnum)index);
    }

    #endregion

    #region Trigger()

    /// <summary>
    /// 플레이어와 다른 색깔인데 충돌했을 때 실행되는 함수
    /// </summary>
    /// <param name="player"></param>
    private void Different(FloorBlock floor)
    {
        EventManager.TriggerEvent(EventName.OnMiniGameOver);
    }

    /// <summary>
    /// 플레이어와 같은 색깔인데 충돌했을 때 실행되는 함수
    /// </summary>
    /// <param name="player"></param>
    private void Same(FloorBlock floor)
    {
    }

    /// <summary>
    /// 플레이어와 충돌 시
    /// </summary>
    /// <param name="player"></param>
    private void TriggerPlayer(FloorBlock floor)
    {
        if (GetColor.IsSameColor(colorType, floor))
        {
            Same(floor);
        }
        else
        {
            Different(floor);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        FloorBlock floor = collision.collider.GetComponent<FloorBlock>();
        TriggerPlayer(floor);
    }

    #endregion

    #endregion 
    #region Score

    private void ScoreTime()
    {
        score = 0;
        StartCoroutine(ScoreUP());
    }

    private IEnumerator ScoreUP()
    {
        while (isGame)
        {
            yield return new WaitForSeconds(1f);

            score += 1;
            if (score % 20 == 0) LevelUP();

            UpdateScoreUI();
        }
    }

    private void UpdateScoreUI()
    {
        scoreText.SetText(score.ToString());
    }

    private void GameOverUI()
    {
        gameOverScoreText.SetText(score.ToString());
        gameOverPanel.SetActive(true);

        Time.timeScale = 0f;
    }

    #endregion

    #region Event

    private void StartListening()
    {
        EventManager.StartListening(EventName.OnMiniGameOver, StopGame);
        EventManager.StartListening(EventName.OnMiniGameStart, StartGame);
    }

    #endregion

    private void OnDestroy()
    {
        if (faceController)
        {
            faceController.Event.StopListening((int)FaceEvent.MouthOpen, SetNextColor);
        }

        EventManager.StopListening(EventName.OnMiniGameOver, StopGame);
        EventManager.StopListening(EventName.OnMiniGameStart, StartGame);
    }

}
