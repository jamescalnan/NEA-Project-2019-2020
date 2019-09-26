﻿Module HuntAndKill
    Function HuntAndKillRefactored(limits() As Integer, delay As Integer, showMazeGeneration As Boolean)
        Dim currentCell As Cell = PickRandomStartingCell(limits) '(Limits(0) + 3, Limits(1) + 2)
        Dim r As New Random
        Dim visitedCells As Dictionary(Of Cell, Boolean) = InitialiseVisited(limits)
        Dim totalcellcount As Integer = visitedCells.Count
        visitedCells(currentCell) = True
        Dim returnablePath As New List(Of Node)
        Dim usedCellPositions = 1
        SetBoth(ConsoleColor.White)
        returnablePath.Add(New Node(currentCell.X, currentCell.Y))
        If showMazeGeneration Then currentCell.Print("██")
        Dim stopwatch As Stopwatch = Stopwatch.StartNew()
        While usedCellPositions <> totalcellcount
            If ExitCase() Then Return Nothing
            Dim recentCells As List(Of Cell) = Neighbour(currentCell, visitedCells, limits, True)
            If recentCells.Count > 0 Then
                Dim temporaryCell As Cell = recentCells(r.Next(0, recentCells.Count))
                Dim wallCell As Cell = MidPoint(currentCell, temporaryCell)
                currentCell = temporaryCell
                returnablePath.Add(New Node(currentCell.X, currentCell.Y))
                returnablePath.Add(New Node(wallCell.X, wallCell.Y))
                usedCellPositions += 1
                recentCells.Clear()
                If showMazeGeneration Then
                    SetBoth(ConsoleColor.White)
                    wallCell.Print("██")
                    currentCell.Print("██")
                End If
                visitedCells(currentCell) = True
            Else
                Dim cellFound = False
                For y = limits(1) To limits(3) Step 2
                    For x = limits(0) + 3 To limits(2) - 1 Step 4
                        If showMazeGeneration Then
                            SetBoth(ConsoleColor.DarkCyan)
                            Console.SetCursorPosition(x, y)
                            Console.Write("██")
                            If x + 2 < limits(2) - 1 Then
                                Console.SetCursorPosition(x + 2, y)
                                Console.Write("██")
                            End If
                        End If
                        Dim adjancencyList As Integer() = AdjacentCheck(New Cell(x, y), visitedCells)
                        Dim pathCell As Cell = PickAdjancentCell(New Cell(x, y), adjancencyList)
                        If Not IsNothing(pathCell) Then
                            Dim wallCell As Cell = MidPoint(pathCell, New Cell(x, y))
                            currentCell = New Cell(x, y)
                            If showMazeGeneration Then
                                SetBoth(ConsoleColor.White)
                                wallCell.Print("██")
                                currentCell.Print("██")
                                EraseLineHaK(limits, x + 1, returnablePath, y)
                            End If
                            usedCellPositions += 1
                            returnablePath.Add(New Node(currentCell.X, currentCell.Y))
                            returnablePath.Add(New Node(wallCell.X, wallCell.Y))
                            cellFound = True
                            visitedCells(currentCell) = True
                            Exit For
                        End If
                    Next
                    If showMazeGeneration Then
                        Threading.Thread.Sleep(delay)
                        EraseLineHaK(limits, limits(2), returnablePath, y)
                    End If
                    If cellFound Then Exit For
                Next
            End If
            Threading.Thread.Sleep(delay)
        End While
        PrintMessageMiddle($"Time taken to generate the maze: {stopwatch.Elapsed.TotalSeconds}", 1, ConsoleColor.Yellow)
        'EliminateDeadEnds(ReturnablePath)
        If Not showMazeGeneration Then
            SetBoth(ConsoleColor.White)
            PrintMazeHorizontally(returnablePath, limits(2), limits(3))
        End If
        Dim ypos As Integer = Console.CursorTop
        AddStartAndEnd(returnablePath, limits, 0)
        Console.SetCursorPosition(0, ypos)
        Return returnablePath
    End Function
End Module