# Pattern Matching Example

Simple example using several different types of vehicles and how toll is calculated based on certain properties. The most relevant code is found in the `Calculators.cs`, all else is just to set up the scenario.

## If-Else Tree

`PeakTimePremiumIfElse(DateTime timeOfToll, bool inbound)`  
I couldn't resist a little pattern matching in the first `if statement` but from there it's a genuine If-Else tree.

## Pattern Matching

`PeakTimePremium(DateTime timeOfToll, bool inbound)`  
~40 lines of If-Else seemingly condensed into 5 lines.

>Do notice that the pattern matching approach needed an enum and several other switch expressions to set this up so the net difference in number of lines is probably not very large but it sure is a lot more readable and maintainable.
