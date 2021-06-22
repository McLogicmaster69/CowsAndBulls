Imports System

Public Class Main

    '
    ' REQUIRED UI
    ' Function (Type) [Name] {Additional}
    '
    ' Play (Button)
    ' Quit (Button)
    ' User Attempt (Input field) {OPTIONAL Change maximum length to 4}
    ' Reset (Button) [Reset]
    ' Attempts (List) [Attempts]
    ' Cows (List) [CowsList]
    ' Bulls (List) [BullsList]
    '
    ' OPTIONAL UI
    '
    ' Timer (Label) [Clock Text]
    ' Timer (Timer) [Clock] {Set interval to 100}
    '
    ' Attempts (Label) [-]
    ' Cows (Label) [-]
    ' Bulls (Label) [-]
    '

    '
    ' Computer's random number that is going to be guessed
    '
    Dim RandomNumber As String
    '
    ' OPTIONAL Varibles for keeping track of time
    ' CAN REMOVE IF NOT INCLUDING THE CLOCK
    '
    Dim Seconds As Integer
    Dim MilSeconds As Integer
    Dim FirstPlay As Boolean = True

    '
    ' Initial Random Number
    '
    Private Sub Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        GenerateRandom()
    End Sub

    '
    ' Reset button
    '
    Private Sub Reset_Click(sender As Object, e As EventArgs) Handles Reset.Click
        ResetGame()
    End Sub

    '
    ' Play button
    '
    Private Sub Play_Click(sender As Object, e As EventArgs) Handles Play.Click
        RunPlay()
    End Sub

    '
    ' Quit button
    '
    Private Sub Quit_Click(sender As Object, e As EventArgs) Handles Quit.Click
        End
    End Sub

    '
    ' Resets the game
    '
    Private Sub ResetGame()

        ' Reset Clock
        ' CAN REMOVE IF NOT INCLUDING THE CLOCK
        Clock.Stop()
        ClockText.Text = "0.0"
        FirstPlay = True
        ' Generate New Random
        GenerateRandom()
        ' Clear input and lists
        UserInput.Text = ""
        Attempt.Items.Clear()
        CowsList.Items.Clear()
        BullsList.Items.Clear()

    End Sub

    '
    ' Validate the user input
    '
    Private Function ValidateInput(UserInput As String)

        If IsNumeric(UserInput) And UserInput.Length = 4 Then
            Dim Unique As Boolean = True
            For CurrentLetter = 0 To 2
                For CheckingLetter = CurrentLetter + 1 To 3
                    If UserInput(CurrentLetter) = UserInput(CheckingLetter) Then
                        Unique = False
                        Exit For
                    End If
                Next

                If Unique = False Then
                    Exit For
                End If

            Next
            Return Unique

        Else
            Return False
        End If

    End Function

    '
    ' Random Number Generation
    '
    Private Sub GenerateRandom()

        Dim FinalRand As String = ""
        Randomize()
        For i = 0 To 3

            Dim ConLoop As Boolean = True
            While ConLoop = True

                Dim NewRand As Integer = Math.Ceiling(Rnd() * 10) - 1
                Dim Unique As Boolean = True

                For Letter = 0 To FinalRand.Length - 1
                    If FinalRand(Letter) = Convert.ToString(NewRand) Then
                        Unique = False
                    End If
                Next

                If Unique = True Then
                    FinalRand += Convert.ToString(NewRand)
                    ConLoop = False
                End If

            End While
        Next

        RandomNumber = FinalRand

    End Sub

    Private Sub RunPlay()

        If ValidateInput(UserInput.Text) Then
            ' Start Clock
            ' CAN REMOVE IF NOT INCLUDING THE CLOCK
            If FirstPlay Then
                FirstPlay = False
                Seconds = 0
                MilSeconds = 0
                Clock.Start()
            End If

            Dim UI As String = UserInput.Text
            Dim Cows As Integer
            Dim Bulls As Integer
            For CurrentLetter = 0 To 3
                If UI(CurrentLetter) = RandomNumber(CurrentLetter) Then
                    Bulls += 1
                End If
            Next

            For UILetter = 0 To 3
                For RNDLetter = 0 To 3
                    If UI(UILetter) = RandomNumber(RNDLetter) Then
                        Cows += 1
                        Exit For
                    End If
                Next
            Next

            Cows -= Bulls
            CowsList.Items.Add(Cows)
            BullsList.Items.Add(Bulls)
            Attempt.Items.Add(UI)

            ' User wins
            If Bulls = 4 Then

                ' Stop the clock
                ' CAN REMOVE IF NOT INCLUDING THE CLOCK
                Clock.Stop()
                FirstPlay = True

                ' Win text
                MessageBox.Show("You did it! You completed it in " + Convert.ToString(Attempt.Items.Count) + " attempts! You completed it in " + Convert.ToString(Seconds) + "." + Convert.ToString(MilSeconds) + " seconds!")
                ' Generate new random number
                GenerateRandom()
                ' Clear input and lists
                UserInput.Text = ""
                Attempt.Items.Clear()
                CowsList.Items.Clear()
                BullsList.Items.Clear()

            End If
        Else
            MessageBox.Show("Please input a valid number")
        End If

    End Sub

    '
    ' Test if the user hit the ENTER key while in the textbox
    '
    Private Sub UserInput_KeyDown(sender As Object, e As KeyEventArgs) Handles UserInput.KeyDown
        If e.KeyCode = Keys.Enter Then
            RunPlay()
        End If
    End Sub

    '
    ' CAN REMOVE IF NOT INCLUDING THE CLOCK
    '
#Region Clock

    '
    ' Increment the timer at every 0.1 seconds
    '
    Private Sub Tick(sender As Object, e As EventArgs) Handles Clock.Tick

        MilSeconds += 1
        If MilSeconds = 10 Then
            Seconds += 1
            MilSeconds = 0
        End If
        ClockText.Text = Convert.ToString(Seconds) + "." + Convert.ToString(MilSeconds)

    End Sub

#End Region

End Class