# HugeFileSorting
Consits of two parts: InputGenerator and HugeFileSorting

InputGenerator will generate input file from book about Sherlock Homes with size you prefare

```
./InputGenerator <inputFilePath> 209715200(size in bytes)
```
Result will look like:

```
34567. his body was discovered One fact which has not been explained is the
50782. speak in anxious words But I have greater trust in your judgment
65722. to prove that It struck me as rather ingenious because it might be
2175. are old schoolfellows as you must have much to talk over Mr
```
HugeFileSorting takes input file in format Number. String (like InputGenerator provide) and sorts it by string and than by number

```
./HugeFileSorting <inputFilePath> <outputFilePath> 104857600(buffer size in bytes) true(optional save tenporary files or not)
```
In result you will get sorted file like:
```
75773. A beard A beard The man has a beard
75791. A beard A beard The man has a beard
462. a beard called with a note for Godfrey He had not gone to bed and
656. a beard called with a note for Godfrey He had not gone to bed and
```
