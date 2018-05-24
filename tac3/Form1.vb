Public Class Form1
    Dim Pieces(2, 2) As Label
    Dim Turn As String = "0"
    Dim Results As String = "D:\Results.txt"
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Load_Pieces() ' Lodads the pieces when Form1 is open
    End Sub
    Sub Load_Pieces()
        'Adds the table where the game takes place
        For i = 0 To 2
            For i1 = 0 To 2
                Pieces(i1, i) = New Label
                Pieces(i1, i).Text = ""
                Pieces(i1, i).Location = New Point((i1 * 75) + 13, (i * 75) + 13)
                'Style
                Pieces(i1, i).Size = New Size(75, 75)
                Pieces(i1, i).BorderStyle = BorderStyle.FixedSingle
                Pieces(i1, i).Font = New Font("Arial", 50)
                AddHandler Pieces(i1, i).Click, AddressOf Piece_Clicked
                Me.Controls.Add(Pieces(i1, i))
                ' adding the pieces
            Next
        Next
    End Sub
    Sub Piece_Clicked(ByVal sender As Object, ByVal e As EventArgs)
        'Knows when it's being clicked
        Dim Label1 As Label = sender
        If Label1.Text = "" Then
            If Turn = "O" Then
                Label1.Text = "O"
                Turn = "X"

            ElseIf Turn = "X" Then
                Label1.Text = "X"
                Turn = "O"
            End If
            If Check_Diagonal("X") = "" And Check_Diagonal("O") = "" And Check_RowColumn() = "" Then
                Dim c As Integer = 0
                For i = 0 To 2
                    For i1 = 0 To 2
                        If Not Pieces(i, i1).Text = "" Then
                            c += 1 'Add 1 to the pieces being used (maximum of 3 for a win)
                        End If
                    Next
                Next
                If c = 9 Then 'If all 9 boxes are used game ends in a draw
                    GameOver("Draw")
                End If
            Else
                If Not Check_Diagonal("X") = "" Then
                    GameOver("X")
                ElseIf Not Check_Diagonal("O") = "" Then
                    GameOver("O")
                ElseIf Not Check_RowColumn() = "" Then
                    GameOver(Check_RowColumn)
                End If
            End If
        End If
        Label2.Text = Turn + " Turn"

    End Sub
    Sub GameOver(ByVal str As String)

        If str = "Draw" Then
            MsgBox("Draw")
        Else
            MsgBox(str + " Wins")
            Label3.Text = (str + " Wins")

            If System.IO.File.Exists(Results) = True Then

                Dim objWriter As New System.IO.StreamWriter(Results)

                objWriter.Write(Label3.Text)
                objWriter.Close()
                MessageBox.Show("End result written to file Results.txt")

            Else

                MessageBox.Show("File Does Not Exist Please Create Results.txt at D:\")

            End If
        End If
        Pieces_Reset("O")
    End Sub
    Sub Pieces_Reset(ByVal T As String)

        Turn = T
        For i = 0 To 2
            For i1 = 0 To 2
                Pieces(i, i1).Text = ""
            Next
        Next
    End Sub
    Function Check_RowColumn() As String

        Dim flag1 As String = ""
        For i = 0 To 2

            Dim O_Column As Integer = 0
            Dim O_Row As Integer = 0
            Dim X_Column As Integer = 0
            Dim X_Row As Integer = 0

            For i1 = 0 To 2

                If Pieces(i1, i).Text = "O" Then 'Checks for horizontal win
                    O_Column += 1
                ElseIf Pieces(i1, i).Text = "X" Then
                    X_Column += 1
                End If

                If Pieces(i, i1).Text = "O" Then 'Checks for downward win
                    O_Row += 1
                ElseIf Pieces(i, i1).Text = "X" Then
                    X_Row += 1
                End If

                If O_Column = 3 Or O_Row = 3 Then 'Checks for column win
                    flag1 = "O"
                ElseIf X_Column = 3 Or X_Row = 3 Then
                    flag1 = "X"
                End If
            Next

        Next
        Return flag1

    End Function
    Function Check_Diagonal(ByVal str As String) As String
        Dim flag1 As String = ""
        If Pieces(0, 0).Text = str And Pieces(1, 1).Text = str And Pieces(2, 2).Text = str Or Pieces(2, 0).Text = str And Pieces(1, 1).Text = str And Pieces(0, 2).Text = str Then
            flag1 = str
        End If
        Return flag1
    End Function
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If RadioButton1.Checked = True Then
            Pieces_Reset("O") 'Allows "O" to go first
        ElseIf RadioButton2.Checked = True Then
            Pieces_Reset("X") 'Allows "X" to go first
        End If
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Form2.Show()
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        End
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Pieces_Reset("") 'Resets the game
    End Sub
    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged

    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged

    End Sub
End Class