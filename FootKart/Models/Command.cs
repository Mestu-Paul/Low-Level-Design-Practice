namespace FoodKart.Models
{
    public class Command
    {
        public Command(string input)
        {
            Index = 0;
            Name = GetName(input);
            Parameters = GetParameters(input);
        }

        private List<string> GetParameters(string input)
        {
            var parameters = new List<string>();
            var parameter = "";
            for (; Index < input.Length; Index++)
            {
                if (input[Index] is ',' or ')')
                {
                    Index++;
                    parameters.Add(parameter.Trim());
                    parameter = "";
                }
                else parameter += input[Index];
            }
            return parameters;
        }

        private string GetName(string input)
        {
            var name = "";
            for (; Index < input.Length; Index++)
            {
                var x = input[Index];
                if (x == '(')
                {
                    Index++;
                    break;
                }
                name += x;
            }
            return name.Trim();
        }
        public string Name { get; private set; }
        public List<string>? Parameters { get; private set; }
        private int Index { get; set; }
    }
}
