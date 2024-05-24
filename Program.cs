using Ant_puzzle;
using System.Runtime.Intrinsics.X86;
using System;
using System.Reflection.Metadata.Ecma335;

namespace Ant_puzzle
{

    enum Direction
    {
        UP,
        DOWN,
        LEFT,
        RIGHT
    }

    public struct Point
    {
       internal int X;
       internal int Y;
    }


    class AntMovement
    {
        readonly Direction[] DIRECTION_TO_MOVE = [Direction.LEFT, Direction.LEFT, Direction.LEFT];
        readonly HashSet<Point> m_Particles;
        int m_directionIndexToMove = 0;
        Direction m_current_Direction;
        Point m_currentPoint;
        HashSet<Point> m_TracedPath = new HashSet<Point>();
        const int ONE_STEP = 1;

        public AntMovement(Point currentPoint, Direction currentDirection, HashSet<Point> particles)
        {
            this.m_currentPoint = currentPoint;
            this.m_current_Direction = currentDirection;
            this.m_Particles = particles ;
        }

        private void MoveRight()
        {
            m_currentPoint.X += ONE_STEP;
        }

        private void MoveLeft()
        {
            m_currentPoint.X -= ONE_STEP;
        }

        private void MoveUp()
        {
            m_currentPoint.Y += ONE_STEP;
        }

        private void MoveDown()
        {
            m_currentPoint.Y -= ONE_STEP;
        }

        private Direction GetDirection(Direction currentDirection, Direction directionToMoveNext)
        {
            switch (currentDirection)
            {
                case Direction.LEFT:
                        switch (directionToMoveNext)
                        {
                            case Direction.LEFT:
                                return Direction.DOWN;

                            case Direction.RIGHT:
                                return Direction.UP;

                        }
                        break;

                case Direction.RIGHT:
                        switch (directionToMoveNext)
                        {
                            case Direction.LEFT:
                                return Direction.UP;

                            case Direction.RIGHT:
                                return Direction.DOWN;

                        }
                        break;

                case Direction.DOWN:
                        switch (directionToMoveNext)
                        {
                            case Direction.LEFT:
                                return Direction.RIGHT;

                            case Direction.RIGHT:
                                return Direction.LEFT;

                        }
                        break;

                case Direction.UP:
                        switch (directionToMoveNext)
                        {
                            case Direction.LEFT:
                                return Direction.LEFT;

                            case Direction.RIGHT:
                                return Direction.RIGHT;

                        }
                        break;
            }
            throw new Exception("Invalid direction");
        }


        public Point Move()
        {
            //Add the current point as traced
            m_TracedPath.Add(m_currentPoint);

            int stepCounter = 1;
            while (stepCounter <= 10)
            {
                switch (m_current_Direction)
                {
                    case Direction.UP:
                        this.MoveUp();
                        break;
                    case Direction.DOWN:
                        this.MoveDown();
                        break;
                    case Direction.LEFT:
                        this.MoveLeft();
                        break;
                    case Direction.RIGHT:
                        this.MoveRight();
                        break;
                }


                Console.WriteLine("({0}, {1})", m_currentPoint.X, m_currentPoint.Y);

                //return the point if its already traversed before
                if (m_TracedPath.Contains(m_currentPoint))
                {
                    return m_currentPoint;
                }
                else
                {
                    //Add it as the traversed point
                    m_TracedPath.Add(m_currentPoint);
                }

                //If the point found in the map, that means its colliding with particles, change the direction as per DIRECTION_TO_MOVE slice
                if (m_Particles.Contains(m_currentPoint))
                {
                    m_current_Direction = GetDirection(m_current_Direction, DIRECTION_TO_MOVE[m_directionIndexToMove]);
                    m_directionIndexToMove = (m_directionIndexToMove + 1) % DIRECTION_TO_MOVE.Length;
                    stepCounter = 1;
                }
                else
                {
                    stepCounter += 1;
                }
            }
            return m_currentPoint;
        }

    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            var particles = new HashSet<Point>();
            particles.Add(new Point { X = 0, Y = 5 });
            particles.Add(new Point { X = -5, Y = 5 });
            particles.Add(new Point { X = -5, Y = -2 });
            particles.Add(new Point { X = 0, Y = -2 });

            Point current = new Point { X = 0, Y = 0 };


            var antMovement = new AntMovement(current, Direction.UP, particles);
            var result = antMovement.Move();

            Console.WriteLine("Ant starting point:  ({0},{1}) \n\r EndPoint: ({2}, {3})",
                current.X, current.Y, result.X, result.Y);

        }
    }
}
