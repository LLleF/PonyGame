using UnityEngine;

public class Pony : MonoBehaviour
{

    private int _speedPony = 40;
    private int _minDistanseForFollowing;

    private GameObject _dog;

    private Animator _ponyAnimator;

    private Vector3 _newMovingCoordinateForPonyInsidePaddock;

    private const int borderPaddock = 23;

    void Start()
    {
        _ponyAnimator = GetComponent<Animator>();
        _newMovingCoordinateForPonyInsidePaddock = transform.position;
        _minDistanseForFollowing = Dog.GetMinDistanseForFollowing();
    }


    void Update()
    {
        PonyFollowingTheDog();
        FreePony();
        FreePonyMovingInsidePaddock();
    }




    public void SetDog(GameObject dog)
    {
        _dog = dog;
    }


    private void PonyFollowingTheDog()
    {
        if (_dog != null)
        {
            TurnPony(_dog.transform.position);

            if (Vector2.Distance(transform.position, _dog.transform.position) > _minDistanseForFollowing)
            {
                _ponyAnimator.SetBool("HaveActivDog", true);
                transform.position = Vector2.MoveTowards(transform.position, _dog.transform.position, _speedPony * Time.deltaTime);
            }
        }
    }

    private void TurnPony(Vector3 direction)
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

    private void FreePony()
    {
        if (_dog != null && InsideBorrderOfPaddock(transform.position) && InsideBorrderOfPaddock(_dog.transform.position))
        {
            NewÑoordinatesOfMovement();
            _dog = null;
            Dog.AddCountPonyÑorralledForBonus();
            WinGame.AddCountPonyInsidePaddock();
        }
    }

    private void FreePonyMovingInsidePaddock()
    {
        if (InsideBorrderOfPaddock(transform.position) && _dog == null)
        {
            if (Vector3.Distance(transform.position, _newMovingCoordinateForPonyInsidePaddock) < 2)
            {
                NewÑoordinatesOfMovement();
                TurnPony(_newMovingCoordinateForPonyInsidePaddock);
            }

            transform.position = Vector2.MoveTowards(transform.position, _newMovingCoordinateForPonyInsidePaddock, 10f * Time.deltaTime);
        }
    }

    private bool InsideBorrderOfPaddock(Vector3 animalPosition)
    {
        bool InsideBorrderOfPaddockForX = (animalPosition.x < borderPaddock) && (-borderPaddock < animalPosition.x);
        bool InsideBorrderOfPaddockForY = (animalPosition.y < borderPaddock) && (-borderPaddock < animalPosition.y);
        return (InsideBorrderOfPaddockForX && InsideBorrderOfPaddockForY);
    }

    private void NewÑoordinatesOfMovement()
    {
        _newMovingCoordinateForPonyInsidePaddock = new Vector2(Random.Range(-borderPaddock, borderPaddock), Random.Range(-borderPaddock, borderPaddock));
    }



}







