using System.Diagnostics;
using HugeFileSorting;

const int SizeGB = 1073741824;
const int hundredMb = 104857600;
const int tenMb = 10485760;

var envArgs = Environment.GetCommandLineArgs();
var fileInputName = envArgs[1];
var fileOutputName = envArgs[2];

long bufferSize = tenMb;
if (envArgs.Length > 3 && !long.TryParse(envArgs[3], out bufferSize))
	bufferSize = tenMb;

Console.WriteLine("Start to split and sort into temporary files");

var watch = new Stopwatch();
watch.Start();

FileSorter.SplitFileInSortedParts(fileInputName, bufferSize);

Console.WriteLine(watch.Elapsed);
Console.WriteLine("Finished to split and sort into temporary files");
Console.WriteLine("Start to merge and sort into output file");
var saveTemporaryFiles = false;
if (envArgs.Length > 4 && !bool.TryParse(envArgs[4], out saveTemporaryFiles))
	saveTemporaryFiles = false;

FileSorter.MergeSort(fileOutputName,saveTemporaryFiles);
Console.WriteLine(watch.Elapsed);
watch.Stop();
Console.WriteLine("File was successfully sorted");