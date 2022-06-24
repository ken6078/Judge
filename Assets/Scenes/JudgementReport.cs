using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class JudgementReport : MonoBehaviour
{
    //劇本
    [SerializeField] TextAsset txt;
    //報告書內容
    [SerializeField] TextMeshProUGUI ReportText;
    // UI
    [SerializeField] GameObject NextPage;
    //要顯示的字&跳出來的速度
    [SerializeField] List<string> textList = new List<string>();
    [SerializeField] float typingSpeed = 0.001f;
    // Start is called before the first frame update
    void Start()
    {
        ReportText.text = "";
        textList = new List<string>(txt.text.Split('\n'));
        NextPage.SetActive(false);
        StartCoroutine(Type());
    }
    IEnumerator Type() {
        for (int index = 0; index < textList.Count; index++){
            if (textList[index] == ""){

            }else if(textList[index][0] == '#'){
                //等待使用者按下空白
                NextPage.SetActive(true);
                while(!Input.GetKeyDown(KeyCode.Space))
                    yield return null;
                ReportText.text = "";
                NextPage.SetActive(false);
                if ( index == textList.Count -1){}

            }else if (textList[index][1] == '-'){
                string name = textList[index].Substring(3, textList[index].IndexOf(":") - 3);
                string positiveAns = textList[index].Substring(textList[index].IndexOf(":")+1, textList[index].IndexOf("|") - textList[index].IndexOf(":")-1);
                string negitiveAns = textList[index].Substring(textList[index].IndexOf("|")+1);
                string showText = "\t" + name + ":" + positiveAns;
                showText = showText.Replace("\\t", "\t").Replace("\\n", "\n");
                Debug.Log(showText);
                foreach (char letter in showText.ToCharArray()) {
                    ReportText.text += letter;
                    yield return new WaitForSeconds(typingSpeed);
                }
            }else if (textList[index][1] == '='){
                string positiveAns = textList[index].Substring(3, textList[index].IndexOf("|") - 3);
                string negitiveAns = textList[index].Substring(textList[index].IndexOf("|")+1);
                string showText =  "\t" + positiveAns;
                showText = showText.Replace("\\t", "\t").Replace("\\n", "\n");
                Debug.Log(showText);
                foreach (char letter in showText.ToCharArray()) {
                    ReportText.text += letter;
                    yield return new WaitForSeconds(typingSpeed);
                }
            }else{
                foreach (char letter in textList[index].ToCharArray()) {
                    ReportText.text += letter;
                    yield return new WaitForSeconds(typingSpeed);
                }
            }
            ReportText.text += '\n';
        }
    }
}
