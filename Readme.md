# PokerLib
PokerLib is a C# library designed to determine the winner between a set of poker hands. 

PokerLib can take either a relative filepath or a string array as input and displays output in the console window.
PokerLib has implementation of Flush, 3 of a kind, pair, and high card hands, and is able to determine when there is a draw for any set of 5 card hands.

PokerClient is a simple client program that uses PokerLib to determine the hand winner. The implementation in PokerClient only takes the name of a .txt file as input.
Simply download the **bin** folder in this repository to gt both PokerClient and PokerLib.

Also included in the **bin** folder is the input.txt file, containing:
```
Joe
QD, 8D, KD, 7D, 3D
Bob
AS, QS, 8S, 6S, 4S
Sally
4S, 4H, 3H, QC, 8C
```
Input text can contain 1 - infinite hands, where each hand is in the order of:
```
NAME
HAND
```
Where any name is valid and all hands must be in the form of VALUE|SUIT and must have exactly 5 cards.
Any valid set of 5 card hands will be accepted. All cards **must be separated by a comma**, but the library will help if you run into formatting issues.

