2048 Bubble pop game done with Entitas

A simple bubble pop game demonstrating ECS architecture based on Entitas

![Screenshot 2020-07-24 at 13 47 45](https://user-images.githubusercontent.com/2452120/88388438-d5067900-cdb4-11ea-9328-d300e62c461e.png)

Features:
* Board which moves up and down depending of how many rows are present on the screen
* Bubble of the same number get merged on impact producing higher number of power of 2. More bubble involved into the merge higher power of 2 will be taken.
* Merge always go in direction of possible next automatic merge
* Bubbles clusters not connected to the top row will fall
* Simple particle effects, trails and animations are present
