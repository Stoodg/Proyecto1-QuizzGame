using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuizManager : MonoBehaviour
{
	public List<QuestionAndAnswers> QnA;
	public GameObject[] options;
	public int currentQuestion;
	
	public GameObject Quizpanel;
	public GameObject GoPanel;
	
	public UnityEngine.UI.Text QuestionTxt;
	
	public UnityEngine.UI.Text ScoreTxt;
	
	public UnityEngine.UI.Text PuntajeTxt;
	
	int	TotalQuestions=0;
	public int score;
	public int puntaje;
	
	private void Start()
	{
		TotalQuestions = QnA.Count;
		GoPanel.SetActive(false);
		generateQuestion();
		
	}
	
	public void	 retry()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
	
	
	void GameOver()
	{
		Quizpanel.SetActive(false);
		GoPanel.SetActive(true);
		ScoreTxt.text= score + "/" + TotalQuestions;
	}
	
	
	
	public void correct()
	{
		score +=1;
		puntaje = score;
		PuntajeTxt.text= puntaje + "/" + TotalQuestions;
		QnA.RemoveAt(currentQuestion);
		StartCoroutine(WaitForNext());
	}
	
	public void wrong()
	{
		QnA.RemoveAt(currentQuestion);
		StartCoroutine(WaitForNext());
		
		
	}
	
	
	IEnumerator WaitForNext()
	{
		yield return new WaitForSeconds(1);
		generateQuestion();
	}
	
	
	
	void SetAnswers()
	{
		for (int i = 0; i < options.Length; i++) 
		{
			options [i].GetComponent <Image>().color = options [i].GetComponent <AnswerScript>().startColor;
			options[i].GetComponent<AnswerScript>().isCorrect = false;
			options[i].transform.GetChild(0).GetComponent<UnityEngine.UI.Text>().text = QnA	[currentQuestion].Answers[i];
			
			if (QnA[currentQuestion].CorrectAnswer == i+1)
			{
				options[i].GetComponent<AnswerScript>().isCorrect = true;
			}
		}
	}
	
	
	
	void generateQuestion ()
	{
		if(QnA.Count > 0)
		{
			currentQuestion = Random.Range(0, QnA.Count);
			QuestionTxt.text= QnA[currentQuestion].Question;
			SetAnswers();
		}
		else
		{
			Debug.Log("No mas preguntas");
			GameOver();
			
		}
		
	}
	
}
