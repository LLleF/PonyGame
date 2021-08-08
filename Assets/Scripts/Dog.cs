using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Dog : MonoBehaviour
{
    private int _speedDog = 30;
    private int _myIndex;
    private int _countPonyFollowingThisDog;
    private static int _indexPlayerDog;
    private static int MinDistanseForFollowing = 8;
    private static int _countPony—orralledForBonus;

    private Animator _dogAnimator;

    private Vector2 _clickCoordinates = new Vector2();

    [SerializeField] private GameObject _bonus;
    [SerializeField] private GameObject _indicator;

    [SerializeField] private static List<GameObject> _newBonus = new List<GameObject>();





    void Start()
    {
        _dogAnimator = GetComponent<Animator>();
        _clickCoordinates = transform.position;
        AssignmentIndex();
    }

    void Update()
    {
        IndicatorControl();
        DogMoving();
        TakeNewPony();
        SpawnBonus();
        CollectBonus();
    }



    public static void SetIndexPlayerDog(int newIndex)
    {
        _indexPlayerDog = newIndex;
    }

    public static void AddCountPony—orralledForBonus()
    {
        _countPony—orralledForBonus++;
    }

    public static int GetIndexPlayerDog()
    {
        return _indexPlayerDog;
    }

    public static int GetMinDistanseForFollowing()
    {
        return MinDistanseForFollowing;
    }





    private void AssignmentIndex()
    {
        _indexPlayerDog++;
        _myIndex = _indexPlayerDog;
    }

    private void IndicatorControl()
    {
        bool isActivDog = (_myIndex == _indexPlayerDog);
        if (!isActivDog && _indicator.activeSelf)
        {
            _indicator.SetActive(false);
        }
        else if (isActivDog && !_indicator.activeSelf)
        {
            _indicator.SetActive(true);
        }
    }

    private void DogMoving()
    {
        bool isActivDog = (_myIndex == _indexPlayerDog);
        if (isActivDog)
        {
            NewClickCoordinate();
            TurnDog(_clickCoordinates);
            transform.position = Vector2.MoveTowards(transform.position, _clickCoordinates, _speedDog * Time.deltaTime);
            _dogAnimator.SetBool("IsDogMove", Vector2.Distance(transform.position, _clickCoordinates) > 2);
        }
        else
        {
            _dogAnimator.SetBool("IsDogMove", false);
        }
    }

    private void NewClickCoordinate()
    {
        if (Input.GetKey(KeyCode.Mouse0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Vector2 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _clickCoordinates = worldPosition;
            BlockMovingAcrossBorder();
        }
    }

    private void BlockMovingAcrossBorder()
    {
        if (Physics2D.Linecast(transform.position, _clickCoordinates))
        {
            _clickCoordinates = transform.position;
        }
    }

    private void TurnDog(Vector3 direction)
    {
        if (direction.x > transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (direction.x < transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    private void TakeNewPony()
    {
        List<GameObject> _ponyPosition = SpawnAnimal.GetNewSpawnPonyPosition();
        for (int pony = 0; pony < _ponyPosition.Count; pony++)
        {
            if (Vector3.Distance(transform.position, _ponyPosition[pony].transform.position) < MinDistanseForFollowing)
            {
                Pony _scriptPony = _ponyPosition[pony].GetComponent<Pony>();
                _scriptPony.SetDog(this.gameObject);
                SpawnAnimal.RemoveAtNewSpawnPony(pony);
                _countPonyFollowingThisDog++;
            }
        }
    }

    private void SpawnBonus()
    {
        if (_countPonyFollowingThisDog >= 5 && _countPony—orralledForBonus >= 5)
        {
            _countPony—orralledForBonus = 0;
            _countPonyFollowingThisDog = 0;
            float _xCoordinateObjectSpawn;
            float _yCoordinateObjectSpawn;
            SpawnAnimal.NewRandomCoordinate(out _xCoordinateObjectSpawn, out _yCoordinateObjectSpawn);
            _newBonus.Add(Instantiate(_bonus, new Vector3(_xCoordinateObjectSpawn, _yCoordinateObjectSpawn, 0), Quaternion.identity));
        }
        else if (_countPony—orralledForBonus == _countPonyFollowingThisDog)
        {
            _countPony—orralledForBonus = 0;
            _countPonyFollowingThisDog = 0;
        }
    }

    private void CollectBonus()
    {
        if (_newBonus.Count > 0)
        {
            for (int i = 0; i < _newBonus.Count; i++)
            {
                if (_newBonus[i] != null && Vector2.Distance(transform.position, _newBonus[i].transform.position) < 5)
                {
                    Destroy(_newBonus[i]);
                    _newBonus.RemoveAt(i);
                    Timer.ReduceTime(7f);
                }
                else if (_newBonus[i] == null)
                {
                    _newBonus.RemoveAt(i);
                }
            }
        }
    }

}

