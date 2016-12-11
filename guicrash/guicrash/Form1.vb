Imports System
Imports System.IO
Imports System.Threading

Public Class Form1
        Public Const VK_RETURN = &HD
        Const WM_CHAR = &H102        '按下某键，并已发出WM_KEYDOWN，   WM_KEYUP消息  

        Private Declare Auto Function FindWindowEx Lib "user32" (ByVal xHd1 As Integer, ByVal xHd2 As Integer, ByVal lpClassName As String, ByVal lpWindowName As String) As Integer
        Private Declare Auto Function GetWindowText Lib "user32" (ByVal xHd1 As Integer, ByVal lpClassName As String, ByVal xLen As Integer) As Integer
        Private Declare Auto Function GetWindowTextLength Lib "user32" (ByVal xHd As Integer) As Integer

        Private Declare Auto Function ShowWindow Lib "user32" (ByVal hwnd As Integer, ByVal nCmdShow As Integer) As Integer
        Private Declare Auto Function FindWindow Lib "user32" (ByVal lpClassName As String, ByVal lpWindowName As String) As Integer
        Private Declare Auto Function PostMessage Lib "user32" (ByVal hWnd As Integer, ByVal wMsg As Long, ByVal wParam As Long, ByVal lParam As String) As Long
        Private Declare Auto Function SendMessage Lib "user32" (ByVal hWnd As Integer, ByVal wMsg As Integer, ByVal wParam As Integer, ByVal lParam As String) As Integer

        '设置鼠标指针在屏幕中的位置
        Private Declare Auto Function SetCursorPos Lib "user32" (ByVal x As Integer, ByVal y As Integer) As Integer
        Private Declare Auto Sub mouse_event Lib "user32" (ByVal dwFlags As Long, ByVal dx As Long, ByVal dy As Long, ByVal cButtons As Long, ByVal dwExtraInfo As Long)
        Private Declare Auto Sub keybd_event Lib "user32" (ByVal bVk As Byte, ByVal bScan As Byte, ByVal dwFlags As Integer, ByVal dwExtraInfo As Integer)

        Public Shared gFmCfg As New FM_CFG      'To access configuration form.

        '<<<<<<<<<Logging related
        Dim gLogFileName As String
        Dim gLogId As Integer = 0
        Dim gLogMutex As New Threading.Mutex(False)
        Const LOG_SHOW_LINES = 6
        Const LOG_LINES = 800
        Sub LogOne(ByVal RdLog As String)
                'On Error Resume Next
                Dim Ridx As Integer

                If (gExiting) Then
                        Exit Sub
                End If
                gLogMutex.WaitOne()
                Ridx = gLogId Mod LOG_LINES
                DgvLog.Rows(Ridx).Cells(0).Value = gLogId
                DgvLog.Rows(Ridx).Cells(0).Style.BackColor = Color.Brown
                DgvLog.Rows(Ridx).Cells(1).Value = Now().ToString("HH:mm:ss")
                DgvLog.Rows(Ridx).Cells(2).Value = RdLog

                If (Now().ToString("HH:mm") >= "14:30") And (Now().ToString("HH:mm") <= "15:01") Then
                        File.AppendAllText(gLogFileName, gLogId & vbTab & Now().ToString("HH:mm:ss") & vbTab & RdLog & vbCrLf)
                Else
                        File.AppendAllText(gLogFileName, gLogId & vbTab & Now().ToString("HH:mm:ss") & vbTab & RdLog & vbCrLf)
                End If
                If (Ridx > LOG_SHOW_LINES) Then
                        DgvLog.FirstDisplayedScrollingRowIndex = Ridx - LOG_SHOW_LINES '到最后一行
                        DgvLog.Rows(Ridx - 1).Cells(0).Style.BackColor = Color.White
                Else
                        DgvLog.FirstDisplayedScrollingRowIndex = 0 '到最后一行
                        If (Ridx > 0) Then
                                DgvLog.Rows(Ridx - 1).Cells(0).Style.BackColor = Color.White
                        Else
                                DgvLog.Rows(LOG_LINES - 1).Cells(0).Style.BackColor = Color.White
                        End If
                End If
                gLogId = gLogId + 1
                gLogMutex.ReleaseMutex()
                System.Windows.Forms.Application.DoEvents()
        End Sub

        Sub InitLog(ByRef xDgv As DataGridView)
                '初始化事件日志DGV
                gLogFileName = Application.StartupPath & "\LOG.TXT"
                My.Computer.FileSystem.WriteAllText(gLogFileName, "", True)

                xDgv.ReadOnly = False
                xDgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None
                xDgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells
                xDgv.ColumnCount = 3
                xDgv.Columns(0).Name = "LOGID"
                xDgv.Columns(1).Name = "时分秒"
                xDgv.Columns(2).Name = "事件具体描叙"
                xDgv.Columns(0).Width = 40
                xDgv.Columns(1).Width = 60
                xDgv.Columns(2).Width = 586
                xDgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
                xDgv.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing
                xDgv.RowHeadersWidth = 25
                '必须将日志空项全填上，LogOne实现需要这样做
                For i = 0 To (LOG_LINES - 1)
                        xDgv.Rows.Add("", "", "")
                Next
        End Sub

        '<<<<<<< Most fundamental functions
        Function GetWndTitle(ByVal xHd As Integer) As String
                Dim strTitle As String = Space(GetWindowTextLength(xHd) + 1)
                GetWindowText(xHd, strTitle, strTitle.Length)
                Return (strTitle)
        End Function

        Function RvalA() As String()
                Dim aRval(0) As String
                aRval(0) = "Command is executed successfully. Please re-run it again"
                Return (aRval)
        End Function

        Function JIAN(ByVal xHex As String, ByVal xInt As Integer) As String
                Dim sBig As String
                Dim sRval As String

                If (xHex.IndexOf("0x") = 0) Then
                        xHex = xHex.Substring(2, xHex.Length - 2)
                End If
                sBig = xHex.Substring(1, xHex.Length - 1)
                sRval = Hex((Convert.ToInt64(sBig, 16) - xInt))
                Return (xHex.Substring(0, 1) & sRval).ToLower()
        End Function

        Function JIA(ByVal xHex As String, ByVal xInt As Integer) As String
                Dim sBig As String
                Dim sRval As String

                If (xHex.IndexOf("0x") = 0) Then
                        xHex = xHex.Substring(2, xHex.Length - 2)
                End If
                sBig = xHex.Substring(1, xHex.Length - 1)
                sRval = Hex((Convert.ToInt64(sBig, 16) + xInt))
                Return (xHex.Substring(0, 1) & sRval).ToLower()
        End Function

        Function FMT(ByVal xStr As String) As String
                Dim aCells() As String
                Dim bFirst As Boolean = True

                If (xStr = Nothing) Or (xStr = "") Or (xStr = " ") Or (xStr = vbTab) Then
                        'LogOne("The (string) is not valid in FMT.")
                        Return ("")
                End If

                aCells = xStr.Split()
                For i = 0 To aCells.Length - 1
                        If (aCells(i) <> "") And (aCells(i) <> " ") And (aCells(i) <> vbTab) Then
                                If (bFirst) Then
                                        xStr = aCells(i)
                                        bFirst = False
                                Else
                                        xStr &= (" " & aCells(i))
                                End If
                        End If
                Next
                aCells = Nothing

                Return (xStr)
        End Function

        Function GetFirstLineId(ByRef xLines() As String, ByVal xStrictKey As String) As Integer
                If (xLines Is Nothing) Then
                        Return (-1)
                End If

                If (xLines Is gLines) Then
                        If (gHtCmd.ContainsKey(xStrictKey)) Then
                                Return (gHtCmd.Item(xStrictKey))
                        Else
                                Return (-1)
                        End If
                End If

                For i = 0 To xLines.Length - 1
                        If (xLines(i).IndexOf(xStrictKey) <> -1) Then
                                Return (i)
                        End If
                Next
                Return (-1)
        End Function

        Function GetDdosVersion() As Integer
                Dim aSqh() As String

                aSqh = GetLogTxt(gLines, "list qla_ha_list", "list qla_ha_list", Nothing)
                For i = 1 To aSqh.Length - 1
                        If (aSqh(i).IndexOf("list: invalid argument:") <> -1) And (aSqh(i - 1).IndexOf("list qla_ha_list") <> -1) Then
                                Return (5400)
                        End If
                Next
                Return (5500)
        End Function

        Function GetNodeDdr() As String
                Dim iStt As Integer

                For i = 0 To gLines.Length - 1
                        If (gLines(i) = "sys XXSTT") Then
                                iStt = i
                                Exit For
                        End If
                Next

                Return (Trim(gLines(iStt + 8).Split(":")(1)) & "_" & Trim(gLines(iStt + 9).Split(":")(1)))
        End Function

        Dim gBasicReady As Boolean = False
        Function BasicReady() As Boolean
                If (Not File.Exists(gLogDir & "crashcmd.log")) Then
                        If ((gTick Mod 80) < 5) Then
                                LogOne("There is no log file ready to be analyzed. Please re-select the right log file.")
                        End If
                        Return (False)
                End If

                If (GetFirstLineId(gLines, "list qla_ha_list XXEND") < 0) Then
                        If ((gTick Mod 80) < 5) Then
                                LogOne("The log file don't have the basic information. Local analysis mode is not supported for this log file.")
                        End If
                        Return (False)
                End If

                gBasicReady = True
                Return (True)
        End Function

        '<<<<<<<<<< Set
        Sub SetWnds(ByVal xTick As Integer)
                Dim iWnd As Integer

                iWnd = 0
                Do
                        iWnd = FindWindowEx(0, iWnd, "mintty", vbNullString)
                        If (iWnd <> 0) Then
                                If (GetWndTitle(iWnd).IndexOf("crashcmd") <> -1) Then
                                        gCrashWnd = iWnd
                                End If
                                If (GetWndTitle(iWnd).IndexOf("autocode") <> -1) Then
                                        gCodeWnd = iWnd
                                End If
                        Else
                                Exit Do
                        End If
                Loop

                If ((xTick Mod 60) = 59) Then
                        If (gCrashWnd = 0) Or (gCodeWnd = 0) Then
                                LogOne("There is at least one cygwin windows is not open: please check {autocode} {crashcmd}.")
                        End If
                End If
        End Sub

        Sub SetXcmd()
                Dim aCells() As String
                Dim sCmd As String
                Dim sTmp As String
                Dim iPos As Integer
                Dim bBlacked As Boolean

                For i = 0 To gLines.Length - 1
                        If (gLines(i).IndexOf(" XXEND") <> -1) And (gLines(i).IndexOf("echo") = -1) Then
                                If (gLines(i - 1).IndexOf("echo") < 0) Then
                                        Continue For
                                End If
                                aCells = gLines(i).Split()
                                sCmd = aCells(0)
                                For j = 1 To aCells.Count - 2
                                        sCmd &= " " & aCells(j)
                                Next
                                sCmd = FMT(sCmd)        'Found one command

                                If (Not TB_CMD.Items.Contains(sCmd)) Then
                                        bBlacked = False
                                        For j = 0 To gFmCfg.zLines.Length - 1
                                                iPos = gFmCfg.zLines(j).IndexOf("BLK ")
                                                If (iPos <> -1) Then
                                                        sTmp = FMT(gFmCfg.zLines(j).Substring(4))
                                                        If (sTmp = sCmd) Then
                                                                bBlacked = True
                                                                Exit For
                                                        End If
                                                End If
                                        Next
                                        If (Not bBlacked) Then
                                                'struct must be avoided first, and continue to check white list. FIXME
                                                TB_CMD.Items.Add(sCmd)
                                        End If
                                End If
                        End If
                Next
        End Sub

        Function RunShell(ByVal xShellCmd As String) As String
                Dim sResult As String
                Dim oSr As System.IO.StreamReader

                If Directory.Exists("C:\cygwin\") Then
                        Shell("C:\cygwin\bin\bash.exe " & xShellCmd, AppWinStyle.MinimizedFocus, True)
                        oSr = New System.IO.StreamReader("C:\cygwin\tmp\vb.result.out.txt")
                Else
                        Shell("C:\cygwin64\bin\bash.exe " & xShellCmd, AppWinStyle.MinimizedFocus, True)
                        oSr = New System.IO.StreamReader("C:\cygwin64\tmp\vb.result.out.txt")
                End If
                sResult = oSr.ReadToEnd()
                oSr.Close()

                Return (sResult)
        End Function

        '<<<<<<<<<<<<<<Key functions
        Function GetMemDef(ByRef xDefStt As Integer, ByVal xAddr As String) As String
                Dim sLine As String = ""
                Dim aCells() As String
                Dim iStt As Integer
                Dim iLen As Integer
                Dim sRval As String
                Dim iLidx As Integer = xDefStt - 1

                Do
                        iLidx += 1
                        sLine = gLines(iLidx)
                        iStt = sLine.IndexOf("[")
                Loop Until (iStt <> -1)

                sLine = FMT(sLine)
                iStt = sLine.IndexOf("[")
                iLen = sLine.IndexOf("]") - iStt - 1

                sRval = "0x" & Hex(sLine.Substring(iStt + 1, iLen))
                sRval &= (" " & JIA(xAddr, (sLine.Substring(iStt + 1, iLen))))

                aCells = sLine.Split()
                For i = 1 To aCells.Count - 2
                        sRval &= (" " & aCells(i))
                Next

                xDefStt = (iLidx + 1)      'To speed up for for next call
                Return (sRval)
        End Function

        Function FoundMem(ByVal xLine As String, ByVal xMemName As String) As Boolean
                Dim iPos As Integer
                Dim iLast As Integer
                Dim iStt As Integer

                xLine = FMT(xLine)
                If (xLine = "") Then
                        Return (False)
                End If
                If (xLine(0) <> "[") Then
                        Return (False)
                End If
                xMemName = FMT(xMemName)

                iLast = 0
                Do Until (iLast < 0)
                        iPos = iLast
                        iLast = xLine.IndexOf(xMemName, iStt)
                        If (iLast >= 0) Then
                                iStt = iLast + xMemName.Length
                        End If
                Loop
                If (iPos <= 0) Then
                        Return (False)
                End If

                If (iPos > 0) Then
                        If (xLine(iPos - 1) <> " ") And (xLine(iPos - 1) <> "*") Then
                                'avoid p_slot_no slot_no
                                Return (False)
                        End If
                End If

                iPos += xMemName.Length
                If (xLine(iPos) <> " ") And (xLine(iPos) <> ":") And (xLine(iPos) <> ";") And _
                        (xLine(iPos) <> ",") Then
                        'avoid slot_no slot_no_p
                        Return (False)
                End If

                Return (True)
        End Function

        Const INVALID_VAL = "-99999"
        Function GetVal(ByVal xAddr As String, ByVal xStructName As String, ByVal xMemName As String) As String
                Dim iStt As Integer
                Dim iEnd As Integer
                Dim iLen As Integer
                Dim sCmd As String

                xStructName = FMT(xStructName)
                xMemName = FMT(xMemName)
                sCmd = xStructName & " " & xAddr
                iStt = GetFirstLineId(gLines, sCmd & " XXSTT")
                iEnd = GetFirstLineId(gLines, sCmd & " XXEND")
                If (iStt < 0) Or (iEnd < 0) Or (iEnd < iStt) Then
                        LogOne("Can't get valid value for " & xAddr & " " & xStructName & "->" & xMemName)
                        Return (INVALID_VAL)
                End If

                For i = iStt To iEnd
                        If (gLines(i).IndexOf(xMemName) <> -1) Then
                                gLines(i) = FMT(gLines(i))
                                If (gLines(i).Split()(0) <> xMemName) Then
                                        Continue For
                                End If
                        Else
                                Continue For
                        End If
                        iStt = gLines(i).IndexOf("=")
                        iLen = gLines(i).IndexOf(",") - iStt - 1
                        Return (Trim(gLines(i).Substring(iStt + 1, iLen)))
                Next
                LogOne("Can't get valid value for " & xAddr & " " & xStructName & "->" & xMemName)
                Return (INVALID_VAL)
        End Function

        Function GetOff(ByVal xStructName As String, ByVal xMemName As String) As Integer
                Dim iStt As Integer
                Dim iEnd As Integer
                Dim iLen As Integer
                Dim sCmd As String

                xStructName = FMT(xStructName)
                xMemName = FMT(xMemName)

                sCmd = FMT(xStructName) & " -o"
                If (Not IsThisExecuted(gLines, sCmd)) Then
                        Return (-1)
                End If
                iStt = GetFirstLineId(gLines, sCmd & " XXSTT")
                iEnd = GetFirstLineId(gLines, sCmd & " XXEND")
                If (iStt < 0) Or (iEnd < 0) Or (iEnd < iStt) Then
                        Return (-1)
                End If

                For i = iStt To iEnd
                        If (Not FoundMem(gLines(i), xMemName)) Then
                                Continue For
                        End If
                        iStt = gLines(i).IndexOf("[")
                        iLen = gLines(i).IndexOf("]") - iStt - 1
                        Return (Trim(gLines(i).Substring(iStt + 1, iLen)))
                Next
                Return (-1)
        End Function


        Sub MakeObjList(ByVal xAddr As String, ByVal xOff As Integer, ByRef xList As List(Of String))
                Dim iStt As Integer
                Dim iEnd As Integer
                Dim sCmd As String

                xList.Clear()
                xAddr = FMT(xAddr)

                sCmd = "list " & xAddr
                If (Not IsThisExecuted(gLines, sCmd)) Then
                        Exit Sub
                End If

                iStt = GetFirstLineId(gLines, sCmd & " XXSTT")
                iEnd = GetFirstLineId(gLines, sCmd & " XXEND")
                If (iStt < 0) Or (iEnd < 0) Or (iEnd < iStt) Then
                        Exit Sub
                End If

                For i = (iStt + 1) To (iEnd - 1)
                        xList.Add(JIAN(gLines(i), xOff))
                Next
        End Sub

        Function GetListTxt(ByVal xAddr As String, ByVal xStructName As String, ByVal xListMemName As String) As String()
                Dim sCmd As String
                Dim oAddrList As New List(Of String)
                Dim bAllDone As Boolean = True
                Dim aRval() As String
                Dim aAll(10000000) As String
                Dim iPos As Integer
                Dim iOff As Integer

                sCmd = "list " & Trim(xAddr)
                If (Not IsThisExecuted(gLines, sCmd)) Then
                        LogOne("This command has not been executed before: " & sCmd)
                        Return (RvalA())
                End If

                sCmd = Trim(xStructName) & " -o"
                If (Not IsThisExecuted(gLines, sCmd)) Then
                        LogOne("This command has not been executed before: " & sCmd)
                        Return (RvalA())
                End If

                iOff = GetOff(xStructName, xListMemName)
                If (iOff < 0) Then
                        LogOne("Can't find the member for " & xStructName & "->" & xListMemName)
                        Return (RvalA())
                End If
                MakeObjList(Trim(xAddr), iOff, oAddrList)
                For Each sOneAddr In oAddrList
                        sCmd = Trim(xStructName) & " " & sOneAddr
                        If (Not IsThisExecuted(gLines, sCmd)) Then
                                LogOne("This command has not been executed before: " & sCmd)
                                bAllDone = False
                        End If
                Next

                If (bAllDone And (oAddrList.Count > 0)) Then
                        iPos = 0
                        For i = 0 To oAddrList.Count - 1
                                aRval = GetStructTxt(xStructName, oAddrList.Item(i))
                                For j = 0 To aRval.Count - 1
                                        aAll(iPos) = aRval(j)
                                        iPos += 1
                                Next
                        Next
                        ReDim aRval(iPos - 1)
                        For i = 0 To iPos - 1
                                aRval(i) = aAll(i)
                        Next
                        aAll = Nothing
                        Return (aRval)
                Else
                        Return (RvalA())
                End If
        End Function

        Function GetSymTxt(ByVal xAddr As String) As String()
                Dim sCmd As String
                Dim aRval() As String
                Dim aCval() As String
                Dim aSymRval() As String
                Dim sFile As String = ""
                Dim aCells() As String
                Dim iLine As String = -1

                sCmd = "sym " & Trim(xAddr)
                If (Not IsThisExecuted(gLines, sCmd)) Then
                        LogOne("This command has not been executed (gLines): " & sCmd)
                        Return (RvalA())
                End If

                If (GetCodeCmd(sCmd, sFile, iLine) = Nothing) Then
                        'non source file related symbol
                        Return (GetLogTxt(gLines, sCmd, sCmd, Nothing))
                End If

                If (Not IsThisExecuted(gCodes, sCmd)) Then
                        LogOne("This command has not been executed (gCodes): " & sCmd)
                        Return (RvalA())
                End If

                aRval = GetLogTxt(gLines, sCmd, sCmd, Nothing)
                aCval = GetLogTxt(gCodes, sCmd, sCmd, Nothing)
                ReDim aSymRval(aRval.Length + aCval.Length - 1)

                aCells = aRval(aRval.Length - 2).Split()
                aSymRval(0) = "LINE " & iLine & " http://tc24.datadomain.com" & sFile

                For i = 0 To aRval.Length - 1
                        aSymRval(i + 1) = aRval(i)
                Next

                For i = 1 To aCval.Length - 1
                        aSymRval(i + aRval.Length) = aCval(i)
                Next

                aRval = Nothing
                aCval = Nothing
                Return (aSymRval)
        End Function

        'xStt doesn't contain XXSTT, xEnd doesn't contain XXEND.
        Function GetLogTxt(ByRef xLines() As String, ByVal xStt As String, ByVal xEnd As String, ByVal xKey As String) As String()
                Dim iStt As Integer
                Dim iEnd As Integer
                Dim iPos As Integer
                Dim aKval() As String
                Dim aRval() As String

                xStt = FMT(xStt)
                xEnd = FMT(xEnd)
                xKey = FMT(xKey)
                If (xLines Is Nothing) Then
                        Return (RvalA())
                End If

                If (Not IsThisExecuted(xLines, xStt)) Then
                        Return (RvalA())
                End If
                If (Not IsThisExecuted(xLines, xEnd)) Then
                        Return (RvalA())
                End If
                iStt = GetFirstLineId(xLines, xStt & " XXSTT")
                If (xLines Is gCodes) Then
                        iEnd = -1
                        For i = 0 To gCodes.Length - 1
                                If (gCodes(i) = (xEnd & " XXEND")) Then
                                        iEnd = i
                                        Exit For
                                End If
                        Next
                Else
                        iEnd = GetFirstLineId(xLines, xEnd & " XXEND")
                End If
                If (iStt < 0) Or (iEnd < 0) Or (iEnd < iStt) Then
                        Return (RvalA())
                End If

                ReDim aRval(iEnd - iStt)
                iPos = 0
                For i = iStt To iEnd
                        If (xKey Is Nothing) Or (xKey = "") Or (xKey = " ") Or (xKey = vbTab) Then
                                aRval(i - iStt) = xLines(i)
                        Else
                                If (xLines(i).IndexOf(xKey) <> -1) Then
                                        aRval(iPos) = xLines(i)
                                        iPos += 1
                                End If
                        End If
                Next

                If (iPos > 0) Then
                        ReDim aKval(iPos)
                        For i = 0 To iPos - 1
                                aKval(i) = aRval(i)
                        Next
                        aRval = Nothing
                        Return (aKval)
                Else
                        Return (aRval)
                End If
        End Function

        Function GetStructTxt(ByVal xStructName As String, ByVal xAddr As String) As String()
                Dim iDefStt As Integer
                Dim iDefEnd As Integer
                Dim iObjStt As Integer
                Dim iObjEnd As Integer
                Dim aRval() As String
                Dim aCells() As String
                Dim sTmp As String
                Dim sCmd As String

                xStructName = FMT(xStructName)
                xAddr = FMT(xAddr)

                sCmd = (xStructName & " " & xAddr)
                If (Not IsThisExecuted(gLines, sCmd)) Then
                        Return (RvalA())
                End If
                iObjStt = GetFirstLineId(gLines, sCmd & " XXSTT")
                iObjEnd = GetFirstLineId(gLines, sCmd & " XXEND")

                sCmd = (xStructName & " -o")
                If (Not IsThisExecuted(gLines, sCmd)) Then
                        Return (RvalA())
                End If
                iDefStt = GetFirstLineId(gLines, sCmd & " XXSTT")
                iDefEnd = GetFirstLineId(gLines, sCmd & " XXEND")

                If (iDefStt < 0) Or (iDefEnd < 0) Or (iObjStt < 0) Or (iObjEnd < 0) Or (iObjEnd < iObjStt) Or (iDefEnd < iDefStt) Then
                        Return (RvalA())
                End If

                ReDim aRval(iObjEnd - iObjStt)
                For i = 0 To aRval.Count - 1
                        aRval(i) = gLines(iObjStt + i)
                        If (aRval(i).Length > 0) Then
                                If (aRval(i).Chars(0) = " ") And (aRval(i).Length > 2) And (aRval(i).IndexOf("=") <> -1) Then
                                        If (aRval(i).Chars(1) = " ") And (aRval(i).Chars(2) <> " ") Then
                                                aCells = aRval(i).Split()
                                                sTmp = aRval(i).Substring(0, aRval(i).IndexOf("=") + 1)
                                                sTmp &= (" " & GetMemDef(iDefStt, xAddr) & " " & aRval(i).Substring(aRval(i).IndexOf("=") + 1))
                                                aRval(i) = sTmp
                                        End If
                                End If
                        End If
                Next

                Return (aRval)
        End Function

        '<<<<<<<<<<Messages sending related
        Function GetCodeCmd(ByVal xCmd As String, ByRef xFile As String, ByRef xLine As Integer) As String
                Dim aRval() As String
                Dim aCells() As String
                Dim iPos As Integer
                Dim sFile As String
                Dim iLine As Integer

                If (Not (xFile Is Nothing)) Then
                        xFile = ""
                        xLine = -1
                End If
                xCmd = FMT(xCmd)
                If (GetFirstLineId(gLines, xCmd & " XXEND") < 0) Then
                        'To avoid embedded SendString()
                        Return (Nothing)
                End If

                aRval = GetLogTxt(gLines, xCmd, xCmd, Nothing)
                If (aRval.Count < 2) Then
                        Return (Nothing)
                End If

                iPos = aRval.Count - 2 'related with GetFirstLineId
                If (aRval(iPos).IndexOf("auto") < 0) Or (aRval(iPos).IndexOf("builds") < 0) Then
                        Return (Nothing)
                End If

                aCells = aRval(iPos).Split()
                iPos = aCells.Length - 2
                sFile = aCells(iPos).Split(":")(0)
                iLine = aCells(iPos + 1)
                If (Not (xFile Is Nothing)) Then
                        xFile = sFile
                        xLine = iLine
                End If

                Dim sCln As Integer = 20
                If (IsNumeric(TB_CLN.Text)) Then
                        If (CInt(TB_CLN.Text) <= 20) Then
                                TB_CLN.Text = "20"
                        Else
                                sCln = CInt(TB_CLN.Text)
                        End If
                Else
                        TB_CLN.Text = "20"
                End If
                Return ("cat -n " & sFile & " | grep -A" & sCln & " -B" & sCln & " '\b" & iLine & "\b'")
        End Function

        '注意：使用SED截取输出后，echo本身都不会出现文件中，不能用echo去确定命令的完成与否，正好符合我的需求。2015-07-23
        Function IsThisExecuted(ByRef xLines() As String, ByVal xCmd As String) As Boolean
                'It's expected to be one atomic command.
                If (xLines Is gLines) Then
                        If (Not gHtCmd.ContainsKey(FMT(xCmd) & " XXEND")) And (Not gHtCmd.ContainsKey(FMT(xCmd) & " XXSTT")) Then
                                SendString(gCrashWnd, FMT(xCmd), Nothing)
                                Return (False)
                        ElseIf (gHtCmd.ContainsKey(FMT(xCmd) & " XXEND")) And (gHtCmd.ContainsKey(FMT(xCmd) & " XXSTT")) Then
                                Return (True)
                        Else
                                LogOne("The command is still in execution: " & xCmd)
                                Return (False)
                        End If
                End If

                If (GetFirstLineId(xLines, FMT(xCmd) & " XXEND") < 0) And (GetFirstLineId(xLines, FMT(xCmd) & " XXSTT") < 0) Then
                        If (xLines Is gLines) Then
                                SendString(gCrashWnd, FMT(xCmd), Nothing)
                        Else
                                SendString(gCodeWnd, FMT(xCmd), Nothing)
                        End If
                        Return (False)
                ElseIf (GetFirstLineId(xLines, FMT(xCmd) & " XXEND") >= 0) And (GetFirstLineId(xLines, FMT(xCmd) & " XXSTT") >= 0) Then
                        Return (True)
                Else
                        Return (False)
                End If
        End Function

        Sub SendString(ByVal xWnd As Integer, ByVal xCmd As String, ByVal xNoPrevPost As Boolean)
                Dim aWmChar() As Char
                Dim iVk As Integer
                Dim aLines() As String

                xCmd = FMT(xCmd)
                If (xWnd = 0) Then
                        LogOne("The windows doesn't exist.")
                        Exit Sub
                End If

                If (xWnd = gCrashWnd) Then
                        aLines = gLines
                        If gHtCmd.ContainsKey(xCmd & " XXSTT") And (gQlaPhase > 0) Then
                                'LogOne("This command has been executed successfully: " & xCmd)
                                Exit Sub
                        End If
                Else
                        aLines = gCodes
                        If (GetFirstLineId(aLines, xCmd & " XXSTT") >= 0) Then
                                'LogOne("This command has been executed successfully: " & xCmd)
                                Exit Sub
                        End If
                        If (GetCodeCmd(xCmd, Nothing, iVk) = Nothing) Then
                                Exit Sub
                        End If
                End If

                If (xNoPrevPost = False) Then
                        aWmChar = ("echo        ").ToCharArray
                        For i = 0 To aWmChar.Count - 1
                                iVk = Asc(aWmChar(i))
                                SendMessage(xWnd, WM_CHAR, iVk, &HF0001)
                        Next
                        SendMessage(xWnd, WM_CHAR, VK_RETURN, &HF0001)

                        'PREV handling
                        aWmChar = ("echo " & xCmd & " XXSTT").ToCharArray
                        For i = 0 To aWmChar.Count - 1
                                iVk = Asc(aWmChar(i))
                                SendMessage(xWnd, WM_CHAR, iVk, &HF0001)
                        Next
                        SendMessage(xWnd, WM_CHAR, VK_RETURN, &HF0001)
                End If

                'Command itself
                If (xWnd = gCrashWnd) Then
                        aWmChar = (xCmd).ToCharArray
                Else
                        'Code
                        aWmChar = GetCodeCmd(xCmd, Nothing, iVk).ToCharArray
                End If
                For i = 0 To aWmChar.Count - 1
                        iVk = Asc(aWmChar(i))
                        SendMessage(xWnd, WM_CHAR, iVk, &HF0001)
                Next
                SendMessage(xWnd, WM_CHAR, VK_RETURN, &HF0001)

                If (xNoPrevPost = False) Then
                        'POST handling
                        If (xCmd.IndexOf("sym ") <> -1) And (xWnd <> gCrashWnd) Then
                                Thread.Sleep(500)
                        End If
                        aWmChar = ("echo " & xCmd & " XXEND").ToCharArray
                        For i = 0 To aWmChar.Count - 1
                                iVk = Asc(aWmChar(i))
                                SendMessage(xWnd, WM_CHAR, iVk, &HF0001)
                        Next
                        SendMessage(xWnd, WM_CHAR, VK_RETURN, &HF0001)
                End If
        End Sub

        '<<<<<<<<<<<<VHBA related
        Function GetVhbaAddr(ByRef xId As Integer) As String
                Dim iEnd As Integer
                Dim sAddr As String
                Dim iSize As Integer

                For i = gLines.Length - 1 To 0 Step -1
                        If (gLines(i) = "rd vhba_list XXEND") Then
                                iEnd = i
                                Exit For
                        End If
                Next
                sAddr = gLines(iEnd - 2).Split(":")(0)  'ffffffff8a318a50:  0a10d94400000001

                For i = gLines.Length - 1 To 0 Step -1
                        If (gLines(i) = "vhba_t -o XXEND") Then
                                iEnd = i
                                Exit For
                        End If
                Next
                iSize = gLines(iEnd - 2).Split(":")(1)  'SIZE: 541064

                Return (JIA(sAddr, xId * iSize))
        End Function

        Function GetVhba(ByRef xId As Integer) As String()
                Return (GetStructTxt("vhba_t", GetVhbaAddr(xId)))
        End Function

        '<<<<<<<<<<<<<<<<VTL related
        Sub DoQlaAnalysis()
                Dim oAddrList As New List(Of String)
                Dim sHaAddr As String = ""
                Dim iOff As Integer

                If (gQlaPhase = 1) Then
                        If (GetDdosVersion() >= 5500) Then
                                iOff = GetOff("struct scsi_qla_host", "ha_list_entry")
                                If (iOff < 0) Then
                                        Exit Sub
                                End If
                                MakeObjList("qla_ha_list", iOff, oAddrList)
                        Else
                                iOff = GetOff("struct scsi_qla_host", "list")
                                If (iOff < 0) Then
                                        Exit Sub
                                End If
                                MakeObjList("qla_hostlist", iOff, oAddrList)
                        End If

                        For Each sHaAddr In oAddrList
                                SendString(gCrashWnd, "struct scsi_qla_host " & sHaAddr, Nothing)
                        Next

                        If (oAddrList.Count > 0) Then
                                gQlaPhase += 1
                        End If
                        'Exit Sub '必须退出
                End If

                '第二阶段处理（目前还只考虑两阶段）
                If (gQlaPhase = 2) Then
                        If (GetDdosVersion() >= 5500) Then
                                iOff = GetOff("struct scsi_qla_host", "ha_list_entry")
                                If (iOff < 0) Then
                                        Exit Sub
                                End If
                                MakeObjList("qla_ha_list", iOff, oAddrList)
                        Else
                                iOff = GetOff("struct scsi_qla_host", "list")
                                If (iOff < 0) Then
                                        Exit Sub
                                End If
                                MakeObjList("qla_hostlist", iOff, oAddrList)
                        End If

                        For Each sHaAddr In oAddrList
                                If (GetFirstLineId(gLines, "struct scsi_qla_host " & sHaAddr & " XXEND") < 0) Then
                                        Exit Sub
                                End If
                        Next

                        For Each sHaAddr In oAddrList
                                CB_SQH.Items.Add(sHaAddr & " " & GetVal(sHaAddr, "struct scsi_qla_host", "slot_no") & ":" & _
                                                 GetVal(sHaAddr, "struct scsi_qla_host", "port_no"))
                        Next

                        If (oAddrList.Count > 0) Then
                                gQlaPhase += 1
                        End If
                        Exit Sub '必须退出
                End If
        End Sub

        Sub MarkMe(ByRef xRtb As RichTextBox, ByVal xKey As String)
                Dim iIdx As Integer
                Dim iStt As Integer
                Dim iNidx As Integer

                If (xKey.Length < 2) Then
                        LogOne("KEY值太短")
                        Exit Sub
                End If

                xRtb.ForeColor = Color.Black
                iIdx = xRtb.Find(xKey, RichTextBoxFinds.None)
                iStt = iIdx
                If (iIdx < 0) Then
                        LogOne("KEY值没找到")
                        Exit Sub
                End If
                iNidx = 0
                While (iNidx <> iStt)
                        xRtb.SelectionStart = iIdx
                        xRtb.SelectionLength = xKey.Length
                        xRtb.SelectionColor = Color.Red
                        xRtb.Focus()
                        iNidx = xRtb.Find(xKey, iIdx + xKey.Length, RichTextBoxFinds.None)
                        If (iNidx = -1) Then
                                iNidx = iStt
                        End If
                        iIdx = iNidx
                End While
        End Sub

        'FilterMe need use these to keep messages to be filtered
        Dim gMoreLines() As String = Nothing
        Dim gMore2Lines() As String = Nothing
        Sub FilterMe(ByRef xRtb As RichTextBox, ByVal xKey As String)
                Dim iIdx As Integer
                Dim iStt As Integer
                Dim iNidx As Integer
                Dim iEnd As Integer
                Dim iLines As Integer
                Dim sLines As String
                Dim aLines() As String
                Dim aFilter() As String
                Dim aCells() As String

                If (xKey.Length < 2) Then
                        LogOne("KEY值太短")
                        Exit Sub
                End If

                If (xRtb.Lines.Length > 0) Then
                        ReDim aLines(xRtb.Lines.Length - 1)
                Else
                        Exit Sub
                End If

                xRtb.ForeColor = Color.Black
                iIdx = xRtb.Find(xKey, RichTextBoxFinds.None)
                iStt = iIdx
                If (iIdx < 0) Then
                        LogOne("KEY值没找到")
                        Exit Sub
                End If

                iLines = 0
                iNidx = 0
                While (iNidx <> iStt)
                        'xRtb.Text这里的换行就是这个啦vbLf，文件中的才是vbCrLf
                        iEnd = xRtb.Text.IndexOf(vbLf, iIdx)
                        If (iIdx > 200) Then
                                iIdx -= 200
                        Else
                                iIdx = 0
                        End If
                        sLines = xRtb.Text.Substring(iIdx, iEnd - iIdx)
                        aCells = sLines.Split(vbLf)
                        aLines(iLines) = aCells(aCells.Length - 1)    'save it now
                        iLines += 1

                        iNidx = xRtb.Find(xKey, iEnd, RichTextBoxFinds.None)
                        If (iNidx = -1) Then
                                iNidx = iStt
                        End If
                        iIdx = iNidx
                End While

                If (iLines > 0) Then
                        ReDim aFilter(iLines - 1)
                        For i = 0 To iLines - 1
                                aFilter(i) = aLines(i)
                        Next
                        SetLines(xRtb, aFilter)
                End If
                aFilter = Nothing
                aLines = Nothing
        End Sub

        Function GetCodeAddr(ByVal xVal As Integer) As String
                Dim aRval() As String
                Dim sCmd As String
                Dim sBase As String

                sBase = Nothing
                sCmd = "_text"
                aRval = GetLogTxt(gLines, sCmd, sCmd, Nothing)
                For i = 0 To aRval.Length - 1
                        aRval(i) = FMT(aRval(i))
                        If (aRval(i).IndexOf("_text =") <> -1) Then
                                sBase = aRval(i).Split()(4)
                                Exit For
                        End If
                Next

                If (Not (sBase Is Nothing)) Then
                        Return (JIA(sBase, xVal))
                Else
                        Return (JIA("0xffffffff81000000", xVal))
                End If
        End Function

        Sub SsaCallerOwner(ByRef xRtb As RichTextBox, ByRef xLines() As String, ByVal xLidx As Integer)
                Dim aCaller() As String
                Dim aOwner() As String
                Dim sCmd As String
                Dim iVal As Integer
                Dim sCodeAddr As String

                aCaller = Nothing
                aOwner = Nothing

                'sym: caller
                iVal = CInt(xLines(xLidx + 1).Substring(xLines(xLidx + 1).IndexOf("=") + 1))
                sCodeAddr = GetCodeAddr(iVal)
                sCmd = "sym " & sCodeAddr
                If (IsThisExecuted(gLines, sCmd)) And (IsThisExecuted(gCodes, sCmd)) Then
                        aCaller = GetSymTxt(sCodeAddr)
                End If

                'backtrace first: owner
                iVal = CInt(xLines(xLidx + 2).Substring(xLines(xLidx + 1).IndexOf("=") + 1))
                sCmd = "bt " & CStr(iVal)
                If (IsThisExecuted(gLines, sCmd)) Then
                        aOwner = GetLogTxt(gLines, sCmd, sCmd, Nothing)
                End If

                If (Not (aCaller Is Nothing)) And (Not (aOwner Is Nothing)) Then
                        Dim iLine As Integer
                        Dim sFile As String = ""
                        sCmd = "sym " & sCodeAddr
                        GetCodeCmd(sCmd, sFile, iLine)
                        If (xRtb Is RTB_MORE) Then
                                SetLines(RTB_MORE2, ArrayAdd(aCaller, aOwner))
                                MarkMe(RTB_MORE2, CStr(iLine))
                        Else
                                SetLines(RTB_MORE, ArrayAdd(aCaller, aOwner))
                                MarkMe(RTB_MORE, CStr(iLine))
                        End If
                Else
                        LogOne("Please re-run SSA to get the result.")
                End If
                aCaller = Nothing
                aOwner = Nothing
        End Sub

        Sub SsaKmem(ByRef xRtb As RichTextBox, ByRef xLines() As String, ByVal xLidx As Integer)
                Dim sCmd As String
                Dim aRval() As String

                sCmd = "kmem -S " & xLines(0).Split()(0)
                If (IsThisExecuted(gLines, sCmd)) Then
                        aRval = GetLogTxt(gLines, sCmd, sCmd, Nothing)
                        If (xRtb Is RTB_MORE) Then
                                SetLines(RTB_MORE2, aRval)
                        Else
                                SetLines(RTB_MORE, aRval)
                        End If
                Else
                        LogOne("Please re-run SAA to check the result: " & sCmd)
                End If
        End Sub

        Sub SsaRip(ByRef xRtb As RichTextBox, ByRef xLines() As String, ByVal xLidx As Integer, ByVal xAddr As String)
                Dim sCmd As String
                Dim aRval() As String

                sCmd = "sym " & xAddr
                If (IsThisExecuted(gLines, sCmd)) Then
                        aRval = GetSymTxt(xAddr)
                        If (xRtb Is RTB_MORE) Then
                                SetLines(RTB_MORE2, aRval)
                        Else
                                SetLines(RTB_MORE, aRval)
                        End If
                Else
                        LogOne("Please re-run SAA to check the result: " & sCmd)
                End If
        End Sub

        Sub SsaListKmem(ByRef xRtb As RichTextBox, ByRef xLines() As String, ByVal xLidx As Integer)
                Dim sCmd As String
                Dim oAddrList As New List(Of String)
                Dim bAllDone As Boolean
                Dim aCells() As String

                For i = 0 To xLines.Length - 1
                        xLines(i) = FMT(xLines(i))
                        aCells = xLines(i).Split()
                        For j = 0 To aCells.Length - 1
                                If (aCells(j) <> "") Then
                                        If (aCells(j)(0) = "[") And (aCells(j).Length > 16) Then
                                                If (IsKernelAddr(aCells(j).Substring(1, 16))) Then
                                                        oAddrList.Add(aCells(j).Substring(1, 16))
                                                End If
                                        End If

                                        If (IsKernelAddr(aCells(j))) Then
                                                oAddrList.Add(aCells(j))
                                        End If
                                End If
                        Next
                Next

                bAllDone = True
                For Each sNodeAddr In oAddrList
                        sCmd = TB_SCMD.Text & " " & sNodeAddr
                        If (Not IsThisExecuted(gLines, sCmd)) Then
                                bAllDone = False
                        End If
                Next

                If (bAllDone And (oAddrList.Count > 0)) Then
                        Dim aRval() As String

                        ReDim aRval(1)
                        For Each sNodeAddr In oAddrList
                                'To avoid long list to consume every things.
                                aRval = ArrayAdd(aRval, GetStructTxt(TB_SCMD.Text, sNodeAddr))
                        Next

                        If (xRtb Is RTB_MORE) Then
                                SetLines(RTB_MORE2, aRval)
                        Else
                                SetLines(RTB_MORE, aRval)
                        End If
                        aRval = Nothing
                End If
                If (Not TB_SCMD.Items.Contains(FMT(TB_SCMD.Text))) Then
                        TB_SCMD.Items.Add(FMT(TB_SCMD.Text))
                End If
        End Sub

        Sub SsaListCmd(ByRef xRtb As RichTextBox, ByRef xLines() As String, ByVal xLidx As Integer)
                Dim sMem As String
                Dim sStruct As String
                Dim sCmd As String
                Dim iOff As Integer
                Dim oAddrList As New List(Of String)
                Dim bAllDone As Boolean

                sStruct = TB_SCMD.Text.Split(".")(0)
                sMem = TB_SCMD.Text.Split(".")(1)
                iOff = GetOff(sStruct, sMem)
                If (iOff < 0) Then
                        LogOne("Please re-SAA to finish SsaListCmd " & sStruct & "->" & sMem)
                        Exit Sub
                End If

                For i = 0 To xLines.Length - 1
                        If (IsKernelAddr(xLines(i))) Then
                                oAddrList.Add(JIAN(xLines(i), iOff))
                        End If
                Next

                bAllDone = True
                For Each sNodeAddr In oAddrList
                        sCmd = sStruct & " " & sNodeAddr
                        If (Not IsThisExecuted(gLines, sCmd)) Then
                                bAllDone = False
                        End If
                Next

                If (bAllDone And (oAddrList.Count > 0)) Then
                        Dim aRval() As String

                        ReDim aRval(1)
                        For Each sNodeAddr In oAddrList
                                'To avoid long list to consume every things.
                                aRval = ArrayAdd(aRval, GetStructTxt(sStruct, sNodeAddr))
                        Next

                        If (xRtb Is RTB_MORE) Then
                                SetLines(RTB_MORE2, aRval)
                        Else
                                SetLines(RTB_MORE, aRval)
                        End If
                        aRval = Nothing
                End If
                If (Not TB_SCMD.Items.Contains(FMT(TB_SCMD.Text))) Then
                        TB_SCMD.Items.Add(FMT(TB_SCMD.Text))
                End If
        End Sub

        Sub SsaList(ByRef xRtb As RichTextBox, ByRef xLines() As String, ByVal xLidx As Integer)
                Dim aCells() As String
                Dim sMem As String
                Dim sAddr As String
                Dim sStruct As String
                Dim sCmd As String
                Dim iOff As Integer
                Dim oAddrList As New List(Of String)
                Dim iPos As Integer
                Dim bAllDone As Boolean

                If (xLines(0).IndexOf("=") = -1) Then
                        Exit Sub
                End If

                aCells = xLines(0).Split()
                sAddr = aCells(3)
                sMem = aCells(0)
                If (aCells.Length <> 7) Then
                        LogOne("list command is not complete: " & xLines(0))
                        Exit Sub
                End If

                sCmd = "list " & sAddr
                If (Not IsThisExecuted(gLines, sCmd)) Then
                        LogOne("This command is not exuectued yet. Please re-SSA: " & sCmd)
                        Exit Sub
                End If

                iPos = -1
                For i = 0 To xRtb.Lines.Length - 1
                        If (xRtb.Lines(i).IndexOf("list_head") <> -1) Then
                                If (xRtb.Lines(i).IndexOf(sAddr) <> -1) Then
                                        iPos = i
                                        Exit For
                                End If
                        End If

                Next

                sStruct = ""
                For i = iPos To 0 Step -1
                        If (xRtb.Lines(i).IndexOf("crash") <> -1) And (xRtb.Lines(i).IndexOf(">") <> -1) Then
                                aCells = FMT(xRtb.Lines(i)).Split()
                                If (IsKernelAddr(aCells(aCells.Length - 1))) Then
                                        If (aCells.Length = 3) Then
                                                sStruct = aCells(1)
                                        ElseIf (aCells.Length = 4) Then
                                                sStruct = aCells(1) & " " & aCells(2)
                                        Else
                                                LogOne("Illegal crash cmd: " & xRtb.Lines(i))
                                                Exit Sub
                                        End If
                                End If
                                Exit For
                        End If
                Next

                If (sStruct = "") Then
                        LogOne("Didn't get struct name")
                        Exit Sub
                End If
                iOff = GetOff(sStruct, sMem)
                If (iOff < 0) Then
                        LogOne("Can't get offset of this member fro struct: " & sStruct & "->" & sMem)
                        Exit Sub
                End If

                MakeObjList(sAddr, iOff, oAddrList)
                bAllDone = True
                For Each sNodeAddr In oAddrList
                        sCmd = sStruct & " " & sNodeAddr
                        If (Not IsThisExecuted(gLines, sCmd)) Then
                                bAllDone = False
                        End If
                Next

                If (bAllDone And (oAddrList.Count > 0)) Then
                        Dim aRval() As String
                        Dim iIdx As Integer

                        aRval = GetLogTxt(gLines, "list " & sAddr, "list " & sAddr, Nothing)
                        iIdx = 0
                        For Each sNodeAddr In oAddrList
                                If (iIdx < 6) Or (iIdx > (oAddrList.Count - 5)) Then
                                        'To avoid long list to consume every things.
                                        aRval = ArrayAdd(aRval, GetStructTxt(sStruct, sNodeAddr))
                                End If
                        Next

                        If (xRtb Is RTB_MORE) Then
                                SetLines(RTB_MORE2, aRval)
                        Else
                                SetLines(RTB_MORE, aRval)
                        End If
                        aRval = Nothing
                End If
        End Sub

        Sub DoSsa(ByRef xRtb As RichTextBox)
                Dim aLines() As String
                Dim aCells() As String
                Dim iOff As Integer
                Dim sTmp As String

                If (xRtb.SelectedText Is Nothing) Or (xRtb.SelectedText = "") Then
                        Exit Sub
                End If

                aLines = xRtb.SelectedText.Split(vbLf)
                aLines(0) = FMT(aLines(0))

                If (TB_SCMD.Text.Length > 3) Then
                        SsaListKmem(xRtb, aLines, 0)
                        If (Not TB_SCMD.Items.Contains(FMT(TB_SCMD.Text))) Then
                                TB_SCMD.Items.Add(FMT(TB_SCMD.Text))
                        End If
                        TB_SCMD.Text = ""
                        Exit Sub
                End If

                'To display the value in Hex format
                If (aLines.Length = 1) Then
                        If (aLines(0).Length > 1) And (aLines(0).Split().Length = 1) And (IsNumeric(aLines(0))) Then
                                'TT.RemoveAll()
                                TT.Show("0x" & Hex(aLines(0)), xRtb)
                                Exit Sub
                        End If

                        If (aLines(0).Split().Length = 7) Then
                                If (aLines(0).Split()(6) = "4k") And IsKernelAddr(aLines(0).Split()(0)) Then
                                        SsaKmem(xRtb, aLines, 0)
                                        Exit Sub
                                End If
                        End If

                        iOff = aLines(0).IndexOf("RIP")
                        If (iOff >= 0) Then
                                iOff = aLines(0).IndexOf("[<", iOff)
                                If (iOff >= 0) Then
                                        sTmp = aLines(0).Substring(iOff + 2, 16)
                                        If (IsKernelAddr(sTmp)) Then
                                                SsaRip(xRtb, aLines, 0, sTmp)
                                                Exit Sub
                                        End If
                                End If
                        End If
                End If

                'To display where is blocked thread
                For i = 0 To aLines.Length - 1 - 2
                        If (aLines(i).IndexOf("tracker = {") <> -1) Then
                                If (aLines(i + 1).IndexOf("caller =") <> -1) Then
                                        If (aLines(i + 2).IndexOf("owner =") <> -1) Then
                                                SsaCallerOwner(xRtb, aLines, i)
                                                Exit Sub
                                        End If
                                End If
                        End If
                Next

                If (aLines.Length = 1) And (aLines(0).IndexOf("PID:") <> -1) And (aLines(0).IndexOf("CPU:") <> -1) Then
                        If (aLines(0)(0) = "[") Then
                                SsaPs(xRtb, aLines, 0)
                                Exit Sub
                        End If
                End If

                If (aLines.Length = 1) Then
                        Dim sStr = aLines(0)

                        sStr = FMT(sStr)
                        aCells = sStr.Split()
                        If ((aCells(0) = "struct") And (aCells.Length = 3)) Or (aCells.Length = 2) Then
                                If IsKernelAddr(aCells(aCells.Length - 1)) Then
                                        SsaStruct(xRtb, aLines, 0)
                                        Exit Sub
                                End If
                        End If
                End If

                If (aLines(0).IndexOf("struct list_head {") <> -1) Then
                        SsaList(xRtb, aLines, 0)
                        Exit Sub
                End If

                If ((aLines(0).IndexOf("crash") <> -1) And (aLines(0).IndexOf("list ") <> -1)) Then
                        If (TB_SCMD.Text.Length > 5) Then
                                If (TB_SCMD.Text.Split(".").Length = 2) Then
                                        SsaListCmd(xRtb, aLines, 0)
                                        Exit Sub
                                End If
                        End If
                End If

                If (IsKernelAddr(aLines(0))) Then
                        If (TB_SCMD.Text.Length > 5) Then
                                If (TB_SCMD.Text.Split(".").Length = 2) Then
                                        For i = 0 To gLines.Length - 1
                                                If (gLines(i) = aLines(0)) Then
                                                        For j = i To 0 Step -1
                                                                If (gLines(j).IndexOf("crash") <> -1) Then
                                                                        If (gLines(j).IndexOf("list ") <> -1) Then
                                                                                SsaListCmd(xRtb, aLines, 0)
                                                                        End If
                                                                        Exit For
                                                                End If
                                                        Next
                                                        Exit For
                                                End If
                                        Next
                                End If
                        End If
                End If

                'kmem -S
                If ((aLines(0).IndexOf("crash") <> -1) And (aLines(0).IndexOf("kmem -S ") <> -1)) Then
                        If (TB_SCMD.Text.Length > 5) Then
                                If (TB_SCMD.Text.Split(".").Length = 1) Then
                                        SsaListKmem(xRtb, aLines, 0)
                                        Exit Sub
                                End If
                        End If
                End If

                'kmem -S
                If (TB_SCMD.Text.Length > 5) Then
                        If (TB_SCMD.Text.Split(".").Length = 1) Then
                                For i = 0 To gLines.Length - 1
                                        If (FMT(gLines(i)) = FMT(aLines(0))) Then
                                                For j = i To 0 Step -1
                                                        If (gLines(j).IndexOf("crash") <> -1) Then
                                                                If (gLines(j).IndexOf("kmem -S ") <> -1) Then
                                                                        SsaListKmem(xRtb, aLines, 0)
                                                                End If
                                                                Exit For
                                                        End If
                                                Next
                                                Exit For
                                        End If
                                Next
                        End If
                End If

        End Sub

        Function IsKernelAddr(ByVal xStr As String) As Boolean
                If (xStr.Length < 16) Then
                        Return (False)
                End If
                xStr = xStr.ToLower()
                If (xStr.IndexOf("ff") = 0) Or (xStr.IndexOf("0xff") = 0) Then
                        Return (True)
                Else
                        Return (False)
                End If
        End Function

        Sub SsaStruct(ByRef xRtb As RichTextBox, ByRef xLines() As String, ByVal xLidx As Integer)
                Dim sStr = xLines(0)
                Dim aCells() As String
                Dim sCmd As String

                sStr = FMT(sStr)
                aCells = sStr.Split()
                If ((aCells(0) = "struct") And (aCells.Length = 3)) Or (aCells.Length = 2) Then
                        If IsKernelAddr(aCells(aCells.Length - 1)) Then
                                sCmd = sStr
                                If (Not IsThisExecuted(gLines, sCmd)) Then
                                        LogOne("This command has been exectued yet. Please re-SAA: " & sCmd)
                                        Exit Sub
                                End If

                                Dim aRval() As String
                                If (aCells.Length = 2) Then
                                        aRval = GetStructTxt(aCells(0), aCells(1))
                                Else
                                        aRval = GetStructTxt(aCells(0) & " " & aCells(1), aCells(2))
                                End If

                                If (xRtb Is RTB_MORE) Then
                                        SetLines(RTB_MORE2, aRval)
                                Else
                                        SetLines(RTB_MORE, aRval)
                                End If
                                aRval = Nothing
                        End If
                End If
        End Sub

        Sub SsaPs(ByRef xRtb As RichTextBox, ByRef xLines() As String, ByVal xLidx As Integer)
                Dim sStr As String
                Dim iEnd As Integer
                Dim ulMy As ULong
                Dim sAddr As String
                Dim aCells() As String
                Dim sCmd As String

                sStr = xLines(0)
                iEnd = sStr.IndexOf("]")
                ulMy = sStr.Substring(1, iEnd - 1)

                aCells = sStr.Split()
                sAddr = aCells(5)
                sCmd = "bt " & sAddr
                If (Not IsThisExecuted(gLines, sCmd)) Then
                        LogOne("sCmd is not executed yet, please re-run it again " & sCmd)
                        Exit Sub
                End If

                For i = 2 To gLines.Length - 1
                        If (gLines(i - 1).IndexOf("ps -l") <> -1) And (gLines(i).IndexOf("PID:") <> -1) And (gLines(i).IndexOf("CPU:") <> -1) Then
                                Dim ulBase As ULong
                                Dim aSchedule(1) As String
                                Dim aRval() As String

                                sStr = gLines(i)
                                iEnd = sStr.IndexOf("]")
                                ulBase = sStr.Substring(1, iEnd - 1)
                                aSchedule(0) = sStr
                                aSchedule(1) = "It's scheduled around " & ((ulBase - ulMy) / 1000 / 1000) & "ms."
                                aRval = GetLogTxt(gLines, sCmd, sCmd, Nothing)
                                If (xRtb Is RTB_MORE) Then
                                        SetLines(RTB_MORE2, ArrayAdd(aSchedule, aRval))
                                Else
                                        SetLines(RTB_MORE, ArrayAdd(aSchedule, aRval))
                                End If
                                aSchedule = Nothing
                                aRval = Nothing
                                Exit Sub
                        End If
                Next
        End Sub

        Function Array2Str(ByRef xAstr() As String) As String
                Dim sRval As String

                sRval = ""
                For i = 0 To xAstr.Length - 1
                        sRval = xAstr(i) & vbCrLf
                Next
                Return (sRval)
        End Function

        Function ArrayAdd(ByRef xAa() As String, ByRef xAb() As String) As String()
                Dim aRval() As String

                ReDim aRval(xAa.Length + xAb.Length - 1)
                For i = 0 To xAa.Length - 1
                        aRval(i) = xAa(i)
                Next
                For i = 0 To xAb.Length - 1
                        aRval(i + xAa.Length) = xAb(i)
                Next
                Return (aRval)
        End Function

        Sub ShiftMore(ByRef xRtb As RichTextBox)
                Dim aLines() As String

                If (xRtb Is RTB_MORE2) Then
                        If (Not (gMore2Lines Is Nothing)) Then
                                aLines = RTB_MORE2.Lines
                                RTB_MORE2.Lines = gMore2Lines
                                gMore2Lines = aLines
                                aLines = Nothing
                        End If
                Else
                        If (Not (gMoreLines Is Nothing)) Then
                                aLines = RTB_MORE.Lines
                                RTB_MORE.Lines = gMoreLines
                                gMoreLines = aLines
                                aLines = Nothing
                        End If
                End If
        End Sub

        Sub SetLines(ByRef xRtb As RichTextBox, ByRef xAlines() As String)
                If (xRtb Is RTB_MORE) Then
                        If (Not (xRtb.Lines Is Nothing)) Then
                                gMoreLines = xRtb.Lines
                        End If
                Else
                        If (Not (xRtb.Lines Is Nothing)) Then
                                gMore2Lines = xRtb.Lines
                        End If
                End If

                If (xAlines(0) <> "") Then
                        If (xAlines(0).Split()(0) = "LINE") Then
                                If (xAlines.Length > 6) Then
                                        If (xAlines(6).IndexOf("sym ") = 0) Then
                                                'Replace non-printable characters
                                                xAlines(5) = "liur9 bash$ echo " & xAlines(6)
                                        End If
                                End If
                        End If
                End If
                xRtb.Lines = xAlines
        End Sub

        Const RING_REQ = 0
        Const RING_RSP = 1
        Const RING_ATIO = 2
        Function MakeRingList(ByVal xHaAddr As String, ByVal xRingType As Integer, ByVal xFree As Boolean, ByRef xList As List(Of String)) As String
                Dim sRingBase As String
                Dim sRingPtr As String
                Dim sRingLen As String
                Dim sRingIdx As String

                Dim sMemBase As String
                Dim sMemPtr As String
                Dim sMemLen As String
                Dim sMemIdx As String

                Dim sIocbSize As String
                Dim sStruct As String

                xList.Clear()
                If (xRingType = RING_REQ) Then
                        sMemBase = "request_ring"
                        sMemPtr = "request_ring_ptr"
                        sMemLen = "request_q_length"
                        sMemIdx = "req_ring_index"
                        sIocbSize = GetSize("request_t")
                        sStruct = "request_t"
                ElseIf (xRingType = RING_RSP) Then
                        sMemBase = "response_ring"
                        sMemPtr = "response_ring_ptr"
                        sMemLen = "response_q_length"
                        sMemIdx = "rsp_ring_index"
                        sIocbSize = GetSize("response_t")
                        sStruct = "response_t"
                ElseIf (xRingType = RING_ATIO) Then
                        sMemBase = "atio_ring"
                        sMemPtr = "atio_ring_ptr"
                        sMemLen = "atio_q_length"
                        sMemIdx = "atio_ring_index"
                        sIocbSize = GetSize("atio_t")
                        sStruct = "atio_t"
                Else
                        Return (Nothing)
                End If
                If (sIocbSize Is INVALID_VAL) Then
                        Return (Nothing)
                End If
                sRingBase = GetVal(xHaAddr, "struct scsi_qla_host", sMemBase)
                sRingPtr = GetVal(xHaAddr, "struct scsi_qla_host", sMemPtr)
                sRingLen = GetVal(xHaAddr, "struct scsi_qla_host", sMemLen)
                sRingIdx = GetVal(xHaAddr, "struct scsi_qla_host", sMemIdx)
                If (sRingBase = INVALID_VAL) Or (sRingPtr = INVALID_VAL) Then
                        Return (Nothing)
                End If
                If (sRingLen = INVALID_VAL) Or (sRingIdx = INVALID_VAL) Then
                        Return (Nothing)
                End If

                If (Not IsNumeric(TB_CBN.Text)) Then
                        Return (Nothing)
                End If
                For i = 0 To (TB_CBN.Text - 1)
                        If (xFree) Then
                                If (sRingIdx = 0) Then
                                        sRingIdx = sRingLen - 1
                                        sRingPtr = JIA(sRingBase, sRingIdx * sIocbSize)
                                End If
                                sRingIdx = sRingIdx - 1
                                sRingPtr = JIA(sRingBase, sRingIdx * sIocbSize)
                                xList.Add(sRingPtr)
                                IsThisExecuted(gLines, "rd -8 " & sRingPtr & " 64")
                        Else
                                If (sRingIdx = sRingLen) Then
                                        sRingIdx = 0
                                        sRingPtr = sRingBase
                                End If
                                xList.Add(sRingPtr)
                                sRingIdx = sRingIdx + 1
                                sRingPtr = JIA(sRingBase, sRingIdx * sIocbSize)
                                IsThisExecuted(gLines, "rd -8 " & sRingPtr & " 64")
                        End If
                Next
                Return (sStruct)
        End Function

        Function GetSize(ByVal xStructName As String) As Integer
                Dim iStt As Integer
                Dim iEnd As Integer
                Dim sCmd As String

                xStructName = FMT(xStructName)
                sCmd = FMT(xStructName) & " -o"
                If (Not IsThisExecuted(gLines, sCmd)) Then
                        Return (INVALID_VAL)
                End If

                iStt = GetFirstLineId(gLines, sCmd & " XXSTT")
                iEnd = GetFirstLineId(gLines, sCmd & " XXEND")
                If (iStt < 0) Or (iEnd < 0) Or (iEnd < iStt) Then
                        Return (INVALID_VAL)
                End If

                For i = iEnd To iStt Step -1
                        If (gLines(i).IndexOf("SIZE: ") = 0) Then
                                Return (gLines(i).Split()(1))
                        End If
                Next
                Return (INVALID_VAL)
        End Function

        Sub CheckIocb(ByVal xRingType As Integer, ByVal xFree As Boolean)
                Dim sHaAddr As String
                If (CB_SQH.Text.Length > 16) Then
                        sHaAddr = CB_SQH.Text.Split()(0)
                        If (Not IsKernelAddr(sHaAddr)) Then
                                Exit Sub
                        End If
                Else
                        Exit Sub
                End If

                Dim oList As New List(Of String)
                Dim aAll() As String
                Dim sStruct As String

                sStruct = MakeRingList(sHaAddr, xRingType, xFree, oList)
                If (sStruct = Nothing) Then
                        LogOne("Please re-run it to check the result")
                        Exit Sub
                End If
                If (oList.Count > 0) Then
                        aAll = GetStructTxt(sStruct, oList.Item(0))
                        aAll = ArrayAdd(aAll, GetLogTxt(gLines, "rd -8 " & oList.Item(0) & " 64", "rd -8 " & oList.Item(0) & " 64", Nothing))
                Else
                        LogOne("Didn't get any IOCB, please re-run it to check the result")
                        Exit Sub
                End If
                For i = 1 To oList.Count - 1
                        aAll = ArrayAdd(aAll, GetStructTxt(sStruct, oList.Item(i)))
                        aAll = ArrayAdd(aAll, GetLogTxt(gLines, "rd -8 " & oList.Item(i) & " 64", "rd -8 " & oList.Item(i) & " 64", Nothing))
                Next
                SetLines(RTB_MORE, aAll)
                aAll = Nothing
        End Sub


        '
        '>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>Message Handling
        '
        Private Sub BT_CMD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BT_CMD.Click
                Dim aOcells() As String
                Dim aNcells() As String
                Dim sNew As String = ""
                Dim sOld As String = ""
                Dim bIsStruct As Boolean
                Dim bOld As Boolean

                If (Not gBasicReady) Or ((gLastTick + 10) > gTick) Then
                        LogOne("Please hold on to run next command: " & gBasicReady & " " & gTick & "-" & gLastTick)
                        Exit Sub
                Else
                        gLastTick = gTick
                End If

                bIsStruct = IS_STRUCT.Checked
                IS_STRUCT.Checked = False
                If (TB_CMD.Text.Length < 2) Then
                        LogOne("命令太短" & TB_CMD.Text)
                        Exit Sub
                End If

                gMutex.WaitOne()
                sNew = FMT(TB_CMD.Text)
                aNcells = sNew.Split()
                For i = 0 To TB_CMD.Items.Count - 1
                        sOld = FMT(TB_CMD.Items(i))
                        aOcells = sOld.Split()

                        If (aOcells.Length <> aNcells.Length) Then
                                Continue For
                        End If

                        bOld = True
                        For j = 0 To aNcells.Count - 1
                                If (aOcells(j) <> aNcells(j)) Then
                                        bOld = False
                                        Exit For
                                End If
                        Next

                        If (bOld) Then
                                Exit For
                        End If
                Next

                If (Not bOld) Then
                        gMutex.WaitOne()
                        If (Not TB_CMD.Items.Contains(sNew)) Then
                                TB_CMD.Items.Add(sNew)
                        End If
                        gMutex.ReleaseMutex()
                        If (aNcells(0) = "list") And (aNcells.Length > 3) Then
                                'This compound command (devided into several small commands.)
                                Dim sCmd As String

                                sCmd = (aNcells(0) & " " & aNcells(1))
                                If (IsThisExecuted(gLines, sCmd)) Then
                                        Dim iOff As Integer
                                        Dim oAddrList As New List(Of String)
                                        Dim sStructName As String
                                        Dim sMemName As String
                                        Dim bAllDone As String

                                        If (aNcells.Length = 4) Then
                                                sStructName = aNcells(2)
                                                sMemName = aNcells(3)
                                        Else
                                                sStructName = aNcells(2) & " " & aNcells(3)
                                                sMemName = aNcells(4)

                                        End If
                                        iOff = GetOff(sStructName, sMemName)
                                        If (iOff < 0) Then
                                                LogOne("Cann't find the offset for: " & aNcells(2) & "->" & aNcells(3))
                                                gMutex.ReleaseMutex()
                                                Exit Sub
                                        End If

                                        bAllDone = True
                                        MakeObjList(aNcells(1), iOff, oAddrList)
                                        For Each sAddr In oAddrList
                                                If (Not IsThisExecuted(gLines, sStructName & " " & sAddr) < 0) Then
                                                        bAllDone = False
                                                End If
                                        Next
                                End If
                        ElseIf (IsThisExecuted(gLines, sNew)) Then
                                'To do nothing is OK here, but you can return the result.
                        End If
                        LogOne("New command has been issued, please re-run it to check the result: " & sNew)
                Else
                        'This is one old cmd, we should get it locally.
                        If (bIsStruct) Then
                                If (aNcells.Length > 2) Then
                                        LogOne("This implicit STRUCT command has more arguments: " & sNew)
                                Else
                                        SetLines(RTB_MORE2, GetStructTxt(aNcells(0), aNcells(1)))
                                End If
                        ElseIf (aNcells(0) = "struct") Then
                                If (aNcells.Length > 3) Then
                                        LogOne("This explicit STRUCT command has more arguments: " & sNew)
                                Else
                                        SetLines(RTB_MORE2, GetStructTxt("struct " & aNcells(1), aNcells(2)))
                                End If
                        ElseIf (aNcells(0) = "sym") Then
                                If (aNcells.Length > 2) Then
                                        LogOne("This SYM command has more arguments: " & sNew)
                                Else
                                        SetLines(RTB_MORE2, GetSymTxt(aNcells(1)))
                                End If
                        ElseIf (aNcells(0) = "list") And (aNcells.Length > 3) Then
                                If (aNcells.Length = 4) Then
                                        SetLines(RTB_MORE2, GetListTxt(aNcells(1), aNcells(2), aNcells(3)))
                                ElseIf (aNcells.Length = 5) Then
                                        SetLines(RTB_MORE2, GetListTxt(aNcells(1), aNcells(2) & " " & aNcells(3), aNcells(4)))
                                Else
                                        LogOne("This LIST command has more arguments: " & sNew)
                                End If
                                'Compound command should be added into white list.
                        Else
                                SetLines(RTB_MORE2, GetLogTxt(gLines, sNew, sNew, Nothing))
                        End If
                End If
                gMutex.ReleaseMutex()
        End Sub

        Sub SetLogDir()
                Dim sRoot As String
                Dim aCells() As String

                sRoot = Application.StartupPath
                aCells = sRoot.Split("\")
                sRoot = ""
                For i = 0 To aCells.Length - 1
                        sRoot &= (aCells(i) + "\")
                        If (File.Exists(sRoot & "log\vb_restore.sh")) Then
                                gLogDir = sRoot & "log\"
                                Exit Sub
                        End If
                Next
                gLogDir = Application.StartupPath & "\log\"
                LogOne("log\vb_restore.sh not found. Backup/restore log will not work well. Please try to get right scripts.")
        End Sub

        Dim gLogDir As String
        Dim gExiting As Boolean = False
        Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
                CheckForIllegalCrossThreadCalls = False '允许在线程中访问控件

                gLines = RvalA()
                gCodes = RvalA()
                gFmCfg.Init()

                InitLog(DgvLog)
                LogOne("初始化完毕")

                TM.Start()

                'gLogDir = (Application.StartupPath.Substring(0, Application.StartupPath.IndexOf(Me.Text) + 10) & "\log\")
                SetLogDir()
                LogOne(gLogDir)
                LB_LOGDIR.Text = gLogDir
                If (Not Directory.Exists(gLogDir)) Then
                        Directory.CreateDirectory(gLogDir)
                End If

                For Each sFn In System.IO.Directory.GetFiles(gLogDir, "*.*")
                        If (sFn.IndexOf("crashcmd.log_") <> -1) Then
                                Dim iOff As Integer
                                iOff = sFn.IndexOf("crashcmd.log_")
                                CB_NODE.Items.Add(sFn.Substring(iOff + 13))
                        End If
                Next

                'gFileDlg.InitialDirectory = Environment.SpecialFolder.Recent
                'gFileDlg.Filter = "CRASH-LOG(*.log)"

                LogOne("Today is week no. " & CInt(CStr(DateDiff(DateInterval.Day, CDate("2015-01-01"), Now())) / 7))
        End Sub

        'Analysis phase
        Dim gQlaPhase As Integer = 0
        Dim gScstPhase As Integer = 0
        Dim gVhbaPhase As Integer = 0

        Dim gTick As Integer = 0        '程序级时钟
        Dim gTiming As Boolean = False  '确保定时器不重入

        Dim gLines() As String
        Dim gCodes() As String
        Dim gMutex As New Threading.Mutex(False)

        Dim gCrashWnd As Integer = 0
        Dim gCodeWnd As Integer = 0

        Dim gMoreTick As Integer = -99
        Dim gMore2Tick As Integer = -99
        Dim gHtCmd As New Hashtable
        Dim gLastLength As Integer = 0

        Dim gThread As Threading.Thread

        Sub CopyBack()
                RunShell(LB_LOGDIR.Text & "vb_copyback.sh")
        End Sub

        '1/10s
        Private Sub TM_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TM.Tick
                Dim aLines() As String

                '确保为单ENTRY
                gTick += 1
                If (gTiming) Then
                        Exit Sub
                Else
                        gTiming = True
                End If

                'To check if need do SSA
                If (gTick = (gMoreTick + 2)) Then
                        DoSsa(RTB_MORE)
                        gMoreTick = 0
                End If

                If (gTick = (gMore2Tick + 2)) Then
                        DoSsa(RTB_MORE2)
                        gMore2Tick = 0
                End If

                'To set WindowsHandles
                If ((gTick Mod 30) = 1) Then
                        SetWnds(gTick / 10)  'Set window handles
                End If

                If ((gTick Mod 4000) = 1) Then
                        LogOne("gTick = " & gTick & " gQlaPhase = " & gQlaPhase & " gLines.Length = " & gLines.Length)
                End If

                'To start offical handling per second
                If ((gTick Mod 20) = 1) And (File.Exists(gLogDir & "crashcmd.log")) Then
                        gMutex.WaitOne()

                        'Code
                        If (Not File.Exists(gLogDir & "autocode.log")) Then
                                My.Computer.FileSystem.WriteAllText(gLogDir & "autocode.log", "", True)
                        End If
                        File.Copy(gLogDir & "autocode.log", gLogDir & "autocode.log.copy", True)
                        gCodes = File.ReadAllLines(gLogDir & "autocode.log.copy", System.Text.Encoding.Default)

                        'Log
                        File.Copy(gLogDir & "crashcmd.log", gLogDir & "crashcmd.log.copy", True)
                        aLines = File.ReadAllLines(gLogDir & "crashcmd.log.copy", System.Text.Encoding.Default)
                        ReDim gLines(0)
                        gLines(0) = "Cleared manually"
                        For i = aLines.Length - 1 To 0 Step -1
                                If (aLines(i).IndexOf("version XXEND") <> -1) Then
                                        '注意，哪些echo类的基本上都不可能被放到我们的gLines中
                                        ReDim gLines(aLines.Length - 1 - i)
                                        For j = 0 To aLines.Length - 1 - i
                                                gLines(j) = aLines(i + j)
                                        Next
                                        File.WriteAllLines(gLogDir & "crashcmd.log.copy", gLines, System.Text.Encoding.Default)

                                        'gMutex.WaitOne()
                                        If (gQlaPhase > 2) And ((gTick Mod 2000) = 1) Then
                                                LogOne("rn vb_copyback.sh now")
                                                gThread = New Thread(AddressOf CopyBack)
                                                gThread.IsBackground = True
                                                gThread.Start()
                                        End If
                                        If (i <> 0) Then
                                                gLastLength = 0
                                                CB_SQH.Items.Clear()
                                                CB_SQH.Update()
                                                gHtCmd.Clear()
                                                TB_CMD.Items.Clear()
                                                TB_CMD.Update()
                                                gBasicReady = False
                                                gQlaPhase = 1
                                        End If
                                        'gMutex.ReleaseMutex()
                                        Exit For
                                End If
                        Next
                        aLines = Nothing

                        Dim iStt As Integer
                        iStt = gLastLength
                        If (gLastLength > gLines.Length) Then
                                iStt = 0        'vb_clear_log.sh
                        End If
                        If (iStt > 10) Then
                                iStt -= 5
                        End If
                        For i = iStt To gLines.Length - 1
                                If (i < 0) Then
                                        Continue For
                                End If

                                If (gLines(i).IndexOf(" XX") >= 0) Then
                                        If (Not gHtCmd.ContainsKey(FMT(gLines(i)))) Then
                                                If (gLines(i).IndexOf(" XXSTT") >= 0) Then
                                                        gHtCmd.Add(FMT(gLines(i)), i)
                                                End If
                                                If (gLines(i).IndexOf(" XXEND") >= 0) Then
                                                        gHtCmd.Add(FMT(gLines(i)), i)

                                                        Dim sCmd As String
                                                        gLines(i) = FMT(gLines(i))
                                                        sCmd = ""
                                                        For j = 0 To gLines(i).Split().Length - 2
                                                                sCmd = (sCmd & " " & gLines(i).Split()(j))
                                                        Next
                                                        sCmd = FMT(sCmd)
                                                        If (Not TB_CMD.Items.Contains(sCmd)) Then
                                                                TB_CMD.Items.Add(sCmd)
                                                        End If
                                                End If
                                        End If
                                End If
                        Next

                        If (gLines.Length > 100) And (GetFirstLineId(gLines, "list qla_ha_list XXEND") >= 0) Then
                                'To ensure there are no stuck GDB commands
                                'Start to do something
                                If (gQlaPhase = 0) Then
                                        If (BasicReady()) Then
                                                gQlaPhase += 1
                                        End If
                                Else
                                        If (gQlaPhase = 1) Then
                                                BasicReady()
                                        End If
                                        DoQlaAnalysis() 'Always as high priority tasks.
                                End If
                        End If

                        gMutex.ReleaseMutex()
                        gLastLength = gLines.Length
                End If

                If ((gTick Mod 900) = 399) Then
                        BackupLog()
                End If

OUT:            '确保为单ENTRY
                gTiming = False
        End Sub

        Private Sub BT_START_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BT_START.Click
                If ((gLastTick + 10) > gTick) Then
                        LogOne("Please hold on to run next command: " & gBasicReady & " " & gTick & "-" & gLastTick)
                        Exit Sub
                Else
                        gLastTick = gTick
                End If
                If (Not File.Exists(gLogDir & "crashcmd.log")) Then
                        LogOne("There is no log file ready to be analyzed. Please re-select the right log file.")
                        Exit Sub
                Else
                        gMutex.WaitOne()
                        gLastLength = 0
                        RunShell(LB_LOGDIR.Text & "vb_clear_log.sh")
                        'My.Computer.FileSystem.WriteAllText(gLogDir & "crashcmd.log", "", False) 
                        'My.Computer.FileSystem.WriteAllText(gLogDir & "autocode.log", "", False) 
                        CB_SQH.Items.Clear()
                        CB_SQH.Update()
                        gHtCmd.Clear()
                        TB_CMD.Items.Clear()
                        TB_CMD.Update()

                        gBasicReady = False
                        gQlaPhase = 0
                        gMutex.ReleaseMutex()
                End If

                'LogOne("exit now " & gHtCmd.Count)
                'Exit Sub

                '开始第一阶段的处理
                SendString(gCrashWnd, "version", Nothing)
                SendString(gCrashWnd, "mod -S", Nothing)

                SendString(gCrashWnd, "sys", Nothing)
                SendString(gCrashWnd, "pwd", Nothing)
                SendString(gCrashWnd, "ls", Nothing)
                SendString(gCrashWnd, "mach", Nothing)

                SendString(gCrashWnd, "dmesg", Nothing)

                'SendString(gCrashWnd, "runq", Nothing)'it could cause hang in live DDR

                SendString(gCrashWnd, "ascii", Nothing)

                SendString(gCrashWnd, "files", Nothing)

                SendString(gCrashWnd, "net", Nothing)

                SendString(gCrashWnd, "mount", Nothing)

                SendString(gCrashWnd, "dev", Nothing)

                'SendString(gCrashWnd, "kmem -s", Nothing)

                'SendString(gCrashWnd, "irq", Nothing)'output is too big in live DDR

                SendString(gCrashWnd, "bt -a", Nothing)

                SendString(gCrashWnd, "ps -l", Nothing)

                SendString(gCrashWnd, "_text", Nothing)

                SendString(gCrashWnd, "sym printk", Nothing)

                'vhba related
                SendString(gCrashWnd, "p vhba_queue_list", Nothing)

                SendString(gCrashWnd, "p vhba_options_list", Nothing)

                SendString(gCrashWnd, "vhba_t -o", Nothing)

                SendString(gCrashWnd, "rd vhba_list", Nothing)

                'SendString(gWnd, "echo p vhba_list XXSTT")
                'SendString(gWnd, "p vhba_list")
                'SendString(gWnd, "echo p vhba_list XXEND")

                'qla2xxx_ts related
                SendString(gCrashWnd, "struct scsi_qla_host -o", Nothing)

                '这个必须不可以分割，否则后面有些东西得改动
                SendString(gCrashWnd, "list qla_hostlist", Nothing)
                SendString(gCrashWnd, "list qla_ha_list", Nothing)
        End Sub

        Private Sub CB_SQH_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CB_SQH.TextChanged
                Dim aCells() As String

                aCells = CB_SQH.Text.Split()
                If (aCells.Count > 0) Then
                        If (aCells(0).Length >= 16) Then
                                SetLines(RTB_MORE, GetStructTxt("struct scsi_qla_host", aCells(0)))
                        End If
                End If
        End Sub

        Private Sub BT_MARK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BT_MARK.Click
                MarkMe(RTB_MORE, TB_KEY.Text)
                If (Not TB_KEY.Items.Contains(FMT(TB_KEY.Text))) Then
                        TB_KEY.Items.Add(FMT(TB_KEY.Text))
                End If
        End Sub

        Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
                If (Not gBasicReady) Or ((gLastTick + 10) > gTick) Then
                        LogOne("Please hold on to run next command: " & gBasicReady & " " & gTick & "-" & gLastTick)
                        Exit Sub
                Else
                        gLastTick = gTick
                End If
                SetLines(RTB_MORE, GetLogTxt(gLines, "dmesg", "dmesg", Nothing))
        End Sub

        Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
                Dim aCells() As String

                aCells = CB_SQH.Text.Split()
                If (aCells.Count >= 2) Then
                        If (aCells(0).Length >= 16) Then
                                SetLines(RTB_MORE, GetLogTxt(gLines, "dmesg", "dmesg", aCells(1) & "]:"))
                        End If
                End If
        End Sub

        Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
                If (Not gBasicReady) Or ((gLastTick + 10) > gTick) Then
                        LogOne("Please hold on to run next command: " & gBasicReady & " " & gTick & "-" & gLastTick)
                        Exit Sub
                Else
                        gLastTick = gTick
                End If
                SetLines(RTB_MORE, GetLogTxt(gLines, "sys", "mach", Nothing))
        End Sub

        Private Sub Button3_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
                If (Not gBasicReady) Or ((gLastTick + 10) > gTick) Then
                        LogOne("Please hold on to run next command: " & gBasicReady & " " & gTick & "-" & gLastTick)
                        Exit Sub
                Else
                        gLastTick = gTick
                End If
                SetLines(RTB_MORE, GetLogTxt(gLines, "runq", "runq", Nothing))
        End Sub

        Private Sub Button8_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
                If (Not gBasicReady) Or ((gLastTick + 10) > gTick) Then
                        LogOne("Please hold on to run next command: " & gBasicReady & " " & gTick & "-" & gLastTick)
                        Exit Sub
                Else
                        gLastTick = gTick
                End If
                SetLines(RTB_MORE, GetLogTxt(gLines, "bt -a", "bt -a", Nothing))
        End Sub

        Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
                If (Not gBasicReady) Or ((gLastTick + 10) > gTick) Then
                        LogOne("Please hold on to run next command: " & gBasicReady & " " & gTick & "-" & gLastTick)
                        Exit Sub
                Else
                        gLastTick = gTick
                End If
                SetLines(RTB_MORE, GetLogTxt(gLines, "ps -l", "ps -l", Nothing))
        End Sub

        Private Sub RTB_MORE_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RTB_MORE.SelectionChanged
                If (FMT(RTB_MORE.SelectedText).Length > 2) Then
                        'Save it to clipboard
                        Clipboard.SetText(RTB_MORE.SelectedText)
                        gMoreTick = gTick
                        RTB_MORE2.SelectedText = ""
                Else
                        gMoreTick = 0
                End If
        End Sub

        Private Sub RTB_MORE2_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RTB_MORE2.SelectionChanged
                If (FMT(RTB_MORE2.SelectedText).Length > 2) Then
                        'Save it to clipboard
                        Clipboard.SetText(RTB_MORE2.SelectedText)
                        gMore2Tick = gTick
                        RTB_MORE.SelectedText = ""
                Else
                        gMore2Tick = 0
                End If
        End Sub

        Private Sub BT_MARK2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BT_MARK2.Click
                MarkMe(RTB_MORE2, TB_KEY2.Text)
                If (Not TB_KEY2.Items.Contains(FMT(TB_KEY2.Text))) Then
                        TB_KEY2.Items.Add(FMT(TB_KEY2.Text))
                End If
        End Sub

        Private Sub BT_OPT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BT_OPT.Click
                If (Not gBasicReady) Or ((gLastTick + 10) > gTick) Then
                        LogOne("Please hold on to run next command: " & gBasicReady & " " & gTick & "-" & gLastTick)
                        Exit Sub
                Else
                        gLastTick = gTick
                End If
                SetLines(RTB_MORE, GetLogTxt(gLines, "p vhba_options_list", "p vhba_options_list", Nothing))
        End Sub

        Private Sub BT_QUE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BT_QUE.Click
                If (Not gBasicReady) Or ((gLastTick + 10) > gTick) Then
                        LogOne("Please hold on to run next command: " & gBasicReady & " " & gTick & "-" & gLastTick)
                        Exit Sub
                Else
                        gLastTick = gTick
                End If
                SetLines(RTB_MORE, GetLogTxt(gLines, "p vhba_queue_list", "p vhba_queue_list", Nothing))
        End Sub

        Private Sub BT_VTL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BT_VTL.Click
                SetLines(RTB_MORE, GetVhba(1))
        End Sub

        Private Sub BT_ALL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BT_ALL.Click
                SetLines(RTB_MORE, gLines)
        End Sub

        Private Sub BT_DFC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BT_DFC.Click
                SetLines(RTB_MORE, GetVhba(0))
        End Sub

        Private Sub BT_VDISK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BT_VDISK.Click
                SetLines(RTB_MORE, GetVhba(2))
        End Sub

        Private Sub Button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button10.Click
                If (Not gBasicReady) Or ((gLastTick + 10) > gTick) Then
                        LogOne("Please hold on to run next command: " & gBasicReady & " " & gTick & "-" & gLastTick)
                        Exit Sub
                Else
                        gLastTick = gTick
                End If
                If TB_YES.Text <> "yes" Then
                        LogOne("TB_YES没有设置")
                        Exit Sub
                End If
                TB_YES.Text = ""
                If MsgBox("这需要花费大约10分钟时间，你确认以前没有执行过？", MsgBoxStyle.YesNo, "确认") <> MsgBoxResult.Yes Then
                        Exit Sub
                End If
                'it will take about 10 minutes
                SendString(gCrashWnd, "vhba_t " & GetVhbaAddr(0), Nothing)
                SendString(gCrashWnd, "vhba_t " & GetVhbaAddr(1), Nothing)
                SendString(gCrashWnd, "vhba_t " & GetVhbaAddr(2), Nothing)
        End Sub

        Sub BackupLog()
                Dim info As New FileInfo(gLogDir & "crashcmd.log")
                If (info.Length > 2048) And (gLines.Length > 100) Then
                        File.Copy(gLogDir & "crashcmd.log", gLogDir & "crashcmd.log" & "_" & GetNodeDdr(), True)
                        File.Copy(gLogDir & "autocode.log", gLogDir & "autocode.log" & "_" & GetNodeDdr(), True)
                        LogOne(gLogDir & "crashcmd.log" & "_" & GetNodeDdr())
                        LogOne(gLogDir & "autocode.log" & "_" & GetNodeDdr())
                        If (Not CB_NODE.Items.Contains(GetNodeDdr())) Then
                                CB_NODE.Items.Add(GetNodeDdr())
                        End If
                End If
        End Sub

        Private Sub BT_BKLG_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BT_BKLG.Click
                BackupLog()
        End Sub

        Private Sub Button12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button12.Click
                If (Not gBasicReady) Or ((gLastTick + 10) > gTick) Then
                        LogOne("Please hold on to run next command: " & gBasicReady & " " & gTick & "-" & gLastTick)
                        Exit Sub
                Else
                        gLastTick = gTick
                End If
                'basic simple global variables
                SendString(gCrashWnd, "p scst_trace_flag", Nothing)             '<--
                SendString(gCrashWnd, "p scst_max_tasklet_cmd", Nothing)
                SendString(gCrashWnd, "p scst_flags", Nothing)
                SendString(gCrashWnd, "p scst_threads", Nothing)
                SendString(gCrashWnd, "p  suspend_count", Nothing)
                SendString(gCrashWnd, "p scst_max_itl_queue_depth", Nothing)
                SendString(gCrashWnd, "p scst_max_dev_cmd_mem", Nothing)
                SendString(gCrashWnd, "p scst_max_cmd_mem", Nothing)
                SendString(gCrashWnd, "p scst_major", Nothing)
                SendString(gCrashWnd, "p scst_vlun0_id", Nothing)
                SendString(gCrashWnd, "p default_cpu_mask", Nothing)
                SendString(gCrashWnd, "p scst_virt_dev_last_id", Nothing)
                SendString(gCrashWnd, "p scst_default_acg", Nothing)            '<--

                'mutex
                SendString(gCrashWnd, "p scst_mutex", Nothing)                  '<--
                SendString(gCrashWnd, "p scst_mutex2", Nothing)
                SendString(gCrashWnd, "p scst_init_dev_mutex", Nothing)
                SendString(gCrashWnd, "p scst_suspend_mutex", Nothing)
                SendString(gCrashWnd, "p scst_cmd_threads_mutex", Nothing)      '<--

                'lock
                SendString(gCrashWnd, "p scst_main_lock", Nothing)      '<--
                SendString(gCrashWnd, "p scst_init_lock", Nothing)
                SendString(gCrashWnd, "p scst_mcmd_lock", Nothing)
                SendString(gCrashWnd, "p scst_mgmt_lock", Nothing)      '<--

                'waitq
                SendString(gCrashWnd, "p scst_init_cmd_list_waitQ", Nothing)
                SendString(gCrashWnd, "p scst_mgmt_cmd_list_waitQ", Nothing)    '<--

                'list
                SendString(gCrashWnd, "list scst_cmd_threads_list", Nothing)       '<--
                SendString(gCrashWnd, "list scst_dev_list", Nothing)
                SendString(gCrashWnd, "list scst_template_list", Nothing)
                SendString(gCrashWnd, "list scst_dev_type_list", Nothing)
                SendString(gCrashWnd, "list scst_virtual_dev_type_list", Nothing)
                SendString(gCrashWnd, "list scst_acg_list", Nothing)
                SendString(gCrashWnd, "list scst_init_cmd_list", Nothing)
                SendString(gCrashWnd, "list scst_active_mgmt_cmd_list", Nothing)
                SendString(gCrashWnd, "list scst_delayed_mgmt_cmd_list", Nothing)
                SendString(gCrashWnd, "list scst_sess_init_list", Nothing)
                SendString(gCrashWnd, "list scst_sess_shut_list", Nothing)         '<--

                'threads/percpu
                SendString(gCrashWnd, "struct scst_cmd_threads -o", Nothing)            '<--
                SendString(gCrashWnd, "struct scst_percpu_info -o", Nothing)
                SendString(gCrashWnd, "rd scst_percpu_infos", Nothing)
                SendString(gCrashWnd, "p scst_percpu_infos", Nothing)
                SendString(gCrashWnd, "struct scst_cmd_threads scst_main_cmd_threads", Nothing)
                SendString(gCrashWnd, "p scst_init_cmd_thread", Nothing)
                SendString(gCrashWnd, "p scst_init_cmd_thread", Nothing)
                SendString(gCrashWnd, "p scst_mgmt_thread", Nothing)
                SendString(gCrashWnd, "p scst_mgmt_cmd_thread", Nothing)                '<--
        End Sub

        Private Sub BT_FILTER2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BT_FILTER2.Click
                FilterMe(RTB_MORE2, TB_KEY2.Text)
                If (Not TB_KEY2.Items.Contains(FMT(TB_KEY2.Text))) Then
                        TB_KEY2.Items.Add(FMT(TB_KEY2.Text))
                End If
                TB_KEY2.Text = ""       'revert it to empty string
        End Sub

        Private Sub BT_FILTER_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BT_FILTER.Click
                FilterMe(RTB_MORE, TB_KEY.Text)
                If (Not TB_KEY.Items.Contains(FMT(TB_KEY.Text))) Then
                        TB_KEY.Items.Add(FMT(TB_KEY.Text))
                End If
                TB_KEY.Text = ""       'revert it to empty string
        End Sub

        Private Sub BT_BLK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BT_BLK.Click
                gFmCfg.ShowDialog()     'Can't use .Show(), or else the form will be destroied after closing.
        End Sub

        Private Sub BT_CODES_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BT_CODES.Click
                If File.Exists(gLogDir & "autocode.log.copy") Then
                        SetLines(RTB_MORE, File.ReadAllLines(gLogDir & "autocode.log.copy"))
                Else
                        LogOne("File doesn't exist: " & gLogDir & "crashcmd.log.copy")
                End If
        End Sub

        Private Sub BT_BASIC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BT_BASIC.Click
                If (Not gBasicReady) Or ((gLastTick + 10) > gTick) Then
                        LogOne("Please hold on to run next command: " & gBasicReady & " " & gTick & "-" & gLastTick)
                        Exit Sub
                Else
                        gLastTick = gTick
                End If
                SetLines(RTB_MORE, GetLogTxt(gLines, "p scst_trace_flag", "p scst_default_acg", Nothing))
        End Sub

        Private Sub BT_MUTEX_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BT_MUTEX.Click
                If (Not gBasicReady) Or ((gLastTick + 10) > gTick) Then
                        LogOne("Please hold on to run next command: " & gBasicReady & " " & gTick & "-" & gLastTick)
                        Exit Sub
                Else
                        gLastTick = gTick
                End If
                SetLines(RTB_MORE, GetLogTxt(gLines, "p scst_mutex", "p scst_cmd_threads_mutex", Nothing))
        End Sub

        Private Sub BT_LOCK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BT_LOCK.Click
                If (Not gBasicReady) Or ((gLastTick + 10) > gTick) Then
                        LogOne("Please hold on to run next command: " & gBasicReady & " " & gTick & "-" & gLastTick)
                        Exit Sub
                Else
                        gLastTick = gTick
                End If
                SetLines(RTB_MORE, GetLogTxt(gLines, "p scst_main_lock", "p scst_mgmt_lock", Nothing))
        End Sub

        Private Sub BT_LIST_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BT_LIST.Click
                If (Not gBasicReady) Or ((gLastTick + 10) > gTick) Then
                        LogOne("Please hold on to run next command: " & gBasicReady & " " & gTick & "-" & gLastTick)
                        Exit Sub
                Else
                        gLastTick = gTick
                End If
                SetLines(RTB_MORE, GetLogTxt(gLines, "list scst_cmd_threads_list", "list scst_sess_shut_list", Nothing))
        End Sub

        Private Sub BT_WAITQ_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BT_WAITQ.Click
                If (Not gBasicReady) Or ((gLastTick + 10) > gTick) Then
                        LogOne("Please hold on to run next command: " & gBasicReady & " " & gTick & "-" & gLastTick)
                        Exit Sub
                Else
                        gLastTick = gTick
                End If
                SetLines(RTB_MORE, GetLogTxt(gLines, "p scst_init_cmd_list_waitQ", "p scst_mgmt_cmd_list_waitQ", Nothing))
        End Sub

        Private Sub BT_PERCPU_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BT_PERCPU.Click
                If (Not gBasicReady) Or ((gLastTick + 10) > gTick) Then
                        LogOne("Please hold on to run next command: " & gBasicReady & " " & gTick & "-" & gLastTick)
                        Exit Sub
                Else
                        gLastTick = gTick
                End If
                SetLines(RTB_MORE, GetLogTxt(gLines, "struct scst_percpu_info -o", "struct scst_percpu_info -o", Nothing))
        End Sub

        Private Sub BT_TREADS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BT_TREADS.Click
                If (Not gBasicReady) Or ((gLastTick + 10) > gTick) Then
                        LogOne("Please hold on to run next command: " & gBasicReady & " " & gTick & "-" & gLastTick)
                        Exit Sub
                Else
                        gLastTick = gTick
                End If
                SetLines(RTB_MORE, GetLogTxt(gLines, "struct scst_cmd_threads -o", "struct scst_cmd_threads -o", Nothing))
        End Sub

        Private Sub RTB_MORE2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RTB_MORE2.TextChanged
                Dim sTmp As String
                Dim sFile As String = ""
                Dim iLine As Integer = -1

                If (RTB_MORE2.Lines.Length > 0) Then
                        sTmp = RTB_MORE2.Lines(0)
                        If (sTmp.Split()(0) = "LINE") Then
                                iLine = sTmp.Split()(1)
                        End If
                End If

                If ((FMT(TB_CMD.Text)).IndexOf("sym ") = 0) Then
                        'Mark line# to capture your eyes.
                        If (GetCodeCmd(FMT(TB_CMD.Text), sFile, iLine) = Nothing) Then
                                Exit Sub
                        End If
                End If

                If (iLine <> -1) Then
                        MarkMe(RTB_MORE2, CStr(iLine))
                End If
        End Sub

        Dim gLastTick As Integer = 0
        Private Sub BT_KMEM_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BT_KMEM.Click
                If (Not gBasicReady) Or ((gLastTick + 10) > gTick) Then
                        LogOne("Please hold on to run next command: " & gBasicReady & " " & gTick & "-" & gLastTick)
                        Exit Sub
                Else
                        gLastTick = gTick
                End If
                SetLines(RTB_MORE, GetLogTxt(gLines, "kmem -s", "kmem -s", Nothing))
        End Sub

        Private Sub BT_IRQ_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BT_IRQ.Click
                If (Not gBasicReady) Or ((gLastTick + 10) > gTick) Then
                        LogOne("Please hold on to run next command: " & gBasicReady & " " & gTick & "-" & gLastTick)
                        Exit Sub
                Else
                        gLastTick = gTick
                End If
                SetLines(RTB_MORE, GetLogTxt(gLines, "irq", "irq", Nothing))
        End Sub

        Private Sub BT_ASCII_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BT_ASCII.Click
                If (Not gBasicReady) Or ((gLastTick + 10) > gTick) Then
                        LogOne("Please hold on to run next command: " & gBasicReady & " " & gTick & "-" & gLastTick)
                        Exit Sub
                Else
                        gLastTick = gTick
                End If
                SetLines(RTB_MORE, GetLogTxt(gLines, "ascii", "ascii", Nothing))
        End Sub

        Private Sub BT_DEV_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BT_DEV.Click
                If (Not gBasicReady) Or ((gLastTick + 10) > gTick) Then
                        LogOne("Please hold on to run next command: " & gBasicReady & " " & gTick & "-" & gLastTick)
                        Exit Sub
                Else
                        gLastTick = gTick
                End If
                SetLines(RTB_MORE, GetLogTxt(gLines, "dev", "dev", Nothing))
        End Sub

        Private Sub BT_SSA_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BT_SSA.Click
                If (Not gBasicReady) Or ((gLastTick + 10) > gTick) Then
                        LogOne("Please hold on to run next command: " & gBasicReady & " " & gTick & "-" & gLastTick)
                        Exit Sub
                Else
                        gLastTick = gTick
                End If
                If (RTB_MORE.SelectedText.Length > 10) Then
                        DoSsa(RTB_MORE)
                End If
                If (RTB_MORE2.SelectedText.Length > 10) Then
                        DoSsa(RTB_MORE2)
                End If
        End Sub

        Private Sub RTB_MORE2_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles RTB_MORE2.MouseDown
                If (e.Button = MouseButtons.Right) Then
                        ShiftMore(RTB_MORE2)
                End If
        End Sub

        Private Sub RTB_MORE_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles RTB_MORE.MouseDown
                If (e.Button = MouseButtons.Right) Then
                        ShiftMore(RTB_MORE)
                End If
        End Sub

        Private Sub BT_REOFFLINE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BT_REOFFLINE.Click
                gMutex.WaitOne()
                gLastLength = 0
                CB_SQH.Items.Clear()
                CB_SQH.Update()
                gHtCmd.Clear()
                TB_CMD.Items.Clear()
                TB_CMD.Update()
                gBasicReady = False
                gQlaPhase = 1
                gMutex.ReleaseMutex()

                LogOne("Re-Offline initialization just got kicked off.")
        End Sub

        Private Sub BT_RESTORE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BT_RESTORE.Click
                If (CB_NODE.Text.Length > 2) Then
                        gMutex.WaitOne()
                        RunShell(LB_LOGDIR.Text & "vb_restore.sh " & CB_NODE.Text)
                        gLastLength = 0
                        CB_SQH.Items.Clear()
                        CB_SQH.Update()
                        gHtCmd.Clear()
                        TB_CMD.Items.Clear()
                        TB_CMD.Update()
                        gBasicReady = False
                        gQlaPhase = 1
                        gMutex.ReleaseMutex()
                End If
        End Sub

        Private Sub BT_QLA_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BT_QLA.Click

        End Sub

        Private Sub BT_WREQ_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BT_WREQ.Click
                CheckIocb(RING_REQ, False)
        End Sub

        Private Sub BT_WRSP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BT_WRSP.Click
                CheckIocb(RING_RSP, False)
        End Sub

        Private Sub BT_WATIO_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BT_WATIO.Click
                CheckIocb(RING_ATIO, False)
        End Sub

        Private Sub BT_FREQ_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BT_FREQ.Click
                CheckIocb(RING_REQ, True)
        End Sub

        Private Sub BT_FRSP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BT_FRSP.Click
                CheckIocb(RING_RSP, True)
        End Sub

        Private Sub BT_FATIO_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BT_FATIO.Click
                CheckIocb(RING_ATIO, True)
        End Sub

End Class
