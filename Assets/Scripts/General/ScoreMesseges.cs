using UnityEngine;

public class ScoreMesseges : MonoBehaviour
{
    [SerializeField] private GameObject messegeAll;
    [SerializeField] private GameObject messege7;
    [SerializeField] private GameObject messege4;
    [SerializeField] private GameObject messege1;

    public int waves;

    // Start is called before the first frame update
    void Start()
    {
        waves = HowMuchWaves.GetInstancia().num;
        switch (waves)
        {
            case 1:
            case 2:
            case 3:messege1.SetActive(true); break;
            case 4:
            case 5:
            case 6:messege4.SetActive(true); break;
            case 7:
            case 8:
            case 9:messege7.SetActive(true); break;
            case 10:messegeAll.SetActive(true); break;
        }
    }
}
