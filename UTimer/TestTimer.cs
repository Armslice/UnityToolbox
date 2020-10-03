using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestTimer : MonoBehaviour
{
    public Text text;
    private UTimer timer;


    // Start is called before the first frame update
    void Start()
    {   
        timer = transform.Find("UTimer").gameObject.GetComponent<UTimer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer.timeLeft >0)
            text.text = timer.ToString();
    }

    public void TestTimeup()
    {
        text.text = "TIMES UP";
    }
}
