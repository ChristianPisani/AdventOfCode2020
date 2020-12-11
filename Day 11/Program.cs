using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2020.Day11 {
    class Program {

        public static void Run()
        {
            var rows = Day11.Input.Values.Select(x => x.ToCharArray().Select(y => y.ToString()).ToList()).ToList();

            var prev = rows.Select(x => x.Select(y => y).ToList()).ToList();
            var changedRows = rows.Select(x => x.Select(y => y).ToList()).ToList();

            Draw(changedRows);

            bool changed = true;
            var count = 0;
            while (changed)
            {
                for (int x = 0; x < changedRows.Count; x++)
                {
                    for (int y = 0; y < changedRows[x].Count; y++)
                    {
                        changedRows[x][y] = ChangeRow2(x, y, prev.Select(s => s).ToList());
                    }
                }

                var localChanged = false;
                for (int x = 0; x < changedRows.Count; x++)
                {
                    for (int y = 0; y < changedRows[x].Count; y++)
                    {

                        if (!prev[x][y].Equals(changedRows[x][y]))
                        {
                            localChanged = true;
                            break;
                        }

                    }
                }

                changed = localChanged;

                prev = changedRows.Select(x => x.Select(y => y).ToList()).ToList();

                Draw(changedRows);

                count++;
            }

            Draw(changedRows);

            var seatsOccupied = 0;
            for (int x = 0; x < changedRows.Count; x++)
            {
                for (int y = 0; y < changedRows[x].Count; y++)
                {
                    if (changedRows[x][y] == "#") seatsOccupied++;
                }
            }

            Console.WriteLine(seatsOccupied);

            Console.ReadLine();
        }

        public static void Draw(List<List<string>> rows)
        {
            return;
            for (int x = 0; x < rows.Count; x++)
            {
                for (int y = 0; y < rows[x].Count; y++)
                {
                    Console.Write(rows[x][y]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public static string ChangeRow(int indexX, int indexY, List<List<string>> rows)
        {

            var occupied = 0;

            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    try
                    {
                        if (x == 0 && y == 0) continue;

                        if (rows[indexX + x][indexY + y] == "#") occupied++;

                    } catch (Exception e) { }

                }
            }

            if (rows[indexX][indexY] == "#")
            {
                if (occupied >= 4) return "L";
            }
            else if (rows[indexX][indexY] == "L")
            {
                if (occupied == 0) return "#";
            }

            return rows[indexX][indexY];
        }

        public static string ChangeRow2(int indexX, int indexY, List<List<string>> rows)
        {

            var occupied = 0;

            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    try
                    {
                        if (x == 0 && y == 0) continue;

                        if (rows[indexX + x][indexY + y] == "#")
                        {
                            occupied++;
                        } else
                        {
                            var foundSeat = false;

                            for(int i = 1; i < 100 && !foundSeat; i++)
                            {
                                if(rows[indexX + (i * x)][indexY + (i * y)] == "#")
                                {
                                    occupied++;
                                    foundSeat = true;
                                } else if (rows[indexX + (i * x)][indexY + (i * y)] == "L")
                                {
                                    foundSeat = true;
                                }
                            }
                        }

                    } catch (Exception e) { }
                }
            }

            if (rows[indexX][indexY] == "#")
            {
                if (occupied >= 5) return "L";
            }
            else if (rows[indexX][indexY] == "L")
            {
                if (occupied == 0) return "#";
            }

            return rows[indexX][indexY];
        }
    }
}
