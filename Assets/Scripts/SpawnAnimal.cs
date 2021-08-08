using System.Collections.Generic;
using UnityEngine;


public class SpawnAnimal : MonoBehaviour
{

    [SerializeField] private GameObject _dog;
    [SerializeField] private GameObject _pony;

    private static List<GameObject> _newSpawnPonyPosition = new List<GameObject>();
    private List<Vector3> _spawnPositionAnimals = new List<Vector3>();

    private const int MaxCountPony = 19;
    private const int MaxCountDog = 3;
    private const int GameBorderX = 80;
    private const int GameBorderY = 45;



    void Start()
    {
        SpawnOneTypeAnimals(MaxCountPony, _pony);
        SpawnOneTypeAnimals(MaxCountDog, _dog);
        _spawnPositionAnimals.Clear();
    }


    public static List<GameObject> GetNewSpawnPonyPosition()
    {
        return _newSpawnPonyPosition;
    }

    public static void RemoveAtNewSpawnPony(int elementIndex)
    {
        _newSpawnPonyPosition.RemoveAt(elementIndex);
    }

    public static int GetMaxCountDog()
    {
        return MaxCountDog;
    }

    public static int GetMaxCountPony()
    {
        return MaxCountPony;
    }

    public static void NewRandomCoordinate(out float _xCoordinateObjectSpawn, out float _yCoordinateObjectSpawn)
    {
        _xCoordinateObjectSpawn = Random.Range(-GameBorderX, GameBorderX);
        _yCoordinateObjectSpawn = Random.Range(-GameBorderY, GameBorderY);
    }



    private void SpawnOneTypeAnimals(int maxCountAnimals, GameObject animal)
    {
        for (int _newAnimal = 0; _newAnimal < maxCountAnimals;)
        {
            bool _isTreeCoordinates = true;
            float _xCoordinateObjectSpawn;
            float _yCoordinateObjectSpawn;
            NewRandomCoordinate(out _xCoordinateObjectSpawn, out _yCoordinateObjectSpawn);

            if (OutOffBorderPaddock(_xCoordinateObjectSpawn, _yCoordinateObjectSpawn))
            {
                for (int readyAnimal = 0; readyAnimal < _spawnPositionAnimals.Count; readyAnimal++)
                {
                    //Проверка попадания нового животного на другого готового животного
                    if (OutOfReadyAnimal(_xCoordinateObjectSpawn, _yCoordinateObjectSpawn, readyAnimal))
                    {
                        _isTreeCoordinates = false;
                        break;
                    }
                }

                //Если новое место находится за пределами загона, и в этом месте нету уже готового животного - создаём животное
                if (_isTreeCoordinates)
                {
                    if (animal == _pony)
                    {
                        _newSpawnPonyPosition.Add(Instantiate(animal, new Vector3(_xCoordinateObjectSpawn, _yCoordinateObjectSpawn, 0), Quaternion.identity));
                    }
                    else if (animal == _dog)
                    {
                        Instantiate(animal, new Vector3(_xCoordinateObjectSpawn, _yCoordinateObjectSpawn, 0), Quaternion.identity);
                    }

                    _spawnPositionAnimals.Add(new Vector3(_xCoordinateObjectSpawn, _yCoordinateObjectSpawn, 0));
                    _newAnimal++;
                }
            }
        }



    }

    private bool OutOfReadyAnimal(float _xCoordinateObjectSpawn, float _yCoordinateObjectSpawn, int readyAnimal)
    {
        const int bordersOfSprite = 10;
        bool _outOfReadyAnimalX = ((_xCoordinateObjectSpawn < _spawnPositionAnimals[readyAnimal].x + bordersOfSprite) &&
                                    (_spawnPositionAnimals[readyAnimal].x - bordersOfSprite < _xCoordinateObjectSpawn));
        bool _outOfReadyAnimalY = (_yCoordinateObjectSpawn < _spawnPositionAnimals[readyAnimal].y + bordersOfSprite) &&
                                    (_spawnPositionAnimals[readyAnimal].y - bordersOfSprite < _yCoordinateObjectSpawn);

        return _outOfReadyAnimalX && _outOfReadyAnimalY;
    }

    private bool OutOffBorderPaddock(float _xCoordinateObjectSpawn, float _yCoordinateObjectSpawn)
    {
        const int outBordersOfPaddock = 33;
        bool _outOffBorderPaddockX = _xCoordinateObjectSpawn > outBordersOfPaddock || -outBordersOfPaddock > _xCoordinateObjectSpawn;
        bool _outOffBorderPaddockY = _yCoordinateObjectSpawn > outBordersOfPaddock || -outBordersOfPaddock > _yCoordinateObjectSpawn;
        return _outOffBorderPaddockX || _outOffBorderPaddockY;
    }

}

