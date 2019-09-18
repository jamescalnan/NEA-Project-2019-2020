﻿Module Insertion_Sort_Algorithm
    Function insertionsortR(ByRef A As List(Of Double), ByVal n As Integer)
        'initial call would be insertionSortR(A, length(A)-1) 
        If n > 0 Then
            insertionsortR(A, n - 1)
            Dim x As Double = A(n)
            Dim j As Integer = n - 1
            While j >= 0 AndAlso A(j) > x
                A(j + 1) = A(j)
                j -= 1
            End While
            A(j + 1) = x
        End If
    End Function
    Function insertionsortI(ByVal A As List(Of Double))
        Dim i As Integer = 1
        While i < A.Count
            Dim j = i
            While j > 0 AndAlso A(j - 1) > A(j)
                swap(A, j, j - 1)
                j -= 1

            End While
            AnimateSort(A, 1)
            i += 1
        End While
        Return A
    End Function
End Module