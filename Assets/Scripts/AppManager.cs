using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AppManager : MonoBehaviour
{
    public static AppManager Instance = null;

    [SerializeField] private InputField inputField;
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private GameObject todoUI;
    [SerializeField] private GameObject doneUI;
    [SerializeField] private Sprite todoImage;
    [SerializeField] private Sprite doneImage;

    private List<GameObject> todoList = new List<GameObject>();
    private List<GameObject> doneList = new List<GameObject>();

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(PlayerPrefs.GetString("ToDo"));
        Debug.Log(PlayerPrefs.GetString("Done"));

        if (PlayerPrefs.GetString("ToDo") != null)
        {
            for (int i = 0; i < PlayerPrefs.GetString("ToDo").Split('/').Length; i++)
            {
                if (PlayerPrefs.GetString("ToDo").Split('/')[i] != "")
                {
                    GameObject gO = Instantiate(itemPrefab) as GameObject;
                    gO.GetComponentInChildren<Text>().text = PlayerPrefs.GetString("ToDo").Split('/')[i];
                    gO.transform.SetParent(todoUI.transform);
                    gO.GetComponentInChildren<Image>().sprite = todoImage;

                    if (todoList.Count == 0)
                        gO.GetComponent<RectTransform>().anchoredPosition = itemPrefab.GetComponent<RectTransform>().anchoredPosition;
                    else
                    {
                        gO.GetComponent<RectTransform>().anchoredPosition = itemPrefab.GetComponent<RectTransform>().anchoredPosition;
                        gO.GetComponent<RectTransform>().position = new Vector2(gO.GetComponent<RectTransform>().position.x, todoList[todoList.Count - 1].GetComponent<RectTransform>().position.y + 200);
                    }

                    todoList.Add(gO);

                    gO.GetComponentInChildren<Toggle>().onValueChanged.AddListener(delegate { AddDone(gO); });
                    gO.GetComponentInChildren<Button>().onClick.AddListener(delegate {
                        if (todoList.Contains(gO))
                            todoList.Remove(gO);
                        else if (doneList.Contains(gO))
                            doneList.Remove(gO);

                        Destroy(gO);
                    });
                }
            }
        }

        if (PlayerPrefs.GetString("Done") != null)
        {
            for (int i = 0; i < PlayerPrefs.GetString("Done").Split('/').Length; i++)
            {
                if (PlayerPrefs.GetString("Done").Split('/')[i] != "")
                {
                    GameObject gO = Instantiate(itemPrefab) as GameObject;
                    gO.GetComponentInChildren<Text>().text = PlayerPrefs.GetString("Done").Split('/')[i];
                    gO.transform.SetParent(doneUI.transform);
                    gO.GetComponentInChildren<Toggle>().isOn = true;
                    gO.GetComponentInChildren<Image>().sprite = doneImage;

                    if (doneList.Count == 0)
                        gO.GetComponent<RectTransform>().anchoredPosition = itemPrefab.GetComponent<RectTransform>().anchoredPosition;
                    else
                    {
                        gO.GetComponent<RectTransform>().anchoredPosition = itemPrefab.GetComponent<RectTransform>().anchoredPosition;
                        gO.GetComponent<RectTransform>().position = new Vector2(gO.GetComponent<RectTransform>().position.x, doneList[doneList.Count - 1].GetComponent<RectTransform>().position.y + 200);
                    }

                    doneList.Add(gO);

                    gO.GetComponentInChildren<Toggle>().onValueChanged.AddListener(delegate { AddDone(gO); });
                    gO.GetComponentInChildren<Button>().onClick.AddListener(delegate {
                        if (todoList.Contains(gO))
                            todoList.Remove(gO);
                        else if (doneList.Contains(gO))
                            doneList.Remove(gO);

                        Destroy(gO);
                    });
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            AddToDo();
        }
    }

    public void AddToDo()
    {
        if (inputField.text != "")
        {
            GameObject gO = Instantiate(itemPrefab) as GameObject;
            gO.GetComponentInChildren<Text>().text = inputField.text;
            gO.transform.SetParent(todoUI.transform);
            gO.GetComponentInChildren<Image>().sprite = todoImage;

            if (todoList.Count == 0)
                gO.GetComponent<RectTransform>().anchoredPosition = itemPrefab.GetComponent<RectTransform>().anchoredPosition;
            else
            {
                gO.GetComponent<RectTransform>().anchoredPosition = itemPrefab.GetComponent<RectTransform>().anchoredPosition;
                gO.GetComponent<RectTransform>().position = new Vector2(gO.GetComponent<RectTransform>().position.x, todoList[todoList.Count - 1].GetComponent<RectTransform>().position.y + 200);
            }

            todoList.Add(gO);

            inputField.text = "";

            gO.GetComponentInChildren<Toggle>().onValueChanged.AddListener(delegate { AddDone(gO); });
            gO.GetComponentInChildren<Button>().onClick.AddListener(delegate {
                if (todoList.Contains(gO))
                    todoList.Remove(gO);
                else if (doneList.Contains(gO))
                    doneList.Remove(gO);

                Destroy(gO);
            });
        }
    }

    public void AddDone(GameObject gO)
    {
        if (gO.GetComponentInChildren<Toggle>().isOn)
        {
            todoList.Remove(gO);
            gO.transform.SetParent(doneUI.transform);
            gO.GetComponentInChildren<Image>().sprite = doneImage;

            if (doneList.Count == 0)
                gO.GetComponent<RectTransform>().anchoredPosition = itemPrefab.GetComponent<RectTransform>().anchoredPosition;
            else
            {
                gO.GetComponent<RectTransform>().anchoredPosition = itemPrefab.GetComponent<RectTransform>().anchoredPosition;
                gO.GetComponent<RectTransform>().position = new Vector2(gO.GetComponent<RectTransform>().position.x, doneList[doneList.Count - 1].GetComponent<RectTransform>().position.y + 200);
            }

            doneList.Add(gO);
        }
        else
        {
            doneList.Remove(gO);
            gO.transform.SetParent(todoUI.transform);
            gO.GetComponentInChildren<Image>().sprite = todoImage;

            if (todoList.Count == 0)
                gO.GetComponent<RectTransform>().anchoredPosition = itemPrefab.GetComponent<RectTransform>().anchoredPosition;
            else
            {
                gO.GetComponent<RectTransform>().anchoredPosition = itemPrefab.GetComponent<RectTransform>().anchoredPosition;
                gO.GetComponent<RectTransform>().position = new Vector2(gO.GetComponent<RectTransform>().position.x, todoList[todoList.Count - 1].GetComponent<RectTransform>().position.y + 200);
            }

            todoList.Add(gO);
        }
    }

    public void DeleteAll()
    {
        foreach (GameObject gO in doneList)
        {
            Destroy(gO);
        }

        doneList.Clear();
    }

    private void OnApplicationQuit()
    {
        string tempToDo = "";
        string tempDone = "";

        for (int i = 0; i < todoList.Count; i++)
        {
            tempToDo += todoList[i].GetComponentInChildren<Text>().text + "/";
        }

        for (int i = 0; i < doneList.Count; i++)
        {
            tempDone += doneList[i].GetComponentInChildren<Text>().text + "/";
        }

        Debug.Log(tempToDo);
        PlayerPrefs.SetString("ToDo", tempToDo);
        Debug.Log(tempDone);
        PlayerPrefs.SetString("Done", tempDone);
    }
}
