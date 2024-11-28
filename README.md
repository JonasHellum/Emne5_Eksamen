# Emne 5 eksamen

## Search
My search functionality is built from two methods: Find() and Search(). <br/>

The Find() method handles the core search algorithm, while the Search() method uses Find() to locate the searched value within a list of Messier objects. <br/>

The Find() is a linear search algorythm. <br/>

Here’s how Find() works: It first converts both the field and the search value to<br/>
lowercase to perform a case-insensitive comparison. The outer loop loops over starting positions<br/>
in the field and moves to the next position if the inner loop doesn’t find a match. <br/>
The inner loop checks characters from the current position of outer loop index against the characters in the search value, <br/>
if the first index is a match it continues to checking the subsequent characters. <br/>
If all characters match the method exits and returns true. However if a mismatch occurs the inner loop breaks, <br/>
and the outer loop moves to the next position and inner loop starts searching for a match again. <br/>

The Search() loops through the list of Messier objects, using the Find() method on each relevant field to locate matches. <br/>
When a match is found, the corresponding Messier object is added to a new list. <br/>
After processing all objects, the method returns this list containing all matching results. <br/>

## Sort
The sorting functionality in my project is implemented using a custom selection sort algorithm.  <br/>

Here’s how the Sort() method works: It firsts validates if the provided field is valid using IsValidField() <br/>
which checks if the field matches something inside the method. <br/>

Then it sends the list of Messier objects to SelectionSort(), the outer loop loops over starting positions<br/>
and moves to the next position if the inner loop doesn’t find a match. <br/>
Inner loop loops over the values to find the correct value to place at i, <br/>
it uses bool and ShouldSwap() to find out if it should be swapped, if they should be swapped it <br/>
changes the position of index minOrMax to current value then checks if  position of index i and minOrMax <br/>
is not the same it swaps the position. <br/>

ShouldSwap() takes in the current value of the outer loop and the value of the inner loop, <br/>
get’s the values from field using GetFieldValue() and uses CompareValues() to compare them, <br/>
sends back true or false depending on which order the values should be in also depending on descending or ascending. <br/>
 

CompareValues() compares two strings taking into account both alphabetical and numerical <br/>
characters through the ExtractNumericPart() and ExtractAlphabeticalPart() which uses regex to parse the values. <br/>

The Sort() method sorts a list of Messier objects based on the field, as an example: “messier catalogue number” <br/>
returns a list which is sorted with messier catalogue number and can decide if it’s ascending or descending. <br/>
