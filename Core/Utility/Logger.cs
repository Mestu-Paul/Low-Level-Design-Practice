namespace Core.Utility
{
    public static class Logger
    {
        public static void Info(string message)
        {
            Console.WriteLine(message);
        }

        public static void Warn(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Info(message);
            Console.ResetColor();
        }

        public static void Success(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Info(message);
            Console.ResetColor();
        }
        public static void Error(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Info(message);
            Console.ResetColor();
        }

        public static void WriteLine(List<string> tokens, List<int> lengths)
        {
            if (tokens.Count != lengths.Count)
                throw new Exception($"Size of tokens {tokens.Count} and their lengths {lengths.Count} are not same");

            Console.Write("|");
            for (int i = 0; i < tokens.Count; i++)
            {
                Console.Write(GetPadded(tokens[i], lengths[i]));
                Console.Write("|");
            }
            Console.WriteLine();
        }

        private static string GetPadded(string text, int length)
        {
            if (text.Length > length) throw new Exception($"Can not add padding {length} to text {text}");
            text = " " + text + " ";
            while (text.Length < length + 2)
            {
                text += " ";
            }

            return text;
        }

        public static void WriteTable(List<string> headers, List<List<string>> data)
        {
            var lengths = new List<int>();
            for (int i = 0; i < headers.Count; i++)
            {
                lengths.Add(headers[i].Length);
            }

            for (int j = 0; j < data.Count; j++)
            {
                var d = data[j];
                if (d.Count > headers.Count)
                    throw new Exception($"Over items than headers in {j}th row");

                for (int i = 0; i < d.Count; i++)
                {
                    lengths[i] = Math.Max(lengths[i], d[i].Length);
                }
            }

            var hrLine = "+";
            foreach (var length in lengths)
            {
                for (int i = -1; i <= length; i++)
                    hrLine += "-";
                hrLine += "+";
            }

            Info(hrLine);
            WriteLine(headers, lengths);
            Info(hrLine);
            foreach (var d in data)
            {
                WriteLine(d, lengths);
                Info(hrLine);
            }
        }
    }
}
