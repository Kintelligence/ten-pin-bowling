# Ten Pin Bowling
Small code exercise to implement the score tracking of Ten Pin Bowling using [Traditional Scoring](https://en.wikipedia.org/wiki/Ten-pin_bowling#Traditional_scoring).

Not as simple as I would want it to be, but it's interesting and something to talk about.

My first thought was just to have a simple program with an array of ints that I would run over, but the prospect of testing that was annoying. Instead I isolated a small part that could be tested independently: the frame. 

## The frame

I wanted each frame to be able to calculate it's own score and validate it's own rolls. For this, each frame would need to know about its surrounding frames. Each frame needs to know of it predecessors to calculate the subtotal and it might need it's descendants to calculate it's own score. Hence the double linked list.

When calculating the subtotal of a frame we need to know the scores of all preceding frames. But, those frames might need to know about future rolls. I needed some way to iterate over all past frames that are still unknown. And some way to trigger this.
This is combined in the calculation of each frames subtotal. Instead of asking every single frame every time if it has enough information to calculate it's own score or not. I simply ask the current frame to calculate it's subtotal. Which prompts it to ask its predecessor, thus going through all previous frames untill we hit a frame that is calculated, and see what else we can calculate from there on.

Consolidating the game rules down to what might apply to a single frame allows us to test it in isolation. But also allows the game to be simple.

## The game

As the frame carries most of the rules, the game itself becomes very simple. We don't need to do extra code to keep track of how far in the game we are or what the current round is. 
The game itself only keeps track of the current frame, and ends once it realizes it has reached the bonus frames. The bonus frames themselves are only displayed if they are necessary to calculate the final score.

The game class becomes very simple. Expanding the game to a different amount of rounds simply requires a change to the constructor of the game class.

## Alternatives

If I had to implement this in a distributed manner with databases and an API on top I would not want anything stateful, rather consider a simple POCO/DTO to model the state and some controllers with logic to validate and change the state. Perhaps having individual endpoints like:

- POST bowling/game/4/frame/5/roll/1
  - Allowing rolls to be updated. 
- GET bowling/game/4/score
  - For getting a current total score
- GET bowling/game/4/frame/4/score
  - for getting the scor of an individual frame. 

A CLI seemed like a more interesting way to implement it.