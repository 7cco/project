using System.Collections.Generic;
using NUnit.Framework;
using System.Text;

namespace TableParser;

[TestFixture]
public class QuotedFieldTaskTests
{
	[TestCase("''", 0, "", 2)]
	[TestCase("'a'", 0, "a", 3)]
	[TestCase("''", 0, "", 2)]
	[TestCase("'a'", 0, "a", 3)]
    [TestCase("'a\\''", 0, "a'", 5)]
    [TestCase("'a\\\"'", 0, "a\"", 5)]
    [TestCase("'a", 0, "a", 2)]
    [TestCase("\"a b\"", 0, "a b", 5)]

	public void Test(string line, int startIndex, string expectedValue, int expectedLength)
	{
		var actualToken = QuotedFieldTask.ReadQuotedField(line, startIndex);
		Assert.AreEqual(new Token(expectedValue, startIndex, expectedLength), actualToken);
	}

	// Добавьте свои тесты
}

class QuotedFieldTask
{
	public static Token ReadQuotedField(string line, int startIndex)
  {
    StringBuilder sb = new StringBuilder();
	int len=1;
	var result=sb.ToString();
	for (int i = startIndex + 1; i < line.Length; i++)
    {
		if (line[i] == line[startIndex])
		{
			len++;
			return new Token(result, startIndex, len);
		}
		if (line[i] == '\\')
		{
			sb.Append(line[i + 1]);
			i+=2;
			len+=2;
		}
		sb.Append(line[i]);
		len++;
    }
    return new Token(result, startIndex, len);
  }
}