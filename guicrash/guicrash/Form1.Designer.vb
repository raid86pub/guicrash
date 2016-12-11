<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows 窗体设计器所必需的
    Private components As System.ComponentModel.IContainer

    '注意: 以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
                Me.components = New System.ComponentModel.Container()
                Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
                Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
                Me.BT_CMD = New System.Windows.Forms.Button()
                Me.TB_WINCLASS = New System.Windows.Forms.TextBox()
                Me.Label1 = New System.Windows.Forms.Label()
                Me.TM = New System.Windows.Forms.Timer(Me.components)
                Me.BT_START = New System.Windows.Forms.Button()
                Me.DgvLog = New System.Windows.Forms.DataGridView()
                Me.CB_SQH = New System.Windows.Forms.ComboBox()
                Me.BT_MARK = New System.Windows.Forms.Button()
                Me.RTB_MORE = New System.Windows.Forms.RichTextBox()
                Me.Button5 = New System.Windows.Forms.Button()
                Me.Button6 = New System.Windows.Forms.Button()
                Me.Button7 = New System.Windows.Forms.Button()
                Me.ComboBox1 = New System.Windows.Forms.ComboBox()
                Me.Button3 = New System.Windows.Forms.Button()
                Me.Button8 = New System.Windows.Forms.Button()
                Me.Button9 = New System.Windows.Forms.Button()
                Me.TB_CMD = New System.Windows.Forms.ComboBox()
                Me.RTB_MORE2 = New System.Windows.Forms.RichTextBox()
                Me.BT_MARK2 = New System.Windows.Forms.Button()
                Me.BT_VTL = New System.Windows.Forms.Button()
                Me.BT_VDISK = New System.Windows.Forms.Button()
                Me.BT_DFC = New System.Windows.Forms.Button()
                Me.BT_OPT = New System.Windows.Forms.Button()
                Me.BT_QUE = New System.Windows.Forms.Button()
                Me.BT_ALL = New System.Windows.Forms.Button()
                Me.IS_STRUCT = New System.Windows.Forms.CheckBox()
                Me.Button10 = New System.Windows.Forms.Button()
                Me.BT_BKLG = New System.Windows.Forms.Button()
                Me.TB_YES = New System.Windows.Forms.TextBox()
                Me.TextBox1 = New System.Windows.Forms.TextBox()
                Me.Button12 = New System.Windows.Forms.Button()
                Me.BT_MUTEX = New System.Windows.Forms.Button()
                Me.BT_BASIC = New System.Windows.Forms.Button()
                Me.BT_WAITQ = New System.Windows.Forms.Button()
                Me.BT_LIST = New System.Windows.Forms.Button()
                Me.BT_LOCK = New System.Windows.Forms.Button()
                Me.BT_PERCPU = New System.Windows.Forms.Button()
                Me.BT_TREADS = New System.Windows.Forms.Button()
                Me.BT_FILTER2 = New System.Windows.Forms.Button()
                Me.BT_FILTER = New System.Windows.Forms.Button()
                Me.BT_QLA = New System.Windows.Forms.Button()
                Me.BT_BLK = New System.Windows.Forms.Button()
                Me.BT_CODES = New System.Windows.Forms.Button()
                Me.BT_KMEM = New System.Windows.Forms.Button()
                Me.BT_IRQ = New System.Windows.Forms.Button()
                Me.BT_ASCII = New System.Windows.Forms.Button()
                Me.BT_DEV = New System.Windows.Forms.Button()
                Me.BT_SSA = New System.Windows.Forms.Button()
                Me.TT = New System.Windows.Forms.ToolTip(Me.components)
                Me.Label2 = New System.Windows.Forms.Label()
                Me.LB_LOGDIR = New System.Windows.Forms.Label()
                Me.BT_REOFFLINE = New System.Windows.Forms.Button()
                Me.BT_RESTORE = New System.Windows.Forms.Button()
                Me.CB_NODE = New System.Windows.Forms.ComboBox()
                Me.CB_FREE = New System.Windows.Forms.CheckBox()
                Me.CB_USED = New System.Windows.Forms.CheckBox()
                Me.TB_SCMD = New System.Windows.Forms.ComboBox()
                Me.TB_KEY = New System.Windows.Forms.ComboBox()
                Me.TB_KEY2 = New System.Windows.Forms.ComboBox()
                Me.BT_WREQ = New System.Windows.Forms.Button()
                Me.BT_FREQ = New System.Windows.Forms.Button()
                Me.BT_FRSP = New System.Windows.Forms.Button()
                Me.BT_WRSP = New System.Windows.Forms.Button()
                Me.BT_FATIO = New System.Windows.Forms.Button()
                Me.BT_WATIO = New System.Windows.Forms.Button()
                Me.TB_CBN = New System.Windows.Forms.TextBox()
                Me.GroupBox1 = New System.Windows.Forms.GroupBox()
                Me.GroupBox2 = New System.Windows.Forms.GroupBox()
                Me.GroupBox3 = New System.Windows.Forms.GroupBox()
                Me.Label3 = New System.Windows.Forms.Label()
                Me.TB_CLN = New System.Windows.Forms.TextBox()
                CType(Me.DgvLog, System.ComponentModel.ISupportInitialize).BeginInit()
                Me.SuspendLayout()
                '
                'BT_CMD
                '
                Me.BT_CMD.Location = New System.Drawing.Point(1221, 184)
                Me.BT_CMD.Name = "BT_CMD"
                Me.BT_CMD.Size = New System.Drawing.Size(62, 23)
                Me.BT_CMD.TabIndex = 0
                Me.BT_CMD.Text = "command"
                Me.BT_CMD.UseVisualStyleBackColor = True
                '
                'TB_WINCLASS
                '
                Me.TB_WINCLASS.Location = New System.Drawing.Point(84, 4)
                Me.TB_WINCLASS.Name = "TB_WINCLASS"
                Me.TB_WINCLASS.Size = New System.Drawing.Size(69, 21)
                Me.TB_WINCLASS.TabIndex = 3
                Me.TB_WINCLASS.Text = "mintty"
                '
                'Label1
                '
                Me.Label1.AutoSize = True
                Me.Label1.Location = New System.Drawing.Point(13, 73)
                Me.Label1.Name = "Label1"
                Me.Label1.Size = New System.Drawing.Size(83, 12)
                Me.Label1.TabIndex = 6
                Me.Label1.Text = "scsi_qla_host"
                '
                'TM
                '
                Me.TM.Interval = 200
                '
                'BT_START
                '
                Me.BT_START.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
                Me.BT_START.Location = New System.Drawing.Point(386, 2)
                Me.BT_START.Name = "BT_START"
                Me.BT_START.Size = New System.Drawing.Size(71, 23)
                Me.BT_START.TabIndex = 10
                Me.BT_START.Text = "Re-Online"
                Me.BT_START.UseVisualStyleBackColor = False
                '
                'DgvLog
                '
                Me.DgvLog.AllowUserToOrderColumns = True
                DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
                DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
                DataGridViewCellStyle1.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
                DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
                DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
                DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
                DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
                Me.DgvLog.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
                Me.DgvLog.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
                DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
                DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
                DataGridViewCellStyle2.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
                DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
                DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
                DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
                DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
                Me.DgvLog.DefaultCellStyle = DataGridViewCellStyle2
                Me.DgvLog.Location = New System.Drawing.Point(838, 5)
                Me.DgvLog.Name = "DgvLog"
                Me.DgvLog.ReadOnly = True
                Me.DgvLog.RowTemplate.Height = 23
                Me.DgvLog.Size = New System.Drawing.Size(740, 165)
                Me.DgvLog.TabIndex = 69
                '
                'CB_SQH
                '
                Me.CB_SQH.FormattingEnabled = True
                Me.CB_SQH.Location = New System.Drawing.Point(10, 90)
                Me.CB_SQH.Name = "CB_SQH"
                Me.CB_SQH.Size = New System.Drawing.Size(157, 20)
                Me.CB_SQH.TabIndex = 70
                '
                'BT_MARK
                '
                Me.BT_MARK.Location = New System.Drawing.Point(346, 49)
                Me.BT_MARK.Name = "BT_MARK"
                Me.BT_MARK.Size = New System.Drawing.Size(52, 23)
                Me.BT_MARK.TabIndex = 72
                Me.BT_MARK.Text = "search"
                Me.BT_MARK.UseVisualStyleBackColor = True
                '
                'RTB_MORE
                '
                Me.RTB_MORE.Location = New System.Drawing.Point(178, 78)
                Me.RTB_MORE.Name = "RTB_MORE"
                Me.RTB_MORE.ReadOnly = True
                Me.RTB_MORE.Size = New System.Drawing.Size(649, 762)
                Me.RTB_MORE.TabIndex = 73
                Me.RTB_MORE.Text = ""
                '
                'Button5
                '
                Me.Button5.Location = New System.Drawing.Point(10, 112)
                Me.Button5.Name = "Button5"
                Me.Button5.Size = New System.Drawing.Size(67, 23)
                Me.Button5.TabIndex = 75
                Me.Button5.Text = "ha dmesg"
                Me.Button5.UseVisualStyleBackColor = True
                '
                'Button6
                '
                Me.Button6.Location = New System.Drawing.Point(456, 49)
                Me.Button6.Name = "Button6"
                Me.Button6.Size = New System.Drawing.Size(63, 23)
                Me.Button6.TabIndex = 76
                Me.Button6.Text = "sys/mach"
                Me.Button6.UseVisualStyleBackColor = True
                '
                'Button7
                '
                Me.Button7.Location = New System.Drawing.Point(668, 49)
                Me.Button7.Name = "Button7"
                Me.Button7.Size = New System.Drawing.Size(46, 23)
                Me.Button7.TabIndex = 77
                Me.Button7.Text = "dmesg"
                Me.Button7.UseVisualStyleBackColor = True
                '
                'ComboBox1
                '
                Me.ComboBox1.FormattingEnabled = True
                Me.ComboBox1.Location = New System.Drawing.Point(12, 422)
                Me.ComboBox1.Name = "ComboBox1"
                Me.ComboBox1.Size = New System.Drawing.Size(42, 20)
                Me.ComboBox1.TabIndex = 79
                '
                'Button3
                '
                Me.Button3.Location = New System.Drawing.Point(523, 49)
                Me.Button3.Name = "Button3"
                Me.Button3.Size = New System.Drawing.Size(41, 23)
                Me.Button3.TabIndex = 80
                Me.Button3.Text = "runq"
                Me.Button3.UseVisualStyleBackColor = True
                '
                'Button8
                '
                Me.Button8.Location = New System.Drawing.Point(567, 49)
                Me.Button8.Name = "Button8"
                Me.Button8.Size = New System.Drawing.Size(48, 23)
                Me.Button8.TabIndex = 81
                Me.Button8.Text = "bt -a"
                Me.Button8.UseVisualStyleBackColor = True
                '
                'Button9
                '
                Me.Button9.Location = New System.Drawing.Point(618, 49)
                Me.Button9.Name = "Button9"
                Me.Button9.Size = New System.Drawing.Size(48, 23)
                Me.Button9.TabIndex = 82
                Me.Button9.Text = "ps -l"
                Me.Button9.UseVisualStyleBackColor = True
                '
                'TB_CMD
                '
                Me.TB_CMD.FormattingEnabled = True
                Me.TB_CMD.Location = New System.Drawing.Point(955, 184)
                Me.TB_CMD.Name = "TB_CMD"
                Me.TB_CMD.Size = New System.Drawing.Size(264, 20)
                Me.TB_CMD.TabIndex = 83
                '
                'RTB_MORE2
                '
                Me.RTB_MORE2.BackColor = System.Drawing.SystemColors.Control
                Me.RTB_MORE2.Location = New System.Drawing.Point(838, 213)
                Me.RTB_MORE2.Name = "RTB_MORE2"
                Me.RTB_MORE2.ReadOnly = True
                Me.RTB_MORE2.Size = New System.Drawing.Size(734, 627)
                Me.RTB_MORE2.TabIndex = 84
                Me.RTB_MORE2.Text = ""
                '
                'BT_MARK2
                '
                Me.BT_MARK2.Location = New System.Drawing.Point(1466, 183)
                Me.BT_MARK2.Name = "BT_MARK2"
                Me.BT_MARK2.Size = New System.Drawing.Size(57, 23)
                Me.BT_MARK2.TabIndex = 86
                Me.BT_MARK2.Text = "search"
                Me.BT_MARK2.UseVisualStyleBackColor = True
                '
                'BT_VTL
                '
                Me.BT_VTL.Location = New System.Drawing.Point(42, 536)
                Me.BT_VTL.Name = "BT_VTL"
                Me.BT_VTL.Size = New System.Drawing.Size(41, 23)
                Me.BT_VTL.TabIndex = 87
                Me.BT_VTL.Text = "vtl"
                Me.BT_VTL.UseVisualStyleBackColor = True
                '
                'BT_VDISK
                '
                Me.BT_VDISK.Location = New System.Drawing.Point(85, 536)
                Me.BT_VDISK.Name = "BT_VDISK"
                Me.BT_VDISK.Size = New System.Drawing.Size(41, 23)
                Me.BT_VDISK.TabIndex = 88
                Me.BT_VDISK.Text = "vdisk"
                Me.BT_VDISK.UseVisualStyleBackColor = True
                '
                'BT_DFC
                '
                Me.BT_DFC.Location = New System.Drawing.Point(131, 536)
                Me.BT_DFC.Name = "BT_DFC"
                Me.BT_DFC.Size = New System.Drawing.Size(36, 23)
                Me.BT_DFC.TabIndex = 89
                Me.BT_DFC.Text = "dfc"
                Me.BT_DFC.UseVisualStyleBackColor = True
                '
                'BT_OPT
                '
                Me.BT_OPT.Location = New System.Drawing.Point(66, 565)
                Me.BT_OPT.Name = "BT_OPT"
                Me.BT_OPT.Size = New System.Drawing.Size(50, 23)
                Me.BT_OPT.TabIndex = 90
                Me.BT_OPT.Text = "options"
                Me.BT_OPT.UseVisualStyleBackColor = True
                '
                'BT_QUE
                '
                Me.BT_QUE.Location = New System.Drawing.Point(122, 565)
                Me.BT_QUE.Name = "BT_QUE"
                Me.BT_QUE.Size = New System.Drawing.Size(45, 23)
                Me.BT_QUE.TabIndex = 91
                Me.BT_QUE.Text = "queue"
                Me.BT_QUE.UseVisualStyleBackColor = True
                '
                'BT_ALL
                '
                Me.BT_ALL.Location = New System.Drawing.Point(766, 49)
                Me.BT_ALL.Name = "BT_ALL"
                Me.BT_ALL.Size = New System.Drawing.Size(47, 23)
                Me.BT_ALL.TabIndex = 93
                Me.BT_ALL.Text = "all log"
                Me.BT_ALL.UseVisualStyleBackColor = True
                '
                'IS_STRUCT
                '
                Me.IS_STRUCT.AutoSize = True
                Me.IS_STRUCT.Location = New System.Drawing.Point(891, 186)
                Me.IS_STRUCT.Name = "IS_STRUCT"
                Me.IS_STRUCT.Size = New System.Drawing.Size(60, 16)
                Me.IS_STRUCT.TabIndex = 94
                Me.IS_STRUCT.Text = "struct"
                Me.IS_STRUCT.UseVisualStyleBackColor = True
                '
                'Button10
                '
                Me.Button10.ForeColor = System.Drawing.Color.Fuchsia
                Me.Button10.Location = New System.Drawing.Point(10, 565)
                Me.Button10.Name = "Button10"
                Me.Button10.Size = New System.Drawing.Size(50, 23)
                Me.Button10.TabIndex = 95
                Me.Button10.Text = "VHBA"
                Me.Button10.UseVisualStyleBackColor = True
                '
                'BT_BKLG
                '
                Me.BT_BKLG.Location = New System.Drawing.Point(157, 1)
                Me.BT_BKLG.Name = "BT_BKLG"
                Me.BT_BKLG.Size = New System.Drawing.Size(76, 23)
                Me.BT_BKLG.TabIndex = 96
                Me.BT_BKLG.Text = "backup log"
                Me.BT_BKLG.UseVisualStyleBackColor = True
                '
                'TB_YES
                '
                Me.TB_YES.Location = New System.Drawing.Point(10, 539)
                Me.TB_YES.Name = "TB_YES"
                Me.TB_YES.Size = New System.Drawing.Size(30, 21)
                Me.TB_YES.TabIndex = 97
                '
                'TextBox1
                '
                Me.TextBox1.Location = New System.Drawing.Point(10, 364)
                Me.TextBox1.Name = "TextBox1"
                Me.TextBox1.Size = New System.Drawing.Size(30, 21)
                Me.TextBox1.TabIndex = 104
                '
                'Button12
                '
                Me.Button12.ForeColor = System.Drawing.Color.Fuchsia
                Me.Button12.Location = New System.Drawing.Point(10, 390)
                Me.Button12.Name = "Button12"
                Me.Button12.Size = New System.Drawing.Size(50, 23)
                Me.Button12.TabIndex = 103
                Me.Button12.Text = "SCST"
                Me.Button12.UseVisualStyleBackColor = True
                '
                'BT_MUTEX
                '
                Me.BT_MUTEX.Location = New System.Drawing.Point(122, 390)
                Me.BT_MUTEX.Name = "BT_MUTEX"
                Me.BT_MUTEX.Size = New System.Drawing.Size(45, 23)
                Me.BT_MUTEX.TabIndex = 102
                Me.BT_MUTEX.Text = "mutex"
                Me.BT_MUTEX.UseVisualStyleBackColor = True
                '
                'BT_BASIC
                '
                Me.BT_BASIC.Location = New System.Drawing.Point(66, 390)
                Me.BT_BASIC.Name = "BT_BASIC"
                Me.BT_BASIC.Size = New System.Drawing.Size(50, 23)
                Me.BT_BASIC.TabIndex = 101
                Me.BT_BASIC.Text = "basic"
                Me.BT_BASIC.UseVisualStyleBackColor = True
                '
                'BT_WAITQ
                '
                Me.BT_WAITQ.Location = New System.Drawing.Point(124, 361)
                Me.BT_WAITQ.Name = "BT_WAITQ"
                Me.BT_WAITQ.Size = New System.Drawing.Size(43, 23)
                Me.BT_WAITQ.TabIndex = 100
                Me.BT_WAITQ.Text = "waitq"
                Me.BT_WAITQ.UseVisualStyleBackColor = True
                '
                'BT_LIST
                '
                Me.BT_LIST.Location = New System.Drawing.Point(83, 361)
                Me.BT_LIST.Name = "BT_LIST"
                Me.BT_LIST.Size = New System.Drawing.Size(41, 23)
                Me.BT_LIST.TabIndex = 99
                Me.BT_LIST.Text = "list"
                Me.BT_LIST.UseVisualStyleBackColor = True
                '
                'BT_LOCK
                '
                Me.BT_LOCK.Location = New System.Drawing.Point(41, 361)
                Me.BT_LOCK.Name = "BT_LOCK"
                Me.BT_LOCK.Size = New System.Drawing.Size(42, 23)
                Me.BT_LOCK.TabIndex = 98
                Me.BT_LOCK.Text = "lock"
                Me.BT_LOCK.UseVisualStyleBackColor = True
                '
                'BT_PERCPU
                '
                Me.BT_PERCPU.Location = New System.Drawing.Point(122, 419)
                Me.BT_PERCPU.Name = "BT_PERCPU"
                Me.BT_PERCPU.Size = New System.Drawing.Size(45, 23)
                Me.BT_PERCPU.TabIndex = 105
                Me.BT_PERCPU.Text = "percpu"
                Me.BT_PERCPU.UseVisualStyleBackColor = True
                '
                'BT_TREADS
                '
                Me.BT_TREADS.Location = New System.Drawing.Point(59, 419)
                Me.BT_TREADS.Name = "BT_TREADS"
                Me.BT_TREADS.Size = New System.Drawing.Size(57, 23)
                Me.BT_TREADS.TabIndex = 106
                Me.BT_TREADS.Text = "threads"
                Me.BT_TREADS.UseVisualStyleBackColor = True
                '
                'BT_FILTER2
                '
                Me.BT_FILTER2.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer))
                Me.BT_FILTER2.Location = New System.Drawing.Point(1525, 183)
                Me.BT_FILTER2.Name = "BT_FILTER2"
                Me.BT_FILTER2.Size = New System.Drawing.Size(53, 23)
                Me.BT_FILTER2.TabIndex = 108
                Me.BT_FILTER2.Text = "filter"
                Me.BT_FILTER2.UseVisualStyleBackColor = False
                '
                'BT_FILTER
                '
                Me.BT_FILTER.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer))
                Me.BT_FILTER.Location = New System.Drawing.Point(401, 49)
                Me.BT_FILTER.Name = "BT_FILTER"
                Me.BT_FILTER.Size = New System.Drawing.Size(52, 23)
                Me.BT_FILTER.TabIndex = 109
                Me.BT_FILTER.Text = "filter"
                Me.BT_FILTER.UseVisualStyleBackColor = False
                '
                'BT_QLA
                '
                Me.BT_QLA.ForeColor = System.Drawing.Color.Fuchsia
                Me.BT_QLA.Location = New System.Drawing.Point(83, 114)
                Me.BT_QLA.Name = "BT_QLA"
                Me.BT_QLA.Size = New System.Drawing.Size(50, 23)
                Me.BT_QLA.TabIndex = 110
                Me.BT_QLA.Text = "QLA"
                Me.BT_QLA.UseVisualStyleBackColor = True
                '
                'BT_BLK
                '
                Me.BT_BLK.Location = New System.Drawing.Point(833, 182)
                Me.BT_BLK.Name = "BT_BLK"
                Me.BT_BLK.Size = New System.Drawing.Size(53, 23)
                Me.BT_BLK.TabIndex = 111
                Me.BT_BLK.Text = "black"
                Me.BT_BLK.UseVisualStyleBackColor = True
                '
                'BT_CODES
                '
                Me.BT_CODES.Location = New System.Drawing.Point(717, 49)
                Me.BT_CODES.Name = "BT_CODES"
                Me.BT_CODES.Size = New System.Drawing.Size(47, 23)
                Me.BT_CODES.TabIndex = 112
                Me.BT_CODES.Text = "codes"
                Me.BT_CODES.UseVisualStyleBackColor = True
                '
                'BT_KMEM
                '
                Me.BT_KMEM.Location = New System.Drawing.Point(756, 20)
                Me.BT_KMEM.Name = "BT_KMEM"
                Me.BT_KMEM.Size = New System.Drawing.Size(57, 23)
                Me.BT_KMEM.TabIndex = 113
                Me.BT_KMEM.Text = "kmem -s"
                Me.BT_KMEM.UseVisualStyleBackColor = True
                '
                'BT_IRQ
                '
                Me.BT_IRQ.Location = New System.Drawing.Point(693, 20)
                Me.BT_IRQ.Name = "BT_IRQ"
                Me.BT_IRQ.Size = New System.Drawing.Size(57, 23)
                Me.BT_IRQ.TabIndex = 114
                Me.BT_IRQ.Text = "irq"
                Me.BT_IRQ.UseVisualStyleBackColor = True
                '
                'BT_ASCII
                '
                Me.BT_ASCII.Location = New System.Drawing.Point(630, 20)
                Me.BT_ASCII.Name = "BT_ASCII"
                Me.BT_ASCII.Size = New System.Drawing.Size(57, 23)
                Me.BT_ASCII.TabIndex = 115
                Me.BT_ASCII.Text = "ascii"
                Me.BT_ASCII.UseVisualStyleBackColor = True
                '
                'BT_DEV
                '
                Me.BT_DEV.Location = New System.Drawing.Point(567, 20)
                Me.BT_DEV.Name = "BT_DEV"
                Me.BT_DEV.Size = New System.Drawing.Size(57, 23)
                Me.BT_DEV.TabIndex = 116
                Me.BT_DEV.Text = "dev"
                Me.BT_DEV.UseVisualStyleBackColor = True
                '
                'BT_SSA
                '
                Me.BT_SSA.BackColor = System.Drawing.Color.Red
                Me.BT_SSA.Location = New System.Drawing.Point(10, 272)
                Me.BT_SSA.Name = "BT_SSA"
                Me.BT_SSA.Size = New System.Drawing.Size(157, 23)
                Me.BT_SSA.TabIndex = 117
                Me.BT_SSA.Text = "Selected Smart Analysis"
                Me.BT_SSA.UseVisualStyleBackColor = False
                '
                'TT
                '
                Me.TT.AutomaticDelay = 100
                Me.TT.AutoPopDelay = 9000
                Me.TT.InitialDelay = 100
                Me.TT.ReshowDelay = 20
                '
                'Label2
                '
                Me.Label2.AutoSize = True
                Me.Label2.Location = New System.Drawing.Point(0, 7)
                Me.Label2.Name = "Label2"
                Me.Label2.Size = New System.Drawing.Size(77, 12)
                Me.Label2.TabIndex = 118
                Me.Label2.Text = "WndClassName"
                '
                'LB_LOGDIR
                '
                Me.LB_LOGDIR.AutoSize = True
                Me.LB_LOGDIR.Location = New System.Drawing.Point(239, 7)
                Me.LB_LOGDIR.Name = "LB_LOGDIR"
                Me.LB_LOGDIR.Size = New System.Drawing.Size(89, 12)
                Me.LB_LOGDIR.TabIndex = 119
                Me.LB_LOGDIR.Text = "D:\santgt\log\"
                '
                'BT_REOFFLINE
                '
                Me.BT_REOFFLINE.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
                Me.BT_REOFFLINE.Location = New System.Drawing.Point(464, 2)
                Me.BT_REOFFLINE.Name = "BT_REOFFLINE"
                Me.BT_REOFFLINE.Size = New System.Drawing.Size(78, 23)
                Me.BT_REOFFLINE.TabIndex = 120
                Me.BT_REOFFLINE.Text = "Re-Offline"
                Me.BT_REOFFLINE.UseVisualStyleBackColor = False
                '
                'BT_RESTORE
                '
                Me.BT_RESTORE.Location = New System.Drawing.Point(157, 25)
                Me.BT_RESTORE.Name = "BT_RESTORE"
                Me.BT_RESTORE.Size = New System.Drawing.Size(76, 23)
                Me.BT_RESTORE.TabIndex = 121
                Me.BT_RESTORE.Text = "Restore"
                Me.BT_RESTORE.UseVisualStyleBackColor = True
                '
                'CB_NODE
                '
                Me.CB_NODE.FormattingEnabled = True
                Me.CB_NODE.Location = New System.Drawing.Point(241, 27)
                Me.CB_NODE.Name = "CB_NODE"
                Me.CB_NODE.Size = New System.Drawing.Size(301, 20)
                Me.CB_NODE.TabIndex = 122
                '
                'CB_FREE
                '
                Me.CB_FREE.AutoSize = True
                Me.CB_FREE.Location = New System.Drawing.Point(27, 227)
                Me.CB_FREE.Name = "CB_FREE"
                Me.CB_FREE.Size = New System.Drawing.Size(48, 16)
                Me.CB_FREE.TabIndex = 124
                Me.CB_FREE.Text = "Free"
                Me.CB_FREE.UseVisualStyleBackColor = True
                '
                'CB_USED
                '
                Me.CB_USED.AutoSize = True
                Me.CB_USED.Checked = True
                Me.CB_USED.CheckState = System.Windows.Forms.CheckState.Checked
                Me.CB_USED.Location = New System.Drawing.Point(82, 227)
                Me.CB_USED.Name = "CB_USED"
                Me.CB_USED.Size = New System.Drawing.Size(48, 16)
                Me.CB_USED.TabIndex = 125
                Me.CB_USED.Text = "Used"
                Me.CB_USED.UseVisualStyleBackColor = True
                '
                'TB_SCMD
                '
                Me.TB_SCMD.FormattingEnabled = True
                Me.TB_SCMD.Location = New System.Drawing.Point(10, 248)
                Me.TB_SCMD.Name = "TB_SCMD"
                Me.TB_SCMD.Size = New System.Drawing.Size(157, 20)
                Me.TB_SCMD.TabIndex = 126
                '
                'TB_KEY
                '
                Me.TB_KEY.FormattingEnabled = True
                Me.TB_KEY.Location = New System.Drawing.Point(178, 52)
                Me.TB_KEY.Name = "TB_KEY"
                Me.TB_KEY.Size = New System.Drawing.Size(167, 20)
                Me.TB_KEY.TabIndex = 127
                '
                'TB_KEY2
                '
                Me.TB_KEY2.FormattingEnabled = True
                Me.TB_KEY2.Location = New System.Drawing.Point(1289, 186)
                Me.TB_KEY2.Name = "TB_KEY2"
                Me.TB_KEY2.Size = New System.Drawing.Size(171, 20)
                Me.TB_KEY2.TabIndex = 128
                '
                'BT_WREQ
                '
                Me.BT_WREQ.Location = New System.Drawing.Point(10, 138)
                Me.BT_WREQ.Name = "BT_WREQ"
                Me.BT_WREQ.Size = New System.Drawing.Size(40, 23)
                Me.BT_WREQ.TabIndex = 129
                Me.BT_WREQ.Text = "w-req"
                Me.BT_WREQ.UseVisualStyleBackColor = True
                '
                'BT_FREQ
                '
                Me.BT_FREQ.Location = New System.Drawing.Point(10, 165)
                Me.BT_FREQ.Name = "BT_FREQ"
                Me.BT_FREQ.Size = New System.Drawing.Size(40, 23)
                Me.BT_FREQ.TabIndex = 130
                Me.BT_FREQ.Text = "f-req"
                Me.BT_FREQ.UseVisualStyleBackColor = True
                '
                'BT_FRSP
                '
                Me.BT_FRSP.Location = New System.Drawing.Point(55, 165)
                Me.BT_FRSP.Name = "BT_FRSP"
                Me.BT_FRSP.Size = New System.Drawing.Size(47, 23)
                Me.BT_FRSP.TabIndex = 132
                Me.BT_FRSP.Text = "f-rsp"
                Me.BT_FRSP.UseVisualStyleBackColor = True
                '
                'BT_WRSP
                '
                Me.BT_WRSP.Location = New System.Drawing.Point(55, 138)
                Me.BT_WRSP.Name = "BT_WRSP"
                Me.BT_WRSP.Size = New System.Drawing.Size(47, 23)
                Me.BT_WRSP.TabIndex = 131
                Me.BT_WRSP.Text = "w-rsp"
                Me.BT_WRSP.UseVisualStyleBackColor = True
                '
                'BT_FATIO
                '
                Me.BT_FATIO.Location = New System.Drawing.Point(106, 165)
                Me.BT_FATIO.Name = "BT_FATIO"
                Me.BT_FATIO.Size = New System.Drawing.Size(61, 23)
                Me.BT_FATIO.TabIndex = 134
                Me.BT_FATIO.Text = "f-atio"
                Me.BT_FATIO.UseVisualStyleBackColor = True
                '
                'BT_WATIO
                '
                Me.BT_WATIO.Location = New System.Drawing.Point(106, 138)
                Me.BT_WATIO.Name = "BT_WATIO"
                Me.BT_WATIO.Size = New System.Drawing.Size(61, 23)
                Me.BT_WATIO.TabIndex = 133
                Me.BT_WATIO.Text = "w-atio"
                Me.BT_WATIO.UseVisualStyleBackColor = True
                '
                'TB_CBN
                '
                Me.TB_CBN.Location = New System.Drawing.Point(137, 66)
                Me.TB_CBN.Name = "TB_CBN"
                Me.TB_CBN.Size = New System.Drawing.Size(30, 21)
                Me.TB_CBN.TabIndex = 135
                Me.TB_CBN.Text = "50"
                '
                'GroupBox1
                '
                Me.GroupBox1.Location = New System.Drawing.Point(3, 54)
                Me.GroupBox1.Name = "GroupBox1"
                Me.GroupBox1.Size = New System.Drawing.Size(169, 557)
                Me.GroupBox1.TabIndex = 136
                Me.GroupBox1.TabStop = False
                Me.GroupBox1.Text = "SANIO shortcuts"
                '
                'GroupBox2
                '
                Me.GroupBox2.Location = New System.Drawing.Point(3, 622)
                Me.GroupBox2.Name = "GroupBox2"
                Me.GroupBox2.Size = New System.Drawing.Size(169, 108)
                Me.GroupBox2.TabIndex = 137
                Me.GroupBox2.TabStop = False
                Me.GroupBox2.Text = "SCSI shortcuts"
                '
                'GroupBox3
                '
                Me.GroupBox3.Location = New System.Drawing.Point(3, 738)
                Me.GroupBox3.Name = "GroupBox3"
                Me.GroupBox3.Size = New System.Drawing.Size(169, 101)
                Me.GroupBox3.TabIndex = 138
                Me.GroupBox3.TabStop = False
                Me.GroupBox3.Text = "Network shortcuts"
                '
                'Label3
                '
                Me.Label3.AutoSize = True
                Me.Label3.Location = New System.Drawing.Point(567, 4)
                Me.Label3.Name = "Label3"
                Me.Label3.Size = New System.Drawing.Size(143, 12)
                Me.Label3.TabIndex = 139
                Me.Label3.Text = "Source Code Line Number"
                '
                'TB_CLN
                '
                Me.TB_CLN.Location = New System.Drawing.Point(699, 1)
                Me.TB_CLN.Name = "TB_CLN"
                Me.TB_CLN.Size = New System.Drawing.Size(30, 21)
                Me.TB_CLN.TabIndex = 140
                Me.TB_CLN.Text = "20"
                '
                'Form1
                '
                Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
                Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
                Me.ClientSize = New System.Drawing.Size(1584, 851)
                Me.Controls.Add(Me.TB_CLN)
                Me.Controls.Add(Me.Label3)
                Me.Controls.Add(Me.GroupBox3)
                Me.Controls.Add(Me.GroupBox2)
                Me.Controls.Add(Me.TB_CBN)
                Me.Controls.Add(Me.BT_FATIO)
                Me.Controls.Add(Me.BT_WATIO)
                Me.Controls.Add(Me.BT_FRSP)
                Me.Controls.Add(Me.BT_WRSP)
                Me.Controls.Add(Me.BT_FREQ)
                Me.Controls.Add(Me.BT_WREQ)
                Me.Controls.Add(Me.TB_KEY2)
                Me.Controls.Add(Me.TB_KEY)
                Me.Controls.Add(Me.TB_SCMD)
                Me.Controls.Add(Me.CB_USED)
                Me.Controls.Add(Me.CB_FREE)
                Me.Controls.Add(Me.CB_NODE)
                Me.Controls.Add(Me.BT_RESTORE)
                Me.Controls.Add(Me.BT_REOFFLINE)
                Me.Controls.Add(Me.LB_LOGDIR)
                Me.Controls.Add(Me.Label2)
                Me.Controls.Add(Me.BT_SSA)
                Me.Controls.Add(Me.BT_DEV)
                Me.Controls.Add(Me.BT_ASCII)
                Me.Controls.Add(Me.BT_IRQ)
                Me.Controls.Add(Me.BT_KMEM)
                Me.Controls.Add(Me.BT_CODES)
                Me.Controls.Add(Me.BT_BLK)
                Me.Controls.Add(Me.BT_QLA)
                Me.Controls.Add(Me.BT_FILTER)
                Me.Controls.Add(Me.BT_FILTER2)
                Me.Controls.Add(Me.BT_TREADS)
                Me.Controls.Add(Me.BT_PERCPU)
                Me.Controls.Add(Me.TextBox1)
                Me.Controls.Add(Me.Button12)
                Me.Controls.Add(Me.BT_MUTEX)
                Me.Controls.Add(Me.BT_BASIC)
                Me.Controls.Add(Me.BT_WAITQ)
                Me.Controls.Add(Me.BT_LIST)
                Me.Controls.Add(Me.BT_LOCK)
                Me.Controls.Add(Me.TB_YES)
                Me.Controls.Add(Me.BT_BKLG)
                Me.Controls.Add(Me.Button10)
                Me.Controls.Add(Me.IS_STRUCT)
                Me.Controls.Add(Me.BT_ALL)
                Me.Controls.Add(Me.BT_QUE)
                Me.Controls.Add(Me.BT_OPT)
                Me.Controls.Add(Me.BT_DFC)
                Me.Controls.Add(Me.BT_VDISK)
                Me.Controls.Add(Me.BT_VTL)
                Me.Controls.Add(Me.BT_MARK2)
                Me.Controls.Add(Me.RTB_MORE2)
                Me.Controls.Add(Me.TB_CMD)
                Me.Controls.Add(Me.Button9)
                Me.Controls.Add(Me.Button8)
                Me.Controls.Add(Me.Button3)
                Me.Controls.Add(Me.ComboBox1)
                Me.Controls.Add(Me.Button7)
                Me.Controls.Add(Me.Button6)
                Me.Controls.Add(Me.Button5)
                Me.Controls.Add(Me.RTB_MORE)
                Me.Controls.Add(Me.BT_MARK)
                Me.Controls.Add(Me.CB_SQH)
                Me.Controls.Add(Me.DgvLog)
                Me.Controls.Add(Me.BT_START)
                Me.Controls.Add(Me.Label1)
                Me.Controls.Add(Me.TB_WINCLASS)
                Me.Controls.Add(Me.BT_CMD)
                Me.Controls.Add(Me.GroupBox1)
                Me.Name = "Form1"
                Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
                Me.Text = "GUI Crash"
                CType(Me.DgvLog, System.ComponentModel.ISupportInitialize).EndInit()
                Me.ResumeLayout(False)
                Me.PerformLayout()

        End Sub
        Friend WithEvents BT_CMD As System.Windows.Forms.Button
        Friend WithEvents TB_WINCLASS As System.Windows.Forms.TextBox
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents TM As System.Windows.Forms.Timer
        Friend WithEvents BT_START As System.Windows.Forms.Button
        Friend WithEvents DgvLog As System.Windows.Forms.DataGridView
        Friend WithEvents CB_SQH As System.Windows.Forms.ComboBox
        Friend WithEvents BT_MARK As System.Windows.Forms.Button
        Friend WithEvents RTB_MORE As System.Windows.Forms.RichTextBox
        Friend WithEvents Button7 As System.Windows.Forms.Button
        Friend WithEvents Button6 As System.Windows.Forms.Button
        Friend WithEvents Button5 As System.Windows.Forms.Button
        Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
        Friend WithEvents Button9 As System.Windows.Forms.Button
        Friend WithEvents Button8 As System.Windows.Forms.Button
        Friend WithEvents Button3 As System.Windows.Forms.Button
        Friend WithEvents TB_CMD As System.Windows.Forms.ComboBox
        Friend WithEvents RTB_MORE2 As System.Windows.Forms.RichTextBox
        Friend WithEvents BT_MARK2 As System.Windows.Forms.Button
        Friend WithEvents BT_QUE As System.Windows.Forms.Button
        Friend WithEvents BT_OPT As System.Windows.Forms.Button
        Friend WithEvents BT_DFC As System.Windows.Forms.Button
        Friend WithEvents BT_VDISK As System.Windows.Forms.Button
        Friend WithEvents BT_VTL As System.Windows.Forms.Button
        Friend WithEvents BT_ALL As System.Windows.Forms.Button
        Friend WithEvents IS_STRUCT As System.Windows.Forms.CheckBox
        Friend WithEvents Button10 As System.Windows.Forms.Button
        Friend WithEvents BT_BKLG As System.Windows.Forms.Button
        Friend WithEvents TB_YES As System.Windows.Forms.TextBox
        Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
        Friend WithEvents Button12 As System.Windows.Forms.Button
        Friend WithEvents BT_MUTEX As System.Windows.Forms.Button
        Friend WithEvents BT_BASIC As System.Windows.Forms.Button
        Friend WithEvents BT_WAITQ As System.Windows.Forms.Button
        Friend WithEvents BT_LIST As System.Windows.Forms.Button
        Friend WithEvents BT_LOCK As System.Windows.Forms.Button
        Friend WithEvents BT_TREADS As System.Windows.Forms.Button
        Friend WithEvents BT_PERCPU As System.Windows.Forms.Button
        Friend WithEvents BT_FILTER As System.Windows.Forms.Button
        Friend WithEvents BT_FILTER2 As System.Windows.Forms.Button
        Friend WithEvents BT_QLA As System.Windows.Forms.Button
        Friend WithEvents BT_BLK As System.Windows.Forms.Button
        Friend WithEvents BT_CODES As System.Windows.Forms.Button
        Friend WithEvents BT_KMEM As System.Windows.Forms.Button
        Friend WithEvents BT_IRQ As System.Windows.Forms.Button
        Friend WithEvents BT_ASCII As System.Windows.Forms.Button
        Friend WithEvents BT_DEV As System.Windows.Forms.Button
        Friend WithEvents BT_SSA As System.Windows.Forms.Button
        Friend WithEvents TT As System.Windows.Forms.ToolTip
        Friend WithEvents Label2 As System.Windows.Forms.Label
        Friend WithEvents LB_LOGDIR As System.Windows.Forms.Label
        Friend WithEvents BT_REOFFLINE As System.Windows.Forms.Button
        Friend WithEvents BT_RESTORE As System.Windows.Forms.Button
        Friend WithEvents CB_NODE As System.Windows.Forms.ComboBox
        Friend WithEvents CB_FREE As System.Windows.Forms.CheckBox
        Friend WithEvents CB_USED As System.Windows.Forms.CheckBox
        Friend WithEvents TB_SCMD As System.Windows.Forms.ComboBox
        Friend WithEvents TB_KEY As System.Windows.Forms.ComboBox
        Friend WithEvents TB_KEY2 As System.Windows.Forms.ComboBox
        Friend WithEvents BT_WREQ As System.Windows.Forms.Button
        Friend WithEvents BT_FREQ As System.Windows.Forms.Button
        Friend WithEvents BT_FRSP As System.Windows.Forms.Button
        Friend WithEvents BT_WRSP As System.Windows.Forms.Button
        Friend WithEvents BT_FATIO As System.Windows.Forms.Button
        Friend WithEvents BT_WATIO As System.Windows.Forms.Button
        Friend WithEvents TB_CBN As System.Windows.Forms.TextBox
        Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
        Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
        Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
        Friend WithEvents Label3 As System.Windows.Forms.Label
        Friend WithEvents TB_CLN As System.Windows.Forms.TextBox

End Class
