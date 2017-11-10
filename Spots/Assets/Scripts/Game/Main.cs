using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour {

    private const int MAX_SIZE = 4;
    private const float START_POSITION = 225f;
    private const float BIAS_POSITION = 150f;

    private Vector3[,] _positions;
    private GameObject[,] _spots;

    [SerializeField]
    private GameObject spot, panel;
    [SerializeField]
    private Text param;

    private float _posX, _posY;

    private void Awake()
    {
        _positions = new Vector3[MAX_SIZE, MAX_SIZE];
        _spots = new GameObject[MAX_SIZE, MAX_SIZE];
        _posX = 0;
        _posY = 0;

        GenerateEnum();
    }

    private void GenerateEnum()
    {
        int inc = 0;
        _posX = -START_POSITION;
        _posY = START_POSITION;

        int[] numbers = Enumerable.Range(0, MAX_SIZE * MAX_SIZE - 1).ToArray();

        for (int i = 0; i < MAX_SIZE * MAX_SIZE - 1; i++)
        {
            int j = Random.Range(0, MAX_SIZE * MAX_SIZE - 1);
            int temp = numbers[i];
            numbers[i] = numbers[j];
            numbers[j] = temp;
        }

        for (int i = 0; i < MAX_SIZE; i++)
        {
            for (int j = 0; j < MAX_SIZE; j++)
            {
                if (i != 3 || j != 3)
                {
                    _positions[i, j].x = _posX;
                    _positions[i, j].y = _posY;
                    _spots[i, j] = Instantiate(spot, _positions[i, j], Quaternion.identity) as GameObject;
                    _spots[i, j].name = "spot_" + i + "_" + j;
                    _spots[i, j].transform.SetParent(gameObject.transform, false);
                    _spots[i, j].GetComponentInChildren<Text>().text = (numbers[inc] + 1).ToString();
                    _spots[i, j].AddComponent<Spot>();
                    inc++;
                }
                
                _posX += BIAS_POSITION;
            }

            _posY -= BIAS_POSITION;
            _posX = -START_POSITION;
        }

        if(CheckPlayability())
        {
            return;
        }
        else
        {
            DestroyAllSPots();
            GenerateEnum();
        }
    }
    
    internal void MoveSpots(string name, int step = 0, bool isMove = false)
    {
        Vector3 voidPos = new Vector3(0,0,0);
        int i = 0, j = 0;
        string temp = name;
        string tempText = "";
        
        temp = temp.Substring(5);
        i = int.Parse(temp.Substring(0, 1));
        j = int.Parse(temp.Substring(2));

        if (step <= 3)
        {
            if ((i + step <= 3) && (_positions[i + step, j] == voidPos && _spots[i + step, j] == null))
            {
                for (int index = 0; index < step; index++)
                {
                    _positions[i + step - index, j] = new Vector3(_positions[i + step - index - 1, j].x, 
                                                                 _positions[i + step - index - 1, j].y - BIAS_POSITION, 0);

                    tempText = _spots[i + step - index - 1, j].GetComponentInChildren<Text>().text;
                    Destroy(_spots[i + step - index - 1, j]);
                    _spots[i + step - index - 1, j] = null;

                    _spots[i + step - index, j] = Instantiate(spot, _positions[i + step - index, j], Quaternion.identity) as GameObject;
                    _spots[i + step - index, j].name = "spot_" + (i + step - index) + "_" + j;
                    _spots[i + step - index, j].transform.SetParent(gameObject.transform, false);
                    _spots[i + step - index, j].GetComponentInChildren<Text>().text = tempText;
                    _spots[i + step - index, j].AddComponent<Spot>();

                    _positions[i + step - index - 1, j] = new Vector3(0, 0, 0);
                    isMove = true;
                }

                if (isMove)
                {
                    CheckIfGameFinish();
                    isMove = false;
                    return;
                }
            }

            if ((i - step >= 0) && (_positions[i - step, j] == voidPos && _spots[i - step, j] == null))
            {
                for (int index = 0; index < step; index++)
                {
                    _positions[i - step + index, j] = new Vector3(_positions[i - step + index + 1, j].x,
                                                                 _positions[i - step + index + 1, j].y + BIAS_POSITION, 0);

                    tempText = _spots[i - step + index + 1, j].GetComponentInChildren<Text>().text;
                    Destroy(_spots[i - step + index + 1, j]);
                    _spots[i - step + index + 1, j] = null;

                    _spots[i - step + index, j] = Instantiate(spot, _positions[i - step + index, j], Quaternion.identity) as GameObject;
                    _spots[i - step + index, j].name = "spot_" + (i - step + index) + "_" + j;
                    _spots[i - step + index, j].transform.SetParent(gameObject.transform, false);
                    _spots[i - step + index, j].GetComponentInChildren<Text>().text = tempText;
                    _spots[i - step + index, j].AddComponent<Spot>();

                    _positions[i - step + index + 1, j] = new Vector3(0, 0, 0);
                    isMove = true;
                }

                if (isMove)
                {
                    CheckIfGameFinish();
                    isMove = false;
                    return;
                }

            }
            
            if ((j + step <= 3) && (_positions[i, j + step] == voidPos && _spots[i, j + step] == null))
            {
                for (int index = 0; index < step; index++)
                {
                    _positions[i, j + step - index] = new Vector3(_positions[i, j + step - index - 1].x + BIAS_POSITION,
                                                                 _positions[i, j + step - index - 1].y, 0);

                    tempText = _spots[i, j + step - index - 1].GetComponentInChildren<Text>().text;
                    Destroy(_spots[i, j + step - index - 1]);
                    _spots[i, j + step - index - 1] = null;

                    _spots[i, j + step - index] = Instantiate(spot, _positions[i, j + step - index], Quaternion.identity) as GameObject;
                    _spots[i, j + step - index].name = "spot_" + i + "_" + (j + step - index);
                    _spots[i, j + step - index].transform.SetParent(gameObject.transform, false);
                    _spots[i, j + step - index].GetComponentInChildren<Text>().text = tempText;
                    _spots[i, j + step - index].AddComponent<Spot>();

                    _positions[i, j + step - index - 1] = new Vector3(0, 0, 0);
                    isMove = true;
                }

                if (isMove)
                {
                    CheckIfGameFinish();
                    isMove = false;
                    return;
                }
            }

            if ((j - step >= 0) && (_positions[i, j - step] == voidPos))
            {
                for (int index = 0; index < step; index++)
                {
                    _positions[i, j - step + index] = new Vector3(_positions[i, j - step + index + 1].x - BIAS_POSITION,
                                                                 _positions[i, j - step + index + 1].y, 0);

                    tempText = _spots[i, j - step + index + 1].GetComponentInChildren<Text>().text;
                    Destroy(_spots[i, j - step + index + 1]);
                    _spots[i, j - step + index + 1] = null;

                    _spots[i, j - step + index] = Instantiate(spot, _positions[i, j - step + index], Quaternion.identity) as GameObject;
                    _spots[i, j - step + index].name = "spot_" + i + "_" + (j - step + index);
                    _spots[i, j - step + index].transform.SetParent(gameObject.transform, false);
                    _spots[i, j - step + index].GetComponentInChildren<Text>().text = tempText;
                    _spots[i, j - step + index].AddComponent<Spot>();

                    _positions[i, j - step + index + 1] = new Vector3(0, 0, 0);
                    isMove = true;
                }

                if (isMove)
                {
                    CheckIfGameFinish();
                    isMove = false;
                    return;
                }
            }

            MoveSpots(name, step + 1);
        }
        return;
    }

    private bool CheckPlayability()
    {
        return true;
    }

    private void DestroyAllSPots()
    {
        for (int i = 0; i < MAX_SIZE; i++)
        {
            for (int j = 0; j < MAX_SIZE; j++)
            {
                if(_spots[i,j] != null)
                Destroy(_spots[i, j]);
            }
        }
    }

    private void CheckIfGameFinish()
    {
        int inc = 1;
        int temp = 0;

        foreach (GameObject item in _spots)
        {
            if(item != null)
            temp = int.Parse(item.GetComponentInChildren<Text>().text);

            if (temp == inc)
            {
                inc++;
                print(inc);
            }
            else if (inc > MAX_SIZE * MAX_SIZE - 1)
            {
                Finish();
                break;
            }
            else
            {
                break;
            }
        }
    }

    private void Finish()
    {
        DestroyAllSPots();
        panel.SetActive(true);
    }

    private void Update()
    {
        switch (param.text)
        {
            case "1":
                Finish();
                param.text = "";
                break;
            case "2":

                param.text = "";
                break;
            default:
                break;
        }

        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }
    }
}
