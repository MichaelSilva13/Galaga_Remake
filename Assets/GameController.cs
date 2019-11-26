using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    protected GameState _currentState;
    protected GameState _previousState;
    protected bool _inTransition;
    public int credits = 0;
    public GameObject PressStart;
    public GameObject LifeCounter;
        [SerializeField]
    private float _delta, _speed;

    [SerializeField] private Text levelText;

    [SerializeField] private int nextLevel = 1000;
    public GameObject EndScreen;
    public GameObject WelcomeScreen;

    private int level;

    public int Level
    {
        get => level;
        set
        {
            level = value;
            levelText.text = level.ToString();
            if (level > 1)
            {
                EnemySpawner.LevelUp();
                startSound.Play();
                nextLevel += 1500;
            }
        }
    }

    public AudioSource coinSound;
    public AudioSource homeSound;
    public AudioSource startSound;
    public Text finaleScoreText;
    public GameObject LeaderBoardTexts;
    public EnemySpawner EnemySpawner;


    private int _score;

    public int Score
    {
        get => _score;
        set
        {
            _score = value;
            _scoreText.text = $"{_score:000,000}";
            if (_score % nextLevel == 0 && _score >= nextLevel)
            {
                Level++;
            }
        }
    }
    [SerializeField] private Text _scoreText;

    private Rigidbody2D _rigidbody2D;
    [SerializeField]
    private List<Vector2> _positions = new List<Vector2>();

    [SerializeField] private GameObject _player;


    // private AbstractCharacter _character;

    private Animator _anim;

    private Transform _targetTransform;
    // private AbstractWeapon _weapon;

    public Transform TargetTransform
    {
        get { return _targetTransform; }
        set { _targetTransform = value; }
    }

    #region Properties

    public float Delta => _delta;

    public float Speed => _speed;

    public List<Vector2> Positions => _positions;

    public virtual GameState CurrentState
    {
        get { return _currentState; }
        set { Transition (value); }
    }
    
    #endregion

    /**
     * If a state of the demanded type is not present in the components it is added
     */
    public virtual T GetState<T> () where T : GameState
    {
        T target = GetComponent<T>();
        if (target == null)
            target = gameObject.AddComponent<T>();
        return target;
    }
    /**
     * Changes the current state of the machine
     */
    public virtual void ChangeState<T> () where T : GameState
    {
        CurrentState = GetState<T>();
    }

    public void BackToPrevious()
    {

        CurrentState = _previousState;
    }
    
    /**
     * Exits last state and enters new state
     */
    protected virtual void Transition (GameState value)
    {
        if (_currentState == value || _inTransition)
            return;
        _inTransition = true;
    
        if (_currentState != null)
            _currentState.Exit();
        
        _previousState = _currentState;
        _currentState = value;
    
        if (_currentState != null)
            _currentState.Enter();
    
        _inTransition = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _currentState.StateUpdate();
    }

    private void LateUpdate()
    {
        if(Input.GetButtonDown("Cancel"))
        {
            credits++;
            coinSound.Play();
            if(homeSound.isPlaying)
                homeSound.Stop();
        }
    }

    private void Start()
    {
        GameObjectPoolController.AddEntry("Player", _player, 1, 3);
        Score = 0;
        ChangeState<HomeState>();
        
        //EnemySpawner = FindObjectOfType<EnemySpawner>();
    }
}
