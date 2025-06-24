using System;
using System.Collections;
using UnityEngine;
using System.Threading.Tasks;

public class TaskTest : MonoBehaviour
{
    // 비동기 : 동기의 반대말로 어떤 '작업'을 실행할 때 그 작업이 완료 되지 않아도 다음 코드를 실행하는 방식
    // 그 작업의 특징 : 시간이 오래걸리는 작업 (EX) 연산량이 많거나, IO 작업 등)
    public GameObject target;
    private async void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("space");
            
            Task<int> task2 = new Task<int>(LongLoop2);
            task2.Start();
            int result = await task2;
            
            Debug.Log(result);
            // // LongLoop();
            // StartCoroutine(LongLoop_Coroutine());
            
            // // 비동기 :TASK
            //
            // Task task1 = new Task(LongLoop);
            // task1.Start();
            //
            //반환 값이 있는 task
             // task2.Start();
            // //
            // task2.Wait(); // => 비동기를 동기로 바꾸기 때문에 사용하지 않는다.
            // int resul = task2.Result;
            // Debug.Log(resul);
            // task2.ContinueWith((t) =>
            // {
            //     int result = t.Result;
            //     Debug.Log(result);
            // });
            
            // 비동기를 동기처럼 이해하기 쉽게 만드는 키워드가 async + await
 
        }
    }

    // 연산량이 많은 작업
    private void LongLoop()
    {
        long sum = 1;
        for (long i = 0; i < 1000000000; i++)
        {
            sum *= i;
        }
        Debug.Log("작업 완료"); 
        target.SetActive(false);
    }
    private int LongLoop2()
    {
        long sum = 1;
        for (long i = 0; i < 1000000000; i++)
        {
            sum *= i;
        }
        Debug.Log("작업 완료"); 

        return 12512522;
    }
    // 연산량이 많은 작업
    private IEnumerator LongLoop_Coroutine()
    {
        double sum = 1;
        for (double i = 1; i < 1000000000; i++)
        {
            sum *= i;
            if (i % 10000000 == 0)
            {
                Debug.Log(i);
                yield return null;
            }
        }
        Debug.Log($"결과는:{sum} ");
    }
    
    // 인터넷 IO 작업
    private void ServerIO()
    {
        // 2초 걸림
        
    }
}
