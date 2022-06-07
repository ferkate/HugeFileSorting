namespace HugeFileSorting;

public class Row
{
	public int Id { get; set; }
	public string Text { get; set; }
	public int FileId { get; set; }

	public Row(int id, string text)
	{
		Id = id;
		Text = text;
	}

	public Row(int id, string text, int fileId = 0)
	{
		Id = id;
		Text = text;
		FileId = fileId;
	}
}