<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FM_CFG
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
                Me.RTB_CFG = New System.Windows.Forms.RichTextBox()
                Me.BT_OK = New System.Windows.Forms.Button()
                Me.BT_CANCEL = New System.Windows.Forms.Button()
                Me.SuspendLayout()
                '
                'RTB_CFG
                '
                Me.RTB_CFG.Location = New System.Drawing.Point(5, 12)
                Me.RTB_CFG.Name = "RTB_CFG"
                Me.RTB_CFG.Size = New System.Drawing.Size(249, 581)
                Me.RTB_CFG.TabIndex = 0
                Me.RTB_CFG.Text = ""
                '
                'BT_OK
                '
                Me.BT_OK.Location = New System.Drawing.Point(277, 533)
                Me.BT_OK.Name = "BT_OK"
                Me.BT_OK.Size = New System.Drawing.Size(75, 23)
                Me.BT_OK.TabIndex = 1
                Me.BT_OK.Text = "OK"
                Me.BT_OK.UseVisualStyleBackColor = True
                '
                'BT_CANCEL
                '
                Me.BT_CANCEL.Location = New System.Drawing.Point(277, 562)
                Me.BT_CANCEL.Name = "BT_CANCEL"
                Me.BT_CANCEL.Size = New System.Drawing.Size(75, 23)
                Me.BT_CANCEL.TabIndex = 2
                Me.BT_CANCEL.Text = "Cancel"
                Me.BT_CANCEL.UseVisualStyleBackColor = True
                '
                'FM_CFG
                '
                Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
                Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
                Me.ClientSize = New System.Drawing.Size(364, 597)
                Me.Controls.Add(Me.BT_CANCEL)
                Me.Controls.Add(Me.BT_OK)
                Me.Controls.Add(Me.RTB_CFG)
                Me.Name = "FM_CFG"
                Me.Text = "Configuration"
                Me.ResumeLayout(False)

        End Sub
        Friend WithEvents RTB_CFG As System.Windows.Forms.RichTextBox
        Friend WithEvents BT_OK As System.Windows.Forms.Button
        Friend WithEvents BT_CANCEL As System.Windows.Forms.Button
End Class
