using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class OptionController : MonoBehaviour
{
    private bool enable = true;

    public GameObject option1;
    public GameObject option2;
    public GameObject label;
    public GameObject option1_text;
    public GameObject option2_text;
    public GameObject count_text;
    public GameObject correct_text;
    public GameObject wrong_text;
    public GameObject endImage; 
    public AudioSource audioSource_correct;
    public AudioSource audioSource_wrong;
    public PlacementController placementController;
    public OptionUIAnimationController optionUIAnimationController;
    public CountButtonAnimationController countButtonAnimationController;
    private int question_count = 10;
        
    private int count = 0;
    private int correct_count = 0;
    private int wrong_count = 0;
    
    private string[] str_arr = {"niau","ke/kue","hôo-lî","iûnn","bé","hóo","tshiūnn","hîm","pà","lo̍k"};
    private string[] str_ch_arr = {"貓","雞","狐狸","羊","馬","虎","象","熊","豹","鹿"};

    private string[] str_eng_arr = {"Cat", "Chicken", "Fox", "Goat", "Horse","Tiger","Elephant","Bear","Jaguar","Deer"};
    
    private string str1;
    private string str2;
    
    public string str1_ch;
    public string str2_ch;
    public string ans_ch;
    private int[] ans_arr;
    private int[] wrong_arr;
    private int[] ans_option_arr;

    void Start()
    {
        placementController = GameObject.Find("AR Session Origin").GetComponent<PlacementController>();
        optionUIAnimationController = GameObject.Find("Option").GetComponent<OptionUIAnimationController>();
        countButtonAnimationController = GameObject.Find("Count").GetComponent<CountButtonAnimationController>();

        ans_arr = new int[question_count];
        wrong_arr = new int[question_count];
        ans_option_arr = new int[question_count];
        init();
    }
    
    void Update()
    {
    }
    

    public void next(){
        count_text.GetComponent<Text>().text = $"{(count+1).ToString()} / {question_count.ToString()}";
        if(ans_option_arr[count]==0){
            option1_text.GetComponent<Text>().text = str_arr[ans_arr[count]];
            option2_text.GetComponent<Text>().text = str_arr[wrong_arr[count]];
            str1_ch = str_ch_arr[ans_arr[count]];
            str2_ch = str_ch_arr[wrong_arr[count]];
            ans_ch = str1_ch;
        }
        else{
            option2_text.GetComponent<Text>().text = str_arr[ans_arr[count]];
            option1_text.GetComponent<Text>().text = str_arr[wrong_arr[count]];
            str2_ch = str_ch_arr[ans_arr[count]];
            str1_ch = str_ch_arr[wrong_arr[count]];
            ans_ch = str2_ch;
        }
        // label.GetComponent<Text>().text = ans_ch;
        for(int i = 0; i < str_ch_arr.Length; i++){
            if(str_ch_arr[i] == ans_ch){
                placementController.SpawnAnimal(str_eng_arr[i], ans_ch);
                break;
            }
        }
        
        if(enable){
            option1.GetComponent<Button>().interactable = true;
            option2.GetComponent<Button>().interactable = true;
            option1.GetComponent<Image>().color = new Color32(0,0,0,120);
            option2.GetComponent<Image>().color = new Color32(0,0,0,120);
        }
    }

    public void init(){
        count = 0;
        correct_count = 0;
        wrong_count = 0;

        for (int i = 0; i < question_count; i++){
            ans_arr[i] = i;
            ans_option_arr[i] = UnityEngine.Random.Range(0, 2);
        }
    
        int tmp; 
        for (int i = 0; i < question_count - 1; i++)
        {
            int r = UnityEngine.Random.Range(i, question_count);//取亂數，範圍從自己到最後，決定要和哪個位置交換，因此也不用跑最後一圈了
            if (i == r) continue;
            tmp = ans_arr[i];
            ans_arr[i] = ans_arr[r];
            ans_arr[r] = tmp;
        }

        for (int i = 0; i < question_count; i++)
        {
            while(true){
                wrong_arr[i] = UnityEngine.Random.Range(0, question_count);
                if(wrong_arr[i]!=ans_arr[i])
                    break;
            }
        }
        next();
    }
   
   public void OnClick1()
    {
        if (enable)
        {
            if (ans_option_arr[count]==0){
                option1.GetComponent<Image>().color = new Color32(0,255,0,200);
                correct_count++;
                audioSource_correct.Play();
            }
            else{
                option1.GetComponent<Image>().color = new Color32(255,0,0,200);
                wrong_count++;
                audioSource_wrong.Play();
            }
            enable = false;
            option1.GetComponent<Button>().interactable = false;
            option2.GetComponent<Button>().interactable = false;
            StartCoroutine(Coroutine());
        }
    }

    public void OnClick2()
    {
        if (enable)
        {
            if (ans_option_arr[count]==1){
                option2.GetComponent<Image>().color = new Color32(0,255,0,200);
                correct_count++;
                audioSource_correct.Play();
            }
            else{
                option2.GetComponent<Image>().color = new Color32(255,0,0,200);
                wrong_count++;
                audioSource_wrong.Play();
            }
            enable = false;
            option1.GetComponent<Button>().interactable = false;
            option2.GetComponent<Button>().interactable = false;
            StartCoroutine(Coroutine());
        }       
    }

    public void try_again_OnClick(){
        init();
        endImage.active = false;
    }

    IEnumerator Coroutine()
    {
        yield return new WaitForSeconds(3);
        enable = true;
        count++;
        if(count == question_count){
            placementController.cleanAnimal();
            correct_text.GetComponent<Text>().text = $"答對題數 : {correct_count}";
            wrong_text.GetComponent<Text>().text = $"答錯題數 : {wrong_count}";
            endImage.active = true;
            optionUIAnimationController.toggleOption();
            countButtonAnimationController.toggleMenu();
        }else{
            next();
        }
       
        
    }
}
