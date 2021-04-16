using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public Order[] orders;
    public Text incomeText;
    public Text timerText;
    public GameObject gameoverScreen;
    public GameObject pauseScreen;
    public GameObject gameoverFirstButton;
    public GameObject pauseFirstButton;
    public GameObject tips;
    public Text totalIncome;
    public float timer;
    public int income = 0;
    [SerializeField] private GameObject ai;
    private Item[] choices = {Item.COOKED_STEAK_IP, Item.COOKED_FRIES_IP, Item.COOKED_CHICKEN_IP, Item.DRINK};

    private static GameManager m_Instance;
    public static GameManager Instance
    {
        get
        {
            if (m_Instance == null){
                if (FindObjectOfType<GameManager>() != null)
                {
                    m_Instance = FindObjectOfType<GameManager>();
                }
                else
                {
                    GameObject gm = new GameObject("GameManager");
                    m_Instance = gm.AddComponent<GameManager>();
                } 
            }
            return m_Instance;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        IncomeChange(0);

        orders[0].gameObject.SetActive(true);
        orders[0].NewOrder(choices[Random.Range(0, choices.Length)]);

        StartCoroutine(AcceptOrder());
        StartCoroutine(ReadyToSpawnAI());
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        timerText.text = ((int)timer).ToString();

        if (timer < 0)
        {
            Time.timeScale = 0;
            gameoverScreen.SetActive(true);
            totalIncome.text = income.ToString();
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(gameoverFirstButton);
        }

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.JoystickButton2))
        {
            Pause();
        }

        if (Input.GetKeyDown(KeyCode.JoystickButton3) || Input.GetKeyDown(KeyCode.Tab))
        {
            tips.SetActive(!tips.activeSelf);
        }
    }

    IEnumerator AcceptOrder()
    {
        yield return new WaitForSeconds(Random.Range(12.0f, 14.0f));
        if (FullOrder() <= 5)
        {
            int UIIndex = FullOrder();
            int dishIndex = Random.Range(0, choices.Length);
            orders[UIIndex].gameObject.SetActive(true);
            orders[UIIndex].NewOrder(choices[dishIndex]);
        }
        StartCoroutine(AcceptOrder());
    }

    int FullOrder()
    {
        for (int i = 0; i < 5; i++)
        {
            if (orders[i].gameObject.activeSelf == false)
            {
                return i;
            }
        }

        return 99;
    }


    public void DelieverFood(Item dish)
    {
        bool match = false;
        List<Order> sameOrders = new List<Order>();

        for (int i = 0; i < 5; i++)
        {
            if (orders[i].gameObject.activeSelf == true && orders[i].desiredDish == dish)
            {
                sameOrders.Add((orders[i]));
            }
        }

        if (sameOrders.Count > 0)
        {
            match = true;
            
            Order sendingOrder = sameOrders[0];
            foreach(Order order in sameOrders)
            {
                if (order.remainingTime < sendingOrder.remainingTime)
                {
                    sendingOrder = order;
                }
            }

            sendingOrder.Reset();
            sendingOrder.gameObject.SetActive(false);

            switch (dish)
            {
                case Item.COOKED_STEAK_IP:
                    IncomeChange(200);
                    break;
                case Item.COOKED_CHICKEN_IP:
                    IncomeChange(160);
                    break;
                case Item.COOKED_FRIES_IP:
                    IncomeChange(120);
                    break;
                case Item.DRINK:
                    IncomeChange(80);
                    break;
            }
        }

        if (match)
        {
            GetComponent<AudioSource>().Play();
            incomeText.gameObject.GetComponent<Animator>().SetTrigger("Earn");
        }
        else
        {
            incomeText.gameObject.GetComponent<Animator>().SetTrigger("Lose");
            IncomeChange(-50);
        }
    }

    /*
    public void DelieverFood(Item dish)
    {
        bool match = false;

        for (int i = 0; i < 5; i++)
        {
            Order[] sameOrders;
            if (orders[i].gameObject.activeSelf == true && orders[i].desiredDish == dish)
            {
                match = true;
                switch (dish)
                {
                    case Item.COOKED_STEAK_IP:
                        IncomeChange(200);
                        break;
                    case Item.COOKED_CHICKEN_IP:
                        IncomeChange(160);
                        break;
                    case Item.COOKED_FRIES_IP:
                        IncomeChange(120);
                        break;
                    case Item.DRINK:
                        IncomeChange(80);
                        break;
                }
                orders[i].Reset();
                orders[i].gameObject.SetActive(false);
                break;
            }
        }

        if (match)
        {
            GetComponent<AudioSource>().Play();
            incomeText.gameObject.GetComponent<Animator>().SetTrigger("Earn");
        }
        else
        {
            incomeText.gameObject.GetComponent<Animator>().SetTrigger("Lose");
            IncomeChange(-50);
        }
    }
    */

    void IncomeChange(int amount)
    {
        income += amount;
        incomeText.text = income.ToString();
    }

    IEnumerator ReadyToSpawnAI()
    {
        //yield return new WaitForSeconds(3.0f);
        yield return new WaitForSeconds(60.0f);

        ai.SetActive(true);
    }

    public void NewGame()
    {
        timer = 1.0f;
        income = 0;
    }

    public void BackToMenu()
    {
        Time.timeScale = 1;
        NewGame();
        SceneManager.LoadScene("Menu");
    }

    public void Pause()
    {
        Time.timeScale = 0;
        pauseScreen.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(pauseFirstButton);
    }

    public void Replay()
    {
        Time.timeScale = 1;
        NewGame();
        SceneManager.LoadScene("GameScene");
    }

    public void Resume()
    {
        Time.timeScale = 1;
        pauseScreen.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
