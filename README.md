# Ten Pin Bowling
Small code exercise to implement the score tracking of Ten Pin Bowling using [Traditional Scoring](https://en.wikipedia.org/wiki/Ten-pin_bowling#Traditional_scoring).

Not as simple as I would want it to be, but it's interesting and something to talk about.

My first thought was just to have a simple program with an array of ints that I would run over, but the prospect of testing that was annoying. Instead I isolated a small part that could be tested independently: the frame. I wanted each frame to be able to calculate it's own score and validate it's own rolls, hence the double linked list.

Each frame needs to know of it predecessors to calculate the subtotal and it might need it's descendants to calculate it's own score.

While the frame might look strange testing it was feels pretty straighforward. I don't need to do extra code to keep track of how far in the game I am or what round I am on. Hence why the game class becomes very simple. Expanding the game to a different amount of rounds simply requires a change to the constructor of the game class.

If I had to implement this in a distributed manner with databases and an API on top I would not want anything stateful, but in this case a CLI seemed like a more interesting way to implement it.