using System;
using System.IO;

namespace go_to_app_2
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"..\..\..\text2.txt";
            StreamReader sr = new StreamReader(path);
            string @string = sr.ReadToEnd();
            sr.Close();

            int arr_length = 7000;
            string[] the_words = new string[arr_length];
            string[] page_nums = new string[arr_length];
            int[] last_page = new int[arr_length];
            int[] words_counter = new int[arr_length];

            int cur_page = 1;
            int rn_symb_counter = 0;

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
                    rn_symb_counter++;
                    if(rn_symb_counter > 45)
                    {
                        rn_symb_counter = 0;
                        cur_page++;
                    }
                }
                if (!cur_flag)
                {
                    if (word.Length > 3 && word != null && word != "")
                    {
                        the_words[arr_head_pointer] = word;
                        page_nums[arr_head_pointer] = "" + cur_page;
                        last_page[arr_head_pointer] = cur_page;
                        words_counter[arr_head_pointer] = words_counter[arr_head_pointer] + 1;
                        arr_head_pointer++;
                        if (arr_head_pointer >= arr_length)
                        {
                            goto loopEnd;
                        }
                    }
                    word = "";
                }
                else
                {
                    if(last_page[cur_index] != cur_page && words_counter[cur_index] <= 100)
                    {
                        page_nums[cur_index] = page_nums[cur_index] + ", " + cur_page;
                        last_page[cur_index] = cur_page;
                        words_counter[cur_index] = words_counter[cur_index] + 1;
                    }
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
                page_nums[arr_head_pointer] = "" + cur_page;
                word = "";
                arr_head_pointer++;
            }
            else
            {
                page_nums[cur_index] = page_nums[cur_index] + 1;
                word = "";
            }

            //second sort
            i = 0;
            j = 0;
            string tmp_int = "";
            string tmp_str = "";
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
            if ((byte)(the_words[j][local_index]) > (byte)(the_words[j + 1][local_index]))
            {
                local_index = 0;

                tmp_int = page_nums[j];
                page_nums[j] = page_nums[j + 1];
                page_nums[j + 1] = tmp_int;

                tmp_str = the_words[j];
                the_words[j] = the_words[j + 1];
                the_words[j + 1] = tmp_str;
            }
            else
            {
                if (local_index + 1 < the_words[j].Length && local_index + 1 < the_words[j + 1].Length && eql_mark)
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

            int stat = 0;

            int k = 0;

        loopCondition3:
            if (k < arr_head_pointer) goto loopBody3;
            goto loopEnd3;

        loopCounter3:
            k = k + 1;
            goto loopCondition3;

        loopBody3:

            if (page_nums[k] == "")
            {
                goto loopEnd3;
            }
            Console.WriteLine(the_words[k] + " - " + page_nums[k]);
            stat++;

            goto loopCounter3;

        loopEnd3:
            Console.WriteLine("END");
            Console.WriteLine((byte)'a'+ " "+ (byte)'z' + " " + (byte)'A' + " " + (byte)'Z' + "");
            Console.WriteLine($"Vocabulary: {stat} words");
        }
    }
}
