using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueField : MonoBehaviour
{
    public TMP_Text dialogueText;
    public List<string> dialogues;

    public float writingSpeed;
    private int index;
    private int charIndex;
    private bool started;
    private bool waitForNext;

    private void Awake()
    {
        dialogueText.text = string.Empty;
    }

    public void StartDialogue()
    {
        if (started)
            return;
        started = true;
        GetDialogue(0);
    }

    private void GetDialogue(int i)
    {
        index = i;
        charIndex = 0;
        dialogueText.text = string.Empty;
        StartCoroutine(Writing());
    }

    public void EndDialogue()
    {
        started = false;
        waitForNext = false;
        StopAllCoroutines();
        dialogueText.text = string.Empty;
    }

    IEnumerator Writing() // ������� �������� ��������� ����� �������
    {
        //�������, ������� ��������� ����������� ��������� ��������
        yield return new WaitForSeconds(writingSpeed);

        //������ ����� �������
        string currentDialogue = dialogues[index];
        //����� ������ �� ����� �����
        AudioManager2.instance.PlaySFX("char");
        dialogueText.text += currentDialogue[charIndex];
        //�������� ������ �����
        charIndex++;
        //�������� �� ����� �����������
        if (charIndex < currentDialogue.Length)
        {
            //������� ��� �� ��������� ������� 
            yield return new WaitForSeconds(writingSpeed);
            //���������� ������  //��� �������, ������� ����� ������������� ���������� � ������� ���������� Unity, � ����� ���������� ������ � ���� �����, �� ������� ������������, � ��������� �����.
            StartCoroutine(Writing());
        }
        else
        {
            //��������� �� ��������� ������
            waitForNext = true;
        }
    }

    private void Update()
    {

        if (!started)
            return;

        if (waitForNext && Input.GetKeyDown(KeyCode.E))
        {
            waitForNext = false;
            index++;

            if (index < dialogues.Count)
            {
                GetDialogue(index);
            }
            else
            {
                EndDialogue();
            }
        }
    }
}
