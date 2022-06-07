using System.Text;

namespace HugeFileSorting;

public static class FileSorter
	{
		public static void SplitFileInSortedParts(string inputName, long bufferSize)
		{
			var batchList = new List<Row>();
			var fileIndex = 1;

			using (var fileStream = File.OpenRead(inputName))
			using (var reader = new StreamReader(fileStream, Encoding.UTF8))
			{
				string line;
				while ((line = reader.ReadLine()) != null)
				{
					if(reader.BaseStream.Position < bufferSize * fileIndex)
						batchList.Add(GetRow(line));
					else
					{
                        WriteToFile(batchList, fileIndex);
						fileIndex++;
						batchList.Clear();
					}
				}
                if(batchList.Any())
					WriteToFile(batchList, fileIndex);
			}
		}

		private static void WriteToFile(List<Row> batchList, int fileIndex)
		{
			var fileName = Path.Combine(Environment.CurrentDirectory, "sorted" + fileIndex + ".txt");

			using (var writer =
					new StreamWriter(
						new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None),
						Encoding.UTF8, 8192))
			{
				batchList.Sort(new RowComparer());
				foreach (var item in batchList)
					writer.WriteLine(item.Id + ". " + item.Text);
			}
		}

		public static void MergeSort(string outputFileName, bool saveTemporaryFiles)
		{
			var paths = Directory.GetFiles(Environment.CurrentDirectory, "sorted*.txt");
			var rowQueue = new PriorityQueue<Row, Row>(new RowComparer());

			var readers = new StreamReader[paths.Length];
			for (var i = 0; i < paths.Length; i++)
			{
				readers[i] = new StreamReader(paths[i], Encoding.UTF8);
				var firstLine = readers[i].ReadLine();
				var row = GetRow(firstLine, i);
				rowQueue.Enqueue(row, row);
			}

			var writer = new StreamWriter(outputFileName);

			while (rowQueue.Count > 0)
			{
				var minRow = rowQueue.Dequeue();
				writer.WriteLine(minRow.Id + ". " + minRow.Text);
				var nextLine = readers[minRow.FileId].ReadLine();
				if (!string.IsNullOrEmpty(nextLine))
				{
					var row = GetRow(nextLine, minRow.FileId);
					rowQueue.Enqueue(row, row);
				}
			}

			writer.Close();
			for (var i = 0; i < paths.Length; i++)
			{
				readers[i].Close();
				if (!saveTemporaryFiles)
					File.Delete(paths[i]);
			}
		}

		private static Row GetRow(string line, int fileId = 0)
		{
			var items = line.Split(". ");
			var res = int.TryParse(items.FirstOrDefault(), out var id);
			return new Row(id, items.LastOrDefault(), fileId);
		}
	}