# PokerLib
PokerLib is a C# library designed to determine the winner between a set of poker hands. 

PokerLib can take either a relative file path or a string array as input and displays output in the console window.
PokerLib can parse **all poker hands**: Royal Flush, Straight Flush, 4 of a kind, Full House, Flush, Straight, 3 of a kind, 2 pair, pair, and high card hands. PokerLib is also able to determine when there is a draw for any set of 5 card hands.

PokerClient is a simple client program that uses PokerLib to determine the hand winner. The implementation in PokerClient only takes the name of a .txt file as input.
Simply download the **bin** folder in this repository to get both PokerClient and PokerLib.

Also included in the **bin** folder is the input.txt file, containing:
```
Joe
QD, 8C, KD, 7D, 3D
Bob
AC, QS, 8S, 6S, 4S
Sally
4S, 4H, 3H, QC, 8C
Bill
6H, 3D, 9s, 9H, 8D
Tim
3C, 6S, 3C, 4S, 4H
McMann
3D, 5H, 3C, 4d, 4H
Lonny
10C, 9D, 8H, 7H, 6H
Homer
3D, 8H, jS, 4S, 5S
Pete
2S, AC, AH, 9C, 9H
```
Input text can contain 1 - infinite hands, where each hand is in the order of:
```
NAME
HAND
```
Where any name is valid, and all hands must be in the form of VALUE|SUIT and must have exactly 5 cards.
Any valid set of 5 card hands will be accepted. All cards **must be separated by a comma**, but the library will help if you run into formatting issues.

## Assumptions
The following are all assumptions used to code PokerLib. Note that some robustness has been built around these assumptions to help guide the user to create a correct input.
* All inputs must have an even number of lines (Name, Cards) repeating 1 or more times
* All hands consist of 5 cards
* All cards are separated by **exactly** one comma and 0 or more whitespace
* All cards accept lower or uppercase letters, and only certain values **(2, 3, 4, 5, 6, 7, 8, 9, 10, j|J, q|Q, k|K, a|A)** and suits **(s|S, c|C, h|H, d|D)** will make the program yield an output
* **Any poker hand is valid**, and all poker hands are evaluated in traditional 5 card poker priority
* Tiebreakers are determined with card value (Higher 3 of a kind winds, then next highest card, etc.)
* If tiebreaker doesn't resolve, a list of winners is produced

* Impossible hands (ex. repeating cards 4H, 4H, 4H, 3S, 8S) are acceptable inputs
* Duplicate values between hands (Sally and Bob both have a 3H) are acceptable inputs
