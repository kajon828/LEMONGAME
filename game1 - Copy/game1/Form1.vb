Public Class hehelemonlevel1
    Private Sub hehelemonlevel1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        PictureBox1.Visible = True

    End Sub

    Dim score As Integer
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        chase(LemonEnemy)
        chase(Lemon2Enemy)
        follow(limefriend)
        follow2(limefriend2)
        follow3(limefriend3)
        move(LimeWIN, 0, 0)
    End Sub
    Sub move(p As PictureBox, x As Integer, y As Integer)
        p.Location = New Point(p.Location.X + x, p.Location.Y + y)
    End Sub

    Private Sub Form1_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If hehe.Visible Then
            Exit Sub
        End If
        Select Case e.KeyCode
            Case Keys.Up
                MoveTo(Lemon, 0, -5)
            Case Keys.Down
                MoveTo(Lemon, 0, 5)
            Case Keys.Left
                MoveTo(Lemon, -5, 0)
            Case Keys.Right
                MoveTo(Lemon, 5, 0)
            Case Keys.W
                uplemonn.Location = Lemon.Location
                uplemonn.Visible = True
                Timerup.Enabled = True
            Case Keys.S
                downlemonn.Location = Lemon.Location
                downlemonn.Visible = True
                Timerdown.Enabled = True
            Case Keys.R
                Lemon.Image.RotateFlip(RotateFlipType.Rotate90FlipNone)
                Me.Refresh()

        End Select
    End Sub
    Sub follow(p As PictureBox)
        Static headstart As Integer
        Static c As New Collection
        c.Add(Lemon.Location)
        headstart = headstart + 5
        If headstart > 10 Then
            p.Location = c.Item(1)
            c.Remove(1)
        End If
    End Sub

    Sub follow2(p As PictureBox)
        Static headstart As Integer
        Static c As New Collection
        c.Add(Lemon.Location)
        headstart = headstart + 3
        If headstart > 10 Then
            p.Location = c.Item(1)
            c.Remove(1)
        End If
    End Sub

    Sub follow3(p As PictureBox)
        Static headstart As Integer
        Static c As New Collection
        c.Add(Lemon.Location)
        headstart = headstart + 2
        If headstart > 10 Then
            p.Location = c.Item(1)
            c.Remove(1)
        End If
    End Sub

    Public Sub chase(p As PictureBox)
        Dim x, y As Integer
        If p.Location.X > Lemon.Location.X Then
            x = -6
        Else
            x = 6
        End If
        MoveTo(p, x, 0)
        If p.Location.Y < Lemon.Location.Y Then
            y = 6
        Else
            y = -6
        End If
        MoveTo(p, x, y)
    End Sub



    Function Collision(p As PictureBox, t As String, Optional ByRef other As Object = vbNull)
        Dim col As Boolean

        For Each c In Controls
            Dim obj As Control
            obj = c
            If obj.Visible AndAlso p.Bounds.IntersectsWith(obj.Bounds) And obj.Name.ToUpper.Contains(t.ToUpper) Then
                col = True
                other = obj
            End If
        Next
        Return col
    End Function
    'Return true or false if moving to the new location is clear of objects ending with t
    Function IsClear(p As PictureBox, distx As Integer, disty As Integer, t As String) As Boolean
        Dim b As Boolean

        p.Location += New Point(distx, disty)
        b = Not Collision(p, t)
        p.Location -= New Point(distx, disty)
        Return b
    End Function

    'Moves and object (won't move onto objects containing  "wall" and shows green if object ends with "win"
    Sub MoveTo(p As PictureBox, distx As Integer, disty As Integer)
        If IsClear(p, distx, disty, "WALL") Then
            p.Location += New Point(distx, disty)
        End If

        If p.Name = "Lemon" And Collision(p, "WIN") Then
            score = score + 1
            LimeWIN.Visible = Not LimeWIN.Visible
            limefriend.Visible = True
            Return
        ElseIf p.Name = "Lemon" And Collision(p, "Enemy") Then
            score = score - 1
            inv = 20
            Timer2.Enabled = True

            LimeWIN.Visible = True
            limefriend.Visible = False
        ElseIf p.Name = "Lemon" And Collision(p, "NIW") Then
            score = score + 1
            LimeNIW.Visible = Not LimeNIW.Visible
            limefriend2.Visible = True
        ElseIf p.Name = "Lemon" And Collision(p, "INW") Then
            score = score + 1
            LimeINW.Visible = Not LimeINW.Visible
            limefriend3.Visible = True
        End If

        Label1.Text = score

        If p.Name = "uplemonn" And Collision(p, "2Enemy") Or Collision(p, "wall") Then
            Lemon2Enemy.Visible = False
            uplemonn.Visible = False
        ElseIf p.Name = "uplemonn" And Collision(p, "Enemy") Or Collision(p, "wall") Then
            LemonEnemy.Visible = False
            uplemonn.Visible = False
        ElseIf p.Name = "downlemonn" And Collision(p, "2Enemy") Or Collision(p, "wall") Then
            Lemon2Enemy.Visible = False
            downlemonn.Visible = False
        ElseIf p.Name = "downlemonn" And Collision(p, "Enemy") Or Collision(p, "wall") Then
            LemonEnemy.Visible = False
            downlemonn.Visible = False
        End If


        If limefriend.Visible = True And limefriend2.Visible = True And limefriend3.Visible = True Then
            Dim z As New Form2
            Me.Visible = False
            z.ShowDialog()
            Me.Visible = True
        End If

    End Sub

    Private Sub Timerup_Tick(sender As Object, e As EventArgs) Handles Timerup.Tick
        MoveTo(uplemonn, 0, -5)
    End Sub

    Private Sub Timerdown_Tick(sender As Object, e As EventArgs) Handles Timerup.Tick
        MoveTo(downlemonn, 0, 5)
    End Sub
    Private Sub Timerleft_Tick(sender As Object, e As EventArgs) Handles Timerup.Tick
        MoveTo(downlemonn, -5, 0)
    End Sub

    Private Sub Timerright_Tick(sender As Object, e As EventArgs) Handles Timerup.Tick
        MoveTo(downlemonn, 5, 0)
    End Sub

    Dim inv As Integer
    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        inv = inv - 1
        If inv > 0 Then
            PictureBox2.Location = Lemon.Location
            PictureBox2.Visible = Not PictureBox2.Visible
        Else
            PictureBox2.Visible = False
            Timer2.Enabled = False
            inv = 0
        End If

    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        PictureBox1.Visible = False
    End Sub
End Class
