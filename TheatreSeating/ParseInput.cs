using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TheatreSeating.Models;

namespace TheatreSeating
{
    static class ParseInput
    {
        public static string ParseInputData(string input)
        {
            string[] parsedInput = input.Split(new string[] { "\r\n", "\r", "\n" },StringSplitOptions.None); //split the input based on new line
            bool isTheatrelayoutCompleted = false; //flag to tell whether theatre layout is parsed
            List<Row> rows = new List<Row>();
            StringBuilder output = new StringBuilder();
            int rowNum = 1;
            int totSeats = 0;

            foreach (string str in parsedInput)
            {
                if (!isTheatrelayoutCompleted && string.IsNullOrWhiteSpace(str))
                {
                    isTheatrelayoutCompleted = true;
                    continue;
                }

                string[] parseLayout = str.Split(' ');

                if (!isTheatrelayoutCompleted)
                {
                    rows.Add(new Row(rowNum, parseLayout));
                    rowNum += 1;
                    totSeats += parseLayout.Sum(x=>int.Parse(x));
                }
                else
                {
                    //Theatre layout is completed, now fill the seats
                    if (int.Parse(parseLayout[1]) > totSeats)
                    {
                        output.Append(parseLayout[0] + " Sorry, we can't handle your party.\n");
                    }
                    else
                    {
                        string result = string.Empty;

                        foreach (Row row in rows)
                        {
                            result = row.CheckAvailability(int.Parse(parseLayout[1]));

                            if (!string.IsNullOrWhiteSpace(result))
                            {
                                totSeats -= int.Parse(parseLayout[1]);
                                output.Append(parseLayout[0] + result);
                                break;
                            }
                        }

                        if (string.IsNullOrWhiteSpace(result))
                        {
                            output.Append(parseLayout[0] + " Call to split party.\n");
                        }
                    }
                }
            }

            return output.ToString();
        }
    }
}
