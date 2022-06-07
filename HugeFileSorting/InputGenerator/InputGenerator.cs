using System.Text;
using System.Text.RegularExpressions;

namespace InputGenerator;

public class InputGenerator
{
	public void GenerateInputFile(string fileName, long fileSize)
	{
		var bookPath = Path.Combine(Environment.CurrentDirectory, "cano.txt");
		var text = File.ReadAllLines(bookPath);
		text = text.Select(line => Regex.Replace(line, "\\p{P}+", "").Trim()).ToArray();


		using (var writer =
				new StreamWriter(new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None),
					Encoding.UTF8, 8192))
		{
			var random = new Random();
			var i = 0;
			while (writer.BaseStream.Length < fileSize)
			{
				var line = text[random.Next(text.Length)];
				var number = random.Next(text.Length);
				if (!string.IsNullOrEmpty(line))
				{
					var fileStr = string.Concat(number, ". ", line);
					writer.WriteLine(fileStr);
					if (i % 1000 == 0)
					{
						number = random.Next(text.Length);
						writer.WriteLine(string.Concat(number, ". ", line));
					}
				}
				i++;
			}
		}
	}
}