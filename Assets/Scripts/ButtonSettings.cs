using UnityEngine;


public class ButtonSettings : MonoBehaviour
{
    public void SwichDog()
    {
        int indexPlayerDog = Dog.GetIndexPlayerDog();
        int maxCountDog = SpawnAnimal.GetMaxCountDog();
        int firstIndexDog = 1;

        if (indexPlayerDog == maxCountDog)
        {
            Dog.SetIndexPlayerDog(firstIndexDog);
            return;
        }
        indexPlayerDog++;
        Dog.SetIndexPlayerDog(indexPlayerDog);
    }

}




