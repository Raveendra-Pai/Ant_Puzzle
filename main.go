package main

import "fmt"

type Point struct {
	X int
	Y int
}

var DIRECTION_TO_MOVE = []string{"L", "L", "L"}

type MoveData struct {
	particles        map[Point]bool // particles list
	directionToMove  int            //to store the index of DIRECTION_TO_MOVE
	currentDirection string         //current Direction
	currentPoint     Point
	tracedPath       map[Point]bool //to store traversed points
}

func (mv *MoveData) MoveRight() {
	mv.currentPoint.X += 1
}

func (mv *MoveData) MoveLeft() {
	mv.currentPoint.X -= 1
}

func (mv *MoveData) MoveUp() {
	mv.currentPoint.Y += 1
}

func (mv *MoveData) MoveDown() {
	mv.currentPoint.Y -= 1
}

func GetDirection(currentDirection string, directionToMoveNext string) string {
	switch currentDirection {

	case "L":
		switch directionToMoveNext {
		case "L":
			return "D"
		case "R":
			return "U"
		}
	case "R":
		switch directionToMoveNext {
		case "L":
			return "U"
		case "R":
			return "D"
		}
	case "D":
		switch directionToMoveNext {
		case "L":
			return "R"
		case "R":
			return "L"
		}
	case "U":
		switch directionToMoveNext {
		case "L":
			return "L"
		case "R":
			return "R"
		}
	}
	panic("Invalid direction")
}

func Move(moveData MoveData) Point {
	stepCounter := 1
	for stepCounter <= 10 {
		if moveData.currentDirection == "U" {
			moveData.MoveUp()
		} else if moveData.currentDirection == "D" {
			moveData.MoveDown()

		} else if moveData.currentDirection == "L" {
			moveData.MoveLeft()

		} else if moveData.currentDirection == "R" {
			moveData.MoveRight()
		}
		fmt.Printf("(%d, %d)\r\n", moveData.currentPoint.X, moveData.currentPoint.Y)
		//return the point if its already traversed before
		if _, ok := moveData.tracedPath[moveData.currentPoint]; ok {
			return moveData.currentPoint
		} else {
			//Add it as the traversed point
			moveData.tracedPath[moveData.currentPoint] = true
		}

		//If the point found in the map, that means its colliding with particles, change the direction as per DIRECTION_TO_MOVE slice
		if _, ok := moveData.particles[moveData.currentPoint]; ok {
			moveData.currentDirection = GetDirection(moveData.currentDirection, DIRECTION_TO_MOVE[moveData.directionToMove])
			moveData.directionToMove = (moveData.directionToMove + 1) % len(DIRECTION_TO_MOVE)
			stepCounter = 1 //reset step counter
		} else {
			stepCounter += 1
		}
	}
	return moveData.currentPoint
}

func main() {

	//Storing the particles in the map to access it in O(1)
	particles := make(map[Point]bool)
	particles[Point{X: 0, Y: 5}] = true
	particles[Point{-5, 5}] = true
	particles[Point{-5, -2}] = true
	particles[Point{0, -2}] = true

	startingPoint := Point{X: 0, Y: 0}
	traceMap := make(map[Point]bool)
	traceMap[startingPoint] = true

	mvdata := MoveData{
		particles:        particles,
		directionToMove:  0,
		currentDirection: "U",
		currentPoint:     startingPoint,
		tracedPath:       traceMap,
	}

	result := Move(mvdata)
	fmt.Printf("Starting Point: (%v, %v), Final point: (%v, %v)",
		startingPoint.X, startingPoint.Y, result.X, result.Y)
}
