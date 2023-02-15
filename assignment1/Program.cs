
using System.Text;

Console.WriteLine("first commit vv");

public class ReadExpression
{
	public static int Evaluate(string ExpressionString)
	{
		char[] tokens = ExpressionString.ToCharArray();
		Stack<char> operation = new Stack<char>();
		Stack<int> values = new Stack<int>();
		for (int s = 0; s < tokens.Length; s++)
		{
			if (tokens[s] >= '0' && tokens[s] <= '9')
			{
				StringBuilder buffer = new StringBuilder();
				while (tokens[s] >= '0' && tokens[s] <= '9' && s < tokens.Length)
				{
					buffer.Append(tokens[s++]);
				}

				values.Push(int.Parse(buffer.ToString()));

			}
			else if (tokens[s] == '(')
			{
				operation.Push(tokens[s]);
			}
			else if (tokens[s] == ')')
			{
				while (operation.Peek() != '(')
				{
					values.Push(OperationUse(operation.Pop(), values.Pop()));
				}

				operation.Pop();
			}

			else if (tokens[s] == '+' || tokens[s] == '-' || tokens[s] == '*' || tokens[s] == '/')
			{
				while (operation.Count > 0 && Priority(tokens[s], operation.Peek()))
				{
					values.Push(OperationUse(operation.Pop(), values.Pop()));
				}

				operation.Push(tokens[s]);
			}
		}

		while (operation.Count > 0)
		{
			values.Push(OperationUse(operation.Pop(), values.Pop()));
		}

		return values.Pop();
	}
// write Priority & OperationUse