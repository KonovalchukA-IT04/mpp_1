using System;
using System.IO;

namespace go_to_app
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"..\..\..\text1.txt";
            StreamReader sr = new StreamReader(path);
            string @string = sr.ReadToEnd();
            sr.Close();
            Console.WriteLine("Input: " + @string);

            string[] the_words = new string[1000];
            int[] count_of_words = new int[1000];

            int arr_head_pointer = 0;
            string word = "";

            string cur_word = "";
            int cur_index = 0;
            bool cur_flag = true;

            bool zf = false;

            int i = 0;

        loopCondition:
            if (i < @string.Length) goto loopBody;
            goto loopEnd;

        loopCounter:
            i = i + 1;
            goto loopCondition;

        loopBody:

            cur_word = word;
            goto FindLoop;
        findLoopBack:

            if ((byte)@string[i] < 65 || (byte)@string[i] > 122)
            {
                if (i + 1 < @string.Length && (("" + @string[i] + @string[i + 1]) == "\r\n"))
                {
                    i = i + 1;
                }
                if (!cur_flag)
                {
                    if (word.Length > 3 && word != null && word != "")
                    {
                        the_words[arr_head_pointer] = word;
                        count_of_words[arr_head_pointer] = 1;
                        arr_head_pointer++;
                    }                    
                    word = "";
                }
                else
                {
                    count_of_words[cur_index] = count_of_words[cur_index] + 1;
                    word = "";
                }
            }
            else
            {
                if ((byte)@string[i] <= 90 && (byte)@string[i] >= 65)
                {
                    word += (char)((byte)@string[i] + 32);
                }
                else
                {
                    if((byte)@string[i] >= 65 && (byte)@string[i] <= 122)
                    {
                        word += @string[i];
                    }
                }
            }

            goto loopCounter;

            loopEnd:
            zf = true;
            cur_word = word;

            //find index loop
            FindLoop:
            int j = 0;
            cur_flag = false;
            loopCondition2:
            if (j < the_words.Length) goto loopBody2;
            goto loopEnd2;

            loopCounter2:
            j = j + 1;
            goto loopCondition2;

            loopBody2:

            if (cur_word == the_words[j])
            {
                cur_index = j;
                cur_flag = true;
                goto loopEnd2;
            }
            goto loopCounter2;

            loopEnd2:
            if (!zf)
            {
                goto findLoopBack;
            }         

            if (!cur_flag && word != null && word != "")
            {
                the_words[arr_head_pointer] = word;
                count_of_words[arr_head_pointer] = 1;
                word = "";
                arr_head_pointer++;
            }
            else
            {
                count_of_words[cur_index] = count_of_words[cur_index] + 1;
                word = "";
            }

            //first sort
            i = 0;
            j = 0;
            int tmp_int = 0;
            string tmp_str = "";


            loopConditionSort1:
            if (i < arr_head_pointer - 1) goto loopBodySort1;
            goto loopEndSort1;

            loopCounterSort1:
            i = i + 1;
            goto loopConditionSort1;

            loopBodySort1:
            // second loop

            loopConditionSort2:
            if (j < arr_head_pointer - i - 1) goto loopBodySort2;
            goto loopEndSort2;

            loopCounterSort2:
            j = j + 1;
            goto loopConditionSort2;

            loopBodySort2:
                
            if(count_of_words[j] < count_of_words[j + 1])
            {
                tmp_int = count_of_words[j];
                count_of_words[j] = count_of_words[j+1];
                count_of_words[j+1] = tmp_int;

                tmp_str = the_words[j];
                the_words[j] = the_words[j+1];
                the_words[j+1] = tmp_str;
            }

            goto loopCounterSort2;

            loopEndSort2:
            j = 0;
            
            goto loopCounterSort1;

            loopEndSort1:
            i = 0;

            //second sort
            i = 0;
            j = 0;
            tmp_int = 0;
            tmp_str = "";
            int local_index = 0;
            bool eql_mark = false;

            loopConditionSort3:
            if (i < arr_head_pointer - 1) goto loopBodySort3;
            goto loopEndSort3;

            loopCounterSort3:
            i = i + 1;
            goto loopConditionSort3;

            loopBodySort3:
            // second loop

            loopConditionSort4:
            if (j < arr_head_pointer - i - 1) goto loopBodySort4;
            goto loopEndSort4;

            loopCounterSort4:
            j = j + 1;
            goto loopConditionSort4;

            loopBodySort4:

            local_index = 0;
            ret_lable:
            if ((byte)(the_words[j][local_index]) == (byte)(the_words[j + 1][local_index]))
            {
                eql_mark = true;
            }
            if ((byte)(the_words[j][local_index]) > (byte)(the_words[j + 1][local_index]) && count_of_words[j] <= count_of_words[j + 1])
            {
                local_index = 0;

                tmp_int = count_of_words[j];
                count_of_words[j] = count_of_words[j + 1];
                count_of_words[j + 1] = tmp_int;

                tmp_str = the_words[j];
                the_words[j] = the_words[j + 1];
                the_words[j + 1] = tmp_str;
            }
            else
            {
                if (local_index + 1 < the_words[j].Length && local_index + 1 < the_words[j+1].Length && eql_mark)
                {
                    local_index = local_index + 1;
                    eql_mark = false;
                    goto ret_lable;
                }
            }
            goto loopCounterSort4;

            loopEndSort4:
            j = 0;

            goto loopCounterSort3;

            loopEndSort3:
            i = 0;

            //end sotr


            int k = 0;

            loopCondition3:
            if (k < arr_head_pointer) goto loopBody3;
            goto loopEnd3;

            loopCounter3:
            k = k + 1;
            goto loopCondition3;

            loopBody3:

            if (count_of_words[k] == 0)
            {
                goto loopEnd3;
            }
            Console.WriteLine(the_words[k] + " - " + count_of_words[k]);

            goto loopCounter3;

            loopEnd3:
            Console.WriteLine("END");
        }
    }
}
