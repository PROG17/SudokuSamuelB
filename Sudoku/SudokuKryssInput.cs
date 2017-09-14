using System;
using System.IO;
using System.Text.RegularExpressions;

namespace Sudoku
{
    class SudokuKryssInput : ISudokuInput
    {
        public int[][] GetData(out string info)
        {
 
            var data = WebPageTools.ParseWebPage(@"http://www.sudokukryss.se/sudoku/?grade=4", str =>
            {
                int[][] gamePlan = new int[9][];
                string sudokuUrl = "";

                    //Match sudoku rows
                    string pattern = @"[<]tr.*?[<]td.*?[>](.+?)[<]/td[>]\s*[<]td.*?[>](.+?)[<]/td[>]\s*[<]td.*?[>](.+?)[<]/td[>]\s*[<]td.*?[>](.+?)[<]/td[>]\s*[<]td.*?[>](.+?)[<]/td[>]\s*[<]td.*?[>](.+?)[<]/td[>]\s*[<]td.*?[>](.+?)[<]/td[>]\s*[<]td.*?[>](.+?)[<]/td[>]\s*[<]td.*?[>](.+?)[<]/td[>]\s*";
                RegexOptions options = RegexOptions.IgnoreCase | RegexOptions.Singleline;
                MatchCollection matches;
                Regex optionRegex = new Regex(pattern, options);
                matches = optionRegex.Matches(str);
                    //Match url-source for this Sudoku                    
                    string patternUrl = @"skicka.*?vän.*?br/>\s*(.*?)</span";
                Regex optionRegexUrl = new Regex(patternUrl, options);
                Match matchUrl = optionRegexUrl.Match(str);
                sudokuUrl = matchUrl.Groups[1].Value;
                    //parse into gameplan
                    for (int i = 0; i < 9; i++)
                {
                    gamePlan[i] = new int[9];

                    for (int j = 1; j < 10; j++)
                    {
                        int num;
                        if (!int.TryParse(matches[i].Groups[j].Value, out num))
                            num = 0;
                        gamePlan[i][j - 1] = num;
                    }
                }

                return new { GamePlan = gamePlan, SudokuUrl = sudokuUrl };

            });

            info = data.SudokuUrl;
            return data.GamePlan;
        }
    }
}
