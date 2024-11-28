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

