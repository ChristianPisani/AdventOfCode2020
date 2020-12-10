using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AdventOfCode2020.Day5 {
    class Program {
        public static void Run()
        {
            var highest = 0;

            var seatList = new List<Seat>();

            foreach (var id in Day5.Input.Values)
            {
                var seat = ParseSeatID(id);

                if (seat.SeatID > highest) highest = seat.SeatID;

                seatList.Add(seat);
            }

            seatList.Sort((a,b) => a.SeatID - b.SeatID);

            for(int i = 1; i < seatList.Count - 1; i++)
            {
                var diff = Math.Abs(seatList[i - 1].SeatID - seatList[i + 1].SeatID);

                if (diff != 2)
                {
                    Console.WriteLine(seatList[i]);
                }
            }

            Console.WriteLine(highest);
            Console.ReadKey();
        }

        public static Seat ParseSeatID(string ID)
        {
            return GetPartitionRow(ID, 0, 127);
        }

        public static Seat GetPartitionRow(string ID, int sectionStart, int sectionEnd)
        {
            if (ID.Length <= 3)
            {
                return new Seat()
                {
                    BoardingID = ID,
                    Row = sectionStart,
                    Column = GetPartitionColumn(ID, 0, 7)
                };
            }
            else
            {
                var partition = ID.Substring(0, 1);

                ID = ID.Substring(1);

                var sectionLength = sectionEnd - sectionStart;

                switch (partition)
                {
                    case "F":
                        return GetPartitionRow(ID, sectionStart, sectionStart + (sectionLength / 2));

                    case "B":
                        return GetPartitionRow(ID, sectionStart + (int)Math.Ceiling(sectionLength / 2.0f), sectionEnd);
                }
            }

            return null;
        }

        public static int GetPartitionColumn(string ID, int sectionStart, int sectionEnd)
        {
            if (ID.Length <= 0)
            {
                return sectionStart;
            }
            else
            {
                var partition = ID.Substring(0, 1);

                ID = ID.Substring(1);

                var sectionLength = sectionEnd - sectionStart;

                switch (partition)
                {
                    case "L":
                        return GetPartitionColumn(ID, sectionStart, sectionStart + (sectionLength / 2));

                    case "R":
                        return GetPartitionColumn(ID, sectionStart + (int)Math.Ceiling(sectionLength / 2.0f), sectionEnd);
                }
            }

            return -1;
        }

        public class Seat {
            public string BoardingID;
            public int SeatID { get { return Row * 8 + Column; } }
            public int Column;
            public int Row;

            public override string ToString()
            {
                return $"ID: {BoardingID} SeatID: {SeatID} Column: {Column} Row: {Row}";
            }
        }
    }
}
