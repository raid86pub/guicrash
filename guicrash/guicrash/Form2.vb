Public Class FM_CFG
        '让外界访问的以z为前缀
        Public Shared zFileName As String
        Public Shared zLines() As String

        Public Shared Sub Init()
                zFileName = Application.StartupPath & "\CFG.TXT"
                My.Computer.FileSystem.WriteAllText(zFileName, "", True)
                zLines = IO.File.ReadAllLines(zFileName)
        End Sub

        Private Sub BT_OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BT_OK.Click
                Dim sOut As String

                ReDim zLines(RTB_CFG.Lines.Length - 1)
                zLines = RTB_CFG.Lines

                'IO.File.Delete(zFileName)
                'IO.File.WriteAllLines(zFileName, zLines)

                sOut = ""
                For i = 0 To zLines.Length - 1
                        sOut &= zLines(i) & vbCrLf
                Next
                My.Computer.FileSystem.WriteAllText(zFileName, sOut, False)
                Me.Close()
        End Sub

        Private Sub FM_CFG_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
                Init()
                RTB_CFG.Lines = zLines
        End Sub

        Private Sub BT_CANCEL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BT_CANCEL.Click
                Me.Close()
        End Sub
End Class