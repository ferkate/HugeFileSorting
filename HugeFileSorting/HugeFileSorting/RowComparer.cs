namespace HugeFileSorting;

public class RowComparer : IComparer<Row>
{
	public int Compare(Row x, Row y)
	{
		var compare = string.Compare(x.Text, y.Text);
		if (compare == 0)
		{
			if (x.Id == y.Id)
				return 0;
			else
			{
				if (x.Id > y.Id)
					return 1;
				else
					return -1;
			}
		}
		return compare;
	}
}