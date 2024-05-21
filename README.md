This is one of the classic puzzle on Ant movement.

An ant doesn't walk more than 10 steps in any particular direction.
If it finds any particle on its way it turns 90 degree from there.
Whenever ant decides to turn, It knows which turn(left or right) to take using a pattern "left-left-right" which it remembers. So it takes 1st turn as Left, 2nd turns as left, 3rd as right, 4th as again left and so on
This means, 1st turn will be a left, 2nd will be a left and 3rd will be a right.
It stops walking if it encounters any point on its path which it had travelled earlier.
Given the co-ordinates (in X-Y plane) of the particles, and assuming the ant starts its journey from (0,0) in North direction.

Write a program to find out where would the ant stop walking.
