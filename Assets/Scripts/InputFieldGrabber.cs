using UnityEngine;
using UnityEngine.UI;

public class InputFieldGrabber : MonoBehaviour
{

    [SerializeField]
    private string inputText;
    [SerializeField] 
    private GameObject reactoinGroup;
    [SerializeField]
    private Text reactoinTextBox;


    public void GrabFromInputField(string input)
    {
        inputText = input;
        GameManager.Instance.userName = input;
    }

    private void DisplayReactoins()
    {

    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
